//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.KompasDocument3DClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("9237AAFE-992B-4521-BF68-3A0C6D7F7769")]
//[ComSourceInterfaces("KompasAPI7.ksDocument3DNotify7\0\0")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class KompasDocument3DClass : 
//  IKompasDocument3D,
//  KompasDocument3D,
//  ksDocument3DNotify7_Event,
//  IKompasAPIObject,
//  IKompasDocument
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern KompasDocument3DClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3000)]
//  public virtual extern DocumentTypeEnum DocumentType { [DispId(3000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3001)]
//  public virtual extern DocumentFrames DocumentFrames { [DispId(3001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(3002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Close([In] DocumentCloseOptions closeOptions);

//  [DispId(3003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void Save();

//  [DispId(3004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void SaveAs([MarshalAs(UnmanagedType.BStr), In] string PathName);

//  [DispId(3005)]
//  public virtual extern string Name { [DispId(3005), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(3006)]
//  public virtual extern string PathName { [DispId(3006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(3007)]
//  public virtual extern string Path { [DispId(3007), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(3008)]
//  public virtual extern bool Visible { [DispId(3008), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3009)]
//  public virtual extern bool Active { [DispId(3009), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(3009), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(3010)]
//  public virtual extern bool ReadOnly { [DispId(3010), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(3010), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(3011)]
//  public virtual extern bool Changed { [DispId(3011), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3012)]
//  public virtual extern IDocumentSettings DocumentSettings { [DispId(3012), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(3013)]
//  public virtual extern SpecificationDescriptions SpecificationDescriptions { [DispId(3013), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(3014)]
//  public virtual extern LayoutSheets LayoutSheets { [DispId(3014), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(3015)]
//  public virtual extern UserDataStoragesMng UserDataStoragesMng { [DispId(3015), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5002)]
//  public virtual extern Part7 TopPart { [DispId(5002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5003)]
//  public virtual extern bool HideAllPlanes { [DispId(5003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5004)]
//  public virtual extern bool HideAllAxis { [DispId(5004), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5004), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5005)]
//  public virtual extern bool HideAllSketches { [DispId(5005), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5005), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5006)]
//  public virtual extern bool HideAllPlaces { [DispId(5006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5007)]
//  public virtual extern bool HideAllSurfaces { [DispId(5007), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5007), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5008)]
//  public virtual extern bool HideAllThreads { [DispId(5008), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5008), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5009)]
//  public virtual extern bool HideAllCurves { [DispId(5009), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5009), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5010)]
//  public virtual extern bool HideAllControlPoints { [DispId(5010), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5010), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5011)]
//  public virtual extern bool Perspective { [DispId(5011), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5011), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5012)]
//  public virtual extern int DrawMode { [DispId(5012), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5012), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5013)]
//  public virtual extern bool ShadedWireframe { [DispId(5013), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5013), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5014)]
//  public virtual extern bool CreateObjectsInCurrentLocalCS { [DispId(5014), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5014), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5015)]
//  public virtual extern bool HideAllDimensions { [DispId(5015), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5015), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5016)]
//  public virtual extern bool HideAllDesignations { [DispId(5016), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5016), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5017)]
//  public virtual extern bool HideAllAuxiliaryGeom { [DispId(5017), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5017), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5018)]
//  public virtual extern bool HideInComponentsMode { [DispId(5018), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5018), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5019)]
//  public virtual extern SelectionManager SelectionManager { [DispId(5019), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5020)]
//  public virtual extern ChooseManager ChooseManager { [DispId(5020), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5021)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern _Attribute CreateAttrEx(
//    [In] double AttrID,
//    [MarshalAs(UnmanagedType.BStr), In] string Libname,
//    [MarshalAs(UnmanagedType.Struct), In] object Objects,
//    [MarshalAs(UnmanagedType.Interface), In] Part7 SourcePart);

//  [DispId(5022)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  public virtual extern object get_AttributesEx(
//    [In] int Key1,
//    [In] int Key2,
//    [In] int Key3,
//    [In] int Key4,
//    [In] double Numb,
//    [MarshalAs(UnmanagedType.Struct), In] object Objects,
//    [MarshalAs(UnmanagedType.Interface), In] Part7 SourcePart);

//  [DispId(5023)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool RebuildDocument();

//  [DispId(5024)]
//  public virtual extern bool EnableUndo { [DispId(5024), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5024), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5025)]
//  public virtual extern bool UndoContainer { [DispId(5025), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5025), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5026)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool DeleteHistory();

//  [DispId(5027)]
//  public virtual extern TechnicalDemand3D TechnicalDemand3D { [DispId(5027), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5028)]
//  public virtual extern bool ToleranceMode { [DispId(5028), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5028), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern event ksDocument3DNotify7_BeginRebuildEventHandler BeginRebuild;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginRebuild([In] ksDocument3DNotify7_BeginRebuildEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginRebuild([In] ksDocument3DNotify7_BeginRebuildEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Rebuild([In] ksDocument3DNotify7_RebuildEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_RebuildEventHandler Rebuild;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Rebuild([In] ksDocument3DNotify7_RebuildEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginChoiceMaterial(
//    [In] ksDocument3DNotify7_BeginChoiceMaterialEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_BeginChoiceMaterialEventHandler BeginChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginChoiceMaterial(
//    [In] ksDocument3DNotify7_BeginChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChoiceMaterial(
//    [In] ksDocument3DNotify7_ChoiceMaterialEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_ChoiceMaterialEventHandler ChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChoiceMaterial(
//    [In] ksDocument3DNotify7_ChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginChoiceMarking(
//    [In] ksDocument3DNotify7_BeginChoiceMarkingEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_BeginChoiceMarkingEventHandler BeginChoiceMarking;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginChoiceMarking(
//    [In] ksDocument3DNotify7_BeginChoiceMarkingEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChoiceMarking([In] ksDocument3DNotify7_ChoiceMarkingEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_ChoiceMarkingEventHandler ChoiceMarking;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChoiceMarking([In] ksDocument3DNotify7_ChoiceMarkingEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginSetPartFromFile(
//    [In] ksDocument3DNotify7_BeginSetPartFromFileEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_BeginSetPartFromFileEventHandler BeginSetPartFromFile;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginSetPartFromFile(
//    [In] ksDocument3DNotify7_BeginSetPartFromFileEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_BeginCreatePartFromFileEventHandler BeginCreatePartFromFile;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCreatePartFromFile(
//    [In] ksDocument3DNotify7_BeginCreatePartFromFileEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCreatePartFromFile(
//    [In] ksDocument3DNotify7_BeginCreatePartFromFileEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_CreateEmbodimentEventHandler CreateEmbodiment;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CreateEmbodiment(
//    [In] ksDocument3DNotify7_CreateEmbodimentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CreateEmbodiment(
//    [In] ksDocument3DNotify7_CreateEmbodimentEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_DeleteEmbodimentEventHandler DeleteEmbodiment;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DeleteEmbodiment(
//    [In] ksDocument3DNotify7_DeleteEmbodimentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DeleteEmbodiment(
//    [In] ksDocument3DNotify7_DeleteEmbodimentEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_ChangeCurrentEmbodimentEventHandler ChangeCurrentEmbodiment;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeCurrentEmbodiment(
//    [In] ksDocument3DNotify7_ChangeCurrentEmbodimentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeCurrentEmbodiment(
//    [In] ksDocument3DNotify7_ChangeCurrentEmbodimentEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_BeginChoicePropertyEventHandler BeginChoiceProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginChoiceProperty(
//    [In] ksDocument3DNotify7_BeginChoicePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginChoiceProperty(
//    [In] ksDocument3DNotify7_BeginChoicePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChoiceProperty(
//    [In] ksDocument3DNotify7_ChoicePropertyEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_ChoicePropertyEventHandler ChoiceProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChoiceProperty(
//    [In] ksDocument3DNotify7_ChoicePropertyEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_BeginRollbackFeaturesEventHandler BeginRollbackFeatures;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginRollbackFeatures(
//    [In] ksDocument3DNotify7_BeginRollbackFeaturesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginRollbackFeatures(
//    [In] ksDocument3DNotify7_BeginRollbackFeaturesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_RollbackFeatures(
//    [In] ksDocument3DNotify7_RollbackFeaturesEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_RollbackFeaturesEventHandler RollbackFeatures;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_RollbackFeatures(
//    [In] ksDocument3DNotify7_RollbackFeaturesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BedinLoadCombinationChange(
//    [In] ksDocument3DNotify7_BedinLoadCombinationChangeEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_BedinLoadCombinationChangeEventHandler BedinLoadCombinationChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BedinLoadCombinationChange(
//    [In] ksDocument3DNotify7_BedinLoadCombinationChangeEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_LoadCombinationChangeEventHandler LoadCombinationChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_LoadCombinationChange(
//    [In] ksDocument3DNotify7_LoadCombinationChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_LoadCombinationChange(
//    [In] ksDocument3DNotify7_LoadCombinationChangeEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_BeginDeleteMaterialEventHandler BeginDeleteMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDeleteMaterial(
//    [In] ksDocument3DNotify7_BeginDeleteMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDeleteMaterial(
//    [In] ksDocument3DNotify7_BeginDeleteMaterialEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_DeleteMaterialEventHandler DeleteMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DeleteMaterial(
//    [In] ksDocument3DNotify7_DeleteMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DeleteMaterial(
//    [In] ksDocument3DNotify7_DeleteMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDeleteProperty(
//    [In] ksDocument3DNotify7_BeginDeletePropertyEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_BeginDeletePropertyEventHandler BeginDeleteProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDeleteProperty(
//    [In] ksDocument3DNotify7_BeginDeletePropertyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DeleteProperty(
//    [In] ksDocument3DNotify7_DeletePropertyEventHandler obj0);

//  public virtual extern event ksDocument3DNotify7_DeletePropertyEventHandler DeleteProperty;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DeleteProperty(
//    [In] ksDocument3DNotify7_DeletePropertyEventHandler obj0);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern IKompasAPIObject IKompasDocument_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasDocument_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasDocument_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasDocument_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern DocumentTypeEnum IKompasDocument_DocumentType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern DocumentFrames IKompasDocument_DocumentFrames { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IKompasDocument_Close([In] DocumentCloseOptions closeOptions);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void IKompasDocument_Save();

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void IKompasDocument_SaveAs([MarshalAs(UnmanagedType.BStr), In] string PathName);

//  public virtual extern string IKompasDocument_Name { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  public virtual extern string IKompasDocument_PathName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  public virtual extern string IKompasDocument_Path { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  public virtual extern bool IKompasDocument_Visible { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern bool IKompasDocument_Active { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  public virtual extern bool IKompasDocument_ReadOnly { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  public virtual extern bool IKompasDocument_Changed { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern IDocumentSettings IKompasDocument_DocumentSettings { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern SpecificationDescriptions IKompasDocument_SpecificationDescriptions { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern LayoutSheets IKompasDocument_LayoutSheets { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern UserDataStoragesMng IKompasDocument_UserDataStoragesMng { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }
//}
