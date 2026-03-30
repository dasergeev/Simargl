//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksLayoutSheetsNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksLayoutSheetsNotify_SinkHelper : ksLayoutSheetsNotify
//{
//  public ksLayoutSheetsNotify_AddEventHandler m_AddDelegate;
//  public ksLayoutSheetsNotify_DeleteEventHandler m_DeleteDelegate;
//  public ksLayoutSheetsNotify_UpdateEventHandler m_UpdateDelegate;
//  public int m_dwCookie;

//  public virtual bool Add([In] LayoutSheet obj0)
//  {
//    return this.m_AddDelegate != null && this.m_AddDelegate(obj0);
//  }

//  public virtual bool Delete([In] LayoutSheet obj0)
//  {
//    return this.m_DeleteDelegate != null && this.m_DeleteDelegate(obj0);
//  }

//  public virtual bool Update([In] LayoutSheet obj0)
//  {
//    return this.m_UpdateDelegate != null && this.m_UpdateDelegate(obj0);
//  }

//  internal ksLayoutSheetsNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_AddDelegate = (ksLayoutSheetsNotify_AddEventHandler) null;
//    this.m_DeleteDelegate = (ksLayoutSheetsNotify_DeleteEventHandler) null;
//    this.m_UpdateDelegate = (ksLayoutSheetsNotify_UpdateEventHandler) null;
//  }
//}
