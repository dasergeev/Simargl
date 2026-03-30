//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksSpecificationObjectNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksSpecificationObjectNotify_SinkHelper : ksSpecificationObjectNotify
//{
//  public ksSpecificationObjectNotify_BeginDeleteEventHandler m_BeginDeleteDelegate;
//  public ksSpecificationObjectNotify_DeleteEventHandler m_DeleteDelegate;
//  public ksSpecificationObjectNotify_CellDblClickEventHandler m_CellDblClickDelegate;
//  public ksSpecificationObjectNotify_CellBeginEditEventHandler m_CellBeginEditDelegate;
//  public ksSpecificationObjectNotify_ChangeCurrentEventHandler m_ChangeCurrentDelegate;
//  public ksSpecificationObjectNotify_DocumentBeginAddEventHandler m_DocumentBeginAddDelegate;
//  public ksSpecificationObjectNotify_DocumentAddEventHandler m_DocumentAddDelegate;
//  public ksSpecificationObjectNotify_DocumentRemoveEventHandler m_DocumentRemoveDelegate;
//  public ksSpecificationObjectNotify_BeginGeomChangeEventHandler m_BeginGeomChangeDelegate;
//  public ksSpecificationObjectNotify_GeomChangeEventHandler m_GeomChangeDelegate;
//  public ksSpecificationObjectNotify_BeginProcessEventHandler m_BeginProcessDelegate;
//  public ksSpecificationObjectNotify_EndProcessEventHandler m_EndProcessDelegate;
//  public ksSpecificationObjectNotify_CreateObjectEventHandler m_CreateObjectDelegate;
//  public ksSpecificationObjectNotify_UpdateObjectEventHandler m_UpdateObjectDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginDelete([In] ISpecificationObject obj0)
//  {
//    return this.m_BeginDeleteDelegate != null && this.m_BeginDeleteDelegate(obj0);
//  }

//  public virtual bool Delete([In] ISpecificationObject obj0)
//  {
//    return this.m_DeleteDelegate != null && this.m_DeleteDelegate(obj0);
//  }

//  public virtual bool CellDblClick([In] ISpecificationObject obj0, [In] int obj1)
//  {
//    return this.m_CellDblClickDelegate != null && this.m_CellDblClickDelegate(obj0, obj1);
//  }

//  public virtual bool CellBeginEdit([In] ISpecificationObject obj0, [In] int obj1)
//  {
//    return this.m_CellBeginEditDelegate != null && this.m_CellBeginEditDelegate(obj0, obj1);
//  }

//  public virtual bool ChangeCurrent([In] ISpecificationObject obj0)
//  {
//    return this.m_ChangeCurrentDelegate != null && this.m_ChangeCurrentDelegate(obj0);
//  }

//  public virtual bool DocumentBeginAdd([In] ISpecificationObject obj0)
//  {
//    return this.m_DocumentBeginAddDelegate != null && this.m_DocumentBeginAddDelegate(obj0);
//  }

//  public virtual bool DocumentAdd([In] ISpecificationObject obj0, [In] string obj1)
//  {
//    return this.m_DocumentAddDelegate != null && this.m_DocumentAddDelegate(obj0, obj1);
//  }

//  public virtual bool DocumentRemove([In] ISpecificationObject obj0, [In] string obj1)
//  {
//    return this.m_DocumentRemoveDelegate != null && this.m_DocumentRemoveDelegate(obj0, obj1);
//  }

//  public virtual bool BeginGeomChange([In] ISpecificationObject obj0)
//  {
//    return this.m_BeginGeomChangeDelegate != null && this.m_BeginGeomChangeDelegate(obj0);
//  }

//  public virtual bool GeomChange([In] ISpecificationObject obj0)
//  {
//    return this.m_GeomChangeDelegate != null && this.m_GeomChangeDelegate(obj0);
//  }

//  public virtual bool BeginProcess([In] int obj0, [In] ISpecificationObject obj1)
//  {
//    return this.m_BeginProcessDelegate != null && this.m_BeginProcessDelegate(obj0, obj1);
//  }

//  public virtual bool EndProcess([In] int obj0)
//  {
//    return this.m_EndProcessDelegate != null && this.m_EndProcessDelegate(obj0);
//  }

//  public virtual bool CreateObject([In] ISpecificationObject obj0)
//  {
//    return this.m_CreateObjectDelegate != null && this.m_CreateObjectDelegate(obj0);
//  }

//  public virtual bool UpdateObject([In] ISpecificationObject obj0)
//  {
//    return this.m_UpdateObjectDelegate != null && this.m_UpdateObjectDelegate(obj0);
//  }

//  internal ksSpecificationObjectNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginDeleteDelegate = (ksSpecificationObjectNotify_BeginDeleteEventHandler) null;
//    this.m_DeleteDelegate = (ksSpecificationObjectNotify_DeleteEventHandler) null;
//    this.m_CellDblClickDelegate = (ksSpecificationObjectNotify_CellDblClickEventHandler) null;
//    this.m_CellBeginEditDelegate = (ksSpecificationObjectNotify_CellBeginEditEventHandler) null;
//    this.m_ChangeCurrentDelegate = (ksSpecificationObjectNotify_ChangeCurrentEventHandler) null;
//    this.m_DocumentBeginAddDelegate = (ksSpecificationObjectNotify_DocumentBeginAddEventHandler) null;
//    this.m_DocumentAddDelegate = (ksSpecificationObjectNotify_DocumentAddEventHandler) null;
//    this.m_DocumentRemoveDelegate = (ksSpecificationObjectNotify_DocumentRemoveEventHandler) null;
//    this.m_BeginGeomChangeDelegate = (ksSpecificationObjectNotify_BeginGeomChangeEventHandler) null;
//    this.m_GeomChangeDelegate = (ksSpecificationObjectNotify_GeomChangeEventHandler) null;
//    this.m_BeginProcessDelegate = (ksSpecificationObjectNotify_BeginProcessEventHandler) null;
//    this.m_EndProcessDelegate = (ksSpecificationObjectNotify_EndProcessEventHandler) null;
//    this.m_CreateObjectDelegate = (ksSpecificationObjectNotify_CreateObjectEventHandler) null;
//    this.m_UpdateObjectDelegate = (ksSpecificationObjectNotify_UpdateObjectEventHandler) null;
//  }
//}
