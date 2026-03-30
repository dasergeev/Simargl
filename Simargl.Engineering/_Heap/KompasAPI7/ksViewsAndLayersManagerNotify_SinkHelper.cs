//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksViewsAndLayersManagerNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksViewsAndLayersManagerNotify_SinkHelper : ksViewsAndLayersManagerNotify
//{
//  public ksViewsAndLayersManagerNotify_BeginEditEventHandler m_BeginEditDelegate;
//  public ksViewsAndLayersManagerNotify_EndEditEventHandler m_EndEditDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginEdit() => this.m_BeginEditDelegate != null && this.m_BeginEditDelegate();

//  public virtual bool EndEdit([In] bool obj0)
//  {
//    return this.m_EndEditDelegate != null && this.m_EndEditDelegate(obj0);
//  }

//  internal ksViewsAndLayersManagerNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginEditDelegate = (ksViewsAndLayersManagerNotify_BeginEditEventHandler) null;
//    this.m_EndEditDelegate = (ksViewsAndLayersManagerNotify_EndEditEventHandler) null;
//  }
//}
