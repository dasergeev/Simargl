//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksSpecificationDescriptionNotify_Event
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ComEventInterface(typeof (ksSpecificationDescriptionNotify), typeof (ksSpecificationDescriptionNotify_EventProvider))]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComVisible(false)]
//public interface ksSpecificationDescriptionNotify_Event
//{
//  event ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler TuningSpcStyleBeginChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_TuningSpcStyleBeginChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_TuningSpcStyleBeginChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler obj0);

//  event ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler TuningSpcStyleChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_TuningSpcStyleChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_TuningSpcStyleChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler obj0);

//  event ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler ChangeCurrentSpcDescription;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeCurrentSpcDescription(
//    [In] ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeCurrentSpcDescription(
//    [In] ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SpcDescriptionAdd(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler obj0);

//  event ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler SpcDescriptionAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SpcDescriptionAdd(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SpcDescriptionRemove(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler obj0);

//  event ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler SpcDescriptionRemove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SpcDescriptionRemove(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SpcDescriptionBeginEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler obj0);

//  event ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler SpcDescriptionBeginEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SpcDescriptionBeginEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler obj0);

//  event ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler SpcDescriptionEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SpcDescriptionEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SpcDescriptionEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SynchronizationBegin(
//    [In] ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler obj0);

//  event ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler SynchronizationBegin;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SynchronizationBegin(
//    [In] ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Synchronization(
//    [In] ksSpecificationDescriptionNotify_SynchronizationEventHandler obj0);

//  event ksSpecificationDescriptionNotify_SynchronizationEventHandler Synchronization;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Synchronization(
//    [In] ksSpecificationDescriptionNotify_SynchronizationEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCalcPositions(
//    [In] ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler obj0);

//  event ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler BeginCalcPositions;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCalcPositions(
//    [In] ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler obj0);

//  event ksSpecificationDescriptionNotify_CalcPositionsEventHandler CalcPositions;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CalcPositions(
//    [In] ksSpecificationDescriptionNotify_CalcPositionsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CalcPositions(
//    [In] ksSpecificationDescriptionNotify_CalcPositionsEventHandler obj0);

//  event ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler BeginCreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCreateObject(
//    [In] ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCreateObject(
//    [In] ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler obj0);
//}
