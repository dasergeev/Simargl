//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksDrawingObjectNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksDrawingObjectNotify_SinkHelper : ksDrawingObjectNotify
//{
//  public ksDrawingObjectNotify_ChangeActiveEventHandler m_ChangeActiveDelegate;
//  public ksDrawingObjectNotify_BeginDeleteEventHandler m_BeginDeleteDelegate;
//  public ksDrawingObjectNotify_DeleteEventHandler m_DeleteDelegate;
//  public ksDrawingObjectNotify_BeginMoveEventHandler m_BeginMoveDelegate;
//  public ksDrawingObjectNotify_MoveEventHandler m_MoveDelegate;
//  public ksDrawingObjectNotify_BeginRotateEventHandler m_BeginRotateDelegate;
//  public ksDrawingObjectNotify_RotateEventHandler m_RotateDelegate;
//  public ksDrawingObjectNotify_BeginScaleEventHandler m_BeginScaleDelegate;
//  public ksDrawingObjectNotify_ScaleEventHandler m_ScaleDelegate;
//  public ksDrawingObjectNotify_BeginTransformEventHandler m_BeginTransformDelegate;
//  public ksDrawingObjectNotify_TransformEventHandler m_TransformDelegate;
//  public ksDrawingObjectNotify_BeginCopyEventHandler m_BeginCopyDelegate;
//  public ksDrawingObjectNotify_CopyEventHandler m_CopyDelegate;
//  public ksDrawingObjectNotify_BeginSymmetryEventHandler m_BeginSymmetryDelegate;
//  public ksDrawingObjectNotify_SymmetryEventHandler m_SymmetryDelegate;
//  public ksDrawingObjectNotify_BeginProcessEventHandler m_BeginProcessDelegate;
//  public ksDrawingObjectNotify_EndProcessEventHandler m_EndProcessDelegate;
//  public ksDrawingObjectNotify_CreateObjectEventHandler m_CreateObjectDelegate;
//  public ksDrawingObjectNotify_UpdateObjectEventHandler m_UpdateObjectDelegate;
//  public ksDrawingObjectNotify_BeginDestroyObjectEventHandler m_BeginDestroyObjectDelegate;
//  public ksDrawingObjectNotify_DestroyObjectEventHandler m_DestroyObjectDelegate;
//  public ksDrawingObjectNotify_BeginPropertyChangedEventHandler m_BeginPropertyChangedDelegate;
//  public ksDrawingObjectNotify_PropertyChangedEventHandler m_PropertyChangedDelegate;
//  public int m_dwCookie;

//  public virtual bool ChangeActive([In] object obj0)
//  {
//    return this.m_ChangeActiveDelegate != null && this.m_ChangeActiveDelegate(obj0);
//  }

//  public virtual bool BeginDelete([In] object obj0)
//  {
//    return this.m_BeginDeleteDelegate != null && this.m_BeginDeleteDelegate(obj0);
//  }

//  public virtual bool Delete([In] object obj0)
//  {
//    return this.m_DeleteDelegate != null && this.m_DeleteDelegate(obj0);
//  }

//  public virtual bool BeginMove([In] object obj0)
//  {
//    return this.m_BeginMoveDelegate != null && this.m_BeginMoveDelegate(obj0);
//  }

//  public virtual bool Move([In] object obj0)
//  {
//    return this.m_MoveDelegate != null && this.m_MoveDelegate(obj0);
//  }

//  public virtual bool BeginRotate([In] object obj0)
//  {
//    return this.m_BeginRotateDelegate != null && this.m_BeginRotateDelegate(obj0);
//  }

//  public virtual bool Rotate([In] object obj0)
//  {
//    return this.m_RotateDelegate != null && this.m_RotateDelegate(obj0);
//  }

//  public virtual bool BeginScale([In] object obj0)
//  {
//    return this.m_BeginScaleDelegate != null && this.m_BeginScaleDelegate(obj0);
//  }

//  public virtual bool Scale([In] object obj0)
//  {
//    return this.m_ScaleDelegate != null && this.m_ScaleDelegate(obj0);
//  }

//  public virtual bool BeginTransform([In] object obj0)
//  {
//    return this.m_BeginTransformDelegate != null && this.m_BeginTransformDelegate(obj0);
//  }

//  public virtual bool Transform([In] object obj0)
//  {
//    return this.m_TransformDelegate != null && this.m_TransformDelegate(obj0);
//  }

//  public virtual bool BeginCopy([In] object obj0)
//  {
//    return this.m_BeginCopyDelegate != null && this.m_BeginCopyDelegate(obj0);
//  }

//  public virtual bool Copy([In] object obj0)
//  {
//    return this.m_CopyDelegate != null && this.m_CopyDelegate(obj0);
//  }

//  public virtual bool BeginSymmetry([In] object obj0)
//  {
//    return this.m_BeginSymmetryDelegate != null && this.m_BeginSymmetryDelegate(obj0);
//  }

//  public virtual bool Symmetry([In] object obj0)
//  {
//    return this.m_SymmetryDelegate != null && this.m_SymmetryDelegate(obj0);
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

//  public virtual bool BeginDestroyObject([In] object obj0)
//  {
//    return this.m_BeginDestroyObjectDelegate != null && this.m_BeginDestroyObjectDelegate(obj0);
//  }

//  public virtual bool DestroyObject([In] object obj0)
//  {
//    return this.m_DestroyObjectDelegate != null && this.m_DestroyObjectDelegate(obj0);
//  }

//  public virtual bool BeginPropertyChanged([In] object obj0)
//  {
//    return this.m_BeginPropertyChangedDelegate != null && this.m_BeginPropertyChangedDelegate(obj0);
//  }

//  public virtual bool PropertyChanged([In] object obj0)
//  {
//    return this.m_PropertyChangedDelegate != null && this.m_PropertyChangedDelegate(obj0);
//  }

//  internal ksDrawingObjectNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_ChangeActiveDelegate = (ksDrawingObjectNotify_ChangeActiveEventHandler) null;
//    this.m_BeginDeleteDelegate = (ksDrawingObjectNotify_BeginDeleteEventHandler) null;
//    this.m_DeleteDelegate = (ksDrawingObjectNotify_DeleteEventHandler) null;
//    this.m_BeginMoveDelegate = (ksDrawingObjectNotify_BeginMoveEventHandler) null;
//    this.m_MoveDelegate = (ksDrawingObjectNotify_MoveEventHandler) null;
//    this.m_BeginRotateDelegate = (ksDrawingObjectNotify_BeginRotateEventHandler) null;
//    this.m_RotateDelegate = (ksDrawingObjectNotify_RotateEventHandler) null;
//    this.m_BeginScaleDelegate = (ksDrawingObjectNotify_BeginScaleEventHandler) null;
//    this.m_ScaleDelegate = (ksDrawingObjectNotify_ScaleEventHandler) null;
//    this.m_BeginTransformDelegate = (ksDrawingObjectNotify_BeginTransformEventHandler) null;
//    this.m_TransformDelegate = (ksDrawingObjectNotify_TransformEventHandler) null;
//    this.m_BeginCopyDelegate = (ksDrawingObjectNotify_BeginCopyEventHandler) null;
//    this.m_CopyDelegate = (ksDrawingObjectNotify_CopyEventHandler) null;
//    this.m_BeginSymmetryDelegate = (ksDrawingObjectNotify_BeginSymmetryEventHandler) null;
//    this.m_SymmetryDelegate = (ksDrawingObjectNotify_SymmetryEventHandler) null;
//    this.m_BeginProcessDelegate = (ksDrawingObjectNotify_BeginProcessEventHandler) null;
//    this.m_EndProcessDelegate = (ksDrawingObjectNotify_EndProcessEventHandler) null;
//    this.m_CreateObjectDelegate = (ksDrawingObjectNotify_CreateObjectEventHandler) null;
//    this.m_UpdateObjectDelegate = (ksDrawingObjectNotify_UpdateObjectEventHandler) null;
//    this.m_BeginDestroyObjectDelegate = (ksDrawingObjectNotify_BeginDestroyObjectEventHandler) null;
//    this.m_DestroyObjectDelegate = (ksDrawingObjectNotify_DestroyObjectEventHandler) null;
//    this.m_BeginPropertyChangedDelegate = (ksDrawingObjectNotify_BeginPropertyChangedEventHandler) null;
//    this.m_PropertyChangedDelegate = (ksDrawingObjectNotify_PropertyChangedEventHandler) null;
//  }
//}
