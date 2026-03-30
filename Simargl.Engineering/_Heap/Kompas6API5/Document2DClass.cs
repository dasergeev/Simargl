//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.Document2DClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ComSourceInterfaces("Kompas6API5.ksDocumentFileNotify\0\0")]
//[ClassInterface(ClassInterfaceType.None)]
//[Guid("14FD27F5-B7FD-4276-AC2C-2804EDC3944F")]
//[ComImport]
//public class Document2DClass : ksDocument2D, Document2D, ksDocumentFileNotify_Event
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern Document2DClass();

//  [DispId(1)]
//  public virtual extern int reference { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(212)]
//  public virtual extern bool orthoMode { [DispId(212), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(212), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLineSeg(double x1, double y1, double x2, double y2, int style);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksRectangle([MarshalAs(UnmanagedType.IDispatch)] object par, short centre = 0);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCircle(double xc, double yc, double rad, int style);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksArcByAngle(
//    double xc,
//    double yc,
//    double rad,
//    double f1,
//    double f2,
//    short direction,
//    int style);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksArcByPoint(
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
//  public virtual extern int ksArcBy3Points(
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    double x3,
//    double y3,
//    int style);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetObjParam(int @ref, [MarshalAs(UnmanagedType.IDispatch)] object param, int parType = -1);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetObjParam(int referObj, [MarshalAs(UnmanagedType.IDispatch)] object param, int parType = -1);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksContour(int style);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksMacro(short type);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksParagraph([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksTextLine([MarshalAs(UnmanagedType.IDispatch)] object textItem);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksUpdateMacro(int macro, int gr);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLine(double x, double y, double angle);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPoint(double x, double y, int style);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksText(
//    double x,
//    double y,
//    double ang,
//    double hStr,
//    double ksuStr,
//    int bitVector,
//    [MarshalAs(UnmanagedType.BStr)] string s);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksHatchByParam([MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksEndObj();

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLayer(int n);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetLayerNumber(int p);

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetLayerReference(int number);

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksChangeObjectLayer(int obj, int number);

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksNewGroup(short type);

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksEndGroup();

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksMakeEncloseContours(int gr, double x, double y);

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsCursorOrPlacementDocument();

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsActiveProcessRunnig();

//  [DispId(29)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPhantomShowHide([MarshalAs(UnmanagedType.BStr)] string show);

//  [DispId(30)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetCursorPosition(out double x, out double y, [In] int type);

//  [DispId(31 /*0x1F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksKeepReference(int r);

//  [DispId(32 /*0x20*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetMacroParamSize(int @ref);

//  [DispId(33)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksEditMacroMode();

//  [DispId(34)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteObj(int @ref);

//  [DispId(35)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksMoveObj(int @ref, double x, double y);

//  [DispId(36)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksRotateObj(int @ref, double x, double y, double angle);

//  [DispId(37)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksTransformObj(int @ref);

//  [DispId(38)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLightObj(int @ref, short light);

//  [DispId(39)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksFindObj(double x, double y, double limit);

//  [DispId(40)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSymmetryObj(
//    int @ref,
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    [MarshalAs(UnmanagedType.BStr)] string copy);

//  [DispId(41)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCopyObj(
//    int @ref,
//    double xOld,
//    double yOld,
//    double xNew,
//    double yNew,
//    double scale,
//    double angle);

//  [DispId(42)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCreateViewObject(int type);

//  [DispId(43)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksEditViewObject(int @ref);

//  [DispId(44)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnLineSeg(
//    double x1,
//    double y1,
//    double x2,
//    double y2,
//    short term1,
//    short term2,
//    int style);

//  [DispId(45)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPointArraw(double x, double y, double ang, short term);

//  [DispId(46)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnArcByPoint(
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
//  public virtual extern int ksEllipse([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(48 /*0x30*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksEllipseArc([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(49)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksParEllipseArc([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(50)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksEquidistant([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(51)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsPointInsideContour(int p, double x, double y, double precision);

//  [DispId(52)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksConvertTextToCurve(int text);

//  [DispId(53)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksStoreTmpGroup(int g);

//  [DispId(54)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksOpenMacro(int macro);

//  [DispId(55)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAddObjectToMacro(int macro, int obj);

//  [DispId(56)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksHatch(
//    int style,
//    double angle,
//    double step,
//    double width,
//    double x0,
//    double y0);

//  [DispId(57)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksColouring(int color);

//  [DispId(58)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDuplicateBoundaries(int p);

//  [DispId(59)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksNurbsPoint([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(60)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksNurbsKnot(double knot);

//  [DispId(61)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksNurbs(short degree, bool close, int style);

//  [DispId(62)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksRegularPolygon([MarshalAs(UnmanagedType.IDispatch)] object par, short centre = 0);

//  [DispId(63 /*0x3F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksClearGroup(int g, bool deleteTmp);

//  [DispId(64 /*0x40*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExcludeObjGroup(int g, int p);

//  [DispId(65)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAddObjGroup(int g, int p);

//  [DispId(66)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSaveGroup(int g, [MarshalAs(UnmanagedType.BStr)] string name);

//  [DispId(67)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSelectGroup(
//    int g,
//    short selectMode,
//    double xmin,
//    double ymin,
//    double xmax,
//    double ymax);

//  [DispId(68)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExistGroupObj(int g);

//  [DispId(69)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksWriteGroupToClip(int g, bool copy);

//  [DispId(70)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetGroup([MarshalAs(UnmanagedType.BStr)] string name);

//  [DispId(71)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReadGroupFromClip();

//  [DispId(72)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksConicArc([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(73)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCentreMarker([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(74)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksBezier(short closed, int style);

//  [DispId(75)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksBezierPoint([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(76)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetStyleParam(short type, short styleId, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(77)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksInsertRaster([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(78)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLinDimension([MarshalAs(UnmanagedType.IDispatch)] object linPar);

//  [DispId(79)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAngDimension([MarshalAs(UnmanagedType.IDispatch)] object angPar);

//  [DispId(80 /*0x50*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDiamDimension([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(81)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksRadDimension([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(82)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksRadBreakDimension([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(83)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksOrdinatedDimension([MarshalAs(UnmanagedType.IDispatch)] object ordPar);

//  [DispId(84)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAddStyle(short type, [MarshalAs(UnmanagedType.IDispatch)] object param, short copy);

//  [DispId(85)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsStyleInDocument(short type, [MarshalAs(UnmanagedType.IDispatch)] object param, short copy);

//  [DispId(86)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteStyleFromDocument(short type, [MarshalAs(UnmanagedType.IDispatch)] object param, short copy);

//  [DispId(87)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExistObj(int @ref);

//  [DispId(88)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetObjGabaritRect(int p, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(89)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSheetToView([In] double x, [In] double y, out double outX, out double outY);

//  [DispId(90)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksViewToSheet([In] double x, [In] double y, out double outX, out double outY);

//  [DispId(91)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPolyline(int style);

//  [DispId(92)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPolylineByParam([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(93)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetReferenceDocumentPart(short t);

//  [DispId(94)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetViewReference(int number);

//  [DispId(95)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetViewNumber(int p);

//  [DispId(96 /*0x60*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksOpenView(int number);

//  [DispId(97)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksNewViewNumber();

//  [DispId(98)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCreateSheetView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In, Out] ref int number);

//  [DispId(99)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDecomposeObj(int p, short level, double arrow, short type);

//  [DispId(100)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetViewObjCount(int p);

//  [DispId(101)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLinBreakDimension([MarshalAs(UnmanagedType.IDispatch)] object linPar);

//  [DispId(102)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAngBreakDimension([MarshalAs(UnmanagedType.IDispatch)] object angPar);

//  [DispId(103)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsCurveClosed(int p);

//  [DispId(104)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksBase([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(105)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksRough([MarshalAs(UnmanagedType.IDispatch)] object roughPar);

//  [DispId(106)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetGroupName([In] int gr, out int group, [In] int size);

//  [DispId(107)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksMtr(double x, double y, double angle, double scaleX, double scaleY);

//  [DispId(108)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteMtr();

//  [DispId(109)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPointIntoMtr([In] double x, [In] double y, out double xn, out double yn);

//  [DispId(110)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPointFromMtr([In] double x, [In] double y, out double xn, out double yn);

//  [DispId(111)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLengthIntoMtr([In, Out] ref double len);

//  [DispId(112 /*0x70*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLengthFromMtr([In, Out] ref double len);

//  [DispId(113)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLeader([MarshalAs(UnmanagedType.IDispatch)] object leaderPar);

//  [DispId(114)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPositionLeader([MarshalAs(UnmanagedType.IDispatch)] object posLeaderParam);

//  [DispId(115)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksBrandLeader([MarshalAs(UnmanagedType.IDispatch)] object brandLeaderParam);

//  [DispId(116)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksMarkerLeader([MarshalAs(UnmanagedType.IDispatch)] object markerLeaderParam);

//  [DispId(117)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCursor([MarshalAs(UnmanagedType.IDispatch), In] object info, [In, Out] ref double x, [In, Out] ref double y, [MarshalAs(UnmanagedType.IDispatch), In] object phantom);

//  [DispId(118)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPlacement(
//    [MarshalAs(UnmanagedType.IDispatch), In] object info,
//    [In, Out] ref double x,
//    [In, Out] ref double y,
//    [In, Out] ref double angle,
//    [MarshalAs(UnmanagedType.IDispatch), In] object phantom);

//  [DispId(119)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCommandWindow([MarshalAs(UnmanagedType.IDispatch)] object info);

//  [DispId(120)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksChangeObjectInLibRequest([MarshalAs(UnmanagedType.IDispatch)] object info, [MarshalAs(UnmanagedType.IDispatch)] object phantom);

//  [DispId(121)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReleaseReference(int p);

//  [DispId(122)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetFragment();

//  [DispId(123)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksInitFilePreviewFunc([MarshalAs(UnmanagedType.BStr)] string funcName, int hInst, [MarshalAs(UnmanagedType.IDispatch)] object dispatchOCX);

//  [DispId(124)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksTable();

//  [DispId(125)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksRebuildTableVirtualGrid();

//  [DispId(126)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetTableItemsCount(int type);

//  [DispId(127 /*0x7F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetTableColumnText([In, Out] ref int numb, [MarshalAs(UnmanagedType.IDispatch), In] object par);

//  [DispId(128 /*0x80*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetTableColumnText(int numb, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(129)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksClearTableColumnText(int numb);

//  [DispId(130)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCombineTwoTableItems(int index1, int index2);

//  [DispId(131)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDivideTableItem(int index, bool vertical, int style);

//  [DispId(132)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetTableBorderStyle(int index, short typeBorder, int style);

//  [DispId(133)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetTableBorderStyle(int index, short typeBorder);

//  [DispId(134)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksOpenTable(int table);

//  [DispId(135)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksColumnNumber(int numb);

//  [DispId(136)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetMacroParam(
//    int @ref,
//    [MarshalAs(UnmanagedType.IDispatch)] object userPars,
//    bool dblClickOff,
//    bool hotpoints,
//    bool externEdit);

//  [DispId(137)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetMacroParam(int @ref, [MarshalAs(UnmanagedType.IDispatch)] object userPars);

//  [DispId(138)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksOpenTechnicalDemand([MarshalAs(UnmanagedType.IDispatch)] object pGab, int style);

//  [DispId(139)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCloseTechnicalDemand();

//  [DispId(140)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpecRough([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(141)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksViewPointer([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(142)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCutLine([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(143)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksApproximationCurve(
//    int p,
//    double eps,
//    bool curentLayer,
//    double maxRad,
//    bool smooth);

//  [DispId(144 /*0x90*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksTolerance([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(145)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksOpenTolerance(int tolerance);

//  [DispId(146)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetToleranceColumnText([In, Out] ref int numb, [MarshalAs(UnmanagedType.IDispatch), In] object par);

//  [DispId(147)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetToleranceColumnText(int numb, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(148)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetPointOnToleranceTable(int tolerance, short entry, [MarshalAs(UnmanagedType.IDispatch)] object point);

//  [DispId(149)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksOpenDocument([MarshalAs(UnmanagedType.BStr)] string nameDoc, bool regim);

//  [DispId(150)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSaveDocument([MarshalAs(UnmanagedType.BStr)] string fileName);

//  [DispId(151)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksCloseDocument();

//  [DispId(152)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksCreateDocument([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(153)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksMovePoint([In, Out] ref double x, [In, Out] ref double y, [In] double ang, [In] double len);

//  [DispId(154)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReadTableFromFile([MarshalAs(UnmanagedType.BStr)] string tblFileName);

//  [DispId(155)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawKompasDocument(int HWindow, [MarshalAs(UnmanagedType.BStr)] string docFileName);

//  [DispId(156)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksViewGetObjectArea();

//  [DispId(157)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetMacroPlacement(
//    [In] int macro,
//    out double x,
//    out double y,
//    [In, Out] ref double angl);

//  [DispId(158)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetMacroPlacement(
//    int macro,
//    double x,
//    double y,
//    double angl,
//    int relativ);

//  [DispId(159)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawKompasGroup(int HWindow, int gr);

//  [DispId(160 /*0xA0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetDocVariableArray(int p);

//  [DispId(161)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetDocVariableArray(int obj, [MarshalAs(UnmanagedType.IDispatch)] object arr, bool setNote);

//  [DispId(162)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetDocOptions(int optionsType, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(163)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetObjConstraint(int obj, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(164)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetObjConstraints(int obj);

//  [DispId(165)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDestroyObjConstraint(int obj, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(166)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetZona([In] double x, [In] double y, out int result_);

//  [DispId(167)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksClearRegion(int grClear, int grRegion, bool inside);

//  [DispId(168)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksNurbsForConicCurve([MarshalAs(UnmanagedType.IDispatch)] object xArr, [MarshalAs(UnmanagedType.IDispatch)] object yArr, short style);

//  [DispId(169)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetStamp();

//  [DispId(170)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksGetTextLength([MarshalAs(UnmanagedType.BStr)] string text, int style);

//  [DispId(171)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksGetTextLengthFromReference(int pText);

//  [DispId(172)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksZoom(double x1, double y1, double x2, double y2);

//  [DispId(173)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksZoomScale(double x, double y, double scale);

//  [DispId(174)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksZoomPrevNextOrAll(short type);

//  [DispId(175)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetZoomScale(out double x, out double y, out double scale);

//  [DispId(176 /*0xB0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpecificationOnSheet(short onSheet);

//  [DispId(177)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetSpecification();

//  [DispId(178)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReDrawDocPart([MarshalAs(UnmanagedType.IDispatch)] object rect, int view);

//  [DispId(179)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksGetCursorLimit();

//  [DispId(180)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetTextAlign(int pText);

//  [DispId(181)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetTextAlign(int pText, int align);

//  [DispId(182)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetTextLineAlign(short align);

//  [DispId(183)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDestroyObjects(int gr);

//  [DispId(184)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksEnableUndo(bool enabl);

//  [DispId(185)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAddPowerForm(double x, double y);

//  [DispId(186)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCreatePowerArc();

//  [DispId(187)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksTrimNurbs(int pObj, double tMin, double tMax);

//  [DispId(188)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetDimensionVariableName(int dimObj);

//  [DispId(189)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksTrimmCurve(
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
//  public virtual extern bool SaveAsToRasterFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object rasterPar);

//  [DispId(191)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object RasterFormatParam();

//  [DispId(192 /*0xC0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksGetLeaderShelfLength([In] int leader, out double x, out double y);

//  [DispId(193)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetDocOptions(int optionsType, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(194)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAxisLine([MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(195)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SaveAsToUncompressedRasterFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object rasterPar);

//  [DispId(196)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCreateSheetArbitraryView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number);

//  [DispId(197)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksCreateSheetStandartViews(
//    [MarshalAs(UnmanagedType.IDispatch)] object par,
//    int bitVector,
//    double dx,
//    double dy);

//  [DispId(198)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCreateSheetProjectionView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number, [In] int view);

//  [DispId(199)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCreateSheetArrowView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number, [In] int obj);

//  [DispId(200)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCreateSheetSectionView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number, [In] int obj);

//  [DispId(201)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCreateSheetRemoteView([MarshalAs(UnmanagedType.IDispatch), In] object par, [In] ref int number, [In] int obj);

//  [DispId(202)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksRebuildDocument();

//  [DispId(203)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksRemoteElement([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(204)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCopyObjEx([MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(205)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  public virtual extern object GetObject2DNotify(int objType);

//  [DispId(206)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  public virtual extern object GetSelectionMngNotify();

//  [DispId(207)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Object2DNotifyResult GetObject2DNotifyResult();

//  [DispId(208 /*0xD0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  public virtual extern object GetDocument2DNotify();

//  [DispId(209)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetMaterialParam([MarshalAs(UnmanagedType.IDispatch)] object material, double density);

//  [DispId(210)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksChangeObjectsOrder(int group, int obj, int type);

//  [DispId(211)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsSlaveSpcOpened();

//  [DispId(213)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetMacroWaitDblClickEdit(int @ref);

//  [DispId(214)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetMacroWaitDblClickEdit(int @ref, int waitDblClick);

//  [DispId(215)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksParametrizeObjects(int obj, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(216)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCursorEx(
//    [MarshalAs(UnmanagedType.IDispatch), In] object info,
//    [In, Out] ref double x,
//    [In, Out] ref double y,
//    [MarshalAs(UnmanagedType.IDispatch), In] object phantom,
//    [MarshalAs(UnmanagedType.IDispatch), In] object processParam);

//  [DispId(217)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPlacementEx(
//    [MarshalAs(UnmanagedType.IDispatch), In] object info,
//    [In, Out] ref double x,
//    [In, Out] ref double y,
//    [In, Out] ref double angle,
//    [MarshalAs(UnmanagedType.IDispatch), In] object phantom,
//    [MarshalAs(UnmanagedType.IDispatch), In] object processParam);

//  [DispId(218)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksCalcRasterScale([MarshalAs(UnmanagedType.BStr)] string fileName, double w, double h);

//  [DispId(219)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksTextEx([MarshalAs(UnmanagedType.IDispatch)] object txtParam, int align);

//  [DispId(220)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksColouringEx(int color, int group);

//  [DispId(221)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSaveDocumentEx([MarshalAs(UnmanagedType.BStr)] string fileName, int SaveMode);

//  [DispId(222)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  public virtual extern object ksAssociationViewMatrix3D(int ViewRef);

//  [DispId(223)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksPoint3DToAssociationView(
//    [In] int view,
//    [In] double x3D,
//    [In] double y3D,
//    [In] double z3D,
//    out double x2D,
//    out double y2D);

//  [DispId(224 /*0xE0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetReferenceDocumentPartEx(short t, int SheetNumb);

//  [DispId(225)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetStampEx(int SheetNumb);

//  [DispId(226)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetDocumentPagesCount();

//  [DispId(227)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSaveToDXF([MarshalAs(UnmanagedType.BStr)] string DXFFileName);

//  [DispId(228)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksInitFilePreviewFuncW([MarshalAs(UnmanagedType.BStr)] string funcName, int hInst, [MarshalAs(UnmanagedType.IDispatch)] object dispatchOCX);

//  [DispId(229)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksChangeLeader([MarshalAs(UnmanagedType.IDispatch)] object leaderParam);

//  [DispId(230)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksGetShelfPoint(
//    [In] int p,
//    [In] int index,
//    out double x,
//    out double y,
//    [In] int paramType);

//  [DispId(231)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksMakeEncloseContoursEx(int gr, double x, double y, bool forHatch);

//  [DispId(232)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnPolylineEx([MarshalAs(UnmanagedType.IDispatch)] object par, short term1, short term2);

//  [DispId(233)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnEllipseArc([MarshalAs(UnmanagedType.IDispatch)] object par, short term1, short term2);

//  [DispId(234)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnParEllipseArc([MarshalAs(UnmanagedType.IDispatch)] object par, short term1, short term2);

//  [DispId(235)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnCircle(double xc, double yc, double rad, int style);

//  [DispId(236)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnEllipse([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(237)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnPolyline(int style, short term1, short term2);

//  [DispId(238)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnTextEx([MarshalAs(UnmanagedType.IDispatch)] object txtParam, int align);

//  [DispId(239)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAnnPoint(double x, double y, int style);

//  [DispId(240 /*0xF0*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksGetAnnObjTerminators([In] int annObj, out short term1, out short term2);

//  [DispId(241)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetObjectStyle(int obj);

//  [DispId(242)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSetObjectStyle(int obj, int style);

//  [DispId(243)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetMacroPlacementEx(
//    [In] int macro,
//    out double x,
//    out double y,
//    out double angl,
//    [In] int sheetParam,
//    out int mirrorSymmetry);

//  [DispId(244)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetMacroPlacementEx(
//    int macro,
//    double x,
//    double y,
//    double angl,
//    int relativ,
//    int mirrorSymmetry);

//  [DispId(245)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksUndoContainer(bool Add);

//  [DispId(246)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksGetEditMacroVisibleRegime(int p);

//  [DispId(247)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCopyGroupToDocument(int gr, int from);

//  [DispId(248)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetSnapInfo();

//  [DispId(249)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReDrawDocPartEx([MarshalAs(UnmanagedType.IDispatch)] object rect, int view, int paramType);

//  [DispId(250)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksShowHideTmpObj(int @ref, int show);

//  [DispId(251)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetLightObjType(int @ref, int light);

//  [DispId(252)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetObjectNameByType(int type);

//  [DispId(253)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetObjectsNameByType(int type);

//  [DispId(254)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetMixDlgMaterialParam([MarshalAs(UnmanagedType.BStr)] string material, double density);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCloseDocument(
//    [In] ksDocumentFileNotify_BeginCloseDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginCloseDocumentEventHandler BeginCloseDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCloseDocument(
//    [In] ksDocumentFileNotify_BeginCloseDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CloseDocument(
//    [In] ksDocumentFileNotify_CloseDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_CloseDocumentEventHandler CloseDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CloseDocument(
//    [In] ksDocumentFileNotify_CloseDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginSaveDocument(
//    [In] ksDocumentFileNotify_BeginSaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginSaveDocumentEventHandler BeginSaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginSaveDocument(
//    [In] ksDocumentFileNotify_BeginSaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SaveDocument([In] ksDocumentFileNotify_SaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_SaveDocumentEventHandler SaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SaveDocument([In] ksDocumentFileNotify_SaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_ActivateEventHandler Activate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Activate([In] ksDocumentFileNotify_ActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Activate([In] ksDocumentFileNotify_ActivateEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_DeactivateEventHandler Deactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Deactivate([In] ksDocumentFileNotify_DeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Deactivate([In] ksDocumentFileNotify_DeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginSaveAsDocument(
//    [In] ksDocumentFileNotify_BeginSaveAsDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginSaveAsDocumentEventHandler BeginSaveAsDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginSaveAsDocument(
//    [In] ksDocumentFileNotify_BeginSaveAsDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_DocumentFrameOpenEventHandler DocumentFrameOpen;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DocumentFrameOpen(
//    [In] ksDocumentFileNotify_DocumentFrameOpenEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DocumentFrameOpen(
//    [In] ksDocumentFileNotify_DocumentFrameOpenEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_ProcessActivateEventHandler ProcessActivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ProcessActivate(
//    [In] ksDocumentFileNotify_ProcessActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ProcessActivate(
//    [In] ksDocumentFileNotify_ProcessActivateEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_ProcessDeactivateEventHandler ProcessDeactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ProcessDeactivate(
//    [In] ksDocumentFileNotify_ProcessDeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ProcessDeactivate(
//    [In] ksDocumentFileNotify_ProcessDeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginProcess([In] ksDocumentFileNotify_BeginProcessEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginProcess([In] ksDocumentFileNotify_BeginProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndProcess([In] ksDocumentFileNotify_EndProcessEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndProcess([In] ksDocumentFileNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginAutoSaveDocument(
//    [In] ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler BeginAutoSaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginAutoSaveDocument(
//    [In] ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_AutoSaveDocument(
//    [In] ksDocumentFileNotify_AutoSaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_AutoSaveDocumentEventHandler AutoSaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_AutoSaveDocument(
//    [In] ksDocumentFileNotify_AutoSaveDocumentEventHandler obj0);
//}
