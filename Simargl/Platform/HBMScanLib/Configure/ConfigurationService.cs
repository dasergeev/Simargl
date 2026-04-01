// <copyright file="ConfigurationService.cs" company="Hottinger Baldwin Messtechnik GmbH">
//
// SharpScan, a library for scanning and configuring HBM devices.
//
// The MIT License (MIT)
//
// Copyright (C) Hottinger Baldwin Messtechnik GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
// BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
// ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// </copyright>

namespace Hbm.Devices.Scan.Configure
{
    using System;
    using System.Collections.Generic;
    using System.Timers;

    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationService
    {
        private readonly ConfigurationSerializer serializer;
        private readonly IMulticastSender sender;
        private readonly ResponseDeserializer parser;
        private readonly IDictionary<string, ConfigQuery> awaitingResponses;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="sender"></param>
        public ConfigurationService(ResponseDeserializer parser, IMulticastSender sender)
        {
            awaitingResponses = new Dictionary<string, ConfigQuery>();
            serializer = new ConfigurationSerializer();
            this.sender = sender;
            this.parser = parser;
            parser.HandleMessage += HandleEvent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="callbacks"></param>
        /// <param name="timeoutMs"></param>
        public void SendConfiguration(ConfigurationParams parameters, IConfigurationCallback callbacks, double timeoutMs)
        {
            Guid queryId = Guid.NewGuid();
            SendConfiguration(parameters, queryId.ToString(), callbacks, timeoutMs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="queryId"></param>
        /// <param name="callbacks"></param>
        /// <param name="timeoutMs"></param>
        public void SendConfiguration(ConfigurationParams parameters, string queryId, IConfigurationCallback callbacks, double timeoutMs)
        {
            
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (callbacks == null)
            {
                throw new ArgumentNullException(nameof(callbacks));
            }

            if (timeoutMs <= 0)
            {
                throw new ArgumentException("timeout");
            }

            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentException(null, nameof(queryId));
            }

            ConfigurationRequest request = new(parameters, queryId);
            ConfigurationTimer timer = new(timeoutMs, request);
            ConfigQuery query = new(request, callbacks, timer);
            lock (awaitingResponses)
            {
                awaitingResponses.Add(queryId, query);
                timer.AutoReset = false;
                timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                timer.Start();
            }

            sender.SendMessage(serializer.Serialize(request));
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            parser.HandleMessage -= HandleEvent;
            lock (awaitingResponses)
            {
                foreach (KeyValuePair<string, ConfigQuery> entry in awaitingResponses)
                {
                    ConfigurationTimer timer = entry.Value.Timer;
                    timer.Stop();
                }

                awaitingResponses.Clear();
            }

            sender.Close();
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            if(sender == null)
                return;

            ConfigurationTimer timer = (ConfigurationTimer)sender;
            lock (awaitingResponses)
            {
                if (awaitingResponses.TryGetValue(timer.Request.QueryId, out ConfigQuery? query))
                {
                    awaitingResponses.Remove(timer.Request.QueryId);
                    query.Callbacks.OnTimeout();
                }
            }
        }

        private void HandleEvent(object? sender, JsonRpcResponseEventArgs args)
        {
            JsonRpcResponse? response = args.Response;
            if ((response?.Result != null) || (response?.Error != null))
            {
                string queryId = response.Id;
                if (!string.IsNullOrEmpty(queryId))
                {
                    lock (awaitingResponses)
                    {
                        if (awaitingResponses.TryGetValue(queryId, out ConfigQuery? query))
                        {
                            awaitingResponses.Remove(queryId);
                            query.Timer.Stop();

                            if (response.Error != null)
                            {
                                query.Callbacks.OnError(response);
                            }
                            else
                            {
                                query.Callbacks.OnSuccess(response);
                            }
                        }
                    }
                }
            }
        }

        internal class ConfigQuery
        {
            internal ConfigQuery(ConfigurationRequest request, IConfigurationCallback callbacks, ConfigurationTimer timer)
            {
                Request = request;
                Callbacks = callbacks;
                Timer = timer;
            }

            internal ConfigurationRequest Request { get; set; }

            internal IConfigurationCallback Callbacks { get; set; }

            internal ConfigurationTimer Timer { get; set; }
        }

        internal class ConfigurationTimer : Timer
        {
            internal ConfigurationTimer(double expire, ConfigurationRequest request)
                : base(expire)
            {
                Request = request;
            }

            internal ConfigurationRequest Request { get; set; }
        }
    }
}
