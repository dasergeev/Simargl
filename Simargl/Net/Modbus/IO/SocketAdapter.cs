using Simargl.Net.Modbus.Unme.Common;

namespace Simargl.Net.Modbus.IO;

/// <summary>
///     Concrete Implementor - http://en.wikipedia.org/wiki/Bridge_Pattern
///     This implementation is for sockets that Convert Rs485 to Ethernet.
/// </summary>
public class SocketAdapter :
    IStreamResource
{
    private Socket _socketClient;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="socketClient"></param>
    public SocketAdapter(Socket socketClient)
    {
        Debug.Assert(socketClient != null, "Argument socketClient van not be null");
        _socketClient = socketClient;
    }

    /// <summary>
    /// 
    /// </summary>
    public int InfiniteTimeout => Timeout.Infinite;

    /// <summary>
    /// 
    /// </summary>
    public int ReadTimeout 
    { 
        get => _socketClient.SendTimeout;
        set => _socketClient.SendTimeout = value;

    }

    /// <summary>
    /// 
    /// </summary>
    public int WriteTimeout
    {
        get => _socketClient.ReceiveTimeout;
        set => _socketClient.ReceiveTimeout = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public void DiscardInBuffer()
    {
        // socket does not hold buffers.
        return;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public int Read(byte[] buffer, int offset, int size)
    {
        
        return _socketClient.Receive(buffer,offset,size,0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    public void Write(byte[] buffer, int offset, int size)
    {
        _socketClient.Send(buffer,offset,size,0);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DisposableUtility.Dispose(ref _socketClient);
        }
    }
}
