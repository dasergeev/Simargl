//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksProcess2DNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksProcess2DNotify_SinkHelper : ksProcess2DNotify
//{
//  public ksProcess2DNotify_PlacementChangeEventHandler m_PlacementChangeDelegate;
//  public ksProcess2DNotify_ExecuteCommandEventHandler m_ExecuteCommandDelegate;
//  public ksProcess2DNotify_RunEventHandler m_RunDelegate;
//  public ksProcess2DNotify_StopEventHandler m_StopDelegate;
//  public ksProcess2DNotify_ActivateEventHandler m_ActivateDelegate;
//  public ksProcess2DNotify_DeactivateEventHandler m_DeactivateDelegate;
//  public ksProcess2DNotify_EndProcessEventHandler m_EndProcessDelegate;
//  public ksProcess2DNotify_GetMouseEnterLeavePointEventHandler m_GetMouseEnterLeavePointDelegate;
//  public ksProcess2DNotify_AbortProcessEventHandler m_AbortProcessDelegate;
//  public int m_dwCookie;

//  public virtual bool PlacementChange([In] double obj0, [In] double obj1, [In] double obj2, [In] bool obj3)
//  {
//    return this.m_PlacementChangeDelegate != null && this.m_PlacementChangeDelegate(obj0, obj1, obj2, obj3);
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

//  public virtual bool EndProcess()
//  {
//    return this.m_EndProcessDelegate != null && this.m_EndProcessDelegate();
//  }

//  public virtual bool GetMouseEnterLeavePoint([In] object obj0, [In] int obj1, [In] int obj2, [In] object obj3)
//  {
//    return this.m_GetMouseEnterLeavePointDelegate != null && this.m_GetMouseEnterLeavePointDelegate(obj0, obj1, obj2, obj3);
//  }

//  public virtual bool AbortProcess()
//  {
//    return this.m_AbortProcessDelegate != null && this.m_AbortProcessDelegate();
//  }

//  internal ksProcess2DNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_PlacementChangeDelegate = (ksProcess2DNotify_PlacementChangeEventHandler) null;
//    this.m_ExecuteCommandDelegate = (ksProcess2DNotify_ExecuteCommandEventHandler) null;
//    this.m_RunDelegate = (ksProcess2DNotify_RunEventHandler) null;
//    this.m_StopDelegate = (ksProcess2DNotify_StopEventHandler) null;
//    this.m_ActivateDelegate = (ksProcess2DNotify_ActivateEventHandler) null;
//    this.m_DeactivateDelegate = (ksProcess2DNotify_DeactivateEventHandler) null;
//    this.m_EndProcessDelegate = (ksProcess2DNotify_EndProcessEventHandler) null;
//    this.m_GetMouseEnterLeavePointDelegate = (ksProcess2DNotify_GetMouseEnterLeavePointEventHandler) null;
//    this.m_AbortProcessDelegate = (ksProcess2DNotify_AbortProcessEventHandler) null;
//  }
//}
