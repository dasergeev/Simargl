//// Decompiled with JetBrains decompiler
//// Type: KGAXLib.KGAXClass
//// Assembly: KGAXLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: E31777E8-3D29-4A2D-9394-6416A05AC4DD
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KGAXLib.dll

//using Kompas6API5;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KGAXLib;

//[ComSourceInterfaces("KGAXLib._DKGAXEvents\0\0")]
//[TypeLibType(34)]
//[Guid("6B943E71-5CA2-435D-AFA3-D7817B13ACA2")]
//[ClassInterface(0)]
//[ComImport]
//public class KGAXClass : _DKGAX, KGAX, _DKGAXEvents_Event
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern KGAXClass();

//  [DispId(-518)]
//  public virtual extern string Caption { [DispId(-518), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(-518), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(-517)]
//  public virtual extern string Text { [DispId(-517), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(-517), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(1)]
//  public virtual extern KDocumentType DocumentType { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(2)]
//  public virtual extern string DocumenFileName { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(3)]
//  public virtual extern KDocument3DDrawMode Document3DDrawMode { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(4)]
//  public virtual extern bool Document3DWireframeShadedMode { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Application GetKompasObject();

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern KDocumentType GetDocumentType([MarshalAs(UnmanagedType.Struct), In] object index);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetDocumentInterface([MarshalAs(UnmanagedType.Struct), In] object index, int newAPI);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int GetActiveDocumentID();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int GetDocumentsCount();

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int AddDocument([MarshalAs(UnmanagedType.BStr), In] string fileName);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int AddNewDocument([In] KDocumentType type);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int InsertDocument([MarshalAs(UnmanagedType.BStr), In] string fileName, [MarshalAs(UnmanagedType.Struct), In] object index);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int InsertNewDocument([In] KDocumentType type, [MarshalAs(UnmanagedType.Struct), In] object index);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool RemoveDocument([MarshalAs(UnmanagedType.Struct), In] object index);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ActivateDocument([MarshalAs(UnmanagedType.Struct), In] object index);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int CloseAll();

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int TestLoadDocument([MarshalAs(UnmanagedType.BStr), In] string fileName);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool InvalidateActiveDocument([In] bool erase);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void ZoomEntireDocument();

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void MoveViewDocument();

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void PanoramaViewDocument();

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void RotateViewDocument();

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void OrientationDocument();

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void ZoomWindow([In] KZoomType type = KZoomType.vt_ZoomWindow);

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void StopCurrentProcess([In] bool cancel = false);

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool DrawToDC([ComAliasName("stdole.OLE_HANDLE"), In] int dc, [In] int left, [In] int top, [In] int width, [In] int height);

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void SetCurrentLibManager([In] int t);

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void SetGabaritModifying();

//  [DispId(29)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int GetDocumentID([MarshalAs(UnmanagedType.Struct), In] object index);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgMouseDown([In] _DKGAXEvents_OnKgMouseDownEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgMouseDownEventHandler OnKgMouseDown;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgMouseDown([In] _DKGAXEvents_OnKgMouseDownEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgMouseUpEventHandler OnKgMouseUp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgMouseUp([In] _DKGAXEvents_OnKgMouseUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgMouseUp([In] _DKGAXEvents_OnKgMouseUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgMouseDblClick([In] _DKGAXEvents_OnKgMouseDblClickEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgMouseDblClickEventHandler OnKgMouseDblClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgMouseDblClick(
//    [In] _DKGAXEvents_OnKgMouseDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgStopCurrentProcess(
//    [In] _DKGAXEvents_OnKgStopCurrentProcessEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgStopCurrentProcessEventHandler OnKgStopCurrentProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgStopCurrentProcess(
//    [In] _DKGAXEvents_OnKgStopCurrentProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgCreate([In] _DKGAXEvents_OnKgCreateEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgCreateEventHandler OnKgCreate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgCreate([In] _DKGAXEvents_OnKgCreateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgPaint([In] _DKGAXEvents_OnKgPaintEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgPaintEventHandler OnKgPaint;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgPaint([In] _DKGAXEvents_OnKgPaintEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgCreateGLListEventHandler OnKgCreateGLList;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgCreateGLList([In] _DKGAXEvents_OnKgCreateGLListEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgCreateGLList([In] _DKGAXEvents_OnKgCreateGLListEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgAddGabatitEventHandler OnKgAddGabatit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgAddGabatit([In] _DKGAXEvents_OnKgAddGabatitEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgAddGabatit([In] _DKGAXEvents_OnKgAddGabatitEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgErrorLoadDocument(
//    [In] _DKGAXEvents_OnKgErrorLoadDocumentEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgErrorLoadDocumentEventHandler OnKgErrorLoadDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgErrorLoadDocument(
//    [In] _DKGAXEvents_OnKgErrorLoadDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgKeyDown([In] _DKGAXEvents_OnKgKeyDownEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgKeyDownEventHandler OnKgKeyDown;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgKeyDown([In] _DKGAXEvents_OnKgKeyDownEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgKeyUp([In] _DKGAXEvents_OnKgKeyUpEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgKeyUpEventHandler OnKgKeyUp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgKeyUp([In] _DKGAXEvents_OnKgKeyUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OnKgKeyPress([In] _DKGAXEvents_OnKgKeyPressEventHandler obj0);

//  public virtual extern event _DKGAXEvents_OnKgKeyPressEventHandler OnKgKeyPress;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OnKgKeyPress([In] _DKGAXEvents_OnKgKeyPressEventHandler obj0);
//}
