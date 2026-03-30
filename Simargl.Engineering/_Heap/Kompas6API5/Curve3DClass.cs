//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.Curve3DClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[Guid("54152184-0B08-4DFB-8249-4579A7368BF4")]
//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class Curve3DClass : ksCurve3D, Curve3D
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern Curve3DClass();

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetPoint([In] double paramT, out double x, out double y, out double z);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetTangentVector(
//    [In] double paramT,
//    out double x,
//    out double y,
//    out double z);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetNormal([In] double paramT, out double x, out double y, out double z);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeT(
//    [In] double paramT,
//    out double x,
//    out double y,
//    out double z);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeTT(
//    [In] double paramT,
//    out double x,
//    out double y,
//    out double z);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeTTT(
//    [In] double paramT,
//    out double x,
//    out double y,
//    out double z);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double GetParamMin();

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double GetParamMax();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsClosed();

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsPeriodic();

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double GetMetricLength(double startParam, double endParam);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetGabarit(
//    out double x1,
//    out double y1,
//    out double z1,
//    out double x2,
//    out double y2,
//    out double z2);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsDegenerate();

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsPlanar();

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsLineSeg();

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsArc();

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsCircle();

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsEllipse();

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsNurbs();

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetCurveParam();

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double GetLength(uint bitVector);

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool NearPointProjection(
//    [In] double x,
//    [In] double y,
//    [In] double z,
//    out double t,
//    [In] bool ext);

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetNurbs3dParam();

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  public virtual extern object CalculatePolygon(double step);

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsEllipseArc();

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsStraight();
//}
