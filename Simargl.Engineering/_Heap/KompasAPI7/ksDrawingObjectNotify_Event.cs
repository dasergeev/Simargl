//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksDrawingObjectNotify_Event
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ComEventInterface(typeof (ksDrawingObjectNotify), typeof (ksDrawingObjectNotify_EventProvider))]
//[ComVisible(false)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public interface ksDrawingObjectNotify_Event
//{
//  event ksDrawingObjectNotify_ChangeActiveEventHandler ChangeActive;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeActive(
//    [In] ksDrawingObjectNotify_ChangeActiveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeActive(
//    [In] ksDrawingObjectNotify_ChangeActiveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDelete([In] ksDrawingObjectNotify_BeginDeleteEventHandler obj0);

//  event ksDrawingObjectNotify_BeginDeleteEventHandler BeginDelete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDelete([In] ksDrawingObjectNotify_BeginDeleteEventHandler obj0);

//  event ksDrawingObjectNotify_DeleteEventHandler Delete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Delete([In] ksDrawingObjectNotify_DeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Delete([In] ksDrawingObjectNotify_DeleteEventHandler obj0);

//  event ksDrawingObjectNotify_BeginMoveEventHandler BeginMove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginMove([In] ksDrawingObjectNotify_BeginMoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginMove([In] ksDrawingObjectNotify_BeginMoveEventHandler obj0);

//  event ksDrawingObjectNotify_MoveEventHandler Move;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Move([In] ksDrawingObjectNotify_MoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Move([In] ksDrawingObjectNotify_MoveEventHandler obj0);

//  event ksDrawingObjectNotify_BeginRotateEventHandler BeginRotate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginRotate([In] ksDrawingObjectNotify_BeginRotateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginRotate([In] ksDrawingObjectNotify_BeginRotateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Rotate([In] ksDrawingObjectNotify_RotateEventHandler obj0);

//  event ksDrawingObjectNotify_RotateEventHandler Rotate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Rotate([In] ksDrawingObjectNotify_RotateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginScale([In] ksDrawingObjectNotify_BeginScaleEventHandler obj0);

//  event ksDrawingObjectNotify_BeginScaleEventHandler BeginScale;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginScale([In] ksDrawingObjectNotify_BeginScaleEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Scale([In] ksDrawingObjectNotify_ScaleEventHandler obj0);

//  event ksDrawingObjectNotify_ScaleEventHandler Scale;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Scale([In] ksDrawingObjectNotify_ScaleEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginTransform(
//    [In] ksDrawingObjectNotify_BeginTransformEventHandler obj0);

//  event ksDrawingObjectNotify_BeginTransformEventHandler BeginTransform;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginTransform(
//    [In] ksDrawingObjectNotify_BeginTransformEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Transform([In] ksDrawingObjectNotify_TransformEventHandler obj0);

//  event ksDrawingObjectNotify_TransformEventHandler Transform;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Transform([In] ksDrawingObjectNotify_TransformEventHandler obj0);

//  event ksDrawingObjectNotify_BeginCopyEventHandler BeginCopy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCopy([In] ksDrawingObjectNotify_BeginCopyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCopy([In] ksDrawingObjectNotify_BeginCopyEventHandler obj0);

//  event ksDrawingObjectNotify_CopyEventHandler Copy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Copy([In] ksDrawingObjectNotify_CopyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Copy([In] ksDrawingObjectNotify_CopyEventHandler obj0);

//  event ksDrawingObjectNotify_BeginSymmetryEventHandler BeginSymmetry;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginSymmetry(
//    [In] ksDrawingObjectNotify_BeginSymmetryEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginSymmetry(
//    [In] ksDrawingObjectNotify_BeginSymmetryEventHandler obj0);

//  event ksDrawingObjectNotify_SymmetryEventHandler Symmetry;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Symmetry([In] ksDrawingObjectNotify_SymmetryEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Symmetry([In] ksDrawingObjectNotify_SymmetryEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginProcess(
//    [In] ksDrawingObjectNotify_BeginProcessEventHandler obj0);

//  event ksDrawingObjectNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginProcess(
//    [In] ksDrawingObjectNotify_BeginProcessEventHandler obj0);

//  event ksDrawingObjectNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EndProcess([In] ksDrawingObjectNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EndProcess([In] ksDrawingObjectNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CreateObject(
//    [In] ksDrawingObjectNotify_CreateObjectEventHandler obj0);

//  event ksDrawingObjectNotify_CreateObjectEventHandler CreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CreateObject(
//    [In] ksDrawingObjectNotify_CreateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_UpdateObject(
//    [In] ksDrawingObjectNotify_UpdateObjectEventHandler obj0);

//  event ksDrawingObjectNotify_UpdateObjectEventHandler UpdateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_UpdateObject(
//    [In] ksDrawingObjectNotify_UpdateObjectEventHandler obj0);

//  event ksDrawingObjectNotify_BeginDestroyObjectEventHandler BeginDestroyObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDestroyObject(
//    [In] ksDrawingObjectNotify_BeginDestroyObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDestroyObject(
//    [In] ksDrawingObjectNotify_BeginDestroyObjectEventHandler obj0);

//  event ksDrawingObjectNotify_DestroyObjectEventHandler DestroyObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DestroyObject(
//    [In] ksDrawingObjectNotify_DestroyObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DestroyObject(
//    [In] ksDrawingObjectNotify_DestroyObjectEventHandler obj0);

//  event ksDrawingObjectNotify_BeginPropertyChangedEventHandler BeginPropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginPropertyChanged(
//    [In] ksDrawingObjectNotify_BeginPropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginPropertyChanged(
//    [In] ksDrawingObjectNotify_BeginPropertyChangedEventHandler obj0);

//  event ksDrawingObjectNotify_PropertyChangedEventHandler PropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_PropertyChanged(
//    [In] ksDrawingObjectNotify_PropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_PropertyChanged(
//    [In] ksDrawingObjectNotify_PropertyChangedEventHandler obj0);
//}
