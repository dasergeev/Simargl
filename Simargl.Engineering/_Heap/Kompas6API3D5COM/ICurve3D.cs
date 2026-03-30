//// Decompiled with JetBrains decompiler
//// Type: Kompas6API3D5COM.ICurve3D
//// Assembly: Kompas6API3D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 98DBB410-35A6-4482-8352-058793489E25
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API3D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API3D5COM;

//[Guid("E5066490-773D-4289-A60B-2FC19865174A")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface ICurve3D
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetPoint([In] double paramT, out double x, out double y, out double z);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetTangentVector([In] double paramT, [In] ref double x, [In] ref double y, [In] ref double z);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetNormal([In] double paramT, out double x, out double y, out double z);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetDerivativeT([In] double paramT, out double x, out double y, out double z);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetDerivativeTT([In] double paramT, out double x, out double y, out double z);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetDerivativeTTT([In] double paramT, out double x, out double y, out double z);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double GetParamMin();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double GetParamMax();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsClosed();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsPeriodic();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double GetMetricLength(double startParam, double endParam);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetGabarit(
//    out double x1,
//    out double y1,
//    out double z1,
//    out double x2,
//    out double y2,
//    out double z2);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsDegenerate();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsPlanar();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsLineSeg();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsArc();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsCircle();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsEllipse();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsNurbs();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  object GetCurveParam();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double GetLength(uint bitVector);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int NearPointProjection([In] double x, [In] double y, [In] double z, out double t, [In] int ext);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  INurbs3dParam GetNurbs3dParam();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int CalculatePolygon([In] double step, [MarshalAs(UnmanagedType.Struct)] out object points);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsEllipseArc();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsStraight();
//}
