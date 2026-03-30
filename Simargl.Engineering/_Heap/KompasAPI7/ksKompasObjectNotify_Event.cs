//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksKompasObjectNotify_Event
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6API5;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ComVisible(false)]
//[ComEventInterface(typeof (ksKompasObjectNotify), typeof (ksKompasObjectNotify_EventProvider))]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public interface ksKompasObjectNotify_Event
//{
//  event ksKompasObjectNotify_CreateDocumentEventHandler CreateDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CreateDocument(
//    [In] ksKompasObjectNotify_CreateDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CreateDocument(
//    [In] ksKompasObjectNotify_CreateDocumentEventHandler obj0);

//  event ksKompasObjectNotify_BeginOpenDocumentEventHandler BeginOpenDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginOpenDocument(
//    [In] ksKompasObjectNotify_BeginOpenDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginOpenDocument(
//    [In] ksKompasObjectNotify_BeginOpenDocumentEventHandler obj0);

//  event ksKompasObjectNotify_OpenDocumentEventHandler OpenDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OpenDocument([In] ksKompasObjectNotify_OpenDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OpenDocument([In] ksKompasObjectNotify_OpenDocumentEventHandler obj0);

//  event ksKompasObjectNotify_ChangeActiveDocumentEventHandler ChangeActiveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeActiveDocument(
//    [In] ksKompasObjectNotify_ChangeActiveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeActiveDocument(
//    [In] ksKompasObjectNotify_ChangeActiveDocumentEventHandler obj0);

//  event ksKompasObjectNotify_ApplicationDestroyEventHandler ApplicationDestroy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ApplicationDestroy(
//    [In] ksKompasObjectNotify_ApplicationDestroyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ApplicationDestroy(
//    [In] ksKompasObjectNotify_ApplicationDestroyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCreate([In] ksKompasObjectNotify_BeginCreateEventHandler obj0);

//  event ksKompasObjectNotify_BeginCreateEventHandler BeginCreate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCreate([In] ksKompasObjectNotify_BeginCreateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginOpenFile(
//    [In] ksKompasObjectNotify_BeginOpenFileEventHandler obj0);

//  event ksKompasObjectNotify_BeginOpenFileEventHandler BeginOpenFile;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginOpenFile(
//    [In] ksKompasObjectNotify_BeginOpenFileEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCloseAllDocument(
//    [In] ksKompasObjectNotify_BeginCloseAllDocumentEventHandler obj0);

//  event ksKompasObjectNotify_BeginCloseAllDocumentEventHandler BeginCloseAllDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCloseAllDocument(
//    [In] ksKompasObjectNotify_BeginCloseAllDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_KeyDown([In] ksKompasObjectNotify_KeyDownEventHandler obj0);

//  event ksKompasObjectNotify_KeyDownEventHandler KeyDown;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_KeyDown([In] ksKompasObjectNotify_KeyDownEventHandler obj0);

//  event ksKompasObjectNotify_KeyUpEventHandler KeyUp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_KeyUp([In] ksKompasObjectNotify_KeyUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_KeyUp([In] ksKompasObjectNotify_KeyUpEventHandler obj0);

//  event ksKompasObjectNotify_KeyPressEventHandler KeyPress;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_KeyPress([In] ksKompasObjectNotify_KeyPressEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_KeyPress([In] ksKompasObjectNotify_KeyPressEventHandler obj0);

//  event ksKompasObjectNotify_BeginReguestFilesEventHandler BeginReguestFiles;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginReguestFiles(
//    [In] ksKompasObjectNotify_BeginReguestFilesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginReguestFiles(
//    [In] ksKompasObjectNotify_BeginReguestFilesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginChoiceMaterial(
//    [In] ksKompasObjectNotify_BeginChoiceMaterialEventHandler obj0);

//  event ksKompasObjectNotify_BeginChoiceMaterialEventHandler BeginChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginChoiceMaterial(
//    [In] ksKompasObjectNotify_BeginChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChoiceMaterial(
//    [In] ksKompasObjectNotify_ChoiceMaterialEventHandler obj0);

//  event ksKompasObjectNotify_ChoiceMaterialEventHandler ChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChoiceMaterial(
//    [In] ksKompasObjectNotify_ChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_IsNeedConvertToSavePrevious(
//    [In] ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler obj0);

//  event ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler IsNeedConvertToSavePrevious;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_IsNeedConvertToSavePrevious(
//    [In] ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginConvertToSavePrevious(
//    [In] ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler obj0);

//  event ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler BeginConvertToSavePrevious;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginConvertToSavePrevious(
//    [In] ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler obj0);

//  event ksKompasObjectNotify_EndConvertToSavePreviousEventHandler EndConvertToSavePrevious;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EndConvertToSavePrevious(
//    [In] ksKompasObjectNotify_EndConvertToSavePreviousEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EndConvertToSavePrevious(
//    [In] ksKompasObjectNotify_EndConvertToSavePreviousEventHandler obj0);

//  event ksKompasObjectNotify_ChangeThemeEventHandler ChangeTheme;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeTheme([In] ksKompasObjectNotify_ChangeThemeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeTheme([In] ksKompasObjectNotify_ChangeThemeEventHandler obj0);

//  event ksKompasObjectNotify_BeginDragOpenFilesEventHandler BeginDragOpenFiles;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDragOpenFiles(
//    [In] ksKompasObjectNotify_BeginDragOpenFilesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDragOpenFiles(
//    [In] ksKompasObjectNotify_BeginDragOpenFilesEventHandler obj0);
//}
