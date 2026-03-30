//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksSpecificationDescriptionNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksSpecificationDescriptionNotify_SinkHelper : ksSpecificationDescriptionNotify
//{
//  public ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler m_TuningSpcStyleBeginChangeDelegate;
//  public ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler m_TuningSpcStyleChangeDelegate;
//  public ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler m_ChangeCurrentSpcDescriptionDelegate;
//  public ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler m_SpcDescriptionAddDelegate;
//  public ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler m_SpcDescriptionRemoveDelegate;
//  public ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler m_SpcDescriptionBeginEditDelegate;
//  public ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler m_SpcDescriptionEditDelegate;
//  public ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler m_SynchronizationBeginDelegate;
//  public ksSpecificationDescriptionNotify_SynchronizationEventHandler m_SynchronizationDelegate;
//  public ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler m_BeginCalcPositionsDelegate;
//  public ksSpecificationDescriptionNotify_CalcPositionsEventHandler m_CalcPositionsDelegate;
//  public ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler m_BeginCreateObjectDelegate;
//  public int m_dwCookie;

//  public virtual bool TuningSpcStyleBeginChange([In] SpecificationDescription obj0)
//  {
//    return this.m_TuningSpcStyleBeginChangeDelegate != null && this.m_TuningSpcStyleBeginChangeDelegate(obj0);
//  }

//  public virtual bool TuningSpcStyleChange([In] SpecificationDescription obj0, [In] bool obj1)
//  {
//    return this.m_TuningSpcStyleChangeDelegate != null && this.m_TuningSpcStyleChangeDelegate(obj0, obj1);
//  }

//  public virtual bool ChangeCurrentSpcDescription([In] SpecificationDescription obj0)
//  {
//    return this.m_ChangeCurrentSpcDescriptionDelegate != null && this.m_ChangeCurrentSpcDescriptionDelegate(obj0);
//  }

//  public virtual bool SpcDescriptionAdd([In] SpecificationDescription obj0)
//  {
//    return this.m_SpcDescriptionAddDelegate != null && this.m_SpcDescriptionAddDelegate(obj0);
//  }

//  public virtual bool SpcDescriptionRemove([In] SpecificationDescription obj0)
//  {
//    return this.m_SpcDescriptionRemoveDelegate != null && this.m_SpcDescriptionRemoveDelegate(obj0);
//  }

//  public virtual bool SpcDescriptionBeginEdit([In] SpecificationDescription obj0)
//  {
//    return this.m_SpcDescriptionBeginEditDelegate != null && this.m_SpcDescriptionBeginEditDelegate(obj0);
//  }

//  public virtual bool SpcDescriptionEdit([In] SpecificationDescription obj0, [In] bool obj1)
//  {
//    return this.m_SpcDescriptionEditDelegate != null && this.m_SpcDescriptionEditDelegate(obj0, obj1);
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

//  internal ksSpecificationDescriptionNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_TuningSpcStyleBeginChangeDelegate = (ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler) null;
//    this.m_TuningSpcStyleChangeDelegate = (ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler) null;
//    this.m_ChangeCurrentSpcDescriptionDelegate = (ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler) null;
//    this.m_SpcDescriptionAddDelegate = (ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler) null;
//    this.m_SpcDescriptionRemoveDelegate = (ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler) null;
//    this.m_SpcDescriptionBeginEditDelegate = (ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler) null;
//    this.m_SpcDescriptionEditDelegate = (ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler) null;
//    this.m_SynchronizationBeginDelegate = (ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler) null;
//    this.m_SynchronizationDelegate = (ksSpecificationDescriptionNotify_SynchronizationEventHandler) null;
//    this.m_BeginCalcPositionsDelegate = (ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler) null;
//    this.m_CalcPositionsDelegate = (ksSpecificationDescriptionNotify_CalcPositionsEventHandler) null;
//    this.m_BeginCreateObjectDelegate = (ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler) null;
//  }
//}
