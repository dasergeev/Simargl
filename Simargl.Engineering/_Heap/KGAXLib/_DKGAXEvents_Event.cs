//// Decompiled with JetBrains decompiler
//// Type: KGAXLib._DKGAXEvents_Event
//// Assembly: KGAXLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: E31777E8-3D29-4A2D-9394-6416A05AC4DD
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KGAXLib.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KGAXLib;

//[ComVisible(false)]
//[TypeLibType(16 /*0x10*/)]
//[ComEventInterface(typeof (_DKGAXEvents), typeof (_DKGAXEvents_EventProvider))]
//public interface _DKGAXEvents_Event
//{
//  event _DKGAXEvents_OnKgMouseDownEventHandler OnKgMouseDown;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgMouseDown([In] _DKGAXEvents_OnKgMouseDownEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgMouseDown([In] _DKGAXEvents_OnKgMouseDownEventHandler obj0);

//  event _DKGAXEvents_OnKgMouseUpEventHandler OnKgMouseUp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgMouseUp([In] _DKGAXEvents_OnKgMouseUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgMouseUp([In] _DKGAXEvents_OnKgMouseUpEventHandler obj0);

//  event _DKGAXEvents_OnKgMouseDblClickEventHandler OnKgMouseDblClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgMouseDblClick([In] _DKGAXEvents_OnKgMouseDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgMouseDblClick([In] _DKGAXEvents_OnKgMouseDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgStopCurrentProcess(
//    [In] _DKGAXEvents_OnKgStopCurrentProcessEventHandler obj0);

//  event _DKGAXEvents_OnKgStopCurrentProcessEventHandler OnKgStopCurrentProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgStopCurrentProcess(
//    [In] _DKGAXEvents_OnKgStopCurrentProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgCreate([In] _DKGAXEvents_OnKgCreateEventHandler obj0);

//  event _DKGAXEvents_OnKgCreateEventHandler OnKgCreate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgCreate([In] _DKGAXEvents_OnKgCreateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgPaint([In] _DKGAXEvents_OnKgPaintEventHandler obj0);

//  event _DKGAXEvents_OnKgPaintEventHandler OnKgPaint;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgPaint([In] _DKGAXEvents_OnKgPaintEventHandler obj0);

//  event _DKGAXEvents_OnKgCreateGLListEventHandler OnKgCreateGLList;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgCreateGLList([In] _DKGAXEvents_OnKgCreateGLListEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgCreateGLList([In] _DKGAXEvents_OnKgCreateGLListEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgAddGabatit([In] _DKGAXEvents_OnKgAddGabatitEventHandler obj0);

//  event _DKGAXEvents_OnKgAddGabatitEventHandler OnKgAddGabatit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgAddGabatit([In] _DKGAXEvents_OnKgAddGabatitEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgErrorLoadDocument(
//    [In] _DKGAXEvents_OnKgErrorLoadDocumentEventHandler obj0);

//  event _DKGAXEvents_OnKgErrorLoadDocumentEventHandler OnKgErrorLoadDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgErrorLoadDocument(
//    [In] _DKGAXEvents_OnKgErrorLoadDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgKeyDown([In] _DKGAXEvents_OnKgKeyDownEventHandler obj0);

//  event _DKGAXEvents_OnKgKeyDownEventHandler OnKgKeyDown;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgKeyDown([In] _DKGAXEvents_OnKgKeyDownEventHandler obj0);

//  event _DKGAXEvents_OnKgKeyUpEventHandler OnKgKeyUp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgKeyUp([In] _DKGAXEvents_OnKgKeyUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgKeyUp([In] _DKGAXEvents_OnKgKeyUpEventHandler obj0);

//  event _DKGAXEvents_OnKgKeyPressEventHandler OnKgKeyPress;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_OnKgKeyPress([In] _DKGAXEvents_OnKgKeyPressEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_OnKgKeyPress([In] _DKGAXEvents_OnKgKeyPressEventHandler obj0);
//}
