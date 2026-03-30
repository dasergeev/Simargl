//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.SpecificationClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[Guid("51E74526-9A3A-11D6-95CE-00C0262D30E3")]
//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[ClassInterface(ClassInterfaceType.None)]
//[ComSourceInterfaces("Kompas6API5.ksSpecificationNotify\0\0")]
//[ComImport]
//public class SpecificationClass : ksSpecification, Specification, ksSpecificationNotify_Event
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern SpecificationClass();

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpcObjectEnd();

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpcObjectEdit(int spcObj);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpcObjectCreate(
//    [MarshalAs(UnmanagedType.BStr)] string nameLib,
//    int styleNumb,
//    int secNumb,
//    int subSecNumb,
//    double numb,
//    short typeObj);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpcChangeValue(
//    int colNumb,
//    int itemNumb,
//    [MarshalAs(UnmanagedType.IDispatch)] object userPars,
//    short typeVal);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpcVisible(int colNumb, int itemNumb, short flagOn);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpcMassa([MarshalAs(UnmanagedType.BStr)] string sMassa);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpcCount(short ispoln, [MarshalAs(UnmanagedType.BStr)] string sCount);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpcPosition(int pos);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSpcIncludeReference(int obj, short Clear);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcObjForGeom(
//    [MarshalAs(UnmanagedType.BStr)] string nameLib,
//    int numb,
//    int obj,
//    short equal,
//    short First);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcObjForGeomWithLimit(
//    [MarshalAs(UnmanagedType.BStr)] string nameLib,
//    int numb,
//    int obj,
//    short equal,
//    short First,
//    int section,
//    double attrTypeNumb);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetSpcSectionName(int spcObj);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksEditWindowSpcObject(int obj);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksGetSpcObjectNumber(int spcObj);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcObject(double objNumb);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetSpcObjectColumnText(
//    int spcObj,
//    int columnType,
//    int ispoln,
//    int block);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetSpcObjectColumnText(
//    int columnType,
//    int ispoln,
//    int block,
//    [MarshalAs(UnmanagedType.BStr)] string str);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcTableColumn([MarshalAs(UnmanagedType.BStr)] string nameLib, int numb, short additioanalCol);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcColumnType(int spcObj, int colNumb, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcColumnNumb(int spcObj, int columnType, int ispoln, int block);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcStyleParam([MarshalAs(UnmanagedType.BStr)] string nameLib, int numb, [MarshalAs(UnmanagedType.IDispatch)] object par, int tPar);

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcDescription([In] int index, [MarshalAs(UnmanagedType.IDispatch), In] object param, out bool state);

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetSpcDescription(int index, [MarshalAs(UnmanagedType.IDispatch)] object param, short state);

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAddSpcDescription([MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteSpcDescription(int index);

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool D3SpcIncludePart([MarshalAs(UnmanagedType.IDispatch)] object part, bool fillTexts);

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int D3GetSpcObjForGeomWithLimit(
//    [MarshalAs(UnmanagedType.BStr)] string nameLib,
//    int numb,
//    [MarshalAs(UnmanagedType.IDispatch)] object part,
//    short First,
//    short section,
//    double attrTypeNumb);

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksGetWidthColumnSpc(int numColumn, bool cellOrText);

//  [DispId(29)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetCurrentSpcObject();

//  [DispId(30)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetCurrentSpcObject(int spcObj, int index);

//  [DispId(31 /*0x1F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetTuningSpcStyleParam(int index, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(32 /*0x20*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetTuningSpcStyleParam(int index, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(33)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcObjGeometry(int spcObj);

//  [DispId(34)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object D3GetSpcObjGeometry(int spcObj);

//  [DispId(35)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetSpcObjectColumnTextEx(
//    int spcObj,
//    int columnType,
//    int ispoln,
//    int block);

//  [DispId(36)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetSpcObjectColumnTextEx(
//    int columnType,
//    int ispoln,
//    int block,
//    [MarshalAs(UnmanagedType.IDispatch)] object arr);

//  [DispId(37)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  public virtual extern object GetSpcObjectNotify(int objType);

//  [DispId(38)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcObjGeometryEx(int spcObj, int geomMode);

//  [DispId(39)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSetSpcObjectColumnTextAlign(
//    int spcObj,
//    int columnNumber,
//    int lineIndex,
//    int align);

//  [DispId(40)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcObjectColumnTextAlign(
//    int spcObj,
//    int columnNumber,
//    int lineIndex);

//  [DispId(41)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksGetSpcObjectAttributeNumber(int spcObj);

//  [DispId(42)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSetSpcObjectAttributeNumber(int spcObj, double attrNumber);

//  [DispId(43)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksGetSpcObjectSummaryCount(int spcObj, int ispoln, int blockNumber);

//  [DispId(44)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSetSpcObjectMaterial(int spcObj, [MarshalAs(UnmanagedType.BStr)] string material, double density);

//  [DispId(45)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetSpcPerformanceName(int index, int ispoln, int block);

//  [DispId(46)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSetSpcPerformanceName(
//    int index,
//    int ispoln,
//    int block,
//    [MarshalAs(UnmanagedType.BStr)] string name);

//  [DispId(47)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSpcDocLinksClear(int doc);

//  [DispId(48 /*0x30*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSpcDocLinksClearEx(int doc, int mode);

//  [DispId(49)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksGetSpcPropertyFill(int spcObj);

//  [DispId(50)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSetSpcPropertyFill(int spcObj, int val);

//  [DispId(51)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetSpcObjectSummaryCountText(int spcObj, int ispoln, int block);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_TuningSpcStyleBeginChange(
//    [In] ksSpecificationNotify_TuningSpcStyleBeginChangeEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_TuningSpcStyleBeginChangeEventHandler TuningSpcStyleBeginChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_TuningSpcStyleBeginChange(
//    [In] ksSpecificationNotify_TuningSpcStyleBeginChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_TuningSpcStyleChange(
//    [In] ksSpecificationNotify_TuningSpcStyleChangeEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_TuningSpcStyleChangeEventHandler TuningSpcStyleChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_TuningSpcStyleChange(
//    [In] ksSpecificationNotify_TuningSpcStyleChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeCurrentSpcDescription(
//    [In] ksSpecificationNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_ChangeCurrentSpcDescriptionEventHandler ChangeCurrentSpcDescription;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeCurrentSpcDescription(
//    [In] ksSpecificationNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_SpcDescriptionAddEventHandler SpcDescriptionAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionAdd(
//    [In] ksSpecificationNotify_SpcDescriptionAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionAdd(
//    [In] ksSpecificationNotify_SpcDescriptionAddEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_SpcDescriptionRemoveEventHandler SpcDescriptionRemove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionRemove(
//    [In] ksSpecificationNotify_SpcDescriptionRemoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionRemove(
//    [In] ksSpecificationNotify_SpcDescriptionRemoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionBeginEdit(
//    [In] ksSpecificationNotify_SpcDescriptionBeginEditEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_SpcDescriptionBeginEditEventHandler SpcDescriptionBeginEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionBeginEdit(
//    [In] ksSpecificationNotify_SpcDescriptionBeginEditEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_SpcDescriptionEditEventHandler SpcDescriptionEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionEdit(
//    [In] ksSpecificationNotify_SpcDescriptionEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionEdit(
//    [In] ksSpecificationNotify_SpcDescriptionEditEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_SynchronizationBeginEventHandler SynchronizationBegin;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SynchronizationBegin(
//    [In] ksSpecificationNotify_SynchronizationBeginEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SynchronizationBegin(
//    [In] ksSpecificationNotify_SynchronizationBeginEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_SynchronizationEventHandler Synchronization;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Synchronization(
//    [In] ksSpecificationNotify_SynchronizationEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Synchronization(
//    [In] ksSpecificationNotify_SynchronizationEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_BeginCalcPositionsEventHandler BeginCalcPositions;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCalcPositions(
//    [In] ksSpecificationNotify_BeginCalcPositionsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCalcPositions(
//    [In] ksSpecificationNotify_BeginCalcPositionsEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_CalcPositionsEventHandler CalcPositions;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CalcPositions(
//    [In] ksSpecificationNotify_CalcPositionsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CalcPositions(
//    [In] ksSpecificationNotify_CalcPositionsEventHandler obj0);

//  public virtual extern event ksSpecificationNotify_BeginCreateObjectEventHandler BeginCreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCreateObject(
//    [In] ksSpecificationNotify_BeginCreateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCreateObject(
//    [In] ksSpecificationNotify_BeginCreateObjectEventHandler obj0);
//}
