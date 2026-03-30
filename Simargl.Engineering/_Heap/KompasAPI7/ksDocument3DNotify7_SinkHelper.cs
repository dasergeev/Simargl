//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksDocument3DNotify7_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksDocument3DNotify7_SinkHelper : ksDocument3DNotify7
//{
//  public ksDocument3DNotify7_BeginRebuildEventHandler m_BeginRebuildDelegate;
//  public ksDocument3DNotify7_RebuildEventHandler m_RebuildDelegate;
//  public ksDocument3DNotify7_BeginChoiceMaterialEventHandler m_BeginChoiceMaterialDelegate;
//  public ksDocument3DNotify7_ChoiceMaterialEventHandler m_ChoiceMaterialDelegate;
//  public ksDocument3DNotify7_BeginChoiceMarkingEventHandler m_BeginChoiceMarkingDelegate;
//  public ksDocument3DNotify7_ChoiceMarkingEventHandler m_ChoiceMarkingDelegate;
//  public ksDocument3DNotify7_BeginSetPartFromFileEventHandler m_BeginSetPartFromFileDelegate;
//  public ksDocument3DNotify7_BeginCreatePartFromFileEventHandler m_BeginCreatePartFromFileDelegate;
//  public ksDocument3DNotify7_CreateEmbodimentEventHandler m_CreateEmbodimentDelegate;
//  public ksDocument3DNotify7_DeleteEmbodimentEventHandler m_DeleteEmbodimentDelegate;
//  public ksDocument3DNotify7_ChangeCurrentEmbodimentEventHandler m_ChangeCurrentEmbodimentDelegate;
//  public ksDocument3DNotify7_BeginChoicePropertyEventHandler m_BeginChoicePropertyDelegate;
//  public ksDocument3DNotify7_ChoicePropertyEventHandler m_ChoicePropertyDelegate;
//  public ksDocument3DNotify7_BeginRollbackFeaturesEventHandler m_BeginRollbackFeaturesDelegate;
//  public ksDocument3DNotify7_RollbackFeaturesEventHandler m_RollbackFeaturesDelegate;
//  public ksDocument3DNotify7_BedinLoadCombinationChangeEventHandler m_BedinLoadCombinationChangeDelegate;
//  public ksDocument3DNotify7_LoadCombinationChangeEventHandler m_LoadCombinationChangeDelegate;
//  public ksDocument3DNotify7_BeginDeleteMaterialEventHandler m_BeginDeleteMaterialDelegate;
//  public ksDocument3DNotify7_DeleteMaterialEventHandler m_DeleteMaterialDelegate;
//  public ksDocument3DNotify7_BeginDeletePropertyEventHandler m_BeginDeletePropertyDelegate;
//  public ksDocument3DNotify7_DeletePropertyEventHandler m_DeletePropertyDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginRebuild()
//  {
//    return this.m_BeginRebuildDelegate != null && this.m_BeginRebuildDelegate();
//  }

//  public virtual bool Rebuild() => this.m_RebuildDelegate != null && this.m_RebuildDelegate();

//  public virtual bool BeginChoiceMaterial()
//  {
//    return this.m_BeginChoiceMaterialDelegate != null && this.m_BeginChoiceMaterialDelegate();
//  }

//  public virtual bool ChoiceMaterial([In] string obj0, [In] double obj1)
//  {
//    return this.m_ChoiceMaterialDelegate != null && this.m_ChoiceMaterialDelegate(obj0, obj1);
//  }

//  public virtual bool BeginChoiceMarking()
//  {
//    return this.m_BeginChoiceMarkingDelegate != null && this.m_BeginChoiceMarkingDelegate();
//  }

//  public virtual bool ChoiceMarking([In] string obj0)
//  {
//    return this.m_ChoiceMarkingDelegate != null && this.m_ChoiceMarkingDelegate(obj0);
//  }

//  public virtual bool BeginSetPartFromFile()
//  {
//    return this.m_BeginSetPartFromFileDelegate != null && this.m_BeginSetPartFromFileDelegate();
//  }

//  public virtual bool BeginCreatePartFromFile([In] bool obj0, [In] IModelObject obj1)
//  {
//    return this.m_BeginCreatePartFromFileDelegate != null && this.m_BeginCreatePartFromFileDelegate(obj0, obj1);
//  }

//  public virtual bool CreateEmbodiment([In] string obj0)
//  {
//    return this.m_CreateEmbodimentDelegate != null && this.m_CreateEmbodimentDelegate(obj0);
//  }

//  public virtual bool DeleteEmbodiment([In] string obj0)
//  {
//    return this.m_DeleteEmbodimentDelegate != null && this.m_DeleteEmbodimentDelegate(obj0);
//  }

//  public virtual bool ChangeCurrentEmbodiment([In] string obj0)
//  {
//    return this.m_ChangeCurrentEmbodimentDelegate != null && this.m_ChangeCurrentEmbodimentDelegate(obj0);
//  }

//  public virtual bool BeginChoiceProperty([In] object obj0, [In] double obj1)
//  {
//    return this.m_BeginChoicePropertyDelegate != null && this.m_BeginChoicePropertyDelegate(obj0, obj1);
//  }

//  public virtual bool ChoiceProperty([In] object obj0, [In] double obj1)
//  {
//    return this.m_ChoicePropertyDelegate != null && this.m_ChoicePropertyDelegate(obj0, obj1);
//  }

//  public virtual bool BeginRollbackFeatures()
//  {
//    return this.m_BeginRollbackFeaturesDelegate != null && this.m_BeginRollbackFeaturesDelegate();
//  }

//  public virtual bool RollbackFeatures()
//  {
//    return this.m_RollbackFeaturesDelegate != null && this.m_RollbackFeaturesDelegate();
//  }

//  public virtual bool BedinLoadCombinationChange([In] int obj0)
//  {
//    return this.m_BedinLoadCombinationChangeDelegate != null && this.m_BedinLoadCombinationChangeDelegate(obj0);
//  }

//  public virtual bool LoadCombinationChange([In] int obj0)
//  {
//    return this.m_LoadCombinationChangeDelegate != null && this.m_LoadCombinationChangeDelegate(obj0);
//  }

//  public virtual bool BeginDeleteMaterial()
//  {
//    return this.m_BeginDeleteMaterialDelegate != null && this.m_BeginDeleteMaterialDelegate();
//  }

//  public virtual bool DeleteMaterial()
//  {
//    return this.m_DeleteMaterialDelegate != null && this.m_DeleteMaterialDelegate();
//  }

//  public virtual bool BeginDeleteProperty([In] object obj0, [In] double obj1)
//  {
//    return this.m_BeginDeletePropertyDelegate != null && this.m_BeginDeletePropertyDelegate(obj0, obj1);
//  }

//  public virtual bool DeleteProperty([In] object obj0, [In] double obj1)
//  {
//    return this.m_DeletePropertyDelegate != null && this.m_DeletePropertyDelegate(obj0, obj1);
//  }

//  internal ksDocument3DNotify7_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginRebuildDelegate = (ksDocument3DNotify7_BeginRebuildEventHandler) null;
//    this.m_RebuildDelegate = (ksDocument3DNotify7_RebuildEventHandler) null;
//    this.m_BeginChoiceMaterialDelegate = (ksDocument3DNotify7_BeginChoiceMaterialEventHandler) null;
//    this.m_ChoiceMaterialDelegate = (ksDocument3DNotify7_ChoiceMaterialEventHandler) null;
//    this.m_BeginChoiceMarkingDelegate = (ksDocument3DNotify7_BeginChoiceMarkingEventHandler) null;
//    this.m_ChoiceMarkingDelegate = (ksDocument3DNotify7_ChoiceMarkingEventHandler) null;
//    this.m_BeginSetPartFromFileDelegate = (ksDocument3DNotify7_BeginSetPartFromFileEventHandler) null;
//    this.m_BeginCreatePartFromFileDelegate = (ksDocument3DNotify7_BeginCreatePartFromFileEventHandler) null;
//    this.m_CreateEmbodimentDelegate = (ksDocument3DNotify7_CreateEmbodimentEventHandler) null;
//    this.m_DeleteEmbodimentDelegate = (ksDocument3DNotify7_DeleteEmbodimentEventHandler) null;
//    this.m_ChangeCurrentEmbodimentDelegate = (ksDocument3DNotify7_ChangeCurrentEmbodimentEventHandler) null;
//    this.m_BeginChoicePropertyDelegate = (ksDocument3DNotify7_BeginChoicePropertyEventHandler) null;
//    this.m_ChoicePropertyDelegate = (ksDocument3DNotify7_ChoicePropertyEventHandler) null;
//    this.m_BeginRollbackFeaturesDelegate = (ksDocument3DNotify7_BeginRollbackFeaturesEventHandler) null;
//    this.m_RollbackFeaturesDelegate = (ksDocument3DNotify7_RollbackFeaturesEventHandler) null;
//    this.m_BedinLoadCombinationChangeDelegate = (ksDocument3DNotify7_BedinLoadCombinationChangeEventHandler) null;
//    this.m_LoadCombinationChangeDelegate = (ksDocument3DNotify7_LoadCombinationChangeEventHandler) null;
//    this.m_BeginDeleteMaterialDelegate = (ksDocument3DNotify7_BeginDeleteMaterialEventHandler) null;
//    this.m_DeleteMaterialDelegate = (ksDocument3DNotify7_DeleteMaterialEventHandler) null;
//    this.m_BeginDeletePropertyDelegate = (ksDocument3DNotify7_BeginDeletePropertyEventHandler) null;
//    this.m_DeletePropertyDelegate = (ksDocument3DNotify7_DeletePropertyEventHandler) null;
//  }
//}
