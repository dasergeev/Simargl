//// Decompiled with JetBrains decompiler
//// Type: KGAXLib._DKGAXEvents_SinkHelper
//// Assembly: KGAXLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: E31777E8-3D29-4A2D-9394-6416A05AC4DD
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KGAXLib.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KGAXLib;

//[ClassInterface(ClassInterfaceType.None)]
//public sealed class _DKGAXEvents_SinkHelper : _DKGAXEvents
//{
//  public _DKGAXEvents_OnKgKeyPressEventHandler m_OnKgKeyPressDelegate;
//  public _DKGAXEvents_OnKgKeyUpEventHandler m_OnKgKeyUpDelegate;
//  public _DKGAXEvents_OnKgKeyDownEventHandler m_OnKgKeyDownDelegate;
//  public _DKGAXEvents_OnKgErrorLoadDocumentEventHandler m_OnKgErrorLoadDocumentDelegate;
//  public _DKGAXEvents_OnKgAddGabatitEventHandler m_OnKgAddGabatitDelegate;
//  public _DKGAXEvents_OnKgCreateGLListEventHandler m_OnKgCreateGLListDelegate;
//  public _DKGAXEvents_OnKgPaintEventHandler m_OnKgPaintDelegate;
//  public _DKGAXEvents_OnKgCreateEventHandler m_OnKgCreateDelegate;
//  public _DKGAXEvents_OnKgStopCurrentProcessEventHandler m_OnKgStopCurrentProcessDelegate;
//  public _DKGAXEvents_OnKgMouseDblClickEventHandler m_OnKgMouseDblClickDelegate;
//  public _DKGAXEvents_OnKgMouseUpEventHandler m_OnKgMouseUpDelegate;
//  public _DKGAXEvents_OnKgMouseDownEventHandler m_OnKgMouseDownDelegate;
//  public int m_dwCookie;

//  public virtual void OnKgKeyPress([In] ref int obj0)
//  {
//    if (this.m_OnKgKeyPressDelegate == null)
//      return;
//    this.m_OnKgKeyPressDelegate(ref obj0);
//  }

//  public virtual void OnKgKeyUp([In] ref int obj0, [In] short obj1)
//  {
//    if (this.m_OnKgKeyUpDelegate == null)
//      return;
//    this.m_OnKgKeyUpDelegate(ref obj0, obj1);
//  }

//  public virtual void OnKgKeyDown([In] ref int obj0, [In] short obj1)
//  {
//    if (this.m_OnKgKeyDownDelegate == null)
//      return;
//    this.m_OnKgKeyDownDelegate(ref obj0, obj1);
//  }

//  public virtual void OnKgErrorLoadDocument([In] int obj0, [In] string obj1, [In] int obj2)
//  {
//    if (this.m_OnKgErrorLoadDocumentDelegate == null)
//      return;
//    this.m_OnKgErrorLoadDocumentDelegate(obj0, obj1, obj2);
//  }

//  public virtual void OnKgAddGabatit([In] GabaritObject obj0)
//  {
//    if (this.m_OnKgAddGabatitDelegate == null)
//      return;
//    this.m_OnKgAddGabatitDelegate(obj0);
//  }

//  public virtual void OnKgCreateGLList([In] GLObject obj0, [In] KDocument3DDrawMode obj1)
//  {
//    if (this.m_OnKgCreateGLListDelegate == null)
//      return;
//    this.m_OnKgCreateGLListDelegate(obj0, obj1);
//  }

//  public virtual void OnKgPaint([In] PaintObject obj0)
//  {
//    if (this.m_OnKgPaintDelegate == null)
//      return;
//    this.m_OnKgPaintDelegate(obj0);
//  }

//  public virtual void OnKgCreate([In] int obj0)
//  {
//    if (this.m_OnKgCreateDelegate == null)
//      return;
//    this.m_OnKgCreateDelegate(obj0);
//  }

//  public virtual void OnKgStopCurrentProcess()
//  {
//    if (this.m_OnKgStopCurrentProcessDelegate == null)
//      return;
//    this.m_OnKgStopCurrentProcessDelegate();
//  }

//  public virtual void OnKgMouseDblClick(
//    [In] short obj0,
//    [In] short obj1,
//    [In] int obj2,
//    [In] int obj3,
//    [In] ref bool obj4)
//  {
//    if (this.m_OnKgMouseDblClickDelegate == null)
//      return;
//    this.m_OnKgMouseDblClickDelegate(obj0, obj1, obj2, obj3, out obj4);
//  }

//  public virtual void OnKgMouseUp([In] short obj0, [In] short obj1, [In] int obj2, [In] int obj3, [In] ref bool obj4)
//  {
//    if (this.m_OnKgMouseUpDelegate == null)
//      return;
//    this.m_OnKgMouseUpDelegate(obj0, obj1, obj2, obj3, out obj4);
//  }

//  public virtual void OnKgMouseDown([In] short obj0, [In] short obj1, [In] int obj2, [In] int obj3, [In] ref bool obj4)
//  {
//    if (this.m_OnKgMouseDownDelegate == null)
//      return;
//    this.m_OnKgMouseDownDelegate(obj0, obj1, obj2, obj3, out obj4);
//  }

//  internal _DKGAXEvents_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_OnKgKeyPressDelegate = (_DKGAXEvents_OnKgKeyPressEventHandler) null;
//    this.m_OnKgKeyUpDelegate = (_DKGAXEvents_OnKgKeyUpEventHandler) null;
//    this.m_OnKgKeyDownDelegate = (_DKGAXEvents_OnKgKeyDownEventHandler) null;
//    this.m_OnKgErrorLoadDocumentDelegate = (_DKGAXEvents_OnKgErrorLoadDocumentEventHandler) null;
//    this.m_OnKgAddGabatitDelegate = (_DKGAXEvents_OnKgAddGabatitEventHandler) null;
//    this.m_OnKgCreateGLListDelegate = (_DKGAXEvents_OnKgCreateGLListEventHandler) null;
//    this.m_OnKgPaintDelegate = (_DKGAXEvents_OnKgPaintEventHandler) null;
//    this.m_OnKgCreateDelegate = (_DKGAXEvents_OnKgCreateEventHandler) null;
//    this.m_OnKgStopCurrentProcessDelegate = (_DKGAXEvents_OnKgStopCurrentProcessEventHandler) null;
//    this.m_OnKgMouseDblClickDelegate = (_DKGAXEvents_OnKgMouseDblClickEventHandler) null;
//    this.m_OnKgMouseUpDelegate = (_DKGAXEvents_OnKgMouseUpEventHandler) null;
//    this.m_OnKgMouseDownDelegate = (_DKGAXEvents_OnKgMouseDownEventHandler) null;
//  }
//}
