//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksDocument3DNotify_Event
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComEventInterface(typeof (ksDocument3DNotify), typeof (ksDocument3DNotify_EventProvider))]
//[ComVisible(false)]
//public interface ksDocument3DNotify_Event
//{
//  event ksDocument3DNotify_BeginRebuildEventHandler BeginRebuild;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginRebuild([In] ksDocument3DNotify_BeginRebuildEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginRebuild([In] ksDocument3DNotify_BeginRebuildEventHandler obj0);

//  event ksDocument3DNotify_RebuildEventHandler Rebuild;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Rebuild([In] ksDocument3DNotify_RebuildEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Rebuild([In] ksDocument3DNotify_RebuildEventHandler obj0);

//  event ksDocument3DNotify_BeginChoiceMaterialEventHandler BeginChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginChoiceMaterial(
//    [In] ksDocument3DNotify_BeginChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginChoiceMaterial(
//    [In] ksDocument3DNotify_BeginChoiceMaterialEventHandler obj0);

//  event ksDocument3DNotify_ChoiceMaterialEventHandler ChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChoiceMaterial([In] ksDocument3DNotify_ChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChoiceMaterial([In] ksDocument3DNotify_ChoiceMaterialEventHandler obj0);

//  event ksDocument3DNotify_BeginChoiceMarkingEventHandler BeginChoiceMarking;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginChoiceMarking(
//    [In] ksDocument3DNotify_BeginChoiceMarkingEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginChoiceMarking(
//    [In] ksDocument3DNotify_BeginChoiceMarkingEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChoiceMarking([In] ksDocument3DNotify_ChoiceMarkingEventHandler obj0);

//  event ksDocument3DNotify_ChoiceMarkingEventHandler ChoiceMarking;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChoiceMarking([In] ksDocument3DNotify_ChoiceMarkingEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginSetPartFromFile(
//    [In] ksDocument3DNotify_BeginSetPartFromFileEventHandler obj0);

//  event ksDocument3DNotify_BeginSetPartFromFileEventHandler BeginSetPartFromFile;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginSetPartFromFile(
//    [In] ksDocument3DNotify_BeginSetPartFromFileEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCreatePartFromFile(
//    [In] ksDocument3DNotify_BeginCreatePartFromFileEventHandler obj0);

//  event ksDocument3DNotify_BeginCreatePartFromFileEventHandler BeginCreatePartFromFile;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCreatePartFromFile(
//    [In] ksDocument3DNotify_BeginCreatePartFromFileEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CreateEmbodiment(
//    [In] ksDocument3DNotify_CreateEmbodimentEventHandler obj0);

//  event ksDocument3DNotify_CreateEmbodimentEventHandler CreateEmbodiment;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CreateEmbodiment(
//    [In] ksDocument3DNotify_CreateEmbodimentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DeleteEmbodiment(
//    [In] ksDocument3DNotify_DeleteEmbodimentEventHandler obj0);

//  event ksDocument3DNotify_DeleteEmbodimentEventHandler DeleteEmbodiment;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DeleteEmbodiment(
//    [In] ksDocument3DNotify_DeleteEmbodimentEventHandler obj0);

//  event ksDocument3DNotify_ChangeCurrentEmbodimentEventHandler ChangeCurrentEmbodiment;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeCurrentEmbodiment(
//    [In] ksDocument3DNotify_ChangeCurrentEmbodimentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeCurrentEmbodiment(
//    [In] ksDocument3DNotify_ChangeCurrentEmbodimentEventHandler obj0);

//  event ksDocument3DNotify_BeginChoicePropertyEventHandler BeginChoiceProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginChoiceProperty(
//    [In] ksDocument3DNotify_BeginChoicePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginChoiceProperty(
//    [In] ksDocument3DNotify_BeginChoicePropertyEventHandler obj0);

//  event ksDocument3DNotify_ChoicePropertyEventHandler ChoiceProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChoiceProperty([In] ksDocument3DNotify_ChoicePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChoiceProperty([In] ksDocument3DNotify_ChoicePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginRollbackFeatures(
//    [In] ksDocument3DNotify_BeginRollbackFeaturesEventHandler obj0);

//  event ksDocument3DNotify_BeginRollbackFeaturesEventHandler BeginRollbackFeatures;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginRollbackFeatures(
//    [In] ksDocument3DNotify_BeginRollbackFeaturesEventHandler obj0);

//  event ksDocument3DNotify_RollbackFeaturesEventHandler RollbackFeatures;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_RollbackFeatures(
//    [In] ksDocument3DNotify_RollbackFeaturesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_RollbackFeatures(
//    [In] ksDocument3DNotify_RollbackFeaturesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BedinLoadCombinationChange(
//    [In] ksDocument3DNotify_BedinLoadCombinationChangeEventHandler obj0);

//  event ksDocument3DNotify_BedinLoadCombinationChangeEventHandler BedinLoadCombinationChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BedinLoadCombinationChange(
//    [In] ksDocument3DNotify_BedinLoadCombinationChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_LoadCombinationChange(
//    [In] ksDocument3DNotify_LoadCombinationChangeEventHandler obj0);

//  event ksDocument3DNotify_LoadCombinationChangeEventHandler LoadCombinationChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_LoadCombinationChange(
//    [In] ksDocument3DNotify_LoadCombinationChangeEventHandler obj0);

//  event ksDocument3DNotify_BeginDeleteMaterialEventHandler BeginDeleteMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDeleteMaterial(
//    [In] ksDocument3DNotify_BeginDeleteMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDeleteMaterial(
//    [In] ksDocument3DNotify_BeginDeleteMaterialEventHandler obj0);

//  event ksDocument3DNotify_DeleteMaterialEventHandler DeleteMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DeleteMaterial([In] ksDocument3DNotify_DeleteMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DeleteMaterial([In] ksDocument3DNotify_DeleteMaterialEventHandler obj0);

//  event ksDocument3DNotify_BeginDeletePropertyEventHandler BeginDeleteProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDeleteProperty(
//    [In] ksDocument3DNotify_BeginDeletePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDeleteProperty(
//    [In] ksDocument3DNotify_BeginDeletePropertyEventHandler obj0);

//  event ksDocument3DNotify_DeletePropertyEventHandler DeleteProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DeleteProperty([In] ksDocument3DNotify_DeletePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DeleteProperty([In] ksDocument3DNotify_DeletePropertyEventHandler obj0);
//}
