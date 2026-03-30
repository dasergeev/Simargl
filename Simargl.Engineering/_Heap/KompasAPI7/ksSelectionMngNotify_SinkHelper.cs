//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksSelectionMngNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6API5;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksSelectionMngNotify_SinkHelper : ksSelectionMngNotify
//{
//  public ksSelectionMngNotify_SelectEventHandler m_SelectDelegate;
//  public ksSelectionMngNotify_UnselectEventHandler m_UnselectDelegate;
//  public ksSelectionMngNotify_UnselectAllEventHandler m_UnselectAllDelegate;
//  public int m_dwCookie;

//  public virtual bool Select([In] object obj0)
//  {
//    return this.m_SelectDelegate != null && this.m_SelectDelegate(obj0);
//  }

//  public virtual bool Unselect([In] object obj0)
//  {
//    return this.m_UnselectDelegate != null && this.m_UnselectDelegate(obj0);
//  }

//  public virtual bool UnselectAll()
//  {
//    return this.m_UnselectAllDelegate != null && this.m_UnselectAllDelegate();
//  }

//  internal ksSelectionMngNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_SelectDelegate = (ksSelectionMngNotify_SelectEventHandler) null;
//    this.m_UnselectDelegate = (ksSelectionMngNotify_UnselectEventHandler) null;
//    this.m_UnselectAllDelegate = (ksSelectionMngNotify_UnselectAllEventHandler) null;
//  }
//}
