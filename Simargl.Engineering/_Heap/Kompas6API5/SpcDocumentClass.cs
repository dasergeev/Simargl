//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.SpcDocumentClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[Guid("51E74523-9A3A-11D6-95CE-00C0262D30E3")]
//[ComSourceInterfaces("Kompas6API5.ksDocumentFileNotify\0\0")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class SpcDocumentClass : ksSpcDocument, SpcDocument, ksDocumentFileNotify_Event
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern SpcDocumentClass();

//  [DispId(1)]
//  public virtual extern int reference { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetStamp();

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetSpecification();

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksCloseDocument();

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetSpcSheetSB();

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSpcDocumentPagesCount();

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSaveDocument([MarshalAs(UnmanagedType.BStr)] string fileName);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksCreateDocument([MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteObj(int @ref);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetSpcSheetSB([MarshalAs(UnmanagedType.IDispatch)] object arr);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExistObj(int @ref);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksOpenDocument([MarshalAs(UnmanagedType.BStr)] string nameDoc, short regim);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetObjParam(int @ref, [MarshalAs(UnmanagedType.IDispatch)] object param, int parType = -1);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetObjParam(int @ref, [MarshalAs(UnmanagedType.IDispatch)] object param, int parType = -1);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SaveAsToRasterFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object RasterFormatParam();

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SaveAsToUncompressedRasterFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object rasterPar);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  public virtual extern object GetSpcDocumentNotify();

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSaveDocumentEx([MarshalAs(UnmanagedType.BStr)] string fileName, int SaveMode);

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetStampEx(int SheetNumb);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSaveToDXF([MarshalAs(UnmanagedType.BStr)] string DXFFileName);

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

//  public virtual extern event ksDocumentFileNotify_SaveDocumentEventHandler SaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SaveDocument([In] ksDocumentFileNotify_SaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SaveDocument([In] ksDocumentFileNotify_SaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Activate([In] ksDocumentFileNotify_ActivateEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_ActivateEventHandler Activate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Activate([In] ksDocumentFileNotify_ActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Deactivate([In] ksDocumentFileNotify_DeactivateEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_DeactivateEventHandler Deactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Deactivate([In] ksDocumentFileNotify_DeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginSaveAsDocument(
//    [In] ksDocumentFileNotify_BeginSaveAsDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginSaveAsDocumentEventHandler BeginSaveAsDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginSaveAsDocument(
//    [In] ksDocumentFileNotify_BeginSaveAsDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DocumentFrameOpen(
//    [In] ksDocumentFileNotify_DocumentFrameOpenEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_DocumentFrameOpenEventHandler DocumentFrameOpen;

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

//  public virtual extern event ksDocumentFileNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginProcess([In] ksDocumentFileNotify_BeginProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginProcess([In] ksDocumentFileNotify_BeginProcessEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndProcess([In] ksDocumentFileNotify_EndProcessEventHandler obj0);

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
