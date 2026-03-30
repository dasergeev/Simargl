//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksPropertyUserControlNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksPropertyUserControlNotify_SinkHelper : ksPropertyUserControlNotify
//{
//  public ksPropertyUserControlNotify_CreateOCXEventHandler m_CreateOCXDelegate;
//  public ksPropertyUserControlNotify_DestroyOCXEventHandler m_DestroyOCXDelegate;
//  public int m_dwCookie;

//  public virtual bool CreateOCX([In] object obj0)
//  {
//    return this.m_CreateOCXDelegate != null && this.m_CreateOCXDelegate(obj0);
//  }

//  public virtual bool DestroyOCX()
//  {
//    return this.m_DestroyOCXDelegate != null && this.m_DestroyOCXDelegate();
//  }

//  internal ksPropertyUserControlNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_CreateOCXDelegate = (ksPropertyUserControlNotify_CreateOCXEventHandler) null;
//    this.m_DestroyOCXDelegate = (ksPropertyUserControlNotify_DestroyOCXEventHandler) null;
//  }
//}
