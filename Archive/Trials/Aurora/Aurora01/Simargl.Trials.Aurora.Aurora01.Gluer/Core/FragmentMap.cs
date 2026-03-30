namespace Simargl.Trials.Aurora.Aurora01.Gluer.Core;

/// <summary>
/// Представляет карту фрагментов.
/// </summary>
public sealed class FragmentMap
{
    /// <summary>
    /// Возвращает карту кадров.
    /// </summary>
    public Fragment[] Frame { get; }

    /// <summary>
    /// Возвращает карту данных Adxl.
    /// </summary>
    public Fragment[][] Adxl { get; }

    private FragmentMap(Fragment[] frame, Fragment[][] adxl)
    {
        Frame = frame;
        Adxl = adxl;
    }

    /// <summary>
    /// Асинхронно создаёт карту.
    /// </summary>
    /// <param name="fileMap"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<FragmentMap> CreateAsync(FileMap fileMap, CancellationToken cancellationToken)
    {
        Fragment[] frame = await Fragment.ParseAsync(
            fileMap.Frame, GluerTunnings.FrameStep, GluerTunnings.FrameMinCount,
            -GluerTunnings.FrameMaxDisplacement, GluerTunnings.FrameMaxDisplacement,
            GluerTunnings.FrameMaxCompression, 1.0 / GluerTunnings.FrameMaxCompression,
            cancellationToken).ConfigureAwait(false);

        Fragment[][] adxl = new Fragment[fileMap.Adxl.Length][];
        for (int i = 0; i < adxl.Length; i++)
        {
            adxl[i] = await Fragment.ParseAsync(
                fileMap.Adxl[i], GluerTunnings.AdxlStep, GluerTunnings.AdxlMinCount,
                -GluerTunnings.AdxlMaxDisplacement, GluerTunnings.AdxlMaxDisplacement,
                GluerTunnings.AdxlMaxCompression, 1.0 / GluerTunnings.AdxlMaxCompression,
                cancellationToken).ConfigureAwait(false);
        }

        return new(frame, adxl);
    }
}
