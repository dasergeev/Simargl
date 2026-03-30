//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7._ApplicationClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("8C3719B5-0DF2-4C12-9CA8-3AF4827A3BBB")]
//[TypeLibType(TypeLibTypeFlags.FAppObject | TypeLibTypeFlags.FCanCreate | TypeLibTypeFlags.FPreDeclId)]
//[ClassInterface(ClassInterfaceType.None)]
//[ComSourceInterfaces("Kompas6API5.ksKompasObjectNotify, Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\0\0")]
//[ComImport]
//public class _ApplicationClass : 
//  IApplication,
//  _Application,
//  ksKompasObjectNotify_Event,
//  IKompasAPIObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern _ApplicationClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1)]
//  public virtual extern bool Visible { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2)]
//  public virtual extern Documents Documents { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void Quit();

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern ProcessParam CreateProcessParam();

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void StopCurrentProcess([In] bool PostMessage = false, [MarshalAs(UnmanagedType.Interface), In, Optional] IKompasDocument PDoc);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern PropertyManager CreatePropertyManager([In] bool NewManager = false);

//  [DispId(7)]
//  public virtual extern IKompasDocument ActiveDocument { [DispId(7), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Interface), In] set; [DispId(7), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(8)]
//  public virtual extern LibraryManager LibraryManager { [DispId(8), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(9)]
//  public virtual extern KompasError KompasError { [DispId(9), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Converter get_Converter([MarshalAs(UnmanagedType.Struct), In] object Library);

//  [DispId(11)]
//  public virtual extern CheckSum CheckSum { [DispId(11), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(12)]
//  public virtual extern ProgressBarIndicator ProgressBarIndicator { [DispId(12), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(13)]
//  public virtual extern ksHideMessageEnum HideMessage { [DispId(13), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(13), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ExecuteKompasCommand([In] int CommandID, [In] bool PostMessage = true);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsKompasCommandEnable([In] int CommandID);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int IsKompasCommandCheck([In] int CommandID);

//  [DispId(17)]
//  public virtual extern SystemSettings SystemSettings { [DispId(17), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string get_ApplicationName([In] bool FullName);

//  [DispId(19)]
//  public virtual extern IMath2D Math2D { [DispId(19), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(20)]
//  public virtual extern PrintJob PrintJob { [DispId(20), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int MessageBoxEx([MarshalAs(UnmanagedType.BStr), In] string Text, [MarshalAs(UnmanagedType.BStr), In] string Caption, [In] int Flags);

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int MessageDlg(
//    [ComAliasName("stdole.OLE_HANDLE"), In] int Parent,
//    [MarshalAs(UnmanagedType.BStr), In] string Text,
//    [MarshalAs(UnmanagedType.BStr), In] string Caption,
//    [MarshalAs(UnmanagedType.BStr), In] string Explanation,
//    [In] int Flags,
//    [MarshalAs(UnmanagedType.BStr), In] string PositiveButton,
//    [MarshalAs(UnmanagedType.BStr), In] string NegativeButton,
//    [MarshalAs(UnmanagedType.BStr), In] string CancelButton,
//    [In] int HelpId,
//    [MarshalAs(UnmanagedType.BStr), In] string HelpFileName);

//  [DispId(23)]
//  public virtual extern string CurrentDirectory { [DispId(23), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(23), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Styles get_LibraryStyles([MarshalAs(UnmanagedType.BStr), In] string Path, [In] ksStylesLibraryTypeEnum StylesType);

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetSystemVersion(
//    out int Major,
//    out int Minor,
//    out int Release,
//    out int Build);

//  public virtual extern event ksKompasObjectNotify_CreateDocumentEventHandler CreateDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CreateDocument(
//    [In] ksKompasObjectNotify_CreateDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CreateDocument(
//    [In] ksKompasObjectNotify_CreateDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginOpenDocument(
//    [In] ksKompasObjectNotify_BeginOpenDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginOpenDocumentEventHandler BeginOpenDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginOpenDocument(
//    [In] ksKompasObjectNotify_BeginOpenDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_OpenDocumentEventHandler OpenDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OpenDocument([In] ksKompasObjectNotify_OpenDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OpenDocument([In] ksKompasObjectNotify_OpenDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_ChangeActiveDocumentEventHandler ChangeActiveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeActiveDocument(
//    [In] ksKompasObjectNotify_ChangeActiveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeActiveDocument(
//    [In] ksKompasObjectNotify_ChangeActiveDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_ApplicationDestroyEventHandler ApplicationDestroy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ApplicationDestroy(
//    [In] ksKompasObjectNotify_ApplicationDestroyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ApplicationDestroy(
//    [In] ksKompasObjectNotify_ApplicationDestroyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCreate([In] ksKompasObjectNotify_BeginCreateEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginCreateEventHandler BeginCreate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCreate([In] ksKompasObjectNotify_BeginCreateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginOpenFile(
//    [In] ksKompasObjectNotify_BeginOpenFileEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginOpenFileEventHandler BeginOpenFile;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginOpenFile(
//    [In] ksKompasObjectNotify_BeginOpenFileEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginCloseAllDocumentEventHandler BeginCloseAllDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCloseAllDocument(
//    [In] ksKompasObjectNotify_BeginCloseAllDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCloseAllDocument(
//    [In] ksKompasObjectNotify_BeginCloseAllDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_KeyDownEventHandler KeyDown;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_KeyDown([In] ksKompasObjectNotify_KeyDownEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_KeyDown([In] ksKompasObjectNotify_KeyDownEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_KeyUpEventHandler KeyUp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_KeyUp([In] ksKompasObjectNotify_KeyUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_KeyUp([In] ksKompasObjectNotify_KeyUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_KeyPress([In] ksKompasObjectNotify_KeyPressEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_KeyPressEventHandler KeyPress;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_KeyPress([In] ksKompasObjectNotify_KeyPressEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginReguestFilesEventHandler BeginReguestFiles;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginReguestFiles(
//    [In] ksKompasObjectNotify_BeginReguestFilesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginReguestFiles(
//    [In] ksKompasObjectNotify_BeginReguestFilesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginChoiceMaterial(
//    [In] ksKompasObjectNotify_BeginChoiceMaterialEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginChoiceMaterialEventHandler BeginChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginChoiceMaterial(
//    [In] ksKompasObjectNotify_BeginChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChoiceMaterial(
//    [In] ksKompasObjectNotify_ChoiceMaterialEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_ChoiceMaterialEventHandler ChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChoiceMaterial(
//    [In] ksKompasObjectNotify_ChoiceMaterialEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler IsNeedConvertToSavePrevious;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_IsNeedConvertToSavePrevious(
//    [In] ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_IsNeedConvertToSavePrevious(
//    [In] ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler BeginConvertToSavePrevious;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginConvertToSavePrevious(
//    [In] ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginConvertToSavePrevious(
//    [In] ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_EndConvertToSavePreviousEventHandler EndConvertToSavePrevious;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndConvertToSavePrevious(
//    [In] ksKompasObjectNotify_EndConvertToSavePreviousEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndConvertToSavePrevious(
//    [In] ksKompasObjectNotify_EndConvertToSavePreviousEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeTheme([In] ksKompasObjectNotify_ChangeThemeEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_ChangeThemeEventHandler ChangeTheme;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeTheme([In] ksKompasObjectNotify_ChangeThemeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDragOpenFiles(
//    [In] ksKompasObjectNotify_BeginDragOpenFilesEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginDragOpenFilesEventHandler BeginDragOpenFiles;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDragOpenFiles(
//    [In] ksKompasObjectNotify_BeginDragOpenFilesEventHandler obj0);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
