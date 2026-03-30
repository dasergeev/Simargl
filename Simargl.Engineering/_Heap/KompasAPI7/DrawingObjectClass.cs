//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.DrawingObjectClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("3310AC4A-DC93-4AB5-B2CD-5391E6CB370A")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComSourceInterfaces("KompasAPI7.ksDrawingObjectNotify\0\0")]
//[ComImport]
//public class DrawingObjectClass : 
//  IDrawingObject,
//  DrawingObject,
//  ksDrawingObjectNotify_Event,
//  IKompasAPIObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern DrawingObjectClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3000)]
//  public virtual extern DrawingObjectTypeEnum DrawingObjectType { [DispId(3000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3001)]
//  public virtual extern int LayerNumber { [DispId(3001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(3001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(3002)]
//  public virtual extern bool Temp { [DispId(3002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3003)]
//  public virtual extern bool Valid { [DispId(3003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Update();

//  [DispId(3005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Delete();

//  [DispId(3006)]
//  public virtual extern ksDrawingObjectParamTypeEnum DrawingObjectParamType { [DispId(3006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(3006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeActive(
//    [In] ksDrawingObjectNotify_ChangeActiveEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_ChangeActiveEventHandler ChangeActive;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeActive(
//    [In] ksDrawingObjectNotify_ChangeActiveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDelete([In] ksDrawingObjectNotify_BeginDeleteEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginDeleteEventHandler BeginDelete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDelete([In] ksDrawingObjectNotify_BeginDeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Delete([In] ksDrawingObjectNotify_DeleteEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_DeleteEventHandler ksDrawingObjectNotify_Event_Delete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Delete([In] ksDrawingObjectNotify_DeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginMove([In] ksDrawingObjectNotify_BeginMoveEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginMoveEventHandler BeginMove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginMove([In] ksDrawingObjectNotify_BeginMoveEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_MoveEventHandler Move;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Move([In] ksDrawingObjectNotify_MoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Move([In] ksDrawingObjectNotify_MoveEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginRotateEventHandler BeginRotate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginRotate([In] ksDrawingObjectNotify_BeginRotateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginRotate([In] ksDrawingObjectNotify_BeginRotateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Rotate([In] ksDrawingObjectNotify_RotateEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_RotateEventHandler Rotate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Rotate([In] ksDrawingObjectNotify_RotateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginScale([In] ksDrawingObjectNotify_BeginScaleEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginScaleEventHandler BeginScale;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginScale([In] ksDrawingObjectNotify_BeginScaleEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Scale([In] ksDrawingObjectNotify_ScaleEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_ScaleEventHandler Scale;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Scale([In] ksDrawingObjectNotify_ScaleEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginTransform(
//    [In] ksDrawingObjectNotify_BeginTransformEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginTransformEventHandler BeginTransform;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginTransform(
//    [In] ksDrawingObjectNotify_BeginTransformEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_TransformEventHandler Transform;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Transform([In] ksDrawingObjectNotify_TransformEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Transform([In] ksDrawingObjectNotify_TransformEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginCopyEventHandler BeginCopy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCopy([In] ksDrawingObjectNotify_BeginCopyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCopy([In] ksDrawingObjectNotify_BeginCopyEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_CopyEventHandler Copy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Copy([In] ksDrawingObjectNotify_CopyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Copy([In] ksDrawingObjectNotify_CopyEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginSymmetryEventHandler BeginSymmetry;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginSymmetry(
//    [In] ksDrawingObjectNotify_BeginSymmetryEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginSymmetry(
//    [In] ksDrawingObjectNotify_BeginSymmetryEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Symmetry([In] ksDrawingObjectNotify_SymmetryEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_SymmetryEventHandler Symmetry;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Symmetry([In] ksDrawingObjectNotify_SymmetryEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginProcess(
//    [In] ksDrawingObjectNotify_BeginProcessEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginProcess(
//    [In] ksDrawingObjectNotify_BeginProcessEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndProcess([In] ksDrawingObjectNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndProcess([In] ksDrawingObjectNotify_EndProcessEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_CreateObjectEventHandler CreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CreateObject(
//    [In] ksDrawingObjectNotify_CreateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CreateObject(
//    [In] ksDrawingObjectNotify_CreateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_UpdateObject(
//    [In] ksDrawingObjectNotify_UpdateObjectEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_UpdateObjectEventHandler UpdateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_UpdateObject(
//    [In] ksDrawingObjectNotify_UpdateObjectEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginDestroyObjectEventHandler BeginDestroyObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDestroyObject(
//    [In] ksDrawingObjectNotify_BeginDestroyObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDestroyObject(
//    [In] ksDrawingObjectNotify_BeginDestroyObjectEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_DestroyObjectEventHandler DestroyObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DestroyObject(
//    [In] ksDrawingObjectNotify_DestroyObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DestroyObject(
//    [In] ksDrawingObjectNotify_DestroyObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginPropertyChanged(
//    [In] ksDrawingObjectNotify_BeginPropertyChangedEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_BeginPropertyChangedEventHandler BeginPropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginPropertyChanged(
//    [In] ksDrawingObjectNotify_BeginPropertyChangedEventHandler obj0);

//  public virtual extern event ksDrawingObjectNotify_PropertyChangedEventHandler PropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_PropertyChanged(
//    [In] ksDrawingObjectNotify_PropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_PropertyChanged(
//    [In] ksDrawingObjectNotify_PropertyChangedEventHandler obj0);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
