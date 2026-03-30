//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksObject2DNotify_SinkHelper
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksObject2DNotify_SinkHelper : ksObject2DNotify
//{
//  public ksObject2DNotify_ChangeActiveEventHandler m_ChangeActiveDelegate;
//  public ksObject2DNotify_BeginDeleteEventHandler m_BeginDeleteDelegate;
//  public ksObject2DNotify_DeleteEventHandler m_DeleteDelegate;
//  public ksObject2DNotify_BeginMoveEventHandler m_BeginMoveDelegate;
//  public ksObject2DNotify_MoveEventHandler m_MoveDelegate;
//  public ksObject2DNotify_BeginRotateEventHandler m_BeginRotateDelegate;
//  public ksObject2DNotify_RotateEventHandler m_RotateDelegate;
//  public ksObject2DNotify_BeginScaleEventHandler m_BeginScaleDelegate;
//  public ksObject2DNotify_scaleEventHandler m_scaleDelegate;
//  public ksObject2DNotify_BeginTransformEventHandler m_BeginTransformDelegate;
//  public ksObject2DNotify_TransformEventHandler m_TransformDelegate;
//  public ksObject2DNotify_BeginCopyEventHandler m_BeginCopyDelegate;
//  public ksObject2DNotify_copyEventHandler m_copyDelegate;
//  public ksObject2DNotify_BeginSymmetryEventHandler m_BeginSymmetryDelegate;
//  public ksObject2DNotify_SymmetryEventHandler m_SymmetryDelegate;
//  public ksObject2DNotify_BeginProcessEventHandler m_BeginProcessDelegate;
//  public ksObject2DNotify_EndProcessEventHandler m_EndProcessDelegate;
//  public ksObject2DNotify_CreateObjectEventHandler m_CreateObjectDelegate;
//  public ksObject2DNotify_UpdateObjectEventHandler m_UpdateObjectDelegate;
//  public ksObject2DNotify_BeginDestroyObjectEventHandler m_BeginDestroyObjectDelegate;
//  public ksObject2DNotify_DestroyObjectEventHandler m_DestroyObjectDelegate;
//  public ksObject2DNotify_BeginPropertyChangedEventHandler m_BeginPropertyChangedDelegate;
//  public ksObject2DNotify_PropertyChangedEventHandler m_PropertyChangedDelegate;
//  public int m_dwCookie;

//  public virtual bool ChangeActive([In] int obj0)
//  {
//    return this.m_ChangeActiveDelegate != null && this.m_ChangeActiveDelegate(obj0);
//  }

//  public virtual bool BeginDelete([In] int obj0)
//  {
//    return this.m_BeginDeleteDelegate != null && this.m_BeginDeleteDelegate(obj0);
//  }

//  public virtual bool Delete([In] int obj0)
//  {
//    return this.m_DeleteDelegate != null && this.m_DeleteDelegate(obj0);
//  }

//  public virtual bool BeginMove([In] int obj0)
//  {
//    return this.m_BeginMoveDelegate != null && this.m_BeginMoveDelegate(obj0);
//  }

//  public virtual bool Move([In] int obj0)
//  {
//    return this.m_MoveDelegate != null && this.m_MoveDelegate(obj0);
//  }

//  public virtual bool BeginRotate([In] int obj0)
//  {
//    return this.m_BeginRotateDelegate != null && this.m_BeginRotateDelegate(obj0);
//  }

//  public virtual bool Rotate([In] int obj0)
//  {
//    return this.m_RotateDelegate != null && this.m_RotateDelegate(obj0);
//  }

//  public virtual bool BeginScale([In] int obj0)
//  {
//    return this.m_BeginScaleDelegate != null && this.m_BeginScaleDelegate(obj0);
//  }

//  public virtual bool scale([In] int obj0)
//  {
//    return this.m_scaleDelegate != null && this.m_scaleDelegate(obj0);
//  }

//  public virtual bool BeginTransform([In] int obj0)
//  {
//    return this.m_BeginTransformDelegate != null && this.m_BeginTransformDelegate(obj0);
//  }

//  public virtual bool Transform([In] int obj0)
//  {
//    return this.m_TransformDelegate != null && this.m_TransformDelegate(obj0);
//  }

//  public virtual bool BeginCopy([In] int obj0)
//  {
//    return this.m_BeginCopyDelegate != null && this.m_BeginCopyDelegate(obj0);
//  }

//  public virtual bool copy([In] int obj0)
//  {
//    return this.m_copyDelegate != null && this.m_copyDelegate(obj0);
//  }

//  public virtual bool BeginSymmetry([In] int obj0)
//  {
//    return this.m_BeginSymmetryDelegate != null && this.m_BeginSymmetryDelegate(obj0);
//  }

//  public virtual bool Symmetry([In] int obj0)
//  {
//    return this.m_SymmetryDelegate != null && this.m_SymmetryDelegate(obj0);
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

//  public virtual bool BeginDestroyObject([In] int obj0)
//  {
//    return this.m_BeginDestroyObjectDelegate != null && this.m_BeginDestroyObjectDelegate(obj0);
//  }

//  public virtual bool DestroyObject([In] int obj0)
//  {
//    return this.m_DestroyObjectDelegate != null && this.m_DestroyObjectDelegate(obj0);
//  }

//  public virtual bool BeginPropertyChanged([In] int obj0)
//  {
//    return this.m_BeginPropertyChangedDelegate != null && this.m_BeginPropertyChangedDelegate(obj0);
//  }

//  public virtual bool PropertyChanged([In] int obj0)
//  {
//    return this.m_PropertyChangedDelegate != null && this.m_PropertyChangedDelegate(obj0);
//  }

//  internal ksObject2DNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_ChangeActiveDelegate = (ksObject2DNotify_ChangeActiveEventHandler) null;
//    this.m_BeginDeleteDelegate = (ksObject2DNotify_BeginDeleteEventHandler) null;
//    this.m_DeleteDelegate = (ksObject2DNotify_DeleteEventHandler) null;
//    this.m_BeginMoveDelegate = (ksObject2DNotify_BeginMoveEventHandler) null;
//    this.m_MoveDelegate = (ksObject2DNotify_MoveEventHandler) null;
//    this.m_BeginRotateDelegate = (ksObject2DNotify_BeginRotateEventHandler) null;
//    this.m_RotateDelegate = (ksObject2DNotify_RotateEventHandler) null;
//    this.m_BeginScaleDelegate = (ksObject2DNotify_BeginScaleEventHandler) null;
//    this.m_scaleDelegate = (ksObject2DNotify_scaleEventHandler) null;
//    this.m_BeginTransformDelegate = (ksObject2DNotify_BeginTransformEventHandler) null;
//    this.m_TransformDelegate = (ksObject2DNotify_TransformEventHandler) null;
//    this.m_BeginCopyDelegate = (ksObject2DNotify_BeginCopyEventHandler) null;
//    this.m_copyDelegate = (ksObject2DNotify_copyEventHandler) null;
//    this.m_BeginSymmetryDelegate = (ksObject2DNotify_BeginSymmetryEventHandler) null;
//    this.m_SymmetryDelegate = (ksObject2DNotify_SymmetryEventHandler) null;
//    this.m_BeginProcessDelegate = (ksObject2DNotify_BeginProcessEventHandler) null;
//    this.m_EndProcessDelegate = (ksObject2DNotify_EndProcessEventHandler) null;
//    this.m_CreateObjectDelegate = (ksObject2DNotify_CreateObjectEventHandler) null;
//    this.m_UpdateObjectDelegate = (ksObject2DNotify_UpdateObjectEventHandler) null;
//    this.m_BeginDestroyObjectDelegate = (ksObject2DNotify_BeginDestroyObjectEventHandler) null;
//    this.m_DestroyObjectDelegate = (ksObject2DNotify_DestroyObjectEventHandler) null;
//    this.m_BeginPropertyChangedDelegate = (ksObject2DNotify_BeginPropertyChangedEventHandler) null;
//    this.m_PropertyChangedDelegate = (ksObject2DNotify_PropertyChangedEventHandler) null;
//  }
//}
