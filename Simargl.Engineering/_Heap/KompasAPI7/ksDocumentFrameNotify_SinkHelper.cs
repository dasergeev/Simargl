//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksDocumentFrameNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksDocumentFrameNotify_SinkHelper : ksDocumentFrameNotify
//{
//  public ksDocumentFrameNotify_BeginPaintEventHandler m_BeginPaintDelegate;
//  public ksDocumentFrameNotify_ClosePaintEventHandler m_ClosePaintDelegate;
//  public ksDocumentFrameNotify_MouseDownEventHandler m_MouseDownDelegate;
//  public ksDocumentFrameNotify_MouseUpEventHandler m_MouseUpDelegate;
//  public ksDocumentFrameNotify_MouseDblClickEventHandler m_MouseDblClickDelegate;
//  public ksDocumentFrameNotify_BeginPaintGLEventHandler m_BeginPaintGLDelegate;
//  public ksDocumentFrameNotify_ClosePaintGLEventHandler m_ClosePaintGLDelegate;
//  public ksDocumentFrameNotify_AddGabaritEventHandler m_AddGabaritDelegate;
//  public ksDocumentFrameNotify_ActivateEventHandler m_ActivateDelegate;
//  public ksDocumentFrameNotify_DeactivateEventHandler m_DeactivateDelegate;
//  public ksDocumentFrameNotify_CloseFrameEventHandler m_CloseFrameDelegate;
//  public ksDocumentFrameNotify_MouseMoveEventHandler m_MouseMoveDelegate;
//  public ksDocumentFrameNotify_ShowOcxTreeEventHandler m_ShowOcxTreeDelegate;
//  public ksDocumentFrameNotify_BeginPaintTmpObjectsEventHandler m_BeginPaintTmpObjectsDelegate;
//  public ksDocumentFrameNotify_ClosePaintTmpObjectsEventHandler m_ClosePaintTmpObjectsDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginPaint([In] IPaintObject obj0)
//  {
//    return this.m_BeginPaintDelegate != null && this.m_BeginPaintDelegate(obj0);
//  }

//  public virtual bool ClosePaint([In] IPaintObject obj0)
//  {
//    return this.m_ClosePaintDelegate != null && this.m_ClosePaintDelegate(obj0);
//  }

//  public virtual bool MouseDown([In] short obj0, [In] short obj1, [In] int obj2, [In] int obj3)
//  {
//    return this.m_MouseDownDelegate != null && this.m_MouseDownDelegate(obj0, obj1, obj2, obj3);
//  }

//  public virtual bool MouseUp([In] short obj0, [In] short obj1, [In] int obj2, [In] int obj3)
//  {
//    return this.m_MouseUpDelegate != null && this.m_MouseUpDelegate(obj0, obj1, obj2, obj3);
//  }

//  public virtual bool MouseDblClick([In] short obj0, [In] short obj1, [In] int obj2, [In] int obj3)
//  {
//    return this.m_MouseDblClickDelegate != null && this.m_MouseDblClickDelegate(obj0, obj1, obj2, obj3);
//  }

//  public virtual bool BeginPaintGL([In] ksGLObject obj0, [In] int obj1)
//  {
//    return this.m_BeginPaintGLDelegate != null && this.m_BeginPaintGLDelegate(obj0, obj1);
//  }

//  public virtual bool ClosePaintGL([In] ksGLObject obj0, [In] int obj1)
//  {
//    return this.m_ClosePaintGLDelegate != null && this.m_ClosePaintGLDelegate(obj0, obj1);
//  }

//  public virtual bool AddGabarit([In] IGabaritObject obj0)
//  {
//    return this.m_AddGabaritDelegate != null && this.m_AddGabaritDelegate(obj0);
//  }

//  public virtual bool Activate() => this.m_ActivateDelegate != null && this.m_ActivateDelegate();

//  public virtual bool Deactivate()
//  {
//    return this.m_DeactivateDelegate != null && this.m_DeactivateDelegate();
//  }

//  public virtual bool CloseFrame()
//  {
//    return this.m_CloseFrameDelegate != null && this.m_CloseFrameDelegate();
//  }

//  public virtual bool MouseMove([In] short obj0, [In] int obj1, [In] int obj2)
//  {
//    return this.m_MouseMoveDelegate != null && this.m_MouseMoveDelegate(obj0, obj1, obj2);
//  }

//  public virtual bool ShowOcxTree([In] object obj0, [In] bool obj1)
//  {
//    return this.m_ShowOcxTreeDelegate != null && this.m_ShowOcxTreeDelegate(obj0, obj1);
//  }

//  public virtual bool BeginPaintTmpObjects()
//  {
//    return this.m_BeginPaintTmpObjectsDelegate != null && this.m_BeginPaintTmpObjectsDelegate();
//  }

//  public virtual bool ClosePaintTmpObjects()
//  {
//    return this.m_ClosePaintTmpObjectsDelegate != null && this.m_ClosePaintTmpObjectsDelegate();
//  }

//  internal ksDocumentFrameNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginPaintDelegate = (ksDocumentFrameNotify_BeginPaintEventHandler) null;
//    this.m_ClosePaintDelegate = (ksDocumentFrameNotify_ClosePaintEventHandler) null;
//    this.m_MouseDownDelegate = (ksDocumentFrameNotify_MouseDownEventHandler) null;
//    this.m_MouseUpDelegate = (ksDocumentFrameNotify_MouseUpEventHandler) null;
//    this.m_MouseDblClickDelegate = (ksDocumentFrameNotify_MouseDblClickEventHandler) null;
//    this.m_BeginPaintGLDelegate = (ksDocumentFrameNotify_BeginPaintGLEventHandler) null;
//    this.m_ClosePaintGLDelegate = (ksDocumentFrameNotify_ClosePaintGLEventHandler) null;
//    this.m_AddGabaritDelegate = (ksDocumentFrameNotify_AddGabaritEventHandler) null;
//    this.m_ActivateDelegate = (ksDocumentFrameNotify_ActivateEventHandler) null;
//    this.m_DeactivateDelegate = (ksDocumentFrameNotify_DeactivateEventHandler) null;
//    this.m_CloseFrameDelegate = (ksDocumentFrameNotify_CloseFrameEventHandler) null;
//    this.m_MouseMoveDelegate = (ksDocumentFrameNotify_MouseMoveEventHandler) null;
//    this.m_ShowOcxTreeDelegate = (ksDocumentFrameNotify_ShowOcxTreeEventHandler) null;
//    this.m_BeginPaintTmpObjectsDelegate = (ksDocumentFrameNotify_BeginPaintTmpObjectsEventHandler) null;
//    this.m_ClosePaintTmpObjectsDelegate = (ksDocumentFrameNotify_ClosePaintTmpObjectsEventHandler) null;
//  }
//}
