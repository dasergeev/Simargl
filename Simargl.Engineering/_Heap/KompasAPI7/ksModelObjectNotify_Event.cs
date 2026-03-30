//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksModelObjectNotify_Event
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComVisible(false)]
//[ComEventInterface(typeof (ksModelObjectNotify), typeof (ksModelObjectNotify_EventProvider))]
//public interface ksModelObjectNotify_Event
//{
//  event ksModelObjectNotify_BeginDeleteEventHandler BeginDelete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDelete([In] ksModelObjectNotify_BeginDeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDelete([In] ksModelObjectNotify_BeginDeleteEventHandler obj0);

//  event ksModelObjectNotify_DeleteEventHandler Delete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Delete([In] ksModelObjectNotify_DeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Delete([In] ksModelObjectNotify_DeleteEventHandler obj0);

//  event ksModelObjectNotify_ExcludedEventHandler Excluded;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Excluded([In] ksModelObjectNotify_ExcludedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Excluded([In] ksModelObjectNotify_ExcludedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Hidden([In] ksModelObjectNotify_HiddenEventHandler obj0);

//  event ksModelObjectNotify_HiddenEventHandler Hidden;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Hidden([In] ksModelObjectNotify_HiddenEventHandler obj0);

//  event ksModelObjectNotify_BeginPropertyChangedEventHandler BeginPropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginPropertyChanged(
//    [In] ksModelObjectNotify_BeginPropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginPropertyChanged(
//    [In] ksModelObjectNotify_BeginPropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_PropertyChanged(
//    [In] ksModelObjectNotify_PropertyChangedEventHandler obj0);

//  event ksModelObjectNotify_PropertyChangedEventHandler PropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_PropertyChanged(
//    [In] ksModelObjectNotify_PropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginPlacementChanged(
//    [In] ksModelObjectNotify_BeginPlacementChangedEventHandler obj0);

//  event ksModelObjectNotify_BeginPlacementChangedEventHandler BeginPlacementChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginPlacementChanged(
//    [In] ksModelObjectNotify_BeginPlacementChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_PlacementChanged(
//    [In] ksModelObjectNotify_PlacementChangedEventHandler obj0);

//  event ksModelObjectNotify_PlacementChangedEventHandler PlacementChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_PlacementChanged(
//    [In] ksModelObjectNotify_PlacementChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginProcess([In] ksModelObjectNotify_BeginProcessEventHandler obj0);

//  event ksModelObjectNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginProcess([In] ksModelObjectNotify_BeginProcessEventHandler obj0);

//  event ksModelObjectNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EndProcess([In] ksModelObjectNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EndProcess([In] ksModelObjectNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CreateObject([In] ksModelObjectNotify_CreateObjectEventHandler obj0);

//  event ksModelObjectNotify_CreateObjectEventHandler CreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CreateObject([In] ksModelObjectNotify_CreateObjectEventHandler obj0);

//  event ksModelObjectNotify_UpdateObjectEventHandler UpdateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_UpdateObject([In] ksModelObjectNotify_UpdateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_UpdateObject([In] ksModelObjectNotify_UpdateObjectEventHandler obj0);

//  event ksModelObjectNotify_BeginLoadStateChangeEventHandler BeginLoadStateChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginLoadStateChange(
//    [In] ksModelObjectNotify_BeginLoadStateChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginLoadStateChange(
//    [In] ksModelObjectNotify_BeginLoadStateChangeEventHandler obj0);

//  event ksModelObjectNotify_LoadStateChangeEventHandler LoadStateChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_LoadStateChange(
//    [In] ksModelObjectNotify_LoadStateChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_LoadStateChange(
//    [In] ksModelObjectNotify_LoadStateChangeEventHandler obj0);
//}
