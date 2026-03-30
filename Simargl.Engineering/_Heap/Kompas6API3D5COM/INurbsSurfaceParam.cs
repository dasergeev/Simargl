//// Decompiled with JetBrains decompiler
//// Type: Kompas6API3D5COM.INurbsSurfaceParam
//// Assembly: Kompas6API3D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 98DBB410-35A6-4482-8352-058793489E25
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API3D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API3D5COM;

//[Guid("A5A1CB44-5F2E-4059-86B3-4F5056EFF956")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface INurbsSurfaceParam
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  short GetDegree(int paramU);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetClose(int paramU);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetPeriodic(int paramU);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  INurbsPoint3dCollCollection GetPointCollection();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  INurbsKnotCollection GetKnotCollection(int paramU);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetNurbsParams(
//    [In] int closedV,
//    [In] int closedU,
//    out int degreeV,
//    out int degreeU,
//    out int nPV,
//    out int nPU,
//    [MarshalAs(UnmanagedType.Struct)] out object points,
//    [MarshalAs(UnmanagedType.Struct)] out object weights,
//    [MarshalAs(UnmanagedType.Struct)] out object knotsV,
//    [MarshalAs(UnmanagedType.Struct)] out object knotsU);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetBoundaryUVNurbs(
//    [In] int uv,
//    [In] int closed,
//    [In] int loopIndex,
//    [In] int edgeIndex,
//    out int degree,
//    [MarshalAs(UnmanagedType.Struct)] out object points,
//    [MarshalAs(UnmanagedType.Struct)] out object weights,
//    [MarshalAs(UnmanagedType.Struct)] out object knots,
//    out double tMin,
//    out double tMax);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetBoundaryCount();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetEdgesCount(int loopIndex);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetMinMaxParameters(
//    [In] int closedV,
//    [In] int closedU,
//    out double uMin,
//    out double uMax,
//    out double vMin,
//    out double vMax);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetUVPointFromBoundaryParameter(
//    [In] int uv,
//    [In] int closed,
//    [In] int loopIndex,
//    [In] int edgeIndex,
//    [In] double t,
//    out double u,
//    out double v);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetBoundaryParameterFromUVPoint(
//    [In] int uv,
//    [In] int closed,
//    [In] int loopIndex,
//    [In] int edgeIndex,
//    [In] double u,
//    [In] double v,
//    out double t);
//}
