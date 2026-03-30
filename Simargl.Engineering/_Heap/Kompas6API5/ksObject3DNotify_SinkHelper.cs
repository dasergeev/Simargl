//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksObject3DNotify_SinkHelper
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksObject3DNotify_SinkHelper : ksObject3DNotify
//{
//  public ksObject3DNotify_BeginDeleteEventHandler m_BeginDeleteDelegate;
//  public ksObject3DNotify_DeleteEventHandler m_DeleteDelegate;
//  public ksObject3DNotify_excludedEventHandler m_excludedDelegate;
//  public ksObject3DNotify_hiddenEventHandler m_hiddenDelegate;
//  public ksObject3DNotify_BeginPropertyChangedEventHandler m_BeginPropertyChangedDelegate;
//  public ksObject3DNotify_PropertyChangedEventHandler m_PropertyChangedDelegate;
//  public ksObject3DNotify_BeginPlacementChangedEventHandler m_BeginPlacementChangedDelegate;
//  public ksObject3DNotify_PlacementChangedEventHandler m_PlacementChangedDelegate;
//  public ksObject3DNotify_BeginProcessEventHandler m_BeginProcessDelegate;
//  public ksObject3DNotify_EndProcessEventHandler m_EndProcessDelegate;
//  public ksObject3DNotify_CreateObjectEventHandler m_CreateObjectDelegate;
//  public ksObject3DNotify_UpdateObjectEventHandler m_UpdateObjectDelegate;
//  public ksObject3DNotify_BeginLoadStateChangeEventHandler m_BeginLoadStateChangeDelegate;
//  public ksObject3DNotify_LoadStateChangeEventHandler m_LoadStateChangeDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginDelete([In] object obj0)
//  {
//    return this.m_BeginDeleteDelegate != null && this.m_BeginDeleteDelegate(obj0);
//  }

//  public virtual bool Delete([In] object obj0)
//  {
//    return this.m_DeleteDelegate != null && this.m_DeleteDelegate(obj0);
//  }

//  public virtual bool excluded([In] object obj0, [In] bool obj1)
//  {
//    return this.m_excludedDelegate != null && this.m_excludedDelegate(obj0, obj1);
//  }

//  public virtual bool hidden([In] object obj0, [In] bool obj1)
//  {
//    return this.m_hiddenDelegate != null && this.m_hiddenDelegate(obj0, obj1);
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

//  internal ksObject3DNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginDeleteDelegate = (ksObject3DNotify_BeginDeleteEventHandler) null;
//    this.m_DeleteDelegate = (ksObject3DNotify_DeleteEventHandler) null;
//    this.m_excludedDelegate = (ksObject3DNotify_excludedEventHandler) null;
//    this.m_hiddenDelegate = (ksObject3DNotify_hiddenEventHandler) null;
//    this.m_BeginPropertyChangedDelegate = (ksObject3DNotify_BeginPropertyChangedEventHandler) null;
//    this.m_PropertyChangedDelegate = (ksObject3DNotify_PropertyChangedEventHandler) null;
//    this.m_BeginPlacementChangedDelegate = (ksObject3DNotify_BeginPlacementChangedEventHandler) null;
//    this.m_PlacementChangedDelegate = (ksObject3DNotify_PlacementChangedEventHandler) null;
//    this.m_BeginProcessDelegate = (ksObject3DNotify_BeginProcessEventHandler) null;
//    this.m_EndProcessDelegate = (ksObject3DNotify_EndProcessEventHandler) null;
//    this.m_CreateObjectDelegate = (ksObject3DNotify_CreateObjectEventHandler) null;
//    this.m_UpdateObjectDelegate = (ksObject3DNotify_UpdateObjectEventHandler) null;
//    this.m_BeginLoadStateChangeDelegate = (ksObject3DNotify_BeginLoadStateChangeEventHandler) null;
//    this.m_LoadStateChangeDelegate = (ksObject3DNotify_LoadStateChangeEventHandler) null;
//  }
//}
