using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Device;

/// <summary>
/// Provides concurrency control across multiple Modbus readers/writers.
/// </summary>
public class ConcurrentModbusMaster :
    IConcurrentModbusMaster
{
    private readonly IModbusMaster _master;
    private readonly TimeSpan _minInterval;

    private bool _isDisposed;

    private readonly Stopwatch _stopwatch = new Stopwatch();

    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="master"></param>
    /// <param name="minInterval"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [CLSCompliant(false)]
    public ConcurrentModbusMaster(IModbusMaster master, TimeSpan minInterval)
    {
        _master = master ?? throw new ArgumentNullException(nameof(master));
        _minInterval = minInterval;

        _stopwatch.Start();
    }

    private Task WaitAsync(CancellationToken cancellationToken)
    {
        int difference = (int)(_minInterval - _stopwatch.Elapsed).TotalMilliseconds;

        if (difference > 0)
        {
            return Task.Delay(difference, cancellationToken);
        }

        return Task.CompletedTask;
    }

    private async Task<T> PerformFuncAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken)
    {
        T value = default!;

        await PerformAsync(async () => value = await action(), cancellationToken);

        return value;
    }

    private async Task PerformAsync(Func<Task> action, CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            await WaitAsync(cancellationToken);

            await action();
        }
        finally
        {
            _semaphore.Release();

            _stopwatch.Restart();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="numberOfPoints"></param>
    /// <param name="blockSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public async Task<ushort[]> ReadInputRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints, ushort blockSize, CancellationToken cancellationToken)
    {
        return await PerformFuncAsync(async ()  =>
        {
            List<ushort> registers = new List<ushort>(numberOfPoints);

            int soFar = 0;
            int thisRead = blockSize;

            while (soFar < numberOfPoints)
            {
                //If we're _not_ on the first run through here, wait for the min time
                if (soFar > 0)
                {
                    await Task.Delay(_minInterval, cancellationToken);
                }

                //Check to see if we've ben cancelled
                cancellationToken.ThrowIfCancellationRequested();

                if (thisRead > (numberOfPoints - soFar))
                {
                    thisRead = numberOfPoints - soFar;
                }

                //Perform this operation
                ushort[] registersFromThisRead = await _master.ReadInputRegistersAsync(slaveAddress, (ushort)(startAddress + soFar), (ushort)thisRead);

                //Add these to the result
                registers.AddRange(registersFromThisRead);

                //Increment where we're at
                soFar += thisRead;
            }

            return registers.ToArray();

        }, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="numberOfPoints"></param>
    /// <param name="blockSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints, ushort blockSize, CancellationToken cancellationToken)
    {
        return PerformFuncAsync(async () =>
        {
            List<ushort> registers = new List<ushort>(numberOfPoints);

            int soFar = 0;
            int thisRead = blockSize;

            while (soFar < numberOfPoints)
            {
                //If we're _not_ on the first run through here, wait for the min time
                if (soFar > 0)
                {
                    await Task.Delay(_minInterval, cancellationToken);
                }

                //Check to see if we've ben cancelled
                cancellationToken.ThrowIfCancellationRequested();

                if (thisRead > (numberOfPoints - soFar))
                {
                    thisRead = numberOfPoints - soFar;
                }

                //Perform this operation
                ushort[] registersFromThisRead = await _master.ReadHoldingRegistersAsync(slaveAddress, (ushort)(startAddress + soFar), (ushort)thisRead);

                //Add these to the result
                registers.AddRange(registersFromThisRead);

                //Increment where we're at
                soFar += thisRead;
            }

            return registers.ToArray();

        }, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="data"></param>
    /// <param name="blockSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public Task WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] data, ushort blockSize, CancellationToken cancellationToken)
    {
        return PerformAsync(async () =>
        {
            int soFar = 0;
            int thisWrite = blockSize;

            while (soFar < data.Length)
            {
                //If we're _not_ on the first run through here, wait for the min time
                if (soFar > 0)
                {
                    await Task.Delay(_minInterval, cancellationToken);
                }

                if (thisWrite > (data.Length - soFar))
                {
                    thisWrite = data.Length - soFar;
                }

                ushort[] registers = data.Skip(soFar).Take(thisWrite).ToArray();

                await _master.WriteMultipleRegistersAsync(slaveAddress, (ushort) (startAddress + soFar), registers);

                soFar += thisWrite;
            }

        }, cancellationToken);  
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="address"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public Task WriteSingleRegisterAsync(byte slaveAddress, ushort address, ushort value, CancellationToken cancellationToken)
    {
        return PerformAsync(() => _master.WriteSingleRegisterAsync(slaveAddress, address, value), cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="data"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public Task WriteCoilsAsync(byte slaveAddress, ushort startAddress, bool[] data, CancellationToken cancellationToken)
    {
        return PerformAsync(() => _master.WriteMultipleCoilsAsync(slaveAddress, startAddress, data),  cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="number"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public Task<bool[]> ReadCoilsAsync(byte slaveAddress, ushort startAddress, ushort number,
        CancellationToken cancellationToken)
    {
        return PerformFuncAsync(() => _master.ReadCoilsAsync(slaveAddress, startAddress, number), cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="number"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public Task<bool[]> ReadDiscretesAsync(byte slaveAddress, ushort startAddress, ushort number, CancellationToken cancellationToken)
    {
        return PerformFuncAsync(() => _master.ReadInputsAsync(slaveAddress, startAddress, number), cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="coilAddress"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public Task WriteSingleCoilAsync(byte slaveAddress, ushort coilAddress, bool value, CancellationToken cancellationToken)
    {
        return PerformAsync(() => _master.WriteSingleCoilAsync(slaveAddress, coilAddress, value), cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        if (!_isDisposed)
        {
            _isDisposed = true;

            _master.Dispose();
        }
    }
}
