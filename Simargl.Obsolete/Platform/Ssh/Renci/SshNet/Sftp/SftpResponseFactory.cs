using System;
using System.Globalization;
using System.Text;

using Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp;

internal sealed class SftpResponseFactory : ISftpResponseFactory
{
    public SftpMessage Create(uint protocolVersion, byte messageType, Encoding encoding)
    {
        var sftpMessageType = (SftpMessageTypes) messageType;
        SftpMessage? message = sftpMessageType switch
        {
            SftpMessageTypes.Version => new SftpVersionResponse(),
            SftpMessageTypes.Status => new SftpStatusResponse(protocolVersion),
            SftpMessageTypes.Data => new SftpDataResponse(protocolVersion),
            SftpMessageTypes.Handle => new SftpHandleResponse(protocolVersion),
            SftpMessageTypes.Name => new SftpNameResponse(protocolVersion, encoding),
            SftpMessageTypes.Attrs => new SftpAttrsResponse(protocolVersion),
            SftpMessageTypes.ExtendedReply => new SftpExtendedReplyResponse(protocolVersion),
            _ => throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Message type '{0}' is not supported.", sftpMessageType)),
        };
        return message;
    }
}
