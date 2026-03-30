//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksSpecificationObjectNotify_Event
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ComEventInterface(typeof (ksSpecificationObjectNotify), typeof (ksSpecificationObjectNotify_EventProvider))]
//[ComVisible(false)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public interface ksSpecificationObjectNotify_Event
//{
//  event ksSpecificationObjectNotify_BeginDeleteEventHandler BeginDelete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDelete(
//    [In] ksSpecificationObjectNotify_BeginDeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDelete(
//    [In] ksSpecificationObjectNotify_BeginDeleteEventHandler obj0);

//  event ksSpecificationObjectNotify_DeleteEventHandler Delete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Delete(
//    [In] ksSpecificationObjectNotify_DeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Delete(
//    [In] ksSpecificationObjectNotify_DeleteEventHandler obj0);

//  event ksSpecificationObjectNotify_CellDblClickEventHandler CellDblClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CellDblClick(
//    [In] ksSpecificationObjectNotify_CellDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CellDblClick(
//    [In] ksSpecificationObjectNotify_CellDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CellBeginEdit(
//    [In] ksSpecificationObjectNotify_CellBeginEditEventHandler obj0);

//  event ksSpecificationObjectNotify_CellBeginEditEventHandler CellBeginEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CellBeginEdit(
//    [In] ksSpecificationObjectNotify_CellBeginEditEventHandler obj0);

//  event ksSpecificationObjectNotify_ChangeCurrentEventHandler ChangeCurrent;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeCurrent(
//    [In] ksSpecificationObjectNotify_ChangeCurrentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeCurrent(
//    [In] ksSpecificationObjectNotify_ChangeCurrentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DocumentBeginAdd(
//    [In] ksSpecificationObjectNotify_DocumentBeginAddEventHandler obj0);

//  event ksSpecificationObjectNotify_DocumentBeginAddEventHandler DocumentBeginAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DocumentBeginAdd(
//    [In] ksSpecificationObjectNotify_DocumentBeginAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DocumentAdd(
//    [In] ksSpecificationObjectNotify_DocumentAddEventHandler obj0);

//  event ksSpecificationObjectNotify_DocumentAddEventHandler DocumentAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DocumentAdd(
//    [In] ksSpecificationObjectNotify_DocumentAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DocumentRemove(
//    [In] ksSpecificationObjectNotify_DocumentRemoveEventHandler obj0);

//  event ksSpecificationObjectNotify_DocumentRemoveEventHandler DocumentRemove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DocumentRemove(
//    [In] ksSpecificationObjectNotify_DocumentRemoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginGeomChange(
//    [In] ksSpecificationObjectNotify_BeginGeomChangeEventHandler obj0);

//  event ksSpecificationObjectNotify_BeginGeomChangeEventHandler BeginGeomChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginGeomChange(
//    [In] ksSpecificationObjectNotify_BeginGeomChangeEventHandler obj0);

//  event ksSpecificationObjectNotify_GeomChangeEventHandler GeomChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_GeomChange(
//    [In] ksSpecificationObjectNotify_GeomChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_GeomChange(
//    [In] ksSpecificationObjectNotify_GeomChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginProcess(
//    [In] ksSpecificationObjectNotify_BeginProcessEventHandler obj0);

//  event ksSpecificationObjectNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginProcess(
//    [In] ksSpecificationObjectNotify_BeginProcessEventHandler obj0);

//  event ksSpecificationObjectNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EndProcess(
//    [In] ksSpecificationObjectNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EndProcess(
//    [In] ksSpecificationObjectNotify_EndProcessEventHandler obj0);

//  event ksSpecificationObjectNotify_CreateObjectEventHandler CreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CreateObject(
//    [In] ksSpecificationObjectNotify_CreateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CreateObject(
//    [In] ksSpecificationObjectNotify_CreateObjectEventHandler obj0);

//  event ksSpecificationObjectNotify_UpdateObjectEventHandler UpdateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_UpdateObject(
//    [In] ksSpecificationObjectNotify_UpdateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_UpdateObject(
//    [In] ksSpecificationObjectNotify_UpdateObjectEventHandler obj0);
//}
