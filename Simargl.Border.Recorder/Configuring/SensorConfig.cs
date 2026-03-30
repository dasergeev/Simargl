namespace Pulse.Configuring;

/// <summary>
/// Представляет конфигурацию датчика.
/// </summary>
/// <param name="RailroadCar">
/// Номер вагона.
/// </param>
/// <param name="Bogie">
/// Номер тележки.
/// </param>
/// <param name="Number">
/// Номер датчика на тележке.
/// </param>
/// <param name="Address">
/// Адрес датчика.
/// </param>
/// <param name="Orientation">
/// Ориентация датчика.
/// </param>
internal sealed record class SensorConfig(int RailroadCar, int Bogie, int Number, string Address, string Orientation);
