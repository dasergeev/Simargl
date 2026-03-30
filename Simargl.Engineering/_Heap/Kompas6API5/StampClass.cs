//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.StampClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[Guid("FBCC5BA7-996C-11D6-8732-00C0262CDD2C")]
//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[ComSourceInterfaces("Kompas6API5.ksStampNotify\0\0")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class StampClass : ksStamp, Stamp, ksStampNotify_Event
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern StampClass();

//  [DispId(1)]
//  public virtual extern int reference { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksOpenStamp();

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCloseStamp();

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksClearStamp(int numb);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetStampColumnText([In, Out] ref int numb);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetStampColumnText(int numb, [MarshalAs(UnmanagedType.IDispatch)] object textArr);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksColumnNumber(int numb);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksTextLine([MarshalAs(UnmanagedType.IDispatch)] object textItem);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetTextLineAlign(short align);

//  [DispId(10)]
//  public virtual extern int SheetNumb { [DispId(10), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginEditStamp([In] ksStampNotify_BeginEditStampEventHandler obj0);

//  public virtual extern event ksStampNotify_BeginEditStampEventHandler BeginEditStamp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginEditStamp([In] ksStampNotify_BeginEditStampEventHandler obj0);

//  public virtual extern event ksStampNotify_EndEditStampEventHandler EndEditStamp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndEditStamp([In] ksStampNotify_EndEditStampEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndEditStamp([In] ksStampNotify_EndEditStampEventHandler obj0);

//  public virtual extern event ksStampNotify_StampCellDblClickEventHandler StampCellDblClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_StampCellDblClick([In] ksStampNotify_StampCellDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_StampCellDblClick(
//    [In] ksStampNotify_StampCellDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_StampCellBeginEdit(
//    [In] ksStampNotify_StampCellBeginEditEventHandler obj0);

//  public virtual extern event ksStampNotify_StampCellBeginEditEventHandler StampCellBeginEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_StampCellBeginEdit(
//    [In] ksStampNotify_StampCellBeginEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_StampBeginClearCells(
//    [In] ksStampNotify_StampBeginClearCellsEventHandler obj0);

//  public virtual extern event ksStampNotify_StampBeginClearCellsEventHandler StampBeginClearCells;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_StampBeginClearCells(
//    [In] ksStampNotify_StampBeginClearCellsEventHandler obj0);
//}
