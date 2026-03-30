//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IBreakViewParam
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("1B94C65D-3473-4FF2-B185-0B1C2C98FCAE")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IBreakViewParam
//{
//  [DispId(4500)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddBreakLine([In] double X1, [In] double Y1, [In] double X2, [In] double Y2, [In] double Angle);

//  [DispId(4501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteBreakLine([In] int Index);

//  [DispId(4502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteAllBreakLines();

//  [DispId(4503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetBreakLinePosition([In] int Index, [In] double X1, [In] double Y1, [In] double X2, [In] double Y2);

//  [DispId(4504)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetBreakLinePosition(
//    [In] int Index,
//    out double X1,
//    out double Y1,
//    out double X2,
//    out double Y2);

//  [DispId(4505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetBreakLineParams(
//    [In] int Index,
//    [In] double Angle,
//    [In] double Clearance,
//    [In] ksBreakLineTypeEnum BreakLineType,
//    [In] double Amplitude,
//    [In] double MaxAmplitude);

//  [DispId(4506)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetBreakLineParams(
//    [In] int Index,
//    out double Angle,
//    out double Clearance,
//    out ksBreakLineTypeEnum BreakLineType,
//    out double Amplitude,
//    out double MaxAmplitude);

//  [DispId(4507)]
//  int BreaksCount { [DispId(4507), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(4508)]
//  bool BreaksVisible { [DispId(4508), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(4508), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }
//}
