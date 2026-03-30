//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.Object2DNotifyClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ComSourceInterfaces("Kompas6API5.ksObject2DNotify\0\0")]
//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[Guid("C7EBA9A1-9E76-436E-B362-A80C5763944C")]
//[ComImport]
//public class Object2DNotifyClass : Object2DNotify, ksObject2DNotify_Event
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern Object2DNotifyClass();

//  public virtual extern event ksObject2DNotify_ChangeActiveEventHandler ChangeActive;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeActive([In] ksObject2DNotify_ChangeActiveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeActive([In] ksObject2DNotify_ChangeActiveEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginDeleteEventHandler BeginDelete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDelete([In] ksObject2DNotify_BeginDeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDelete([In] ksObject2DNotify_BeginDeleteEventHandler obj0);

//  public virtual extern event ksObject2DNotify_DeleteEventHandler Delete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Delete([In] ksObject2DNotify_DeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Delete([In] ksObject2DNotify_DeleteEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginMoveEventHandler BeginMove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginMove([In] ksObject2DNotify_BeginMoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginMove([In] ksObject2DNotify_BeginMoveEventHandler obj0);

//  public virtual extern event ksObject2DNotify_MoveEventHandler Move;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Move([In] ksObject2DNotify_MoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Move([In] ksObject2DNotify_MoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginRotate([In] ksObject2DNotify_BeginRotateEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginRotateEventHandler BeginRotate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginRotate([In] ksObject2DNotify_BeginRotateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Rotate([In] ksObject2DNotify_RotateEventHandler obj0);

//  public virtual extern event ksObject2DNotify_RotateEventHandler Rotate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Rotate([In] ksObject2DNotify_RotateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginScale([In] ksObject2DNotify_BeginScaleEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginScaleEventHandler BeginScale;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginScale([In] ksObject2DNotify_BeginScaleEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_scale([In] ksObject2DNotify_scaleEventHandler obj0);

//  public virtual extern event ksObject2DNotify_scaleEventHandler scale;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_scale([In] ksObject2DNotify_scaleEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginTransformEventHandler BeginTransform;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginTransform([In] ksObject2DNotify_BeginTransformEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginTransform([In] ksObject2DNotify_BeginTransformEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Transform([In] ksObject2DNotify_TransformEventHandler obj0);

//  public virtual extern event ksObject2DNotify_TransformEventHandler Transform;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Transform([In] ksObject2DNotify_TransformEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginCopyEventHandler BeginCopy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCopy([In] ksObject2DNotify_BeginCopyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCopy([In] ksObject2DNotify_BeginCopyEventHandler obj0);

//  public virtual extern event ksObject2DNotify_copyEventHandler copy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_copy([In] ksObject2DNotify_copyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_copy([In] ksObject2DNotify_copyEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginSymmetryEventHandler BeginSymmetry;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginSymmetry([In] ksObject2DNotify_BeginSymmetryEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginSymmetry([In] ksObject2DNotify_BeginSymmetryEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Symmetry([In] ksObject2DNotify_SymmetryEventHandler obj0);

//  public virtual extern event ksObject2DNotify_SymmetryEventHandler Symmetry;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Symmetry([In] ksObject2DNotify_SymmetryEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginProcess([In] ksObject2DNotify_BeginProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginProcess([In] ksObject2DNotify_BeginProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndProcess([In] ksObject2DNotify_EndProcessEventHandler obj0);

//  public virtual extern event ksObject2DNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndProcess([In] ksObject2DNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CreateObject([In] ksObject2DNotify_CreateObjectEventHandler obj0);

//  public virtual extern event ksObject2DNotify_CreateObjectEventHandler CreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CreateObject([In] ksObject2DNotify_CreateObjectEventHandler obj0);

//  public virtual extern event ksObject2DNotify_UpdateObjectEventHandler UpdateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_UpdateObject([In] ksObject2DNotify_UpdateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_UpdateObject([In] ksObject2DNotify_UpdateObjectEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginDestroyObjectEventHandler BeginDestroyObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDestroyObject(
//    [In] ksObject2DNotify_BeginDestroyObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDestroyObject(
//    [In] ksObject2DNotify_BeginDestroyObjectEventHandler obj0);

//  public virtual extern event ksObject2DNotify_DestroyObjectEventHandler DestroyObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DestroyObject([In] ksObject2DNotify_DestroyObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DestroyObject([In] ksObject2DNotify_DestroyObjectEventHandler obj0);

//  public virtual extern event ksObject2DNotify_BeginPropertyChangedEventHandler BeginPropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginPropertyChanged(
//    [In] ksObject2DNotify_BeginPropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginPropertyChanged(
//    [In] ksObject2DNotify_BeginPropertyChangedEventHandler obj0);

//  public virtual extern event ksObject2DNotify_PropertyChangedEventHandler PropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_PropertyChanged([In] ksObject2DNotify_PropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_PropertyChanged(
//    [In] ksObject2DNotify_PropertyChangedEventHandler obj0);
//}
