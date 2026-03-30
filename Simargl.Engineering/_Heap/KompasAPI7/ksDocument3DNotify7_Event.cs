//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksDocument3DNotify7_Event
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComVisible(false)]
//[ComEventInterface(typeof (ksDocument3DNotify7), typeof (ksDocument3DNotify7_EventProvider))]
//public interface ksDocument3DNotify7_Event
//{
//  event ksDocument3DNotify7_BeginRebuildEventHandler BeginRebuild;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginRebuild([In] ksDocument3DNotify7_BeginRebuildEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginRebuild([In] ksDocument3DNotify7_BeginRebuildEventHandler obj0);

//  event ksDocument3DNotify7_RebuildEventHandler Rebuild;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_Rebuild([In] ksDocument3DNotify7_RebuildEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_Rebuild([In] ksDocument3DNotify7_RebuildEventHandler obj0);

//  event ksDocument3DNotify7_BeginChoiceMaterialEventHandler BeginChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginChoiceMaterial(
//    [In] ksDocument3DNotify7_BeginChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginChoiceMaterial(
//    [In] ksDocument3DNotify7_BeginChoiceMaterialEventHandler obj0);

//  event ksDocument3DNotify7_ChoiceMaterialEventHandler ChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChoiceMaterial(
//    [In] ksDocument3DNotify7_ChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChoiceMaterial(
//    [In] ksDocument3DNotify7_ChoiceMaterialEventHandler obj0);

//  event ksDocument3DNotify7_BeginChoiceMarkingEventHandler BeginChoiceMarking;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginChoiceMarking(
//    [In] ksDocument3DNotify7_BeginChoiceMarkingEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginChoiceMarking(
//    [In] ksDocument3DNotify7_BeginChoiceMarkingEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChoiceMarking([In] ksDocument3DNotify7_ChoiceMarkingEventHandler obj0);

//  event ksDocument3DNotify7_ChoiceMarkingEventHandler ChoiceMarking;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChoiceMarking([In] ksDocument3DNotify7_ChoiceMarkingEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginSetPartFromFile(
//    [In] ksDocument3DNotify7_BeginSetPartFromFileEventHandler obj0);

//  event ksDocument3DNotify7_BeginSetPartFromFileEventHandler BeginSetPartFromFile;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginSetPartFromFile(
//    [In] ksDocument3DNotify7_BeginSetPartFromFileEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginCreatePartFromFile(
//    [In] ksDocument3DNotify7_BeginCreatePartFromFileEventHandler obj0);

//  event ksDocument3DNotify7_BeginCreatePartFromFileEventHandler BeginCreatePartFromFile;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginCreatePartFromFile(
//    [In] ksDocument3DNotify7_BeginCreatePartFromFileEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CreateEmbodiment(
//    [In] ksDocument3DNotify7_CreateEmbodimentEventHandler obj0);

//  event ksDocument3DNotify7_CreateEmbodimentEventHandler CreateEmbodiment;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CreateEmbodiment(
//    [In] ksDocument3DNotify7_CreateEmbodimentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DeleteEmbodiment(
//    [In] ksDocument3DNotify7_DeleteEmbodimentEventHandler obj0);

//  event ksDocument3DNotify7_DeleteEmbodimentEventHandler DeleteEmbodiment;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DeleteEmbodiment(
//    [In] ksDocument3DNotify7_DeleteEmbodimentEventHandler obj0);

//  event ksDocument3DNotify7_ChangeCurrentEmbodimentEventHandler ChangeCurrentEmbodiment;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeCurrentEmbodiment(
//    [In] ksDocument3DNotify7_ChangeCurrentEmbodimentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeCurrentEmbodiment(
//    [In] ksDocument3DNotify7_ChangeCurrentEmbodimentEventHandler obj0);

//  event ksDocument3DNotify7_BeginChoicePropertyEventHandler BeginChoiceProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginChoiceProperty(
//    [In] ksDocument3DNotify7_BeginChoicePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginChoiceProperty(
//    [In] ksDocument3DNotify7_BeginChoicePropertyEventHandler obj0);

//  event ksDocument3DNotify7_ChoicePropertyEventHandler ChoiceProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChoiceProperty(
//    [In] ksDocument3DNotify7_ChoicePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChoiceProperty(
//    [In] ksDocument3DNotify7_ChoicePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginRollbackFeatures(
//    [In] ksDocument3DNotify7_BeginRollbackFeaturesEventHandler obj0);

//  event ksDocument3DNotify7_BeginRollbackFeaturesEventHandler BeginRollbackFeatures;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginRollbackFeatures(
//    [In] ksDocument3DNotify7_BeginRollbackFeaturesEventHandler obj0);

//  event ksDocument3DNotify7_RollbackFeaturesEventHandler RollbackFeatures;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_RollbackFeatures(
//    [In] ksDocument3DNotify7_RollbackFeaturesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_RollbackFeatures(
//    [In] ksDocument3DNotify7_RollbackFeaturesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BedinLoadCombinationChange(
//    [In] ksDocument3DNotify7_BedinLoadCombinationChangeEventHandler obj0);

//  event ksDocument3DNotify7_BedinLoadCombinationChangeEventHandler BedinLoadCombinationChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BedinLoadCombinationChange(
//    [In] ksDocument3DNotify7_BedinLoadCombinationChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_LoadCombinationChange(
//    [In] ksDocument3DNotify7_LoadCombinationChangeEventHandler obj0);

//  event ksDocument3DNotify7_LoadCombinationChangeEventHandler LoadCombinationChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_LoadCombinationChange(
//    [In] ksDocument3DNotify7_LoadCombinationChangeEventHandler obj0);

//  event ksDocument3DNotify7_BeginDeleteMaterialEventHandler BeginDeleteMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDeleteMaterial(
//    [In] ksDocument3DNotify7_BeginDeleteMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDeleteMaterial(
//    [In] ksDocument3DNotify7_BeginDeleteMaterialEventHandler obj0);

//  event ksDocument3DNotify7_DeleteMaterialEventHandler DeleteMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DeleteMaterial(
//    [In] ksDocument3DNotify7_DeleteMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DeleteMaterial(
//    [In] ksDocument3DNotify7_DeleteMaterialEventHandler obj0);

//  event ksDocument3DNotify7_BeginDeletePropertyEventHandler BeginDeleteProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_BeginDeleteProperty(
//    [In] ksDocument3DNotify7_BeginDeletePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_BeginDeleteProperty(
//    [In] ksDocument3DNotify7_BeginDeletePropertyEventHandler obj0);

//  event ksDocument3DNotify7_DeletePropertyEventHandler DeleteProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DeleteProperty(
//    [In] ksDocument3DNotify7_DeletePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DeleteProperty(
//    [In] ksDocument3DNotify7_DeletePropertyEventHandler obj0);
//}
