//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksMathematic2D
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[Guid("F2D5AE01-45DE-4496-B01B-9958CAEF5943")]
//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[ComImport]
//public interface ksMathematic2D
//{
//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksCosD(double x);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksSinD(double x);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksTanD(double x);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksAtanD(double x);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksAngle(double x1, double y1, double x2, double y2);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksEqualPoints(double x1, double y1, double x2, double y2);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectLinSLinS(
//    double x11,
//    double y11,
//    double x12,
//    double y12,
//    double x21,
//    double y21,
//    double x22,
//    double y22,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectLinSLine(
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    double x,
//    double y,
//    double ang,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectArcLin(
//    double xc,
//    double yc,
//    double rad,
//    double f1,
//    double f2,
//    int n,
//    double x,
//    double y,
//    double ang,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectLinLin(
//    double x1,
//    double y1,
//    double angle1,
//    double x2,
//    double y2,
//    double angle2,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectCirCir(
//    double xc1,
//    double yc1,
//    double radius1,
//    double xc2,
//    double yc2,
//    double radius2,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectArcArc(
//    double xac,
//    double yac,
//    double rada,
//    double fa1,
//    double fa2,
//    short directa,
//    double xbc,
//    double ybc,
//    double radb,
//    double fb1,
//    double fb2,
//    int directb,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectLinSArc(
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    double xc,
//    double yc,
//    double rad,
//    double f1,
//    double f2,
//    short direct,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectLinSCir(
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    double xc,
//    double yc,
//    double rad,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectCirLin(
//    double xc,
//    double yc,
//    double rad,
//    double xl,
//    double yl,
//    double angle,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksIntersectCirArc(
//    double xcc,
//    double ycc,
//    double radc,
//    double xac,
//    double yac,
//    double rada,
//    double fa1,
//    double fa2,
//    short directa,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksIntersectCurvCurv(int p1, int p2, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksTanLinePointCircle(double x, double y, double xc, double yc, double rad, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksTanLineAngCircle(double xc, double yc, double rad, double ang, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksTanCircleCircle(
//    double xc1,
//    double yc1,
//    double radius1,
//    double xc2,
//    double yc2,
//    double radius2,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksTanLinePointCurve(double x, double y, int pCur, [MarshalAs(UnmanagedType.IDispatch)] object array);

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksCouplingLineLine(
//    double x1,
//    double y1,
//    double angle1,
//    double x2,
//    double y2,
//    double angle2,
//    double rad,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksCouplingLineCircle(
//    double xc,
//    double yc,
//    double radc,
//    double x1,
//    double y1,
//    double angle1,
//    double rad,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksCouplingCircleCircle(
//    double xc1,
//    double yc1,
//    double radc1,
//    double xc2,
//    double yc2,
//    double radc2,
//    double rad,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksSymmetry(
//    [In] double x,
//    [In] double y,
//    [In] double x1,
//    [In] double y1,
//    [In] double x2,
//    [In] double y2,
//    out double xc,
//    out double yc);

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksRotate(
//    [In] double x,
//    [In] double y,
//    [In] double xc,
//    [In] double yc,
//    [In] double ang,
//    out double xr,
//    out double yr);

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksDistancePntPnt(double x1, double y1, double x2, double y2);

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksDistancePntLineSeg(double x, double y, double x1, double y1, double x2, double y2);

//  [DispId(29)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksDistancePntArc(
//    double x,
//    double y,
//    double xac,
//    double yac,
//    double rada,
//    double fa1,
//    double fa2,
//    short directa);

//  [DispId(30)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksDistancePntCircle(double x, double y, double xc, double yc, double rad);

//  [DispId(31 /*0x1F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksDistancePntLine(double x, double y, double x1, double y1, double angle);

//  [DispId(32 /*0x20*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksDistancePntLineForPoint(
//    double x,
//    double y,
//    double x1,
//    double y1,
//    double x2,
//    double y2);

//  [DispId(33)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksPerpendicular(
//    [In] double x,
//    [In] double y,
//    [In] double x1,
//    [In] double y1,
//    [In] double x2,
//    [In] double y2,
//    out double xp,
//    out double yp);

//  [DispId(34)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object ksPointsOnCurve(int curve, int count);

//  [DispId(35)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksGetCurvePerpendicular(int curve, double x, double y);

//  [DispId(36)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetCurvePointProjection([In] int curve, [In] double x, [In] double y, out double kx, out double ky);

//  [DispId(37)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksMovePointOnCurve([In] int curve, [In, Out] ref double x, [In, Out] ref double y, [In] double len, [In] int dir);

//  [DispId(38)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCalcInertiaProperties(int p, [MarshalAs(UnmanagedType.IDispatch)] object prop, short dimension);

//  [DispId(39)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCalcMassInertiaProperties(int p, [MarshalAs(UnmanagedType.IDispatch)] object prop, double density, double param);

//  [DispId(40)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksGetCurvePerimeter(int curve, short dimension);

//  [DispId(41)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object ksPointsOnCurveByStep(int curve, double step);

//  [DispId(42)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksDistancePntPntOnCurve(int curve, double x1, double y1, double x2, double y2);

//  [DispId(43)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetCurvePointProjectionEx(
//    [In] int curve,
//    [In] double x,
//    [In] double y,
//    out double kx,
//    out double ky,
//    out double t);

//  [DispId(44)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetCurvePoint([In] int curve, [In] double t, out double x, out double y);

//  [DispId(45)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetCurveMinMaxParametr([In] int curve, out double tMin, out double tMax);

//  [DispId(46)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksDistanceT1T2OnCurve(int curve, double t1, double t2);

//  [DispId(47)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksTanCurvCurv(int p1, int p2, [MarshalAs(UnmanagedType.IDispatch)] object pointArr1, [MarshalAs(UnmanagedType.IDispatch)] object pointArr2);

//  [DispId(48 /*0x30*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksIntersectCurvCurvEx(int p1, int p2, [MarshalAs(UnmanagedType.IDispatch)] object param, bool touchInclude);

//  [DispId(49)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksTanLineAngCurve(int p, double ang, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(50)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksLinePointTangentCurve(int p, double xc, double yc, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(51)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksMovePointOnCurveEx(
//    [In] int curve,
//    [In, Out] ref double x,
//    [In, Out] ref double y,
//    [In, Out] ref double t,
//    [In] double len,
//    [In] int dir,
//    [In] int ext);

//  [DispId(52)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksGetCurvePerpendicularByT([In] int curve, [In] double t);

//  [DispId(53)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDistanceCurveCurve([In] int p1, [In] int p2, out double distanse, out double t1, out double t2);
//}
