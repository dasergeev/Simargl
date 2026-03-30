//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ICutViewParam
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("D4C8B5AF-B2A1-4E07-9CD0-A1FC5B0BC1E1")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ICutViewParam
//{
//  [DispId(3500)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddCut(
//    [MarshalAs(UnmanagedType.BStr), In] string Name,
//    [In] int Number,
//    [In] double X,
//    [In] double Y,
//    [In] bool ModelCut,
//    [MarshalAs(UnmanagedType.Interface), In] IDrawingObject Contour,
//    [MarshalAs(UnmanagedType.Interface), In] IView View);

//  [DispId(3501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteCut([In] int Index);

//  [DispId(3502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteAllCuts();

//  [DispId(3503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetCutPlanePosition([In] int Index, [In] double X, [In] double Y);

//  [DispId(3504)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetCutPlanePosition([In] int Index, out double X, out double Y);

//  [DispId(3505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetCutParams([In] int Index, [MarshalAs(UnmanagedType.BStr), In] string Name, [In] int Number, [In] bool ModelCut);

//  [DispId(3506)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IDrawingObject GetCutParams([In] int Index, [MarshalAs(UnmanagedType.BStr)] out string Name, out int Number, out bool ModelCut);

//  [DispId(3507)]
//  int CutsCount { [DispId(3507), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3508)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_LocalCut([In] int Index);

//  [DispId(3508)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_LocalCut([In] int Index, [In] bool Result);

//  [DispId(3509)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IHatchParam get_HatchParam([In] int Index);
//}
