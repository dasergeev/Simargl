//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksContentDialogNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksContentDialogNotify_SinkHelper : ksContentDialogNotify
//{
//  public ksContentDialogNotify_CreateContentCallbackEventHandler m_CreateContentCallbackDelegate;
//  public ksContentDialogNotify_DestroyContentEventHandler m_DestroyContentDelegate;
//  public ksContentDialogNotify_ExecuteCommandEventHandler m_ExecuteCommandDelegate;
//  public ksContentDialogNotify_ButtonUpdateEventHandler m_ButtonUpdateDelegate;
//  public int m_dwCookie;

//  public virtual bool CreateContentCallback([In] int obj0, [In] ref int obj1)
//  {
//    return this.m_CreateContentCallbackDelegate != null && this.m_CreateContentCallbackDelegate(obj0, out obj1);
//  }

//  public virtual bool DestroyContent()
//  {
//    return this.m_DestroyContentDelegate != null && this.m_DestroyContentDelegate();
//  }

//  public virtual bool ExecuteCommand([In] int obj0)
//  {
//    return this.m_ExecuteCommandDelegate != null && this.m_ExecuteCommandDelegate(obj0);
//  }

//  public virtual bool ButtonUpdate([In] int obj0, [In] ref int obj1, [In] ref bool obj2)
//  {
//    return this.m_ButtonUpdateDelegate != null && this.m_ButtonUpdateDelegate(obj0, ref obj1, ref obj2);
//  }

//  internal ksContentDialogNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_CreateContentCallbackDelegate = (ksContentDialogNotify_CreateContentCallbackEventHandler) null;
//    this.m_DestroyContentDelegate = (ksContentDialogNotify_DestroyContentEventHandler) null;
//    this.m_ExecuteCommandDelegate = (ksContentDialogNotify_ExecuteCommandEventHandler) null;
//    this.m_ButtonUpdateDelegate = (ksContentDialogNotify_ButtonUpdateEventHandler) null;
//  }
//}
