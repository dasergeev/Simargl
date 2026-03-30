//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.LibraryManagerClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ComSourceInterfaces("KompasAPI7.ksLibraryManagerNotify\0\0")]
//[Guid("AACB5896-8918-43CB-A5B4-F3EAF45BCEF7")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class LibraryManagerClass : 
//  ILibraryManager,
//  LibraryManager,
//  ksLibraryManagerNotify_Event,
//  IKompasAPIObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern LibraryManagerClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1)]
//  public virtual extern ProceduresLibraries ProceduresLibraries { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(2)]
//  public virtual extern InsertsLibraries FragmentsLibraries { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(3)]
//  public virtual extern InsertsLibraries ModelsLibraries { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(4)]
//  public virtual extern bool Visible { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(5)]
//  public virtual extern PropertyManagerLayout Layout { [DispId(5), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(6)]
//  public virtual extern string ActiveFolder { [DispId(6), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(6), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(7)]
//  public virtual extern object ActiveFolderComment { [DispId(7), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(7), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Struct), In] set; }

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void AddFolder([MarshalAs(UnmanagedType.BStr), In] string PathFolder);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void RemoveFolder([MarshalAs(UnmanagedType.BStr), In] string PathFolder);

//  [DispId(10)]
//  public virtual extern ProceduresLibrary SystemControlStartLibrary { [DispId(10), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(11)]
//  public virtual extern ProceduresLibrary CurrentLibrary { [DispId(11), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetCurrentLibrary([MarshalAs(UnmanagedType.Interface), In] ProceduresLibrary PVal);

//  [DispId(13)]
//  public virtual extern ksSystemControlStartEnum SystemControlStartResult { [DispId(13), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(14)]
//  public virtual extern InsertsLibraries DocumentsLibraries { [DispId(14), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginAttach(
//    [In] ksLibraryManagerNotify_BeginAttachEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_BeginAttachEventHandler BeginAttach;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginAttach(
//    [In] ksLibraryManagerNotify_BeginAttachEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_AttachEventHandler Attach;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Attach([In] ksLibraryManagerNotify_AttachEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Attach([In] ksLibraryManagerNotify_AttachEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_BeginDetachEventHandler BeginDetach;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDetach(
//    [In] ksLibraryManagerNotify_BeginDetachEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDetach(
//    [In] ksLibraryManagerNotify_BeginDetachEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_DetachEventHandler Detach;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Detach([In] ksLibraryManagerNotify_DetachEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Detach([In] ksLibraryManagerNotify_DetachEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_BeginExecuteEventHandler BeginExecute;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginExecute(
//    [In] ksLibraryManagerNotify_BeginExecuteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginExecute(
//    [In] ksLibraryManagerNotify_BeginExecuteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndExecute([In] ksLibraryManagerNotify_EndExecuteEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_EndExecuteEventHandler EndExecute;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndExecute([In] ksLibraryManagerNotify_EndExecuteEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_SystemControlStopEventHandler SystemControlStop;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SystemControlStop(
//    [In] ksLibraryManagerNotify_SystemControlStopEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SystemControlStop(
//    [In] ksLibraryManagerNotify_SystemControlStopEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_SystemControlStartEventHandler SystemControlStart;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SystemControlStart(
//    [In] ksLibraryManagerNotify_SystemControlStartEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SystemControlStart(
//    [In] ksLibraryManagerNotify_SystemControlStartEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_AddLibraryDescription(
//    [In] ksLibraryManagerNotify_AddLibraryDescriptionEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_AddLibraryDescriptionEventHandler AddLibraryDescription;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_AddLibraryDescription(
//    [In] ksLibraryManagerNotify_AddLibraryDescriptionEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_DeleteLibraryDescriptionEventHandler DeleteLibraryDescription;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DeleteLibraryDescription(
//    [In] ksLibraryManagerNotify_DeleteLibraryDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DeleteLibraryDescription(
//    [In] ksLibraryManagerNotify_DeleteLibraryDescriptionEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_AddInsertEventHandler AddInsert;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_AddInsert([In] ksLibraryManagerNotify_AddInsertEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_AddInsert([In] ksLibraryManagerNotify_AddInsertEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_DeleteInsertEventHandler DeleteInsert;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DeleteInsert(
//    [In] ksLibraryManagerNotify_DeleteInsertEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DeleteInsert(
//    [In] ksLibraryManagerNotify_DeleteInsertEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_EditInsertEventHandler EditInsert;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EditInsert([In] ksLibraryManagerNotify_EditInsertEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EditInsert([In] ksLibraryManagerNotify_EditInsertEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_TryExecuteEventHandler TryExecute;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_TryExecute([In] ksLibraryManagerNotify_TryExecuteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_TryExecute([In] ksLibraryManagerNotify_TryExecuteEventHandler obj0);

//  public virtual extern event ksLibraryManagerNotify_BeginInsertDocumentEventHandler BeginInsertDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginInsertDocument(
//    [In] ksLibraryManagerNotify_BeginInsertDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginInsertDocument(
//    [In] ksLibraryManagerNotify_BeginInsertDocumentEventHandler obj0);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
