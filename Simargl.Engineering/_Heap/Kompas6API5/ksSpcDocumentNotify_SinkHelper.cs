//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksSpcDocumentNotify_SinkHelper
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksSpcDocumentNotify_SinkHelper : ksSpcDocumentNotify
//{
//  public ksSpcDocumentNotify_DocumentBeginAddEventHandler m_DocumentBeginAddDelegate;
//  public ksSpcDocumentNotify_DocumentAddEventHandler m_DocumentAddDelegate;
//  public ksSpcDocumentNotify_DocumentBeginRemoveEventHandler m_DocumentBeginRemoveDelegate;
//  public ksSpcDocumentNotify_DocumentRemoveEventHandler m_DocumentRemoveDelegate;
//  public ksSpcDocumentNotify_SpcStyleBeginChangeEventHandler m_SpcStyleBeginChangeDelegate;
//  public ksSpcDocumentNotify_SpcStyleChangeEventHandler m_SpcStyleChangeDelegate;
//  public int m_dwCookie;

//  public virtual bool DocumentBeginAdd()
//  {
//    return this.m_DocumentBeginAddDelegate != null && this.m_DocumentBeginAddDelegate();
//  }

//  public virtual bool DocumentAdd([In] string obj0)
//  {
//    return this.m_DocumentAddDelegate != null && this.m_DocumentAddDelegate(obj0);
//  }

//  public virtual bool DocumentBeginRemove([In] string obj0)
//  {
//    return this.m_DocumentBeginRemoveDelegate != null && this.m_DocumentBeginRemoveDelegate(obj0);
//  }

//  public virtual bool DocumentRemove([In] string obj0)
//  {
//    return this.m_DocumentRemoveDelegate != null && this.m_DocumentRemoveDelegate(obj0);
//  }

//  public virtual bool SpcStyleBeginChange([In] string obj0, [In] int obj1)
//  {
//    return this.m_SpcStyleBeginChangeDelegate != null && this.m_SpcStyleBeginChangeDelegate(obj0, obj1);
//  }

//  public virtual bool SpcStyleChange([In] string obj0, [In] int obj1)
//  {
//    return this.m_SpcStyleChangeDelegate != null && this.m_SpcStyleChangeDelegate(obj0, obj1);
//  }

//  internal ksSpcDocumentNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_DocumentBeginAddDelegate = (ksSpcDocumentNotify_DocumentBeginAddEventHandler) null;
//    this.m_DocumentAddDelegate = (ksSpcDocumentNotify_DocumentAddEventHandler) null;
//    this.m_DocumentBeginRemoveDelegate = (ksSpcDocumentNotify_DocumentBeginRemoveEventHandler) null;
//    this.m_DocumentRemoveDelegate = (ksSpcDocumentNotify_DocumentRemoveEventHandler) null;
//    this.m_SpcStyleBeginChangeDelegate = (ksSpcDocumentNotify_SpcStyleBeginChangeEventHandler) null;
//    this.m_SpcStyleChangeDelegate = (ksSpcDocumentNotify_SpcStyleChangeEventHandler) null;
//  }
//}
