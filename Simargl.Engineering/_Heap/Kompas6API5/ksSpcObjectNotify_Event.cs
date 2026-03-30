//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksSpcObjectNotify_Event
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ComEventInterface(typeof (ksSpcObjectNotify), typeof (ksSpcObjectNotify_EventProvider))]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComVisible(false)]
//public interface ksSpcObjectNotify_Event
//{
//  event ksSpcObjectNotify_BeginDeleteEventHandler BeginDelete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDelete([In] ksSpcObjectNotify_BeginDeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDelete([In] ksSpcObjectNotify_BeginDeleteEventHandler obj0);

//  event ksSpcObjectNotify_DeleteEventHandler Delete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Delete([In] ksSpcObjectNotify_DeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Delete([In] ksSpcObjectNotify_DeleteEventHandler obj0);

//  event ksSpcObjectNotify_CellDblClickEventHandler CellDblClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CellDblClick([In] ksSpcObjectNotify_CellDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CellDblClick([In] ksSpcObjectNotify_CellDblClickEventHandler obj0);

//  event ksSpcObjectNotify_CellBeginEditEventHandler CellBeginEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CellBeginEdit([In] ksSpcObjectNotify_CellBeginEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CellBeginEdit([In] ksSpcObjectNotify_CellBeginEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeCurrent([In] ksSpcObjectNotify_ChangeCurrentEventHandler obj0);

//  event ksSpcObjectNotify_ChangeCurrentEventHandler ChangeCurrent;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeCurrent([In] ksSpcObjectNotify_ChangeCurrentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DocumentBeginAdd(
//    [In] ksSpcObjectNotify_DocumentBeginAddEventHandler obj0);

//  event ksSpcObjectNotify_DocumentBeginAddEventHandler DocumentBeginAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DocumentBeginAdd(
//    [In] ksSpcObjectNotify_DocumentBeginAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DocumentAdd([In] ksSpcObjectNotify_DocumentAddEventHandler obj0);

//  event ksSpcObjectNotify_DocumentAddEventHandler DocumentAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DocumentAdd([In] ksSpcObjectNotify_DocumentAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DocumentRemove([In] ksSpcObjectNotify_DocumentRemoveEventHandler obj0);

//  event ksSpcObjectNotify_DocumentRemoveEventHandler DocumentRemove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DocumentRemove([In] ksSpcObjectNotify_DocumentRemoveEventHandler obj0);

//  event ksSpcObjectNotify_BeginGeomChangeEventHandler BeginGeomChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginGeomChange([In] ksSpcObjectNotify_BeginGeomChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginGeomChange([In] ksSpcObjectNotify_BeginGeomChangeEventHandler obj0);

//  event ksSpcObjectNotify_GeomChangeEventHandler GeomChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_GeomChange([In] ksSpcObjectNotify_GeomChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_GeomChange([In] ksSpcObjectNotify_GeomChangeEventHandler obj0);

//  event ksSpcObjectNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginProcess([In] ksSpcObjectNotify_BeginProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginProcess([In] ksSpcObjectNotify_BeginProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EndProcess([In] ksSpcObjectNotify_EndProcessEventHandler obj0);

//  event ksSpcObjectNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EndProcess([In] ksSpcObjectNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CreateObject([In] ksSpcObjectNotify_CreateObjectEventHandler obj0);

//  event ksSpcObjectNotify_CreateObjectEventHandler CreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CreateObject([In] ksSpcObjectNotify_CreateObjectEventHandler obj0);

//  event ksSpcObjectNotify_UpdateObjectEventHandler UpdateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_UpdateObject([In] ksSpcObjectNotify_UpdateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_UpdateObject([In] ksSpcObjectNotify_UpdateObjectEventHandler obj0);

//  event ksSpcObjectNotify_BeginCopyEventHandler BeginCopy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCopy([In] ksSpcObjectNotify_BeginCopyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCopy([In] ksSpcObjectNotify_BeginCopyEventHandler obj0);

//  event ksSpcObjectNotify_copyEventHandler copy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_copy([In] ksSpcObjectNotify_copyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_copy([In] ksSpcObjectNotify_copyEventHandler obj0);
//}
