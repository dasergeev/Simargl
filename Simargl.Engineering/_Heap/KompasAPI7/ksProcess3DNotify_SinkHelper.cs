//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksProcess3DNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksProcess3DNotify_SinkHelper : ksProcess3DNotify
//{
//  public ksProcess3DNotify_PlacementChangeEventHandler m_PlacementChangeDelegate;
//  public ksProcess3DNotify_ExecuteCommandEventHandler m_ExecuteCommandDelegate;
//  public ksProcess3DNotify_RunEventHandler m_RunDelegate;
//  public ksProcess3DNotify_StopEventHandler m_StopDelegate;
//  public ksProcess3DNotify_ActivateEventHandler m_ActivateDelegate;
//  public ksProcess3DNotify_DeactivateEventHandler m_DeactivateDelegate;
//  public ksProcess3DNotify_FilterObjectEventHandler m_FilterObjectDelegate;
//  public ksProcess3DNotify_CreateTakeObjectEventHandler m_CreateTakeObjectDelegate;
//  public ksProcess3DNotify_EndProcessEventHandler m_EndProcessDelegate;
//  public ksProcess3DNotify_ProcessingGroupObjectsEventHandler m_ProcessingGroupObjectsDelegate;
//  public ksProcess3DNotify_AbortProcessEventHandler m_AbortProcessDelegate;
//  public int m_dwCookie;

//  public virtual bool PlacementChange([In] object obj0)
//  {
//    return this.m_PlacementChangeDelegate != null && this.m_PlacementChangeDelegate(obj0);
//  }

//  public virtual bool ExecuteCommand([In] int obj0)
//  {
//    return this.m_ExecuteCommandDelegate != null && this.m_ExecuteCommandDelegate(obj0);
//  }

//  public virtual bool Run() => this.m_RunDelegate != null && this.m_RunDelegate();

//  public virtual bool Stop() => this.m_StopDelegate != null && this.m_StopDelegate();

//  public virtual bool Activate() => this.m_ActivateDelegate != null && this.m_ActivateDelegate();

//  public virtual bool Deactivate()
//  {
//    return this.m_DeactivateDelegate != null && this.m_DeactivateDelegate();
//  }

//  public virtual bool FilterObject([In] object obj0)
//  {
//    return this.m_FilterObjectDelegate != null && this.m_FilterObjectDelegate(obj0);
//  }

//  public virtual bool CreateTakeObject([In] object obj0)
//  {
//    return this.m_CreateTakeObjectDelegate != null && this.m_CreateTakeObjectDelegate(obj0);
//  }

//  public virtual bool EndProcess()
//  {
//    return this.m_EndProcessDelegate != null && this.m_EndProcessDelegate();
//  }

//  public virtual bool ProcessingGroupObjects([In] object obj0, [In] int obj1)
//  {
//    return this.m_ProcessingGroupObjectsDelegate != null && this.m_ProcessingGroupObjectsDelegate(obj0, obj1);
//  }

//  public virtual bool AbortProcess()
//  {
//    return this.m_AbortProcessDelegate != null && this.m_AbortProcessDelegate();
//  }

//  internal ksProcess3DNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_PlacementChangeDelegate = (ksProcess3DNotify_PlacementChangeEventHandler) null;
//    this.m_ExecuteCommandDelegate = (ksProcess3DNotify_ExecuteCommandEventHandler) null;
//    this.m_RunDelegate = (ksProcess3DNotify_RunEventHandler) null;
//    this.m_StopDelegate = (ksProcess3DNotify_StopEventHandler) null;
//    this.m_ActivateDelegate = (ksProcess3DNotify_ActivateEventHandler) null;
//    this.m_DeactivateDelegate = (ksProcess3DNotify_DeactivateEventHandler) null;
//    this.m_FilterObjectDelegate = (ksProcess3DNotify_FilterObjectEventHandler) null;
//    this.m_CreateTakeObjectDelegate = (ksProcess3DNotify_CreateTakeObjectEventHandler) null;
//    this.m_EndProcessDelegate = (ksProcess3DNotify_EndProcessEventHandler) null;
//    this.m_ProcessingGroupObjectsDelegate = (ksProcess3DNotify_ProcessingGroupObjectsEventHandler) null;
//    this.m_AbortProcessDelegate = (ksProcess3DNotify_AbortProcessEventHandler) null;
//  }
//}
