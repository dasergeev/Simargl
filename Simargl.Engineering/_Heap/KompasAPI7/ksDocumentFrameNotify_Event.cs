//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksDocumentFrameNotify_Event
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComEventInterface(typeof (ksDocumentFrameNotify), typeof (ksDocumentFrameNotify_EventProvider))]
//[ComVisible(false)]
//public interface ksDocumentFrameNotify_Event
//{
//  event ksDocumentFrameNotify_BeginPaintEventHandler BeginPaint;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginPaint([In] ksDocumentFrameNotify_BeginPaintEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginPaint([In] ksDocumentFrameNotify_BeginPaintEventHandler obj0);

//  event ksDocumentFrameNotify_ClosePaintEventHandler ClosePaint;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ClosePaint([In] ksDocumentFrameNotify_ClosePaintEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ClosePaint([In] ksDocumentFrameNotify_ClosePaintEventHandler obj0);

//  event ksDocumentFrameNotify_MouseDownEventHandler MouseDown;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_MouseDown([In] ksDocumentFrameNotify_MouseDownEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_MouseDown([In] ksDocumentFrameNotify_MouseDownEventHandler obj0);

//  event ksDocumentFrameNotify_MouseUpEventHandler MouseUp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_MouseUp([In] ksDocumentFrameNotify_MouseUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_MouseUp([In] ksDocumentFrameNotify_MouseUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_MouseDblClick(
//    [In] ksDocumentFrameNotify_MouseDblClickEventHandler obj0);

//  event ksDocumentFrameNotify_MouseDblClickEventHandler MouseDblClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_MouseDblClick(
//    [In] ksDocumentFrameNotify_MouseDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginPaintGL(
//    [In] ksDocumentFrameNotify_BeginPaintGLEventHandler obj0);

//  event ksDocumentFrameNotify_BeginPaintGLEventHandler BeginPaintGL;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginPaintGL(
//    [In] ksDocumentFrameNotify_BeginPaintGLEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ClosePaintGL(
//    [In] ksDocumentFrameNotify_ClosePaintGLEventHandler obj0);

//  event ksDocumentFrameNotify_ClosePaintGLEventHandler ClosePaintGL;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ClosePaintGL(
//    [In] ksDocumentFrameNotify_ClosePaintGLEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_AddGabarit([In] ksDocumentFrameNotify_AddGabaritEventHandler obj0);

//  event ksDocumentFrameNotify_AddGabaritEventHandler AddGabarit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_AddGabarit([In] ksDocumentFrameNotify_AddGabaritEventHandler obj0);

//  event ksDocumentFrameNotify_ActivateEventHandler Activate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Activate([In] ksDocumentFrameNotify_ActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Activate([In] ksDocumentFrameNotify_ActivateEventHandler obj0);

//  event ksDocumentFrameNotify_DeactivateEventHandler Deactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Deactivate([In] ksDocumentFrameNotify_DeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Deactivate([In] ksDocumentFrameNotify_DeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CloseFrame([In] ksDocumentFrameNotify_CloseFrameEventHandler obj0);

//  event ksDocumentFrameNotify_CloseFrameEventHandler CloseFrame;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CloseFrame([In] ksDocumentFrameNotify_CloseFrameEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_MouseMove([In] ksDocumentFrameNotify_MouseMoveEventHandler obj0);

//  event ksDocumentFrameNotify_MouseMoveEventHandler MouseMove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_MouseMove([In] ksDocumentFrameNotify_MouseMoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ShowOcxTree([In] ksDocumentFrameNotify_ShowOcxTreeEventHandler obj0);

//  event ksDocumentFrameNotify_ShowOcxTreeEventHandler ShowOcxTree;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ShowOcxTree([In] ksDocumentFrameNotify_ShowOcxTreeEventHandler obj0);

//  event ksDocumentFrameNotify_BeginPaintTmpObjectsEventHandler BeginPaintTmpObjects;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginPaintTmpObjects(
//    [In] ksDocumentFrameNotify_BeginPaintTmpObjectsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginPaintTmpObjects(
//    [In] ksDocumentFrameNotify_BeginPaintTmpObjectsEventHandler obj0);

//  event ksDocumentFrameNotify_ClosePaintTmpObjectsEventHandler ClosePaintTmpObjects;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ClosePaintTmpObjects(
//    [In] ksDocumentFrameNotify_ClosePaintTmpObjectsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ClosePaintTmpObjects(
//    [In] ksDocumentFrameNotify_ClosePaintTmpObjectsEventHandler obj0);
//}
