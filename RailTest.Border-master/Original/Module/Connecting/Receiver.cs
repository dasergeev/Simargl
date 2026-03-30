using RailTest.Memory;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RailTest.Border
{
    /// <summary>
    /// Представляет приёмник данных.
    /// </summary>
    internal unsafe class Receiver
    {
        /// <summary>
        /// Постоянная, определяющая порт, по которому осуществляется подключение.
        /// </summary>
        public const int ConnectionPort = 1337;
        
        /// <summary>
        /// Постоянная, определяющее количество ожидаемых подключений.
        /// </summary>
        public const int ClientsCount = 84;

        /// <summary>
        /// Постоянная, определяющая размер одного блока.
        /// </summary>
        private const int BlockSize = 50;

        /// <summary>
        /// Постоянная, определяющая размер буфера в блоках.
        /// </summary>
        private const int SizeInBlocks = 256;

        /// <summary>
        /// Постоянная определяющая размер блока для чтения.
        /// </summary>
        public const int ReadSize = 128;

        /// <summary>
        /// Постоянная определяющая количество блоков для четния.
        /// </summary>
        public const int ReadCount = 100;

        /// <summary>
        /// Поле для хранения потока, в котором выполняется работа.
        /// </summary>
        private readonly Thread _Thread;

        /// <summary>
        /// Поле для хранения значения, определяющего выполняется ли работа.
        /// </summary>
        private bool _IsWork;

        /// <summary>
        /// Поле для хранения массива флагов существования.
        /// </summary>
        private readonly bool[] _Exists;

        /// <summary>
        /// Поле для хранения массива клиентов.
        /// </summary>
        private readonly TcpClient[] _Clients;

        /// <summary>
        /// Поле для хранения массива средств чтения.
        /// </summary>
        private readonly BinaryReader[] _Readers;

        /// <summary>
        /// Поле для хранения буфера.
        /// </summary>
        private readonly double*[] _Buffer;

        /// <summary>
        /// Поле для хранения количества полученных блоков.
        /// </summary>
        private readonly long[] _Blocks;

        /// <summary>
        /// Поле для хранения массива индексов блоков.
        /// </summary>
        private readonly int[] _BlockIndices;

        /// <summary>
        /// Поле для хранения последнего записанного индекса блока.
        /// </summary>
        private int _BlockIndex;

        /// <summary>
        /// Поле для хранения массива маркеров.
        /// </summary>
        private readonly long[] _Markers;

        /// <summary>
        /// Поле для хранения времени запуска.
        /// </summary>
        private readonly DateTime[] _StartTime;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public Receiver()
        {
            _Thread = new Thread(ThreadEntry);

            _IsWork = true;
            _Exists = new bool[ClientsCount];
            _Clients = new TcpClient[ClientsCount];
            _Readers = new BinaryReader[ClientsCount];
            Active = 0;
            _Buffer = new double*[3 * ClientsCount];
            _Blocks = new long[ClientsCount];
            ReadIndex = 0;
            for (int i = 0; i != 3 * ClientsCount; ++i)
            {
                _Buffer[i] = (double*)MemoryManager.Alloc(SizeInBlocks * BlockSize * sizeof(double));
            }
            _BlockIndices = new int[ClientsCount];
            _BlockIndex = 0;
            _Markers = new long[ClientsCount];
            _StartTime = new DateTime[ClientsCount];

            _Thread.IsBackground = true;
            _Thread.Priority = ThreadPriority.Highest;
            _Thread.Start();
        }

        /// <summary>
        /// Разрушает объект.
        /// </summary>
        ~Receiver()
        {
            for (int i = 0; i != 3 * ClientsCount; ++i)
            {
                MemoryManager.Free(_Buffer[i]);
            }
        }

        /// <summary>
        /// Возвращает количество активных подключений.
        /// </summary>
        public int Active { get; private set; }

        /// <summary>
        /// Возвращает последний индекс доступный для чтения.
        /// </summary>
        public int ReadIndex { get; private set; }

        /// <summary>
        /// Возвращает значение, определяющее активно ли подключение с указанным индексом.
        /// </summary>
        /// <param name="connectionIndex">
        /// Индекс подключения.
        /// </param>
        /// <returns>
        /// Значение, определяющее активно ли подключение.
        /// </returns>
        public bool GetConnected(int connectionIndex)
        {
            return _Exists[connectionIndex];
        }

        /// <summary>
        /// Возвращает последний маркер.
        /// </summary>
        /// <param name="connectionIndex">
        /// Индекс подключения.
        /// </param>
        /// <returns>
        /// Маркер.
        /// </returns>
        public long GetMarker(int connectionIndex)
        {
            return _Markers[connectionIndex];
        }

        /// <summary>
        /// Возвращает количество полученных блоков.
        /// </summary>
        /// <param name="connectionIndex">
        /// Индекс подключения.
        /// </param>
        /// <returns>
        /// Количество полученных блоков.
        /// </returns>
        public long GetBlocks(int connectionIndex)
        {
            return _Blocks[connectionIndex];
        }

        /// <summary>
        /// Возвращает индекс блока.
        /// </summary>
        /// <param name="connectionIndex">
        /// Индекс подключения.
        /// </param>
        /// <returns>
        /// Индекс блока.
        /// </returns>
        public int GetBlockIndex(int connectionIndex)
        {
            return _BlockIndices[connectionIndex];
        }

        /// <summary>
        /// Возвращает время запуска.
        /// </summary>
        /// <param name="connectionIndex">
        /// Индекс подключения.
        /// </param>
        /// <returns>
        /// Время запуска.
        /// </returns>
        public DateTime GetStartTime(int connectionIndex)
        {
            return _StartTime[connectionIndex];
        }

        /// <summary>
        /// Выполняет чтение данных.
        /// </summary>
        /// <param name="readIndex">
        /// Индекс блока.
        /// </param>
        /// <param name="channelIndex">
        /// Индекс канала.
        /// </param>
        /// <param name="data">
        /// Указатель на область памяти, в которую необходимо записать данные.
        /// </param>
        public void Read(int readIndex, int channelIndex, double* data)
        {
            double* source = _Buffer[channelIndex] + readIndex * ReadSize;
            MemoryManager.Copy(data, source, ReadSize * sizeof(double));
            MemoryManager.Zero(source, ReadSize * sizeof(double));
        }

        /// <summary>
        /// Получает новые клиенты.
        /// </summary>
        /// <param name="listener">
        /// Средство прослушивания сети.
        /// </param>
        /// <returns>
        /// Значение, определяющее, были ли получен хотя бы один новый клиент.
        /// </returns>
        private bool AcceptClients(TcpListener listener)
        {
            bool result = false;
            while (listener.Pending() && _IsWork)
            {
                TcpClient client = listener.AcceptTcpClient();
                int index;
                try
                {
                    //index = int.Parse(client.Client.RemoteEndPoint.ToString()
                    //    .Replace("192.168.1.", "").Replace($":{ConnectionPort}", "")) - 2;
                    string address = client.Client.RemoteEndPoint.ToString().Replace($":{ConnectionPort}", "");
                    index = ModuleAddresses.GetIndex(address);
                    if (index >= 0 && index < ClientsCount)
                    {
                        if (_Exists[index])
                        {
                            _Readers[index].Dispose();
                            _Clients[index].Close();
                            --Active;
                        }
                        _Exists[index] = true;
                        _Clients[index] = client;
                        _Readers[index] = new BinaryReader(client.GetStream());
                        _Blocks[index] = 0;
                        _StartTime[index] = DateTime.Now;
                        ++Active;
                        result = true;
                    }
                }
                catch (FormatException)
                {

                }
                catch (OverflowException)
                {

                }
            }
            return result;
        }

        /// <summary>
        /// Получает новые данные.
        /// </summary>
        /// <returns>
        /// Значение, определяющее, были ли получены новые данные.
        /// </returns>
        private bool ReceiveData()
        {
            bool result = false;
            for (int i = 0; i != ClientsCount; ++i)
            {
                if (!_IsWork)
                {
                    break;
                }

                if (_Exists[i])
                {
                    TcpClient client = _Clients[i];
                    BinaryReader reader = _Readers[i];
                    double* data0 = _Buffer[3 * i + 0];
                    double* data1 = _Buffer[3 * i + 1];
                    double* data2 = _Buffer[3 * i + 2];
                    if (client.Connected)
                    {
                        while (client.Available >= 611 && _IsWork)
                        {
                            result = true;
                            reader.ReadBytes(3);
                            uint marker = reader.ReadUInt32();
                            _Markers[i] = marker;

                            int baseIndex = (int)(marker % SizeInBlocks);
                            _BlockIndices[i] = baseIndex;
                            baseIndex *= 50;

                            for (int j = 0; j != 50; ++j)
                            {
                                int index = baseIndex + j;
                                data0[index] = reader.ReadInt32();
                                data1[index] = reader.ReadInt32();
                                data2[index] = reader.ReadInt32();
                            }

                            if (reader.ReadUInt32() != 0xffffffff)
                            {
                                throw new InvalidOperationException("Ошибка синхронизации.");
                            }

                            _Blocks[i] = (_Blocks[i] + 1) % uint.MaxValue;
                            if (_Blocks[i] == 100)
                            {
                                _StartTime[i] = DateTime.Now;
                            }
                        }
                    }
                    else
                    {
                        _Exists[i] = false;
                        _Readers[i].Dispose();
                        _Clients[i].Close();
                        --Active;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Определение последнего записанного индекса.
        /// </summary>
        private void IndexDefinition()
        {
            for (int i = 0; i != ClientsCount; ++i)
            {
                int index = _BlockIndices[i];
                if (_BlockIndex != index)
                {
                    if (_BlockIndex < index)
                    {
                        if (index - _BlockIndex < SizeInBlocks >> 1)
                        {
                            _BlockIndex = index;
                        }
                    }
                    else
                    {
                        if (_BlockIndex - index > SizeInBlocks >> 1)
                        {
                            _BlockIndex = index;
                        }
                    }
                }
            }
            ReadIndex = ((_BlockIndex * BlockSize / ReadSize) + ReadCount - 2) % ReadCount;
        }

        /// <summary>
        /// Представляет точку входа потока.
        /// </summary>
        private void ThreadEntry()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, ConnectionPort);
            try
            {
                listener.Start();

                while (_IsWork)
                {
                    bool isSleep = !AcceptClients(listener);
                    isSleep = isSleep && !ReceiveData();
                    IndexDefinition();

                    if (isSleep)
                    {
                        Thread.Sleep(10);
                    }
                }
            }
            catch (InvalidOperationException)
            {

            }
            catch (SocketException)
            {

            }
            finally
            {
                for (int i = 0; i != ClientsCount; ++i)
                {
                    if (_Exists[i])
                    {
                        _Exists[i] = false;
                        _Readers[i].Dispose();
                        _Clients[i].Close();
                        --Active;
                    }
                }
                try
                {
                    listener.Stop();
                }
                catch (SocketException)
                {

                }
            }
            if (_IsWork)
            {
                ThreadEntry();
            }
        }

        /// <summary>
        /// Останавливает работу.
        /// </summary>
        public void Stop()
        {
            _IsWork = false;
            bool result = false;
            try
            {
                result = _Thread.Join(1000);
            }
            catch (ThreadStateException)
            {

            }
            if (!result)
            {
                try
                {
                    _Thread.Abort();
                }
                catch (ThreadStateException)
                {

                }
            }
        }
    }
}
