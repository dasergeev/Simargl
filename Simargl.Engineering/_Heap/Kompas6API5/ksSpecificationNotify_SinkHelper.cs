//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksSpecificationNotify_SinkHelper
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksSpecificationNotify_SinkHelper : ksSpecificationNotify
//{
//  public ksSpecificationNotify_TuningSpcStyleBeginChangeEventHandler m_TuningSpcStyleBeginChangeDelegate;
//  public ksSpecificationNotify_TuningSpcStyleChangeEventHandler m_TuningSpcStyleChangeDelegate;
//  public ksSpecificationNotify_ChangeCurrentSpcDescriptionEventHandler m_ChangeCurrentSpcDescriptionDelegate;
//  public ksSpecificationNotify_SpcDescriptionAddEventHandler m_SpcDescriptionAddDelegate;
//  public ksSpecificationNotify_SpcDescriptionRemoveEventHandler m_SpcDescriptionRemoveDelegate;
//  public ksSpecificationNotify_SpcDescriptionBeginEditEventHandler m_SpcDescriptionBeginEditDelegate;
//  public ksSpecificationNotify_SpcDescriptionEditEventHandler m_SpcDescriptionEditDelegate;
//  public ksSpecificationNotify_SynchronizationBeginEventHandler m_SynchronizationBeginDelegate;
//  public ksSpecificationNotify_SynchronizationEventHandler m_SynchronizationDelegate;
//  public ksSpecificationNotify_BeginCalcPositionsEventHandler m_BeginCalcPositionsDelegate;
//  public ksSpecificationNotify_CalcPositionsEventHandler m_CalcPositionsDelegate;
//  public ksSpecificationNotify_BeginCreateObjectEventHandler m_BeginCreateObjectDelegate;
//  public int m_dwCookie;

//  public virtual bool TuningSpcStyleBeginChange([In] string obj0, [In] int obj1)
//  {
//    return this.m_TuningSpcStyleBeginChangeDelegate != null && this.m_TuningSpcStyleBeginChangeDelegate(obj0, obj1);
//  }

//  public virtual bool TuningSpcStyleChange([In] string obj0, [In] int obj1, [In] bool obj2)
//  {
//    return this.m_TuningSpcStyleChangeDelegate != null && this.m_TuningSpcStyleChangeDelegate(obj0, obj1, obj2);
//  }

//  public virtual bool ChangeCurrentSpcDescription([In] string obj0, [In] int obj1)
//  {
//    return this.m_ChangeCurrentSpcDescriptionDelegate != null && this.m_ChangeCurrentSpcDescriptionDelegate(obj0, obj1);
//  }

//  public virtual bool SpcDescriptionAdd([In] string obj0, [In] int obj1)
//  {
//    return this.m_SpcDescriptionAddDelegate != null && this.m_SpcDescriptionAddDelegate(obj0, obj1);
//  }

//  public virtual bool SpcDescriptionRemove([In] string obj0, [In] int obj1)
//  {
//    return this.m_SpcDescriptionRemoveDelegate != null && this.m_SpcDescriptionRemoveDelegate(obj0, obj1);
//  }

//  public virtual bool SpcDescriptionBeginEdit([In] string obj0, [In] int obj1)
//  {
//    return this.m_SpcDescriptionBeginEditDelegate != null && this.m_SpcDescriptionBeginEditDelegate(obj0, obj1);
//  }

//  public virtual bool SpcDescriptionEdit([In] string obj0, [In] int obj1, [In] bool obj2)
//  {
//    return this.m_SpcDescriptionEditDelegate != null && this.m_SpcDescriptionEditDelegate(obj0, obj1, obj2);
//  }

//  public virtual bool SynchronizationBegin()
//  {
//    return this.m_SynchronizationBeginDelegate != null && this.m_SynchronizationBeginDelegate();
//  }

//  public virtual bool Synchronization()
//  {
//    return this.m_SynchronizationDelegate != null && this.m_SynchronizationDelegate();
//  }

//  public virtual bool BeginCalcPositions()
//  {
//    return this.m_BeginCalcPositionsDelegate != null && this.m_BeginCalcPositionsDelegate();
//  }

//  public virtual bool CalcPositions()
//  {
//    return this.m_CalcPositionsDelegate != null && this.m_CalcPositionsDelegate();
//  }

//  public virtual bool BeginCreateObject([In] int obj0)
//  {
//    return this.m_BeginCreateObjectDelegate != null && this.m_BeginCreateObjectDelegate(obj0);
//  }

//  internal ksSpecificationNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_TuningSpcStyleBeginChangeDelegate = (ksSpecificationNotify_TuningSpcStyleBeginChangeEventHandler) null;
//    this.m_TuningSpcStyleChangeDelegate = (ksSpecificationNotify_TuningSpcStyleChangeEventHandler) null;
//    this.m_ChangeCurrentSpcDescriptionDelegate = (ksSpecificationNotify_ChangeCurrentSpcDescriptionEventHandler) null;
//    this.m_SpcDescriptionAddDelegate = (ksSpecificationNotify_SpcDescriptionAddEventHandler) null;
//    this.m_SpcDescriptionRemoveDelegate = (ksSpecificationNotify_SpcDescriptionRemoveEventHandler) null;
//    this.m_SpcDescriptionBeginEditDelegate = (ksSpecificationNotify_SpcDescriptionBeginEditEventHandler) null;
//    this.m_SpcDescriptionEditDelegate = (ksSpecificationNotify_SpcDescriptionEditEventHandler) null;
//    this.m_SynchronizationBeginDelegate = (ksSpecificationNotify_SynchronizationBeginEventHandler) null;
//    this.m_SynchronizationDelegate = (ksSpecificationNotify_SynchronizationEventHandler) null;
//    this.m_BeginCalcPositionsDelegate = (ksSpecificationNotify_BeginCalcPositionsEventHandler) null;
//    this.m_CalcPositionsDelegate = (ksSpecificationNotify_CalcPositionsEventHandler) null;
//    this.m_BeginCreateObjectDelegate = (ksSpecificationNotify_BeginCreateObjectEventHandler) null;
//  }
//}
