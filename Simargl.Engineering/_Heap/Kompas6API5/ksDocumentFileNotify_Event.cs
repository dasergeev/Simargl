//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksDocumentFileNotify_Event
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComEventInterface(typeof (ksDocumentFileNotify), typeof (ksDocumentFileNotify_EventProvider))]
//[ComVisible(false)]
//public interface ksDocumentFileNotify_Event
//{
//  event ksDocumentFileNotify_BeginCloseDocumentEventHandler BeginCloseDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCloseDocument(
//    [In] ksDocumentFileNotify_BeginCloseDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCloseDocument(
//    [In] ksDocumentFileNotify_BeginCloseDocumentEventHandler obj0);

//  event ksDocumentFileNotify_CloseDocumentEventHandler CloseDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CloseDocument(
//    [In] ksDocumentFileNotify_CloseDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CloseDocument(
//    [In] ksDocumentFileNotify_CloseDocumentEventHandler obj0);

//  event ksDocumentFileNotify_BeginSaveDocumentEventHandler BeginSaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginSaveDocument(
//    [In] ksDocumentFileNotify_BeginSaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginSaveDocument(
//    [In] ksDocumentFileNotify_BeginSaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SaveDocument([In] ksDocumentFileNotify_SaveDocumentEventHandler obj0);

//  event ksDocumentFileNotify_SaveDocumentEventHandler SaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SaveDocument([In] ksDocumentFileNotify_SaveDocumentEventHandler obj0);

//  event ksDocumentFileNotify_ActivateEventHandler Activate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Activate([In] ksDocumentFileNotify_ActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Activate([In] ksDocumentFileNotify_ActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Deactivate([In] ksDocumentFileNotify_DeactivateEventHandler obj0);

//  event ksDocumentFileNotify_DeactivateEventHandler Deactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Deactivate([In] ksDocumentFileNotify_DeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginSaveAsDocument(
//    [In] ksDocumentFileNotify_BeginSaveAsDocumentEventHandler obj0);

//  event ksDocumentFileNotify_BeginSaveAsDocumentEventHandler BeginSaveAsDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginSaveAsDocument(
//    [In] ksDocumentFileNotify_BeginSaveAsDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DocumentFrameOpen(
//    [In] ksDocumentFileNotify_DocumentFrameOpenEventHandler obj0);

//  event ksDocumentFileNotify_DocumentFrameOpenEventHandler DocumentFrameOpen;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DocumentFrameOpen(
//    [In] ksDocumentFileNotify_DocumentFrameOpenEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ProcessActivate(
//    [In] ksDocumentFileNotify_ProcessActivateEventHandler obj0);

//  event ksDocumentFileNotify_ProcessActivateEventHandler ProcessActivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ProcessActivate(
//    [In] ksDocumentFileNotify_ProcessActivateEventHandler obj0);

//  event ksDocumentFileNotify_ProcessDeactivateEventHandler ProcessDeactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ProcessDeactivate(
//    [In] ksDocumentFileNotify_ProcessDeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ProcessDeactivate(
//    [In] ksDocumentFileNotify_ProcessDeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginProcess([In] ksDocumentFileNotify_BeginProcessEventHandler obj0);

//  event ksDocumentFileNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginProcess([In] ksDocumentFileNotify_BeginProcessEventHandler obj0);

//  event ksDocumentFileNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EndProcess([In] ksDocumentFileNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EndProcess([In] ksDocumentFileNotify_EndProcessEventHandler obj0);

//  event ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler BeginAutoSaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginAutoSaveDocument(
//    [In] ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginAutoSaveDocument(
//    [In] ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler obj0);

//  event ksDocumentFileNotify_AutoSaveDocumentEventHandler AutoSaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_AutoSaveDocument(
//    [In] ksDocumentFileNotify_AutoSaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_AutoSaveDocument(
//    [In] ksDocumentFileNotify_AutoSaveDocumentEventHandler obj0);
//}
