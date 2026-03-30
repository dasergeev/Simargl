//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksDocument2D
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[Guid("AF4E160D-5C89-4F21-B0F2-D53397BDAF78")]
//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[ComImport]
//public interface ksDocument2D
//{
//  [DispId(1)]
//  int reference { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(212)]
//  bool orthoMode { [DispId(212), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(212), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksLineSeg(double x1, double y1, double x2, double y2, int style);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksRectangle([MarshalAs(UnmanagedType.IDispatch)] object par, short centre = 0);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCircle(double xc, double yc, double rad, int style);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksArcByAngle(
//    double xc,
//    double yc,
//    double rad,
//    double f1,
//    double f2,
//    short direction,
//    int style);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksArcByPoint(
//    double xc,
//    double yc,
//    double rad,
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    short direction,
//    int style);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksArcBy3Points(
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    double x3,
//    double y3,
//    int style);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetObjParam(int @ref, [MarshalAs(UnmanagedType.IDispatch)] object param, int parType = -1);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetObjParam(int referObj, [MarshalAs(UnmanagedType.IDispatch)] object param, int parType = -1);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksContour(int style);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksMacro(short type);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksParagraph([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksTextLine([MarshalAs(UnmanagedType.IDispatch)] object textItem);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksUpdateMacro(int macro, int gr);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksLine(double x, double y, double angle);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPoint(double x, double y, int style);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksText(
//    double x,
//    double y,
//    double ang,
//    double hStr,
//    double ksuStr,
//    int bitVector,
//    [MarshalAs(UnmanagedType.BStr)] string s);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksHatchByParam([MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksEndObj();

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksLayer(int n);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetLayerNumber(int p);

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetLayerReference(int number);

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksChangeObjectLayer(int obj, int number);

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksNewGroup(short type);

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksEndGroup();

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksMakeEncloseContours(int gr, double x, double y);

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksIsCursorOrPlacementDocument();

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksIsActiveProcessRunnig();

//  [DispId(29)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPhantomShowHide([MarshalAs(UnmanagedType.BStr)] string show);

//  [DispId(30)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetCursorPosition(out double x, out double y, [In] int type);

//  [DispId(31 /*0x1F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksKeepReference(int r);

//  [DispId(32 /*0x20*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetMacroParamSize(int @ref);

//  [DispId(33)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksEditMacroMode();

//  [DispId(34)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDeleteObj(int @ref);

//  [DispId(35)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksMoveObj(int @ref, double x, double y);

//  [DispId(36)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksRotateObj(int @ref, double x, double y, double angle);

//  [DispId(37)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksTransformObj(int @ref);

//  [DispId(38)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksLightObj(int @ref, short light);

//  [DispId(39)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksFindObj(double x, double y, double limit);

//  [DispId(40)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSymmetryObj(int @ref, double x1, double y1, double x2, double y2, [MarshalAs(UnmanagedType.BStr)] string copy);

//  [DispId(41)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCopyObj(
//    int @ref,
//    double xOld,
//    double yOld,
//    double xNew,
//    double yNew,
//    double scale,
//    double angle);

//  [DispId(42)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCreateViewObject(int type);

//  [DispId(43)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksEditViewObject(int @ref);

//  [DispId(44)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnLineSeg(
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    short term1,
//    short term2,
//    int style);

//  [DispId(45)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPointArraw(double x, double y, double ang, short term);

//  [DispId(46)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnArcByPoint(
//    double xc,
//    double yc,
//    double rad,
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    short direction,
//    short term1,
//    short term2,
//    int style);

//  [DispId(47)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksEllipse([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(48 /*0x30*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksEllipseArc([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(49)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksParEllipseArc([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(50)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksEquidistant([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(51)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksIsPointInsideContour(int p, double x, double y, double precision);

//  [DispId(52)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksConvertTextToCurve(int text);

//  [DispId(53)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksStoreTmpGroup(int g);

//  [DispId(54)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksOpenMacro(int macro);

//  [DispId(55)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAddObjectToMacro(int macro, int obj);

//  [DispId(56)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksHatch(int style, double angle, double step, double width, double x0, double y0);

//  [DispId(57)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksColouring(int color);

//  [DispId(58)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDuplicateBoundaries(int p);

//  [DispId(59)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksNurbsPoint([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(60)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksNurbsKnot(double knot);

//  [DispId(61)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksNurbs(short degree, bool close, int style);

//  [DispId(62)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksRegularPolygon([MarshalAs(UnmanagedType.IDispatch)] object par, short centre = 0);

//  [DispId(63 /*0x3F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksClearGroup(int g, bool deleteTmp);

//  [DispId(64 /*0x40*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksExcludeObjGroup(int g, int p);

//  [DispId(65)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAddObjGroup(int g, int p);

//  [DispId(66)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSaveGroup(int g, [MarshalAs(UnmanagedType.BStr)] string name);

//  [DispId(67)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSelectGroup(int g, short selectMode, double xmin, double ymin, double xmax, double ymax);

//  [DispId(68)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksExistGroupObj(int g);

//  [DispId(69)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksWriteGroupToClip(int g, bool copy);

//  [DispId(70)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetGroup([MarshalAs(UnmanagedType.BStr)] string name);

//  [DispId(71)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksReadGroupFromClip();

//  [DispId(72)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksConicArc([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(73)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCentreMarker([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(74)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksBezier(short closed, int style);

//  [DispId(75)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksBezierPoint([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(76)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetStyleParam(short type, short styleId, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(77)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksInsertRaster([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(78)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksLinDimension([MarshalAs(UnmanagedType.IDispatch)] object linPar);

//  [DispId(79)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAngDimension([MarshalAs(UnmanagedType.IDispatch)] object angPar);

//  [DispId(80 /*0x50*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDiamDimension([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(81)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksRadDimension([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(82)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksRadBreakDimension([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(83)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksOrdinatedDimension([MarshalAs(UnmanagedType.IDispatch)] object ordPar);

//  [DispId(84)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAddStyle(short type, [MarshalAs(UnmanagedType.IDispatch)] object param, short copy);

//  [DispId(85)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksIsStyleInDocument(short type, [MarshalAs(UnmanagedType.IDispatch)] object param, short copy);

//  [DispId(86)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDeleteStyleFromDocument(short type, [MarshalAs(UnmanagedType.IDispatch)] object param, short copy);

//  [DispId(87)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksExistObj(int @ref);

//  [DispId(88)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetObjGabaritRect(int p, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(89)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSheetToView([In] double x, [In] double y, out double outX, out double outY);

//  [DispId(90)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksViewToSheet([In] double x, [In] double y, out double outX, out double outY);

//  [DispId(91)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPolyline(int style);

//  [DispId(92)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPolylineByParam([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(93)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetReferenceDocumentPart(short t);

//  [DispId(94)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetViewReference(int number);

//  [DispId(95)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetViewNumber(int p);

//  [DispId(96 /*0x60*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksOpenView(int number);

//  [DispId(97)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksNewViewNumber();

//  [DispId(98)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCreateSheetView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In, Out] ref int number);

//  [DispId(99)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDecomposeObj(int p, short level, double arrow, short type);

//  [DispId(100)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetViewObjCount(int p);

//  [DispId(101)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksLinBreakDimension([MarshalAs(UnmanagedType.IDispatch)] object linPar);

//  [DispId(102)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAngBreakDimension([MarshalAs(UnmanagedType.IDispatch)] object angPar);

//  [DispId(103)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksIsCurveClosed(int p);

//  [DispId(104)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksBase([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(105)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksRough([MarshalAs(UnmanagedType.IDispatch)] object roughPar);

//  [DispId(106)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string ksGetGroupName([In] int gr, out int group, [In] int size);

//  [DispId(107)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksMtr(double x, double y, double angle, double scaleX, double scaleY);

//  [DispId(108)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDeleteMtr();

//  [DispId(109)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPointIntoMtr([In] double x, [In] double y, out double xn, out double yn);

//  [DispId(110)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPointFromMtr([In] double x, [In] double y, out double xn, out double yn);

//  [DispId(111)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksLengthIntoMtr([In, Out] ref double len);

//  [DispId(112 /*0x70*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksLengthFromMtr([In, Out] ref double len);

//  [DispId(113)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksLeader([MarshalAs(UnmanagedType.IDispatch)] object leaderPar);

//  [DispId(114)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPositionLeader([MarshalAs(UnmanagedType.IDispatch)] object posLeaderParam);

//  [DispId(115)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksBrandLeader([MarshalAs(UnmanagedType.IDispatch)] object brandLeaderParam);

//  [DispId(116)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksMarkerLeader([MarshalAs(UnmanagedType.IDispatch)] object markerLeaderParam);

//  [DispId(117)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCursor([MarshalAs(UnmanagedType.IDispatch), In] object info, [In, Out] ref double x, [In, Out] ref double y, [MarshalAs(UnmanagedType.IDispatch), In] object phantom);

//  [DispId(118)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPlacement([MarshalAs(UnmanagedType.IDispatch), In] object info, [In, Out] ref double x, [In, Out] ref double y, [In, Out] ref double angle, [MarshalAs(UnmanagedType.IDispatch), In] object phantom);

//  [DispId(119)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCommandWindow([MarshalAs(UnmanagedType.IDispatch)] object info);

//  [DispId(120)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksChangeObjectInLibRequest([MarshalAs(UnmanagedType.IDispatch)] object info, [MarshalAs(UnmanagedType.IDispatch)] object phantom);

//  [DispId(121)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksReleaseReference(int p);

//  [DispId(122)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object GetFragment();

//  [DispId(123)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksInitFilePreviewFunc([MarshalAs(UnmanagedType.BStr)] string funcName, int hInst, [MarshalAs(UnmanagedType.IDispatch)] object dispatchOCX);

//  [DispId(124)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksTable();

//  [DispId(125)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksRebuildTableVirtualGrid();

//  [DispId(126)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetTableItemsCount(int type);

//  [DispId(127 /*0x7F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetTableColumnText([In, Out] ref int numb, [MarshalAs(UnmanagedType.IDispatch), In] object par);

//  [DispId(128 /*0x80*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetTableColumnText(int numb, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(129)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksClearTableColumnText(int numb);

//  [DispId(130)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCombineTwoTableItems(int index1, int index2);

//  [DispId(131)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDivideTableItem(int index, bool vertical, int style);

//  [DispId(132)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetTableBorderStyle(int index, short typeBorder, int style);

//  [DispId(133)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetTableBorderStyle(int index, short typeBorder);

//  [DispId(134)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksOpenTable(int table);

//  [DispId(135)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksColumnNumber(int numb);

//  [DispId(136)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetMacroParam(
//    int @ref,
//    [MarshalAs(UnmanagedType.IDispatch)] object userPars,
//    bool dblClickOff,
//    bool hotpoints,
//    bool externEdit);

//  [DispId(137)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetMacroParam(int @ref, [MarshalAs(UnmanagedType.IDispatch)] object userPars);

//  [DispId(138)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksOpenTechnicalDemand([MarshalAs(UnmanagedType.IDispatch)] object pGab, int style);

//  [DispId(139)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCloseTechnicalDemand();

//  [DispId(140)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSpecRough([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(141)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksViewPointer([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(142)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCutLine([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(143)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksApproximationCurve(int p, double eps, bool curentLayer, double maxRad, bool smooth);

//  [DispId(144 /*0x90*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksTolerance([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(145)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksOpenTolerance(int tolerance);

//  [DispId(146)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetToleranceColumnText([In, Out] ref int numb, [MarshalAs(UnmanagedType.IDispatch), In] object par);

//  [DispId(147)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetToleranceColumnText(int numb, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(148)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetPointOnToleranceTable(int tolerance, short entry, [MarshalAs(UnmanagedType.IDispatch)] object point);

//  [DispId(149)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksOpenDocument([MarshalAs(UnmanagedType.BStr)] string nameDoc, bool regim);

//  [DispId(150)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksSaveDocument([MarshalAs(UnmanagedType.BStr)] string fileName);

//  [DispId(151)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksCloseDocument();

//  [DispId(152)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksCreateDocument([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(153)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksMovePoint([In, Out] ref double x, [In, Out] ref double y, [In] double ang, [In] double len);

//  [DispId(154)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksReadTableFromFile([MarshalAs(UnmanagedType.BStr)] string tblFileName);

//  [DispId(155)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDrawKompasDocument(int HWindow, [MarshalAs(UnmanagedType.BStr)] string docFileName);

//  [DispId(156)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksViewGetObjectArea();

//  [DispId(157)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetMacroPlacement([In] int macro, out double x, out double y, [In, Out] ref double angl);

//  [DispId(158)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetMacroPlacement(int macro, double x, double y, double angl, int relativ);

//  [DispId(159)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDrawKompasGroup(int HWindow, int gr);

//  [DispId(160 /*0xA0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object ksGetDocVariableArray(int p);

//  [DispId(161)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetDocVariableArray(int obj, [MarshalAs(UnmanagedType.IDispatch)] object arr, bool setNote);

//  [DispId(162)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetDocOptions(int optionsType, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(163)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetObjConstraint(int obj, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(164)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object ksGetObjConstraints(int obj);

//  [DispId(165)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDestroyObjConstraint(int obj, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(166)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string ksGetZona([In] double x, [In] double y, out int result_);

//  [DispId(167)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksClearRegion(int grClear, int grRegion, bool inside);

//  [DispId(168)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksNurbsForConicCurve([MarshalAs(UnmanagedType.IDispatch)] object xArr, [MarshalAs(UnmanagedType.IDispatch)] object yArr, short style);

//  [DispId(169)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object GetStamp();

//  [DispId(170)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksGetTextLength([MarshalAs(UnmanagedType.BStr)] string text, int style);

//  [DispId(171)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksGetTextLengthFromReference(int pText);

//  [DispId(172)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksZoom(double x1, double y1, double x2, double y2);

//  [DispId(173)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksZoomScale(double x, double y, double scale);

//  [DispId(174)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksZoomPrevNextOrAll(short type);

//  [DispId(175)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetZoomScale(out double x, out double y, out double scale);

//  [DispId(176 /*0xB0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSpecificationOnSheet(short onSheet);

//  [DispId(177)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object GetSpecification();

//  [DispId(178)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksReDrawDocPart([MarshalAs(UnmanagedType.IDispatch)] object rect, int view);

//  [DispId(179)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksGetCursorLimit();

//  [DispId(180)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetTextAlign(int pText);

//  [DispId(181)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetTextAlign(int pText, int align);

//  [DispId(182)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetTextLineAlign(short align);

//  [DispId(183)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksDestroyObjects(int gr);

//  [DispId(184)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksEnableUndo(bool enabl);

//  [DispId(185)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAddPowerForm(double x, double y);

//  [DispId(186)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCreatePowerArc();

//  [DispId(187)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksTrimNurbs(int pObj, double tMin, double tMax);

//  [DispId(188)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string ksGetDimensionVariableName(int dimObj);

//  [DispId(189)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksTrimmCurve(
//    int curve,
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    double x3,
//    double y3,
//    short deleteOldCurve);

//  [DispId(190)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SaveAsToRasterFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object rasterPar);

//  [DispId(191)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object RasterFormatParam();

//  [DispId(192 /*0xC0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksGetLeaderShelfLength([In] int leader, out double x, out double y);

//  [DispId(193)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetDocOptions(int optionsType, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(194)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAxisLine([MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(195)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SaveAsToUncompressedRasterFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object rasterPar);

//  [DispId(196)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCreateSheetArbitraryView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number);

//  [DispId(197)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksCreateSheetStandartViews([MarshalAs(UnmanagedType.IDispatch)] object par, int bitVector, double dx, double dy);

//  [DispId(198)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCreateSheetProjectionView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number, [In] int view);

//  [DispId(199)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCreateSheetArrowView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number, [In] int obj);

//  [DispId(200)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCreateSheetSectionView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number, [In] int obj);

//  [DispId(201)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCreateSheetRemoteView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number, [In] int obj);

//  [DispId(202)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksRebuildDocument();

//  [DispId(203)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksRemoteElement([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(204)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCopyObjEx([MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(205)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  object GetObject2DNotify(int objType);

//  [DispId(206)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  object GetSelectionMngNotify();

//  [DispId(207)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Object2DNotifyResult GetObject2DNotifyResult();

//  [DispId(208 /*0xD0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  object GetDocument2DNotify();

//  [DispId(209)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetMaterialParam([MarshalAs(UnmanagedType.IDispatch)] object material, double density);

//  [DispId(210)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksChangeObjectsOrder(int group, int obj, int type);

//  [DispId(211)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksIsSlaveSpcOpened();

//  [DispId(213)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetMacroWaitDblClickEdit(int @ref);

//  [DispId(214)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetMacroWaitDblClickEdit(int @ref, int waitDblClick);

//  [DispId(215)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksParametrizeObjects(int obj, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(216)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCursorEx([MarshalAs(UnmanagedType.IDispatch), In] object info, [In, Out] ref double x, [In, Out] ref double y, [MarshalAs(UnmanagedType.IDispatch), In] object phantom, [MarshalAs(UnmanagedType.IDispatch), In] object processParam);

//  [DispId(217)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksPlacementEx(
//    [MarshalAs(UnmanagedType.IDispatch), In] object info,
//    [In, Out] ref double x,
//    [In, Out] ref double y,
//    [In, Out] ref double angle,
//    [MarshalAs(UnmanagedType.IDispatch), In] object phantom,
//    [MarshalAs(UnmanagedType.IDispatch), In] object processParam);

//  [DispId(218)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double ksCalcRasterScale([MarshalAs(UnmanagedType.BStr)] string fileName, double w, double h);

//  [DispId(219)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksTextEx([MarshalAs(UnmanagedType.IDispatch)] object txtParam, int align);

//  [DispId(220)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksColouringEx(int color, int group);

//  [DispId(221)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksSaveDocumentEx([MarshalAs(UnmanagedType.BStr)] string fileName, int SaveMode);

//  [DispId(222)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object ksAssociationViewMatrix3D(int ViewRef);

//  [DispId(223)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksPoint3DToAssociationView(
//    [In] int view,
//    [In] double x3D,
//    [In] double y3D,
//    [In] double z3D,
//    out double x2D,
//    out double y2D);

//  [DispId(224 /*0xE0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetReferenceDocumentPartEx(short t, int SheetNumb);

//  [DispId(225)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object GetStampEx(int SheetNumb);

//  [DispId(226)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetDocumentPagesCount();

//  [DispId(227)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksSaveToDXF([MarshalAs(UnmanagedType.BStr)] string DXFFileName);

//  [DispId(228)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksInitFilePreviewFuncW([MarshalAs(UnmanagedType.BStr)] string funcName, int hInst, [MarshalAs(UnmanagedType.IDispatch)] object dispatchOCX);

//  [DispId(229)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksChangeLeader([MarshalAs(UnmanagedType.IDispatch)] object leaderParam);

//  [DispId(230)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksGetShelfPoint([In] int p, [In] int index, out double x, out double y, [In] int paramType);

//  [DispId(231)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksMakeEncloseContoursEx(int gr, double x, double y, bool forHatch);

//  [DispId(232)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnPolylineEx([MarshalAs(UnmanagedType.IDispatch)] object par, short term1, short term2);

//  [DispId(233)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnEllipseArc([MarshalAs(UnmanagedType.IDispatch)] object par, short term1, short term2);

//  [DispId(234)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnParEllipseArc([MarshalAs(UnmanagedType.IDispatch)] object par, short term1, short term2);

//  [DispId(235)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnCircle(double xc, double yc, double rad, int style);

//  [DispId(236)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnEllipse([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(237)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnPolyline(int style, short term1, short term2);

//  [DispId(238)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnTextEx([MarshalAs(UnmanagedType.IDispatch)] object txtParam, int align);

//  [DispId(239)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksAnnPoint(double x, double y, int style);

//  [DispId(240 /*0xF0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksGetAnnObjTerminators([In] int annObj, out short term1, out short term2);

//  [DispId(241)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetObjectStyle(int obj);

//  [DispId(242)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksSetObjectStyle(int obj, int style);

//  [DispId(243)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksGetMacroPlacementEx(
//    [In] int macro,
//    out double x,
//    out double y,
//    out double angl,
//    [In] int sheetParam,
//    out int mirrorSymmetry);

//  [DispId(244)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetMacroPlacementEx(
//    int macro,
//    double x,
//    double y,
//    double angl,
//    int relativ,
//    int mirrorSymmetry);

//  [DispId(245)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksUndoContainer(bool Add);

//  [DispId(246)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ksGetEditMacroVisibleRegime(int p);

//  [DispId(247)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksCopyGroupToDocument(int gr, int from);

//  [DispId(248)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object ksGetSnapInfo();

//  [DispId(249)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksReDrawDocPartEx([MarshalAs(UnmanagedType.IDispatch)] object rect, int view, int paramType);

//  [DispId(250)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksShowHideTmpObj(int @ref, int show);

//  [DispId(251)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetLightObjType(int @ref, int light);

//  [DispId(252)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string ksGetObjectNameByType(int type);

//  [DispId(253)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string ksGetObjectsNameByType(int type);

//  [DispId(254)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ksSetMixDlgMaterialParam([MarshalAs(UnmanagedType.BStr)] string material, double density);
//}
