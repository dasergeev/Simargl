//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksLibraryManagerNotify_Event
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComVisible(false)]
//[ComEventInterface(typeof (ksLibraryManagerNotify), typeof (ksLibraryManagerNotify_EventProvider))]
//public interface ksLibraryManagerNotify_Event
//{
//  event ksLibraryManagerNotify_BeginAttachEventHandler BeginAttach;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginAttach(
//    [In] ksLibraryManagerNotify_BeginAttachEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginAttach(
//    [In] ksLibraryManagerNotify_BeginAttachEventHandler obj0);

//  event ksLibraryManagerNotify_AttachEventHandler Attach;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Attach([In] ksLibraryManagerNotify_AttachEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Attach([In] ksLibraryManagerNotify_AttachEventHandler obj0);

//  event ksLibraryManagerNotify_BeginDetachEventHandler BeginDetach;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDetach(
//    [In] ksLibraryManagerNotify_BeginDetachEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDetach(
//    [In] ksLibraryManagerNotify_BeginDetachEventHandler obj0);

//  event ksLibraryManagerNotify_DetachEventHandler Detach;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Detach([In] ksLibraryManagerNotify_DetachEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Detach([In] ksLibraryManagerNotify_DetachEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginExecute(
//    [In] ksLibraryManagerNotify_BeginExecuteEventHandler obj0);

//  event ksLibraryManagerNotify_BeginExecuteEventHandler BeginExecute;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginExecute(
//    [In] ksLibraryManagerNotify_BeginExecuteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EndExecute([In] ksLibraryManagerNotify_EndExecuteEventHandler obj0);

//  event ksLibraryManagerNotify_EndExecuteEventHandler EndExecute;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EndExecute([In] ksLibraryManagerNotify_EndExecuteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SystemControlStop(
//    [In] ksLibraryManagerNotify_SystemControlStopEventHandler obj0);

//  event ksLibraryManagerNotify_SystemControlStopEventHandler SystemControlStop;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SystemControlStop(
//    [In] ksLibraryManagerNotify_SystemControlStopEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SystemControlStart(
//    [In] ksLibraryManagerNotify_SystemControlStartEventHandler obj0);

//  event ksLibraryManagerNotify_SystemControlStartEventHandler SystemControlStart;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SystemControlStart(
//    [In] ksLibraryManagerNotify_SystemControlStartEventHandler obj0);

//  event ksLibraryManagerNotify_AddLibraryDescriptionEventHandler AddLibraryDescription;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_AddLibraryDescription(
//    [In] ksLibraryManagerNotify_AddLibraryDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_AddLibraryDescription(
//    [In] ksLibraryManagerNotify_AddLibraryDescriptionEventHandler obj0);

//  event ksLibraryManagerNotify_DeleteLibraryDescriptionEventHandler DeleteLibraryDescription;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DeleteLibraryDescription(
//    [In] ksLibraryManagerNotify_DeleteLibraryDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DeleteLibraryDescription(
//    [In] ksLibraryManagerNotify_DeleteLibraryDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_AddInsert([In] ksLibraryManagerNotify_AddInsertEventHandler obj0);

//  event ksLibraryManagerNotify_AddInsertEventHandler AddInsert;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_AddInsert([In] ksLibraryManagerNotify_AddInsertEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DeleteInsert(
//    [In] ksLibraryManagerNotify_DeleteInsertEventHandler obj0);

//  event ksLibraryManagerNotify_DeleteInsertEventHandler DeleteInsert;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DeleteInsert(
//    [In] ksLibraryManagerNotify_DeleteInsertEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EditInsert([In] ksLibraryManagerNotify_EditInsertEventHandler obj0);

//  event ksLibraryManagerNotify_EditInsertEventHandler EditInsert;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EditInsert([In] ksLibraryManagerNotify_EditInsertEventHandler obj0);

//  event ksLibraryManagerNotify_TryExecuteEventHandler TryExecute;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_TryExecute([In] ksLibraryManagerNotify_TryExecuteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_TryExecute([In] ksLibraryManagerNotify_TryExecuteEventHandler obj0);

//  event ksLibraryManagerNotify_BeginInsertDocumentEventHandler BeginInsertDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginInsertDocument(
//    [In] ksLibraryManagerNotify_BeginInsertDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginInsertDocument(
//    [In] ksLibraryManagerNotify_BeginInsertDocumentEventHandler obj0);
//}
