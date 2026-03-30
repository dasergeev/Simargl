//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ModelObjectsClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Collections;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;
//using System.Runtime.InteropServices.CustomMarshalers;

//#nullable disable
//namespace KompasAPI7;

//[Guid("B0223EED-4659-4EAC-BDA2-20B0FDD4E7B7")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComSourceInterfaces("KompasAPI7.ksModelObjectNotify\0\0")]
//[ComImport]
//public class ModelObjectsClass : 
//  IModelObjects,
//  ModelObjects,
//  ksModelObjectNotify_Event,
//  IKompasAPIObject,
//  IKompasCollection,
//  IEnumerable
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern ModelObjectsClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(-4)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (EnumeratorToEnumVariantMarshaler))]
//  public virtual extern IEnumerator GetEnumerator();

//  [DispId(2000)]
//  public virtual extern int Count { [DispId(2000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(0)]
//  public virtual extern IModelObject this[[MarshalAs(UnmanagedType.Struct), In] object Index] { [DispId(0), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDelete([In] ksModelObjectNotify_BeginDeleteEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_BeginDeleteEventHandler BeginDelete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDelete([In] ksModelObjectNotify_BeginDeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Delete([In] ksModelObjectNotify_DeleteEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_DeleteEventHandler Delete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Delete([In] ksModelObjectNotify_DeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Excluded([In] ksModelObjectNotify_ExcludedEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_ExcludedEventHandler Excluded;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Excluded([In] ksModelObjectNotify_ExcludedEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_HiddenEventHandler Hidden;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Hidden([In] ksModelObjectNotify_HiddenEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Hidden([In] ksModelObjectNotify_HiddenEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginPropertyChanged(
//    [In] ksModelObjectNotify_BeginPropertyChangedEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_BeginPropertyChangedEventHandler BeginPropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginPropertyChanged(
//    [In] ksModelObjectNotify_BeginPropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_PropertyChanged(
//    [In] ksModelObjectNotify_PropertyChangedEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_PropertyChangedEventHandler PropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_PropertyChanged(
//    [In] ksModelObjectNotify_PropertyChangedEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_BeginPlacementChangedEventHandler BeginPlacementChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginPlacementChanged(
//    [In] ksModelObjectNotify_BeginPlacementChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginPlacementChanged(
//    [In] ksModelObjectNotify_BeginPlacementChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_PlacementChanged(
//    [In] ksModelObjectNotify_PlacementChangedEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_PlacementChangedEventHandler PlacementChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_PlacementChanged(
//    [In] ksModelObjectNotify_PlacementChangedEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginProcess([In] ksModelObjectNotify_BeginProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginProcess([In] ksModelObjectNotify_BeginProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndProcess([In] ksModelObjectNotify_EndProcessEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndProcess([In] ksModelObjectNotify_EndProcessEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_CreateObjectEventHandler CreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CreateObject([In] ksModelObjectNotify_CreateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CreateObject([In] ksModelObjectNotify_CreateObjectEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_UpdateObjectEventHandler UpdateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_UpdateObject([In] ksModelObjectNotify_UpdateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_UpdateObject([In] ksModelObjectNotify_UpdateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginLoadStateChange(
//    [In] ksModelObjectNotify_BeginLoadStateChangeEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_BeginLoadStateChangeEventHandler BeginLoadStateChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginLoadStateChange(
//    [In] ksModelObjectNotify_BeginLoadStateChangeEventHandler obj0);

//  public virtual extern event ksModelObjectNotify_LoadStateChangeEventHandler LoadStateChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_LoadStateChange(
//    [In] ksModelObjectNotify_LoadStateChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_LoadStateChange(
//    [In] ksModelObjectNotify_LoadStateChangeEventHandler obj0);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern IKompasAPIObject IKompasCollection_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasCollection_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasCollection_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasCollection_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (EnumeratorToEnumVariantMarshaler))]
//  public virtual extern IEnumerator IKompasCollection_GetEnumerator();

//  public virtual extern int IKompasCollection_Count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
