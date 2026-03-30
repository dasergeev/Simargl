//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksLibraryManagerNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksLibraryManagerNotify_SinkHelper : ksLibraryManagerNotify
//{
//  public ksLibraryManagerNotify_BeginAttachEventHandler m_BeginAttachDelegate;
//  public ksLibraryManagerNotify_AttachEventHandler m_AttachDelegate;
//  public ksLibraryManagerNotify_BeginDetachEventHandler m_BeginDetachDelegate;
//  public ksLibraryManagerNotify_DetachEventHandler m_DetachDelegate;
//  public ksLibraryManagerNotify_BeginExecuteEventHandler m_BeginExecuteDelegate;
//  public ksLibraryManagerNotify_EndExecuteEventHandler m_EndExecuteDelegate;
//  public ksLibraryManagerNotify_SystemControlStopEventHandler m_SystemControlStopDelegate;
//  public ksLibraryManagerNotify_SystemControlStartEventHandler m_SystemControlStartDelegate;
//  public ksLibraryManagerNotify_AddLibraryDescriptionEventHandler m_AddLibraryDescriptionDelegate;
//  public ksLibraryManagerNotify_DeleteLibraryDescriptionEventHandler m_DeleteLibraryDescriptionDelegate;
//  public ksLibraryManagerNotify_AddInsertEventHandler m_AddInsertDelegate;
//  public ksLibraryManagerNotify_DeleteInsertEventHandler m_DeleteInsertDelegate;
//  public ksLibraryManagerNotify_EditInsertEventHandler m_EditInsertDelegate;
//  public ksLibraryManagerNotify_TryExecuteEventHandler m_TryExecuteDelegate;
//  public ksLibraryManagerNotify_BeginInsertDocumentEventHandler m_BeginInsertDocumentDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginAttach([In] ILibrary obj0)
//  {
//    return this.m_BeginAttachDelegate != null && this.m_BeginAttachDelegate(obj0);
//  }

//  public virtual bool Attach([In] ILibrary obj0)
//  {
//    return this.m_AttachDelegate != null && this.m_AttachDelegate(obj0);
//  }

//  public virtual bool BeginDetach([In] ILibrary obj0)
//  {
//    return this.m_BeginDetachDelegate != null && this.m_BeginDetachDelegate(obj0);
//  }

//  public virtual bool Detach([In] ILibrary obj0)
//  {
//    return this.m_DetachDelegate != null && this.m_DetachDelegate(obj0);
//  }

//  public virtual bool BeginExecute([In] ILibrary obj0)
//  {
//    return this.m_BeginExecuteDelegate != null && this.m_BeginExecuteDelegate(obj0);
//  }

//  public virtual bool EndExecute([In] ILibrary obj0)
//  {
//    return this.m_EndExecuteDelegate != null && this.m_EndExecuteDelegate(obj0);
//  }

//  public virtual bool SystemControlStop([In] ILibrary obj0)
//  {
//    return this.m_SystemControlStopDelegate != null && this.m_SystemControlStopDelegate(obj0);
//  }

//  public virtual bool SystemControlStart([In] ILibrary obj0)
//  {
//    return this.m_SystemControlStartDelegate != null && this.m_SystemControlStartDelegate(obj0);
//  }

//  public virtual bool AddLibraryDescription([In] ILibrary obj0)
//  {
//    return this.m_AddLibraryDescriptionDelegate != null && this.m_AddLibraryDescriptionDelegate(obj0);
//  }

//  public virtual bool DeleteLibraryDescription([In] ILibrary obj0)
//  {
//    return this.m_DeleteLibraryDescriptionDelegate != null && this.m_DeleteLibraryDescriptionDelegate(obj0);
//  }

//  public virtual bool AddInsert([In] Insert obj0, [In] bool obj1)
//  {
//    return this.m_AddInsertDelegate != null && this.m_AddInsertDelegate(obj0, obj1);
//  }

//  public virtual bool DeleteInsert([In] Insert obj0)
//  {
//    return this.m_DeleteInsertDelegate != null && this.m_DeleteInsertDelegate(obj0);
//  }

//  public virtual bool EditInsert([In] ILibrary obj0, [In] IKompasDocument obj1, [In] bool obj2)
//  {
//    return this.m_EditInsertDelegate != null && this.m_EditInsertDelegate(obj0, obj1, obj2);
//  }

//  public virtual bool TryExecute([In] ILibrary obj0, [In] int obj1)
//  {
//    return this.m_TryExecuteDelegate != null && this.m_TryExecuteDelegate(obj0, obj1);
//  }

//  public virtual bool BeginInsertDocument([In] ILibrary obj0, [In] int obj1, [In] string obj2)
//  {
//    return this.m_BeginInsertDocumentDelegate != null && this.m_BeginInsertDocumentDelegate(obj0, obj1, obj2);
//  }

//  internal ksLibraryManagerNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginAttachDelegate = (ksLibraryManagerNotify_BeginAttachEventHandler) null;
//    this.m_AttachDelegate = (ksLibraryManagerNotify_AttachEventHandler) null;
//    this.m_BeginDetachDelegate = (ksLibraryManagerNotify_BeginDetachEventHandler) null;
//    this.m_DetachDelegate = (ksLibraryManagerNotify_DetachEventHandler) null;
//    this.m_BeginExecuteDelegate = (ksLibraryManagerNotify_BeginExecuteEventHandler) null;
//    this.m_EndExecuteDelegate = (ksLibraryManagerNotify_EndExecuteEventHandler) null;
//    this.m_SystemControlStopDelegate = (ksLibraryManagerNotify_SystemControlStopEventHandler) null;
//    this.m_SystemControlStartDelegate = (ksLibraryManagerNotify_SystemControlStartEventHandler) null;
//    this.m_AddLibraryDescriptionDelegate = (ksLibraryManagerNotify_AddLibraryDescriptionEventHandler) null;
//    this.m_DeleteLibraryDescriptionDelegate = (ksLibraryManagerNotify_DeleteLibraryDescriptionEventHandler) null;
//    this.m_AddInsertDelegate = (ksLibraryManagerNotify_AddInsertEventHandler) null;
//    this.m_DeleteInsertDelegate = (ksLibraryManagerNotify_DeleteInsertEventHandler) null;
//    this.m_EditInsertDelegate = (ksLibraryManagerNotify_EditInsertEventHandler) null;
//    this.m_TryExecuteDelegate = (ksLibraryManagerNotify_TryExecuteEventHandler) null;
//    this.m_BeginInsertDocumentDelegate = (ksLibraryManagerNotify_BeginInsertDocumentEventHandler) null;
//  }
//}
