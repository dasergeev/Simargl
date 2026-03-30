
#pragma warning disable CS8622 // Допустимость значений NULL для ссылочных типов в типе параметра не соответствует целевому объекту делегирования (возможно, из-за атрибутов допустимости значений NULL).
#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;

using Simargl.Zero.Ssh.Renci.SshNet.Abstractions;
using Simargl.Zero.Ssh.Renci.SshNet.Common;
using Simargl.Zero.Ssh.Renci.SshNet.Messages;
using Simargl.Zero.Ssh.Renci.SshNet.Messages.Authentication;

namespace Simargl.Zero.Ssh.Renci.SshNet;

/// <summary>
/// Provides functionality to perform keyboard interactive authentication.
/// </summary>
internal class KeyboardInteractiveAuthenticationMethod : AuthenticationMethod, IDisposable
{
    private readonly RequestMessage _requestMessage;
    private AuthenticationResult _authenticationResult = AuthenticationResult.Failure;
    private Session _session;
    private EventWaitHandle _authenticationCompleted = new AutoResetEvent(initialState: false);
    private Exception _exception;
    private bool _isDisposed;

    /// <summary>
    /// Gets the name of the authentication method.
    /// </summary>
    /// <value>
    /// The name of the authentication method.
    /// </value>
    public override string Name
    {
        get { return _requestMessage.MethodName; }
    }

    /// <summary>
    /// Occurs when server prompts for more authentication information.
    /// </summary>
    public event EventHandler<AuthenticationPromptEventArgs> AuthenticationPrompt;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardInteractiveAuthenticationMethod"/> class.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <exception cref="ArgumentException"><paramref name="username"/> is whitespace or <see langword="null"/>.</exception>
    public KeyboardInteractiveAuthenticationMethod(string username)
        : base(username)
    {
        _requestMessage = new RequestMessageKeyboardInteractive(ServiceName.Connection, username);
    }

    /// <summary>
    /// Authenticates the specified session.
    /// </summary>
    /// <param name="session">The session to authenticate.</param>
    /// <returns>Result of authentication  process.</returns>
    public override AuthenticationResult Authenticate(Session session)
    {
        _session = session;

        session.UserAuthenticationSuccessReceived += Session_UserAuthenticationSuccessReceived;
        session.UserAuthenticationFailureReceived += Session_UserAuthenticationFailureReceived;
        session.UserAuthenticationInformationRequestReceived += Session_UserAuthenticationInformationRequestReceived;

        session.RegisterMessage("SSH_MSG_USERAUTH_INFO_REQUEST");

        try
        {
            session.SendMessage(_requestMessage);
            session.WaitOnHandle(_authenticationCompleted);
        }
        finally
        {
            session.UnRegisterMessage("SSH_MSG_USERAUTH_INFO_REQUEST");
            session.UserAuthenticationSuccessReceived -= Session_UserAuthenticationSuccessReceived;
            session.UserAuthenticationFailureReceived -= Session_UserAuthenticationFailureReceived;
            session.UserAuthenticationInformationRequestReceived -= Session_UserAuthenticationInformationRequestReceived;
        }

        if (_exception != null)
        {
            ExceptionDispatchInfo.Capture(_exception).Throw();
        }

        return _authenticationResult;
    }

    private void Session_UserAuthenticationSuccessReceived(object sender, MessageEventArgs<SuccessMessage> e)
    {
        _authenticationResult = AuthenticationResult.Success;
        _ = _authenticationCompleted.Set();
    }

    private void Session_UserAuthenticationFailureReceived(object sender, MessageEventArgs<FailureMessage> e)
    {
        if (e.Message.PartialSuccess)
        {
            _authenticationResult = AuthenticationResult.PartialSuccess;
        }
        else
        {
            _authenticationResult = AuthenticationResult.Failure;
        }

        // Copy allowed authentication methods
        AllowedAuthentications = e.Message.AllowedAuthentications;

        _ = _authenticationCompleted.Set();
    }

    private void Session_UserAuthenticationInformationRequestReceived(object sender, MessageEventArgs<InformationRequestMessage> e)
    {
        var informationRequestMessage = e.Message;

        var eventArgs = new AuthenticationPromptEventArgs(Username,
                                                          informationRequestMessage.Instruction,
                                                          informationRequestMessage.Language,
                                                          informationRequestMessage.Prompts);

        ThreadAbstraction.ExecuteThread(() =>
            {
                try
                {
                    AuthenticationPrompt?.Invoke(this, eventArgs);

                    var informationResponse = new InformationResponseMessage();

                    foreach (var prompt in eventArgs.Prompts.OrderBy(r => r.Id))
                    {
                        if (prompt.Response is null)
                        {
                            throw new SshAuthenticationException(
                                $"{nameof(AuthenticationPrompt)}.{nameof(prompt.Response)} is null for " +
                                $"prompt \"{prompt.Request}\". You can set this by subscribing to " +
                                $"{nameof(KeyboardInteractiveAuthenticationMethod)}.{nameof(AuthenticationPrompt)} " +
                                $"and inspecting the {nameof(AuthenticationPromptEventArgs.Prompts)} property " +
                                $"of the event args.");
                        }

                        informationResponse.Responses.Add(prompt.Response);
                    }

                    // Send information response message
                    _session.SendMessage(informationResponse);
                }
                catch (Exception exp)
                {
                    _exception = exp;
                    _ = _authenticationCompleted.Set();
                }
            });
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (disposing)
        {
            var authenticationCompleted = _authenticationCompleted;
            if (authenticationCompleted != null)
            {
                _authenticationCompleted = null;
                authenticationCompleted.Dispose();
            }

            _isDisposed = true;
        }
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="KeyboardInteractiveAuthenticationMethod"/> class.
    /// </summary>
    ~KeyboardInteractiveAuthenticationMethod()
    {
        Dispose(disposing: false);
    }
}
