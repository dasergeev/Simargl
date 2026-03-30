//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksSpecificationNotify_Event
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComEventInterface(typeof (ksSpecificationNotify), typeof (ksSpecificationNotify_EventProvider))]
//[ComVisible(false)]
//public interface ksSpecificationNotify_Event
//{
//  event ksSpecificationNotify_TuningSpcStyleBeginChangeEventHandler TuningSpcStyleBeginChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_TuningSpcStyleBeginChange(
//    [In] ksSpecificationNotify_TuningSpcStyleBeginChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_TuningSpcStyleBeginChange(
//    [In] ksSpecificationNotify_TuningSpcStyleBeginChangeEventHandler obj0);

//  event ksSpecificationNotify_TuningSpcStyleChangeEventHandler TuningSpcStyleChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_TuningSpcStyleChange(
//    [In] ksSpecificationNotify_TuningSpcStyleChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_TuningSpcStyleChange(
//    [In] ksSpecificationNotify_TuningSpcStyleChangeEventHandler obj0);

//  event ksSpecificationNotify_ChangeCurrentSpcDescriptionEventHandler ChangeCurrentSpcDescription;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeCurrentSpcDescription(
//    [In] ksSpecificationNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeCurrentSpcDescription(
//    [In] ksSpecificationNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SpcDescriptionAdd(
//    [In] ksSpecificationNotify_SpcDescriptionAddEventHandler obj0);

//  event ksSpecificationNotify_SpcDescriptionAddEventHandler SpcDescriptionAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SpcDescriptionAdd(
//    [In] ksSpecificationNotify_SpcDescriptionAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SpcDescriptionRemove(
//    [In] ksSpecificationNotify_SpcDescriptionRemoveEventHandler obj0);

//  event ksSpecificationNotify_SpcDescriptionRemoveEventHandler SpcDescriptionRemove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SpcDescriptionRemove(
//    [In] ksSpecificationNotify_SpcDescriptionRemoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SpcDescriptionBeginEdit(
//    [In] ksSpecificationNotify_SpcDescriptionBeginEditEventHandler obj0);

//  event ksSpecificationNotify_SpcDescriptionBeginEditEventHandler SpcDescriptionBeginEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SpcDescriptionBeginEdit(
//    [In] ksSpecificationNotify_SpcDescriptionBeginEditEventHandler obj0);

//  event ksSpecificationNotify_SpcDescriptionEditEventHandler SpcDescriptionEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SpcDescriptionEdit(
//    [In] ksSpecificationNotify_SpcDescriptionEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SpcDescriptionEdit(
//    [In] ksSpecificationNotify_SpcDescriptionEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SynchronizationBegin(
//    [In] ksSpecificationNotify_SynchronizationBeginEventHandler obj0);

//  event ksSpecificationNotify_SynchronizationBeginEventHandler SynchronizationBegin;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SynchronizationBegin(
//    [In] ksSpecificationNotify_SynchronizationBeginEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Synchronization(
//    [In] ksSpecificationNotify_SynchronizationEventHandler obj0);

//  event ksSpecificationNotify_SynchronizationEventHandler Synchronization;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Synchronization(
//    [In] ksSpecificationNotify_SynchronizationEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCalcPositions(
//    [In] ksSpecificationNotify_BeginCalcPositionsEventHandler obj0);

//  event ksSpecificationNotify_BeginCalcPositionsEventHandler BeginCalcPositions;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCalcPositions(
//    [In] ksSpecificationNotify_BeginCalcPositionsEventHandler obj0);

//  event ksSpecificationNotify_CalcPositionsEventHandler CalcPositions;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CalcPositions(
//    [In] ksSpecificationNotify_CalcPositionsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CalcPositions(
//    [In] ksSpecificationNotify_CalcPositionsEventHandler obj0);

//  event ksSpecificationNotify_BeginCreateObjectEventHandler BeginCreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCreateObject(
//    [In] ksSpecificationNotify_BeginCreateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCreateObject(
//    [In] ksSpecificationNotify_BeginCreateObjectEventHandler obj0);
//}
