//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksSelectionMngNotify_SinkHelper
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

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
