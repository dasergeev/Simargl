//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ISheetMetalLinearRuledShell
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("934BCC92-BC49-4A46-9A88-70FD2E74537D")]
//[ComImport]
//public interface ISheetMetalLinearRuledShell
//{
//  [DispId(5001)]
//  Sketch Sketch2 { [DispId(5001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Interface), In] set; [DispId(5001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5002)]
//  bool UseCommonSegmentationParameters { [DispId(5002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(5002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(5003)]
//  int CurvesCount { [DispId(5003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_CurveUseSegmentation([In] int CurveIndex, [In] bool PVal);

//  [DispId(5004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_CurveUseSegmentation([In] int CurveIndex);

//  [DispId(5005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_CurveSegmentationMethod([In] int CurveIndex, [In] ksSegmentationMethodEnum PVal);

//  [DispId(5005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  ksSegmentationMethodEnum get_CurveSegmentationMethod([In] int CurveIndex);

//  [DispId(5006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_CurveSegmentationSplitValue([In] int CurveIndex, [In] double PVal);

//  [DispId(5006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double get_CurveSegmentationSplitValue([In] int CurveIndex);

//  [DispId(5007)]
//  bool AutoSegmentation { [DispId(5007), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5007), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddNewEdge([In] int IndexAt);

//  [DispId(5009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteEdge([In] int Index);

//  [DispId(5010)]
//  int EdgesCount { [DispId(5010), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5011)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetEdgePointParam(
//    [In] int EdgeIndex,
//    [In] bool StartPoint,
//    [In] double X,
//    [In] double Y,
//    [In] double Z,
//    [In] ref double T,
//    [MarshalAs(UnmanagedType.Interface), In] IModelObject AssociateVertex);

//  [DispId(5012)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetEdgePointParam(
//    [In] int EdgeIndex,
//    [In] bool StartPoint,
//    out double X,
//    out double Y,
//    out double Z,
//    out double T,
//    [MarshalAs(UnmanagedType.Interface)] out IModelObject AssociateVertex);

//  [DispId(5013)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetEdgePointParams(
//    [MarshalAs(UnmanagedType.Struct)] out object Points1,
//    [MarshalAs(UnmanagedType.Struct)] out object T1,
//    [MarshalAs(UnmanagedType.Struct)] out object AssociateVertexes1,
//    [MarshalAs(UnmanagedType.Struct)] out object Points2,
//    [MarshalAs(UnmanagedType.Struct)] out object T2,
//    [MarshalAs(UnmanagedType.Struct)] out object AssociateVertexes2);
//}
