//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksObject3DNotify_Event
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ComEventInterface(typeof (ksObject3DNotify), typeof (ksObject3DNotify_EventProvider))]
//[ComVisible(false)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public interface ksObject3DNotify_Event
//{
//  event ksObject3DNotify_BeginDeleteEventHandler BeginDelete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDelete([In] ksObject3DNotify_BeginDeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDelete([In] ksObject3DNotify_BeginDeleteEventHandler obj0);

//  event ksObject3DNotify_DeleteEventHandler Delete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Delete([In] ksObject3DNotify_DeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Delete([In] ksObject3DNotify_DeleteEventHandler obj0);

//  event ksObject3DNotify_excludedEventHandler excluded;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_excluded([In] ksObject3DNotify_excludedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_excluded([In] ksObject3DNotify_excludedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_hidden([In] ksObject3DNotify_hiddenEventHandler obj0);

//  event ksObject3DNotify_hiddenEventHandler hidden;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_hidden([In] ksObject3DNotify_hiddenEventHandler obj0);

//  event ksObject3DNotify_BeginPropertyChangedEventHandler BeginPropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginPropertyChanged(
//    [In] ksObject3DNotify_BeginPropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginPropertyChanged(
//    [In] ksObject3DNotify_BeginPropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_PropertyChanged([In] ksObject3DNotify_PropertyChangedEventHandler obj0);

//  event ksObject3DNotify_PropertyChangedEventHandler PropertyChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_PropertyChanged([In] ksObject3DNotify_PropertyChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginPlacementChanged(
//    [In] ksObject3DNotify_BeginPlacementChangedEventHandler obj0);

//  event ksObject3DNotify_BeginPlacementChangedEventHandler BeginPlacementChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginPlacementChanged(
//    [In] ksObject3DNotify_BeginPlacementChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_PlacementChanged([In] ksObject3DNotify_PlacementChangedEventHandler obj0);

//  event ksObject3DNotify_PlacementChangedEventHandler PlacementChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_PlacementChanged([In] ksObject3DNotify_PlacementChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginProcess([In] ksObject3DNotify_BeginProcessEventHandler obj0);

//  event ksObject3DNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginProcess([In] ksObject3DNotify_BeginProcessEventHandler obj0);

//  event ksObject3DNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EndProcess([In] ksObject3DNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EndProcess([In] ksObject3DNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CreateObject([In] ksObject3DNotify_CreateObjectEventHandler obj0);

//  event ksObject3DNotify_CreateObjectEventHandler CreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CreateObject([In] ksObject3DNotify_CreateObjectEventHandler obj0);

//  event ksObject3DNotify_UpdateObjectEventHandler UpdateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_UpdateObject([In] ksObject3DNotify_UpdateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_UpdateObject([In] ksObject3DNotify_UpdateObjectEventHandler obj0);

//  event ksObject3DNotify_BeginLoadStateChangeEventHandler BeginLoadStateChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginLoadStateChange(
//    [In] ksObject3DNotify_BeginLoadStateChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginLoadStateChange(
//    [In] ksObject3DNotify_BeginLoadStateChangeEventHandler obj0);

//  event ksObject3DNotify_LoadStateChangeEventHandler LoadStateChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_LoadStateChange([In] ksObject3DNotify_LoadStateChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_LoadStateChange([In] ksObject3DNotify_LoadStateChangeEventHandler obj0);
//}
