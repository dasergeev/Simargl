//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksModelObjectNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksModelObjectNotify_SinkHelper : ksModelObjectNotify
//{
//  public ksModelObjectNotify_BeginDeleteEventHandler m_BeginDeleteDelegate;
//  public ksModelObjectNotify_DeleteEventHandler m_DeleteDelegate;
//  public ksModelObjectNotify_ExcludedEventHandler m_ExcludedDelegate;
//  public ksModelObjectNotify_HiddenEventHandler m_HiddenDelegate;
//  public ksModelObjectNotify_BeginPropertyChangedEventHandler m_BeginPropertyChangedDelegate;
//  public ksModelObjectNotify_PropertyChangedEventHandler m_PropertyChangedDelegate;
//  public ksModelObjectNotify_BeginPlacementChangedEventHandler m_BeginPlacementChangedDelegate;
//  public ksModelObjectNotify_PlacementChangedEventHandler m_PlacementChangedDelegate;
//  public ksModelObjectNotify_BeginProcessEventHandler m_BeginProcessDelegate;
//  public ksModelObjectNotify_EndProcessEventHandler m_EndProcessDelegate;
//  public ksModelObjectNotify_CreateObjectEventHandler m_CreateObjectDelegate;
//  public ksModelObjectNotify_UpdateObjectEventHandler m_UpdateObjectDelegate;
//  public ksModelObjectNotify_BeginLoadStateChangeEventHandler m_BeginLoadStateChangeDelegate;
//  public ksModelObjectNotify_LoadStateChangeEventHandler m_LoadStateChangeDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginDelete([In] object obj0)
//  {
//    return this.m_BeginDeleteDelegate != null && this.m_BeginDeleteDelegate(obj0);
//  }

//  public virtual bool Delete([In] object obj0)
//  {
//    return this.m_DeleteDelegate != null && this.m_DeleteDelegate(obj0);
//  }

//  public virtual bool Excluded([In] object obj0, [In] bool obj1)
//  {
//    return this.m_ExcludedDelegate != null && this.m_ExcludedDelegate(obj0, obj1);
//  }

//  public virtual bool Hidden([In] object obj0, [In] bool obj1)
//  {
//    return this.m_HiddenDelegate != null && this.m_HiddenDelegate(obj0, obj1);
//  }

//  public virtual bool BeginPropertyChanged([In] object obj0)
//  {
//    return this.m_BeginPropertyChangedDelegate != null && this.m_BeginPropertyChangedDelegate(obj0);
//  }

//  public virtual bool PropertyChanged([In] object obj0)
//  {
//    return this.m_PropertyChangedDelegate != null && this.m_PropertyChangedDelegate(obj0);
//  }

//  public virtual bool BeginPlacementChanged([In] object obj0)
//  {
//    return this.m_BeginPlacementChangedDelegate != null && this.m_BeginPlacementChangedDelegate(obj0);
//  }

//  public virtual bool PlacementChanged([In] object obj0)
//  {
//    return this.m_PlacementChangedDelegate != null && this.m_PlacementChangedDelegate(obj0);
//  }

//  public virtual bool BeginProcess([In] int obj0, [In] object obj1)
//  {
//    return this.m_BeginProcessDelegate != null && this.m_BeginProcessDelegate(obj0, obj1);
//  }

//  public virtual bool EndProcess([In] int obj0)
//  {
//    return this.m_EndProcessDelegate != null && this.m_EndProcessDelegate(obj0);
//  }

//  public virtual bool CreateObject([In] object obj0)
//  {
//    return this.m_CreateObjectDelegate != null && this.m_CreateObjectDelegate(obj0);
//  }

//  public virtual bool UpdateObject([In] object obj0)
//  {
//    return this.m_UpdateObjectDelegate != null && this.m_UpdateObjectDelegate(obj0);
//  }

//  public virtual bool BeginLoadStateChange([In] object obj0, [In] int obj1)
//  {
//    return this.m_BeginLoadStateChangeDelegate != null && this.m_BeginLoadStateChangeDelegate(obj0, obj1);
//  }

//  public virtual bool LoadStateChange([In] object obj0, [In] int obj1)
//  {
//    return this.m_LoadStateChangeDelegate != null && this.m_LoadStateChangeDelegate(obj0, obj1);
//  }

//  internal ksModelObjectNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginDeleteDelegate = (ksModelObjectNotify_BeginDeleteEventHandler) null;
//    this.m_DeleteDelegate = (ksModelObjectNotify_DeleteEventHandler) null;
//    this.m_ExcludedDelegate = (ksModelObjectNotify_ExcludedEventHandler) null;
//    this.m_HiddenDelegate = (ksModelObjectNotify_HiddenEventHandler) null;
//    this.m_BeginPropertyChangedDelegate = (ksModelObjectNotify_BeginPropertyChangedEventHandler) null;
//    this.m_PropertyChangedDelegate = (ksModelObjectNotify_PropertyChangedEventHandler) null;
//    this.m_BeginPlacementChangedDelegate = (ksModelObjectNotify_BeginPlacementChangedEventHandler) null;
//    this.m_PlacementChangedDelegate = (ksModelObjectNotify_PlacementChangedEventHandler) null;
//    this.m_BeginProcessDelegate = (ksModelObjectNotify_BeginProcessEventHandler) null;
//    this.m_EndProcessDelegate = (ksModelObjectNotify_EndProcessEventHandler) null;
//    this.m_CreateObjectDelegate = (ksModelObjectNotify_CreateObjectEventHandler) null;
//    this.m_UpdateObjectDelegate = (ksModelObjectNotify_UpdateObjectEventHandler) null;
//    this.m_BeginLoadStateChangeDelegate = (ksModelObjectNotify_BeginLoadStateChangeEventHandler) null;
//    this.m_LoadStateChangeDelegate = (ksModelObjectNotify_LoadStateChangeEventHandler) null;
//  }
//}
