//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IRuledSurfaceParametersByCurves
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("72C0CEFC-B8F6-44B0-AC75-BA5F95DAE41F")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IRuledSurfaceParametersByCurves
//{
//  [DispId(4001)]
//  object Curves1 { [DispId(4001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Struct), In] set; [DispId(4001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(4002)]
//  object Curves2 { [DispId(4002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Struct), In] set; [DispId(4002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(4003)]
//  bool AutoSegmentation { [DispId(4003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(4003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(4004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddNewEdge([In] int IndexAt);

//  [DispId(4005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteEdge([In] int Index);

//  [DispId(4006)]
//  int EdgesCount { [DispId(4006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(4007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetEdgePointParam(
//    [In] int EdgeIndex,
//    [In] bool StartPoint,
//    [In] double X,
//    [In] double Y,
//    [In] double Z,
//    [In] ref double T,
//    [MarshalAs(UnmanagedType.Interface), In] IModelObject Segment,
//    [MarshalAs(UnmanagedType.Interface), In] IModelObject AssociateVertex);

//  [DispId(4008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetEdgePointParam(
//    [In] int EdgeIndex,
//    [In] bool StartPoint,
//    out double X,
//    out double Y,
//    out double Z,
//    out double T,
//    [MarshalAs(UnmanagedType.Interface)] out IModelObject Segment,
//    [MarshalAs(UnmanagedType.Interface)] out IModelObject AssociateVertex);

//  [DispId(4009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetEdgePointParams(
//    [MarshalAs(UnmanagedType.Struct)] out object Points1,
//    [MarshalAs(UnmanagedType.Struct)] out object T1,
//    [MarshalAs(UnmanagedType.Struct)] out object Segments1,
//    [MarshalAs(UnmanagedType.Struct)] out object AssociateVertexes1,
//    [MarshalAs(UnmanagedType.Struct)] out object Points2,
//    [MarshalAs(UnmanagedType.Struct)] out object T2,
//    [MarshalAs(UnmanagedType.Struct)] out object Segments2,
//    [MarshalAs(UnmanagedType.Struct)] out object AssociateVertexes2);

//  [DispId(4010)]
//  ksRuledSurfaceSectionAlignmentTypeEnum SectionAlignmentType { [DispId(4010), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(4010), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(4011)]
//  bool SplitEdges { [DispId(4011), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(4011), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
