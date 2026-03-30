//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksSpcObjectNotify_SinkHelper
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksSpcObjectNotify_SinkHelper : ksSpcObjectNotify
//{
//  public ksSpcObjectNotify_BeginDeleteEventHandler m_BeginDeleteDelegate;
//  public ksSpcObjectNotify_DeleteEventHandler m_DeleteDelegate;
//  public ksSpcObjectNotify_CellDblClickEventHandler m_CellDblClickDelegate;
//  public ksSpcObjectNotify_CellBeginEditEventHandler m_CellBeginEditDelegate;
//  public ksSpcObjectNotify_ChangeCurrentEventHandler m_ChangeCurrentDelegate;
//  public ksSpcObjectNotify_DocumentBeginAddEventHandler m_DocumentBeginAddDelegate;
//  public ksSpcObjectNotify_DocumentAddEventHandler m_DocumentAddDelegate;
//  public ksSpcObjectNotify_DocumentRemoveEventHandler m_DocumentRemoveDelegate;
//  public ksSpcObjectNotify_BeginGeomChangeEventHandler m_BeginGeomChangeDelegate;
//  public ksSpcObjectNotify_GeomChangeEventHandler m_GeomChangeDelegate;
//  public ksSpcObjectNotify_BeginProcessEventHandler m_BeginProcessDelegate;
//  public ksSpcObjectNotify_EndProcessEventHandler m_EndProcessDelegate;
//  public ksSpcObjectNotify_CreateObjectEventHandler m_CreateObjectDelegate;
//  public ksSpcObjectNotify_UpdateObjectEventHandler m_UpdateObjectDelegate;
//  public ksSpcObjectNotify_BeginCopyEventHandler m_BeginCopyDelegate;
//  public ksSpcObjectNotify_copyEventHandler m_copyDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginDelete([In] int obj0)
//  {
//    return this.m_BeginDeleteDelegate != null && this.m_BeginDeleteDelegate(obj0);
//  }

//  public virtual bool Delete([In] int obj0)
//  {
//    return this.m_DeleteDelegate != null && this.m_DeleteDelegate(obj0);
//  }

//  public virtual bool CellDblClick([In] int obj0, [In] int obj1)
//  {
//    return this.m_CellDblClickDelegate != null && this.m_CellDblClickDelegate(obj0, obj1);
//  }

//  public virtual bool CellBeginEdit([In] int obj0, [In] int obj1)
//  {
//    return this.m_CellBeginEditDelegate != null && this.m_CellBeginEditDelegate(obj0, obj1);
//  }

//  public virtual bool ChangeCurrent([In] int obj0)
//  {
//    return this.m_ChangeCurrentDelegate != null && this.m_ChangeCurrentDelegate(obj0);
//  }

//  public virtual bool DocumentBeginAdd([In] int obj0)
//  {
//    return this.m_DocumentBeginAddDelegate != null && this.m_DocumentBeginAddDelegate(obj0);
//  }

//  public virtual bool DocumentAdd([In] int obj0, [In] string obj1)
//  {
//    return this.m_DocumentAddDelegate != null && this.m_DocumentAddDelegate(obj0, obj1);
//  }

//  public virtual bool DocumentRemove([In] int obj0, [In] string obj1)
//  {
//    return this.m_DocumentRemoveDelegate != null && this.m_DocumentRemoveDelegate(obj0, obj1);
//  }

//  public virtual bool BeginGeomChange([In] int obj0)
//  {
//    return this.m_BeginGeomChangeDelegate != null && this.m_BeginGeomChangeDelegate(obj0);
//  }

//  public virtual bool GeomChange([In] int obj0)
//  {
//    return this.m_GeomChangeDelegate != null && this.m_GeomChangeDelegate(obj0);
//  }

//  public virtual bool BeginProcess([In] int obj0, [In] int obj1)
//  {
//    return this.m_BeginProcessDelegate != null && this.m_BeginProcessDelegate(obj0, obj1);
//  }

//  public virtual bool EndProcess([In] int obj0)
//  {
//    return this.m_EndProcessDelegate != null && this.m_EndProcessDelegate(obj0);
//  }

//  public virtual bool CreateObject([In] int obj0)
//  {
//    return this.m_CreateObjectDelegate != null && this.m_CreateObjectDelegate(obj0);
//  }

//  public virtual bool UpdateObject([In] int obj0)
//  {
//    return this.m_UpdateObjectDelegate != null && this.m_UpdateObjectDelegate(obj0);
//  }

//  public virtual bool BeginCopy([In] int obj0)
//  {
//    return this.m_BeginCopyDelegate != null && this.m_BeginCopyDelegate(obj0);
//  }

//  public virtual bool copy([In] int obj0)
//  {
//    return this.m_copyDelegate != null && this.m_copyDelegate(obj0);
//  }

//  internal ksSpcObjectNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginDeleteDelegate = (ksSpcObjectNotify_BeginDeleteEventHandler) null;
//    this.m_DeleteDelegate = (ksSpcObjectNotify_DeleteEventHandler) null;
//    this.m_CellDblClickDelegate = (ksSpcObjectNotify_CellDblClickEventHandler) null;
//    this.m_CellBeginEditDelegate = (ksSpcObjectNotify_CellBeginEditEventHandler) null;
//    this.m_ChangeCurrentDelegate = (ksSpcObjectNotify_ChangeCurrentEventHandler) null;
//    this.m_DocumentBeginAddDelegate = (ksSpcObjectNotify_DocumentBeginAddEventHandler) null;
//    this.m_DocumentAddDelegate = (ksSpcObjectNotify_DocumentAddEventHandler) null;
//    this.m_DocumentRemoveDelegate = (ksSpcObjectNotify_DocumentRemoveEventHandler) null;
//    this.m_BeginGeomChangeDelegate = (ksSpcObjectNotify_BeginGeomChangeEventHandler) null;
//    this.m_GeomChangeDelegate = (ksSpcObjectNotify_GeomChangeEventHandler) null;
//    this.m_BeginProcessDelegate = (ksSpcObjectNotify_BeginProcessEventHandler) null;
//    this.m_EndProcessDelegate = (ksSpcObjectNotify_EndProcessEventHandler) null;
//    this.m_CreateObjectDelegate = (ksSpcObjectNotify_CreateObjectEventHandler) null;
//    this.m_UpdateObjectDelegate = (ksSpcObjectNotify_UpdateObjectEventHandler) null;
//    this.m_BeginCopyDelegate = (ksSpcObjectNotify_BeginCopyEventHandler) null;
//    this.m_copyDelegate = (ksSpcObjectNotify_copyEventHandler) null;
//  }
//}
