//using System;

//namespace RailTest.Hardware.Hbm.Core
//{
//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.ActiveSupplyType"/>.
//	/// </summary>
//	public enum HbmCoreActiveSupplyType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ActiveSupplyType.Voltage"/>.
//		/// </summary>
//		Voltage = (int)global::Hbm.Api.SensorDB.Enums.ActiveSupplyType.Voltage,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ActiveSupplyType.Current"/>.
//		/// </summary>
//		Current = (int)global::Hbm.Api.SensorDB.Enums.ActiveSupplyType.Current,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode"/>.
//	/// </summary>
//	public enum HbmCoreAnalogOutMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.Fixed0V"/>.
//		/// </summary>
//		Fixed0V = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.Fixed0V,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.Adjustable"/>.
//		/// </summary>
//		Adjustable = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.Adjustable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToSignal"/>.
//		/// </summary>
//		LinkedToSignal = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToSignal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator1"/>.
//		/// </summary>
//		LinkedToFunctionGenerator1 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator1,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator2"/>.
//		/// </summary>
//		LinkedToFunctionGenerator2 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator2,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator3"/>.
//		/// </summary>
//		LinkedToFunctionGenerator3 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator3,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator4"/>.
//		/// </summary>
//		LinkedToFunctionGenerator4 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator4,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator5"/>.
//		/// </summary>
//		LinkedToFunctionGenerator5 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator5,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator6"/>.
//		/// </summary>
//		LinkedToFunctionGenerator6 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator6,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator7"/>.
//		/// </summary>
//		LinkedToFunctionGenerator7 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator7,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator8"/>.
//		/// </summary>
//		LinkedToFunctionGenerator8 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator8,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator9"/>.
//		/// </summary>
//		LinkedToFunctionGenerator9 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator9,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator10"/>.
//		/// </summary>
//		LinkedToFunctionGenerator10 = (int)global::Hbm.Api.Mgc.Enums.AnalogOutMode.LinkedToFunctionGenerator10,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.AutoCalibrationMode"/>.
//	/// </summary>
//	public enum HbmCoreAutoCalibrationMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.AutoCalibrationMode.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.AutoCalibrationMode.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.AutoCalibrationMode.Default"/>.
//		/// </summary>
//		Default = (int)global::Hbm.Api.Common.Enums.AutoCalibrationMode.Default,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.AutoCalibrationMode.AutoAdjust"/>.
//		/// </summary>
//		AutoAdjust = (int)global::Hbm.Api.Common.Enums.AutoCalibrationMode.AutoAdjust,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.AutoCalibrationMode.AutoCalibrate"/>.
//		/// </summary>
//		AutoCalibrate = (int)global::Hbm.Api.Common.Enums.AutoCalibrationMode.AutoCalibrate,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.AutoCalibrationMode.Off"/>.
//		/// </summary>
//		Off = (int)global::Hbm.Api.Common.Enums.AutoCalibrationMode.Off,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.BitSequence"/>.
//	/// </summary>
//	public enum HbmCoreBitSequence
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BitSequence.Intel"/>.
//		/// </summary>
//		Intel = (int)global::Hbm.Api.SensorDB.Enums.BitSequence.Intel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BitSequence.Motorola"/>.
//		/// </summary>
//		Motorola = (int)global::Hbm.Api.SensorDB.Enums.BitSequence.Motorola,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring"/>.
//	/// </summary>
//	public enum HbmCoreBridgeSensorWiring
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.NotSet"/>.
//		/// </summary>
//		NotSet = (int)global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.NotSet,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.ThreeWire"/>.
//		/// </summary>
//		ThreeWire = (int)global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.ThreeWire,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.FourWire"/>.
//		/// </summary>
//		FourWire = (int)global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.FourWire,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.FiveWire"/>.
//		/// </summary>
//		FiveWire = (int)global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.FiveWire,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.SixWire"/>.
//		/// </summary>
//		SixWire = (int)global::Hbm.Api.SensorDB.Enums.BridgeSensorWiring.SixWire,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.BridgeType"/>.
//	/// </summary>
//	public enum HbmCoreBridgeType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BridgeType.FullBridge"/>.
//		/// </summary>
//		FullBridge = (int)global::Hbm.Api.SensorDB.Enums.BridgeType.FullBridge,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BridgeType.HalfBridge"/>.
//		/// </summary>
//		HalfBridge = (int)global::Hbm.Api.SensorDB.Enums.BridgeType.HalfBridge,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.BridgeType.QuarterBridge"/>.
//		/// </summary>
//		QuarterBridge = (int)global::Hbm.Api.SensorDB.Enums.BridgeType.QuarterBridge,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.ByteOrder"/>.
//	/// </summary>
//	public enum HbmCoreByteOrder
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ByteOrder.Intel"/>.
//		/// </summary>
//		Intel = (int)global::Hbm.Api.Mgc.Enums.ByteOrder.Intel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ByteOrder.Motorola"/>.
//		/// </summary>
//		Motorola = (int)global::Hbm.Api.Mgc.Enums.ByteOrder.Motorola,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.CanSignalType"/>.
//	/// </summary>
//	public enum HbmCoreCanSignalType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.CanSignalType.Standard"/>.
//		/// </summary>
//		Standard = (int)global::Hbm.Api.SensorDB.Enums.CanSignalType.Standard,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.CanSignalType.ModeDependent"/>.
//		/// </summary>
//		ModeDependent = (int)global::Hbm.Api.SensorDB.Enums.CanSignalType.ModeDependent,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.CanSignalType.Mode"/>.
//		/// </summary>
//		Mode = (int)global::Hbm.Api.SensorDB.Enums.CanSignalType.Mode,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.CarrierFrequencyType"/>.
//	/// </summary>
//	public enum HbmCoreCarrierFrequencyType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.CarrierFrequencyType.Auto"/>.
//		/// </summary>
//		Auto = (int)global::Hbm.Api.SensorDB.Enums.CarrierFrequencyType.Auto,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.CarrierFrequencyType.AutoAc"/>.
//		/// </summary>
//		AutoAc = (int)global::Hbm.Api.SensorDB.Enums.CarrierFrequencyType.AutoAc,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.CarrierFrequencyType.Dc"/>.
//		/// </summary>
//		Dc = (int)global::Hbm.Api.SensorDB.Enums.CarrierFrequencyType.Dc,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.CarrierFrequencyType.Value"/>.
//		/// </summary>
//		Value = (int)global::Hbm.Api.SensorDB.Enums.CarrierFrequencyType.Value,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.ChangeReason"/>.
//	/// </summary>
//	public enum HbmCoreChangeReason
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ChangeReason.DependentChannel"/>.
//		/// </summary>
//		DependentChannel = (int)global::Hbm.Api.Common.Enums.ChangeReason.DependentChannel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ChangeReason.Teds"/>.
//		/// </summary>
//		Teds = (int)global::Hbm.Api.Common.Enums.ChangeReason.Teds,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ChangeReason.Unspecific"/>.
//		/// </summary>
//		Unspecific = (int)global::Hbm.Api.Common.Enums.ChangeReason.Unspecific,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.ChannelConnectedValueType"/>.
//	/// </summary>
//	public enum HbmCoreChannelConnectedValueType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ChannelConnectedValueType.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.ChannelConnectedValueType.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ChannelConnectedValueType.NotConnected"/>.
//		/// </summary>
//		NotConnected = (int)global::Hbm.Api.Common.Enums.ChannelConnectedValueType.NotConnected,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ChannelConnectedValueType.IsConnected"/>.
//		/// </summary>
//		IsConnected = (int)global::Hbm.Api.Common.Enums.ChannelConnectedValueType.IsConnected,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.ChannelOverflowValueType"/>.
//	/// </summary>
//	public enum HbmCoreChannelOverflowValueType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ChannelOverflowValueType.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.ChannelOverflowValueType.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ChannelOverflowValueType.NoMeasValOverflow"/>.
//		/// </summary>
//		NoMeasValOverflow = (int)global::Hbm.Api.Common.Enums.ChannelOverflowValueType.NoMeasValOverflow,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ChannelOverflowValueType.MeasValOverflow"/>.
//		/// </summary>
//		MeasValOverflow = (int)global::Hbm.Api.Common.Enums.ChannelOverflowValueType.MeasValOverflow,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType"/>.
//	/// </summary>
//	public enum HbmCoreConnectionBoardType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.Unknown"/>.
//		/// </summary>
//		Unknown = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.Unknown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.NoAP"/>.
//		/// </summary>
//		NoAP = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.NoAP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP01"/>.
//		/// </summary>
//		AP01 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP01,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP0506"/>.
//		/// </summary>
//		AP0506 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP0506,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP08"/>.
//		/// </summary>
//		AP08 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP08,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP14"/>.
//		/// </summary>
//		AP14 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP14,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP18"/>.
//		/// </summary>
//		AP18 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP18,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP09"/>.
//		/// </summary>
//		AP09 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP09,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP801"/>.
//		/// </summary>
//		AP801 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP801,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP809"/>.
//		/// </summary>
//		AP809 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP809,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP835"/>.
//		/// </summary>
//		AP835 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP835,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP409"/>.
//		/// </summary>
//		AP409 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP409,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP4092"/>.
//		/// </summary>
//		AP4092 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP4092,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP810"/>.
//		/// </summary>
//		AP810 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP810,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP814"/>.
//		/// </summary>
//		AP814 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP814,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP71"/>.
//		/// </summary>
//		AP71 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP71,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP72"/>.
//		/// </summary>
//		AP72 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP72,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP75"/>.
//		/// </summary>
//		AP75 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP75,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP77"/>.
//		/// </summary>
//		AP77 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP77,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP78"/>.
//		/// </summary>
//		AP78 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP78,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP815"/>.
//		/// </summary>
//		AP815 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP815,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP401"/>.
//		/// </summary>
//		AP401 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP401,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP4012"/>.
//		/// </summary>
//		AP4012 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP4012,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP836"/>.
//		/// </summary>
//		AP836 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP836,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP460"/>.
//		/// </summary>
//		AP460 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP460,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP801S6"/>.
//		/// </summary>
//		AP801S6 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP801S6,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP814B"/>.
//		/// </summary>
//		AP814B = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP814B,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP801S41"/>.
//		/// </summary>
//		AP801S41 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP801S41,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP418"/>.
//		/// </summary>
//		AP418 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP418,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP4182"/>.
//		/// </summary>
//		AP4182 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP4182,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP17"/>.
//		/// </summary>
//		AP17 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP17,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP01i"/>.
//		/// </summary>
//		AP01i = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP01i,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP03i"/>.
//		/// </summary>
//		AP03i = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP03i,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP11i"/>.
//		/// </summary>
//		AP11i = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP11i,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP74"/>.
//		/// </summary>
//		AP74 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP74,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP18i"/>.
//		/// </summary>
//		AP18i = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP18i,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP13i"/>.
//		/// </summary>
//		AP13i = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP13i,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP455"/>.
//		/// </summary>
//		AP455 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP455,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP402"/>.
//		/// </summary>
//		AP402 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP402,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP836i"/>.
//		/// </summary>
//		AP836i = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP836i,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP816"/>.
//		/// </summary>
//		AP816 = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.AP816,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.APTEST"/>.
//		/// </summary>
//		APTEST = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.APTEST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.APEINMESS"/>.
//		/// </summary>
//		APEINMESS = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.APEINMESS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ConnectionBoardType.APWRONG"/>.
//		/// </summary>
//		APWRONG = (int)global::Hbm.Api.Mgc.Enums.ConnectionBoardType.APWRONG,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.ConnectorParamBusyValueType"/>.
//	/// </summary>
//	public enum HbmCoreConnectorParamBusyValueType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ConnectorParamBusyValueType.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.ConnectorParamBusyValueType.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ConnectorParamBusyValueType.NotBusy"/>.
//		/// </summary>
//		NotBusy = (int)global::Hbm.Api.Common.Enums.ConnectorParamBusyValueType.NotBusy,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ConnectorParamBusyValueType.Busy"/>.
//		/// </summary>
//		Busy = (int)global::Hbm.Api.Common.Enums.ConnectorParamBusyValueType.Busy,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.ConnectorType"/>.
//	/// </summary>
//	public enum HbmCoreConnectorType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.ConnectorType.AnalogInChannel"/>.
//		/// </summary>
//		AnalogInChannel = (int)global::Hbm.Api.QuantumX.Enums.ConnectorType.AnalogInChannel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.ConnectorType.CanBus"/>.
//		/// </summary>
//		CanBus = (int)global::Hbm.Api.QuantumX.Enums.ConnectorType.CanBus,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.CurrentType"/>.
//	/// </summary>
//	public enum HbmCoreCurrentType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.CurrentType.DC"/>.
//		/// </summary>
//		DC = (int)global::Hbm.Api.SensorDB.Enums.CurrentType.DC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.CurrentType.AC"/>.
//		/// </summary>
//		AC = (int)global::Hbm.Api.SensorDB.Enums.CurrentType.AC,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.DataAcquisitionMode"/>.
//	/// </summary>
//	public enum HbmCoreDataAcquisitionMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DataAcquisitionMode.TimestampSynchronized"/>.
//		/// </summary>
//		TimestampSynchronized = (int)global::Hbm.Api.Common.Enums.DataAcquisitionMode.TimestampSynchronized,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DataAcquisitionMode.HardwareSynchronized"/>.
//		/// </summary>
//		HardwareSynchronized = (int)global::Hbm.Api.Common.Enums.DataAcquisitionMode.HardwareSynchronized,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DataAcquisitionMode.Unsynchronized"/>.
//		/// </summary>
//		Unsynchronized = (int)global::Hbm.Api.Common.Enums.DataAcquisitionMode.Unsynchronized,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DataAcquisitionMode.Auto"/>.
//		/// </summary>
//		Auto = (int)global::Hbm.Api.Common.Enums.DataAcquisitionMode.Auto,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.DataFormat"/>.
//	/// </summary>
//	public enum HbmCoreDataFormat
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DataFormat.SignedInt"/>.
//		/// </summary>
//		SignedInt = (int)global::Hbm.Api.Mgc.Enums.DataFormat.SignedInt,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DataFormat.UnsignedInt"/>.
//		/// </summary>
//		UnsignedInt = (int)global::Hbm.Api.Mgc.Enums.DataFormat.UnsignedInt,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DataFormat.Float"/>.
//		/// </summary>
//		Float = (int)global::Hbm.Api.Mgc.Enums.DataFormat.Float,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.DataRateDomainType"/>.
//	/// </summary>
//	public enum HbmCoreDataRateDomainType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DataRateDomainType.Classic"/>.
//		/// </summary>
//		Classic = (int)global::Hbm.Api.Common.Enums.DataRateDomainType.Classic,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DataRateDomainType.Decimal"/>.
//		/// </summary>
//		Decimal = (int)global::Hbm.Api.Common.Enums.DataRateDomainType.Decimal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DataRateDomainType.Binary"/>.
//		/// </summary>
//		Binary = (int)global::Hbm.Api.Common.Enums.DataRateDomainType.Binary,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.DelayMechanism"/>.
//	/// </summary>
//	public enum HbmCoreDelayMechanism
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DelayMechanism.Auto"/>.
//		/// </summary>
//		Auto = (int)global::Hbm.Api.Common.Enums.DelayMechanism.Auto,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DelayMechanism.E2E"/>.
//		/// </summary>
//		E2E = (int)global::Hbm.Api.Common.Enums.DelayMechanism.E2E,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DelayMechanism.P2P"/>.
//		/// </summary>
//		P2P = (int)global::Hbm.Api.Common.Enums.DelayMechanism.P2P,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.DeviceBusyValueType"/>.
//	/// </summary>
//	public enum HbmCoreDeviceBusyValueType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DeviceBusyValueType.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.DeviceBusyValueType.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DeviceBusyValueType.NotBusy"/>.
//		/// </summary>
//		NotBusy = (int)global::Hbm.Api.Common.Enums.DeviceBusyValueType.NotBusy,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DeviceBusyValueType.Busy"/>.
//		/// </summary>
//		Busy = (int)global::Hbm.Api.Common.Enums.DeviceBusyValueType.Busy,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.DeviceSynchronization"/>.
//	/// </summary>
//	public enum HbmCoreDeviceSynchronization
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DeviceSynchronization.NotSynchrnonized"/>.
//		/// </summary>
//		NotSynchrnonized = (int)global::Hbm.Api.Mgc.Enums.DeviceSynchronization.NotSynchrnonized,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DeviceSynchronization.CableSynchronized"/>.
//		/// </summary>
//		CableSynchronized = (int)global::Hbm.Api.Mgc.Enums.DeviceSynchronization.CableSynchronized,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DeviceSynchronization.PtpSynchonized"/>.
//		/// </summary>
//		PtpSynchonized = (int)global::Hbm.Api.Mgc.Enums.DeviceSynchronization.PtpSynchonized,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DeviceSynchronization.IrigSynchronized"/>.
//		/// </summary>
//		IrigSynchronized = (int)global::Hbm.Api.Mgc.Enums.DeviceSynchronization.IrigSynchronized,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DeviceSynchronization.NtpSynchronized"/>.
//		/// </summary>
//		NtpSynchronized = (int)global::Hbm.Api.Mgc.Enums.DeviceSynchronization.NtpSynchronized,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.DigitalInputType"/>.
//	/// </summary>
//	public enum HbmCoreDigitalInputType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.DigitalInputType.Differential"/>.
//		/// </summary>
//		Differential = (int)global::Hbm.Api.SensorDB.Enums.DigitalInputType.Differential,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.DigitalInputType.SingleEnded"/>.
//		/// </summary>
//		SingleEnded = (int)global::Hbm.Api.SensorDB.Enums.DigitalInputType.SingleEnded,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.DigitalIoInputThreshold"/>.
//	/// </summary>
//	public enum HbmCoreDigitalIoInputThreshold
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.DigitalIoInputThreshold.Low"/>.
//		/// </summary>
//		Low = (int)global::Hbm.Api.QuantumX.Enums.DigitalIoInputThreshold.Low,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.DigitalIoInputThreshold.High"/>.
//		/// </summary>
//		High = (int)global::Hbm.Api.QuantumX.Enums.DigitalIoInputThreshold.High,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.DigitalOutMode"/>.
//	/// </summary>
//	public enum HbmCoreDigitalOutMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DigitalOutMode.LogicalCombination"/>.
//		/// </summary>
//		LogicalCombination = (int)global::Hbm.Api.Mgc.Enums.DigitalOutMode.LogicalCombination,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DigitalOutMode.Adjustable"/>.
//		/// </summary>
//		Adjustable = (int)global::Hbm.Api.Mgc.Enums.DigitalOutMode.Adjustable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.DigitalOutMode.LinkedToSignal"/>.
//		/// </summary>
//		LinkedToSignal = (int)global::Hbm.Api.Mgc.Enums.DigitalOutMode.LinkedToSignal,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.DigitalValueType"/>.
//	/// </summary>
//	public enum HbmCoreDigitalValueType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DigitalValueType.Low"/>.
//		/// </summary>
//		Low = (int)global::Hbm.Api.Common.Enums.DigitalValueType.Low,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DigitalValueType.High"/>.
//		/// </summary>
//		High = (int)global::Hbm.Api.Common.Enums.DigitalValueType.High,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.DirectionType"/>.
//	/// </summary>
//	public enum HbmCoreDirectionType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DirectionType.In"/>.
//		/// </summary>
//		In = (int)global::Hbm.Api.Common.Enums.DirectionType.In,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.DirectionType.Out"/>.
//		/// </summary>
//		Out = (int)global::Hbm.Api.Common.Enums.DirectionType.Out,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.DischargeTimeType"/>.
//	/// </summary>
//	public enum HbmCoreDischargeTimeType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.DischargeTimeType.Slow"/>.
//		/// </summary>
//		Slow = (int)global::Hbm.Api.SensorDB.Enums.DischargeTimeType.Slow,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.DischargeTimeType.Medium"/>.
//		/// </summary>
//		Medium = (int)global::Hbm.Api.SensorDB.Enums.DischargeTimeType.Medium,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.DischargeTimeType.Fast"/>.
//		/// </summary>
//		Fast = (int)global::Hbm.Api.SensorDB.Enums.DischargeTimeType.Fast,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.ErrorResetMode"/>.
//	/// </summary>
//	public enum HbmCoreErrorResetMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ErrorResetMode.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.ErrorResetMode.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ErrorResetMode.OnRead"/>.
//		/// </summary>
//		OnRead = (int)global::Hbm.Api.Common.Enums.ErrorResetMode.OnRead,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ErrorResetMode.Delayed"/>.
//		/// </summary>
//		Delayed = (int)global::Hbm.Api.Common.Enums.ErrorResetMode.Delayed,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.FrameFormat"/>.
//	/// </summary>
//	public enum HbmCoreFrameFormat
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.FrameFormat.Standard"/>.
//		/// </summary>
//		Standard = (int)global::Hbm.Api.SensorDB.Enums.FrameFormat.Standard,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.FrameFormat.Extended"/>.
//		/// </summary>
//		Extended = (int)global::Hbm.Api.SensorDB.Enums.FrameFormat.Extended,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes"/>.
//	/// </summary>
//	public enum HbmCoreFrameworkDllResultCodes
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnModuleNotReadyForFieldbusAcq"/>.
//		/// </summary>
//		WarnModuleNotReadyForFieldbusAcq = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnModuleNotReadyForFieldbusAcq,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatFunctionblockSetPdoStatus"/>.
//		/// </summary>
//		ErrEcatFunctionblockSetPdoStatus = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatFunctionblockSetPdoStatus,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatFunctionblockSetPdoIndex"/>.
//		/// </summary>
//		ErrEcatFunctionblockSetPdoIndex = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatFunctionblockSetPdoIndex,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatFunctionblockCreateProblem"/>.
//		/// </summary>
//		ErrEcatFunctionblockCreateProblem = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatFunctionblockCreateProblem,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatRtsignaleNotValid"/>.
//		/// </summary>
//		ErrEcatRtsignaleNotValid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatRtsignaleNotValid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnEcatDemuxSignaleNotValid"/>.
//		/// </summary>
//		WarnEcatDemuxSignaleNotValid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnEcatDemuxSignaleNotValid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnEcatModuleParamNotReady"/>.
//		/// </summary>
//		WarnEcatModuleParamNotReady = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnEcatModuleParamNotReady,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatModuleStatusNotReadable"/>.
//		/// </summary>
//		ErrEcatModuleStatusNotReadable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatModuleStatusNotReadable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnEcatCorrectedFwSignaleListSize"/>.
//		/// </summary>
//		WarnEcatCorrectedFwSignaleListSize = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnEcatCorrectedFwSignaleListSize,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatCountOfFwSignalsLimitReached"/>.
//		/// </summary>
//		ErrEcatCountOfFwSignalsLimitReached = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEcatCountOfFwSignalsLimitReached,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNtpServerNotReached"/>.
//		/// </summary>
//		ErrNtpServerNotReached = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNtpServerNotReached,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFwNoNecChannel"/>.
//		/// </summary>
//		ErrFwNoNecChannel = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFwNoNecChannel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFwNoNecDownload"/>.
//		/// </summary>
//		ErrFwNoNecDownload = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFwNoNecDownload,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFwNoMspChannel"/>.
//		/// </summary>
//		ErrFwNoMspChannel = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFwNoMspChannel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnExcitationcalibrationDataMissing"/>.
//		/// </summary>
//		WarnExcitationcalibrationDataMissing = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnExcitationcalibrationDataMissing,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCalcalibrationDataMissing"/>.
//		/// </summary>
//		WarnCalcalibrationDataMissing = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCalcalibrationDataMissing,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMspDataCorrupt"/>.
//		/// </summary>
//		ErrMspDataCorrupt = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMspDataCorrupt,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrInitjustFromMsp"/>.
//		/// </summary>
//		ErrInitjustFromMsp = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrInitjustFromMsp,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoValidInitjust"/>.
//		/// </summary>
//		ErrNoValidInitjust = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoValidInitjust,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrStoringInitjust"/>.
//		/// </summary>
//		ErrStoringInitjust = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrStoringInitjust,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrGettingInitjust"/>.
//		/// </summary>
//		ErrGettingInitjust = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrGettingInitjust,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPllvolOutofrange"/>.
//		/// </summary>
//		ErrPllvolOutofrange = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPllvolOutofrange,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPllvolCorrupt"/>.
//		/// </summary>
//		ErrPllvolCorrupt = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPllvolCorrupt,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPllvolFromMsp"/>.
//		/// </summary>
//		ErrPllvolFromMsp = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPllvolFromMsp,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDataAddrBustest"/>.
//		/// </summary>
//		ErrDataAddrBustest = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDataAddrBustest,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDataRateDomainNotSupported"/>.
//		/// </summary>
//		ErrDataRateDomainNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDataRateDomainNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysFilename"/>.
//		/// </summary>
//		ErrSysFilename = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysFilename,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysFileTooLarge"/>.
//		/// </summary>
//		ErrSysFileTooLarge = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysFileTooLarge,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysBadDelivery"/>.
//		/// </summary>
//		ErrSysBadDelivery = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysBadDelivery,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysOpenlogfile"/>.
//		/// </summary>
//		ErrSysOpenlogfile = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysOpenlogfile,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysFrameworkStart"/>.
//		/// </summary>
//		ErrSysFrameworkStart = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysFrameworkStart,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysOpenCloseWd"/>.
//		/// </summary>
//		ErrSysOpenCloseWd = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysOpenCloseWd,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysInvalidTime"/>.
//		/// </summary>
//		ErrSysInvalidTime = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysInvalidTime,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysTooManyEvents"/>.
//		/// </summary>
//		ErrSysTooManyEvents = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysTooManyEvents,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysCreatingObject"/>.
//		/// </summary>
//		ErrSysCreatingObject = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSysCreatingObject,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiOpenDriver"/>.
//		/// </summary>
//		ErrSpiOpenDriver = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiOpenDriver,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiWakeupDac"/>.
//		/// </summary>
//		ErrSpiWakeupDac = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiWakeupDac,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiWriteDriverDigits"/>.
//		/// </summary>
//		ErrSpiWriteDriverDigits = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiWriteDriverDigits,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiWriteDriverCommand"/>.
//		/// </summary>
//		ErrSpiWriteDriverCommand = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiWriteDriverCommand,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiVoltOutofRange"/>.
//		/// </summary>
//		ErrSpiVoltOutofRange = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiVoltOutofRange,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiPlldac"/>.
//		/// </summary>
//		ErrSpiPlldac = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSpiPlldac,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncOpenDriver"/>.
//		/// </summary>
//		ErrSyncOpenDriver = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncOpenDriver,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncAnyTsdProblem"/>.
//		/// </summary>
//		ErrSyncAnyTsdProblem = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncAnyTsdProblem,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncNotAllModulesReady"/>.
//		/// </summary>
//		ErrSyncNotAllModulesReady = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncNotAllModulesReady,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncTimesourceDataNotValid"/>.
//		/// </summary>
//		ErrSyncTimesourceDataNotValid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncTimesourceDataNotValid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivateEthercat"/>.
//		/// </summary>
//		ErrSyncActivateEthercat = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivateEthercat,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivateIrig"/>.
//		/// </summary>
//		ErrSyncActivateIrig = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivateIrig,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivatePtp"/>.
//		/// </summary>
//		ErrSyncActivatePtp = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivatePtp,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivateNtp"/>.
//		/// </summary>
//		ErrSyncActivateNtp = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivateNtp,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivateAuto"/>.
//		/// </summary>
//		ErrSyncActivateAuto = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncActivateAuto,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamIpSubnetmaskSyntax"/>.
//		/// </summary>
//		ErrParamIpSubnetmaskSyntax = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamIpSubnetmaskSyntax,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamIpAddressSyntax"/>.
//		/// </summary>
//		ErrParamIpAddressSyntax = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamIpAddressSyntax,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamSyncTimesourceNotSupported"/>.
//		/// </summary>
//		ErrParamSyncTimesourceNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamSyncTimesourceNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncCyclediffTooLarge"/>.
//		/// </summary>
//		ErrSyncCyclediffTooLarge = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncCyclediffTooLarge,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncFreeChannel"/>.
//		/// </summary>
//		ErrSyncFreeChannel = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncFreeChannel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncRecvCtxRunning"/>.
//		/// </summary>
//		ErrSyncRecvCtxRunning = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncRecvCtxRunning,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncAllocTx"/>.
//		/// </summary>
//		ErrSyncAllocTx = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncAllocTx,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncFreeTx"/>.
//		/// </summary>
//		ErrSyncFreeTx = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncFreeTx,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncAllocRx"/>.
//		/// </summary>
//		ErrSyncAllocRx = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncAllocRx,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncFreeRx"/>.
//		/// </summary>
//		ErrSyncFreeRx = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncFreeRx,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncDriver"/>.
//		/// </summary>
//		ErrSyncDriver = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncDriver,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncNotInSync"/>.
//		/// </summary>
//		ErrSyncNotInSync = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncNotInSync,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncInitVoltage"/>.
//		/// </summary>
//		ErrSyncInitVoltage = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncInitVoltage,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncAdjustPll"/>.
//		/// </summary>
//		ErrSyncAdjustPll = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncAdjustPll,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncSyncbufEmpty"/>.
//		/// </summary>
//		ErrSyncSyncbufEmpty = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncSyncbufEmpty,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncCtx"/>.
//		/// </summary>
//		ErrSyncCtx = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSyncCtx,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjSetInitCalFacts"/>.
//		/// </summary>
//		ErrAdjSetInitCalFacts = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjSetInitCalFacts,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjSetActJustFacts"/>.
//		/// </summary>
//		ErrAdjSetActJustFacts = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjSetActJustFacts,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjGetActJustFacts"/>.
//		/// </summary>
//		ErrAdjGetActJustFacts = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjGetActJustFacts,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjGetInitCalFacts"/>.
//		/// </summary>
//		ErrAdjGetInitCalFacts = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjGetInitCalFacts,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjGetInitMeasFacts"/>.
//		/// </summary>
//		ErrAdjGetInitMeasFacts = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjGetInitMeasFacts,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjMeasrangeInvalid"/>.
//		/// </summary>
//		ErrAdjMeasrangeInvalid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjMeasrangeInvalid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjMeasrangeOverrun"/>.
//		/// </summary>
//		ErrAdjMeasrangeOverrun = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjMeasrangeOverrun,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjWrongConnector"/>.
//		/// </summary>
//		ErrAdjWrongConnector = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAdjWrongConnector,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrThermoWrongConnector"/>.
//		/// </summary>
//		ErrThermoWrongConnector = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrThermoWrongConnector,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConnWrongConnector"/>.
//		/// </summary>
//		ErrConnWrongConnector = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConnWrongConnector,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConnInstanceNull"/>.
//		/// </summary>
//		ErrConnInstanceNull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConnInstanceNull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAnalogoutNoconn"/>.
//		/// </summary>
//		ErrAnalogoutNoconn = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAnalogoutNoconn,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAnalogoutSigovfl"/>.
//		/// </summary>
//		ErrAnalogoutSigovfl = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAnalogoutSigovfl,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAnalogoutOccf"/>.
//		/// </summary>
//		ErrAnalogoutOccf = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAnalogoutOccf,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAnalogoutOvfl"/>.
//		/// </summary>
//		ErrAnalogoutOvfl = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAnalogoutOvfl,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceDecoderindexNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceDecoderindexNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceDecoderindexNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceDecoderindexLocalNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceDecoderindexLocalNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceDecoderindexLocalNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignalgeneratorindexNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceSignalgeneratorindexNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignalgeneratorindexNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignalgeneratorindexLocalNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceSignalgeneratorindexLocalNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignalgeneratorindexLocalNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceDigitalioindexLocalNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceDigitalioindexLocalNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceDigitalioindexLocalNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferencePeakindexLocalNotSupported"/>.
//		/// </summary>
//		ErrSignalreferencePeakindexLocalNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferencePeakindexLocalNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceMathindexLocalNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceMathindexLocalNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceMathindexLocalNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceAlarmindexLocalNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceAlarmindexLocalNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceAlarmindexLocalNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceConnectorLocalNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceConnectorLocalNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceConnectorLocalNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignalindexLocalNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceSignalindexLocalNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignalindexLocalNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceCannotCreate"/>.
//		/// </summary>
//		ErrSignalreferenceCannotCreate = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceCannotCreate,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceUnknown"/>.
//		/// </summary>
//		ErrSignalreferenceUnknown = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceUnknown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnSignalreferenceUnknown"/>.
//		/// </summary>
//		WarnSignalreferenceUnknown = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnSignalreferenceUnknown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamLevelsIdentical"/>.
//		/// </summary>
//		ErrParamLevelsIdentical = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamLevelsIdentical,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceConnectorNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceConnectorNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceConnectorNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnMaxIsoSignalindexReached"/>.
//		/// </summary>
//		WarnMaxIsoSignalindexReached = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnMaxIsoSignalindexReached,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignalindexNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceSignalindexNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignalindexNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrIndexNotSupported"/>.
//		/// </summary>
//		ErrIndexNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrIndexNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignaltypeNotSupportedOrSyntaxError"/>.
//		/// </summary>
//		ErrSignalreferenceSignaltypeNotSupportedOrSyntaxError = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSignaltypeNotSupportedOrSyntaxError,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnSignalreferenceEmpty"/>.
//		/// </summary>
//		WarnSignalreferenceEmpty = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnSignalreferenceEmpty,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModulereferenceSyntax"/>.
//		/// </summary>
//		ErrModulereferenceSyntax = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModulereferenceSyntax,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferencePeakindexNotSupported"/>.
//		/// </summary>
//		ErrSignalreferencePeakindexNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferencePeakindexNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceMathindexNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceMathindexNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceMathindexNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceAlarmindexNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceAlarmindexNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceAlarmindexNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModulereferenceNotLocal"/>.
//		/// </summary>
//		ErrModulereferenceNotLocal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModulereferenceNotLocal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModulereferenceNotExternal"/>.
//		/// </summary>
//		ErrModulereferenceNotExternal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModulereferenceNotExternal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSyntax"/>.
//		/// </summary>
//		ErrSignalreferenceSyntax = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceSyntax,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPhysunitNotAvailable"/>.
//		/// </summary>
//		ErrPhysunitNotAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPhysunitNotAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceAsyncsignalNotAllowedOrSyntaxError"/>.
//		/// </summary>
//		ErrSignalreferenceAsyncsignalNotAllowedOrSyntaxError = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceAsyncsignalNotAllowedOrSyntaxError,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceDigitalioindexNotSupported"/>.
//		/// </summary>
//		ErrSignalreferenceDigitalioindexNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSignalreferenceDigitalioindexNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrIoctlDevice"/>.
//		/// </summary>
//		ErrIoctlDevice = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrIoctlDevice,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDeviceNotOpen"/>.
//		/// </summary>
//		ErrDeviceNotOpen = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDeviceNotOpen,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCloseDevice"/>.
//		/// </summary>
//		ErrCloseDevice = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCloseDevice,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDeviceAlreadyOpen"/>.
//		/// </summary>
//		ErrDeviceAlreadyOpen = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDeviceAlreadyOpen,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrOpenDevice"/>.
//		/// </summary>
//		ErrOpenDevice = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrOpenDevice,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPinfoNull"/>.
//		/// </summary>
//		ErrPinfoNull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPinfoNull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPointerNull"/>.
//		/// </summary>
//		ErrPointerNull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPointerNull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamsettingsNull"/>.
//		/// </summary>
//		ErrParamsettingsNull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamsettingsNull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamCalcsyncsignal"/>.
//		/// </summary>
//		ErrParamCalcsyncsignal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParamCalcsyncsignal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPplatformNull"/>.
//		/// </summary>
//		ErrPplatformNull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPplatformNull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPampNull"/>.
//		/// </summary>
//		ErrPampNull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPampNull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPanalogNull"/>.
//		/// </summary>
//		ErrPanalogNull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrPanalogNull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTedsData"/>.
//		/// </summary>
//		ErrTedsData = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTedsData,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoSignalGeneratorUnitAvailable"/>.
//		/// </summary>
//		ErrNoSignalGeneratorUnitAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoSignalGeneratorUnitAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoGateUnitAvailable"/>.
//		/// </summary>
//		ErrNoGateUnitAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoGateUnitAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoPeakUnitAvailable"/>.
//		/// </summary>
//		ErrNoPeakUnitAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoPeakUnitAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoMathUnitAvailable"/>.
//		/// </summary>
//		ErrNoMathUnitAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoMathUnitAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoAlarmUnitAvailable"/>.
//		/// </summary>
//		ErrNoAlarmUnitAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoAlarmUnitAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoDigitalIoAvailable"/>.
//		/// </summary>
//		ErrNoDigitalIoAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoDigitalIoAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoCanConnectorsAvailable"/>.
//		/// </summary>
//		ErrNoCanConnectorsAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoCanConnectorsAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoAnalogOutConnectorsAvalibale"/>.
//		/// </summary>
//		ErrNoAnalogOutConnectorsAvalibale = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoAnalogOutConnectorsAvalibale,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoAnalogInConnectorsAvailable"/>.
//		/// </summary>
//		ErrNoAnalogInConnectorsAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoAnalogInConnectorsAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrHandlingExternalAdaptor"/>.
//		/// </summary>
//		ErrHandlingExternalAdaptor = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrHandlingExternalAdaptor,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModeNotSupportedBySensortype"/>.
//		/// </summary>
//		ErrModeNotSupportedBySensortype = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModeNotSupportedBySensortype,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnModeNotSupportedBySensortype"/>.
//		/// </summary>
//		WarnModeNotSupportedBySensortype = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnModeNotSupportedBySensortype,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNotAvailableIfExternalAdaptorPresent"/>.
//		/// </summary>
//		ErrNotAvailableIfExternalAdaptorPresent = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNotAvailableIfExternalAdaptorPresent,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnNotAvailableIfExternalAdaptorPresent"/>.
//		/// </summary>
//		WarnNotAvailableIfExternalAdaptorPresent = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnNotAvailableIfExternalAdaptorPresent,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrExecuteControl"/>.
//		/// </summary>
//		ErrExecuteControl = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrExecuteControl,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnExecuteControl"/>.
//		/// </summary>
//		WarnExecuteControl = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnExecuteControl,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrValueNotSupported"/>.
//		/// </summary>
//		ErrValueNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrValueNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnValueNotSupported"/>.
//		/// </summary>
//		WarnValueNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnValueNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAvailableOnlyWithExternalAdaptor"/>.
//		/// </summary>
//		ErrAvailableOnlyWithExternalAdaptor = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAvailableOnlyWithExternalAdaptor,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnAvailableOnlyWithExternalAdaptor"/>.
//		/// </summary>
//		WarnAvailableOnlyWithExternalAdaptor = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnAvailableOnlyWithExternalAdaptor,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrControlNotSupportedBySensortype"/>.
//		/// </summary>
//		ErrControlNotSupportedBySensortype = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrControlNotSupportedBySensortype,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrControlNotSupportedAtConnector"/>.
//		/// </summary>
//		ErrControlNotSupportedAtConnector = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrControlNotSupportedAtConnector,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrControlNotSupported"/>.
//		/// </summary>
//		ErrControlNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrControlNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnControlNotSupported"/>.
//		/// </summary>
//		WarnControlNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnControlNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModeNotSupported"/>.
//		/// </summary>
//		ErrModeNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrModeNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnModeNotSupported"/>.
//		/// </summary>
//		WarnModeNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnModeNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrKeyNotFound"/>.
//		/// </summary>
//		ErrKeyNotFound = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrKeyNotFound,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoSpaceLeft"/>.
//		/// </summary>
//		ErrNoSpaceLeft = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoSpaceLeft,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAssemblySizeNotSupported"/>.
//		/// </summary>
//		ErrAssemblySizeNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAssemblySizeNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCtrlPeakmodeSignalctrl"/>.
//		/// </summary>
//		WarnCtrlPeakmodeSignalctrl = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCtrlPeakmodeSignalctrl,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCtrlRunmodeSignalctrl"/>.
//		/// </summary>
//		WarnCtrlRunmodeSignalctrl = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCtrlRunmodeSignalctrl,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToSetSignalgenerator"/>.
//		/// </summary>
//		ErrCtrlUnableToSetSignalgenerator = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToSetSignalgenerator,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToControlAlarmUnit"/>.
//		/// </summary>
//		ErrCtrlUnableToControlAlarmUnit = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToControlAlarmUnit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToControlPeakUnit"/>.
//		/// </summary>
//		ErrCtrlUnableToControlPeakUnit = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToControlPeakUnit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToSetZero"/>.
//		/// </summary>
//		ErrCtrlUnableToSetZero = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToSetZero,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToControlMathUnit"/>.
//		/// </summary>
//		ErrCtrlUnableToControlMathUnit = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToControlMathUnit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToControlSignalGenerator"/>.
//		/// </summary>
//		ErrCtrlUnableToControlSignalGenerator = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnableToControlSignalGenerator,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnspecified"/>.
//		/// </summary>
//		ErrCtrlUnspecified = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlUnspecified,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWrongSecurityCode"/>.
//		/// </summary>
//		ErrWrongSecurityCode = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWrongSecurityCode,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlWrongPar"/>.
//		/// </summary>
//		ErrCtrlWrongPar = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCtrlWrongPar,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCalpointTypeProtected"/>.
//		/// </summary>
//		ErrCalpointTypeProtected = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCalpointTypeProtected,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanRawReceiverEnableDisable"/>.
//		/// </summary>
//		ErrCanRawReceiverEnableDisable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanRawReceiverEnableDisable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanResourceOverload"/>.
//		/// </summary>
//		ErrCanResourceOverload = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanResourceOverload,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransmissiontypeSourcechangeNoSupport64Bit"/>.
//		/// </summary>
//		ErrTransmissiontypeSourcechangeNoSupport64Bit = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransmissiontypeSourcechangeNoSupport64Bit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanWrongIndex"/>.
//		/// </summary>
//		ErrCanWrongIndex = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanWrongIndex,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanValueOnErrorTypeNotSupported"/>.
//		/// </summary>
//		ErrCanValueOnErrorTypeNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanValueOnErrorTypeNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanValueOnErrorTypeNotSupportedByDataformat"/>.
//		/// </summary>
//		ErrCanValueOnErrorTypeNotSupportedByDataformat = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanValueOnErrorTypeNotSupportedByDataformat,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanSetBitrate"/>.
//		/// </summary>
//		ErrCanSetBitrate = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanSetBitrate,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanBitrateChanged"/>.
//		/// </summary>
//		WarnCanBitrateChanged = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanBitrateChanged,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanBitrateSamplepointratioChanged"/>.
//		/// </summary>
//		WarnCanBitrateSamplepointratioChanged = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanBitrateSamplepointratioChanged,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanAnyDecoderTimeout"/>.
//		/// </summary>
//		ErrCanAnyDecoderTimeout = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanAnyDecoderTimeout,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanAnyDecoderTimeout"/>.
//		/// </summary>
//		WarnCanAnyDecoderTimeout = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanAnyDecoderTimeout,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanAnyDecoderLossOfSignal"/>.
//		/// </summary>
//		ErrCanAnyDecoderLossOfSignal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanAnyDecoderLossOfSignal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanAnyDecoderLossOfSignal"/>.
//		/// </summary>
//		WarnCanAnyDecoderLossOfSignal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanAnyDecoderLossOfSignal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanMappingMassageInactive"/>.
//		/// </summary>
//		ErrCanMappingMassageInactive = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanMappingMassageInactive,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanMappingSourcesOverlap"/>.
//		/// </summary>
//		ErrCanMappingSourcesOverlap = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanMappingSourcesOverlap,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanMappingNoSourceDefined"/>.
//		/// </summary>
//		WarnCanMappingNoSourceDefined = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanMappingNoSourceDefined,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrOnlyOneMappingsourcePossible"/>.
//		/// </summary>
//		ErrOnlyOneMappingsourcePossible = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrOnlyOneMappingsourcePossible,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnTimerSetToMinimumPossible"/>.
//		/// </summary>
//		WarnTimerSetToMinimumPossible = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnTimerSetToMinimumPossible,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnSignallengthChanged"/>.
//		/// </summary>
//		WarnSignallengthChanged = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnSignallengthChanged,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanCcp"/>.
//		/// </summary>
//		ErrCanCcp = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanCcp,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanWriteCansignal"/>.
//		/// </summary>
//		ErrCanWriteCansignal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanWriteCansignal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitSignalreferenceInvalid"/>.
//		/// </summary>
//		ErrCanTransmitSignalreferenceInvalid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitSignalreferenceInvalid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitModulereferenceNotLocal"/>.
//		/// </summary>
//		ErrCanTransmitModulereferenceNotLocal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitModulereferenceNotLocal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitMsg"/>.
//		/// </summary>
//		ErrCanTransmitMsg = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitMsg,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanTransmissiontypeChanged"/>.
//		/// </summary>
//		WarnCanTransmissiontypeChanged = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanTransmissiontypeChanged,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmissiontypeNotSupported"/>.
//		/// </summary>
//		ErrCanTransmissiontypeNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmissiontypeNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanConnectorNotSupported"/>.
//		/// </summary>
//		ErrCanConnectorNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanConnectorNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitChannel"/>.
//		/// </summary>
//		ErrCanTransmitChannel = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitChannel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitUserdefMsg"/>.
//		/// </summary>
//		ErrCanTransmitUserdefMsg = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitUserdefMsg,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitByteorderNotSupported"/>.
//		/// </summary>
//		ErrCanTransmitByteorderNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitByteorderNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitTable"/>.
//		/// </summary>
//		ErrCanTransmitTable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitTable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitBytevalue"/>.
//		/// </summary>
//		ErrCanTransmitBytevalue = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitBytevalue,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitBytecount"/>.
//		/// </summary>
//		ErrCanTransmitBytecount = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitBytecount,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitterUserdefMsgSetTable"/>.
//		/// </summary>
//		ErrCanTransmitterUserdefMsgSetTable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitterUserdefMsgSetTable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderModelength64"/>.
//		/// </summary>
//		ErrCanDecoderModelength64 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderModelength64,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderModestartbit63"/>.
//		/// </summary>
//		ErrCanDecoderModestartbit63 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderModestartbit63,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderLength64"/>.
//		/// </summary>
//		ErrCanDecoderLength64 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderLength64,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderStartbit63"/>.
//		/// </summary>
//		ErrCanDecoderStartbit63 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderStartbit63,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanCombinationIdentifierFrameformat"/>.
//		/// </summary>
//		ErrCanCombinationIdentifierFrameformat = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanCombinationIdentifierFrameformat,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanChannelno"/>.
//		/// </summary>
//		ErrCanChannelno = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanChannelno,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderModeStartbitLength"/>.
//		/// </summary>
//		ErrCanDecoderModeStartbitLength = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderModeStartbitLength,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanCombinationStartbitLength"/>.
//		/// </summary>
//		ErrCanCombinationStartbitLength = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanCombinationStartbitLength,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderFormatDouble"/>.
//		/// </summary>
//		ErrCanDecoderFormatDouble = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderFormatDouble,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderFormatFloat"/>.
//		/// </summary>
//		ErrCanDecoderFormatFloat = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderFormatFloat,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderFormat32"/>.
//		/// </summary>
//		ErrCanDecoderFormat32 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderFormat32,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderRbufnoInvalid"/>.
//		/// </summary>
//		ErrCanDecoderRbufnoInvalid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderRbufnoInvalid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderRbufnoUsedTwice"/>.
//		/// </summary>
//		ErrCanDecoderRbufnoUsedTwice = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderRbufnoUsedTwice,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderGetTable"/>.
//		/// </summary>
//		ErrCanDecoderGetTable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderGetTable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderSetTable"/>.
//		/// </summary>
//		ErrCanDecoderSetTable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderSetTable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderTableFull"/>.
//		/// </summary>
//		ErrCanDecoderTableFull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderTableFull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderDisable"/>.
//		/// </summary>
//		ErrCanDecoderDisable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderDisable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderEnable"/>.
//		/// </summary>
//		ErrCanDecoderEnable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderEnable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderTableNotValid"/>.
//		/// </summary>
//		ErrCanDecoderTableNotValid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderTableNotValid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderParamCanSignal"/>.
//		/// </summary>
//		ErrCanDecoderParamCanSignal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDecoderParamCanSignal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanNoResponse"/>.
//		/// </summary>
//		ErrCanNoResponse = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanNoResponse,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanResetRequest"/>.
//		/// </summary>
//		WarnCanResetRequest = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanResetRequest,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitterOverrun"/>.
//		/// </summary>
//		ErrCanTransmitterOverrun = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanTransmitterOverrun,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusErrorTransmitter"/>.
//		/// </summary>
//		ErrCanBusErrorTransmitter = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusErrorTransmitter,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusWarningTransmitter"/>.
//		/// </summary>
//		ErrCanBusWarningTransmitter = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusWarningTransmitter,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanBusWarningTransmitter"/>.
//		/// </summary>
//		WarnCanBusWarningTransmitter = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanBusWarningTransmitter,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanReceiverOverrun"/>.
//		/// </summary>
//		ErrCanReceiverOverrun = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanReceiverOverrun,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusOff"/>.
//		/// </summary>
//		ErrCanBusOff = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusOff,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusErrorReceiver"/>.
//		/// </summary>
//		ErrCanBusErrorReceiver = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusErrorReceiver,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusWarningReceiver"/>.
//		/// </summary>
//		ErrCanBusWarningReceiver = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBusWarningReceiver,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanBusWarningReceiver"/>.
//		/// </summary>
//		WarnCanBusWarningReceiver = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnCanBusWarningReceiver,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanInitCansignalParams"/>.
//		/// </summary>
//		ErrCanInitCansignalParams = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanInitCansignalParams,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanGetBitrate"/>.
//		/// </summary>
//		ErrCanGetBitrate = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanGetBitrate,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanGetState"/>.
//		/// </summary>
//		ErrCanGetState = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanGetState,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanInvalidHandle"/>.
//		/// </summary>
//		ErrCanInvalidHandle = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanInvalidHandle,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanIndex"/>.
//		/// </summary>
//		ErrCanIndex = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanIndex,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanIoctl"/>.
//		/// </summary>
//		ErrCanIoctl = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanIoctl,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBitrateNotSupported"/>.
//		/// </summary>
//		ErrCanBitrateNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanBitrateNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDisable"/>.
//		/// </summary>
//		ErrCanDisable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanDisable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanEnable"/>.
//		/// </summary>
//		ErrCanEnable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanEnable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanOpendrv"/>.
//		/// </summary>
//		ErrCanOpendrv = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCanOpendrv,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFbfsAccessLed"/>.
//		/// </summary>
//		ErrFbfsAccessLed = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFbfsAccessLed,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFbfsAccessCan"/>.
//		/// </summary>
//		ErrFbfsAccessCan = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrFbfsAccessCan,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetCarrierfreqMode"/>.
//		/// </summary>
//		ErrDaqSetCarrierfreqMode = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetCarrierfreqMode,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetFreezedFlag"/>.
//		/// </summary>
//		ErrDaqSetFreezedFlag = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetFreezedFlag,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetRtmeasmode"/>.
//		/// </summary>
//		ErrDaqSetRtmeasmode = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetRtmeasmode,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqTrigtime"/>.
//		/// </summary>
//		ErrDaqTrigtime = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqTrigtime,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqTriggerunknown"/>.
//		/// </summary>
//		ErrDaqTriggerunknown = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqTriggerunknown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqIsosignallistVersion"/>.
//		/// </summary>
//		ErrDaqIsosignallistVersion = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqIsosignallistVersion,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqAmpNull"/>.
//		/// </summary>
//		ErrDaqAmpNull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqAmpNull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqRtdriverNull"/>.
//		/// </summary>
//		ErrDaqRtdriverNull = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqRtdriverNull,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqWrongChanType"/>.
//		/// </summary>
//		ErrDaqWrongChanType = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqWrongChanType,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnScalingAnalogoutGainIfAnaloginLintable"/>.
//		/// </summary>
//		WarnScalingAnalogoutGainIfAnaloginLintable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnScalingAnalogoutGainIfAnaloginLintable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqGetDemodInfo"/>.
//		/// </summary>
//		ErrDaqGetDemodInfo = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqGetDemodInfo,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetErrorbuf"/>.
//		/// </summary>
//		ErrDaqSetErrorbuf = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetErrorbuf,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqDrvSetPtCoeffs"/>.
//		/// </summary>
//		ErrDaqDrvSetPtCoeffs = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqDrvSetPtCoeffs,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqDrvSetColdjunction"/>.
//		/// </summary>
//		ErrDaqDrvSetColdjunction = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqDrvSetColdjunction,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqDrvSetInitjust"/>.
//		/// </summary>
//		ErrDaqDrvSetInitjust = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqDrvSetInitjust,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomNotSupportedIfAnalogoutEnabled"/>.
//		/// </summary>
//		ErrScalingPolynomNotSupportedIfAnalogoutEnabled = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomNotSupportedIfAnalogoutEnabled,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomTooMuchCoeffs"/>.
//		/// </summary>
//		ErrScalingPolynomTooMuchCoeffs = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomTooMuchCoeffs,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomNotEnoughCoeffs"/>.
//		/// </summary>
//		ErrScalingPolynomNotEnoughCoeffs = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomNotEnoughCoeffs,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomTooMuchSegments"/>.
//		/// </summary>
//		ErrScalingPolynomTooMuchSegments = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomTooMuchSegments,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingLintableTooMuchPoints"/>.
//		/// </summary>
//		ErrScalingLintableTooMuchPoints = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingLintableTooMuchPoints,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomNotEnoughSegments"/>.
//		/// </summary>
//		ErrScalingPolynomNotEnoughSegments = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingPolynomNotEnoughSegments,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAccessRtdriver"/>.
//		/// </summary>
//		ErrAccessRtdriver = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrAccessRtdriver,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingLintableNotEnoughPoints"/>.
//		/// </summary>
//		ErrScalingLintableNotEnoughPoints = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrScalingLintableNotEnoughPoints,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqScalingInternal"/>.
//		/// </summary>
//		ErrDaqScalingInternal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqScalingInternal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqScaletype"/>.
//		/// </summary>
//		ErrDaqScaletype = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqScaletype,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWrongChannelIndex"/>.
//		/// </summary>
//		ErrWrongChannelIndex = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWrongChannelIndex,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWrongSigIndex"/>.
//		/// </summary>
//		ErrWrongSigIndex = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWrongSigIndex,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWrongConnector"/>.
//		/// </summary>
//		ErrWrongConnector = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWrongConnector,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqInitjustNotfound"/>.
//		/// </summary>
//		ErrDaqInitjustNotfound = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqInitjustNotfound,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqInitjustPointnr"/>.
//		/// </summary>
//		ErrDaqInitjustPointnr = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqInitjustPointnr,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqInitjustWrongpar"/>.
//		/// </summary>
//		ErrDaqInitjustWrongpar = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqInitjustWrongpar,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSigidLen"/>.
//		/// </summary>
//		ErrDaqSigidLen = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSigidLen,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSigidUnknown"/>.
//		/// </summary>
//		ErrDaqSigidUnknown = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSigidUnknown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqFilter"/>.
//		/// </summary>
//		ErrDaqFilter = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqFilter,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSignalNotAvailable"/>.
//		/// </summary>
//		ErrDaqSignalNotAvailable = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSignalNotAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqAddFbtype"/>.
//		/// </summary>
//		ErrDaqAddFbtype = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqAddFbtype,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqAddFbInstance"/>.
//		/// </summary>
//		ErrDaqAddFbInstance = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqAddFbInstance,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetFbInput"/>.
//		/// </summary>
//		ErrDaqSetFbInput = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetFbInput,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOutrate"/>.
//		/// </summary>
//		ErrDaqOutrate = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOutrate,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetBufferPars"/>.
//		/// </summary>
//		ErrDaqSetBufferPars = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetBufferPars,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetZeroval"/>.
//		/// </summary>
//		ErrDaqSetZeroval = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetZeroval,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqFifoParams"/>.
//		/// </summary>
//		ErrDaqFifoParams = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqFifoParams,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqFiforeaderCalParams"/>.
//		/// </summary>
//		ErrDaqFiforeaderCalParams = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqFiforeaderCalParams,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqScalingParams"/>.
//		/// </summary>
//		ErrDaqScalingParams = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqScalingParams,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetIsoModuleId"/>.
//		/// </summary>
//		ErrDaqSetIsoModuleId = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetIsoModuleId,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetIsoTransmit"/>.
//		/// </summary>
//		ErrDaqSetIsoTransmit = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSetIsoTransmit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqGetIsoTransmit"/>.
//		/// </summary>
//		ErrDaqGetIsoTransmit = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqGetIsoTransmit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOverrun"/>.
//		/// </summary>
//		ErrDaqOverrun = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOverrun,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqUndefined"/>.
//		/// </summary>
//		ErrDaqUndefined = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqUndefined,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqFifoSync"/>.
//		/// </summary>
//		ErrDaqFifoSync = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqFifoSync,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOverload"/>.
//		/// </summary>
//		ErrDaqOverload = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOverload,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqProcess"/>.
//		/// </summary>
//		ErrDaqProcess = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqProcess,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCriticalHighVoltage"/>.
//		/// </summary>
//		ErrCriticalHighVoltage = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrCriticalHighVoltage,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOccf"/>.
//		/// </summary>
//		ErrDaqOccf = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOccf,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqLimse"/>.
//		/// </summary>
//		ErrDaqLimse = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqLimse,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqNosignalsubscribed"/>.
//		/// </summary>
//		ErrDaqNosignalsubscribed = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqNosignalsubscribed,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqBuffersize"/>.
//		/// </summary>
//		ErrDaqBuffersize = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqBuffersize,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqAmpoff"/>.
//		/// </summary>
//		ErrDaqAmpoff = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqAmpoff,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOpenrtdrv"/>.
//		/// </summary>
//		ErrDaqOpenrtdrv = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqOpenrtdrv,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSubscrRunning"/>.
//		/// </summary>
//		ErrDaqSubscrRunning = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSubscrRunning,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSubscrUuid"/>.
//		/// </summary>
//		ErrDaqSubscrUuid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSubscrUuid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSubscrState"/>.
//		/// </summary>
//		ErrDaqSubscrState = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSubscrState,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSubscrition"/>.
//		/// </summary>
//		ErrDaqSubscrition = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDaqSubscrition,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDataFormatNotSupported"/>.
//		/// </summary>
//		ErrDataFormatNotSupported = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrDataFormatNotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvStatComposer"/>.
//		/// </summary>
//		ErrSrvStatComposer = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvStatComposer,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysNorestartfkt"/>.
//		/// </summary>
//		ErrMthdSysNorestartfkt = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysNorestartfkt,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysWrongchantype"/>.
//		/// </summary>
//		ErrMthdSysWrongchantype = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysWrongchantype,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysWrongchan"/>.
//		/// </summary>
//		ErrMthdSysWrongchan = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysWrongchan,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysInvalidfwfile"/>.
//		/// </summary>
//		ErrMthdSysInvalidfwfile = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysInvalidfwfile,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysFilenotfound"/>.
//		/// </summary>
//		ErrMthdSysFilenotfound = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrMthdSysFilenotfound,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrOpenfile"/>.
//		/// </summary>
//		ErrOpenfile = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrOpenfile,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWriteFile"/>.
//		/// </summary>
//		ErrWriteFile = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrWriteFile,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrReadFile"/>.
//		/// </summary>
//		ErrReadFile = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrReadFile,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParseXmlSettingRefused"/>.
//		/// </summary>
//		ErrParseXmlSettingRefused = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParseXmlSettingRefused,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParWrongType"/>.
//		/// </summary>
//		ErrParWrongType = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParWrongType,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParNodefset"/>.
//		/// </summary>
//		WarnParNodefset = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParNodefset,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParViewidInuse"/>.
//		/// </summary>
//		ErrParViewidInuse = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParViewidInuse,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParReadingVal"/>.
//		/// </summary>
//		ErrParReadingVal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParReadingVal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParEmptyVal"/>.
//		/// </summary>
//		WarnParEmptyVal = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParEmptyVal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParViewinvalid"/>.
//		/// </summary>
//		ErrParViewinvalid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParViewinvalid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParWrongxpathstate"/>.
//		/// </summary>
//		ErrParWrongxpathstate = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParWrongxpathstate,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParXpathinvalid"/>.
//		/// </summary>
//		ErrParXpathinvalid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParXpathinvalid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParNoroot"/>.
//		/// </summary>
//		ErrParNoroot = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParNoroot,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParParsing"/>.
//		/// </summary>
//		WarnParParsing = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParParsing,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParParsing"/>.
//		/// </summary>
//		ErrParParsing = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParParsing,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParInvalidaddr"/>.
//		/// </summary>
//		ErrParInvalidaddr = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParInvalidaddr,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParTagunkown"/>.
//		/// </summary>
//		WarnParTagunkown = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParTagunkown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParTagunkown"/>.
//		/// </summary>
//		ErrParTagunkown = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParTagunkown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParOutofrange"/>.
//		/// </summary>
//		ErrParOutofrange = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParOutofrange,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParOutofrange"/>.
//		/// </summary>
//		WarnParOutofrange = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParOutofrange,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParInvalidattr"/>.
//		/// </summary>
//		ErrParInvalidattr = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParInvalidattr,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParReadonly"/>.
//		/// </summary>
//		WarnParReadonly = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnParReadonly,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParChoiceinvalid"/>.
//		/// </summary>
//		ErrParChoiceinvalid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParChoiceinvalid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParCntxtlost"/>.
//		/// </summary>
//		ErrParCntxtlost = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParCntxtlost,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParChoicevaluesyntaxinvalid"/>.
//		/// </summary>
//		ErrParChoicevaluesyntaxinvalid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParChoicevaluesyntaxinvalid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParRestorefailed"/>.
//		/// </summary>
//		ErrParRestorefailed = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParRestorefailed,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParXmlinvalid"/>.
//		/// </summary>
//		ErrParXmlinvalid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParXmlinvalid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTxtelcontainerTooSmall"/>.
//		/// </summary>
//		ErrTxtelcontainerTooSmall = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTxtelcontainerTooSmall,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParseProcessingData"/>.
//		/// </summary>
//		ErrParseProcessingData = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParseProcessingData,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParseInvalidData"/>.
//		/// </summary>
//		ErrParseInvalidData = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrParseInvalidData,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilFileRestore"/>.
//		/// </summary>
//		ErrUtilFileRestore = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilFileRestore,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilInvalidFilehandle"/>.
//		/// </summary>
//		ErrUtilInvalidFilehandle = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilInvalidFilehandle,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilStrconvert"/>.
//		/// </summary>
//		ErrUtilStrconvert = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilStrconvert,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilNoPatternMatch"/>.
//		/// </summary>
//		ErrUtilNoPatternMatch = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilNoPatternMatch,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEvflgTimeout"/>.
//		/// </summary>
//		ErrEvflgTimeout = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEvflgTimeout,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEvflgPthreads"/>.
//		/// </summary>
//		ErrEvflgPthreads = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrEvflgPthreads,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilTimerInvalid"/>.
//		/// </summary>
//		ErrUtilTimerInvalid = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilTimerInvalid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnUtilTimerWasrunning"/>.
//		/// </summary>
//		WarnUtilTimerWasrunning = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.WarnUtilTimerWasrunning,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilLanguage"/>.
//		/// </summary>
//		ErrUtilLanguage = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUtilLanguage,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrComposerTagNotDefined"/>.
//		/// </summary>
//		ErrComposerTagNotDefined = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrComposerTagNotDefined,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNetNetworkadapter"/>.
//		/// </summary>
//		ErrNetNetworkadapter = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNetNetworkadapter,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpBindtodevice"/>.
//		/// </summary>
//		ErrUdpBindtodevice = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpBindtodevice,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpBind"/>.
//		/// </summary>
//		ErrUdpBind = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpBind,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpSetbroadcast"/>.
//		/// </summary>
//		ErrUdpSetbroadcast = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpSetbroadcast,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpSetreuseadr"/>.
//		/// </summary>
//		ErrUdpSetreuseadr = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpSetreuseadr,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpOpensock"/>.
//		/// </summary>
//		ErrUdpOpensock = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrUdpOpensock,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpBind"/>.
//		/// </summary>
//		ErrTcpBind = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpBind,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpListen"/>.
//		/// </summary>
//		ErrTcpListen = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpListen,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpCheckip"/>.
//		/// </summary>
//		ErrTcpCheckip = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpCheckip,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpGetadr"/>.
//		/// </summary>
//		ErrTcpGetadr = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpGetadr,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpGetport"/>.
//		/// </summary>
//		ErrTcpGetport = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpGetport,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSetnodelay"/>.
//		/// </summary>
//		ErrTcpSetnodelay = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSetnodelay,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSetnonblock"/>.
//		/// </summary>
//		ErrTcpSetnonblock = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSetnonblock,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpOpensock"/>.
//		/// </summary>
//		ErrTcpOpensock = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpOpensock,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpConnect"/>.
//		/// </summary>
//		ErrTcpConnect = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpConnect,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpAccept"/>.
//		/// </summary>
//		ErrTcpAccept = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpAccept,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpConnectFailed"/>.
//		/// </summary>
//		ErrTcpConnectFailed = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpConnectFailed,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpIpadrAlreadyConnected"/>.
//		/// </summary>
//		ErrTcpIpadrAlreadyConnected = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpIpadrAlreadyConnected,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpAdrUnknown"/>.
//		/// </summary>
//		ErrTcpAdrUnknown = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpAdrUnknown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSocketEintr"/>.
//		/// </summary>
//		ErrTcpSocketEintr = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSocketEintr,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSocketHup"/>.
//		/// </summary>
//		ErrTcpSocketHup = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSocketHup,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSocketDescriptor"/>.
//		/// </summary>
//		ErrTcpSocketDescriptor = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTcpSocketDescriptor,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConRec"/>.
//		/// </summary>
//		ErrConRec = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConRec,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConSending"/>.
//		/// </summary>
//		ErrConSending = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConSending,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConInvalidTeltype"/>.
//		/// </summary>
//		ErrConInvalidTeltype = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConInvalidTeltype,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConAdrresiniFailed"/>.
//		/// </summary>
//		ErrConAdrresiniFailed = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConAdrresiniFailed,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConConhndlrUuidunknown"/>.
//		/// </summary>
//		ErrConConhndlrUuidunknown = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrConConhndlrUuidunknown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransReadinghdr"/>.
//		/// </summary>
//		ErrTransReadinghdr = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransReadinghdr,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransNotfound"/>.
//		/// </summary>
//		ErrTransNotfound = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransNotfound,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransCanceled"/>.
//		/// </summary>
//		ErrTransCanceled = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransCanceled,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransSendReqTimeout"/>.
//		/// </summary>
//		ErrTransSendReqTimeout = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransSendReqTimeout,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransAckTimeout"/>.
//		/// </summary>
//		ErrTransAckTimeout = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransAckTimeout,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransReqTimeout"/>.
//		/// </summary>
//		ErrTransReqTimeout = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransReqTimeout,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransInvalidTelformat"/>.
//		/// </summary>
//		ErrTransInvalidTelformat = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransInvalidTelformat,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransRequestorWrongprottype"/>.
//		/// </summary>
//		ErrTransRequestorWrongprottype = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransRequestorWrongprottype,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransResponderWrongprottype"/>.
//		/// </summary>
//		ErrTransResponderWrongprottype = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransResponderWrongprottype,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransNotForUs"/>.
//		/// </summary>
//		ErrTransNotForUs = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrTransNotForUs,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoMspChksum"/>.
//		/// </summary>
//		ErrNoMspChksum = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrNoMspChksum,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvControl"/>.
//		/// </summary>
//		ErrSrvControl = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvControl,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvNodata"/>.
//		/// </summary>
//		ErrSrvNodata = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvNodata,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvNoresponseYet"/>.
//		/// </summary>
//		ErrSrvNoresponseYet = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvNoresponseYet,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvReqrmthdNoreqtel"/>.
//		/// </summary>
//		ErrSrvReqrmthdNoreqtel = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvReqrmthdNoreqtel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvParamerror"/>.
//		/// </summary>
//		ErrSrvParamerror = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvParamerror,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvMthdoutUnknownmthd"/>.
//		/// </summary>
//		ErrSrvMthdoutUnknownmthd = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvMthdoutUnknownmthd,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvMthdoutWrongansw"/>.
//		/// </summary>
//		ErrSrvMthdoutWrongansw = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvMthdoutWrongansw,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvMthdoutToomuch"/>.
//		/// </summary>
//		ErrSrvMthdoutToomuch = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvMthdoutToomuch,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvUnknownsrv"/>.
//		/// </summary>
//		ErrSrvUnknownsrv = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvUnknownsrv,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvUnknownmthd"/>.
//		/// </summary>
//		ErrSrvUnknownmthd = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrSrvUnknownmthd,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrIndexOutOfRange"/>.
//		/// </summary>
//		ErrIndexOutOfRange = (int)global::Hbm.Api.QuantumX.Enums.FrameworkDllResultCodes.ErrIndexOutOfRange,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError"/>.
//	/// </summary>
//	public enum HbmCoreFrameworkError
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_SUCCESS"/>.
//		/// </summary>
//		ERR_NO_SUCCESS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_SUCCESS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SUCCESS"/>.
//		/// </summary>
//		ERR_SUCCESS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SUCCESS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_UNSPECIFIED"/>.
//		/// </summary>
//		WARN_UNSPECIFIED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_UNSPECIFIED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_VALUE_CHANGED"/>.
//		/// </summary>
//		WARN_VALUE_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_VALUE_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.INTERNAL_ACTIVATE_PROFINET"/>.
//		/// </summary>
//		INTERNAL_ACTIVATE_PROFINET = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.INTERNAL_ACTIVATE_PROFINET,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.INTERNAL_ACTIVATE_PROFINET_DONE"/>.
//		/// </summary>
//		INTERNAL_ACTIVATE_PROFINET_DONE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.INTERNAL_ACTIVATE_PROFINET_DONE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_INIT_DRIVER"/>.
//		/// </summary>
//		ERR_FIELDBUS_INIT_DRIVER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_INIT_DRIVER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_ACCESS_OD"/>.
//		/// </summary>
//		ERR_FIELDBUS_ACCESS_OD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_ACCESS_OD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_READ_ODXML"/>.
//		/// </summary>
//		ERR_FIELDBUS_READ_ODXML = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_READ_ODXML,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_START_OD"/>.
//		/// </summary>
//		ERR_FIELDBUS_START_OD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_START_OD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_STOP_OD"/>.
//		/// </summary>
//		ERR_FIELDBUS_STOP_OD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_STOP_OD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS"/>.
//		/// </summary>
//		ERR_FIELDBUS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_STATECHANGE"/>.
//		/// </summary>
//		ERR_FIELDBUS_STATECHANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_STATECHANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_INVALIDTIME"/>.
//		/// </summary>
//		ERR_FIELDBUS_INVALIDTIME = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_INVALIDTIME,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_UNAVAILABLPAR"/>.
//		/// </summary>
//		ERR_FIELDBUS_UNAVAILABLPAR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIELDBUS_UNAVAILABLPAR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INVALID_REVISION_STRING"/>.
//		/// </summary>
//		ERR_INVALID_REVISION_STRING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INVALID_REVISION_STRING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULETYPE_STRING_EMPTY"/>.
//		/// </summary>
//		ERR_MODULETYPE_STRING_EMPTY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULETYPE_STRING_EMPTY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULELABEL_STRING_EMPTY"/>.
//		/// </summary>
//		ERR_MODULELABEL_STRING_EMPTY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULELABEL_STRING_EMPTY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INVALID_MAC_ADDRESS_STRING"/>.
//		/// </summary>
//		ERR_INVALID_MAC_ADDRESS_STRING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INVALID_MAC_ADDRESS_STRING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SCALING_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_CHANGED_TO_STANDARD_FOR_SENSORTYPE"/>.
//		/// </summary>
//		WARN_SCALING_CHANGED_TO_STANDARD_FOR_SENSORTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_CHANGED_TO_STANDARD_FOR_SENSORTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_NOT_SUPPORTED_BY_SENSORTYPE"/>.
//		/// </summary>
//		ERR_SCALING_NOT_SUPPORTED_BY_SENSORTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_NOT_SUPPORTED_BY_SENSORTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_INVALID_TYPE"/>.
//		/// </summary>
//		ERR_SCALING_INVALID_TYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_INVALID_TYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_STRAINGAGE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SCALING_STRAINGAGE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_STRAINGAGE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RESTORING_FACTORY_SCALING"/>.
//		/// </summary>
//		ERR_RESTORING_FACTORY_SCALING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RESTORING_FACTORY_SCALING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_DIV_BY_ZERO"/>.
//		/// </summary>
//		ERR_SCALING_DIV_BY_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_DIV_BY_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FILTERTYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_FILTERTYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FILTERTYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FILTERCHARACT_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_FILTERCHARACT_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FILTERCHARACT_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FILTERFREQ_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_FILTERFREQ_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FILTERFREQ_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_2ND_SIGNAL"/>.
//		/// </summary>
//		ERR_FIR_FILTER_NOT_SUPPORTED_BY_2ND_SIGNAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_2ND_SIGNAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_2ND_SIGNAL"/>.
//		/// </summary>
//		WARN_FIR_FILTER_NOT_SUPPORTED_BY_2ND_SIGNAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_2ND_SIGNAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_CLASSIC"/>.
//		/// </summary>
//		ERR_FIR_FILTER_NOT_SUPPORTED_BY_CLASSIC = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_CLASSIC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_CLASSIC"/>.
//		/// </summary>
//		WARN_FIR_FILTER_NOT_SUPPORTED_BY_CLASSIC = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_CLASSIC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_MODULE"/>.
//		/// </summary>
//		ERR_FIR_FILTER_NOT_SUPPORTED_BY_MODULE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_MODULE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_MODULE"/>.
//		/// </summary>
//		WARN_FIR_FILTER_NOT_SUPPORTED_BY_MODULE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_MODULE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE"/>.
//		/// </summary>
//		ERR_FIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE"/>.
//		/// </summary>
//		WARN_FIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IIR_FILTER_NOT_SUPPORTED_BY_MODULE"/>.
//		/// </summary>
//		ERR_IIR_FILTER_NOT_SUPPORTED_BY_MODULE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IIR_FILTER_NOT_SUPPORTED_BY_MODULE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_IIR_FILTER_NOT_SUPPORTED_BY_MODULE"/>.
//		/// </summary>
//		WARN_IIR_FILTER_NOT_SUPPORTED_BY_MODULE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_IIR_FILTER_NOT_SUPPORTED_BY_MODULE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE"/>.
//		/// </summary>
//		ERR_IIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_IIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE"/>.
//		/// </summary>
//		WARN_IIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_IIR_FILTER_NOT_SUPPORTED_BY_SENSOR_TYPE_OR_MEAS_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTERCHAR_CHANGED_TO_IIR_BESSEL_FIR_NO_HIGHSPEED"/>.
//		/// </summary>
//		WARN_FILTERCHAR_CHANGED_TO_IIR_BESSEL_FIR_NO_HIGHSPEED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTERCHAR_CHANGED_TO_IIR_BESSEL_FIR_NO_HIGHSPEED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTERCHAR_CHANGED_TO_IIR_BUTTERWORTH_FIR_NO_HIGHSPEED"/>.
//		/// </summary>
//		WARN_FILTERCHAR_CHANGED_TO_IIR_BUTTERWORTH_FIR_NO_HIGHSPEED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTERCHAR_CHANGED_TO_IIR_BUTTERWORTH_FIR_NO_HIGHSPEED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_HIGHSPEED"/>.
//		/// </summary>
//		ERR_FIR_FILTER_NOT_SUPPORTED_BY_HIGHSPEED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FIR_FILTER_NOT_SUPPORTED_BY_HIGHSPEED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_HIGHSPEED"/>.
//		/// </summary>
//		WARN_FIR_FILTER_NOT_SUPPORTED_BY_HIGHSPEED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_HIGHSPEED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_SWITCHED_OFF_FIR_NO_HIGHSPEED"/>.
//		/// </summary>
//		WARN_FILTER_SWITCHED_OFF_FIR_NO_HIGHSPEED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_SWITCHED_OFF_FIR_NO_HIGHSPEED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_CHARACT_NOT_SUPPORTED_CHANGED_TO_OFF"/>.
//		/// </summary>
//		WARN_FILTER_CHARACT_NOT_SUPPORTED_CHANGED_TO_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_CHARACT_NOT_SUPPORTED_CHANGED_TO_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_CHARACT_NOT_SUPPORTED_BY_2ND_SIGNAL_CHANGED_TO_OFF"/>.
//		/// </summary>
//		WARN_FILTER_CHARACT_NOT_SUPPORTED_BY_2ND_SIGNAL_CHANGED_TO_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_CHARACT_NOT_SUPPORTED_BY_2ND_SIGNAL_CHANGED_TO_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_CHARACT_NOT_SUPPORTED_BY_HIGHSPEED_CHANGED_TO_OFF"/>.
//		/// </summary>
//		WARN_FILTER_CHARACT_NOT_SUPPORTED_BY_HIGHSPEED_CHANGED_TO_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_CHARACT_NOT_SUPPORTED_BY_HIGHSPEED_CHANGED_TO_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_CLASSIC_CHANGED_TO_OFF"/>.
//		/// </summary>
//		WARN_FIR_FILTER_NOT_SUPPORTED_BY_CLASSIC_CHANGED_TO_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FIR_FILTER_NOT_SUPPORTED_BY_CLASSIC_CHANGED_TO_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_IIR_BESSEL"/>.
//		/// </summary>
//		WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_IIR_BESSEL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_IIR_BESSEL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_IIR_BUTTERWORTH"/>.
//		/// </summary>
//		WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_IIR_BUTTERWORTH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_IIR_BUTTERWORTH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_FIR_LINEARPHASE"/>.
//		/// </summary>
//		WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_FIR_LINEARPHASE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_FIR_LINEARPHASE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_FIR_BUTTERWORTH"/>.
//		/// </summary>
//		WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_FIR_BUTTERWORTH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_OFF_NOT_SUPPORTED_CHANGED_TO_MAX_FIR_BUTTERWORTH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONNMODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_CONNMODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONNMODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SENSORTYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SENSORTYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SENSORTYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_OUTPUTRATE_VS_FILTER_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_OUTPUTRATE_VS_FILTER_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_OUTPUTRATE_VS_FILTER_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_OUTPUTRATE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_OUTPUTRATE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_OUTPUTRATE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSICAL_UNIT_NOT_NATIVE"/>.
//		/// </summary>
//		ERR_PHYSICAL_UNIT_NOT_NATIVE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSICAL_UNIT_NOT_NATIVE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PHYSICAL_UNIT_NOT_NATIVE"/>.
//		/// </summary>
//		WARN_PHYSICAL_UNIT_NOT_NATIVE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PHYSICAL_UNIT_NOT_NATIVE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PHYSICAL_UNIT_CHANGED_TO_NATIVE"/>.
//		/// </summary>
//		WARN_PHYSICAL_UNIT_CHANGED_TO_NATIVE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PHYSICAL_UNIT_CHANGED_TO_NATIVE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_VOLTAGERANGE_NOT_SUPP"/>.
//		/// </summary>
//		ERR_VOLTAGERANGE_NOT_SUPP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_VOLTAGERANGE_NOT_SUPP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CURRENTRANGE_NOT_SUPP"/>.
//		/// </summary>
//		ERR_CURRENTRANGE_NOT_SUPP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CURRENTRANGE_NOT_SUPP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WIRING_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_WIRING_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WIRING_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WIRING_NOT_SUPPORTED_CHECK_HW_REVISION"/>.
//		/// </summary>
//		ERR_WIRING_NOT_SUPPORTED_CHECK_HW_REVISION = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WIRING_NOT_SUPPORTED_CHECK_HW_REVISION,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXCITFREQ_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_EXCITFREQ_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXCITFREQ_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXCITAMPL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_EXCITAMPL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXCITAMPL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IMPEDANCE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_IMPEDANCE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IMPEDANCE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PT_RESISTANCE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_PT_RESISTANCE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PT_RESISTANCE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TC_TYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TC_TYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TC_TYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RESISTOR_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_RESISTOR_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RESISTOR_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SSI_CODING_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SSI_CODING_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SSI_CODING_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_RANGE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_PRESSURE_RANGE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_RANGE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_MAX_AVAILABLE"/>.
//		/// </summary>
//		ERR_PRESSURE_MAX_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_MAX_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_TYPE_ONLY_ABSOLUTE_SUPPORTED"/>.
//		/// </summary>
//		ERR_PRESSURE_TYPE_ONLY_ABSOLUTE_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_TYPE_ONLY_ABSOLUTE_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_TYPE_ONLY_RELATIVE_SUPPORTED"/>.
//		/// </summary>
//		ERR_PRESSURE_TYPE_ONLY_RELATIVE_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_TYPE_ONLY_RELATIVE_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_TYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_PRESSURE_TYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_TYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_SENSOR_NOT_AVAILABLE"/>.
//		/// </summary>
//		ERR_PRESSURE_SENSOR_NOT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_SENSOR_NOT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_MIN_AVAILABLE"/>.
//		/// </summary>
//		ERR_PRESSURE_MIN_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PRESSURE_MIN_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FREQUENCYRANGE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_FREQUENCYRANGE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FREQUENCYRANGE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_REQUESTED_MAX_COUNTER_EXCEEDS_POSSIBLE"/>.
//		/// </summary>
//		WARN_REQUESTED_MAX_COUNTER_EXCEEDS_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_REQUESTED_MAX_COUNTER_EXCEEDS_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SENSORSUPPLY_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SENSORSUPPLY_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SENSORSUPPLY_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SET_SENSOR_SUPPLY_NOT_ALLOWED_IF_USED_BY_PARAMETER_SET"/>.
//		/// </summary>
//		ERR_SET_SENSOR_SUPPLY_NOT_ALLOWED_IF_USED_BY_PARAMETER_SET = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SET_SENSOR_SUPPLY_NOT_ALLOWED_IF_USED_BY_PARAMETER_SET,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SETZERO_INHIBITED"/>.
//		/// </summary>
//		ERR_SETZERO_INHIBITED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SETZERO_INHIBITED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SETZERO_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SETZERO_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SETZERO_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOG_OUT_SETZERO"/>.
//		/// </summary>
//		ERR_ANALOG_OUT_SETZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOG_OUT_SETZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IND_COUNTER"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_IND_COUNTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IND_COUNTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IND_FREQUENCY"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_IND_FREQUENCY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IND_FREQUENCY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_BRIDGE"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_BRIDGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_BRIDGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_VOLTAGE"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_VOLTAGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_VOLTAGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_CURRENT"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_CURRENT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_CURRENT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_THERMOCOUPLE"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_THERMOCOUPLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_THERMOCOUPLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PT_ELEMENT"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_PT_ELEMENT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PT_ELEMENT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_POTI"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_POTI = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_POTI,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_SSI"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_SSI = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_SSI,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_COUNTER"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_COUNTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_COUNTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_FREQUENCY"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_FREQUENCY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_FREQUENCY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_INDBRIDGE"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_INDBRIDGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_INDBRIDGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_LVDT"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_LVDT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_LVDT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PRESSURE"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_PRESSURE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PRESSURE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IEPE"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_IEPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IEPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PWM"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_PWM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PWM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PULSEWIDTH"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_PULSEWIDTH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PULSEWIDTH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PERIOD"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_PERIOD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_PERIOD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_RESISTOR"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_RESISTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_RESISTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IRIG_FORMAT_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_IRIG_FORMAT_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IRIG_FORMAT_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CONNECTOR_SET_TO_VOLTAGE_BECAUSE_ANOTHER_SET_TO_IRIG"/>.
//		/// </summary>
//		WARN_CONNECTOR_SET_TO_VOLTAGE_BECAUSE_ANOTHER_SET_TO_IRIG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CONNECTOR_SET_TO_VOLTAGE_BECAUSE_ANOTHER_SET_TO_IRIG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_IRIG_SENSOR_CAN_ONLY_BE_ACTIVE_ONCE"/>.
//		/// </summary>
//		WARN_IRIG_SENSOR_CAN_ONLY_BE_ACTIVE_ONCE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_IRIG_SENSOR_CAN_ONLY_BE_ACTIVE_ONCE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IND_CRANK_COUNTER"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_IND_CRANK_COUNTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IND_CRANK_COUNTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IND_CRANK_FREQUENCY"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_IND_CRANK_FREQUENCY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_IND_CRANK_FREQUENCY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GAP_POSITION_SYNTAX"/>.
//		/// </summary>
//		ERR_GAP_POSITION_SYNTAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GAP_POSITION_SYNTAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GAP_POSITION_ZERO_NOT_ALLOWED"/>.
//		/// </summary>
//		ERR_GAP_POSITION_ZERO_NOT_ALLOWED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GAP_POSITION_ZERO_NOT_ALLOWED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GAP_POSITION_REPETITION_NOT_ALLOWED"/>.
//		/// </summary>
//		ERR_GAP_POSITION_REPETITION_NOT_ALLOWED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GAP_POSITION_REPETITION_NOT_ALLOWED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GAP_POSITION_VALUE_GREATER_THAN_NUMBER_OF_TEETH"/>.
//		/// </summary>
//		ERR_GAP_POSITION_VALUE_GREATER_THAN_NUMBER_OF_TEETH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GAP_POSITION_VALUE_GREATER_THAN_NUMBER_OF_TEETH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INDEX_POSITION_VALUE_GREATER_THAN_NUMBER_OF_TEETH"/>.
//		/// </summary>
//		ERR_INDEX_POSITION_VALUE_GREATER_THAN_NUMBER_OF_TEETH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INDEX_POSITION_VALUE_GREATER_THAN_NUMBER_OF_TEETH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TOO_MUCH_GAP_POSITIONS_DEFINED"/>.
//		/// </summary>
//		ERR_TOO_MUCH_GAP_POSITIONS_DEFINED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TOO_MUCH_GAP_POSITIONS_DEFINED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PULSES_PER_ROUND_OUT_OF_RANGE"/>.
//		/// </summary>
//		ERR_PULSES_PER_ROUND_OUT_OF_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PULSES_PER_ROUND_OUT_OF_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PULSES_PER_ROUND_OUT_OF_RANGE_SET_TO_ZERO"/>.
//		/// </summary>
//		WARN_PULSES_PER_ROUND_OUT_OF_RANGE_SET_TO_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PULSES_PER_ROUND_OUT_OF_RANGE_SET_TO_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INDEX_SHIFT_OUT_OF_RANGE"/>.
//		/// </summary>
//		ERR_INDEX_SHIFT_OUT_OF_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INDEX_SHIFT_OUT_OF_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_INDEX_SHIFT_OUT_OF_RANGE_SET_TO_ZERO"/>.
//		/// </summary>
//		WARN_INDEX_SHIFT_OUT_OF_RANGE_SET_TO_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_INDEX_SHIFT_OUT_OF_RANGE_SET_TO_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_INDEX_DIVISOR_OUT_OF_RANGE_SET_TO_MINIMUM"/>.
//		/// </summary>
//		WARN_INDEX_DIVISOR_OUT_OF_RANGE_SET_TO_MINIMUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_INDEX_DIVISOR_OUT_OF_RANGE_SET_TO_MINIMUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_INDEX_DIVISOR_OUT_OF_RANGE_SET_TO_MAXIMUM"/>.
//		/// </summary>
//		WARN_INDEX_DIVISOR_OUT_OF_RANGE_SET_TO_MAXIMUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_INDEX_DIVISOR_OUT_OF_RANGE_SET_TO_MAXIMUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_CRANK_COUNTER"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_CRANK_COUNTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_CRANK_COUNTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_CRANK_FREQUENCY"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_CRANK_FREQUENCY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_CRANK_FREQUENCY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_ZERO_NOT_VALID"/>.
//		/// </summary>
//		ERR_PARAM_ZERO_NOT_VALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_ZERO_NOT_VALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PREVIOUS_SETTING_RESTORED"/>.
//		/// </summary>
//		ERR_PREVIOUS_SETTING_RESTORED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PREVIOUS_SETTING_RESTORED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMETERIZATION_REFUSED"/>.
//		/// </summary>
//		ERR_PARAMETERIZATION_REFUSED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMETERIZATION_REFUSED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_LINTABLE_WRONG_NUMBER_OF_POINTS"/>.
//		/// </summary>
//		ERR_SCALING_LINTABLE_WRONG_NUMBER_OF_POINTS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_LINTABLE_WRONG_NUMBER_OF_POINTS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_NR_OF_SEGMENTS_CHANGED"/>.
//		/// </summary>
//		WARN_SCALING_NR_OF_SEGMENTS_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_NR_OF_SEGMENTS_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_NR_OF_COEFFS_CHANGED"/>.
//		/// </summary>
//		WARN_SCALING_NR_OF_COEFFS_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_NR_OF_COEFFS_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPACE_IN_STRING_NOT_ALLOWED"/>.
//		/// </summary>
//		ERR_SPACE_IN_STRING_NOT_ALLOWED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPACE_IN_STRING_NOT_ALLOWED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_STRING_SIZE_TOO_LONG"/>.
//		/// </summary>
//		ERR_STRING_SIZE_TOO_LONG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_STRING_SIZE_TOO_LONG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCM_HV_NOT_SUPPORTED_BY_VOLTAGE_RANGE"/>.
//		/// </summary>
//		ERR_SCM_HV_NOT_SUPPORTED_BY_VOLTAGE_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCM_HV_NOT_SUPPORTED_BY_VOLTAGE_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_VOLTAGE_EXCEEDS_MEAS_RANGE"/>.
//		/// </summary>
//		ERR_VOLTAGE_EXCEEDS_MEAS_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_VOLTAGE_EXCEEDS_MEAS_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HIGHVOLTAGE_ADAPTOR_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_HIGHVOLTAGE_ADAPTOR_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HIGHVOLTAGE_ADAPTOR_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_VOLTAGE_RANGE_OR_HIGHVOLTAGE_ADAPTOR_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_VOLTAGE_RANGE_OR_HIGHVOLTAGE_ADAPTOR_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_VOLTAGE_RANGE_OR_HIGHVOLTAGE_ADAPTOR_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXTERNAL_HIGHVOLTAGE_ADAPTOR_NOT_CONNECTED"/>.
//		/// </summary>
//		WARN_EXTERNAL_HIGHVOLTAGE_ADAPTOR_NOT_CONNECTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXTERNAL_HIGHVOLTAGE_ADAPTOR_NOT_CONNECTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXTERNAL_HIGHVOLTAGE_ADAPTOR_NOT_CONNECTED"/>.
//		/// </summary>
//		ERR_EXTERNAL_HIGHVOLTAGE_ADAPTOR_NOT_CONNECTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXTERNAL_HIGHVOLTAGE_ADAPTOR_NOT_CONNECTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXTERNAL_QUARTER_BRIDGE_ADAPTOR_NOT_CONNECTED"/>.
//		/// </summary>
//		WARN_EXTERNAL_QUARTER_BRIDGE_ADAPTOR_NOT_CONNECTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXTERNAL_QUARTER_BRIDGE_ADAPTOR_NOT_CONNECTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXTERNAL_QUARTER_BRIDGE_ADAPTOR_NOT_CONNECTED"/>.
//		/// </summary>
//		ERR_EXTERNAL_QUARTER_BRIDGE_ADAPTOR_NOT_CONNECTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXTERNAL_QUARTER_BRIDGE_ADAPTOR_NOT_CONNECTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXTERNAL_ADAPTOR_COEFFS_NOT_AVAILABLE"/>.
//		/// </summary>
//		ERR_EXTERNAL_ADAPTOR_COEFFS_NOT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXTERNAL_ADAPTOR_COEFFS_NOT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_OUTPUTUNIT_INCOMPATIBLE_TO_SENSORUNIT"/>.
//		/// </summary>
//		WARN_OUTPUTUNIT_INCOMPATIBLE_TO_SENSORUNIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_OUTPUTUNIT_INCOMPATIBLE_TO_SENSORUNIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_OUTPUTUNIT_DISABLED_BECAUSE_PHYS_UNIT_USER_DEF"/>.
//		/// </summary>
//		WARN_OUTPUTUNIT_DISABLED_BECAUSE_PHYS_UNIT_USER_DEF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_OUTPUTUNIT_DISABLED_BECAUSE_PHYS_UNIT_USER_DEF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_OUTPUTUNIT_IS_USER_DEFINED"/>.
//		/// </summary>
//		WARN_OUTPUTUNIT_IS_USER_DEFINED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_OUTPUTUNIT_IS_USER_DEFINED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_REPTIME_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_AUTOCAL_REPTIME_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_REPTIME_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_NOT_SUPPORTED_SENSOR"/>.
//		/// </summary>
//		ERR_AUTOCAL_NOT_SUPPORTED_SENSOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_NOT_SUPPORTED_SENSOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_DISABLED"/>.
//		/// </summary>
//		ERR_AUTOCAL_DISABLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_DISABLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_SENSOR_NOT_CONNECTED"/>.
//		/// </summary>
//		ERR_AUTOCAL_SENSOR_NOT_CONNECTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_SENSOR_NOT_CONNECTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_NOT_SUPPORTED_MODULE"/>.
//		/// </summary>
//		ERR_AUTOCAL_NOT_SUPPORTED_MODULE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_NOT_SUPPORTED_MODULE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUSTMENT_SENSOR_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_ADJUSTMENT_SENSOR_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUSTMENT_SENSOR_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUSTMENT_SENSOR_NOT_SUPPORTED"/>.
//		/// </summary>
//		WARN_ADJUSTMENT_SENSOR_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUSTMENT_SENSOR_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUSTMENT_REPTIME_TOO_SHORT"/>.
//		/// </summary>
//		WARN_ADJUSTMENT_REPTIME_TOO_SHORT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUSTMENT_REPTIME_TOO_SHORT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUSTMENT_REPTIME_TOO_LONG"/>.
//		/// </summary>
//		WARN_ADJUSTMENT_REPTIME_TOO_LONG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUSTMENT_REPTIME_TOO_LONG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUSTMENT_NOT_DEFAULT"/>.
//		/// </summary>
//		WARN_ADJUSTMENT_NOT_DEFAULT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUSTMENT_NOT_DEFAULT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOADJUST_NOT_SUPPORTED_SENSOR"/>.
//		/// </summary>
//		ERR_AUTOADJUST_NOT_SUPPORTED_SENSOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOADJUST_NOT_SUPPORTED_SENSOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_CYCLE_IS_RUNNING_REPEAT_LATER"/>.
//		/// </summary>
//		ERR_AUTOCAL_CYCLE_IS_RUNNING_REPEAT_LATER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AUTOCAL_CYCLE_IS_RUNNING_REPEAT_LATER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_SENSOR_TEMPLATE"/>.
//		/// </summary>
//		ERR_TEDS_NO_SENSOR_TEMPLATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_SENSOR_TEMPLATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_DATA_OR_INVALID"/>.
//		/// </summary>
//		ERR_TEDS_NO_DATA_OR_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_DATA_OR_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MIN_PHYS_VAL"/>.
//		/// </summary>
//		ERR_TEDS_NO_MIN_PHYS_VAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MIN_PHYS_VAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MAX_PHYS_VAL"/>.
//		/// </summary>
//		ERR_TEDS_NO_MAX_PHYS_VAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MAX_PHYS_VAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MIN_ELEC_VAL"/>.
//		/// </summary>
//		ERR_TEDS_NO_MIN_ELEC_VAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MIN_ELEC_VAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MAX_ELEC_VAL"/>.
//		/// </summary>
//		ERR_TEDS_NO_MAX_ELEC_VAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MAX_ELEC_VAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MDEF_UCC"/>.
//		/// </summary>
//		ERR_TEDS_NO_MDEF_UCC = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MDEF_UCC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALCURVE_COEFFS_INVALID"/>.
//		/// </summary>
//		ERR_TEDS_CALCURVE_COEFFS_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALCURVE_COEFFS_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NODATA"/>.
//		/// </summary>
//		ERR_TEDS_NODATA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NODATA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NOPARSEBIN"/>.
//		/// </summary>
//		ERR_TEDS_NOPARSEBIN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NOPARSEBIN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NOAMP"/>.
//		/// </summary>
//		ERR_TEDS_NOAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NOAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_EXCITE_AMPL_NOM"/>.
//		/// </summary>
//		ERR_TEDS_NO_EXCITE_AMPL_NOM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_EXCITE_AMPL_NOM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_EXCITE_AMPL_MIN"/>.
//		/// </summary>
//		ERR_TEDS_NO_EXCITE_AMPL_MIN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_EXCITE_AMPL_MIN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_EXCITE_AMPL_MAX"/>.
//		/// </summary>
//		ERR_TEDS_NO_EXCITE_AMPL_MAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_EXCITE_AMPL_MAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_ATTACHED_SERIALNUM"/>.
//		/// </summary>
//		ERR_TEDS_NO_ATTACHED_SERIALNUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_ATTACHED_SERIALNUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_SENSOR_IMPED"/>.
//		/// </summary>
//		ERR_TEDS_NO_SENSOR_IMPED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_SENSOR_IMPED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_GAUGEFACTOR"/>.
//		/// </summary>
//		ERR_TEDS_GAUGEFACTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_GAUGEFACTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_BRIDGETYPE"/>.
//		/// </summary>
//		ERR_TEDS_NO_BRIDGETYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_BRIDGETYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_TC_TYPE"/>.
//		/// </summary>
//		ERR_TEDS_TC_TYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_TC_TYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_PT_TYPE"/>.
//		/// </summary>
//		ERR_TEDS_PT_TYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_PT_TYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_LOWPASSFILTER"/>.
//		/// </summary>
//		ERR_TEDS_LOWPASSFILTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_LOWPASSFILTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_ZEROSPANSCALING"/>.
//		/// </summary>
//		ERR_TEDS_ZEROSPANSCALING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_ZEROSPANSCALING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_GAINSCALING"/>.
//		/// </summary>
//		ERR_TEDS_GAINSCALING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_GAINSCALING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_PARAM"/>.
//		/// </summary>
//		ERR_TEDS_PARAM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_PARAM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NOT_SUPPORTED_BY_CONNECTOR"/>.
//		/// </summary>
//		ERR_TEDS_NOT_SUPPORTED_BY_CONNECTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NOT_SUPPORTED_BY_CONNECTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TEDS_NOT_SUPPORTED_CHANGED_TO_IGNORE"/>.
//		/// </summary>
//		WARN_TEDS_NOT_SUPPORTED_CHANGED_TO_IGNORE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TEDS_NOT_SUPPORTED_CHANGED_TO_IGNORE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_CALTABLE"/>.
//		/// </summary>
//		ERR_TEDS_NO_CALTABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_CALTABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_CALCURVE"/>.
//		/// </summary>
//		ERR_TEDS_NO_CALCURVE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_CALCURVE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALCURVE_POWER_INVALID"/>.
//		/// </summary>
//		ERR_TEDS_CALCURVE_POWER_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALCURVE_POWER_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALTABLE_NOT_ENOUGH_POINTS"/>.
//		/// </summary>
//		ERR_TEDS_CALTABLE_NOT_ENOUGH_POINTS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALTABLE_NOT_ENOUGH_POINTS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALTABLE_TOO_MUCH_POINTS"/>.
//		/// </summary>
//		ERR_TEDS_CALTABLE_TOO_MUCH_POINTS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALTABLE_TOO_MUCH_POINTS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_BUFFER_TOO_SMALL"/>.
//		/// </summary>
//		ERR_TEDS_BUFFER_TOO_SMALL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_BUFFER_TOO_SMALL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_WRITE_NOT_ALLOWED"/>.
//		/// </summary>
//		ERR_TEDS_WRITE_NOT_ALLOWED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_WRITE_NOT_ALLOWED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_TID_DONT_MATCH"/>.
//		/// </summary>
//		ERR_TEDS_TID_DONT_MATCH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_TID_DONT_MATCH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_SCALINGTYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TEDS_SCALINGTYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_SCALINGTYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_SCALING_PARAMETER_INVALID"/>.
//		/// </summary>
//		ERR_TEDS_SCALING_PARAMETER_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_SCALING_PARAMETER_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_UNIT_CONVERSION_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TEDS_UNIT_CONVERSION_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_UNIT_CONVERSION_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPEEDMODE_CHANGING_NOT_POSSIBLE_SWITCH_OFF"/>.
//		/// </summary>
//		ERR_SPEEDMODE_CHANGING_NOT_POSSIBLE_SWITCH_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPEEDMODE_CHANGING_NOT_POSSIBLE_SWITCH_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPEEDMODE_TOO_MANY_CONNECTORS_HIGH"/>.
//		/// </summary>
//		ERR_SPEEDMODE_TOO_MANY_CONNECTORS_HIGH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPEEDMODE_TOO_MANY_CONNECTORS_HIGH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPEEDMODE_MIXED_MODE_NOT_POSSIBLE"/>.
//		/// </summary>
//		ERR_SPEEDMODE_MIXED_MODE_NOT_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPEEDMODE_MIXED_MODE_NOT_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SPEEDMODE_CHANGED_TO_HIGH"/>.
//		/// </summary>
//		WARN_SPEEDMODE_CHANGED_TO_HIGH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SPEEDMODE_CHANGED_TO_HIGH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HIGHSPEEDMODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		WARN_HIGHSPEEDMODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HIGHSPEEDMODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_PARAMS_DONT_SUPPORT_HIGHSPEEDMODE"/>.
//		/// </summary>
//		WARN_SCALING_PARAMS_DONT_SUPPORT_HIGHSPEEDMODE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_PARAMS_DONT_SUPPORT_HIGHSPEEDMODE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPEEDMODE_HIGH_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SPEEDMODE_HIGH_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPEEDMODE_HIGH_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SENSOR_PARAMS_DONT_SUPPORT_HIGHSPEEDMODE"/>.
//		/// </summary>
//		WARN_SENSOR_PARAMS_DONT_SUPPORT_HIGHSPEEDMODE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SENSOR_PARAMS_DONT_SUPPORT_HIGHSPEEDMODE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONNECTORSOURCE"/>.
//		/// </summary>
//		ERR_CONNECTORSOURCE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONNECTORSOURCE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSUNIT_NOT_FOUND"/>.
//		/// </summary>
//		ERR_PHYSUNIT_NOT_FOUND = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSUNIT_NOT_FOUND,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSUNIT_NOT_RELATED"/>.
//		/// </summary>
//		ERR_PHYSUNIT_NOT_RELATED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSUNIT_NOT_RELATED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSUNIT_SCALING_FACTOR_ZERO"/>.
//		/// </summary>
//		ERR_PHYSUNIT_SCALING_FACTOR_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSUNIT_SCALING_FACTOR_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SUPPLY_VOLTAGE_SWITCHED_OFF"/>.
//		/// </summary>
//		WARN_SUPPLY_VOLTAGE_SWITCHED_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SUPPLY_VOLTAGE_SWITCHED_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_WIRING_ALTERED"/>.
//		/// </summary>
//		WARN_WIRING_ALTERED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_WIRING_ALTERED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SUPPLY_VOLTAGE_CHANGED_TO_MAX_POSSIBLE"/>.
//		/// </summary>
//		WARN_SUPPLY_VOLTAGE_CHANGED_TO_MAX_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SUPPLY_VOLTAGE_CHANGED_TO_MAX_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXCITATION_FREQ_CHANGED"/>.
//		/// </summary>
//		WARN_EXCITATION_FREQ_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXCITATION_FREQ_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXCITATION_AMPL_CHANGED"/>.
//		/// </summary>
//		WARN_EXCITATION_AMPL_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXCITATION_AMPL_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SUPPLY_VOLTAGE_BELOW_MINIMUM"/>.
//		/// </summary>
//		ERR_SUPPLY_VOLTAGE_BELOW_MINIMUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SUPPLY_VOLTAGE_BELOW_MINIMUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SSI_CLK_FREQ_CHANGED"/>.
//		/// </summary>
//		WARN_SSI_CLK_FREQ_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SSI_CLK_FREQ_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SSI_RESOLUTION_CHANGED"/>.
//		/// </summary>
//		WARN_SSI_RESOLUTION_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SSI_RESOLUTION_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_CUTOFF_FREQ_CHANGED"/>.
//		/// </summary>
//		WARN_FILTER_CUTOFF_FREQ_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTER_CUTOFF_FREQ_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTERTYPE_CHANGED"/>.
//		/// </summary>
//		WARN_FILTERTYPE_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTERTYPE_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTERCHAR_CHANGED"/>.
//		/// </summary>
//		WARN_FILTERCHAR_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_FILTERCHAR_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_OUTPUTRATE_CHANGED"/>.
//		/// </summary>
//		WARN_OUTPUTRATE_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_OUTPUTRATE_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GLITCHFILTER_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_GLITCHFILTER_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GLITCHFILTER_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_GLITCHFILTER_CHANGED"/>.
//		/// </summary>
//		WARN_GLITCHFILTER_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_GLITCHFILTER_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ERROR"/>.
//		/// </summary>
//		ERRHAL_ERROR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ERROR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_OPENMSPDRV"/>.
//		/// </summary>
//		ERRHAL_OPENMSPDRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_OPENMSPDRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_OPENCPLDDRV"/>.
//		/// </summary>
//		ERRHAL_OPENCPLDDRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_OPENCPLDDRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETAMPTEMP"/>.
//		/// </summary>
//		ERRHAL_GETAMPTEMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETAMPTEMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETAMPTEMPCAL"/>.
//		/// </summary>
//		ERRHAL_GETAMPTEMPCAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETAMPTEMPCAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_READ_I2C"/>.
//		/// </summary>
//		ERRHAL_READ_I2C = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_READ_I2C,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_WRITE_I2C"/>.
//		/// </summary>
//		ERRHAL_WRITE_I2C = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_WRITE_I2C,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_WRITE_MSP"/>.
//		/// </summary>
//		ERRHAL_WRITE_MSP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_WRITE_MSP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ENABLECHN"/>.
//		/// </summary>
//		ERRHAL_ENABLECHN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ENABLECHN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETMEASINPUT"/>.
//		/// </summary>
//		ERRHAL_SETMEASINPUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETMEASINPUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSENSCIRCUIT"/>.
//		/// </summary>
//		ERRHAL_SETSENSCIRCUIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSENSCIRCUIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETCALINPUT"/>.
//		/// </summary>
//		ERRHAL_SETCALINPUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETCALINPUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETGAINCAL"/>.
//		/// </summary>
//		ERRHAL_SETGAINCAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETGAINCAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETGAINMEAS"/>.
//		/// </summary>
//		ERRHAL_SETGAINMEAS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETGAINMEAS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SET_SENSORSUPPLY"/>.
//		/// </summary>
//		ERRHAL_SET_SENSORSUPPLY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SET_SENSORSUPPLY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETFEEDBACK"/>.
//		/// </summary>
//		ERRHAL_SETFEEDBACK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETFEEDBACK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETEXCIT"/>.
//		/// </summary>
//		ERRHAL_SETEXCIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETEXCIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETSNAMP"/>.
//		/// </summary>
//		ERRHAL_GETSNAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETSNAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETSNAMP"/>.
//		/// </summary>
//		WARN_HAL_GETSNAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETSNAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSNAMP"/>.
//		/// </summary>
//		ERRHAL_SETSNAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSNAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETHWRAMP"/>.
//		/// </summary>
//		ERRHAL_GETHWRAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETHWRAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETHWRAMP"/>.
//		/// </summary>
//		WARN_HAL_GETHWRAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETHWRAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETHWRAMP"/>.
//		/// </summary>
//		ERRHAL_SETHWRAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETHWRAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETTID"/>.
//		/// </summary>
//		ERRHAL_GETTID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETTID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETTEDSDATHW"/>.
//		/// </summary>
//		ERRHAL_GETTEDSDATHW = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETTEDSDATHW,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETTEDSDAT"/>.
//		/// </summary>
//		ERRHAL_GETTEDSDAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETTEDSDAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_TEDSCHECKSUM"/>.
//		/// </summary>
//		ERRHAL_TEDSCHECKSUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_TEDSCHECKSUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_NOTEDS"/>.
//		/// </summary>
//		ERRHAL_NOTEDS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_NOTEDS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_TEDS_ALLZERO"/>.
//		/// </summary>
//		ERRHAL_TEDS_ALLZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_TEDS_ALLZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_TEDSCHECKSUMINSERT"/>.
//		/// </summary>
//		ERRHAL_TEDSCHECKSUMINSERT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_TEDSCHECKSUMINSERT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTEDSDAT"/>.
//		/// </summary>
//		ERRHAL_SETTEDSDAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTEDSDAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTEDSCON"/>.
//		/// </summary>
//		ERRHAL_SETTEDSCON = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTEDSCON,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SIZETEDSDAT"/>.
//		/// </summary>
//		ERRHAL_SIZETEDSDAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SIZETEDSDAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETLEDAMP"/>.
//		/// </summary>
//		ERRHAL_SETLEDAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETLEDAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSYSLEDAMP"/>.
//		/// </summary>
//		ERRHAL_SETSYSLEDAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSYSLEDAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_WRSETTING"/>.
//		/// </summary>
//		ERRHAL_WRSETTING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_WRSETTING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARNHAL_WRSETTING"/>.
//		/// </summary>
//		WARNHAL_WRSETTING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARNHAL_WRSETTING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTING_NOT_AVAILABLE"/>.
//		/// </summary>
//		ERRHAL_SETTING_NOT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTING_NOT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETADCCLK"/>.
//		/// </summary>
//		ERRHAL_SETADCCLK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETADCCLK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMAXSRAMP"/>.
//		/// </summary>
//		ERRHAL_GETMAXSRAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMAXSRAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SET_FM_CONF"/>.
//		/// </summary>
//		ERRHAL_SET_FM_CONF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SET_FM_CONF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SET_SSI_CONF"/>.
//		/// </summary>
//		ERRHAL_SET_SSI_CONF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SET_SSI_CONF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPVERAMP"/>.
//		/// </summary>
//		ERRHAL_GETMSPVERAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPVERAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPSTATAMP"/>.
//		/// </summary>
//		ERRHAL_GETMSPSTATAMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPSTATAMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SYNCADC"/>.
//		/// </summary>
//		ERRHAL_SYNCADC = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SYNCADC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETCHNPROP"/>.
//		/// </summary>
//		ERRHAL_SETCHNPROP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETCHNPROP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPREG"/>.
//		/// </summary>
//		ERRHAL_GETMSPREG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPREG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETMSPREG"/>.
//		/// </summary>
//		ERRHAL_SETMSPREG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETMSPREG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTSTSIGMEAS"/>.
//		/// </summary>
//		ERRHAL_SETTSTSIGMEAS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTSTSIGMEAS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTSTSIGCAL"/>.
//		/// </summary>
//		ERRHAL_SETTSTSIGCAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTSTSIGCAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSIMUVAL"/>.
//		/// </summary>
//		ERRHAL_SETSIMUVAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSIMUVAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSIMUVALFORMAT"/>.
//		/// </summary>
//		ERRHAL_SETSIMUVALFORMAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSIMUVALFORMAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSIMUVALTYPE"/>.
//		/// </summary>
//		ERRHAL_SETSIMUVALTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSIMUVALTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETSNAPSHOT"/>.
//		/// </summary>
//		ERRHAL_GETSNAPSHOT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETSNAPSHOT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SNAPSHOT_MAXDATA"/>.
//		/// </summary>
//		ERRHAL_SNAPSHOT_MAXDATA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SNAPSHOT_MAXDATA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CALCPHASE"/>.
//		/// </summary>
//		ERRHAL_CALCPHASE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CALCPHASE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_NO_VALID_SNAPSHOTDATA"/>.
//		/// </summary>
//		ERRHAL_NO_VALID_SNAPSHOTDATA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_NO_VALID_SNAPSHOTDATA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETCALDATHW"/>.
//		/// </summary>
//		ERRHAL_GETCALDATHW = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETCALDATHW,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETCALDAT"/>.
//		/// </summary>
//		ERRHAL_GETCALDAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETCALDAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETCALDAT"/>.
//		/// </summary>
//		ERRHAL_SETCALDAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETCALDAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETCSURCAL"/>.
//		/// </summary>
//		ERRHAL_SETCSURCAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETCSURCAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETCSURCAL"/>.
//		/// </summary>
//		ERRHAL_GETCSURCAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETCSURCAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_LDRCHKSUM"/>.
//		/// </summary>
//		ERRHAL_LDRCHKSUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_LDRCHKSUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_STORESENSSUPPLYOFFS"/>.
//		/// </summary>
//		ERRHAL_STORESENSSUPPLYOFFS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_STORESENSSUPPLYOFFS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETSENSSUPPLYOFFS"/>.
//		/// </summary>
//		ERRHAL_GETSENSSUPPLYOFFS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETSENSSUPPLYOFFS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETCSURCAL"/>.
//		/// </summary>
//		WARN_HAL_GETCSURCAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETCSURCAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETMEASVALINVAL"/>.
//		/// </summary>
//		ERRHAL_SETMEASVALINVAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETMEASVALINVAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETSENSSUPPLYOFFS"/>.
//		/// </summary>
//		WARN_HAL_GETSENSSUPPLYOFFS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETSENSSUPPLYOFFS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SELFPGACHN"/>.
//		/// </summary>
//		ERRHAL_SELFPGACHN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SELFPGACHN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_REDDATRATE"/>.
//		/// </summary>
//		ERRHAL_REDDATRATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_REDDATRATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_RATE_NOT_SUP_BY_FPGA"/>.
//		/// </summary>
//		ERRHAL_RATE_NOT_SUP_BY_FPGA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_RATE_NOT_SUP_BY_FPGA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FB_NOT_AVAILABLE"/>.
//		/// </summary>
//		ERRHAL_FB_NOT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FB_NOT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FB_UNKNOWN_PROPERTY"/>.
//		/// </summary>
//		ERRHAL_FB_UNKNOWN_PROPERTY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FB_UNKNOWN_PROPERTY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGATEST_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERRHAL_FPGATEST_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGATEST_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETPOTIWIPERFROMMSP"/>.
//		/// </summary>
//		WARN_HAL_GETPOTIWIPERFROMMSP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETPOTIWIPERFROMMSP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETPOTIWIPERFROMMSP"/>.
//		/// </summary>
//		ERRHAL_GETPOTIWIPERFROMMSP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETPOTIWIPERFROMMSP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_POTIWIPERCOMPARE"/>.
//		/// </summary>
//		ERRHAL_POTIWIPERCOMPARE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_POTIWIPERCOMPARE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETIEPEHP"/>.
//		/// </summary>
//		ERRHAL_SETIEPEHP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETIEPEHP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FB_ARBSIGGEN_BUFFER_BUSY"/>.
//		/// </summary>
//		ERRHAL_FB_ARBSIGGEN_BUFFER_BUSY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FB_ARBSIGGEN_BUFFER_BUSY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETHWIDENTPF"/>.
//		/// </summary>
//		ERRHAL_GETHWIDENTPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETHWIDENTPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETHWIDENTPF"/>.
//		/// </summary>
//		WARN_HAL_GETHWIDENTPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETHWIDENTPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETHWIDENTPF"/>.
//		/// </summary>
//		ERRHAL_SETHWIDENTPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETHWIDENTPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETADDHWREVPF"/>.
//		/// </summary>
//		ERRHAL_SETADDHWREVPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETADDHWREVPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETADDHWIDENTPF"/>.
//		/// </summary>
//		ERRHAL_SETADDHWIDENTPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETADDHWIDENTPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETHWRPF"/>.
//		/// </summary>
//		ERRHAL_GETHWRPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETHWRPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETADDHWRPF"/>.
//		/// </summary>
//		ERRHAL_GETADDHWRPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETADDHWRPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETADDHWIDENTPF"/>.
//		/// </summary>
//		ERRHAL_GETADDHWIDENTPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETADDHWIDENTPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETHWRPF"/>.
//		/// </summary>
//		WARN_HAL_GETHWRPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETHWRPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETADDHWRPF"/>.
//		/// </summary>
//		WARN_HAL_GETADDHWRPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETADDHWRPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETHWRPF"/>.
//		/// </summary>
//		ERRHAL_SETHWRPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETHWRPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETADDHWIDENTPF"/>.
//		/// </summary>
//		WARN_HAL_GETADDHWIDENTPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETADDHWIDENTPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETPLLVOLTAGE"/>.
//		/// </summary>
//		ERRHAL_GETPLLVOLTAGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETPLLVOLTAGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETPLLVOLTAGE"/>.
//		/// </summary>
//		WARN_HAL_GETPLLVOLTAGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETPLLVOLTAGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETPLLVOLTAGE"/>.
//		/// </summary>
//		ERRHAL_SETPLLVOLTAGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETPLLVOLTAGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETMACADDR"/>.
//		/// </summary>
//		ERRHAL_SETMACADDR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETMACADDR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMACADDR"/>.
//		/// </summary>
//		ERRHAL_GETMACADDR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMACADDR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETLEDPF"/>.
//		/// </summary>
//		ERRHAL_SETLEDPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETLEDPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETPFTEMP"/>.
//		/// </summary>
//		ERRHAL_GETPFTEMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETPFTEMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMODVOL"/>.
//		/// </summary>
//		ERRHAL_GETMODVOL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMODVOL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMODCUR"/>.
//		/// </summary>
//		ERRHAL_GETMODCUR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMODCUR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETEXTCUR"/>.
//		/// </summary>
//		ERRHAL_GETEXTCUR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETEXTCUR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPAPPLDATEPF"/>.
//		/// </summary>
//		ERRHAL_GETMSPAPPLDATEPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPAPPLDATEPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPLOADERDATEPF"/>.
//		/// </summary>
//		ERRHAL_GETMSPLOADERDATEPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPLOADERDATEPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPVERPF"/>.
//		/// </summary>
//		ERRHAL_GETMSPVERPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPVERPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPSTATPF"/>.
//		/// </summary>
//		ERRHAL_GETMSPSTATPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMSPSTATPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETLIMCURR"/>.
//		/// </summary>
//		ERRHAL_GETLIMCURR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETLIMCURR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETMSPVERPF"/>.
//		/// </summary>
//		WARN_HAL_GETMSPVERPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_GETMSPVERPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTSTSIGGEN"/>.
//		/// </summary>
//		ERRHAL_SETTSTSIGGEN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETTSTSIGGEN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_STRTDATSTRM"/>.
//		/// </summary>
//		ERRHAL_STRTDATSTRM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_STRTDATSTRM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_STOPDATSTRM"/>.
//		/// </summary>
//		ERRHAL_STOPDATSTRM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_STOPDATSTRM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_IISDUMDAT"/>.
//		/// </summary>
//		ERRHAL_IISDUMDAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_IISDUMDAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ENIIS"/>.
//		/// </summary>
//		ERRHAL_ENIIS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ENIIS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_RSTIIS"/>.
//		/// </summary>
//		ERRHAL_RSTIIS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_RSTIIS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CHKFLHETRY"/>.
//		/// </summary>
//		ERRHAL_CHKFLHETRY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CHKFLHETRY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMAXSRPF"/>.
//		/// </summary>
//		ERRHAL_GETMAXSRPF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_GETMAXSRPF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_OPENSNAPSHOTDRV"/>.
//		/// </summary>
//		ERRHAL_OPENSNAPSHOTDRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_OPENSNAPSHOTDRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CLOSESNAPSHOTDRV"/>.
//		/// </summary>
//		ERRHAL_CLOSESNAPSHOTDRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CLOSESNAPSHOTDRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ENCXUPPER"/>.
//		/// </summary>
//		ERRHAL_ENCXUPPER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ENCXUPPER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SIGNUMBER"/>.
//		/// </summary>
//		ERRHAL_SIGNUMBER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SIGNUMBER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FOSDELAY"/>.
//		/// </summary>
//		ERRHAL_FOSDELAY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FOSDELAY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSCALPARS"/>.
//		/// </summary>
//		ERRHAL_SETSCALPARS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETSCALPARS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_COMPENSATION_OFFSET"/>.
//		/// </summary>
//		ERRHAL_COMPENSATION_OFFSET = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_COMPENSATION_OFFSET,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_DACINPUTTYPE"/>.
//		/// </summary>
//		ERRHAL_DACINPUTTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_DACINPUTTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_UNKNOWNSYNCCODE"/>.
//		/// </summary>
//		ERRHAL_UNKNOWNSYNCCODE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_UNKNOWNSYNCCODE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ADDFUNCMATH"/>.
//		/// </summary>
//		ERRHAL_ADDFUNCMATH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_ADDFUNCMATH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_UNKNOWN_MATH_TYPE"/>.
//		/// </summary>
//		ERRHAL_UNKNOWN_MATH_TYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_UNKNOWN_MATH_TYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_PEAK_OUTRATE"/>.
//		/// </summary>
//		ERRHAL_PEAK_OUTRATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_PEAK_OUTRATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CX_SYNC_TEST"/>.
//		/// </summary>
//		ERRHAL_CX_SYNC_TEST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CX_SYNC_TEST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETZERO_NO_VALID_MEASVAL"/>.
//		/// </summary>
//		ERRHAL_SETZERO_NO_VALID_MEASVAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETZERO_NO_VALID_MEASVAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SET_FILTER_FIR"/>.
//		/// </summary>
//		ERRHAL_SET_FILTER_FIR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SET_FILTER_FIR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_PARAM_IS_ZERO"/>.
//		/// </summary>
//		ERRHAL_PARAM_IS_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_PARAM_IS_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FIR_TAB_CNT"/>.
//		/// </summary>
//		ERRHAL_FIR_TAB_CNT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FIR_TAB_CNT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FIR_TAB_CNT_SUM"/>.
//		/// </summary>
//		ERRHAL_FIR_TAB_CNT_SUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FIR_TAB_CNT_SUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETDEMODFACTORS"/>.
//		/// </summary>
//		ERRHAL_SETDEMODFACTORS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETDEMODFACTORS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETFIRDEMODFILTER"/>.
//		/// </summary>
//		ERRHAL_SETFIRDEMODFILTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETFIRDEMODFILTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETPOLYCOEFFSTOFPGA"/>.
//		/// </summary>
//		ERRHAL_SETPOLYCOEFFSTOFPGA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SETPOLYCOEFFSTOFPGA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAOPENDRV"/>.
//		/// </summary>
//		ERRHAL_FPGAOPENDRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAOPENDRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGACLOSEDRV"/>.
//		/// </summary>
//		ERRHAL_FPGACLOSEDRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGACLOSEDRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGABUSTST"/>.
//		/// </summary>
//		ERRHAL_FPGABUSTST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGABUSTST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAREAD"/>.
//		/// </summary>
//		ERRHAL_FPGAREAD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAREAD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAWRITE"/>.
//		/// </summary>
//		ERRHAL_FPGAWRITE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAWRITE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAINITI2C"/>.
//		/// </summary>
//		ERRHAL_FPGAINITI2C = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAINITI2C,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGARSTI2C"/>.
//		/// </summary>
//		ERRHAL_FPGARSTI2C = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGARSTI2C,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGARSTHZR"/>.
//		/// </summary>
//		ERRHAL_FPGARSTHZR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGARSTHZR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAMMAP"/>.
//		/// </summary>
//		ERRHAL_FPGAMMAP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAMMAP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAMUNMAP"/>.
//		/// </summary>
//		ERRHAL_FPGAMUNMAP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAMUNMAP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAGETTIME"/>.
//		/// </summary>
//		ERRHAL_FPGAGETTIME = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAGETTIME,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGARESET"/>.
//		/// </summary>
//		ERRHAL_FPGARESET = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGARESET,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAIOCTL"/>.
//		/// </summary>
//		ERRHAL_FPGAIOCTL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_FPGAIOCTL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETTMP"/>.
//		/// </summary>
//		ERRHAL_MSPGETTMP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETTMP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPDISOVRCURRPROT"/>.
//		/// </summary>
//		ERRHAL_MSPDISOVRCURRPROT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPDISOVRCURRPROT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETALLSWITCHES"/>.
//		/// </summary>
//		ERRHAL_MSPGETALLSWITCHES = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETALLSWITCHES,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETALLSWITCHES"/>.
//		/// </summary>
//		ERRHAL_MSPSETALLSWITCHES = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETALLSWITCHES,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPMEAS"/>.
//		/// </summary>
//		ERRHAL_MSPMEAS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPMEAS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRDCTRREG"/>.
//		/// </summary>
//		ERRHAL_MSPRDCTRREG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRDCTRREG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWRCTRREG"/>.
//		/// </summary>
//		ERRHAL_MSPWRCTRREG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWRCTRREG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWRREGBIT"/>.
//		/// </summary>
//		ERRHAL_MSPWRREGBIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWRREGBIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETSHUNT"/>.
//		/// </summary>
//		ERRHAL_MSPSETSHUNT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETSHUNT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETVARIANT"/>.
//		/// </summary>
//		ERRHAL_MSPSETVARIANT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETVARIANT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETWIRING"/>.
//		/// </summary>
//		ERRHAL_MSPSETWIRING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETWIRING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWRINFO"/>.
//		/// </summary>
//		ERRHAL_MSPWRINFO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWRINFO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRDINFO"/>.
//		/// </summary>
//		ERRHAL_MSPRDINFO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRDINFO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETPOTI"/>.
//		/// </summary>
//		ERRHAL_MSPSETPOTI = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETPOTI,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETPOTI"/>.
//		/// </summary>
//		ERRHAL_MSPGETPOTI = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETPOTI,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETSLNUM"/>.
//		/// </summary>
//		ERRHAL_MSPGETSLNUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETSLNUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETVERSION"/>.
//		/// </summary>
//		ERRHAL_MSPGETVERSION = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETVERSION,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETSTAT"/>.
//		/// </summary>
//		ERRHAL_MSPGETSTAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETSTAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETLIMITEDSENSORSUPPLY"/>.
//		/// </summary>
//		ERRHAL_MSPGETLIMITEDSENSORSUPPLY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETLIMITEDSENSORSUPPLY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETERR"/>.
//		/// </summary>
//		ERRHAL_MSPGETERR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETERR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPDOOWRST"/>.
//		/// </summary>
//		ERRHAL_MSPDOOWRST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPDOOWRST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETOWMODE"/>.
//		/// </summary>
//		ERRHAL_MSPSETOWMODE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETOWMODE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_TEDSCOM_NOT_POSSIBLE"/>.
//		/// </summary>
//		ERRHAL_TEDSCOM_NOT_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_TEDSCOM_NOT_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETFOWNUM"/>.
//		/// </summary>
//		ERRHAL_MSPGETFOWNUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETFOWNUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETOWDAT"/>.
//		/// </summary>
//		ERRHAL_MSPGETOWDAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPGETOWDAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETOWDAT"/>.
//		/// </summary>
//		ERRHAL_MSPSETOWDAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETOWDAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPBEEP"/>.
//		/// </summary>
//		ERRHAL_MSPBEEP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPBEEP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPPRGBLKV"/>.
//		/// </summary>
//		ERRHAL_MSPPRGBLKV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPPRGBLKV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPPRGBLK"/>.
//		/// </summary>
//		ERRHAL_MSPPRGBLK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPPRGBLK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRDBLK"/>.
//		/// </summary>
//		ERRHAL_MSPRDBLK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRDBLK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPPRERDBLK"/>.
//		/// </summary>
//		ERRHAL_MSPPRERDBLK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPPRERDBLK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRDRFID"/>.
//		/// </summary>
//		ERRHAL_MSPRDRFID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRDRFID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWRRFID"/>.
//		/// </summary>
//		ERRHAL_MSPWRRFID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWRRFID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETLED"/>.
//		/// </summary>
//		ERRHAL_MSPSETLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPSETLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRD_NO_RFID"/>.
//		/// </summary>
//		ERRHAL_MSPRD_NO_RFID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRD_NO_RFID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRD_ERR_RFID"/>.
//		/// </summary>
//		ERRHAL_MSPRD_ERR_RFID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRD_ERR_RFID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSP_EEPROM_SIZE"/>.
//		/// </summary>
//		ERRHAL_MSP_EEPROM_SIZE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSP_EEPROM_SIZE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWR_EEPROM"/>.
//		/// </summary>
//		ERRHAL_MSPWR_EEPROM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPWR_EEPROM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRD_EEPROM"/>.
//		/// </summary>
//		ERRHAL_MSPRD_EEPROM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSPRD_EEPROM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSP_SET_ADC_LPM"/>.
//		/// </summary>
//		ERRHAL_MSP_SET_ADC_LPM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSP_SET_ADC_LPM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSP_SET_VARIANT"/>.
//		/// </summary>
//		ERRHAL_MSP_SET_VARIANT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MSP_SET_VARIANT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SPISETDACPLL"/>.
//		/// </summary>
//		ERRHAL_SPISETDACPLL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SPISETDACPLL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SPIOPENDRV"/>.
//		/// </summary>
//		ERRHAL_SPIOPENDRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SPIOPENDRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SPISETSS"/>.
//		/// </summary>
//		ERRHAL_SPISETSS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SPISETSS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MAX11245_REG_ACCESS"/>.
//		/// </summary>
//		ERRHAL_MAX11245_REG_ACCESS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MAX11245_REG_ACCESS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MAX11245_CMD_REG_BUSY"/>.
//		/// </summary>
//		ERRHAL_MAX11245_CMD_REG_BUSY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MAX11245_CMD_REG_BUSY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MAX11245_UNABLE_TO_POWER_DOWN"/>.
//		/// </summary>
//		ERRHAL_MAX11245_UNABLE_TO_POWER_DOWN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_MAX11245_UNABLE_TO_POWER_DOWN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUST_VALUE_OUT_OF_RANGE"/>.
//		/// </summary>
//		ERR_ADJUST_VALUE_OUT_OF_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUST_VALUE_OUT_OF_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUSTMENT_TIMEOUT"/>.
//		/// </summary>
//		ERR_ADJUSTMENT_TIMEOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUSTMENT_TIMEOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUSTMENT_CANCELLED"/>.
//		/// </summary>
//		ERR_ADJUSTMENT_CANCELLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUSTMENT_CANCELLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUST_ZERO_VALUE_OUT_OF_RANGE"/>.
//		/// </summary>
//		ERR_ADJUST_ZERO_VALUE_OUT_OF_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUST_ZERO_VALUE_OUT_OF_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUST_CAL_VALUE_OUT_OF_RANGE"/>.
//		/// </summary>
//		ERR_ADJUST_CAL_VALUE_OUT_OF_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJUST_CAL_VALUE_OUT_OF_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_CALCAL"/>.
//		/// </summary>
//		ERR_HAL_CALCAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_CALCAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_CALMEAS"/>.
//		/// </summary>
//		ERR_HAL_CALMEAS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_CALMEAS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_CORRECTPHASE"/>.
//		/// </summary>
//		ERR_HAL_CORRECTPHASE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_CORRECTPHASE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_CALC_PT_COEFFS"/>.
//		/// </summary>
//		ERR_HAL_CALC_PT_COEFFS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_CALC_PT_COEFFS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_ACTIVATESET"/>.
//		/// </summary>
//		ERR_HAL_ACTIVATESET = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HAL_ACTIVATESET,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_ACTIVATESET"/>.
//		/// </summary>
//		WARN_HAL_ACTIVATESET = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_HAL_ACTIVATESET,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SET_ZERO_INCL_ANALOGOUT"/>.
//		/// </summary>
//		WARN_SET_ZERO_INCL_ANALOGOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SET_ZERO_INCL_ANALOGOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SET_ZERO"/>.
//		/// </summary>
//		WARN_SET_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SET_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SIG_NOT_AVAILABLE"/>.
//		/// </summary>
//		WARN_SIG_NOT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SIG_NOT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUST_RMS_PERIOD"/>.
//		/// </summary>
//		WARN_ADJUST_RMS_PERIOD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ADJUST_RMS_PERIOD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_LOOP"/>.
//		/// </summary>
//		ERRHAL_LOOP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_LOOP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CORRTEMPERATURE"/>.
//		/// </summary>
//		ERRHAL_CORRTEMPERATURE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_CORRTEMPERATURE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_COLDJUNCTION"/>.
//		/// </summary>
//		ERRHAL_COLDJUNCTION = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_COLDJUNCTION,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_COLDJUNCTION_INITJUST_MISSING"/>.
//		/// </summary>
//		WARN_COLDJUNCTION_INITJUST_MISSING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_COLDJUNCTION_INITJUST_MISSING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_AUTOADJUST"/>.
//		/// </summary>
//		ERRHAL_AUTOADJUST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_AUTOADJUST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_EXTERNAL_ADAPTOR"/>.
//		/// </summary>
//		ERRHAL_EXTERNAL_ADAPTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_EXTERNAL_ADAPTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_WRONG_CHANTYPE"/>.
//		/// </summary>
//		ERRHAL_WRONG_CHANTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_WRONG_CHANTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERRHAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONNECTOR_NOT_ENABLED"/>.
//		/// </summary>
//		ERR_CONNECTOR_NOT_ENABLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONNECTOR_NOT_ENABLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_6_WIRING_POSSIBLE"/>.
//		/// </summary>
//		WARN_6_WIRING_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_6_WIRING_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WIRING_2_BROKEN"/>.
//		/// </summary>
//		ERR_WIRING_2_BROKEN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WIRING_2_BROKEN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WIRING_3_BROKEN"/>.
//		/// </summary>
//		ERR_WIRING_3_BROKEN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WIRING_3_BROKEN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLENABLE"/>.
//		/// </summary>
//		ERRHAL_INTERPOLENABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLENABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLINIT"/>.
//		/// </summary>
//		ERRHAL_INTERPOLINIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLINIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLSETDATARATE"/>.
//		/// </summary>
//		ERRHAL_INTERPOLSETDATARATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLSETDATARATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLSETSKALINGOVERFLOW"/>.
//		/// </summary>
//		ERRHAL_INTERPOLSETSKALINGOVERFLOW = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLSETSKALINGOVERFLOW,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLSETSCALING"/>.
//		/// </summary>
//		ERRHAL_INTERPOLSETSCALING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_INTERPOLSETSCALING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SCALDACNULL"/>.
//		/// </summary>
//		ERRHAL_SCALDACNULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SCALDACNULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SCALDACGAIN"/>.
//		/// </summary>
//		ERRHAL_SCALDACGAIN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SCALDACGAIN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SCALDACNULL_MX410"/>.
//		/// </summary>
//		ERRHAL_SCALDACNULL_MX410 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SCALDACNULL_MX410,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARNHAL_SCALDACNULL_MX410"/>.
//		/// </summary>
//		WARNHAL_SCALDACNULL_MX410 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARNHAL_SCALDACNULL_MX410,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SCALDACGAIN_MX410"/>.
//		/// </summary>
//		ERRHAL_SCALDACGAIN_MX410 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERRHAL_SCALDACGAIN_MX410,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARNHAL_SCALDACGAIN_MX410"/>.
//		/// </summary>
//		WARNHAL_SCALDACGAIN_MX410 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARNHAL_SCALDACGAIN_MX410,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CONFIGURE_FIREWIRE_FUNCTIONBLOCK"/>.
//		/// </summary>
//		WARN_CONFIGURE_FIREWIRE_FUNCTIONBLOCK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CONFIGURE_FIREWIRE_FUNCTIONBLOCK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CREATE_FIREWIRE_FUNCTIONBLOCK"/>.
//		/// </summary>
//		ERR_CREATE_FIREWIRE_FUNCTIONBLOCK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CREATE_FIREWIRE_FUNCTIONBLOCK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RT_IOCTL"/>.
//		/// </summary>
//		ERR_RT_IOCTL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RT_IOCTL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RT_COPY_TO_USER"/>.
//		/// </summary>
//		ERR_RT_COPY_TO_USER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RT_COPY_TO_USER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RT_COPY_FROM_USER"/>.
//		/// </summary>
//		ERR_RT_COPY_FROM_USER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_RT_COPY_FROM_USER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_PROPERTY_NOT_FOUND"/>.
//		/// </summary>
//		ERR_TEDS_PROPERTY_NOT_FOUND = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_PROPERTY_NOT_FOUND,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_DATA_CORRUPT"/>.
//		/// </summary>
//		ERR_TEDS_DATA_CORRUPT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_DATA_CORRUPT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_DISCSIGCONFIG"/>.
//		/// </summary>
//		ERR_TEDS_NO_DISCSIGCONFIG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_DISCSIGCONFIG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_ELECSIGTYPE"/>.
//		/// </summary>
//		ERR_TEDS_NO_ELECSIGTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_ELECSIGTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MAPMETH"/>.
//		/// </summary>
//		ERR_TEDS_NO_MAPMETH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_NO_MAPMETH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALCURVE_DOMAIN_INVALID"/>.
//		/// </summary>
//		ERR_TEDS_CALCURVE_DOMAIN_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALCURVE_DOMAIN_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALTABLE_DOMAIN_INVALID"/>.
//		/// </summary>
//		ERR_TEDS_CALTABLE_DOMAIN_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_CALTABLE_DOMAIN_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SELECTION_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SELECTION_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SELECTION_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_TYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_MATH_TYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_TYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ONLY_SINGLE_CONNECTOR_POSSIBLE"/>.
//		/// </summary>
//		ERR_ONLY_SINGLE_CONNECTOR_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ONLY_SINGLE_CONNECTOR_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_MODESTARTBIT_511"/>.
//		/// </summary>
//		ERR_CAN_DECODER_MODESTARTBIT_511 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_MODESTARTBIT_511,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_STARTBIT_511"/>.
//		/// </summary>
//		ERR_CAN_DECODER_STARTBIT_511 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_STARTBIT_511,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_MODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_CAN_MODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_MODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_FDFRAME_ONLY_CLASSICAL_SUPPORTED"/>.
//		/// </summary>
//		ERR_CAN_FDFRAME_ONLY_CLASSICAL_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_FDFRAME_ONLY_CLASSICAL_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_FDFRAME_ONLY_CLASSICAL_SUPPORTED"/>.
//		/// </summary>
//		WARN_CAN_FDFRAME_ONLY_CLASSICAL_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_FDFRAME_ONLY_CLASSICAL_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_FDFRAME_ONLY_CLASSICAL_AVAILABLE"/>.
//		/// </summary>
//		ERR_CAN_FDFRAME_ONLY_CLASSICAL_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_FDFRAME_ONLY_CLASSICAL_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_FDFRAME_ONLY_CLASSICAL_AVAILABLE"/>.
//		/// </summary>
//		WARN_CAN_FDFRAME_ONLY_CLASSICAL_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_FDFRAME_ONLY_CLASSICAL_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_ONLY_ENDLESS_RETRANSMISSION_SUPPORTED"/>.
//		/// </summary>
//		ERR_CAN_ONLY_ENDLESS_RETRANSMISSION_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_ONLY_ENDLESS_RETRANSMISSION_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_RETRANSMISSION_OUT_OF_RANGE"/>.
//		/// </summary>
//		ERR_CAN_RETRANSMISSION_OUT_OF_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_RETRANSMISSION_OUT_OF_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_BYTE_NOT_VALID_HEX_VALUE"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_BYTE_NOT_VALID_HEX_VALUE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_BYTE_NOT_VALID_HEX_VALUE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ETHERCAT_PARAMETERS"/>.
//		/// </summary>
//		ERR_ETHERCAT_PARAMETERS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ETHERCAT_PARAMETERS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PROFINET_PARAMETERS"/>.
//		/// </summary>
//		ERR_PROFINET_PARAMETERS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PROFINET_PARAMETERS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PROFINET_PARAMETERS"/>.
//		/// </summary>
//		WARN_PROFINET_PARAMETERS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PROFINET_PARAMETERS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PROFINET_PARAM_TIMEOUT"/>.
//		/// </summary>
//		ERR_PROFINET_PARAM_TIMEOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PROFINET_PARAM_TIMEOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADDCONN_MODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_ADDCONN_MODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADDCONN_MODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_GUARD_TIMEOUT"/>.
//		/// </summary>
//		WARN_ANALOG_OUT_GUARD_TIMEOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_GUARD_TIMEOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_ONTIMEOUT_VOLTAGE_MAX_POSSIBLE"/>.
//		/// </summary>
//		WARN_ANALOG_OUT_ONTIMEOUT_VOLTAGE_MAX_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_ONTIMEOUT_VOLTAGE_MAX_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_ONTIMEOUT_VOLTAGE_MIN_POSSIBLE"/>.
//		/// </summary>
//		WARN_ANALOG_OUT_ONTIMEOUT_VOLTAGE_MIN_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_ONTIMEOUT_VOLTAGE_MIN_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_GUARDTIME_MIN_POSSIBLE"/>.
//		/// </summary>
//		WARN_ANALOG_OUT_GUARDTIME_MIN_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_GUARDTIME_MIN_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_GUARDTIME_MAX_POSSIBLE"/>.
//		/// </summary>
//		WARN_ANALOG_OUT_GUARDTIME_MAX_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_GUARDTIME_MAX_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOG_OUT_MODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_ANALOG_OUT_MODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOG_OUT_MODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT"/>.
//		/// </summary>
//		WARN_ANALOG_OUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_SCALING_NOT_SUPPORTED_DISABLED"/>.
//		/// </summary>
//		WARN_ANALOG_OUT_SCALING_NOT_SUPPORTED_DISABLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ANALOG_OUT_SCALING_NOT_SUPPORTED_DISABLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_NOT_AVAILABLE_IF_ANALOGIN_POLYNOMIAL_SCALING"/>.
//		/// </summary>
//		ERR_ANALOGOUT_NOT_AVAILABLE_IF_ANALOGIN_POLYNOMIAL_SCALING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_NOT_AVAILABLE_IF_ANALOGIN_POLYNOMIAL_SCALING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIGITALIO_MODE_NOT_OUTPUT"/>.
//		/// </summary>
//		ERR_DIGITALIO_MODE_NOT_OUTPUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIGITALIO_MODE_NOT_OUTPUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIGITALIO_MODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_DIGITALIO_MODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIGITALIO_MODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIGITALIO_TRIGGER_LEVEL_TOO_LOW"/>.
//		/// </summary>
//		ERR_DIGITALIO_TRIGGER_LEVEL_TOO_LOW = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIGITALIO_TRIGGER_LEVEL_TOO_LOW,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIGITALIO_TRIGGER_LEVEL_TOO_HIGH"/>.
//		/// </summary>
//		ERR_DIGITALIO_TRIGGER_LEVEL_TOO_HIGH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIGITALIO_TRIGGER_LEVEL_TOO_HIGH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SET_DIGITAL_IO_IS_INPUT"/>.
//		/// </summary>
//		WARN_SET_DIGITAL_IO_IS_INPUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SET_DIGITAL_IO_IS_INPUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SET_DIGITAL_IO_IS_CONNECTED"/>.
//		/// </summary>
//		WARN_SET_DIGITAL_IO_IS_CONNECTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SET_DIGITAL_IO_IS_CONNECTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MASK_RESULT_LONGER_THAN_64_BITS"/>.
//		/// </summary>
//		ERR_MASK_RESULT_LONGER_THAN_64_BITS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MASK_RESULT_LONGER_THAN_64_BITS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MASK_SYNTAX"/>.
//		/// </summary>
//		ERR_MASK_SYNTAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MASK_SYNTAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DELTA_MUST_BE_GREATER_THAN_ZERO"/>.
//		/// </summary>
//		ERR_DELTA_MUST_BE_GREATER_THAN_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DELTA_MUST_BE_GREATER_THAN_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIVISOR_MUST_BE_GREATER_THAN_ZERO"/>.
//		/// </summary>
//		ERR_DIVISOR_MUST_BE_GREATER_THAN_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DIVISOR_MUST_BE_GREATER_THAN_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_DIVISOR_SET_TO_ZERO"/>.
//		/// </summary>
//		WARN_DIVISOR_SET_TO_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_DIVISOR_SET_TO_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_LISTEN_ONLY"/>.
//		/// </summary>
//		ERR_CAN_LISTEN_ONLY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_LISTEN_ONLY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_TRANSMIT_LISTEN_ONLY"/>.
//		/// </summary>
//		WARN_CAN_TRANSMIT_LISTEN_ONLY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_TRANSMIT_LISTEN_ONLY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TIMER_SET_TO_NEAREST_POSSIBLE"/>.
//		/// </summary>
//		WARN_TIMER_SET_TO_NEAREST_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TIMER_SET_TO_NEAREST_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TIMER_SET_TO_MINIMUM_POSSIBLE"/>.
//		/// </summary>
//		WARN_TIMER_SET_TO_MINIMUM_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TIMER_SET_TO_MINIMUM_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CCP_DBC_DATA_INVALID"/>.
//		/// </summary>
//		ERR_CAN_CCP_DBC_DATA_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CCP_DBC_DATA_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CCP_ERROR_CREATING_LOGGER"/>.
//		/// </summary>
//		ERR_CAN_CCP_ERROR_CREATING_LOGGER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CCP_ERROR_CREATING_LOGGER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CCP_NOT_INITIALIZED"/>.
//		/// </summary>
//		ERR_CAN_CCP_NOT_INITIALIZED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CCP_NOT_INITIALIZED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATHTYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_MATHTYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATHTYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CHECK_ADDFUNC_PARAMETERS"/>.
//		/// </summary>
//		WARN_CHECK_ADDFUNC_PARAMETERS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CHECK_ADDFUNC_PARAMETERS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_DISABLED_SIGNALREFERENCE_OFF"/>.
//		/// </summary>
//		WARN_MATH_DISABLED_SIGNALREFERENCE_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_DISABLED_SIGNALREFERENCE_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PEAK_DISABLED_SIGNALREFERENCE_OFF"/>.
//		/// </summary>
//		WARN_PEAK_DISABLED_SIGNALREFERENCE_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PEAK_DISABLED_SIGNALREFERENCE_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PEAK_DISABLED_ANALOGIN_SCALING_NOT_POSSIBLE"/>.
//		/// </summary>
//		WARN_PEAK_DISABLED_ANALOGIN_SCALING_NOT_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PEAK_DISABLED_ANALOGIN_SCALING_NOT_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PEAK_NOT_POSSIBLE_ANALOGIN_SCALING"/>.
//		/// </summary>
//		ERR_PEAK_NOT_POSSIBLE_ANALOGIN_SCALING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PEAK_NOT_POSSIBLE_ANALOGIN_SCALING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TIME_NEGATIVE_OR_ZERO"/>.
//		/// </summary>
//		ERR_TIME_NEGATIVE_OR_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TIME_NEGATIVE_OR_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TIME_NEGATIVE"/>.
//		/// </summary>
//		ERR_TIME_NEGATIVE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TIME_NEGATIVE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_RMS_PERIOD_CHANGED_TO_MIN"/>.
//		/// </summary>
//		WARN_MATH_RMS_PERIOD_CHANGED_TO_MIN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_RMS_PERIOD_CHANGED_TO_MIN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_ROTVIBE_OUTPUTFILTER_SWITCHED_OFF"/>.
//		/// </summary>
//		WARN_MATH_ROTVIBE_OUTPUTFILTER_SWITCHED_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_ROTVIBE_OUTPUTFILTER_SWITCHED_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_ROTVIBE_OUPUT_HIGHPASS_ALIGNED"/>.
//		/// </summary>
//		WARN_MATH_ROTVIBE_OUPUT_HIGHPASS_ALIGNED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_ROTVIBE_OUPUT_HIGHPASS_ALIGNED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_ROTVIBE_FILTER_SWITCHED_OFF"/>.
//		/// </summary>
//		WARN_MATH_ROTVIBE_FILTER_SWITCHED_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_ROTVIBE_FILTER_SWITCHED_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_ROTVIBE_HIGHPASS_ALIGNED"/>.
//		/// </summary>
//		WARN_MATH_ROTVIBE_HIGHPASS_ALIGNED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_ROTVIBE_HIGHPASS_ALIGNED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_SCALINGFACTOR_ZERO"/>.
//		/// </summary>
//		WARN_MATH_SCALINGFACTOR_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_SCALINGFACTOR_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_PULSESPERROUND_LESS_THAN_1"/>.
//		/// </summary>
//		ERR_MATH_PULSESPERROUND_LESS_THAN_1 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_PULSESPERROUND_LESS_THAN_1,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_INTEGRATOR_NOT_ENABLED"/>.
//		/// </summary>
//		ERR_MATH_INTEGRATOR_NOT_ENABLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_INTEGRATOR_NOT_ENABLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_UNIT_NOT_ENABLED"/>.
//		/// </summary>
//		ERR_MATH_UNIT_NOT_ENABLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_UNIT_NOT_ENABLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_UNIT_NOT_ENABLED"/>.
//		/// </summary>
//		WARN_MATH_UNIT_NOT_ENABLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MATH_UNIT_NOT_ENABLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_PID_MODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_MATH_PID_MODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_PID_MODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SEGMENT_COUNT_TOO_SMALL"/>.
//		/// </summary>
//		ERR_SEGMENT_COUNT_TOO_SMALL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SEGMENT_COUNT_TOO_SMALL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SEGMENT_COUNT_TOO_BIG"/>.
//		/// </summary>
//		ERR_SEGMENT_COUNT_TOO_BIG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SEGMENT_COUNT_TOO_BIG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_PARAM_REFUSED"/>.
//		/// </summary>
//		ERR_MATH_PARAM_REFUSED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MATH_PARAM_REFUSED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PEAK_PARAM_REFUSED"/>.
//		/// </summary>
//		ERR_PEAK_PARAM_REFUSED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PEAK_PARAM_REFUSED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GATE_PARAM_REFUSED"/>.
//		/// </summary>
//		ERR_GATE_PARAM_REFUSED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GATE_PARAM_REFUSED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_MODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_MODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_MODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_AMPLITUDETYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_AMPLITUDETYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_AMPLITUDETYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_AMPLITUDE_LEVEL_MIN_MAX"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_AMPLITUDE_LEVEL_MIN_MAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_AMPLITUDE_LEVEL_MIN_MAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_AMPLITUDE_PEAKPEAK_DELTA"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_AMPLITUDE_PEAKPEAK_DELTA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_AMPLITUDE_PEAKPEAK_DELTA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ZERO_DIVISION"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_ZERO_DIVISION = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ZERO_DIVISION,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_FREQUENCY"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_FREQUENCY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_FREQUENCY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_WRONG_INDEX"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_WRONG_INDEX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_WRONG_INDEX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TESTSIGNAL_DUTYCYCLE_SET_TO_MIN"/>.
//		/// </summary>
//		WARN_TESTSIGNAL_DUTYCYCLE_SET_TO_MIN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TESTSIGNAL_DUTYCYCLE_SET_TO_MIN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TESTSIGNAL_DUTYCYCLE_SET_TO_MAX"/>.
//		/// </summary>
//		WARN_TESTSIGNAL_DUTYCYCLE_SET_TO_MAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TESTSIGNAL_DUTYCYCLE_SET_TO_MAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TESTSIGNAL_PHASE_SET_TO_MIN"/>.
//		/// </summary>
//		WARN_TESTSIGNAL_PHASE_SET_TO_MIN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TESTSIGNAL_PHASE_SET_TO_MIN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TESTSIGNAL_PHASE_SET_TO_MAX"/>.
//		/// </summary>
//		WARN_TESTSIGNAL_PHASE_SET_TO_MAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_TESTSIGNAL_PHASE_SET_TO_MAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_VOLTAGE_OUT_OF_RANGE"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_VOLTAGE_OUT_OF_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_VOLTAGE_OUT_OF_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ARBITRATION_SOURCETYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_ARBITRATION_SOURCETYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ARBITRATION_SOURCETYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ARBITRATION_RUNMODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_ARBITRATION_RUNMODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ARBITRATION_RUNMODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ARBITRATION_REPETITIONRATE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_ARBITRATION_REPETITIONRATE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ARBITRATION_REPETITIONRATE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ARBITRATION_BURSTMODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_ARBITRATION_BURSTMODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_ARBITRATION_BURSTMODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_NOT_ACTIVE_OR_NOT_CONSTANT"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_NOT_ACTIVE_OR_NOT_CONSTANT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_NOT_ACTIVE_OR_NOT_CONSTANT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNAL_GENERATOR_MODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNAL_GENERATOR_MODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNAL_GENERATOR_MODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNAL_GENERATOR_NOT_ENABLED"/>.
//		/// </summary>
//		ERR_SIGNAL_GENERATOR_NOT_ENABLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNAL_GENERATOR_NOT_ENABLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_PARAM_REFUSED"/>.
//		/// </summary>
//		ERR_TESTSIGNAL_PARAM_REFUSED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TESTSIGNAL_PARAM_REFUSED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MODULE_NOT_READY_FOR_FIELDBUS_ACQ"/>.
//		/// </summary>
//		WARN_MODULE_NOT_READY_FOR_FIELDBUS_ACQ = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MODULE_NOT_READY_FOR_FIELDBUS_ACQ,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_FUNCTIONBLOCK_SET_PDO_STATUS"/>.
//		/// </summary>
//		ERR_ECAT_FUNCTIONBLOCK_SET_PDO_STATUS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_FUNCTIONBLOCK_SET_PDO_STATUS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_FUNCTIONBLOCK_SET_PDO_INDEX"/>.
//		/// </summary>
//		ERR_ECAT_FUNCTIONBLOCK_SET_PDO_INDEX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_FUNCTIONBLOCK_SET_PDO_INDEX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_FUNCTIONBLOCK_CREATE_PROBLEM"/>.
//		/// </summary>
//		ERR_ECAT_FUNCTIONBLOCK_CREATE_PROBLEM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_FUNCTIONBLOCK_CREATE_PROBLEM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_RTSIGNALE_NOT_VALID"/>.
//		/// </summary>
//		ERR_ECAT_RTSIGNALE_NOT_VALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_RTSIGNALE_NOT_VALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ECAT_DEMUX_SIGNALE_NOT_VALID"/>.
//		/// </summary>
//		WARN_ECAT_DEMUX_SIGNALE_NOT_VALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ECAT_DEMUX_SIGNALE_NOT_VALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ECAT_MODULE_PARAM_NOT_READY"/>.
//		/// </summary>
//		WARN_ECAT_MODULE_PARAM_NOT_READY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ECAT_MODULE_PARAM_NOT_READY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_MODULE_STATUS_NOT_READABLE"/>.
//		/// </summary>
//		ERR_ECAT_MODULE_STATUS_NOT_READABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_MODULE_STATUS_NOT_READABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ECAT_CORRECTED_FW_SIGNALE_LIST_SIZE"/>.
//		/// </summary>
//		WARN_ECAT_CORRECTED_FW_SIGNALE_LIST_SIZE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_ECAT_CORRECTED_FW_SIGNALE_LIST_SIZE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_COUNT_OF_FW_SIGNALS_LIMIT_REACHED"/>.
//		/// </summary>
//		ERR_ECAT_COUNT_OF_FW_SIGNALS_LIMIT_REACHED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ECAT_COUNT_OF_FW_SIGNALS_LIMIT_REACHED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NTP_SERVER_NOT_REACHED"/>.
//		/// </summary>
//		ERR_NTP_SERVER_NOT_REACHED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NTP_SERVER_NOT_REACHED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FW_NO_NEC_CHANNEL"/>.
//		/// </summary>
//		ERR_FW_NO_NEC_CHANNEL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FW_NO_NEC_CHANNEL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FW_NO_NEC_DOWNLOAD"/>.
//		/// </summary>
//		ERR_FW_NO_NEC_DOWNLOAD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FW_NO_NEC_DOWNLOAD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FW_NO_MSP_CHANNEL"/>.
//		/// </summary>
//		ERR_FW_NO_MSP_CHANNEL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FW_NO_MSP_CHANNEL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXCITATIONCALIBRATION_DATA_MISSING"/>.
//		/// </summary>
//		WARN_EXCITATIONCALIBRATION_DATA_MISSING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXCITATIONCALIBRATION_DATA_MISSING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CALCALIBRATION_DATA_MISSING"/>.
//		/// </summary>
//		WARN_CALCALIBRATION_DATA_MISSING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CALCALIBRATION_DATA_MISSING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MSP_DATA_CORRUPT"/>.
//		/// </summary>
//		ERR_MSP_DATA_CORRUPT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MSP_DATA_CORRUPT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INITJUST_FROM_MSP"/>.
//		/// </summary>
//		ERR_INITJUST_FROM_MSP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INITJUST_FROM_MSP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_VALID_INITJUST"/>.
//		/// </summary>
//		ERR_NO_VALID_INITJUST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_VALID_INITJUST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_STORING_INITJUST"/>.
//		/// </summary>
//		ERR_STORING_INITJUST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_STORING_INITJUST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GETTING_INITJUST"/>.
//		/// </summary>
//		ERR_GETTING_INITJUST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_GETTING_INITJUST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PLLVOL_OUTOFRANGE"/>.
//		/// </summary>
//		ERR_PLLVOL_OUTOFRANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PLLVOL_OUTOFRANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PLLVOL_CORRUPT"/>.
//		/// </summary>
//		ERR_PLLVOL_CORRUPT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PLLVOL_CORRUPT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PLLVOL_FROM_MSP"/>.
//		/// </summary>
//		ERR_PLLVOL_FROM_MSP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PLLVOL_FROM_MSP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DATA_RATE_DOMAIN_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_DATA_RATE_DOMAIN_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DATA_RATE_DOMAIN_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_FILENAME"/>.
//		/// </summary>
//		ERR_SYS_FILENAME = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_FILENAME,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_FILE_TOO_LARGE"/>.
//		/// </summary>
//		ERR_SYS_FILE_TOO_LARGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_FILE_TOO_LARGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_BAD_DELIVERY"/>.
//		/// </summary>
//		ERR_SYS_BAD_DELIVERY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_BAD_DELIVERY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_FRAMEWORK_START"/>.
//		/// </summary>
//		ERR_SYS_FRAMEWORK_START = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_FRAMEWORK_START,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_OPEN_CLOSE_WD"/>.
//		/// </summary>
//		ERR_SYS_OPEN_CLOSE_WD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_OPEN_CLOSE_WD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_INVALID_TIME"/>.
//		/// </summary>
//		ERR_SYS_INVALID_TIME = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_INVALID_TIME,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_TOO_MANY_EVENTS"/>.
//		/// </summary>
//		ERR_SYS_TOO_MANY_EVENTS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_TOO_MANY_EVENTS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_CREATING_OBJECT"/>.
//		/// </summary>
//		ERR_SYS_CREATING_OBJECT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYS_CREATING_OBJECT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_OPEN_DRIVER"/>.
//		/// </summary>
//		ERR_SPI_OPEN_DRIVER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_OPEN_DRIVER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_WAKEUP_DAC"/>.
//		/// </summary>
//		ERR_SPI_WAKEUP_DAC = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_WAKEUP_DAC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_WRITE_DRIVER_DIGITS"/>.
//		/// </summary>
//		ERR_SPI_WRITE_DRIVER_DIGITS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_WRITE_DRIVER_DIGITS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_WRITE_DRIVER_COMMAND"/>.
//		/// </summary>
//		ERR_SPI_WRITE_DRIVER_COMMAND = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_WRITE_DRIVER_COMMAND,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_VOLT_OUTOF_RANGE"/>.
//		/// </summary>
//		ERR_SPI_VOLT_OUTOF_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_VOLT_OUTOF_RANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_PLLDAC"/>.
//		/// </summary>
//		ERR_SPI_PLLDAC = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SPI_PLLDAC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_OPEN_DRIVER"/>.
//		/// </summary>
//		ERR_SYNC_OPEN_DRIVER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_OPEN_DRIVER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ANY_TSD_PROBLEM"/>.
//		/// </summary>
//		ERR_SYNC_ANY_TSD_PROBLEM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ANY_TSD_PROBLEM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_NOT_ALL_MODULES_READY"/>.
//		/// </summary>
//		ERR_SYNC_NOT_ALL_MODULES_READY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_NOT_ALL_MODULES_READY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_TIMESOURCE_DATA_NOT_VALID"/>.
//		/// </summary>
//		ERR_SYNC_TIMESOURCE_DATA_NOT_VALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_TIMESOURCE_DATA_NOT_VALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_ETHERCAT"/>.
//		/// </summary>
//		ERR_SYNC_ACTIVATE_ETHERCAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_ETHERCAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_IRIG"/>.
//		/// </summary>
//		ERR_SYNC_ACTIVATE_IRIG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_IRIG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_PTP"/>.
//		/// </summary>
//		ERR_SYNC_ACTIVATE_PTP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_PTP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_NTP"/>.
//		/// </summary>
//		ERR_SYNC_ACTIVATE_NTP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_NTP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_AUTO"/>.
//		/// </summary>
//		ERR_SYNC_ACTIVATE_AUTO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_ACTIVATE_AUTO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_IP_SUBNETMASK_SYNTAX"/>.
//		/// </summary>
//		ERR_PARAM_IP_SUBNETMASK_SYNTAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_IP_SUBNETMASK_SYNTAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_IP_ADDRESS_SYNTAX"/>.
//		/// </summary>
//		ERR_PARAM_IP_ADDRESS_SYNTAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_IP_ADDRESS_SYNTAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_SYNC_TIMESOURCE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_PARAM_SYNC_TIMESOURCE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_SYNC_TIMESOURCE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_CYCLEDIFF_TOO_LARGE"/>.
//		/// </summary>
//		ERR_SYNC_CYCLEDIFF_TOO_LARGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_CYCLEDIFF_TOO_LARGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_NOT_IN_SYNC"/>.
//		/// </summary>
//		ERR_SYNC_NOT_IN_SYNC = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SYNC_NOT_IN_SYNC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJ_MEASRANGE_INVALID"/>.
//		/// </summary>
//		ERR_ADJ_MEASRANGE_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJ_MEASRANGE_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJ_MEASRANGE_OVERRUN"/>.
//		/// </summary>
//		ERR_ADJ_MEASRANGE_OVERRUN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJ_MEASRANGE_OVERRUN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJ_WRONG_CONNECTOR"/>.
//		/// </summary>
//		ERR_ADJ_WRONG_CONNECTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ADJ_WRONG_CONNECTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONN_WRONG_CONNECTOR"/>.
//		/// </summary>
//		ERR_CONN_WRONG_CONNECTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONN_WRONG_CONNECTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_NOCONN"/>.
//		/// </summary>
//		ERR_ANALOGOUT_NOCONN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_NOCONN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_SIGOVFL"/>.
//		/// </summary>
//		ERR_ANALOGOUT_SIGOVFL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_SIGOVFL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_OCCF"/>.
//		/// </summary>
//		ERR_ANALOGOUT_OCCF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_OCCF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_OVFL"/>.
//		/// </summary>
//		ERR_ANALOGOUT_OVFL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ANALOGOUT_OVFL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_DECODERINDEX_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_DECODERINDEX_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_DECODERINDEX_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_DECODERINDEX_LOCAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_DECODERINDEX_LOCAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_DECODERINDEX_LOCAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALGENERATORINDEX_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_SIGNALGENERATORINDEX_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALGENERATORINDEX_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALGENERATORINDEX_LOCAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_SIGNALGENERATORINDEX_LOCAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALGENERATORINDEX_LOCAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_DIGITALIOINDEX_LOCAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_DIGITALIOINDEX_LOCAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_DIGITALIOINDEX_LOCAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_PEAKINDEX_LOCAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_PEAKINDEX_LOCAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_PEAKINDEX_LOCAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_MATHINDEX_LOCAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_MATHINDEX_LOCAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_MATHINDEX_LOCAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_ALARMINDEX_LOCAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_ALARMINDEX_LOCAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_ALARMINDEX_LOCAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_CONNECTOR_LOCAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_CONNECTOR_LOCAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_CONNECTOR_LOCAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALINDEX_LOCAL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_SIGNALINDEX_LOCAL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALINDEX_LOCAL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_CANNOT_CREATE"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_CANNOT_CREATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_CANNOT_CREATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_UNKNOWN"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_UNKNOWN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_UNKNOWN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SIGNALREFERENCE_UNKNOWN"/>.
//		/// </summary>
//		WARN_SIGNALREFERENCE_UNKNOWN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SIGNALREFERENCE_UNKNOWN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_LEVELS_IDENTICAL"/>.
//		/// </summary>
//		ERR_PARAM_LEVELS_IDENTICAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_LEVELS_IDENTICAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_CONNECTOR_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_CONNECTOR_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_CONNECTOR_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MAX_ISO_SIGNALINDEX_REACHED"/>.
//		/// </summary>
//		WARN_MAX_ISO_SIGNALINDEX_REACHED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MAX_ISO_SIGNALINDEX_REACHED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALINDEX_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_SIGNALINDEX_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALINDEX_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INDEX_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_INDEX_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INDEX_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALTYPE_NOT_SUPPORTED_OR_SYNTAX_ERROR"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_SIGNALTYPE_NOT_SUPPORTED_OR_SYNTAX_ERROR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SIGNALTYPE_NOT_SUPPORTED_OR_SYNTAX_ERROR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SIGNALREFERENCE_EMPTY"/>.
//		/// </summary>
//		WARN_SIGNALREFERENCE_EMPTY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SIGNALREFERENCE_EMPTY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULEREFERENCE_SYNTAX"/>.
//		/// </summary>
//		ERR_MODULEREFERENCE_SYNTAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULEREFERENCE_SYNTAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_PEAKINDEX_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_PEAKINDEX_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_PEAKINDEX_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_MATHINDEX_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_MATHINDEX_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_MATHINDEX_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_ALARMINDEX_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_ALARMINDEX_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_ALARMINDEX_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULEREFERENCE_NOT_LOCAL"/>.
//		/// </summary>
//		ERR_MODULEREFERENCE_NOT_LOCAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULEREFERENCE_NOT_LOCAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULEREFERENCE_NOT_EXTERNAL"/>.
//		/// </summary>
//		ERR_MODULEREFERENCE_NOT_EXTERNAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODULEREFERENCE_NOT_EXTERNAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SYNTAX"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_SYNTAX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_SYNTAX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSUNIT_NOT_AVAILABLE"/>.
//		/// </summary>
//		ERR_PHYSUNIT_NOT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PHYSUNIT_NOT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_ASYNCSIGNAL_NOT_ALLOWED_OR_SYNTAX_ERROR"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_ASYNCSIGNAL_NOT_ALLOWED_OR_SYNTAX_ERROR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_ASYNCSIGNAL_NOT_ALLOWED_OR_SYNTAX_ERROR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_DIGITALIOINDEX_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_SIGNALREFERENCE_DIGITALIOINDEX_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SIGNALREFERENCE_DIGITALIOINDEX_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IOCTL_DEVICE"/>.
//		/// </summary>
//		ERR_IOCTL_DEVICE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_IOCTL_DEVICE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DEVICE_NOT_OPEN"/>.
//		/// </summary>
//		ERR_DEVICE_NOT_OPEN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DEVICE_NOT_OPEN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CLOSE_DEVICE"/>.
//		/// </summary>
//		ERR_CLOSE_DEVICE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CLOSE_DEVICE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DEVICE_ALREADY_OPEN"/>.
//		/// </summary>
//		ERR_DEVICE_ALREADY_OPEN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DEVICE_ALREADY_OPEN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_OPEN_DEVICE"/>.
//		/// </summary>
//		ERR_OPEN_DEVICE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_OPEN_DEVICE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PINFO_NULL"/>.
//		/// </summary>
//		ERR_PINFO_NULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PINFO_NULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_POINTER_NULL"/>.
//		/// </summary>
//		ERR_POINTER_NULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_POINTER_NULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_NULL"/>.
//		/// </summary>
//		ERR_PARAMSETTINGS_NULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAMSETTINGS_NULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_CALCSYNCSIGNAL"/>.
//		/// </summary>
//		ERR_PARAM_CALCSYNCSIGNAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARAM_CALCSYNCSIGNAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PPLATFORM_NULL"/>.
//		/// </summary>
//		ERR_PPLATFORM_NULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PPLATFORM_NULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAMP_NULL"/>.
//		/// </summary>
//		ERR_PAMP_NULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAMP_NULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PANALOG_NULL"/>.
//		/// </summary>
//		ERR_PANALOG_NULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PANALOG_NULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_DATA"/>.
//		/// </summary>
//		ERR_TEDS_DATA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TEDS_DATA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_SIGNAL_GENERATOR_UNIT_AVAILABLE"/>.
//		/// </summary>
//		ERR_NO_SIGNAL_GENERATOR_UNIT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_SIGNAL_GENERATOR_UNIT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_GATE_UNIT_AVAILABLE"/>.
//		/// </summary>
//		ERR_NO_GATE_UNIT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_GATE_UNIT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_PEAK_UNIT_AVAILABLE"/>.
//		/// </summary>
//		ERR_NO_PEAK_UNIT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_PEAK_UNIT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_MATH_UNIT_AVAILABLE"/>.
//		/// </summary>
//		ERR_NO_MATH_UNIT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_MATH_UNIT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_ALARM_UNIT_AVAILABLE"/>.
//		/// </summary>
//		ERR_NO_ALARM_UNIT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_ALARM_UNIT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_DIGITAL_IO_AVAILABLE"/>.
//		/// </summary>
//		ERR_NO_DIGITAL_IO_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_DIGITAL_IO_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_CAN_CONNECTORS_AVAILABLE"/>.
//		/// </summary>
//		ERR_NO_CAN_CONNECTORS_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_CAN_CONNECTORS_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_ANALOG_OUT_CONNECTORS_AVALIBALE"/>.
//		/// </summary>
//		ERR_NO_ANALOG_OUT_CONNECTORS_AVALIBALE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_ANALOG_OUT_CONNECTORS_AVALIBALE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_ANALOG_IN_CONNECTORS_AVAILABLE"/>.
//		/// </summary>
//		ERR_NO_ANALOG_IN_CONNECTORS_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_ANALOG_IN_CONNECTORS_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HANDLING_EXTERNAL_ADAPTOR"/>.
//		/// </summary>
//		ERR_HANDLING_EXTERNAL_ADAPTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_HANDLING_EXTERNAL_ADAPTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODE_NOT_SUPPORTED_BY_SENSORTYPE"/>.
//		/// </summary>
//		ERR_MODE_NOT_SUPPORTED_BY_SENSORTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODE_NOT_SUPPORTED_BY_SENSORTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MODE_NOT_SUPPORTED_BY_SENSORTYPE"/>.
//		/// </summary>
//		WARN_MODE_NOT_SUPPORTED_BY_SENSORTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MODE_NOT_SUPPORTED_BY_SENSORTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NOT_AVAILABLE_IF_EXTERNAL_ADAPTOR_PRESENT"/>.
//		/// </summary>
//		ERR_NOT_AVAILABLE_IF_EXTERNAL_ADAPTOR_PRESENT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NOT_AVAILABLE_IF_EXTERNAL_ADAPTOR_PRESENT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_NOT_AVAILABLE_IF_EXTERNAL_ADAPTOR_PRESENT"/>.
//		/// </summary>
//		WARN_NOT_AVAILABLE_IF_EXTERNAL_ADAPTOR_PRESENT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_NOT_AVAILABLE_IF_EXTERNAL_ADAPTOR_PRESENT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXECUTE_CONTROL"/>.
//		/// </summary>
//		ERR_EXECUTE_CONTROL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EXECUTE_CONTROL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXECUTE_CONTROL"/>.
//		/// </summary>
//		WARN_EXECUTE_CONTROL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_EXECUTE_CONTROL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_VALUE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_VALUE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_VALUE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_VALUE_NOT_SUPPORTED"/>.
//		/// </summary>
//		WARN_VALUE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_VALUE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AVAILABLE_ONLY_WITH_EXTERNAL_ADAPTOR"/>.
//		/// </summary>
//		ERR_AVAILABLE_ONLY_WITH_EXTERNAL_ADAPTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_AVAILABLE_ONLY_WITH_EXTERNAL_ADAPTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_AVAILABLE_ONLY_WITH_EXTERNAL_ADAPTOR"/>.
//		/// </summary>
//		WARN_AVAILABLE_ONLY_WITH_EXTERNAL_ADAPTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_AVAILABLE_ONLY_WITH_EXTERNAL_ADAPTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONTROL_NOT_SUPPORTED_BY_SENSORTYPE"/>.
//		/// </summary>
//		ERR_CONTROL_NOT_SUPPORTED_BY_SENSORTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONTROL_NOT_SUPPORTED_BY_SENSORTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONTROL_NOT_SUPPORTED_AT_CONNECTOR"/>.
//		/// </summary>
//		ERR_CONTROL_NOT_SUPPORTED_AT_CONNECTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONTROL_NOT_SUPPORTED_AT_CONNECTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONTROL_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_CONTROL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CONTROL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CONTROL_NOT_SUPPORTED"/>.
//		/// </summary>
//		WARN_CONTROL_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CONTROL_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_MODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MODE_NOT_SUPPORTED"/>.
//		/// </summary>
//		WARN_MODE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_MODE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_KEY_NOT_FOUND"/>.
//		/// </summary>
//		ERR_KEY_NOT_FOUND = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_KEY_NOT_FOUND,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_SPACE_LEFT"/>.
//		/// </summary>
//		ERR_NO_SPACE_LEFT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_SPACE_LEFT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ASSEMBLY_SIZE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_ASSEMBLY_SIZE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ASSEMBLY_SIZE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CTRL_PEAKMODE_SIGNALCTRL"/>.
//		/// </summary>
//		WARN_CTRL_PEAKMODE_SIGNALCTRL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CTRL_PEAKMODE_SIGNALCTRL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CTRL_RUNMODE_SIGNALCTRL"/>.
//		/// </summary>
//		WARN_CTRL_RUNMODE_SIGNALCTRL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CTRL_RUNMODE_SIGNALCTRL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_SET_SIGNALGENERATOR"/>.
//		/// </summary>
//		ERR_CTRL_UNABLE_TO_SET_SIGNALGENERATOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_SET_SIGNALGENERATOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_CONTROL_ALARM_UNIT"/>.
//		/// </summary>
//		ERR_CTRL_UNABLE_TO_CONTROL_ALARM_UNIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_CONTROL_ALARM_UNIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_CONTROL_PEAK_UNIT"/>.
//		/// </summary>
//		ERR_CTRL_UNABLE_TO_CONTROL_PEAK_UNIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_CONTROL_PEAK_UNIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_SET_ZERO"/>.
//		/// </summary>
//		ERR_CTRL_UNABLE_TO_SET_ZERO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_SET_ZERO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_CONTROL_MATH_UNIT"/>.
//		/// </summary>
//		ERR_CTRL_UNABLE_TO_CONTROL_MATH_UNIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_CONTROL_MATH_UNIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_CONTROL_SIGNAL_GENERATOR"/>.
//		/// </summary>
//		ERR_CTRL_UNABLE_TO_CONTROL_SIGNAL_GENERATOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNABLE_TO_CONTROL_SIGNAL_GENERATOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNSPECIFIED"/>.
//		/// </summary>
//		ERR_CTRL_UNSPECIFIED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_UNSPECIFIED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRONG_SECURITY_CODE"/>.
//		/// </summary>
//		ERR_WRONG_SECURITY_CODE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRONG_SECURITY_CODE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_WRONG_PAR"/>.
//		/// </summary>
//		ERR_CTRL_WRONG_PAR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CTRL_WRONG_PAR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CALPOINT_TYPE_PROTECTED"/>.
//		/// </summary>
//		ERR_CALPOINT_TYPE_PROTECTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CALPOINT_TYPE_PROTECTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_RAW_RECEIVER_ENABLE_DISABLE"/>.
//		/// </summary>
//		ERR_CAN_RAW_RECEIVER_ENABLE_DISABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_RAW_RECEIVER_ENABLE_DISABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_RESOURCE_OVERLOAD"/>.
//		/// </summary>
//		ERR_CAN_RESOURCE_OVERLOAD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_RESOURCE_OVERLOAD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANSMISSIONTYPE_SOURCECHANGE_NO_SUPPORT_64_BIT"/>.
//		/// </summary>
//		ERR_TRANSMISSIONTYPE_SOURCECHANGE_NO_SUPPORT_64_BIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANSMISSIONTYPE_SOURCECHANGE_NO_SUPPORT_64_BIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_WRONG_INDEX"/>.
//		/// </summary>
//		ERR_CAN_WRONG_INDEX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_WRONG_INDEX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_VALUE_ON_ERROR_TYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_CAN_VALUE_ON_ERROR_TYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_VALUE_ON_ERROR_TYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_VALUE_ON_ERROR_TYPE_NOT_SUPPORTED_BY_DATAFORMAT"/>.
//		/// </summary>
//		ERR_CAN_VALUE_ON_ERROR_TYPE_NOT_SUPPORTED_BY_DATAFORMAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_VALUE_ON_ERROR_TYPE_NOT_SUPPORTED_BY_DATAFORMAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_SET_BITRATE"/>.
//		/// </summary>
//		ERR_CAN_SET_BITRATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_SET_BITRATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_BITRATE_CHANGED"/>.
//		/// </summary>
//		WARN_CAN_BITRATE_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_BITRATE_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_BITRATE_SAMPLEPOINTRATIO_CHANGED"/>.
//		/// </summary>
//		WARN_CAN_BITRATE_SAMPLEPOINTRATIO_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_BITRATE_SAMPLEPOINTRATIO_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_ANY_DECODER_TIMEOUT"/>.
//		/// </summary>
//		ERR_CAN_ANY_DECODER_TIMEOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_ANY_DECODER_TIMEOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_ANY_DECODER_TIMEOUT"/>.
//		/// </summary>
//		WARN_CAN_ANY_DECODER_TIMEOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_ANY_DECODER_TIMEOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_ANY_DECODER_LOSS_OF_SIGNAL"/>.
//		/// </summary>
//		ERR_CAN_ANY_DECODER_LOSS_OF_SIGNAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_ANY_DECODER_LOSS_OF_SIGNAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_ANY_DECODER_LOSS_OF_SIGNAL"/>.
//		/// </summary>
//		WARN_CAN_ANY_DECODER_LOSS_OF_SIGNAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_ANY_DECODER_LOSS_OF_SIGNAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_MAPPING_MASSAGE_INACTIVE"/>.
//		/// </summary>
//		ERR_CAN_MAPPING_MASSAGE_INACTIVE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_MAPPING_MASSAGE_INACTIVE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_MAPPING_SOURCES_OVERLAP"/>.
//		/// </summary>
//		ERR_CAN_MAPPING_SOURCES_OVERLAP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_MAPPING_SOURCES_OVERLAP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_MAPPING_NO_SOURCE_DEFINED"/>.
//		/// </summary>
//		WARN_CAN_MAPPING_NO_SOURCE_DEFINED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_MAPPING_NO_SOURCE_DEFINED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ONLY_ONE_MAPPINGSOURCE_POSSIBLE"/>.
//		/// </summary>
//		ERR_ONLY_ONE_MAPPINGSOURCE_POSSIBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ONLY_ONE_MAPPINGSOURCE_POSSIBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SIGNALLENGTH_CHANGED"/>.
//		/// </summary>
//		WARN_SIGNALLENGTH_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SIGNALLENGTH_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CCP"/>.
//		/// </summary>
//		ERR_CAN_CCP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CCP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_WRITE_CANSIGNAL"/>.
//		/// </summary>
//		ERR_CAN_WRITE_CANSIGNAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_WRITE_CANSIGNAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_SIGNALREFERENCE_INVALID"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_SIGNALREFERENCE_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_SIGNALREFERENCE_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_MODULEREFERENCE_NOT_LOCAL"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_MODULEREFERENCE_NOT_LOCAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_MODULEREFERENCE_NOT_LOCAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_MSG"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_MSG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_MSG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_TRANSMISSIONTYPE_CHANGED"/>.
//		/// </summary>
//		WARN_CAN_TRANSMISSIONTYPE_CHANGED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_TRANSMISSIONTYPE_CHANGED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMISSIONTYPE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_CAN_TRANSMISSIONTYPE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMISSIONTYPE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CONNECTOR_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_CAN_CONNECTOR_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CONNECTOR_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_CHANNEL"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_CHANNEL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_CHANNEL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_USERDEF_MSG"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_USERDEF_MSG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_USERDEF_MSG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_BYTEORDER_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_BYTEORDER_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_BYTEORDER_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_TABLE"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_TABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_TABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_BYTEVALUE"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_BYTEVALUE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_BYTEVALUE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_BYTECOUNT"/>.
//		/// </summary>
//		ERR_CAN_TRANSMIT_BYTECOUNT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMIT_BYTECOUNT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMITTER_USERDEF_MSG_SET_TABLE"/>.
//		/// </summary>
//		ERR_CAN_TRANSMITTER_USERDEF_MSG_SET_TABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMITTER_USERDEF_MSG_SET_TABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_MODELENGTH_64"/>.
//		/// </summary>
//		ERR_CAN_DECODER_MODELENGTH_64 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_MODELENGTH_64,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_MODESTARTBIT_63"/>.
//		/// </summary>
//		ERR_CAN_DECODER_MODESTARTBIT_63 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_MODESTARTBIT_63,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_LENGTH_64"/>.
//		/// </summary>
//		ERR_CAN_DECODER_LENGTH_64 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_LENGTH_64,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_STARTBIT_63"/>.
//		/// </summary>
//		ERR_CAN_DECODER_STARTBIT_63 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_STARTBIT_63,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_COMBINATION_IDENTIFIER_FRAMEFORMAT"/>.
//		/// </summary>
//		ERR_CAN_COMBINATION_IDENTIFIER_FRAMEFORMAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_COMBINATION_IDENTIFIER_FRAMEFORMAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CHANNELNO"/>.
//		/// </summary>
//		ERR_CAN_CHANNELNO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_CHANNELNO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_MODE_STARTBIT_LENGTH"/>.
//		/// </summary>
//		ERR_CAN_DECODER_MODE_STARTBIT_LENGTH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_MODE_STARTBIT_LENGTH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_COMBINATION_STARTBIT_LENGTH"/>.
//		/// </summary>
//		ERR_CAN_COMBINATION_STARTBIT_LENGTH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_COMBINATION_STARTBIT_LENGTH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_FORMAT_DOUBLE"/>.
//		/// </summary>
//		ERR_CAN_DECODER_FORMAT_DOUBLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_FORMAT_DOUBLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_FORMAT_FLOAT"/>.
//		/// </summary>
//		ERR_CAN_DECODER_FORMAT_FLOAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_FORMAT_FLOAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_FORMAT_32"/>.
//		/// </summary>
//		ERR_CAN_DECODER_FORMAT_32 = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_FORMAT_32,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_RBUFNO_INVALID"/>.
//		/// </summary>
//		ERR_CAN_DECODER_RBUFNO_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_RBUFNO_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_RBUFNO_USED_TWICE"/>.
//		/// </summary>
//		ERR_CAN_DECODER_RBUFNO_USED_TWICE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_RBUFNO_USED_TWICE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_GET_TABLE"/>.
//		/// </summary>
//		ERR_CAN_DECODER_GET_TABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_GET_TABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_SET_TABLE"/>.
//		/// </summary>
//		ERR_CAN_DECODER_SET_TABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_SET_TABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_TABLE_FULL"/>.
//		/// </summary>
//		ERR_CAN_DECODER_TABLE_FULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_TABLE_FULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_DISABLE"/>.
//		/// </summary>
//		ERR_CAN_DECODER_DISABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_DISABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_ENABLE"/>.
//		/// </summary>
//		ERR_CAN_DECODER_ENABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_ENABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_TABLE_NOT_VALID"/>.
//		/// </summary>
//		ERR_CAN_DECODER_TABLE_NOT_VALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_TABLE_NOT_VALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_PARAM_CAN_SIGNAL"/>.
//		/// </summary>
//		ERR_CAN_DECODER_PARAM_CAN_SIGNAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DECODER_PARAM_CAN_SIGNAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_NO_RESPONSE"/>.
//		/// </summary>
//		ERR_CAN_NO_RESPONSE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_NO_RESPONSE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_RESET_REQUEST"/>.
//		/// </summary>
//		WARN_CAN_RESET_REQUEST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_RESET_REQUEST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMITTER_OVERRUN"/>.
//		/// </summary>
//		ERR_CAN_TRANSMITTER_OVERRUN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_TRANSMITTER_OVERRUN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_ERROR_TRANSMITTER"/>.
//		/// </summary>
//		ERR_CAN_BUS_ERROR_TRANSMITTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_ERROR_TRANSMITTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_WARNING_TRANSMITTER"/>.
//		/// </summary>
//		ERR_CAN_BUS_WARNING_TRANSMITTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_WARNING_TRANSMITTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_BUS_WARNING_TRANSMITTER"/>.
//		/// </summary>
//		WARN_CAN_BUS_WARNING_TRANSMITTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_BUS_WARNING_TRANSMITTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_RECEIVER_OVERRUN"/>.
//		/// </summary>
//		ERR_CAN_RECEIVER_OVERRUN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_RECEIVER_OVERRUN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_OFF"/>.
//		/// </summary>
//		ERR_CAN_BUS_OFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_OFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_ERROR_RECEIVER"/>.
//		/// </summary>
//		ERR_CAN_BUS_ERROR_RECEIVER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_ERROR_RECEIVER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_WARNING_RECEIVER"/>.
//		/// </summary>
//		ERR_CAN_BUS_WARNING_RECEIVER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BUS_WARNING_RECEIVER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_BUS_WARNING_RECEIVER"/>.
//		/// </summary>
//		WARN_CAN_BUS_WARNING_RECEIVER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_CAN_BUS_WARNING_RECEIVER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_INIT_CANSIGNAL_PARAMS"/>.
//		/// </summary>
//		ERR_CAN_INIT_CANSIGNAL_PARAMS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_INIT_CANSIGNAL_PARAMS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_GET_BITRATE"/>.
//		/// </summary>
//		ERR_CAN_GET_BITRATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_GET_BITRATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_GET_STATE"/>.
//		/// </summary>
//		ERR_CAN_GET_STATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_GET_STATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_INVALID_HANDLE"/>.
//		/// </summary>
//		ERR_CAN_INVALID_HANDLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_INVALID_HANDLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_INDEX"/>.
//		/// </summary>
//		ERR_CAN_INDEX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_INDEX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_IOCTL"/>.
//		/// </summary>
//		ERR_CAN_IOCTL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_IOCTL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BITRATE_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_CAN_BITRATE_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_BITRATE_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DISABLE"/>.
//		/// </summary>
//		ERR_CAN_DISABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_DISABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_ENABLE"/>.
//		/// </summary>
//		ERR_CAN_ENABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_ENABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_OPENDRV"/>.
//		/// </summary>
//		ERR_CAN_OPENDRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CAN_OPENDRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FBFS_ACCESS_LED"/>.
//		/// </summary>
//		ERR_FBFS_ACCESS_LED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FBFS_ACCESS_LED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FBFS_ACCESS_CAN"/>.
//		/// </summary>
//		ERR_FBFS_ACCESS_CAN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_FBFS_ACCESS_CAN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_CARRIERFREQ_MODE"/>.
//		/// </summary>
//		ERR_DAQ_SET_CARRIERFREQ_MODE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_CARRIERFREQ_MODE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_FREEZED_FLAG"/>.
//		/// </summary>
//		ERR_DAQ_SET_FREEZED_FLAG = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_FREEZED_FLAG,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_RTMEASMODE"/>.
//		/// </summary>
//		ERR_DAQ_SET_RTMEASMODE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_RTMEASMODE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_TRIGTIME"/>.
//		/// </summary>
//		ERR_DAQ_TRIGTIME = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_TRIGTIME,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_TRIGGERUNKNOWN"/>.
//		/// </summary>
//		ERR_DAQ_TRIGGERUNKNOWN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_TRIGGERUNKNOWN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_ISOSIGNALLIST_VERSION"/>.
//		/// </summary>
//		ERR_DAQ_ISOSIGNALLIST_VERSION = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_ISOSIGNALLIST_VERSION,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_AMP_NULL"/>.
//		/// </summary>
//		ERR_DAQ_AMP_NULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_AMP_NULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_RTDRIVER_NULL"/>.
//		/// </summary>
//		ERR_DAQ_RTDRIVER_NULL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_RTDRIVER_NULL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_WRONG_CHAN_TYPE"/>.
//		/// </summary>
//		ERR_DAQ_WRONG_CHAN_TYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_WRONG_CHAN_TYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_ANALOGOUT_GAIN_IF_ANALOGIN_LINTABLE"/>.
//		/// </summary>
//		WARN_SCALING_ANALOGOUT_GAIN_IF_ANALOGIN_LINTABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_SCALING_ANALOGOUT_GAIN_IF_ANALOGIN_LINTABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_GET_DEMOD_INFO"/>.
//		/// </summary>
//		ERR_DAQ_GET_DEMOD_INFO = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_GET_DEMOD_INFO,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_ERRORBUF"/>.
//		/// </summary>
//		ERR_DAQ_SET_ERRORBUF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_ERRORBUF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_DRV_SET_PT_COEFFS"/>.
//		/// </summary>
//		ERR_DAQ_DRV_SET_PT_COEFFS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_DRV_SET_PT_COEFFS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_DRV_SET_COLDJUNCTION"/>.
//		/// </summary>
//		ERR_DAQ_DRV_SET_COLDJUNCTION = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_DRV_SET_COLDJUNCTION,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_DRV_SET_INITJUST"/>.
//		/// </summary>
//		ERR_DAQ_DRV_SET_INITJUST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_DRV_SET_INITJUST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_NOT_SUPPORTED_IF_ANALOGOUT_ENABLED"/>.
//		/// </summary>
//		ERR_SCALING_POLYNOM_NOT_SUPPORTED_IF_ANALOGOUT_ENABLED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_NOT_SUPPORTED_IF_ANALOGOUT_ENABLED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_TOO_MUCH_COEFFS"/>.
//		/// </summary>
//		ERR_SCALING_POLYNOM_TOO_MUCH_COEFFS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_TOO_MUCH_COEFFS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_NOT_ENOUGH_COEFFS"/>.
//		/// </summary>
//		ERR_SCALING_POLYNOM_NOT_ENOUGH_COEFFS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_NOT_ENOUGH_COEFFS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_TOO_MUCH_SEGMENTS"/>.
//		/// </summary>
//		ERR_SCALING_POLYNOM_TOO_MUCH_SEGMENTS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_TOO_MUCH_SEGMENTS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_LINTABLE_TOO_MUCH_POINTS"/>.
//		/// </summary>
//		ERR_SCALING_LINTABLE_TOO_MUCH_POINTS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_LINTABLE_TOO_MUCH_POINTS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_NOT_ENOUGH_SEGMENTS"/>.
//		/// </summary>
//		ERR_SCALING_POLYNOM_NOT_ENOUGH_SEGMENTS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_POLYNOM_NOT_ENOUGH_SEGMENTS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ACCESS_RTDRIVER"/>.
//		/// </summary>
//		ERR_ACCESS_RTDRIVER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_ACCESS_RTDRIVER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_LINTABLE_NOT_ENOUGH_POINTS"/>.
//		/// </summary>
//		ERR_SCALING_LINTABLE_NOT_ENOUGH_POINTS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SCALING_LINTABLE_NOT_ENOUGH_POINTS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SCALING_INTERNAL"/>.
//		/// </summary>
//		ERR_DAQ_SCALING_INTERNAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SCALING_INTERNAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SCALETYPE"/>.
//		/// </summary>
//		ERR_DAQ_SCALETYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SCALETYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRONG_CHANNEL_INDEX"/>.
//		/// </summary>
//		ERR_WRONG_CHANNEL_INDEX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRONG_CHANNEL_INDEX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRONG_SIG_INDEX"/>.
//		/// </summary>
//		ERR_WRONG_SIG_INDEX = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRONG_SIG_INDEX,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRONG_CONNECTOR"/>.
//		/// </summary>
//		ERR_WRONG_CONNECTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRONG_CONNECTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_INITJUST_NOTFOUND"/>.
//		/// </summary>
//		ERR_DAQ_INITJUST_NOTFOUND = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_INITJUST_NOTFOUND,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_INITJUST_POINTNR"/>.
//		/// </summary>
//		ERR_DAQ_INITJUST_POINTNR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_INITJUST_POINTNR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_INITJUST_WRONGPAR"/>.
//		/// </summary>
//		ERR_DAQ_INITJUST_WRONGPAR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_INITJUST_WRONGPAR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SIGID_LEN"/>.
//		/// </summary>
//		ERR_DAQ_SIGID_LEN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SIGID_LEN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SIGID_UNKNOWN"/>.
//		/// </summary>
//		ERR_DAQ_SIGID_UNKNOWN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SIGID_UNKNOWN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_FILTER"/>.
//		/// </summary>
//		ERR_DAQ_FILTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_FILTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SIGNAL_NOT_AVAILABLE"/>.
//		/// </summary>
//		ERR_DAQ_SIGNAL_NOT_AVAILABLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SIGNAL_NOT_AVAILABLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_ADD_FBTYPE"/>.
//		/// </summary>
//		ERR_DAQ_ADD_FBTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_ADD_FBTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_ADD_FB_INSTANCE"/>.
//		/// </summary>
//		ERR_DAQ_ADD_FB_INSTANCE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_ADD_FB_INSTANCE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_FB_INPUT"/>.
//		/// </summary>
//		ERR_DAQ_SET_FB_INPUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_FB_INPUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OUTRATE"/>.
//		/// </summary>
//		ERR_DAQ_OUTRATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OUTRATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_BUFFER_PARS"/>.
//		/// </summary>
//		ERR_DAQ_SET_BUFFER_PARS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_BUFFER_PARS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_ZEROVAL"/>.
//		/// </summary>
//		ERR_DAQ_SET_ZEROVAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_ZEROVAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_FIFO_PARAMS"/>.
//		/// </summary>
//		ERR_DAQ_FIFO_PARAMS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_FIFO_PARAMS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_FIFOREADER_CAL_PARAMS"/>.
//		/// </summary>
//		ERR_DAQ_FIFOREADER_CAL_PARAMS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_FIFOREADER_CAL_PARAMS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SCALING_PARAMS"/>.
//		/// </summary>
//		ERR_DAQ_SCALING_PARAMS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SCALING_PARAMS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_ISO_MODULE_ID"/>.
//		/// </summary>
//		ERR_DAQ_SET_ISO_MODULE_ID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_ISO_MODULE_ID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_ISO_TRANSMIT"/>.
//		/// </summary>
//		ERR_DAQ_SET_ISO_TRANSMIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SET_ISO_TRANSMIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_GET_ISO_TRANSMIT"/>.
//		/// </summary>
//		ERR_DAQ_GET_ISO_TRANSMIT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_GET_ISO_TRANSMIT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OVERRUN"/>.
//		/// </summary>
//		ERR_DAQ_OVERRUN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OVERRUN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_UNDEFINED"/>.
//		/// </summary>
//		ERR_DAQ_UNDEFINED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_UNDEFINED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_FIFO_SYNC"/>.
//		/// </summary>
//		ERR_DAQ_FIFO_SYNC = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_FIFO_SYNC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OVERLOAD"/>.
//		/// </summary>
//		ERR_DAQ_OVERLOAD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OVERLOAD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_PROCESS"/>.
//		/// </summary>
//		ERR_DAQ_PROCESS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_PROCESS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OCCF"/>.
//		/// </summary>
//		ERR_DAQ_OCCF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OCCF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_LIMSE"/>.
//		/// </summary>
//		ERR_DAQ_LIMSE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_LIMSE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_NOSIGNALSUBSCRIBED"/>.
//		/// </summary>
//		ERR_DAQ_NOSIGNALSUBSCRIBED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_NOSIGNALSUBSCRIBED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_BUFFERSIZE"/>.
//		/// </summary>
//		ERR_DAQ_BUFFERSIZE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_BUFFERSIZE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_AMPOFF"/>.
//		/// </summary>
//		ERR_DAQ_AMPOFF = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_AMPOFF,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OPENRTDRV"/>.
//		/// </summary>
//		ERR_DAQ_OPENRTDRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_OPENRTDRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SUBSCR_RUNNING"/>.
//		/// </summary>
//		ERR_DAQ_SUBSCR_RUNNING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SUBSCR_RUNNING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SUBSCR_UUID"/>.
//		/// </summary>
//		ERR_DAQ_SUBSCR_UUID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SUBSCR_UUID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SUBSCR_STATE"/>.
//		/// </summary>
//		ERR_DAQ_SUBSCR_STATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SUBSCR_STATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SUBSCRITION"/>.
//		/// </summary>
//		ERR_DAQ_SUBSCRITION = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DAQ_SUBSCRITION,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DATA_FORMAT_NOT_SUPPORTED"/>.
//		/// </summary>
//		ERR_DATA_FORMAT_NOT_SUPPORTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_DATA_FORMAT_NOT_SUPPORTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_STAT_COMPOSER"/>.
//		/// </summary>
//		ERR_SRV_STAT_COMPOSER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_STAT_COMPOSER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_NORESTARTFKT"/>.
//		/// </summary>
//		ERR_MTHD_SYS_NORESTARTFKT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_NORESTARTFKT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_WRONGCHANTYPE"/>.
//		/// </summary>
//		ERR_MTHD_SYS_WRONGCHANTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_WRONGCHANTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_WRONGCHAN"/>.
//		/// </summary>
//		ERR_MTHD_SYS_WRONGCHAN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_WRONGCHAN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_INVALIDFWFILE"/>.
//		/// </summary>
//		ERR_MTHD_SYS_INVALIDFWFILE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_INVALIDFWFILE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_FILENOTFOUND"/>.
//		/// </summary>
//		ERR_MTHD_SYS_FILENOTFOUND = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_MTHD_SYS_FILENOTFOUND,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_OPENFILE"/>.
//		/// </summary>
//		ERR_OPENFILE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_OPENFILE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRITE_FILE"/>.
//		/// </summary>
//		ERR_WRITE_FILE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_WRITE_FILE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_READ_FILE"/>.
//		/// </summary>
//		ERR_READ_FILE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_READ_FILE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARSE_XML_SETTING_REFUSED"/>.
//		/// </summary>
//		ERR_PARSE_XML_SETTING_REFUSED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARSE_XML_SETTING_REFUSED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_WRONG_TYPE"/>.
//		/// </summary>
//		ERR_PAR_WRONG_TYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_WRONG_TYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_NODEFSET"/>.
//		/// </summary>
//		WARN_PAR_NODEFSET = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_NODEFSET,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_VIEWID_INUSE"/>.
//		/// </summary>
//		ERR_PAR_VIEWID_INUSE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_VIEWID_INUSE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_READING_VAL"/>.
//		/// </summary>
//		ERR_PAR_READING_VAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_READING_VAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_EMPTY_VAL"/>.
//		/// </summary>
//		WARN_PAR_EMPTY_VAL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_EMPTY_VAL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_VIEWINVALID"/>.
//		/// </summary>
//		ERR_PAR_VIEWINVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_VIEWINVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_WRONGXPATHSTATE"/>.
//		/// </summary>
//		ERR_PAR_WRONGXPATHSTATE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_WRONGXPATHSTATE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_XPATHINVALID"/>.
//		/// </summary>
//		ERR_PAR_XPATHINVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_XPATHINVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_NOROOT"/>.
//		/// </summary>
//		ERR_PAR_NOROOT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_NOROOT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_PARSING"/>.
//		/// </summary>
//		WARN_PAR_PARSING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_PARSING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_PARSING"/>.
//		/// </summary>
//		ERR_PAR_PARSING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_PARSING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_INVALIDADDR"/>.
//		/// </summary>
//		ERR_PAR_INVALIDADDR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_INVALIDADDR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_TAGUNKOWN"/>.
//		/// </summary>
//		WARN_PAR_TAGUNKOWN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_TAGUNKOWN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_TAGUNKOWN"/>.
//		/// </summary>
//		ERR_PAR_TAGUNKOWN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_TAGUNKOWN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_OUTOFRANGE"/>.
//		/// </summary>
//		ERR_PAR_OUTOFRANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_OUTOFRANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_OUTOFRANGE"/>.
//		/// </summary>
//		WARN_PAR_OUTOFRANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_OUTOFRANGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_INVALIDATTR"/>.
//		/// </summary>
//		ERR_PAR_INVALIDATTR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_INVALIDATTR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_READONLY"/>.
//		/// </summary>
//		WARN_PAR_READONLY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_PAR_READONLY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_CHOICEINVALID"/>.
//		/// </summary>
//		ERR_PAR_CHOICEINVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_CHOICEINVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_CNTXTLOST"/>.
//		/// </summary>
//		ERR_PAR_CNTXTLOST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_CNTXTLOST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_CHOICEVALUESYNTAXINVALID"/>.
//		/// </summary>
//		ERR_PAR_CHOICEVALUESYNTAXINVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_CHOICEVALUESYNTAXINVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_RESTOREFAILED"/>.
//		/// </summary>
//		ERR_PAR_RESTOREFAILED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_RESTOREFAILED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_XMLINVALID"/>.
//		/// </summary>
//		ERR_PAR_XMLINVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PAR_XMLINVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TXTELCONTAINER_TOO_SMALL"/>.
//		/// </summary>
//		ERR_TXTELCONTAINER_TOO_SMALL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TXTELCONTAINER_TOO_SMALL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARSE_PROCESSING_DATA"/>.
//		/// </summary>
//		ERR_PARSE_PROCESSING_DATA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARSE_PROCESSING_DATA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARSE_INVALID_DATA"/>.
//		/// </summary>
//		ERR_PARSE_INVALID_DATA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_PARSE_INVALID_DATA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_FILE_RESTORE"/>.
//		/// </summary>
//		ERR_UTIL_FILE_RESTORE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_FILE_RESTORE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_INVALID_FILEHANDLE"/>.
//		/// </summary>
//		ERR_UTIL_INVALID_FILEHANDLE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_INVALID_FILEHANDLE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_STRCONVERT"/>.
//		/// </summary>
//		ERR_UTIL_STRCONVERT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_STRCONVERT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_NO_PATTERN_MATCH"/>.
//		/// </summary>
//		ERR_UTIL_NO_PATTERN_MATCH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_NO_PATTERN_MATCH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EVFLG_TIMEOUT"/>.
//		/// </summary>
//		ERR_EVFLG_TIMEOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EVFLG_TIMEOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EVFLG_PTHREADS"/>.
//		/// </summary>
//		ERR_EVFLG_PTHREADS = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_EVFLG_PTHREADS,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_TIMER_INVALID"/>.
//		/// </summary>
//		ERR_UTIL_TIMER_INVALID = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_TIMER_INVALID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_UTIL_TIMER_WASRUNNING"/>.
//		/// </summary>
//		WARN_UTIL_TIMER_WASRUNNING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.WARN_UTIL_TIMER_WASRUNNING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_LANGUAGE"/>.
//		/// </summary>
//		ERR_UTIL_LANGUAGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UTIL_LANGUAGE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_COMPOSER_TAG_NOT_DEFINED"/>.
//		/// </summary>
//		ERR_COMPOSER_TAG_NOT_DEFINED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_COMPOSER_TAG_NOT_DEFINED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NET_NETWORKADAPTER"/>.
//		/// </summary>
//		ERR_NET_NETWORKADAPTER = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NET_NETWORKADAPTER,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_BINDTODEVICE"/>.
//		/// </summary>
//		ERR_UDP_BINDTODEVICE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_BINDTODEVICE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_BIND"/>.
//		/// </summary>
//		ERR_UDP_BIND = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_BIND,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_SETBROADCAST"/>.
//		/// </summary>
//		ERR_UDP_SETBROADCAST = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_SETBROADCAST,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_SETREUSEADR"/>.
//		/// </summary>
//		ERR_UDP_SETREUSEADR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_SETREUSEADR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_OPENSOCK"/>.
//		/// </summary>
//		ERR_UDP_OPENSOCK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_UDP_OPENSOCK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_BIND"/>.
//		/// </summary>
//		ERR_TCP_BIND = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_BIND,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_LISTEN"/>.
//		/// </summary>
//		ERR_TCP_LISTEN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_LISTEN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_CHECKIP"/>.
//		/// </summary>
//		ERR_TCP_CHECKIP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_CHECKIP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_GETADR"/>.
//		/// </summary>
//		ERR_TCP_GETADR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_GETADR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_GETPORT"/>.
//		/// </summary>
//		ERR_TCP_GETPORT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_GETPORT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SETNODELAY"/>.
//		/// </summary>
//		ERR_TCP_SETNODELAY = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SETNODELAY,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SETNONBLOCK"/>.
//		/// </summary>
//		ERR_TCP_SETNONBLOCK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SETNONBLOCK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_OPENSOCK"/>.
//		/// </summary>
//		ERR_TCP_OPENSOCK = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_OPENSOCK,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_CONNECT"/>.
//		/// </summary>
//		ERR_TCP_CONNECT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_CONNECT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_ACCEPT"/>.
//		/// </summary>
//		ERR_TCP_ACCEPT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_ACCEPT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_CONNECT_FAILED"/>.
//		/// </summary>
//		ERR_TCP_CONNECT_FAILED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_CONNECT_FAILED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_IPADR_ALREADY_CONNECTED"/>.
//		/// </summary>
//		ERR_TCP_IPADR_ALREADY_CONNECTED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_IPADR_ALREADY_CONNECTED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_ADR_UNKNOWN"/>.
//		/// </summary>
//		ERR_TCP_ADR_UNKNOWN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_ADR_UNKNOWN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SOCKET_EINTR"/>.
//		/// </summary>
//		ERR_TCP_SOCKET_EINTR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SOCKET_EINTR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SOCKET_HUP"/>.
//		/// </summary>
//		ERR_TCP_SOCKET_HUP = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SOCKET_HUP,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SOCKET_DESCRIPTOR"/>.
//		/// </summary>
//		ERR_TCP_SOCKET_DESCRIPTOR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TCP_SOCKET_DESCRIPTOR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_REC"/>.
//		/// </summary>
//		ERR_CON_REC = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_REC,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_SENDING"/>.
//		/// </summary>
//		ERR_CON_SENDING = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_SENDING,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_INVALID_TELTYPE"/>.
//		/// </summary>
//		ERR_CON_INVALID_TELTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_INVALID_TELTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_ADRRESINI_FAILED"/>.
//		/// </summary>
//		ERR_CON_ADRRESINI_FAILED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_ADRRESINI_FAILED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_CONHNDLR_UUIDUNKNOWN"/>.
//		/// </summary>
//		ERR_CON_CONHNDLR_UUIDUNKNOWN = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_CON_CONHNDLR_UUIDUNKNOWN,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_READINGHDR"/>.
//		/// </summary>
//		ERR_TRANS_READINGHDR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_READINGHDR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_NOTFOUND"/>.
//		/// </summary>
//		ERR_TRANS_NOTFOUND = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_NOTFOUND,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_CANCELED"/>.
//		/// </summary>
//		ERR_TRANS_CANCELED = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_CANCELED,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_SEND_REQ_TIMEOUT"/>.
//		/// </summary>
//		ERR_TRANS_SEND_REQ_TIMEOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_SEND_REQ_TIMEOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_ACK_TIMEOUT"/>.
//		/// </summary>
//		ERR_TRANS_ACK_TIMEOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_ACK_TIMEOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_REQ_TIMEOUT"/>.
//		/// </summary>
//		ERR_TRANS_REQ_TIMEOUT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_REQ_TIMEOUT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_INVALID_TELFORMAT"/>.
//		/// </summary>
//		ERR_TRANS_INVALID_TELFORMAT = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_INVALID_TELFORMAT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_REQUESTOR_WRONGPROTTYPE"/>.
//		/// </summary>
//		ERR_TRANS_REQUESTOR_WRONGPROTTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_REQUESTOR_WRONGPROTTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_RESPONDER_WRONGPROTTYPE"/>.
//		/// </summary>
//		ERR_TRANS_RESPONDER_WRONGPROTTYPE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_RESPONDER_WRONGPROTTYPE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_NOT_FOR_US"/>.
//		/// </summary>
//		ERR_TRANS_NOT_FOR_US = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_TRANS_NOT_FOR_US,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_MSP_CHKSUM"/>.
//		/// </summary>
//		ERR_NO_MSP_CHKSUM = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_NO_MSP_CHKSUM,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_CONTROL"/>.
//		/// </summary>
//		ERR_SRV_CONTROL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_CONTROL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_NODATA"/>.
//		/// </summary>
//		ERR_SRV_NODATA = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_NODATA,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_NORESPONSE_YET"/>.
//		/// </summary>
//		ERR_SRV_NORESPONSE_YET = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_NORESPONSE_YET,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_REQRMTHD_NOREQTEL"/>.
//		/// </summary>
//		ERR_SRV_REQRMTHD_NOREQTEL = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_REQRMTHD_NOREQTEL,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_PARAMERROR"/>.
//		/// </summary>
//		ERR_SRV_PARAMERROR = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_PARAMERROR,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_MTHDOUT_UNKNOWNMTHD"/>.
//		/// </summary>
//		ERR_SRV_MTHDOUT_UNKNOWNMTHD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_MTHDOUT_UNKNOWNMTHD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_MTHDOUT_WRONGANSW"/>.
//		/// </summary>
//		ERR_SRV_MTHDOUT_WRONGANSW = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_MTHDOUT_WRONGANSW,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_MTHDOUT_TOOMUCH"/>.
//		/// </summary>
//		ERR_SRV_MTHDOUT_TOOMUCH = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_MTHDOUT_TOOMUCH,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_UNKNOWNSRV"/>.
//		/// </summary>
//		ERR_SRV_UNKNOWNSRV = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_UNKNOWNSRV,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_UNKNOWNMTHD"/>.
//		/// </summary>
//		ERR_SRV_UNKNOWNMTHD = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_SRV_UNKNOWNMTHD,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INDEX_OUT_OF_RANGE"/>.
//		/// </summary>
//		ERR_INDEX_OUT_OF_RANGE = (int)global::Hbm.Api.QuantumX.Enums.FrameworkError.ERR_INDEX_OUT_OF_RANGE,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.LedBlinking"/>.
//	/// </summary>
//	public enum HbmCoreLedBlinking
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.LedBlinking.Green"/>.
//		/// </summary>
//		Green = (int)global::Hbm.Api.QuantumX.Enums.LedBlinking.Green,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.LedBlinking.Orange"/>.
//		/// </summary>
//		Orange = (int)global::Hbm.Api.QuantumX.Enums.LedBlinking.Orange,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.LedBlinking.Red"/>.
//		/// </summary>
//		Red = (int)global::Hbm.Api.QuantumX.Enums.LedBlinking.Red,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.LedFlashMode"/>.
//	/// </summary>
//	public enum HbmCoreLedFlashMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.LedFlashMode.Off"/>.
//		/// </summary>
//		Off = (int)global::Hbm.Api.Common.Enums.LedFlashMode.Off,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.LedFlashMode.Green"/>.
//		/// </summary>
//		Green = (int)global::Hbm.Api.Common.Enums.LedFlashMode.Green,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.LedFlashMode.Yellow"/>.
//		/// </summary>
//		Yellow = (int)global::Hbm.Api.Common.Enums.LedFlashMode.Yellow,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.LedFlashMode.Red"/>.
//		/// </summary>
//		Red = (int)global::Hbm.Api.Common.Enums.LedFlashMode.Red,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Logging.Enums.LoggingFramework"/>.
//	/// </summary>
//	public enum HbmCoreLoggingFramework
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LoggingFramework.Loupe"/>.
//		/// </summary>
//		Loupe = (int)global::Hbm.Api.Logging.Enums.LoggingFramework.Loupe,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LoggingFramework.NLog"/>.
//		/// </summary>
//		NLog = (int)global::Hbm.Api.Logging.Enums.LoggingFramework.NLog,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Logging.Enums.LogLevel"/>.
//	/// </summary>
//	[Flags]
//	public enum HbmCoreLogLevel
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LogLevel.Off"/>.
//		/// </summary>
//		Off = (int)global::Hbm.Api.Logging.Enums.LogLevel.Off,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LogLevel.All"/>.
//		/// </summary>
//		All = (int)global::Hbm.Api.Logging.Enums.LogLevel.All,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LogLevel.Trace"/>.
//		/// </summary>
//		Trace = (int)global::Hbm.Api.Logging.Enums.LogLevel.Trace,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LogLevel.Debug"/>.
//		/// </summary>
//		Debug = (int)global::Hbm.Api.Logging.Enums.LogLevel.Debug,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LogLevel.Info"/>.
//		/// </summary>
//		Info = (int)global::Hbm.Api.Logging.Enums.LogLevel.Info,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LogLevel.Warn"/>.
//		/// </summary>
//		Warn = (int)global::Hbm.Api.Logging.Enums.LogLevel.Warn,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LogLevel.Error"/>.
//		/// </summary>
//		Error = (int)global::Hbm.Api.Logging.Enums.LogLevel.Error,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Logging.Enums.LogLevel.Database"/>.
//		/// </summary>
//		Database = (int)global::Hbm.Api.Logging.Enums.LogLevel.Database,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.MeasurementSignalType"/>.
//	/// </summary>
//	public enum HbmCoreMeasurementSignalType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.MeasurementSignalType.GrossSignalType"/>.
//		/// </summary>
//		GrossSignalType = (int)global::Hbm.Api.Mgc.Enums.MeasurementSignalType.GrossSignalType,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.MeasurementSignalType.NetSignalType"/>.
//		/// </summary>
//		NetSignalType = (int)global::Hbm.Api.Mgc.Enums.MeasurementSignalType.NetSignalType,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.MeasurementValueState"/>.
//	/// </summary>
//	[Flags]
//	public enum HbmCoreMeasurementValueState
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.MeasurementValueState.Valid"/>.
//		/// </summary>
//		Valid = (int)global::Hbm.Api.Common.Enums.MeasurementValueState.Valid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.MeasurementValueState.Overflow"/>.
//		/// </summary>
//		Overflow = (int)global::Hbm.Api.Common.Enums.MeasurementValueState.Overflow,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType"/>.
//	/// </summary>
//	public enum HbmCoreMgcAmplifierType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.Unknown"/>.
//		/// </summary>
//		Unknown = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.Unknown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML30"/>.
//		/// </summary>
//		ML30 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML30,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML50"/>.
//		/// </summary>
//		ML50 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML50,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML01"/>.
//		/// </summary>
//		ML01 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML01,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML55"/>.
//		/// </summary>
//		ML55 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML55,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML60"/>.
//		/// </summary>
//		ML60 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML60,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML35"/>.
//		/// </summary>
//		ML35 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML35,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML10"/>.
//		/// </summary>
//		ML10 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML10,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML51"/>.
//		/// </summary>
//		ML51 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML51,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML55S6"/>.
//		/// </summary>
//		ML55S6 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML55S6,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML38"/>.
//		/// </summary>
//		ML38 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML38,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML85_3"/>.
//		/// </summary>
//		ML85_3 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML85_3,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML85_1"/>.
//		/// </summary>
//		ML85_1 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML85_1,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML70"/>.
//		/// </summary>
//		ML70 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML70,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML71"/>.
//		/// </summary>
//		ML71 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML71,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML77"/>.
//		/// </summary>
//		ML77 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML77,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML801"/>.
//		/// </summary>
//		ML801 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML801,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML78"/>.
//		/// </summary>
//		ML78 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML78,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML71S6"/>.
//		/// </summary>
//		ML71S6 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML71S6,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML460"/>.
//		/// </summary>
//		ML460 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML460,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML74"/>.
//		/// </summary>
//		ML74 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML74,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.AmplifierType.ML455"/>.
//		/// </summary>
//		ML455 = (int)global::Hbm.Api.Mgc.Enums.AmplifierType.ML455,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.MgcDeviceStatus"/>.
//	/// </summary>
//	[Flags]
//	public enum HbmCoreMgcDeviceStatus
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.MgcDeviceStatus.NoError"/>.
//		/// </summary>
//		NoError = (int)global::Hbm.Api.Mgc.Enums.MgcDeviceStatus.NoError,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.MgcDeviceStatus.DeviceDependentError"/>.
//		/// </summary>
//		DeviceDependentError = (int)global::Hbm.Api.Mgc.Enums.MgcDeviceStatus.DeviceDependentError,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.MgcDeviceStatus.ParameterDefective"/>.
//		/// </summary>
//		ParameterDefective = (int)global::Hbm.Api.Mgc.Enums.MgcDeviceStatus.ParameterDefective,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.MgcDeviceStatus.UnknownCommand"/>.
//		/// </summary>
//		UnknownCommand = (int)global::Hbm.Api.Mgc.Enums.MgcDeviceStatus.UnknownCommand,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.FrameFormat"/>.
//	/// </summary>
//	public enum HbmCoreMgcFrameFormat
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.FrameFormat.Standard_11BitID"/>.
//		/// </summary>
//		Standard_11BitID = (int)global::Hbm.Api.Mgc.Enums.FrameFormat.Standard_11BitID,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.FrameFormat.Extended_29BitID"/>.
//		/// </summary>
//		Extended_29BitID = (int)global::Hbm.Api.Mgc.Enums.FrameFormat.Extended_29BitID,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection"/>.
//	/// </summary>
//	public enum HbmCoreMgcLimitSwitchOperatingDirection
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.Off"/>.
//		/// </summary>
//		Off = (int)global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.Off,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.AboveLimit"/>.
//		/// </summary>
//		AboveLimit = (int)global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.AboveLimit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.BelowLimit"/>.
//		/// </summary>
//		BelowLimit = (int)global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.BelowLimit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.InBand"/>.
//		/// </summary>
//		InBand = (int)global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.InBand,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.OutOfBand"/>.
//		/// </summary>
//		OutOfBand = (int)global::Hbm.Api.Mgc.Enums.LimitSwitchOperatingDirection.OutOfBand,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.ModeSignalByteOrder"/>.
//	/// </summary>
//	public enum HbmCoreModeSignalByteOrder
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ModeSignalByteOrder.Intel"/>.
//		/// </summary>
//		Intel = (int)global::Hbm.Api.Mgc.Enums.ModeSignalByteOrder.Intel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.ModeSignalByteOrder.Motorola"/>.
//		/// </summary>
//		Motorola = (int)global::Hbm.Api.Mgc.Enums.ModeSignalByteOrder.Motorola,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.NetworkSettingsConfiguration"/>.
//	/// </summary>
//	public enum HbmCoreNetworkSettingsConfiguration
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.NetworkSettingsConfiguration.Dhcp"/>.
//		/// </summary>
//		Dhcp = (int)global::Hbm.Api.Common.Enums.NetworkSettingsConfiguration.Dhcp,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.NetworkSettingsConfiguration.Static"/>.
//		/// </summary>
//		Static = (int)global::Hbm.Api.Common.Enums.NetworkSettingsConfiguration.Static,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.OriginOfNameType"/>.
//	/// </summary>
//	public enum HbmCoreOriginOfNameType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.OriginOfNameType.Default"/>.
//		/// </summary>
//		Default = (int)global::Hbm.Api.QuantumX.Enums.OriginOfNameType.Default,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.OriginOfNameType.User"/>.
//		/// </summary>
//		User = (int)global::Hbm.Api.QuantumX.Enums.OriginOfNameType.User,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.OriginOfNameType.Teds"/>.
//		/// </summary>
//		Teds = (int)global::Hbm.Api.QuantumX.Enums.OriginOfNameType.Teds,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.OriginOfNameType.Unknown"/>.
//		/// </summary>
//		Unknown = (int)global::Hbm.Api.QuantumX.Enums.OriginOfNameType.Unknown,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.OutputScalingType"/>.
//	/// </summary>
//	public enum HbmCoreOutputScalingType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.OutputScalingType.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.OutputScalingType.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.OutputScalingType.Table"/>.
//		/// </summary>
//		Table = (int)global::Hbm.Api.Common.Enums.OutputScalingType.Table,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.OutputScalingType.Off"/>.
//		/// </summary>
//		Off = (int)global::Hbm.Api.Common.Enums.OutputScalingType.Off,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.OutputType"/>.
//	/// </summary>
//	public enum HbmCoreOutputType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.OutputType.Voltage"/>.
//		/// </summary>
//		Voltage = (int)global::Hbm.Api.QuantumX.Enums.OutputType.Voltage,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Pmx.Enums.AmplifierType"/>.
//	/// </summary>
//	public enum HbmCorePmxAmplifierType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.AmplifierType.Unknown"/>.
//		/// </summary>
//		Unknown = (int)global::Hbm.Api.Pmx.Enums.AmplifierType.Unknown,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.AmplifierType.PX401"/>.
//		/// </summary>
//		PX401 = (int)global::Hbm.Api.Pmx.Enums.AmplifierType.PX401,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.AmplifierType.PX455"/>.
//		/// </summary>
//		PX455 = (int)global::Hbm.Api.Pmx.Enums.AmplifierType.PX455,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.AmplifierType.PX878"/>.
//		/// </summary>
//		PX878 = (int)global::Hbm.Api.Pmx.Enums.AmplifierType.PX878,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.AmplifierType.PX460"/>.
//		/// </summary>
//		PX460 = (int)global::Hbm.Api.Pmx.Enums.AmplifierType.PX460,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.AmplifierType.VirtualCalc"/>.
//		/// </summary>
//		VirtualCalc = (int)global::Hbm.Api.Pmx.Enums.AmplifierType.VirtualCalc,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.AmplifierType.DigitalIO"/>.
//		/// </summary>
//		DigitalIO = (int)global::Hbm.Api.Pmx.Enums.AmplifierType.DigitalIO,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus"/>.
//	/// </summary>
//	[Flags]
//	public enum HbmCorePmxDeviceStatus
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.NoError"/>.
//		/// </summary>
//		NoError = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.NoError,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.FactorySettingsError"/>.
//		/// </summary>
//		FactorySettingsError = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.FactorySettingsError,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.SyncMaster"/>.
//		/// </summary>
//		SyncMaster = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.SyncMaster,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.SyncMessageError"/>.
//		/// </summary>
//		SyncMessageError = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.SyncMessageError,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.SyncUnlockedSlaveError"/>.
//		/// </summary>
//		SyncUnlockedSlaveError = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.SyncUnlockedSlaveError,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.Alive"/>.
//		/// </summary>
//		Alive = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.Alive,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.PowerOverLoad"/>.
//		/// </summary>
//		PowerOverLoad = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.PowerOverLoad,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.CatBufferOverrun"/>.
//		/// </summary>
//		CatBufferOverrun = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.CatBufferOverrun,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.SystemNotReady"/>.
//		/// </summary>
//		SystemNotReady = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.SystemNotReady,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.DSPOverRun"/>.
//		/// </summary>
//		DSPOverRun = (int)global::Hbm.Api.Pmx.Enums.PmxDeviceStatus.DSPOverRun,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection"/>.
//	/// </summary>
//	public enum HbmCorePmxLimitSwitchOperatingDirection
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.Off"/>.
//		/// </summary>
//		Off = (int)global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.Off,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.AboveLimit"/>.
//		/// </summary>
//		AboveLimit = (int)global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.AboveLimit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.BelowLimit"/>.
//		/// </summary>
//		BelowLimit = (int)global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.BelowLimit,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.InBand"/>.
//		/// </summary>
//		InBand = (int)global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.InBand,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.OutOfBand"/>.
//		/// </summary>
//		OutOfBand = (int)global::Hbm.Api.Pmx.Enums.LimitSwitchOperatingDirection.OutOfBand,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.PolarityType"/>.
//	/// </summary>
//	public enum HbmCorePolarityType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PolarityType.NotInverted"/>.
//		/// </summary>
//		NotInverted = (int)global::Hbm.Api.SensorDB.Enums.PolarityType.NotInverted,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PolarityType.Inverted"/>.
//		/// </summary>
//		Inverted = (int)global::Hbm.Api.SensorDB.Enums.PolarityType.Inverted,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring"/>.
//	/// </summary>
//	public enum HbmCorePotentiometerSensorWiring
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.NotSet"/>.
//		/// </summary>
//		NotSet = (int)global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.NotSet,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.ThreeWire"/>.
//		/// </summary>
//		ThreeWire = (int)global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.ThreeWire,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.FourWire"/>.
//		/// </summary>
//		FourWire = (int)global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.FourWire,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.FiveWire"/>.
//		/// </summary>
//		FiveWire = (int)global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.FiveWire,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.SixWire"/>.
//		/// </summary>
//		SixWire = (int)global::Hbm.Api.SensorDB.Enums.PotentiometerSensorWiring.SixWire,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.PtSensorWiring"/>.
//	/// </summary>
//	public enum HbmCorePtSensorWiring
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PtSensorWiring.NotSet"/>.
//		/// </summary>
//		NotSet = (int)global::Hbm.Api.SensorDB.Enums.PtSensorWiring.NotSet,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PtSensorWiring.ThreeWire"/>.
//		/// </summary>
//		ThreeWire = (int)global::Hbm.Api.SensorDB.Enums.PtSensorWiring.ThreeWire,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PtSensorWiring.FourWire"/>.
//		/// </summary>
//		FourWire = (int)global::Hbm.Api.SensorDB.Enums.PtSensorWiring.FourWire,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.PwmType"/>.
//	/// </summary>
//	public enum HbmCorePwmType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PwmType.DutyCycle"/>.
//		/// </summary>
//		DutyCycle = (int)global::Hbm.Api.SensorDB.Enums.PwmType.DutyCycle,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PwmType.Duration"/>.
//		/// </summary>
//		Duration = (int)global::Hbm.Api.SensorDB.Enums.PwmType.Duration,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.PwmType.Period"/>.
//		/// </summary>
//		Period = (int)global::Hbm.Api.SensorDB.Enums.PwmType.Period,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.QuantumXEnableConnectorType"/>.
//	/// </summary>
//	public enum HbmCoreQuantumXEnableConnectorType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.QuantumXEnableConnectorType.Default"/>.
//		/// </summary>
//		Default = (int)global::Hbm.Api.QuantumX.Enums.QuantumXEnableConnectorType.Default,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.QuantumXEnableConnectorType.CanBus"/>.
//		/// </summary>
//		CanBus = (int)global::Hbm.Api.QuantumX.Enums.QuantumXEnableConnectorType.CanBus,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.QuantumXTestSignalActiveValueType"/>.
//	/// </summary>
//	public enum HbmCoreQuantumXTestSignalActiveValueType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.QuantumXTestSignalActiveValueType.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.QuantumX.Enums.QuantumXTestSignalActiveValueType.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.QuantumXTestSignalActiveValueType.NotActive"/>.
//		/// </summary>
//		NotActive = (int)global::Hbm.Api.QuantumX.Enums.QuantumXTestSignalActiveValueType.NotActive,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.QuantumXTestSignalActiveValueType.Active"/>.
//		/// </summary>
//		Active = (int)global::Hbm.Api.QuantumX.Enums.QuantumXTestSignalActiveValueType.Active,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.RawValueFormat"/>.
//	/// </summary>
//	public enum HbmCoreRawValueFormat
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.RawValueFormat.UnsignedInt32"/>.
//		/// </summary>
//		UnsignedInt32 = (int)global::Hbm.Api.SensorDB.Enums.RawValueFormat.UnsignedInt32,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.RawValueFormat.SignedInt32"/>.
//		/// </summary>
//		SignedInt32 = (int)global::Hbm.Api.SensorDB.Enums.RawValueFormat.SignedInt32,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.RawValueFormat.UnsignedInt64"/>.
//		/// </summary>
//		UnsignedInt64 = (int)global::Hbm.Api.SensorDB.Enums.RawValueFormat.UnsignedInt64,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.RawValueFormat.SignedInt64"/>.
//		/// </summary>
//		SignedInt64 = (int)global::Hbm.Api.SensorDB.Enums.RawValueFormat.SignedInt64,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.RawValueFormat.Float32"/>.
//		/// </summary>
//		Float32 = (int)global::Hbm.Api.SensorDB.Enums.RawValueFormat.Float32,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.RawValueFormat.Float64"/>.
//		/// </summary>
//		Float64 = (int)global::Hbm.Api.SensorDB.Enums.RawValueFormat.Float64,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.ReleaseType"/>.
//	/// </summary>
//	public enum HbmCoreReleaseType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ReleaseType.All"/>.
//		/// </summary>
//		All = (int)global::Hbm.Api.Common.Enums.ReleaseType.All,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ReleaseType.Latest"/>.
//		/// </summary>
//		Latest = (int)global::Hbm.Api.Common.Enums.ReleaseType.Latest,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.ResistanceSensorWiring"/>.
//	/// </summary>
//	public enum HbmCoreResistanceSensorWiring
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ResistanceSensorWiring.NotSet"/>.
//		/// </summary>
//		NotSet = (int)global::Hbm.Api.SensorDB.Enums.ResistanceSensorWiring.NotSet,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ResistanceSensorWiring.ThreeWire"/>.
//		/// </summary>
//		ThreeWire = (int)global::Hbm.Api.SensorDB.Enums.ResistanceSensorWiring.ThreeWire,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ResistanceSensorWiring.FourWire"/>.
//		/// </summary>
//		FourWire = (int)global::Hbm.Api.SensorDB.Enums.ResistanceSensorWiring.FourWire,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType"/>.
//	/// </summary>
//	public enum HbmCoreScalingType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.SensorDB.Enums.ScalingType.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType.TwoPoint"/>.
//		/// </summary>
//		TwoPoint = (int)global::Hbm.Api.SensorDB.Enums.ScalingType.TwoPoint,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType.ZeroSpan"/>.
//		/// </summary>
//		ZeroSpan = (int)global::Hbm.Api.SensorDB.Enums.ScalingType.ZeroSpan,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType.Table"/>.
//		/// </summary>
//		Table = (int)global::Hbm.Api.SensorDB.Enums.ScalingType.Table,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType.Polynomial"/>.
//		/// </summary>
//		Polynomial = (int)global::Hbm.Api.SensorDB.Enums.ScalingType.Polynomial,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType.Gage"/>.
//		/// </summary>
//		Gage = (int)global::Hbm.Api.SensorDB.Enums.ScalingType.Gage,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType.Internal"/>.
//		/// </summary>
//		Internal = (int)global::Hbm.Api.SensorDB.Enums.ScalingType.Internal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType.CanBus"/>.
//		/// </summary>
//		CanBus = (int)global::Hbm.Api.SensorDB.Enums.ScalingType.CanBus,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.ScalingType.Off"/>.
//		/// </summary>
//		Off = (int)global::Hbm.Api.SensorDB.Enums.ScalingType.Off,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.SensorType"/>.
//	/// </summary>
//	public enum HbmCoreSensorType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.SensorDB.Enums.SensorType.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Bridge"/>.
//		/// </summary>
//		Bridge = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Bridge,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.CanBus"/>.
//		/// </summary>
//		CanBus = (int)global::Hbm.Api.SensorDB.Enums.SensorType.CanBus,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Counter"/>.
//		/// </summary>
//		Counter = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Counter,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Current"/>.
//		/// </summary>
//		Current = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Current,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Frequency"/>.
//		/// </summary>
//		Frequency = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Frequency,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Iepe"/>.
//		/// </summary>
//		Iepe = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Iepe,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Lvdt"/>.
//		/// </summary>
//		Lvdt = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Lvdt,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.PiezoPassive"/>.
//		/// </summary>
//		PiezoPassive = (int)global::Hbm.Api.SensorDB.Enums.SensorType.PiezoPassive,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Potentiometer"/>.
//		/// </summary>
//		Potentiometer = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Potentiometer,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Pt"/>.
//		/// </summary>
//		Pt = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Pt,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Pwm"/>.
//		/// </summary>
//		Pwm = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Pwm,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Resistance"/>.
//		/// </summary>
//		Resistance = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Resistance,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Ssi"/>.
//		/// </summary>
//		Ssi = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Ssi,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.ThermoCouple"/>.
//		/// </summary>
//		ThermoCouple = (int)global::Hbm.Api.SensorDB.Enums.SensorType.ThermoCouple,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.Voltage"/>.
//		/// </summary>
//		Voltage = (int)global::Hbm.Api.SensorDB.Enums.SensorType.Voltage,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.VoltageProbe"/>.
//		/// </summary>
//		VoltageProbe = (int)global::Hbm.Api.SensorDB.Enums.SensorType.VoltageProbe,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.CurrentProbe"/>.
//		/// </summary>
//		CurrentProbe = (int)global::Hbm.Api.SensorDB.Enums.SensorType.CurrentProbe,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.FbgGeneric"/>.
//		/// </summary>
//		FbgGeneric = (int)global::Hbm.Api.SensorDB.Enums.SensorType.FbgGeneric,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.FbgStraingage"/>.
//		/// </summary>
//		FbgStraingage = (int)global::Hbm.Api.SensorDB.Enums.SensorType.FbgStraingage,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.FbgThermocouple"/>.
//		/// </summary>
//		FbgThermocouple = (int)global::Hbm.Api.SensorDB.Enums.SensorType.FbgThermocouple,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.FbgWavelength"/>.
//		/// </summary>
//		FbgWavelength = (int)global::Hbm.Api.SensorDB.Enums.SensorType.FbgWavelength,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SensorType.FbgAccelerometer"/>.
//		/// </summary>
//		FbgAccelerometer = (int)global::Hbm.Api.SensorDB.Enums.SensorType.FbgAccelerometer,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.SettingType"/>.
//	/// </summary>
//	public enum HbmCoreSettingType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.Device"/>.
//		/// </summary>
//		Device = (int)global::Hbm.Api.Common.Enums.SettingType.Device,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.Connector"/>.
//		/// </summary>
//		Connector = (int)global::Hbm.Api.Common.Enums.SettingType.Connector,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.Channel"/>.
//		/// </summary>
//		Channel = (int)global::Hbm.Api.Common.Enums.SettingType.Channel,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.Sensor"/>.
//		/// </summary>
//		Sensor = (int)global::Hbm.Api.Common.Enums.SettingType.Sensor,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.Signal"/>.
//		/// </summary>
//		Signal = (int)global::Hbm.Api.Common.Enums.SettingType.Signal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.Filter"/>.
//		/// </summary>
//		Filter = (int)global::Hbm.Api.Common.Enums.SettingType.Filter,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.Zero"/>.
//		/// </summary>
//		Zero = (int)global::Hbm.Api.Common.Enums.SettingType.Zero,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.Scaling"/>.
//		/// </summary>
//		Scaling = (int)global::Hbm.Api.Common.Enums.SettingType.Scaling,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.AdditionalFeature"/>.
//		/// </summary>
//		AdditionalFeature = (int)global::Hbm.Api.Common.Enums.SettingType.AdditionalFeature,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.ConnectionInfo"/>.
//		/// </summary>
//		ConnectionInfo = (int)global::Hbm.Api.Common.Enums.SettingType.ConnectionInfo,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.TimeSource"/>.
//		/// </summary>
//		TimeSource = (int)global::Hbm.Api.Common.Enums.SettingType.TimeSource,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.DataRateDomain"/>.
//		/// </summary>
//		DataRateDomain = (int)global::Hbm.Api.Common.Enums.SettingType.DataRateDomain,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.TedsUsageMode"/>.
//		/// </summary>
//		TedsUsageMode = (int)global::Hbm.Api.Common.Enums.SettingType.TedsUsageMode,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SettingType.ExtendedSampleRateMode"/>.
//		/// </summary>
//		ExtendedSampleRateMode = (int)global::Hbm.Api.Common.Enums.SettingType.ExtendedSampleRateMode,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.ShuntMode"/>.
//	/// </summary>
//	public enum HbmCoreShuntMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ShuntMode.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.ShuntMode.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ShuntMode.Disabled"/>.
//		/// </summary>
//		Disabled = (int)global::Hbm.Api.Common.Enums.ShuntMode.Disabled,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.ShuntMode.Enabled"/>.
//		/// </summary>
//		Enabled = (int)global::Hbm.Api.Common.Enums.ShuntMode.Enabled,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.Signal2Type"/>.
//	/// </summary>
//	public enum HbmCoreSignal2Type
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.Signal2Type.Off"/>.
//		/// </summary>
//		Off = (int)global::Hbm.Api.SensorDB.Enums.Signal2Type.Off,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.Signal2Type.Dynamic"/>.
//		/// </summary>
//		Dynamic = (int)global::Hbm.Api.SensorDB.Enums.Signal2Type.Dynamic,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.Signal2Type.Static"/>.
//		/// </summary>
//		Static = (int)global::Hbm.Api.SensorDB.Enums.Signal2Type.Static,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.SignalType"/>.
//	/// </summary>
//	public enum HbmCoreSignalType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.SignalType.StandardSignal"/>.
//		/// </summary>
//		StandardSignal = (int)global::Hbm.Api.Mgc.Enums.SignalType.StandardSignal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.SignalType.ModeSignal"/>.
//		/// </summary>
//		ModeSignal = (int)global::Hbm.Api.Mgc.Enums.SignalType.ModeSignal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.SignalType.ModeDependentSignal"/>.
//		/// </summary>
//		ModeDependentSignal = (int)global::Hbm.Api.Mgc.Enums.SignalType.ModeDependentSignal,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.SsiCodingType"/>.
//	/// </summary>
//	public enum HbmCoreSsiCodingType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SsiCodingType.Gray"/>.
//		/// </summary>
//		Gray = (int)global::Hbm.Api.SensorDB.Enums.SsiCodingType.Gray,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.SsiCodingType.Dual"/>.
//		/// </summary>
//		Dual = (int)global::Hbm.Api.SensorDB.Enums.SsiCodingType.Dual,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.StatusType"/>.
//	/// </summary>
//	[Flags]
//	public enum HbmCoreStatusType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.StatusType.None"/>.
//		/// </summary>
//		None = (int)global::Hbm.Api.Common.Enums.StatusType.None,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.StatusType.Overflow"/>.
//		/// </summary>
//		Overflow = (int)global::Hbm.Api.Common.Enums.StatusType.Overflow,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.StatusType.Teds"/>.
//		/// </summary>
//		Teds = (int)global::Hbm.Api.Common.Enums.StatusType.Teds,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.StatusType.Connected"/>.
//		/// </summary>
//		Connected = (int)global::Hbm.Api.Common.Enums.StatusType.Connected,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.StatusType.ParamBusy"/>.
//		/// </summary>
//		ParamBusy = (int)global::Hbm.Api.Common.Enums.StatusType.ParamBusy,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.StatusType.ConnectorShunt"/>.
//		/// </summary>
//		ConnectorShunt = (int)global::Hbm.Api.Common.Enums.StatusType.ConnectorShunt,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.StatusType.TestSignal"/>.
//		/// </summary>
//		TestSignal = (int)global::Hbm.Api.Common.Enums.StatusType.TestSignal,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.StatusType.DeviceBusy"/>.
//		/// </summary>
//		DeviceBusy = (int)global::Hbm.Api.Common.Enums.StatusType.DeviceBusy,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.StatusType.All"/>.
//		/// </summary>
//		All = (int)global::Hbm.Api.Common.Enums.StatusType.All,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.SynchronousMode"/>.
//	/// </summary>
//	public enum HbmCoreSynchronousMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SynchronousMode.Equidistant"/>.
//		/// </summary>
//		Equidistant = (int)global::Hbm.Api.Common.Enums.SynchronousMode.Equidistant,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SynchronousMode.NonEquidistant"/>.
//		/// </summary>
//		NonEquidistant = (int)global::Hbm.Api.Common.Enums.SynchronousMode.NonEquidistant,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.SyncModeType"/>.
//	/// </summary>
//	public enum HbmCoreSyncModeType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SyncModeType.Standalone"/>.
//		/// </summary>
//		Standalone = (int)global::Hbm.Api.Common.Enums.SyncModeType.Standalone,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SyncModeType.Slave"/>.
//		/// </summary>
//		Slave = (int)global::Hbm.Api.Common.Enums.SyncModeType.Slave,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.SyncModeType.Master"/>.
//		/// </summary>
//		Master = (int)global::Hbm.Api.Common.Enums.SyncModeType.Master,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.TedsUsageMode"/>.
//	/// </summary>
//	public enum HbmCoreTedsUsageMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsUsageMode.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.TedsUsageMode.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsUsageMode.Ignore"/>.
//		/// </summary>
//		Ignore = (int)global::Hbm.Api.Common.Enums.TedsUsageMode.Ignore,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsUsageMode.UseIfAvailable"/>.
//		/// </summary>
//		UseIfAvailable = (int)global::Hbm.Api.Common.Enums.TedsUsageMode.UseIfAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsUsageMode.UseRequired"/>.
//		/// </summary>
//		UseRequired = (int)global::Hbm.Api.Common.Enums.TedsUsageMode.UseRequired,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsUsageMode.UseManually"/>.
//		/// </summary>
//		UseManually = (int)global::Hbm.Api.Common.Enums.TedsUsageMode.UseManually,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType"/>.
//	/// </summary>
//	public enum HbmCoreTedsValidationValueType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.NotSupported"/>.
//		/// </summary>
//		NotSupported = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.NotSupported,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsNotAvailable"/>.
//		/// </summary>
//		TedsNotAvailable = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsNotAvailable,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsConnected"/>.
//		/// </summary>
//		TedsConnected = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsConnected,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsValid"/>.
//		/// </summary>
//		TedsValid = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsValid,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsChecksumError"/>.
//		/// </summary>
//		TedsChecksumError = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsChecksumError,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsDataEmpty"/>.
//		/// </summary>
//		TedsDataEmpty = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsDataEmpty,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsDataNotSupportedByConnector"/>.
//		/// </summary>
//		TedsDataNotSupportedByConnector = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsDataNotSupportedByConnector,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsDataParamBusy"/>.
//		/// </summary>
//		TedsDataParamBusy = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsDataParamBusy,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsDataNotUsed"/>.
//		/// </summary>
//		TedsDataNotUsed = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsDataNotUsed,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsValidRequestPressureEqualAmp"/>.
//		/// </summary>
//		TedsValidRequestPressureEqualAmp = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsValidRequestPressureEqualAmp,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsValidRequestPressureBelowAmp"/>.
//		/// </summary>
//		TedsValidRequestPressureBelowAmp = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsValidRequestPressureBelowAmp,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsValidRequestPressureAboveAmp"/>.
//		/// </summary>
//		TedsValidRequestPressureAboveAmp = (int)global::Hbm.Api.Common.Enums.TedsValidationValueType.TedsValidRequestPressureAboveAmp,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.TimeChannelType"/>.
//	/// </summary>
//	public enum HbmCoreTimeChannelType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.TimeChannelType.Ntp64BitTime"/>.
//		/// </summary>
//		Ntp64BitTime = (int)global::Hbm.Api.Mgc.Enums.TimeChannelType.Ntp64BitTime,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Common.Enums.TransportMode"/>.
//	/// </summary>
//	public enum HbmCoreTransportMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TransportMode.IEEE"/>.
//		/// </summary>
//		IEEE = (int)global::Hbm.Api.Common.Enums.TransportMode.IEEE,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TransportMode.UdpIpV4"/>.
//		/// </summary>
//		UdpIpV4 = (int)global::Hbm.Api.Common.Enums.TransportMode.UdpIpV4,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Common.Enums.TransportMode.UdpIpV6"/>.
//		/// </summary>
//		UdpIpV6 = (int)global::Hbm.Api.Common.Enums.TransportMode.UdpIpV6,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.Mgc.Enums.TriggerOperationMode"/>.
//	/// </summary>
//	public enum HbmCoreTriggerOperationMode
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.TriggerOperationMode.StandAlone"/>.
//		/// </summary>
//		StandAlone = (int)global::Hbm.Api.Mgc.Enums.TriggerOperationMode.StandAlone,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.TriggerOperationMode.TriggerSlave"/>.
//		/// </summary>
//		TriggerSlave = (int)global::Hbm.Api.Mgc.Enums.TriggerOperationMode.TriggerSlave,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.Mgc.Enums.TriggerOperationMode.TriggerMaster"/>.
//		/// </summary>
//		TriggerMaster = (int)global::Hbm.Api.Mgc.Enums.TriggerOperationMode.TriggerMaster,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.SensorDB.Enums.VoltageProbeType"/>.
//	/// </summary>
//	public enum HbmCoreVoltageProbeType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.VoltageProbeType.PassiveSingleEnded"/>.
//		/// </summary>
//		PassiveSingleEnded = (int)global::Hbm.Api.SensorDB.Enums.VoltageProbeType.PassiveSingleEnded,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.VoltageProbeType.PassiveSingleEndedWithIsolation"/>.
//		/// </summary>
//		PassiveSingleEndedWithIsolation = (int)global::Hbm.Api.SensorDB.Enums.VoltageProbeType.PassiveSingleEndedWithIsolation,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.VoltageProbeType.PassiveSingleEndedWithIsolationXT"/>.
//		/// </summary>
//		PassiveSingleEndedWithIsolationXT = (int)global::Hbm.Api.SensorDB.Enums.VoltageProbeType.PassiveSingleEndedWithIsolationXT,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.VoltageProbeType.PassiveDifferential"/>.
//		/// </summary>
//		PassiveDifferential = (int)global::Hbm.Api.SensorDB.Enums.VoltageProbeType.PassiveDifferential,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.VoltageProbeType.ActiveDifferential"/>.
//		/// </summary>
//		ActiveDifferential = (int)global::Hbm.Api.SensorDB.Enums.VoltageProbeType.ActiveDifferential,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.SensorDB.Enums.VoltageProbeType.CurrentWithVoltageOut"/>.
//		/// </summary>
//		CurrentWithVoltageOut = (int)global::Hbm.Api.SensorDB.Enums.VoltageProbeType.CurrentWithVoltageOut,

//	}

//	/// <summary>
//	/// Представляет замену перечисления <see cref="global::Hbm.Api.QuantumX.Enums.XmlViewType"/>.
//	/// </summary>
//	public enum HbmCoreXmlViewType
//	{
//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.XmlViewType.ComSettings"/>.
//		/// </summary>
//		ComSettings = (int)global::Hbm.Api.QuantumX.Enums.XmlViewType.ComSettings,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.XmlViewType.System"/>.
//		/// </summary>
//		System = (int)global::Hbm.Api.QuantumX.Enums.XmlViewType.System,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.XmlViewType.Connector"/>.
//		/// </summary>
//		Connector = (int)global::Hbm.Api.QuantumX.Enums.XmlViewType.Connector,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.XmlViewType.AdditionalFunction"/>.
//		/// </summary>
//		AdditionalFunction = (int)global::Hbm.Api.QuantumX.Enums.XmlViewType.AdditionalFunction,

//		/// <summary>
//		/// Представляет значение <see cref="global::Hbm.Api.QuantumX.Enums.XmlViewType.AdditionalConnector"/>.
//		/// </summary>
//		AdditionalConnector = (int)global::Hbm.Api.QuantumX.Enums.XmlViewType.AdditionalConnector,

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Adapter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreAdapter : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAdapter(global::Hbm.Api.Common.Entities.Adapter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Adapter Target { get; }

//		private HbmCoreAdapter CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Utils.Attributes.AdditionalValueAttribute"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreAdditionalValueAttribute : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAdditionalValueAttribute(global::Hbm.Api.Utils.Attributes.AdditionalValueAttribute target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Utils.Attributes.AdditionalValueAttribute Target { get; }

//		private HbmCoreAdditionalValueAttribute CreateInstance(global::System.String value)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Utils.Attributes.AdditionalValueTypeAttribute"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreAdditionalValueTypeAttribute : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAdditionalValueTypeAttribute(global::Hbm.Api.Utils.Attributes.AdditionalValueTypeAttribute target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Utils.Attributes.AdditionalValueTypeAttribute Target { get; }

//		private HbmCoreAdditionalValueTypeAttribute CreateInstance(global::System.Type valueType)
//		{
//			//Void .ctor(System.Type)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Channels.AnalogInChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreAnalogInChannel : HbmCoreChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAnalogInChannel(global::Hbm.Api.Common.Entities.Channels.AnalogInChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Channels.AnalogInChannel Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Connectors.AnalogInConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreAnalogInConnector : HbmCoreConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAnalogInConnector(global::Hbm.Api.Common.Entities.Connectors.AnalogInConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Connectors.AnalogInConnector Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.AnalogInSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreAnalogInSignal : HbmCoreSyncSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAnalogInSignal(global::Hbm.Api.Common.Entities.Signals.AnalogInSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.AnalogInSignal Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Channels.AnalogOutChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreAnalogOutChannel : HbmCoreChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAnalogOutChannel(global::Hbm.Api.Common.Entities.Channels.AnalogOutChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Channels.AnalogOutChannel Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Connectors.AnalogOutConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreAnalogOutConnector : HbmCoreConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAnalogOutConnector(global::Hbm.Api.Common.Entities.Connectors.AnalogOutConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Connectors.AnalogOutConnector Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.AnalogOutSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreAnalogOutSignal : HbmCoreSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAnalogOutSignal(global::Hbm.Api.Common.Entities.Signals.AnalogOutSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.AnalogOutSignal Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.AttachmentToLargeException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreAttachmentToLargeException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAttachmentToLargeException(global::Hbm.Api.SensorDB.Exceptions.AttachmentToLargeException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.AttachmentToLargeException Target { get; }

//		private HbmCoreAttachmentToLargeException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreAttachmentToLargeException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreAttachmentToLargeException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Utils.Exceptions.AttributeNotFoundException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreAttributeNotFoundException : HbmCoreUtilsException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAttributeNotFoundException(global::Hbm.Api.Utils.Exceptions.AttributeNotFoundException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Utils.Exceptions.AttributeNotFoundException Target { get; }

//		private HbmCoreAttributeNotFoundException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreAttributeNotFoundException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreAttributeNotFoundException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.TimeSources.AutoTimeSource"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreAutoTimeSource : HbmCoreTimeSource
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreAutoTimeSource(global::Hbm.Api.Common.Entities.TimeSources.AutoTimeSource target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.TimeSources.AutoTimeSource Target { get; }

//		private HbmCoreAutoTimeSource CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Filters.BesselFilter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreBesselFilter : HbmCoreFilter
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreBesselFilter(global::Hbm.Api.Common.Entities.Filters.BesselFilter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Filters.BesselFilter Target { get; }

//		private HbmCoreBesselFilter CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.BridgeSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreBridgeSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreBridgeSensor(global::Hbm.Api.SensorDB.Entities.Sensors.BridgeSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.BridgeSensor Target { get; }

//		private HbmCoreBridgeSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Filters.ButterworthFilter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreButterworthFilter : HbmCoreFilter
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreButterworthFilter(global::Hbm.Api.Common.Entities.Filters.ButterworthFilter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Filters.ButterworthFilter Target { get; }

//		private HbmCoreButterworthFilter CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Filters.ButterworthFirFilter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreButterworthFirFilter : HbmCoreFilter
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreButterworthFirFilter(global::Hbm.Api.Common.Entities.Filters.ButterworthFirFilter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Filters.ButterworthFirFilter Target { get; }

//		private HbmCoreButterworthFirFilter CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.CalibrationCertificateFileInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCalibrationCertificateFileInfo : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCalibrationCertificateFileInfo(global::Hbm.Api.Common.Entities.CalibrationCertificateFileInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.CalibrationCertificateFileInfo Target { get; }

//		private HbmCoreCalibrationCertificateFileInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.CanBusScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCanBusScaling : HbmCoreScaling
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanBusScaling(global::Hbm.Api.SensorDB.Entities.Scalings.CanBusScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.CanBusScaling Target { get; }

//		private HbmCoreCanBusScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreCanBusScaling CreateInstance(global::System.Decimal offset, global::System.Decimal factor, global::System.String engineeringUnit, global::System.Decimal minEngineeringRange, global::System.Decimal maxEngineeringRange, global::System.Boolean isEngineeringRangeRms)
//		{
//			//Void .ctor(System.Decimal, System.Decimal, System.String, System.Decimal, System.Decimal, Boolean)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.CanBusSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCanBusSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanBusSensor(global::Hbm.Api.SensorDB.Entities.Sensors.CanBusSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.CanBusSensor Target { get; }

//		private HbmCoreCanBusSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Connectors.CanConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreCanConnector : HbmCoreConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanConnector(global::Hbm.Api.Common.Entities.Connectors.CanConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Connectors.CanConnector Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Core.Entities.CanConnectorEcu"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCanConnectorEcu : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanConnectorEcu(global::Hbm.Api.QuantumX.Core.Entities.CanConnectorEcu target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Core.Entities.CanConnectorEcu Target { get; }

//		private HbmCoreCanConnectorEcu CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Core.Entities.CanConnectorEcuDbc"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCanConnectorEcuDbc : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanConnectorEcuDbc(global::Hbm.Api.QuantumX.Core.Entities.CanConnectorEcuDbc target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Core.Entities.CanConnectorEcuDbc Target { get; }

//		private HbmCoreCanConnectorEcuDbc CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Core.Entities.CanConnectorEcuSkb"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCanConnectorEcuSkb : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanConnectorEcuSkb(global::Hbm.Api.QuantumX.Core.Entities.CanConnectorEcuSkb target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Core.Entities.CanConnectorEcuSkb Target { get; }

//		private HbmCoreCanConnectorEcuSkb CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.CanFd"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCanFd : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanFd(global::Hbm.Api.Common.Entities.CanFd target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.CanFd Target { get; }

//		private HbmCoreCanFd CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Channels.CanInChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreCanInChannel : HbmCoreChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanInChannel(global::Hbm.Api.Common.Entities.Channels.CanInChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Channels.CanInChannel Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.CanInSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreCanInSignal : HbmCoreSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanInSignal(global::Hbm.Api.Common.Entities.Signals.CanInSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.CanInSignal Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Channels.CanOutChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreCanOutChannel : HbmCoreChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCanOutChannel(global::Hbm.Api.Common.Entities.Channels.CanOutChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Channels.CanOutChannel Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Category"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCategory : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCategory(global::Hbm.Api.SensorDB.Entities.Category target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Category Target { get; }

//		private HbmCoreCategory CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.CategoryHasChildsException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCategoryHasChildsException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCategoryHasChildsException(global::Hbm.Api.SensorDB.Exceptions.CategoryHasChildsException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.CategoryHasChildsException Target { get; }

//		private HbmCoreCategoryHasChildsException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreCategoryHasChildsException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreCategoryHasChildsException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.CategoryNotAssignedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCategoryNotAssignedException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCategoryNotAssignedException(global::Hbm.Api.SensorDB.Exceptions.CategoryNotAssignedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.CategoryNotAssignedException Target { get; }

//		private HbmCoreCategoryNotAssignedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreCategoryNotAssignedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreCategoryNotAssignedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Channels.Channel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreChannel : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannel(global::Hbm.Api.Common.Entities.Channels.Channel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Channels.Channel Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelChange"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreChannelChange : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelChange(global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelChange target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelChange Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Statuses.ChannelConnectedStatus"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreChannelConnectedStatus : HbmCoreDeviceStatus
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelConnectedStatus(global::Hbm.Api.Common.Entities.Statuses.ChannelConnectedStatus target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Statuses.ChannelConnectedStatus Target { get; }

//		private HbmCoreChannelConnectedStatus CreateInstance(global::System.Int32 originalId, HbmCoreChannelConnectedValueType value, HbmCoreChannel channel)
//		{
//			//Void .ctor(Int32, Hbm.Api.Common.Enums.ChannelConnectedValueType, Hbm.Api.Common.Entities.Channels.Channel)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Statuses.ChannelOverflowStatus"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreChannelOverflowStatus : HbmCoreDeviceStatus
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelOverflowStatus(global::Hbm.Api.Common.Entities.Statuses.ChannelOverflowStatus target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Statuses.ChannelOverflowStatus Target { get; }

//		private HbmCoreChannelOverflowStatus CreateInstance(global::System.Int32 originalId, HbmCoreChannelOverflowValueType value, HbmCoreChannel channel)
//		{
//			//Void .ctor(Int32, Hbm.Api.Common.Enums.ChannelOverflowValueType, Hbm.Api.Common.Entities.Channels.Channel)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelSensorValueChange"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreChannelSensorValueChange : HbmCoreChannelChange
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelSensorValueChange(global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelSensorValueChange target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelSensorValueChange Target { get; }

//		private HbmCoreChannelSensorValueChange CreateInstance(global::System.String channelId, global::System.Collections.Generic.List<global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentChannelSensor> dependentChannelsOldSensors)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[Hbm.Api.Common.Messaging.DataTransferObjects.DependentChannelSensor])
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.ChannelsEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreChannelsEventArgs : HbmCoreDeviceEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelsEventArgs(global::Hbm.Api.Common.Messaging.ChannelsEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.ChannelsEventArgs Target { get; }

//		private HbmCoreChannelsEventArgs CreateInstance(global::System.String uniqueDeviceID, global::System.Collections.Generic.List<global::System.String> uniqueChannelIDs, HbmCoreChangeReason reason)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[System.String], Hbm.Api.Common.Enums.ChangeReason)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.ChannelsStateChangedEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreChannelsStateChangedEventArgs : HbmCoreDeviceEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelsStateChangedEventArgs(global::Hbm.Api.Common.Messaging.ChannelsStateChangedEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.ChannelsStateChangedEventArgs Target { get; }

//		private HbmCoreChannelsStateChangedEventArgs CreateInstance(global::System.String uniqueDeviceId, global::System.Collections.Generic.List<global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelStateChange> channelsStateChanges)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[Hbm.Api.Common.Messaging.DataTransferObjects.ChannelStateChange])
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelStateChange"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreChannelStateChange : HbmCoreChannelChange
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelStateChange(global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelStateChange target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelStateChange Target { get; }

//		private HbmCoreChannelStateChange CreateInstance(global::System.String channelId, global::System.Nullable<global::System.Boolean> isConnected, global::System.Nullable<global::System.Boolean> isInOverflow)
//		{
//			//Void .ctor(System.String, System.Nullable`1[System.Boolean], System.Nullable`1[System.Boolean])
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.ChannelsTedsStatusEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreChannelsTedsStatusEventArgs : HbmCoreDeviceEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelsTedsStatusEventArgs(global::Hbm.Api.Common.Messaging.ChannelsTedsStatusEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.ChannelsTedsStatusEventArgs Target { get; }

//		private HbmCoreChannelsTedsStatusEventArgs CreateInstance(global::System.String uniqueDeviceID, global::System.Collections.Generic.List<global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelTedsChange> tedsValidationValueTypes, HbmCoreChangeReason reason)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[Hbm.Api.Common.Messaging.DataTransferObjects.ChannelTedsChange], Hbm.Api.Common.Enums.ChangeReason)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelTedsChange"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreChannelTedsChange : HbmCoreChannelChange
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelTedsChange(global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelTedsChange target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelTedsChange Target { get; }

//		private HbmCoreChannelTedsChange CreateInstance(global::System.String channelId, HbmCoreTedsValidationValueType tedsValidationValueType)
//		{
//			//Void .ctor(System.String, Hbm.Api.Common.Enums.TedsValidationValueType)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Statuses.ChannelTedsStatus"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreChannelTedsStatus : HbmCoreDeviceStatus
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreChannelTedsStatus(global::Hbm.Api.Common.Entities.Statuses.ChannelTedsStatus target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Statuses.ChannelTedsStatus Target { get; }

//		private HbmCoreChannelTedsStatus CreateInstance(global::System.Int32 originalID, HbmCoreTedsValidationValueType value, HbmCoreChannel channel)
//		{
//			//Void .ctor(Int32, Hbm.Api.Common.Enums.TedsValidationValueType, Hbm.Api.Common.Entities.Channels.Channel)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.CommonAPIException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreCommonAPIException : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCommonAPIException(global::Hbm.Api.Common.Exceptions.CommonAPIException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.CommonAPIException Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.CommunicationFailedError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCommunicationFailedError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCommunicationFailedError(global::Hbm.Api.Common.Entities.Problems.CommunicationFailedError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.CommunicationFailedError Target { get; }

//		private HbmCoreCommunicationFailedError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.Exception exception, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.Exception, System.String)
//			return null;
//		}

//		private HbmCoreCommunicationFailedError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreCommunicationFailedError CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType, global::System.Exception exception, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType, System.Exception, System.String)
//			return null;
//		}

//		private HbmCoreCommunicationFailedError CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType, global::System.Exception exception, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType, System.Exception, System.String)
//			return null;
//		}

//		private HbmCoreCommunicationFailedError CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType, global::System.Exception exception, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType, System.Exception, System.String)
//			return null;
//		}

//		private HbmCoreCommunicationFailedError CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType, global::System.Exception exception, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType, System.Exception, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.CommunicationFailedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCommunicationFailedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCommunicationFailedException(global::Hbm.Api.Common.Exceptions.CommunicationFailedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.CommunicationFailedException Target { get; }

//		private HbmCoreCommunicationFailedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreCommunicationFailedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreCommunicationFailedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.ConnectionFailedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreConnectionFailedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreConnectionFailedException(global::Hbm.Api.Common.Exceptions.ConnectionFailedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.ConnectionFailedException Target { get; }

//		private HbmCoreConnectionFailedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreConnectionFailedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreConnectionFailedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.ConnectionInfos.ConnectionInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreConnectionInfo : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreConnectionInfo(global::Hbm.Api.Common.Entities.ConnectionInfos.ConnectionInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.ConnectionInfos.ConnectionInfo Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.ConnectionNotSupportedError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreConnectionNotSupportedError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreConnectionNotSupportedError(global::Hbm.Api.Common.Entities.Problems.ConnectionNotSupportedError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.ConnectionNotSupportedError Target { get; }

//		private HbmCoreConnectionNotSupportedError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, HbmCoreConnectionInfo connectionInfo)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, Hbm.Api.Common.Entities.ConnectionInfos.ConnectionInfo)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Connectors.Connector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreConnector : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreConnector(global::Hbm.Api.Common.Entities.Connectors.Connector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Connectors.Connector Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Statuses.ConnectorParamBusyStatus"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreConnectorParamBusyStatus : HbmCoreDeviceStatus
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreConnectorParamBusyStatus(global::Hbm.Api.Common.Entities.Statuses.ConnectorParamBusyStatus target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Statuses.ConnectorParamBusyStatus Target { get; }

//		private HbmCoreConnectorParamBusyStatus CreateInstance(global::System.Int32 originalID, HbmCoreConnectorParamBusyValueType value, HbmCoreConnector connector)
//		{
//			//Void .ctor(Int32, Hbm.Api.Common.Enums.ConnectorParamBusyValueType, Hbm.Api.Common.Entities.Connectors.Connector)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Utils.Exceptions.ConversionFailedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreConversionFailedException : HbmCoreUtilsException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreConversionFailedException(global::Hbm.Api.Utils.Exceptions.ConversionFailedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Utils.Exceptions.ConversionFailedException Target { get; }

//		private HbmCoreConversionFailedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreConversionFailedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreConversionFailedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.CounterSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCounterSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCounterSensor(global::Hbm.Api.SensorDB.Entities.Sensors.CounterSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.CounterSensor Target { get; }

//		private HbmCoreCounterSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.CurrentProbeSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCurrentProbeSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCurrentProbeSensor(global::Hbm.Api.SensorDB.Entities.Sensors.CurrentProbeSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.CurrentProbeSensor Target { get; }

//		private HbmCoreCurrentProbeSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.CurrentSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreCurrentSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreCurrentSensor(global::Hbm.Api.SensorDB.Entities.Sensors.CurrentSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.CurrentSensor Target { get; }

//		private HbmCoreCurrentSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.DaqAlreadyStartedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDaqAlreadyStartedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDaqAlreadyStartedException(global::Hbm.Api.Common.Exceptions.DaqAlreadyStartedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.DaqAlreadyStartedException Target { get; }

//		private HbmCoreDaqAlreadyStartedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDaqAlreadyStartedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreDaqAlreadyStartedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.DaqEnvironment"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDaqEnvironment : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDaqEnvironment(global::Hbm.Api.Common.DaqEnvironment target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.DaqEnvironment Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.DaqMeasurement"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDaqMeasurement : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDaqMeasurement(global::Hbm.Api.Common.DaqMeasurement target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.DaqMeasurement Target { get; }

//		private HbmCoreDaqMeasurement CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDaqMeasurement CreateInstance(global::System.Int32 dataFetchingInterval, global::System.Int32 signalMaxTransmissionTime)
//		{
//			//Void .ctor(Int32, Int32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.DaqNoSignalsAddedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDaqNoSignalsAddedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDaqNoSignalsAddedException(global::Hbm.Api.Common.Exceptions.DaqNoSignalsAddedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.DaqNoSignalsAddedException Target { get; }

//		private HbmCoreDaqNoSignalsAddedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDaqNoSignalsAddedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreDaqNoSignalsAddedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.DaqNoSignalsPreparedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDaqNoSignalsPreparedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDaqNoSignalsPreparedException(global::Hbm.Api.Common.Exceptions.DaqNoSignalsPreparedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.DaqNoSignalsPreparedException Target { get; }

//		private HbmCoreDaqNoSignalsPreparedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDaqNoSignalsPreparedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreDaqNoSignalsPreparedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.DaqNotStartedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDaqNotStartedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDaqNotStartedException(global::Hbm.Api.Common.Exceptions.DaqNotStartedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.DaqNotStartedException Target { get; }

//		private HbmCoreDaqNotStartedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDaqNotStartedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreDaqNotStartedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.DaqStartFailedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDaqStartFailedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDaqStartFailedException(global::Hbm.Api.Common.Exceptions.DaqStartFailedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.DaqStartFailedException Target { get; }

//		private HbmCoreDaqStartFailedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDaqStartFailedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreDaqStartFailedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentChannelSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDependentChannelSensor : HbmCoreChannelChange
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDependentChannelSensor(global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentChannelSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentChannelSensor Target { get; }

//		private HbmCoreDependentChannelSensor CreateInstance(global::System.String channelId, HbmCoreSensor sensor)
//		{
//			//Void .ctor(System.String, Hbm.Api.SensorDB.Entities.Sensor)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalFilter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDependentSignalFilter : HbmCoreSignalChange
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDependentSignalFilter(global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalFilter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalFilter Target { get; }

//		private HbmCoreDependentSignalFilter CreateInstance(global::System.String signalId, HbmCoreFilter signalFilter)
//		{
//			//Void .ctor(System.String, Hbm.Api.Common.Entities.Filters.Filter)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalSampleRate"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDependentSignalSampleRate : HbmCoreSignalChange
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDependentSignalSampleRate(global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalSampleRate target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalSampleRate Target { get; }

//		private HbmCoreDependentSignalSampleRate CreateInstance(global::System.String signalId, global::System.Decimal sampleRate)
//		{
//			//Void .ctor(System.String, System.Decimal)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Descriptor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDescriptor : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDescriptor(global::Hbm.Api.SensorDB.Entities.Descriptor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Descriptor Target { get; }

//		private HbmCoreDescriptor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.DetectionRange"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDetectionRange : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDetectionRange(global::Hbm.Api.Common.Entities.DetectionRange target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.DetectionRange Target { get; }

//		private HbmCoreDetectionRange CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Device"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreDevice : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDevice(global::Hbm.Api.Common.Entities.Device target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Device Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.DeviceAlreadyConnectedWarning"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceAlreadyConnectedWarning : HbmCoreWarning
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceAlreadyConnectedWarning(global::Hbm.Api.Common.Entities.Problems.DeviceAlreadyConnectedWarning target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.DeviceAlreadyConnectedWarning Target { get; }

//		private HbmCoreDeviceAlreadyConnectedWarning CreateInstance(HbmCoreDevice device)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.DeviceBusyStateChange"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceBusyStateChange : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceBusyStateChange(global::Hbm.Api.Common.Messaging.DataTransferObjects.DeviceBusyStateChange target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.DeviceBusyStateChange Target { get; }

//		private HbmCoreDeviceBusyStateChange CreateInstance(global::System.String deviceId, global::System.Boolean isBusy)
//		{
//			//Void .ctor(System.String, Boolean)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DeviceBusyStateChangedEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceBusyStateChangedEventArgs : HbmCoreDeviceEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceBusyStateChangedEventArgs(global::Hbm.Api.Common.Messaging.DeviceBusyStateChangedEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DeviceBusyStateChangedEventArgs Target { get; }

//		private HbmCoreDeviceBusyStateChangedEventArgs CreateInstance(global::System.String uniqueDeviceId, HbmCoreDeviceBusyStateChange deviceBusyStateChange)
//		{
//			//Void .ctor(System.String, Hbm.Api.Common.Messaging.DataTransferObjects.DeviceBusyStateChange)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Statuses.DeviceBusyStatus"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceBusyStatus : HbmCoreDeviceStatus
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceBusyStatus(global::Hbm.Api.Common.Entities.Statuses.DeviceBusyStatus target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Statuses.DeviceBusyStatus Target { get; }

//		private HbmCoreDeviceBusyStatus CreateInstance(global::System.Int32 originalId, HbmCoreDeviceBusyValueType value)
//		{
//			//Void .ctor(Int32, Hbm.Api.Common.Enums.DeviceBusyValueType)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.DeviceErrorStatus"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceErrorStatus : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceErrorStatus(global::Hbm.Api.Common.Entities.DeviceErrorStatus target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.DeviceErrorStatus Target { get; }

//		private HbmCoreDeviceErrorStatus CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDeviceErrorStatus CreateInstance(global::System.Int32 errorCode, HbmCoreConnector connector, HbmCoreChannel channel, HbmCoreSignal signal, global::System.String message)
//		{
//			//Void .ctor(Int32, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Entities.Signals.Signal, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DeviceEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceEventArgs : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceEventArgs(global::Hbm.Api.Common.Messaging.DeviceEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DeviceEventArgs Target { get; }

//		private HbmCoreDeviceEventArgs CreateInstance(global::System.String uniqueDeviceID, HbmCoreChangeReason reason)
//		{
//			//Void .ctor(System.String, Hbm.Api.Common.Enums.ChangeReason)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.DeviceFamily"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreDeviceFamily : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceFamily(global::Hbm.Api.Common.Entities.DeviceFamily target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.DeviceFamily Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.DeviceFamilyAlreadyExistsException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceFamilyAlreadyExistsException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceFamilyAlreadyExistsException(global::Hbm.Api.Common.Exceptions.DeviceFamilyAlreadyExistsException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.DeviceFamilyAlreadyExistsException Target { get; }

//		private HbmCoreDeviceFamilyAlreadyExistsException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDeviceFamilyAlreadyExistsException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreDeviceFamilyAlreadyExistsException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.DeviceFamilyNotFoundException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceFamilyNotFoundException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceFamilyNotFoundException(global::Hbm.Api.Common.Exceptions.DeviceFamilyNotFoundException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.DeviceFamilyNotFoundException Target { get; }

//		private HbmCoreDeviceFamilyNotFoundException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDeviceFamilyNotFoundException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreDeviceFamilyNotFoundException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DeviceNameEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceNameEventArgs : HbmCoreDeviceEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceNameEventArgs(global::Hbm.Api.Common.Messaging.DeviceNameEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DeviceNameEventArgs Target { get; }

//		private HbmCoreDeviceNameEventArgs CreateInstance(global::System.String uniqueDeviceID, global::System.String oldName, global::System.String newName, HbmCoreChangeReason reason)
//		{
//			//Void .ctor(System.String, System.String, System.String, Hbm.Api.Common.Enums.ChangeReason)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.DeviceNotConnectedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDeviceNotConnectedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceNotConnectedException(global::Hbm.Api.Common.Exceptions.DeviceNotConnectedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.DeviceNotConnectedException Target { get; }

//		private HbmCoreDeviceNotConnectedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDeviceNotConnectedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreDeviceNotConnectedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.DeviceStatus"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreDeviceStatus : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDeviceStatus(global::Hbm.Api.Common.Entities.DeviceStatus target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.DeviceStatus Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Channels.DigitalChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreDigitalChannel : HbmCoreChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDigitalChannel(global::Hbm.Api.Common.Entities.Channels.DigitalChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Channels.DigitalChannel Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.DigitalCompressedGroupSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDigitalCompressedGroupSignal : HbmCoreDigitalSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDigitalCompressedGroupSignal(global::Hbm.Api.Common.Entities.Signals.DigitalCompressedGroupSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.DigitalCompressedGroupSignal Target { get; }

//		private HbmCoreDigitalCompressedGroupSignal CreateInstance(global::System.Int32 groupNo, global::System.Collections.Generic.List<global::Hbm.Api.Common.Entities.Signals.DigitalCompressedSignal> compressedDigitalSignals)
//		{
//			//Void .ctor(Int32, System.Collections.Generic.List`1[Hbm.Api.Common.Entities.Signals.DigitalCompressedSignal])
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.DigitalCompressedSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreDigitalCompressedSignal : HbmCoreDigitalSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDigitalCompressedSignal(global::Hbm.Api.Common.Entities.Signals.DigitalCompressedSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.DigitalCompressedSignal Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Connectors.DigitalConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreDigitalConnector : HbmCoreConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDigitalConnector(global::Hbm.Api.Common.Entities.Connectors.DigitalConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Connectors.DigitalConnector Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.DigitalSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreDigitalSignal : HbmCoreSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDigitalSignal(global::Hbm.Api.Common.Entities.Signals.DigitalSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.DigitalSignal Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Utils.Exceptions.DuplicateAttributeException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreDuplicateAttributeException : HbmCoreUtilsException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreDuplicateAttributeException(global::Hbm.Api.Utils.Exceptions.DuplicateAttributeException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Utils.Exceptions.DuplicateAttributeException Target { get; }

//		private HbmCoreDuplicateAttributeException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreDuplicateAttributeException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreDuplicateAttributeException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.Error"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreError : HbmCoreProblem
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreError(global::Hbm.Api.Common.Entities.Problems.Error target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.Error Target { get; }

//		private HbmCoreError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreError CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreError CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreError CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreError CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.SpectrumInfos.EtalonSpectrumInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreEtalonSpectrumInfo : HbmCoreSpectrumInfo
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreEtalonSpectrumInfo(global::Hbm.Api.Common.Entities.SpectrumInfos.EtalonSpectrumInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.SpectrumInfos.EtalonSpectrumInfo Target { get; }

//		private HbmCoreEtalonSpectrumInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.TimeSources.EtherCatTimeSource"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreEtherCatTimeSource : HbmCoreTimeSource
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreEtherCatTimeSource(global::Hbm.Api.Common.Entities.TimeSources.EtherCatTimeSource target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.TimeSources.EtherCatTimeSource Target { get; }

//		private HbmCoreEtherCatTimeSource CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.ConnectionInfos.EthernetConnectionInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreEthernetConnectionInfo : HbmCoreConnectionInfo
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreEthernetConnectionInfo(global::Hbm.Api.Common.Entities.ConnectionInfos.EthernetConnectionInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.ConnectionInfos.EthernetConnectionInfo Target { get; }

//		private HbmCoreEthernetConnectionInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreEthernetConnectionInfo CreateInstance(global::System.String ipAddress, global::System.Int32 port)
//		{
//			//Void .ctor(System.String, Int32)
//			return null;
//		}

//		private HbmCoreEthernetConnectionInfo CreateInstance(global::System.String ipAddress, global::System.Int32 port, global::System.String subnetMask)
//		{
//			//Void .ctor(System.String, Int32, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.ExtendedSampleRateModeError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreExtendedSampleRateModeError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreExtendedSampleRateModeError(global::Hbm.Api.Common.Entities.Problems.ExtendedSampleRateModeError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.ExtendedSampleRateModeError Target { get; }

//		private HbmCoreExtendedSampleRateModeError CreateInstance(HbmCoreDevice device, global::System.String errorCodeKey)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, System.String)
//			return null;
//		}

//		private HbmCoreExtendedSampleRateModeError CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, global::System.String errorCodeKey)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.FallbackLanguageDeleteException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFallbackLanguageDeleteException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFallbackLanguageDeleteException(global::Hbm.Api.SensorDB.Exceptions.FallbackLanguageDeleteException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.FallbackLanguageDeleteException Target { get; }

//		private HbmCoreFallbackLanguageDeleteException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreFallbackLanguageDeleteException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreFallbackLanguageDeleteException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.FamilyNameDuplicateException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFamilyNameDuplicateException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFamilyNameDuplicateException(global::Hbm.Api.Common.Exceptions.FamilyNameDuplicateException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.FamilyNameDuplicateException Target { get; }

//		private HbmCoreFamilyNameDuplicateException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreFamilyNameDuplicateException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreFamilyNameDuplicateException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.FamilyNameNotSetException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFamilyNameNotSetException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFamilyNameNotSetException(global::Hbm.Api.Common.Exceptions.FamilyNameNotSetException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.FamilyNameNotSetException Target { get; }

//		private HbmCoreFamilyNameNotSetException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreFamilyNameNotSetException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreFamilyNameNotSetException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.FbgAccelerometerSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFbgAccelerometerSensor : HbmCoreFbgSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgAccelerometerSensor(global::Hbm.Api.SensorDB.Entities.Sensors.FbgAccelerometerSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.FbgAccelerometerSensor Target { get; }

//		private HbmCoreFbgAccelerometerSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Channels.FbgChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreFbgChannel : HbmCoreChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgChannel(global::Hbm.Api.Common.Entities.Channels.FbgChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Channels.FbgChannel Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.FbgChannelsUpdatedEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFbgChannelsUpdatedEventArgs : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgChannelsUpdatedEventArgs(global::Hbm.Api.Common.Messaging.FbgChannelsUpdatedEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.FbgChannelsUpdatedEventArgs Target { get; }

//		private HbmCoreFbgChannelsUpdatedEventArgs CreateInstance(global::System.String uniqueDeviceId, global::System.String uniqueConnectorId, global::System.String uniqueChannelId)
//		{
//			//Void .ctor(System.String, System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Connectors.FbgConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreFbgConnector : HbmCoreConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgConnector(global::Hbm.Api.Common.Entities.Connectors.FbgConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Connectors.FbgConnector Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.FbgGenericSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFbgGenericSensor : HbmCoreFbgSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgGenericSensor(global::Hbm.Api.SensorDB.Entities.Sensors.FbgGenericSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.FbgGenericSensor Target { get; }

//		private HbmCoreFbgGenericSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.FbgCalibration.FbgMultiFactorCalibration"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFbgMultiFactorCalibration : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgMultiFactorCalibration(global::Hbm.Api.SensorDB.Entities.FbgCalibration.FbgMultiFactorCalibration target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.FbgCalibration.FbgMultiFactorCalibration Target { get; }

//		private HbmCoreFbgMultiFactorCalibration CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.FbgSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreFbgSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgSensor(global::Hbm.Api.SensorDB.Entities.Sensors.FbgSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.FbgSensor Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.FbgSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreFbgSignal : HbmCoreSyncSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgSignal(global::Hbm.Api.Common.Entities.Signals.FbgSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.FbgSignal Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.FbgCalibration.FbgSingleFactorCalibration"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFbgSingleFactorCalibration : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgSingleFactorCalibration(global::Hbm.Api.SensorDB.Entities.FbgCalibration.FbgSingleFactorCalibration target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.FbgCalibration.FbgSingleFactorCalibration Target { get; }

//		private HbmCoreFbgSingleFactorCalibration CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.FbgStrainGaugeSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFbgStrainGaugeSensor : HbmCoreFbgSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgStrainGaugeSensor(global::Hbm.Api.SensorDB.Entities.Sensors.FbgStrainGaugeSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.FbgStrainGaugeSensor Target { get; }

//		private HbmCoreFbgStrainGaugeSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.FbgThermocoupleSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFbgThermocoupleSensor : HbmCoreFbgSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgThermocoupleSensor(global::Hbm.Api.SensorDB.Entities.Sensors.FbgThermocoupleSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.FbgThermocoupleSensor Target { get; }

//		private HbmCoreFbgThermocoupleSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.FbgWavelengthSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFbgWavelengthSensor : HbmCoreFbgSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFbgWavelengthSensor(global::Hbm.Api.SensorDB.Entities.Sensors.FbgWavelengthSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.FbgWavelengthSensor Target { get; }

//		private HbmCoreFbgWavelengthSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.SpectrumInfos.FiberSpectrumInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFiberSpectrumInfo : HbmCoreSpectrumInfo
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFiberSpectrumInfo(global::Hbm.Api.Common.Entities.SpectrumInfos.FiberSpectrumInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.SpectrumInfos.FiberSpectrumInfo Target { get; }

//		private HbmCoreFiberSpectrumInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Filters.Filter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreFilter : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFilter(global::Hbm.Api.Common.Entities.Filters.Filter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Filters.Filter Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.FiltersEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFiltersEventArgs : HbmCoreSignalsEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFiltersEventArgs(global::Hbm.Api.Common.Messaging.FiltersEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.FiltersEventArgs Target { get; }

//		private HbmCoreFiltersEventArgs CreateInstance(global::System.String uniqueDeviceID, global::System.Collections.Generic.List<global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalFilterChange> oldFiltersByUniqueSignalIDs, global::System.Collections.Generic.List<global::System.String> uniqueSignalIDs, HbmCoreChangeReason reason)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[Hbm.Api.Common.Messaging.DataTransferObjects.SignalFilterChange], System.Collections.Generic.List`1[System.String], Hbm.Api.Common.Enums.ChangeReason)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.FirmwareImageVersionCallbackEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFirmwareImageVersionCallbackEventArgs : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFirmwareImageVersionCallbackEventArgs(global::Hbm.Api.Common.Messaging.FirmwareImageVersionCallbackEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.FirmwareImageVersionCallbackEventArgs Target { get; }

//		private HbmCoreFirmwareImageVersionCallbackEventArgs CreateInstance(global::System.String imageVersion)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.FirmwareUpdateFailedError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFirmwareUpdateFailedError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFirmwareUpdateFailedError(global::Hbm.Api.Common.Entities.Problems.FirmwareUpdateFailedError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.FirmwareUpdateFailedError Target { get; }

//		private HbmCoreFirmwareUpdateFailedError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.FirmwareUpdater"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFirmwareUpdater : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFirmwareUpdater(global::Hbm.Api.Common.Entities.FirmwareUpdater target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.FirmwareUpdater Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.FirmwareUpdateStatusEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFirmwareUpdateStatusEventArgs : HbmCoreDeviceEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFirmwareUpdateStatusEventArgs(global::Hbm.Api.Common.Messaging.FirmwareUpdateStatusEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.FirmwareUpdateStatusEventArgs Target { get; }

//		private HbmCoreFirmwareUpdateStatusEventArgs CreateInstance(global::System.String overallProgress, global::System.String taskName, global::System.String uniqueDeviceID, global::System.String deviceName, global::System.String error, global::System.String taskProgress, global::System.Boolean updateProcessFinished, global::System.Boolean rebooting, HbmCoreChangeReason reason)
//		{
//			//Void .ctor(System.String, System.String, System.String, System.String, System.String, System.String, Boolean, Boolean, Hbm.Api.Common.Enums.ChangeReason)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Exceptions.FrameworkDllException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFrameworkDllException : HbmCoreQuantumXException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFrameworkDllException(global::Hbm.Api.QuantumX.Exceptions.FrameworkDllException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Exceptions.FrameworkDllException Target { get; }

//		private HbmCoreFrameworkDllException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreFrameworkDllException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreFrameworkDllException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.FrequencySensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreFrequencySensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreFrequencySensor(global::Hbm.Api.SensorDB.Entities.Sensors.FrequencySensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.FrequencySensor Target { get; }

//		private HbmCoreFrequencySensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.GageFactorScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreGageFactorScaling : HbmCoreScaling
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreGageFactorScaling(global::Hbm.Api.SensorDB.Entities.Scalings.GageFactorScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.GageFactorScaling Target { get; }

//		private HbmCoreGageFactorScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.SpectrumInfos.GasCellSpectrumInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreGasCellSpectrumInfo : HbmCoreSpectrumInfo
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreGasCellSpectrumInfo(global::Hbm.Api.Common.Entities.SpectrumInfos.GasCellSpectrumInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.SpectrumInfos.GasCellSpectrumInfo Target { get; }

//		private HbmCoreGasCellSpectrumInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.GenericStreaming.GenericStreamingDevice"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreGenericStreamingDevice : HbmCoreStreamingDevice
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreGenericStreamingDevice(global::Hbm.Api.GenericStreaming.GenericStreamingDevice target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.GenericStreaming.GenericStreamingDevice Target { get; }

//		private HbmCoreGenericStreamingDevice CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.GenericStreaming.GenericStreamingDeviceFamily"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreGenericStreamingDeviceFamily : HbmCoreDeviceFamily
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreGenericStreamingDeviceFamily(global::Hbm.Api.GenericStreaming.GenericStreamingDeviceFamily target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.GenericStreaming.GenericStreamingDeviceFamily Target { get; }

//		private HbmCoreGenericStreamingDeviceFamily CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.GenericStreaming.SkillReaders.GenericStreamingFileSkillReader"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreGenericStreamingFileSkillReader : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreGenericStreamingFileSkillReader(global::Hbm.Api.GenericStreaming.SkillReaders.GenericStreamingFileSkillReader target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.GenericStreaming.SkillReaders.GenericStreamingFileSkillReader Target { get; }

//		private HbmCoreGenericStreamingFileSkillReader CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.GenericStreaming.Channels.GenericStreamingVirtualChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreGenericStreamingVirtualChannel : HbmCoreVirtualChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreGenericStreamingVirtualChannel(global::Hbm.Api.GenericStreaming.Channels.GenericStreamingVirtualChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.GenericStreaming.Channels.GenericStreamingVirtualChannel Target { get; }

//		private HbmCoreGenericStreamingVirtualChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.GenericStreaming.Connectors.GenericStreamingVirtualConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreGenericStreamingVirtualConnector : HbmCoreVirtualConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreGenericStreamingVirtualConnector(global::Hbm.Api.GenericStreaming.Connectors.GenericStreamingVirtualConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.GenericStreaming.Connectors.GenericStreamingVirtualConnector Target { get; }

//		private HbmCoreGenericStreamingVirtualConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.GenericStreaming.Signals.GenericStreamingVirtualSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreGenericStreamingVirtualSignal : HbmCoreVirtualSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreGenericStreamingVirtualSignal(global::Hbm.Api.GenericStreaming.Signals.GenericStreamingVirtualSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.GenericStreaming.Signals.GenericStreamingVirtualSignal Target { get; }

//		private HbmCoreGenericStreamingVirtualSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.HierarchicalCategory"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreHierarchicalCategory : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreHierarchicalCategory(global::Hbm.Api.SensorDB.Entities.HierarchicalCategory target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.HierarchicalCategory Target { get; }

//		private HbmCoreHierarchicalCategory CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreHierarchicalCategory CreateInstance(HbmCoreCategory category, global::System.Int32 hierarchicalLevel)
//		{
//			//Void .ctor(Hbm.Api.SensorDB.Entities.Category, Int32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Filters.HighpassFilter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreHighpassFilter : HbmCoreFilter
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreHighpassFilter(global::Hbm.Api.Common.Entities.Filters.HighpassFilter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Filters.HighpassFilter Target { get; }

//		private HbmCoreHighpassFilter CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.IepeSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreIepeSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreIepeSensor(global::Hbm.Api.SensorDB.Entities.Sensors.IepeSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.IepeSensor Target { get; }

//		private HbmCoreIepeSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Utils.Attributes.IgnoreInTmsAttribute"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreIgnoreInTmsAttribute : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreIgnoreInTmsAttribute(global::Hbm.Api.Utils.Attributes.IgnoreInTmsAttribute target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Utils.Attributes.IgnoreInTmsAttribute Target { get; }

//		private HbmCoreIgnoreInTmsAttribute CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Scan.Entities.Interface"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreInterface : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreInterface(global::Hbm.Api.Scan.Entities.Interface target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Scan.Entities.Interface Target { get; }

//		private HbmCoreInterface CreateInstance(global::System.String adapterMacAddress, global::System.String name, global::System.String type, global::System.String configurationMethod, global::System.String description, global::System.String routerUuid)
//		{
//			//Void .ctor(System.String, System.String, System.String, System.String, System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.InternalScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreInternalScaling : HbmCoreScaling
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreInternalScaling(global::Hbm.Api.SensorDB.Entities.Scalings.InternalScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.InternalScaling Target { get; }

//		private HbmCoreInternalScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.InvalidCANFileFormatException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreInvalidCANFileFormatException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreInvalidCANFileFormatException(global::Hbm.Api.SensorDB.Exceptions.InvalidCANFileFormatException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.InvalidCANFileFormatException Target { get; }

//		private HbmCoreInvalidCANFileFormatException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreInvalidCANFileFormatException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreInvalidCANFileFormatException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.InvalidPropertyOfDeviceError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreInvalidPropertyOfDeviceError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreInvalidPropertyOfDeviceError(global::Hbm.Api.Common.Entities.Problems.InvalidPropertyOfDeviceError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.InvalidPropertyOfDeviceError Target { get; }

//		private HbmCoreInvalidPropertyOfDeviceError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Scan.Entities.Ip4Address"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreIp4Address : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreIp4Address(global::Hbm.Api.Scan.Entities.Ip4Address target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Scan.Entities.Ip4Address Target { get; }

//		private HbmCoreIp4Address CreateInstance(global::System.String ipAddress, global::System.String subnetMask)
//		{
//			//Void .ctor(System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Scan.Entities.Ip6Address"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreIp6Address : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreIp6Address(global::Hbm.Api.Scan.Entities.Ip6Address target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Scan.Entities.Ip6Address Target { get; }

//		private HbmCoreIp6Address CreateInstance(global::System.String ipAddress, global::System.String subnetMask, global::System.UInt32 prefix)
//		{
//			//Void .ctor(System.String, System.String, UInt32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Language"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreLanguage : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreLanguage(global::Hbm.Api.SensorDB.Entities.Language target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Language Target { get; }

//		private HbmCoreLanguage CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreLanguage CreateInstance(global::System.String twoLetterISOLanguageName, global::System.String description)
//		{
//			//Void .ctor(System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.LanguageNotFoundException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreLanguageNotFoundException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreLanguageNotFoundException(global::Hbm.Api.SensorDB.Exceptions.LanguageNotFoundException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.LanguageNotFoundException Target { get; }

//		private HbmCoreLanguageNotFoundException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreLanguageNotFoundException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreLanguageNotFoundException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.LimitSwitch"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreLimitSwitch : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreLimitSwitch(global::Hbm.Api.Pmx.LimitSwitch target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.LimitSwitch Target { get; }

//		private HbmCoreLimitSwitch CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Filters.LinearPhaseFilter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreLinearPhaseFilter : HbmCoreFilter
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreLinearPhaseFilter(global::Hbm.Api.Common.Entities.Filters.LinearPhaseFilter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Filters.LinearPhaseFilter Target { get; }

//		private HbmCoreLinearPhaseFilter CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Logging.Logger.LogContext"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreLogContext : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreLogContext(global::Hbm.Api.Logging.Logger.LogContext target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Logging.Logger.LogContext Target { get; }

//		private HbmCoreLogContext CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreLogContext CreateInstance(global::System.String category)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Logging.Logger.LoggerBase"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreLoggerBase : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreLoggerBase(global::Hbm.Api.Logging.Logger.LoggerBase target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Logging.Logger.LoggerBase Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Logging.LoggerEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreLoggerEventArgs : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreLoggerEventArgs(global::Hbm.Api.Logging.LoggerEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Logging.LoggerEventArgs Target { get; }

//		private HbmCoreLoggerEventArgs CreateInstance(global::Hbm.Api.Logging.Logger.ILogger logger)
//		{
//			//Void .ctor(Hbm.Api.Logging.Logger.ILogger)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.LvdtSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreLvdtSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreLvdtSensor(global::Hbm.Api.SensorDB.Entities.Sensors.LvdtSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.LvdtSensor Target { get; }

//		private HbmCoreLvdtSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.MeasurementValue"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMeasurementValue : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMeasurementValue(global::Hbm.Api.Common.Entities.MeasurementValue target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.MeasurementValue Target { get; }

//		private HbmCoreMeasurementValue CreateInstance(global::System.Double value, global::System.Double timeStamp, HbmCoreMeasurementValueState state)
//		{
//			//Void .ctor(Double, Double, Hbm.Api.Common.Enums.MeasurementValueState)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.MeasurementValues"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMeasurementValues : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMeasurementValues(global::Hbm.Api.Common.Entities.MeasurementValues target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.MeasurementValues Target { get; }

//		private HbmCoreMeasurementValues CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.MessageBroker"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMessageBroker : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMessageBroker(global::Hbm.Api.Common.Messaging.MessageBroker target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.MessageBroker Target { get; }

//		private HbmCoreMessageBroker CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.MgcAdditionalFeatures"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcAdditionalFeatures : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcAdditionalFeatures(global::Hbm.Api.Mgc.MgcAdditionalFeatures target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.MgcAdditionalFeatures Target { get; }

//		private HbmCoreMgcAdditionalFeatures CreateInstance(HbmCoreMgcDevice mgcDevice)
//		{
//			//Void .ctor(Hbm.Api.Mgc.MgcDevice)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Channels.MgcAnalogInChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcAnalogInChannel : HbmCoreAnalogInChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcAnalogInChannel(global::Hbm.Api.Mgc.Channels.MgcAnalogInChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Channels.MgcAnalogInChannel Target { get; }

//		private HbmCoreMgcAnalogInChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Connectors.MgcAnalogInConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcAnalogInConnector : HbmCoreAnalogInConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcAnalogInConnector(global::Hbm.Api.Mgc.Connectors.MgcAnalogInConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Connectors.MgcAnalogInConnector Target { get; }

//		private HbmCoreMgcAnalogInConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Signals.MgcAnalogInSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcAnalogInSignal : HbmCoreAnalogInSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcAnalogInSignal(global::Hbm.Api.Mgc.Signals.MgcAnalogInSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Signals.MgcAnalogInSignal Target { get; }

//		private HbmCoreMgcAnalogInSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Channels.MgcAnalogOutChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcAnalogOutChannel : HbmCoreAnalogOutChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcAnalogOutChannel(global::Hbm.Api.Mgc.Channels.MgcAnalogOutChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Channels.MgcAnalogOutChannel Target { get; }

//		private HbmCoreMgcAnalogOutChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Connectors.MgcAnalogOutConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcAnalogOutConnector : HbmCoreAnalogOutConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcAnalogOutConnector(global::Hbm.Api.Mgc.Connectors.MgcAnalogOutConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Connectors.MgcAnalogOutConnector Target { get; }

//		private HbmCoreMgcAnalogOutConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Signals.MgcAnalogOutSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcAnalogOutSignal : HbmCoreAnalogOutSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcAnalogOutSignal(global::Hbm.Api.Mgc.Signals.MgcAnalogOutSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Signals.MgcAnalogOutSignal Target { get; }

//		private HbmCoreMgcAnalogOutSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Connectors.MgcCanConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcCanConnector : HbmCoreCanConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcCanConnector(global::Hbm.Api.Mgc.Connectors.MgcCanConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Connectors.MgcCanConnector Target { get; }

//		private HbmCoreMgcCanConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Channels.MgcCanInChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcCanInChannel : HbmCoreCanInChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcCanInChannel(global::Hbm.Api.Mgc.Channels.MgcCanInChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Channels.MgcCanInChannel Target { get; }

//		private HbmCoreMgcCanInChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Signals.MgcCanInSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcCanInSignal : HbmCoreCanInSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcCanInSignal(global::Hbm.Api.Mgc.Signals.MgcCanInSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Signals.MgcCanInSignal Target { get; }

//		private HbmCoreMgcCanInSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.MgcDevice"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcDevice : HbmCoreDevice
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcDevice(global::Hbm.Api.Mgc.MgcDevice target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.MgcDevice Target { get; }

//		private HbmCoreMgcDevice CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreMgcDevice CreateInstance(global::System.String ip4Address, global::System.Int32 port)
//		{
//			//Void .ctor(System.String, Int32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.MgcDeviceFamily"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcDeviceFamily : HbmCoreDeviceFamily
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcDeviceFamily(global::Hbm.Api.Mgc.MgcDeviceFamily target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.MgcDeviceFamily Target { get; }

//		private HbmCoreMgcDeviceFamily CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Channels.MgcDigitalChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcDigitalChannel : HbmCoreDigitalChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcDigitalChannel(global::Hbm.Api.Mgc.Channels.MgcDigitalChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Channels.MgcDigitalChannel Target { get; }

//		private HbmCoreMgcDigitalChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Connectors.MgcDigitalConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcDigitalConnector : HbmCoreDigitalConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcDigitalConnector(global::Hbm.Api.Mgc.Connectors.MgcDigitalConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Connectors.MgcDigitalConnector Target { get; }

//		private HbmCoreMgcDigitalConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Signals.MgcDigitalSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcDigitalSignal : HbmCoreDigitalCompressedSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcDigitalSignal(global::Hbm.Api.Mgc.Signals.MgcDigitalSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Signals.MgcDigitalSignal Target { get; }

//		private HbmCoreMgcDigitalSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.SkillReaders.MgcFileSkillReader"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcFileSkillReader : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcFileSkillReader(global::Hbm.Api.Mgc.SkillReaders.MgcFileSkillReader target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.SkillReaders.MgcFileSkillReader Target { get; }

//		private HbmCoreMgcFileSkillReader CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.MgcIndicationScalingValues"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcIndicationScalingValues : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcIndicationScalingValues(global::Hbm.Api.Mgc.MgcIndicationScalingValues target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.MgcIndicationScalingValues Target { get; }

//		private HbmCoreMgcIndicationScalingValues CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.MgcLimitSwitch"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcLimitSwitch : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcLimitSwitch(global::Hbm.Api.Mgc.MgcLimitSwitch target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.MgcLimitSwitch Target { get; }

//		private HbmCoreMgcLimitSwitch CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Channels.MgcVirtualChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcVirtualChannel : HbmCoreVirtualChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcVirtualChannel(global::Hbm.Api.Mgc.Channels.MgcVirtualChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Channels.MgcVirtualChannel Target { get; }

//		private HbmCoreMgcVirtualChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Connectors.MgcVirtualConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcVirtualConnector : HbmCoreVirtualConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcVirtualConnector(global::Hbm.Api.Mgc.Connectors.MgcVirtualConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Connectors.MgcVirtualConnector Target { get; }

//		private HbmCoreMgcVirtualConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Mgc.Signals.MgcVirtualSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreMgcVirtualSignal : HbmCoreVirtualSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreMgcVirtualSignal(global::Hbm.Api.Mgc.Signals.MgcVirtualSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Mgc.Signals.MgcVirtualSignal Target { get; }

//		private HbmCoreMgcVirtualSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Scan.Entities.NetworkAdapter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreNetworkAdapter : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreNetworkAdapter(global::Hbm.Api.Scan.Entities.NetworkAdapter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Scan.Entities.NetworkAdapter Target { get; }

//		private HbmCoreNetworkAdapter CreateInstance(global::System.UInt64 macAddress, global::System.String macAddressString, global::System.String name, global::System.String description, global::System.String friendlyName, global::System.Boolean active)
//		{
//			//Void .ctor(UInt64, System.String, System.String, System.String, System.String, Boolean)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Filters.NoFilter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreNoFilter : HbmCoreFilter
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreNoFilter(global::Hbm.Api.Common.Entities.Filters.NoFilter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Filters.NoFilter Target { get; }

//		private HbmCoreNoFilter CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Filters.NotSupportedFilter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreNotSupportedFilter : HbmCoreFilter
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreNotSupportedFilter(global::Hbm.Api.Common.Entities.Filters.NotSupportedFilter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Filters.NotSupportedFilter Target { get; }

//		private HbmCoreNotSupportedFilter CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Scalings.NotSupportedOutputScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreNotSupportedOutputScaling : HbmCoreOutputScaling
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreNotSupportedOutputScaling(global::Hbm.Api.Common.Entities.Scalings.NotSupportedOutputScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Scalings.NotSupportedOutputScaling Target { get; }

//		private HbmCoreNotSupportedOutputScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.NotSupportedScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreNotSupportedScaling : HbmCoreScaling
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreNotSupportedScaling(global::Hbm.Api.SensorDB.Entities.Scalings.NotSupportedScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.NotSupportedScaling Target { get; }

//		private HbmCoreNotSupportedScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.NotSupportedSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreNotSupportedSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreNotSupportedSensor(global::Hbm.Api.SensorDB.Entities.Sensors.NotSupportedSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.NotSupportedSensor Target { get; }

//		private HbmCoreNotSupportedSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.TimeSources.NotSupportedTimeSource"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreNotSupportedTimeSource : HbmCoreTimeSource
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreNotSupportedTimeSource(global::Hbm.Api.Common.Entities.TimeSources.NotSupportedTimeSource target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.TimeSources.NotSupportedTimeSource Target { get; }

//		private HbmCoreNotSupportedTimeSource CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.TimeSources.NtpTimeSource"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreNtpTimeSource : HbmCoreTimeSource
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreNtpTimeSource(global::Hbm.Api.Common.Entities.TimeSources.NtpTimeSource target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.TimeSources.NtpTimeSource Target { get; }

//		private HbmCoreNtpTimeSource CreateInstance(global::System.String serverIpAddress)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.OffScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreOffScaling : HbmCoreScaling
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreOffScaling(global::Hbm.Api.SensorDB.Entities.Scalings.OffScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.OffScaling Target { get; }

//		private HbmCoreOffScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.OperationFailedError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreOperationFailedError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreOperationFailedError(global::Hbm.Api.Common.Entities.Problems.OperationFailedError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.OperationFailedError Target { get; }

//		private HbmCoreOperationFailedError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreOperationFailedError CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreOperationFailedError CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreOperationFailedError CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreOperationFailedError CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Scalings.OutputScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreOutputScaling : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreOutputScaling(global::Hbm.Api.Common.Entities.Scalings.OutputScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Scalings.OutputScaling Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Scalings.OutputScalingPoint"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreOutputScalingPoint : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreOutputScalingPoint(global::Hbm.Api.Common.Entities.Scalings.OutputScalingPoint target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Scalings.OutputScalingPoint Target { get; }

//		private HbmCoreOutputScalingPoint CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreOutputScalingPoint CreateInstance(global::System.Decimal x, global::System.Decimal y)
//		{
//			//Void .ctor(System.Decimal, System.Decimal)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Scalings.OutputTableScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreOutputTableScaling : HbmCoreOutputScaling
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreOutputTableScaling(global::Hbm.Api.Common.Entities.Scalings.OutputTableScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Scalings.OutputTableScaling Target { get; }

//		private HbmCoreOutputTableScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreOutputTableScaling CreateInstance(HbmCoreTableScaling tableScaling)
//		{
//			//Void .ctor(Hbm.Api.SensorDB.Entities.Scalings.TableScaling)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.ParameterizationNotCompletedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreParameterizationNotCompletedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreParameterizationNotCompletedException(global::Hbm.Api.Common.Exceptions.ParameterizationNotCompletedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.ParameterizationNotCompletedException Target { get; }

//		private HbmCoreParameterizationNotCompletedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreParameterizationNotCompletedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreParameterizationNotCompletedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.PiezoPassiveSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePiezoPassiveSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePiezoPassiveSensor(global::Hbm.Api.SensorDB.Entities.Sensors.PiezoPassiveSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.PiezoPassiveSensor Target { get; }

//		private HbmCorePiezoPassiveSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.PmxAdditionalFeatures"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxAdditionalFeatures : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxAdditionalFeatures(global::Hbm.Api.Pmx.PmxAdditionalFeatures target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.PmxAdditionalFeatures Target { get; }

//		private HbmCorePmxAdditionalFeatures CreateInstance(HbmCorePmxDevice pmxDevice)
//		{
//			//Void .ctor(Hbm.Api.Pmx.PmxDevice)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Channels.PmxAnalogInChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxAnalogInChannel : HbmCoreAnalogInChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxAnalogInChannel(global::Hbm.Api.Pmx.Channels.PmxAnalogInChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Channels.PmxAnalogInChannel Target { get; }

//		private HbmCorePmxAnalogInChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Connectors.PmxAnalogInConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxAnalogInConnector : HbmCoreAnalogInConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxAnalogInConnector(global::Hbm.Api.Pmx.Connectors.PmxAnalogInConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Connectors.PmxAnalogInConnector Target { get; }

//		private HbmCorePmxAnalogInConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Signals.PmxAnalogInSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxAnalogInSignal : HbmCoreAnalogInSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxAnalogInSignal(global::Hbm.Api.Pmx.Signals.PmxAnalogInSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Signals.PmxAnalogInSignal Target { get; }

//		private HbmCorePmxAnalogInSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Channels.PmxAnalogOutChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxAnalogOutChannel : HbmCoreAnalogOutChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxAnalogOutChannel(global::Hbm.Api.Pmx.Channels.PmxAnalogOutChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Channels.PmxAnalogOutChannel Target { get; }

//		private HbmCorePmxAnalogOutChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Connectors.PmxAnalogOutConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxAnalogOutConnector : HbmCoreAnalogOutConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxAnalogOutConnector(global::Hbm.Api.Pmx.Connectors.PmxAnalogOutConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Connectors.PmxAnalogOutConnector Target { get; }

//		private HbmCorePmxAnalogOutConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Signals.PmxAnalogOutSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxAnalogOutSignal : HbmCoreAnalogOutSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxAnalogOutSignal(global::Hbm.Api.Pmx.Signals.PmxAnalogOutSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Signals.PmxAnalogOutSignal Target { get; }

//		private HbmCorePmxAnalogOutSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.PmxDevice"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxDevice : HbmCoreDevice
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxDevice(global::Hbm.Api.Pmx.PmxDevice target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.PmxDevice Target { get; }

//		private HbmCorePmxDevice CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCorePmxDevice CreateInstance(global::System.String ip4Address, global::System.Int32 port)
//		{
//			//Void .ctor(System.String, Int32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.PmxDeviceFamily"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxDeviceFamily : HbmCoreDeviceFamily
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxDeviceFamily(global::Hbm.Api.Pmx.PmxDeviceFamily target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.PmxDeviceFamily Target { get; }

//		private HbmCorePmxDeviceFamily CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Channels.PmxDigitalChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxDigitalChannel : HbmCoreDigitalChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxDigitalChannel(global::Hbm.Api.Pmx.Channels.PmxDigitalChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Channels.PmxDigitalChannel Target { get; }

//		private HbmCorePmxDigitalChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Connectors.PmxDigitalConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxDigitalConnector : HbmCoreDigitalConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxDigitalConnector(global::Hbm.Api.Pmx.Connectors.PmxDigitalConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Connectors.PmxDigitalConnector Target { get; }

//		private HbmCorePmxDigitalConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Signals.PmxDigitalSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxDigitalSignal : HbmCoreDigitalCompressedSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxDigitalSignal(global::Hbm.Api.Pmx.Signals.PmxDigitalSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Signals.PmxDigitalSignal Target { get; }

//		private HbmCorePmxDigitalSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.SkillReaders.PmxFileSkillReader"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxFileSkillReader : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxFileSkillReader(global::Hbm.Api.Pmx.SkillReaders.PmxFileSkillReader target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.SkillReaders.PmxFileSkillReader Target { get; }

//		private HbmCorePmxFileSkillReader CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Channels.PmxVirtualChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxVirtualChannel : HbmCoreVirtualChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxVirtualChannel(global::Hbm.Api.Pmx.Channels.PmxVirtualChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Channels.PmxVirtualChannel Target { get; }

//		private HbmCorePmxVirtualChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Connectors.PmxVirtualConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxVirtualConnector : HbmCoreVirtualConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxVirtualConnector(global::Hbm.Api.Pmx.Connectors.PmxVirtualConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Connectors.PmxVirtualConnector Target { get; }

//		private HbmCorePmxVirtualConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Pmx.Signals.PmxVirtualSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePmxVirtualSignal : HbmCoreVirtualSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePmxVirtualSignal(global::Hbm.Api.Pmx.Signals.PmxVirtualSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Pmx.Signals.PmxVirtualSignal Target { get; }

//		private HbmCorePmxVirtualSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.SpectrumInfos.Point"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePoint : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePoint(global::Hbm.Api.Common.Entities.SpectrumInfos.Point target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.SpectrumInfos.Point Target { get; }

//		private HbmCorePoint CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCorePoint CreateInstance(global::System.Double x, global::System.Double y)
//		{
//			//Void .ctor(Double, Double)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.PointScalingBase"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCorePointScalingBase : HbmCoreScaling
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePointScalingBase(global::Hbm.Api.SensorDB.Entities.Scalings.PointScalingBase target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.PointScalingBase Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.PolynomialScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePolynomialScaling : HbmCoreScaling
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePolynomialScaling(global::Hbm.Api.SensorDB.Entities.Scalings.PolynomialScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.PolynomialScaling Target { get; }

//		private HbmCorePolynomialScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.PolynomialSegment"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePolynomialSegment : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePolynomialSegment(global::Hbm.Api.SensorDB.Entities.Scalings.PolynomialSegment target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.PolynomialSegment Target { get; }

//		private HbmCorePolynomialSegment CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCorePolynomialSegment CreateInstance(global::System.Double startX)
//		{
//			//Void .ctor(Double)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.PolynomialTerm"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePolynomialTerm : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePolynomialTerm(global::Hbm.Api.SensorDB.Entities.Scalings.PolynomialTerm target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.PolynomialTerm Target { get; }

//		private HbmCorePolynomialTerm CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCorePolynomialTerm CreateInstance(global::System.Decimal coefficient, global::System.Int32 exponent)
//		{
//			//Void .ctor(System.Decimal, Int32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.PotentiometerSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePotentiometerSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePotentiometerSensor(global::Hbm.Api.SensorDB.Entities.Sensors.PotentiometerSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.PotentiometerSensor Target { get; }

//		private HbmCorePotentiometerSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.PrepareDaqFailedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePrepareDaqFailedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePrepareDaqFailedException(global::Hbm.Api.Common.Exceptions.PrepareDaqFailedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.PrepareDaqFailedException Target { get; }

//		private HbmCorePrepareDaqFailedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCorePrepareDaqFailedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCorePrepareDaqFailedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.Problem"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreProblem : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreProblem(global::Hbm.Api.Common.Entities.Problems.Problem target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.Problem Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Core.ProducerConsumerQueue"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreProducerConsumerQueue : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreProducerConsumerQueue(global::Hbm.Api.Common.Core.ProducerConsumerQueue target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Core.ProducerConsumerQueue Target { get; }

//		private HbmCoreProducerConsumerQueue CreateInstance(global::System.Int32 numberOfConsumerThreads, global::System.Int32 maxThreadStackSize)
//		{
//			//Void .ctor(Int32, Int32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.TimeSources.PtpTimeSource"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePtpTimeSource : HbmCoreTimeSource
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePtpTimeSource(global::Hbm.Api.Common.Entities.TimeSources.PtpTimeSource target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.TimeSources.PtpTimeSource Target { get; }

//		private HbmCorePtpTimeSource CreateInstance(HbmCoreDelayMechanism delayMechanism, HbmCoreTransportMode transportMode)
//		{
//			//Void .ctor(Hbm.Api.Common.Enums.DelayMechanism, Hbm.Api.Common.Enums.TransportMode)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.PtSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePtSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePtSensor(global::Hbm.Api.SensorDB.Entities.Sensors.PtSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.PtSensor Target { get; }

//		private HbmCorePtSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.PwmSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCorePwmSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCorePwmSensor(global::Hbm.Api.SensorDB.Entities.Sensors.PwmSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.PwmSensor Target { get; }

//		private HbmCorePwmSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.QuantumXAdditionalFeatures"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXAdditionalFeatures : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXAdditionalFeatures(global::Hbm.Api.QuantumX.QuantumXAdditionalFeatures target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.QuantumXAdditionalFeatures Target { get; }

//		private HbmCoreQuantumXAdditionalFeatures CreateInstance(HbmCoreQuantumXDevice quantumXDevice)
//		{
//			//Void .ctor(Hbm.Api.QuantumX.QuantumXDevice)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Channels.QuantumXAnalogInChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXAnalogInChannel : HbmCoreAnalogInChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXAnalogInChannel(global::Hbm.Api.QuantumX.Channels.QuantumXAnalogInChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Channels.QuantumXAnalogInChannel Target { get; }

//		private HbmCoreQuantumXAnalogInChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogInConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXAnalogInConnector : HbmCoreAnalogInConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXAnalogInConnector(global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogInConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogInConnector Target { get; }

//		private HbmCoreQuantumXAnalogInConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Signals.QuantumXAnalogInSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXAnalogInSignal : HbmCoreAnalogInSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXAnalogInSignal(global::Hbm.Api.QuantumX.Signals.QuantumXAnalogInSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Signals.QuantumXAnalogInSignal Target { get; }

//		private HbmCoreQuantumXAnalogInSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreQuantumXAnalogInSignal CreateInstance(HbmCoreQuantumXSyncSignal qxSyncSignal)
//		{
//			//Void .ctor(Hbm.Api.QuantumX.Signals.QuantumXSyncSignal)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Channels.QuantumXAnalogOutChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXAnalogOutChannel : HbmCoreAnalogOutChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXAnalogOutChannel(global::Hbm.Api.QuantumX.Channels.QuantumXAnalogOutChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Channels.QuantumXAnalogOutChannel Target { get; }

//		private HbmCoreQuantumXAnalogOutChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogOutConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXAnalogOutConnector : HbmCoreAnalogOutConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXAnalogOutConnector(global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogOutConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogOutConnector Target { get; }

//		private HbmCoreQuantumXAnalogOutConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Signals.QuantumXAnalogOutSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXAnalogOutSignal : HbmCoreAnalogOutSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXAnalogOutSignal(global::Hbm.Api.QuantumX.Signals.QuantumXAnalogOutSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Signals.QuantumXAnalogOutSignal Target { get; }

//		private HbmCoreQuantumXAnalogOutSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Connectors.QuantumXCanConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXCanConnector : HbmCoreCanConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXCanConnector(global::Hbm.Api.QuantumX.Connectors.QuantumXCanConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Connectors.QuantumXCanConnector Target { get; }

//		private HbmCoreQuantumXCanConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Channels.QuantumXCanInChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXCanInChannel : HbmCoreCanInChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXCanInChannel(global::Hbm.Api.QuantumX.Channels.QuantumXCanInChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Channels.QuantumXCanInChannel Target { get; }

//		private HbmCoreQuantumXCanInChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Signals.QuantumXCanInSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXCanInSignal : HbmCoreCanInSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXCanInSignal(global::Hbm.Api.QuantumX.Signals.QuantumXCanInSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Signals.QuantumXCanInSignal Target { get; }

//		private HbmCoreQuantumXCanInSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Channels.QuantumXCanOutChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXCanOutChannel : HbmCoreCanOutChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXCanOutChannel(global::Hbm.Api.QuantumX.Channels.QuantumXCanOutChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Channels.QuantumXCanOutChannel Target { get; }

//		private HbmCoreQuantumXCanOutChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Statuses.QuantumXChannelTestSignalActiveStatus"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXChannelTestSignalActiveStatus : HbmCoreDeviceStatus
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXChannelTestSignalActiveStatus(global::Hbm.Api.QuantumX.Statuses.QuantumXChannelTestSignalActiveStatus target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Statuses.QuantumXChannelTestSignalActiveStatus Target { get; }

//		private HbmCoreQuantumXChannelTestSignalActiveStatus CreateInstance(global::System.Int32 originalID, HbmCoreQuantumXTestSignalActiveValueType value, HbmCoreChannel channel)
//		{
//			//Void .ctor(Int32, Hbm.Api.QuantumX.Enums.QuantumXTestSignalActiveValueType, Hbm.Api.Common.Entities.Channels.Channel)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Statuses.QuantumXConnectorShuntStatus"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXConnectorShuntStatus : HbmCoreDeviceStatus
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXConnectorShuntStatus(global::Hbm.Api.QuantumX.Statuses.QuantumXConnectorShuntStatus target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Statuses.QuantumXConnectorShuntStatus Target { get; }

//		private HbmCoreQuantumXConnectorShuntStatus CreateInstance(global::System.Int32 originalID, HbmCoreShuntMode value, HbmCoreConnector connector)
//		{
//			//Void .ctor(Int32, Hbm.Api.Common.Enums.ShuntMode, Hbm.Api.Common.Entities.Connectors.Connector)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.QuantumXDevice"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXDevice : HbmCoreStreamingDevice
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXDevice(global::Hbm.Api.QuantumX.QuantumXDevice target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.QuantumXDevice Target { get; }

//		private HbmCoreQuantumXDevice CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreQuantumXDevice CreateInstance(global::System.String ipAddress, global::System.Int32 port, global::System.Int32 streamingPort, global::System.Int32 httpPort)
//		{
//			//Void .ctor(System.String, Int32, Int32, Int32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.QuantumXDeviceFamily"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXDeviceFamily : HbmCoreDeviceFamily
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXDeviceFamily(global::Hbm.Api.QuantumX.QuantumXDeviceFamily target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.QuantumXDeviceFamily Target { get; }

//		private HbmCoreQuantumXDeviceFamily CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Channels.QuantumXDigitalChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXDigitalChannel : HbmCoreDigitalChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXDigitalChannel(global::Hbm.Api.QuantumX.Channels.QuantumXDigitalChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Channels.QuantumXDigitalChannel Target { get; }

//		private HbmCoreQuantumXDigitalChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Connectors.QuantumXDigitalConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXDigitalConnector : HbmCoreDigitalConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXDigitalConnector(global::Hbm.Api.QuantumX.Connectors.QuantumXDigitalConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Connectors.QuantumXDigitalConnector Target { get; }

//		private HbmCoreQuantumXDigitalConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Signals.QuantumXDigitalSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXDigitalSignal : HbmCoreDigitalSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXDigitalSignal(global::Hbm.Api.QuantumX.Signals.QuantumXDigitalSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Signals.QuantumXDigitalSignal Target { get; }

//		private HbmCoreQuantumXDigitalSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.SpectrumInfos.QuantumXEtalonSpectrumInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXEtalonSpectrumInfo : HbmCoreEtalonSpectrumInfo
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXEtalonSpectrumInfo(global::Hbm.Api.QuantumX.SpectrumInfos.QuantumXEtalonSpectrumInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.SpectrumInfos.QuantumXEtalonSpectrumInfo Target { get; }

//		private HbmCoreQuantumXEtalonSpectrumInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Exceptions.QuantumXException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreQuantumXException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXException(global::Hbm.Api.QuantumX.Exceptions.QuantumXException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Exceptions.QuantumXException Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Channels.QuantumXFbgChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXFbgChannel : HbmCoreFbgChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXFbgChannel(global::Hbm.Api.QuantumX.Channels.QuantumXFbgChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Channels.QuantumXFbgChannel Target { get; }

//		private HbmCoreQuantumXFbgChannel CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Connectors.QuantumXFbgConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXFbgConnector : HbmCoreFbgConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXFbgConnector(global::Hbm.Api.QuantumX.Connectors.QuantumXFbgConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Connectors.QuantumXFbgConnector Target { get; }

//		private HbmCoreQuantumXFbgConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Signals.QuantumXFbgSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXFbgSignal : HbmCoreFbgSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXFbgSignal(global::Hbm.Api.QuantumX.Signals.QuantumXFbgSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Signals.QuantumXFbgSignal Target { get; }

//		private HbmCoreQuantumXFbgSignal CreateInstance(HbmCoreQuantumXSyncSignal qxSyncSignal)
//		{
//			//Void .ctor(Hbm.Api.QuantumX.Signals.QuantumXSyncSignal)
//			return null;
//		}

//		private HbmCoreQuantumXFbgSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.SpectrumInfos.QuantumXFiberSpectrumInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXFiberSpectrumInfo : HbmCoreFiberSpectrumInfo
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXFiberSpectrumInfo(global::Hbm.Api.QuantumX.SpectrumInfos.QuantumXFiberSpectrumInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.SpectrumInfos.QuantumXFiberSpectrumInfo Target { get; }

//		private HbmCoreQuantumXFiberSpectrumInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.SkillReaders.QuantumXFileSkillReader"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXFileSkillReader : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXFileSkillReader(global::Hbm.Api.QuantumX.SkillReaders.QuantumXFileSkillReader target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.SkillReaders.QuantumXFileSkillReader Target { get; }

//		private HbmCoreQuantumXFileSkillReader CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.SpectrumInfos.QuantumXGasCellSpectrumInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXGasCellSpectrumInfo : HbmCoreGasCellSpectrumInfo
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXGasCellSpectrumInfo(global::Hbm.Api.QuantumX.SpectrumInfos.QuantumXGasCellSpectrumInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.SpectrumInfos.QuantumXGasCellSpectrumInfo Target { get; }

//		private HbmCoreQuantumXGasCellSpectrumInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Connectors.QuantumXOffConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXOffConnector : HbmCoreConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXOffConnector(global::Hbm.Api.QuantumX.Connectors.QuantumXOffConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Connectors.QuantumXOffConnector Target { get; }

//		private HbmCoreQuantumXOffConnector CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.QuantumX.Signals.QuantumXSyncSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreQuantumXSyncSignal : HbmCoreSyncSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreQuantumXSyncSignal(global::Hbm.Api.QuantumX.Signals.QuantumXSyncSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.QuantumX.Signals.QuantumXSyncSignal Target { get; }

//		private HbmCoreQuantumXSyncSignal CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.ResistanceSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreResistanceSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreResistanceSensor(global::Hbm.Api.SensorDB.Entities.Sensors.ResistanceSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.ResistanceSensor Target { get; }

//		private HbmCoreResistanceSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.RootCategoryNotFoundException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreRootCategoryNotFoundException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreRootCategoryNotFoundException(global::Hbm.Api.SensorDB.Exceptions.RootCategoryNotFoundException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.RootCategoryNotFoundException Target { get; }

//		private HbmCoreRootCategoryNotFoundException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreRootCategoryNotFoundException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreRootCategoryNotFoundException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.SampleRatesEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSampleRatesEventArgs : HbmCoreSignalsEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSampleRatesEventArgs(global::Hbm.Api.Common.Messaging.SampleRatesEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.SampleRatesEventArgs Target { get; }

//		private HbmCoreSampleRatesEventArgs CreateInstance(global::System.String uniqueDeviceID, global::System.Collections.Generic.List<global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalSampleRateChange> dependentSignalsOldSampleRates, global::System.Collections.Generic.List<global::System.String> uniqueSignalIDs, HbmCoreChangeReason reason)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[Hbm.Api.Common.Messaging.DataTransferObjects.SignalSampleRateChange], System.Collections.Generic.List`1[System.String], Hbm.Api.Common.Enums.ChangeReason)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreScaling : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreScaling(global::Hbm.Api.SensorDB.Entities.Scaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scaling Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.ScalingNotAssignedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreScalingNotAssignedException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreScalingNotAssignedException(global::Hbm.Api.SensorDB.Exceptions.ScalingNotAssignedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.ScalingNotAssignedException Target { get; }

//		private HbmCoreScalingNotAssignedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreScalingNotAssignedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreScalingNotAssignedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.ScalingNotSupportedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreScalingNotSupportedException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreScalingNotSupportedException(global::Hbm.Api.SensorDB.Exceptions.ScalingNotSupportedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.ScalingNotSupportedException Target { get; }

//		private HbmCoreScalingNotSupportedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreScalingNotSupportedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreScalingNotSupportedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.ScalingPoint"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreScalingPoint : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreScalingPoint(global::Hbm.Api.SensorDB.Entities.Scalings.ScalingPoint target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.ScalingPoint Target { get; }

//		private HbmCoreScalingPoint CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreScalingPoint CreateInstance(global::System.Decimal x, global::System.Decimal y)
//		{
//			//Void .ctor(System.Decimal, System.Decimal)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Scan.Entities.Device"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreScanDevice : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreScanDevice(global::Hbm.Api.Scan.Entities.Device target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Scan.Entities.Device Target { get; }

//		private HbmCoreScanDevice CreateInstance(global::System.String uuid, global::System.String name, global::System.String type, global::System.String label, global::System.String family, global::System.String firmwareVersion, global::System.String gatewayIp4Address, global::System.String gatewayIp6Address)
//		{
//			//Void .ctor(System.String, System.String, System.String, System.String, System.String, System.String, System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Scan.Entities.ScanFailedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreScanFailedException : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreScanFailedException(global::Hbm.Api.Scan.Entities.ScanFailedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Scan.Entities.ScanFailedException Target { get; }

//		private HbmCoreScanFailedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreScanFailedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreScanFailedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Scan.Entities.Service"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreScanService : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreScanService(global::Hbm.Api.Scan.Entities.Service target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Scan.Entities.Service Target { get; }

//		private HbmCoreScanService CreateInstance(global::System.String type, global::System.UInt16 port, global::System.UInt16 interfaceIndex)
//		{
//			//Void .ctor(System.String, UInt16, UInt16)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreSensor : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSensor(global::Hbm.Api.SensorDB.Entities.Sensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensor Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.SensorDBDataException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSensorDBDataException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSensorDBDataException(global::Hbm.Api.SensorDB.Exceptions.SensorDBDataException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.SensorDBDataException Target { get; }

//		private HbmCoreSensorDBDataException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreSensorDBDataException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreSensorDBDataException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.SensorDBException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreSensorDBException : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSensorDBException(global::Hbm.Api.SensorDB.Exceptions.SensorDBException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.SensorDBException Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.SensorDBManager"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSensorDBManager : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSensorDBManager(global::Hbm.Api.SensorDB.SensorDBManager target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.SensorDBManager Target { get; }

//		private HbmCoreSensorDBManager CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreSensorDBManager CreateInstance(global::System.String twoLetterISOLanguageName)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreSensorDBManager CreateInstance(global::System.String twoLetterISOLanguageName, global::System.String connectionString, global::System.String providerInvariantName)
//		{
//			//Void .ctor(System.String, System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Exceptions.SensorNotSupportedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSensorNotSupportedException : HbmCoreSensorDBException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSensorNotSupportedException(global::Hbm.Api.SensorDB.Exceptions.SensorNotSupportedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Exceptions.SensorNotSupportedException Target { get; }

//		private HbmCoreSensorNotSupportedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreSensorNotSupportedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreSensorNotSupportedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.SensorsEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSensorsEventArgs : HbmCoreChannelsEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSensorsEventArgs(global::Hbm.Api.Common.Messaging.SensorsEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.SensorsEventArgs Target { get; }

//		private HbmCoreSensorsEventArgs CreateInstance(global::System.String uniqueDeviceId, global::System.Collections.Generic.List<global::Hbm.Api.Common.Messaging.DataTransferObjects.ChannelSensorValueChange> oldSensorsByUniqueChannelIDs, global::System.Collections.Generic.List<global::System.String> uniqueChannelIDs, HbmCoreChangeReason reason)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[Hbm.Api.Common.Messaging.DataTransferObjects.ChannelSensorValueChange], System.Collections.Generic.List`1[System.String], Hbm.Api.Common.Enums.ChangeReason)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Service"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreService : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreService(global::Hbm.Api.Common.Entities.Service target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Service Target { get; }

//		private HbmCoreService CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.Signal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreSignal : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSignal(global::Hbm.Api.Common.Entities.Signals.Signal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.Signal Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalChange"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreSignalChange : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSignalChange(global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalChange target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalChange Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalFilterChange"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSignalFilterChange : HbmCoreSignalChange
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSignalFilterChange(global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalFilterChange target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalFilterChange Target { get; }

//		private HbmCoreSignalFilterChange CreateInstance(global::System.String signalId, global::System.Collections.Generic.List<global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalFilter> dependentSignalsOldFilterValue)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalFilter])
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalSampleRateChange"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSignalSampleRateChange : HbmCoreSignalChange
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSignalSampleRateChange(global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalSampleRateChange target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.DataTransferObjects.SignalSampleRateChange Target { get; }

//		private HbmCoreSignalSampleRateChange CreateInstance(global::System.String signalId, global::System.Collections.Generic.List<global::Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalSampleRate> dependentSignalsOldSampleRatesValues)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[Hbm.Api.Common.Messaging.DataTransferObjects.DependentSignalSampleRate])
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Messaging.SignalsEventArgs"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSignalsEventArgs : HbmCoreDeviceEventArgs
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSignalsEventArgs(global::Hbm.Api.Common.Messaging.SignalsEventArgs target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Messaging.SignalsEventArgs Target { get; }

//		private HbmCoreSignalsEventArgs CreateInstance(global::System.String uniqueDeviceID, global::System.Collections.Generic.List<global::System.String> uniqueSignalIDs, HbmCoreChangeReason reason)
//		{
//			//Void .ctor(System.String, System.Collections.Generic.List`1[System.String], Hbm.Api.Common.Enums.ChangeReason)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.SpectrumInfos.Spectrum"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSpectrum : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSpectrum(global::Hbm.Api.Common.Entities.SpectrumInfos.Spectrum target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.SpectrumInfos.Spectrum Target { get; }

//		private HbmCoreSpectrum CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.SpectrumInfos.SpectrumInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreSpectrumInfo : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSpectrumInfo(global::Hbm.Api.Common.Entities.SpectrumInfos.SpectrumInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.SpectrumInfos.SpectrumInfo Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.SsiSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreSsiSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSsiSensor(global::Hbm.Api.SensorDB.Entities.Sensors.SsiSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.SsiSensor Target { get; }

//		private HbmCoreSsiSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.ConnectionInfos.StreamingConnectionInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreStreamingConnectionInfo : HbmCoreEthernetConnectionInfo
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreStreamingConnectionInfo(global::Hbm.Api.Common.Entities.ConnectionInfos.StreamingConnectionInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.ConnectionInfos.StreamingConnectionInfo Target { get; }

//		private HbmCoreStreamingConnectionInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreStreamingConnectionInfo CreateInstance(global::System.String ipAddress, global::System.Int32 port)
//		{
//			//Void .ctor(System.String, Int32)
//			return null;
//		}

//		private HbmCoreStreamingConnectionInfo CreateInstance(global::System.String ipAddress, global::System.Int32 port, global::System.String subnetMask)
//		{
//			//Void .ctor(System.String, Int32, System.String)
//			return null;
//		}

//		private HbmCoreStreamingConnectionInfo CreateInstance(global::System.String ipAddress, global::System.Int32 port, global::System.Int32 streamingPort, global::System.Int32 httpPort)
//		{
//			//Void .ctor(System.String, Int32, Int32, Int32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.StreamingDevice"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreStreamingDevice : HbmCoreDevice
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreStreamingDevice(global::Hbm.Api.Common.Entities.StreamingDevice target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.StreamingDevice Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.StreamingInitializationFailedException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreStreamingInitializationFailedException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreStreamingInitializationFailedException(global::Hbm.Api.Common.Exceptions.StreamingInitializationFailedException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.StreamingInitializationFailedException Target { get; }

//		private HbmCoreStreamingInitializationFailedException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreStreamingInitializationFailedException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreStreamingInitializationFailedException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.StreamingInvalidDataException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreStreamingInvalidDataException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreStreamingInvalidDataException(global::Hbm.Api.Common.Exceptions.StreamingInvalidDataException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.StreamingInvalidDataException Target { get; }

//		private HbmCoreStreamingInvalidDataException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreStreamingInvalidDataException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreStreamingInvalidDataException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.StreamingTimeoutException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreStreamingTimeoutException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreStreamingTimeoutException(global::Hbm.Api.Common.Exceptions.StreamingTimeoutException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.StreamingTimeoutException Target { get; }

//		private HbmCoreStreamingTimeoutException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreStreamingTimeoutException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreStreamingTimeoutException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.SyncSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreSyncSignal : HbmCoreSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreSyncSignal(global::Hbm.Api.Common.Entities.Signals.SyncSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.SyncSignal Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.TableScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTableScaling : HbmCorePointScalingBase
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTableScaling(global::Hbm.Api.SensorDB.Entities.Scalings.TableScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.TableScaling Target { get; }

//		private HbmCoreTableScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.TedsActivationWarning"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTedsActivationWarning : HbmCoreWarning
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTedsActivationWarning(global::Hbm.Api.Common.Entities.Problems.TedsActivationWarning target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.TedsActivationWarning Target { get; }

//		private HbmCoreTedsActivationWarning CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsActivationWarning CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsActivationWarning CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsActivationWarning CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsActivationWarning CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Dal.Converter.TedsConverter"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTedsConverter : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTedsConverter(global::Hbm.Api.SensorDB.Dal.Converter.TedsConverter target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Dal.Converter.TedsConverter Target { get; }

//		private HbmCoreTedsConverter CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.TedsNotAvailableWarning"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTedsNotAvailableWarning : HbmCoreWarning
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTedsNotAvailableWarning(global::Hbm.Api.Common.Entities.Problems.TedsNotAvailableWarning target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.TedsNotAvailableWarning Target { get; }

//		private HbmCoreTedsNotAvailableWarning CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsNotAvailableWarning CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsNotAvailableWarning CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsNotAvailableWarning CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsNotAvailableWarning CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.TedsNotSupportedWarning"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTedsNotSupportedWarning : HbmCoreWarning
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTedsNotSupportedWarning(global::Hbm.Api.Common.Entities.Problems.TedsNotSupportedWarning target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.TedsNotSupportedWarning Target { get; }

//		private HbmCoreTedsNotSupportedWarning CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsNotSupportedWarning CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsNotSupportedWarning CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsNotSupportedWarning CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsNotSupportedWarning CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.TedsReadingWarning"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTedsReadingWarning : HbmCoreWarning
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTedsReadingWarning(global::Hbm.Api.Common.Entities.Problems.TedsReadingWarning target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.TedsReadingWarning Target { get; }

//		private HbmCoreTedsReadingWarning CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsReadingWarning CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsReadingWarning CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsReadingWarning CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsReadingWarning CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.TedsWritingWarning"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTedsWritingWarning : HbmCoreWarning
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTedsWritingWarning(global::Hbm.Api.Common.Entities.Problems.TedsWritingWarning target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.TedsWritingWarning Target { get; }

//		private HbmCoreTedsWritingWarning CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsWritingWarning CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsWritingWarning CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsWritingWarning CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreTedsWritingWarning CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.ThermoCoupleSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreThermoCoupleSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreThermoCoupleSensor(global::Hbm.Api.SensorDB.Entities.Sensors.ThermoCoupleSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.ThermoCoupleSensor Target { get; }

//		private HbmCoreThermoCoupleSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.TimeSources.TimeSource"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreTimeSource : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTimeSource(global::Hbm.Api.Common.Entities.TimeSources.TimeSource target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.TimeSources.TimeSource Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.TooManySampleRatesException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTooManySampleRatesException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTooManySampleRatesException(global::Hbm.Api.Common.Exceptions.TooManySampleRatesException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.TooManySampleRatesException Target { get; }

//		private HbmCoreTooManySampleRatesException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreTooManySampleRatesException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreTooManySampleRatesException CreateInstance(global::System.String message, global::System.String familyName, global::System.String uniqueId, global::System.String deviceName, global::System.Int32 maxSupportedSampleRates)
//		{
//			//Void .ctor(System.String, System.String, System.String, System.String, Int32)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Translation"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTranslation : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTranslation(global::Hbm.Api.SensorDB.Entities.Translation target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Translation Target { get; }

//		private HbmCoreTranslation CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreTranslation CreateInstance(global::System.Int32 translationID, global::System.String twoLetterISOLanguageName, global::System.String description)
//		{
//			//Void .ctor(Int32, System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.TwoPointScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTwoPointScaling : HbmCorePointScalingBase
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTwoPointScaling(global::Hbm.Api.SensorDB.Entities.Scalings.TwoPointScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.TwoPointScaling Target { get; }

//		private HbmCoreTwoPointScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreTwoPointScaling CreateInstance(global::System.Decimal electricalP1, global::System.Decimal electricalP2, global::System.String engineeringUnit, global::System.Decimal engineeringP1, global::System.Decimal engineeringP2, global::System.Boolean isEngineeringRangeRms)
//		{
//			//Void .ctor(System.Decimal, System.Decimal, System.String, System.Decimal, System.Decimal, Boolean)
//			return null;
//		}

//		private HbmCoreTwoPointScaling CreateInstance(global::System.Decimal electricalP1, global::System.Decimal electricalP2, global::System.String engineeringUnit, global::System.Decimal engineeringP1, global::System.Decimal engineeringP2, global::System.Decimal minEngineeringRange, global::System.Decimal maxEngineeringRange, global::System.Boolean isEngineeringRangeRms)
//		{
//			//Void .ctor(System.Decimal, System.Decimal, System.String, System.Decimal, System.Decimal, System.Decimal, System.Decimal, Boolean)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.TypeNotSupportedError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreTypeNotSupportedError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreTypeNotSupportedError(global::Hbm.Api.Common.Entities.Problems.TypeNotSupportedError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.TypeNotSupportedError Target { get; }

//		private HbmCoreTypeNotSupportedError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreTypeNotSupportedError CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreTypeNotSupportedError CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreTypeNotSupportedError CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreTypeNotSupportedError CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.UnexpectedError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreUnexpectedError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreUnexpectedError(global::Hbm.Api.Common.Entities.Problems.UnexpectedError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.UnexpectedError Target { get; }

//		private HbmCoreUnexpectedError CreateInstance(HbmCoreDevice device, global::System.Exception exception, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, System.Exception, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreUnexpectedError CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, global::System.Exception exception, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, System.Exception, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreUnexpectedError CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, global::System.Exception exception, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, System.Exception, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreUnexpectedError CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, global::System.Exception exception, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, System.Exception, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreUnexpectedError CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, global::System.Exception exception, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, System.Exception, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.UnknownChannelException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreUnknownChannelException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreUnknownChannelException(global::Hbm.Api.Common.Exceptions.UnknownChannelException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.UnknownChannelException Target { get; }

//		private HbmCoreUnknownChannelException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreUnknownChannelException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreUnknownChannelException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.UnknownConnectorException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreUnknownConnectorException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreUnknownConnectorException(global::Hbm.Api.Common.Exceptions.UnknownConnectorException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.UnknownConnectorException Target { get; }

//		private HbmCoreUnknownConnectorException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreUnknownConnectorException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreUnknownConnectorException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.UnknownFilterException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreUnknownFilterException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreUnknownFilterException(global::Hbm.Api.Common.Exceptions.UnknownFilterException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.UnknownFilterException Target { get; }

//		private HbmCoreUnknownFilterException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreUnknownFilterException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreUnknownFilterException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.UnknownSignalException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreUnknownSignalException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreUnknownSignalException(global::Hbm.Api.Common.Exceptions.UnknownSignalException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.UnknownSignalException Target { get; }

//		private HbmCoreUnknownSignalException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreUnknownSignalException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreUnknownSignalException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Exceptions.UpdateFirmwareException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreUpdateFirmwareException : HbmCoreCommonAPIException
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreUpdateFirmwareException(global::Hbm.Api.Common.Exceptions.UpdateFirmwareException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Exceptions.UpdateFirmwareException Target { get; }

//		private HbmCoreUpdateFirmwareException CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreUpdateFirmwareException CreateInstance(global::System.String message)
//		{
//			//Void .ctor(System.String)
//			return null;
//		}

//		private HbmCoreUpdateFirmwareException CreateInstance(global::System.String message, global::System.Exception inner)
//		{
//			//Void .ctor(System.String, System.Exception)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Utils.Exceptions.UtilsException"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreUtilsException : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreUtilsException(global::Hbm.Api.Utils.Exceptions.UtilsException target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Utils.Exceptions.UtilsException Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.ValueAdaptedWarning"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreValueAdaptedWarning : HbmCoreWarning
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreValueAdaptedWarning(global::Hbm.Api.Common.Entities.Problems.ValueAdaptedWarning target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.ValueAdaptedWarning Target { get; }

//		private HbmCoreValueAdaptedWarning CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String realizedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueAdaptedWarning CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String realizedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueAdaptedWarning CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String realizedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueAdaptedWarning CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String realizedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueAdaptedWarning CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String realizedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.ValueNotAssignedWarning"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreValueNotAssignedWarning : HbmCoreWarning
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreValueNotAssignedWarning(global::Hbm.Api.Common.Entities.Problems.ValueNotAssignedWarning target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.ValueNotAssignedWarning Target { get; }

//		private HbmCoreValueNotAssignedWarning CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreValueNotAssignedWarning CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreValueNotAssignedWarning CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreValueNotAssignedWarning CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//		private HbmCoreValueNotAssignedWarning CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.ValueNotSetError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreValueNotSetError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreValueNotSetError(global::Hbm.Api.Common.Entities.Problems.ValueNotSetError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.ValueNotSetError Target { get; }

//		private HbmCoreValueNotSetError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueNotSetError CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueNotSetError CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueNotSetError CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueNotSetError CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType, System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.ValueOutOfRangeError"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreValueOutOfRangeError : HbmCoreError
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreValueOutOfRangeError(global::Hbm.Api.Common.Entities.Problems.ValueOutOfRangeError target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.ValueOutOfRangeError Target { get; }

//		private HbmCoreValueOutOfRangeError CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String minPossibleValue, global::System.String maxPossibleValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueOutOfRangeError CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String minPossibleValue, global::System.String maxPossibleValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueOutOfRangeError CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String minPossibleValue, global::System.String maxPossibleValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueOutOfRangeError CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String minPossibleValue, global::System.String maxPossibleValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String, System.String)
//			return null;
//		}

//		private HbmCoreValueOutOfRangeError CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType, global::System.String propertyName, global::System.String demandedValue, global::System.String minPossibleValue, global::System.String maxPossibleValue)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType, System.String, System.String, System.String, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.VersionInfo"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreVersionInfo : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreVersionInfo(global::Hbm.Api.SensorDB.Entities.VersionInfo target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.VersionInfo Target { get; }

//		private HbmCoreVersionInfo CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Channels.VirtualChannel"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreVirtualChannel : HbmCoreChannel
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreVirtualChannel(global::Hbm.Api.Common.Entities.Channels.VirtualChannel target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Channels.VirtualChannel Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Connectors.VirtualConnector"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreVirtualConnector : HbmCoreConnector
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreVirtualConnector(global::Hbm.Api.Common.Entities.Connectors.VirtualConnector target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Connectors.VirtualConnector Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Signals.VirtualSignal"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public abstract class HbmCoreVirtualSignal : HbmCoreSignal
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreVirtualSignal(global::Hbm.Api.Common.Entities.Signals.VirtualSignal target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Signals.VirtualSignal Target { get; }

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.VoltageProbeSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreVoltageProbeSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreVoltageProbeSensor(global::Hbm.Api.SensorDB.Entities.Sensors.VoltageProbeSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.VoltageProbeSensor Target { get; }

//		private HbmCoreVoltageProbeSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Sensors.VoltageSensor"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreVoltageSensor : HbmCoreSensor
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreVoltageSensor(global::Hbm.Api.SensorDB.Entities.Sensors.VoltageSensor target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Sensors.VoltageSensor Target { get; }

//		private HbmCoreVoltageSensor CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Problems.Warning"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreWarning : HbmCoreProblem
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreWarning(global::Hbm.Api.Common.Entities.Problems.Warning target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Problems.Warning Target { get; }

//		private HbmCoreWarning CreateInstance(HbmCoreDevice device, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreWarning CreateInstance(HbmCoreDevice device, HbmCoreConnector connector, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Connectors.Connector, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreWarning CreateInstance(HbmCoreDevice device, HbmCoreChannel channel, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Channels.Channel, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreWarning CreateInstance(HbmCoreDevice device, HbmCoreSignal signal, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.Common.Entities.Signals.Signal, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//		private HbmCoreWarning CreateInstance(HbmCoreDevice device, HbmCoreSensor sensor, HbmCoreSettingType settingType, global::System.String propertyName)
//		{
//			//Void .ctor(Hbm.Api.Common.Entities.Device, Hbm.Api.SensorDB.Entities.Sensor, Hbm.Api.Common.Enums.SettingType, System.String)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.Common.Entities.Zero"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreZero : HbmCoreObject
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreZero(global::Hbm.Api.Common.Entities.Zero target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.Common.Entities.Zero Target { get; }

//		private HbmCoreZero CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//		private HbmCoreZero CreateInstance(global::System.Double offset, global::System.Double target, global::System.Boolean isZeroBalancingInhibited)
//		{
//			//Void .ctor(Double, Double, Boolean)
//			return null;
//		}

//	}

//	/// <summary>
//	/// Представляет оболочку вокруг класса <see cref="global::Hbm.Api.SensorDB.Entities.Scalings.ZeroSpanScaling"/>.
//	/// </summary>
//	[CLSCompliant(false)]
//	public class HbmCoreZeroSpanScaling : HbmCorePointScalingBase
//	{
//		/// <summary>
//		/// Инициализирует новый экземпляр класса.
//		/// </summary>
//		/// <param name="target">
//		/// Целевой объект.
//		/// </param>
//		public HbmCoreZeroSpanScaling(global::Hbm.Api.SensorDB.Entities.Scalings.ZeroSpanScaling target) :
//			base(target)
//		{
//			Target = target;
//		}

//		/// <summary>
//		/// Возвращает целевой объект.
//		/// </summary>
//		public new global::Hbm.Api.SensorDB.Entities.Scalings.ZeroSpanScaling Target { get; }

//		private HbmCoreZeroSpanScaling CreateInstance()
//		{
//			//Void .ctor()
//			return null;
//		}

//	}

//}
