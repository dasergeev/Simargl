//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.NurbsSurfaceParamClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[ClassInterface(ClassInterfaceType.None)]
//[Guid("BA13BE42-059B-4EEB-9C39-673732763EE3")]
//[ComImport]
//public class NurbsSurfaceParamClass : ksNurbsSurfaceParam, NurbsSurfaceParam
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern NurbsSurfaceParamClass();

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern short GetDegree(bool paramU);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetClose(bool paramU);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetPointCollection();

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetKnotCollection(bool paramU);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetPeriodic(bool paramU);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetNurbsParams(
//    [In] bool closedV,
//    [In] bool closedU,
//    out int degreeV,
//    out int degreeU,
//    out int nPV,
//    out int nPU,
//    [MarshalAs(UnmanagedType.Struct)] out object points,
//    [MarshalAs(UnmanagedType.Struct)] out object weights,
//    [MarshalAs(UnmanagedType.Struct)] out object knotsV,
//    [MarshalAs(UnmanagedType.Struct)] out object knotsU);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetBoundaryUVNurbs(
//    [In] bool uv,
//    [In] bool closed,
//    [In] int loopIndex,
//    [In] int edgeIndex,
//    out int degree,
//    [MarshalAs(UnmanagedType.Struct)] out object points,
//    [MarshalAs(UnmanagedType.Struct)] out object weights,
//    [MarshalAs(UnmanagedType.Struct)] out object knots,
//    out double tMin,
//    out double tMax);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int GetBoundaryCount();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int GetEdgesCount(int loopIndex);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetMinMaxParameters(
//    [In] bool closedV,
//    [In] bool closedU,
//    out double uMin,
//    out double uMax,
//    out double vMin,
//    out double vMax);
//}
