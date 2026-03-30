namespace Apeiron.Analysis;

/// <summary>
/// Представляет метод, выполняющий преобразование спектра.
/// </summary>
/// <param name="frequency">
/// Частота, соответствующая амплитуде.
/// </param>
/// <param name="amplitude">
/// Амплитуда.
/// </param>
/// <returns>
/// Преобразованное значение амплитуды.
/// </returns>
public delegate Complex SpectrumReformer(double frequency, Complex amplitude);
