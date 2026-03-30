//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksDocumentFileNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6API5;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksDocumentFileNotify_SinkHelper : ksDocumentFileNotify
//{
//  public ksDocumentFileNotify_BeginCloseDocumentEventHandler m_BeginCloseDocumentDelegate;
//  public ksDocumentFileNotify_CloseDocumentEventHandler m_CloseDocumentDelegate;
//  public ksDocumentFileNotify_BeginSaveDocumentEventHandler m_BeginSaveDocumentDelegate;
//  public ksDocumentFileNotify_SaveDocumentEventHandler m_SaveDocumentDelegate;
//  public ksDocumentFileNotify_ActivateEventHandler m_ActivateDelegate;
//  public ksDocumentFileNotify_DeactivateEventHandler m_DeactivateDelegate;
//  public ksDocumentFileNotify_BeginSaveAsDocumentEventHandler m_BeginSaveAsDocumentDelegate;
//  public ksDocumentFileNotify_DocumentFrameOpenEventHandler m_DocumentFrameOpenDelegate;
//  public ksDocumentFileNotify_ProcessActivateEventHandler m_ProcessActivateDelegate;
//  public ksDocumentFileNotify_ProcessDeactivateEventHandler m_ProcessDeactivateDelegate;
//  public ksDocumentFileNotify_BeginProcessEventHandler m_BeginProcessDelegate;
//  public ksDocumentFileNotify_EndProcessEventHandler m_EndProcessDelegate;
//  public ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler m_BeginAutoSaveDocumentDelegate;
//  public ksDocumentFileNotify_AutoSaveDocumentEventHandler m_AutoSaveDocumentDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginCloseDocument()
//  {
//    return this.m_BeginCloseDocumentDelegate != null && this.m_BeginCloseDocumentDelegate();
//  }

//  public virtual bool CloseDocument()
//  {
//    return this.m_CloseDocumentDelegate != null && this.m_CloseDocumentDelegate();
//  }

//  public virtual bool BeginSaveDocument([In] string obj0)
//  {
//    return this.m_BeginSaveDocumentDelegate != null && this.m_BeginSaveDocumentDelegate(obj0);
//  }

//  public virtual bool SaveDocument()
//  {
//    return this.m_SaveDocumentDelegate != null && this.m_SaveDocumentDelegate();
//  }

//  public virtual bool Activate() => this.m_ActivateDelegate != null && this.m_ActivateDelegate();

//  public virtual bool Deactivate()
//  {
//    return this.m_DeactivateDelegate != null && this.m_DeactivateDelegate();
//  }

//  public virtual bool BeginSaveAsDocument()
//  {
//    return this.m_BeginSaveAsDocumentDelegate != null && this.m_BeginSaveAsDocumentDelegate();
//  }

//  public virtual bool DocumentFrameOpen([In] object obj0)
//  {
//    return this.m_DocumentFrameOpenDelegate != null && this.m_DocumentFrameOpenDelegate(obj0);
//  }

//  public virtual bool ProcessActivate([In] int obj0)
//  {
//    return this.m_ProcessActivateDelegate != null && this.m_ProcessActivateDelegate(obj0);
//  }

//  public virtual bool ProcessDeactivate([In] int obj0)
//  {
//    return this.m_ProcessDeactivateDelegate != null && this.m_ProcessDeactivateDelegate(obj0);
//  }

//  public virtual bool BeginProcess([In] int obj0)
//  {
//    return this.m_BeginProcessDelegate != null && this.m_BeginProcessDelegate(obj0);
//  }

//  public virtual bool EndProcess([In] int obj0, [In] bool obj1)
//  {
//    return this.m_EndProcessDelegate != null && this.m_EndProcessDelegate(obj0, obj1);
//  }

//  public virtual bool BeginAutoSaveDocument([In] string obj0)
//  {
//    return this.m_BeginAutoSaveDocumentDelegate != null && this.m_BeginAutoSaveDocumentDelegate(obj0);
//  }

//  public virtual bool AutoSaveDocument()
//  {
//    return this.m_AutoSaveDocumentDelegate != null && this.m_AutoSaveDocumentDelegate();
//  }

//  internal ksDocumentFileNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginCloseDocumentDelegate = (ksDocumentFileNotify_BeginCloseDocumentEventHandler) null;
//    this.m_CloseDocumentDelegate = (ksDocumentFileNotify_CloseDocumentEventHandler) null;
//    this.m_BeginSaveDocumentDelegate = (ksDocumentFileNotify_BeginSaveDocumentEventHandler) null;
//    this.m_SaveDocumentDelegate = (ksDocumentFileNotify_SaveDocumentEventHandler) null;
//    this.m_ActivateDelegate = (ksDocumentFileNotify_ActivateEventHandler) null;
//    this.m_DeactivateDelegate = (ksDocumentFileNotify_DeactivateEventHandler) null;
//    this.m_BeginSaveAsDocumentDelegate = (ksDocumentFileNotify_BeginSaveAsDocumentEventHandler) null;
//    this.m_DocumentFrameOpenDelegate = (ksDocumentFileNotify_DocumentFrameOpenEventHandler) null;
//    this.m_ProcessActivateDelegate = (ksDocumentFileNotify_ProcessActivateEventHandler) null;
//    this.m_ProcessDeactivateDelegate = (ksDocumentFileNotify_ProcessDeactivateEventHandler) null;
//    this.m_BeginProcessDelegate = (ksDocumentFileNotify_BeginProcessEventHandler) null;
//    this.m_EndProcessDelegate = (ksDocumentFileNotify_EndProcessEventHandler) null;
//    this.m_BeginAutoSaveDocumentDelegate = (ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler) null;
//    this.m_AutoSaveDocumentDelegate = (ksDocumentFileNotify_AutoSaveDocumentEventHandler) null;
//  }
//}
