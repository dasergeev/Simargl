//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IThinParameters
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("B90D597D-4213-4A59-98D4-0B67E719551B")]
//[ComImport]
//public interface IThinParameters
//{
//  [DispId(2501)]
//  bool Thin { [DispId(2501), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(2501), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2502)]
//  ksDirectionTypeEnum ThinType { [DispId(2502), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(2502), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_Thickness([In] bool Normal, [In] double PVal);

//  [DispId(2503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double get_Thickness([In] bool Normal);

//  [DispId(2504)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetThinParameters(
//    out bool Thin,
//    out ksDirectionTypeEnum ThinType,
//    out double ThicknessNormal,
//    out double ThicknessReverse);

//  [DispId(2505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetThinParameters(
//    [In] bool Thin,
//    [In] ksDirectionTypeEnum ThinType,
//    [In] double ThicknessNormal,
//    [In] double ThicknessReverse);
//}
