//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksDocument3DNotify_SinkHelper
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksDocument3DNotify_SinkHelper : ksDocument3DNotify
//{
//  public ksDocument3DNotify_BeginRebuildEventHandler m_BeginRebuildDelegate;
//  public ksDocument3DNotify_RebuildEventHandler m_RebuildDelegate;
//  public ksDocument3DNotify_BeginChoiceMaterialEventHandler m_BeginChoiceMaterialDelegate;
//  public ksDocument3DNotify_ChoiceMaterialEventHandler m_ChoiceMaterialDelegate;
//  public ksDocument3DNotify_BeginChoiceMarkingEventHandler m_BeginChoiceMarkingDelegate;
//  public ksDocument3DNotify_ChoiceMarkingEventHandler m_ChoiceMarkingDelegate;
//  public ksDocument3DNotify_BeginSetPartFromFileEventHandler m_BeginSetPartFromFileDelegate;
//  public ksDocument3DNotify_BeginCreatePartFromFileEventHandler m_BeginCreatePartFromFileDelegate;
//  public ksDocument3DNotify_CreateEmbodimentEventHandler m_CreateEmbodimentDelegate;
//  public ksDocument3DNotify_DeleteEmbodimentEventHandler m_DeleteEmbodimentDelegate;
//  public ksDocument3DNotify_ChangeCurrentEmbodimentEventHandler m_ChangeCurrentEmbodimentDelegate;
//  public ksDocument3DNotify_BeginChoicePropertyEventHandler m_BeginChoicePropertyDelegate;
//  public ksDocument3DNotify_ChoicePropertyEventHandler m_ChoicePropertyDelegate;
//  public ksDocument3DNotify_BeginRollbackFeaturesEventHandler m_BeginRollbackFeaturesDelegate;
//  public ksDocument3DNotify_RollbackFeaturesEventHandler m_RollbackFeaturesDelegate;
//  public ksDocument3DNotify_BedinLoadCombinationChangeEventHandler m_BedinLoadCombinationChangeDelegate;
//  public ksDocument3DNotify_LoadCombinationChangeEventHandler m_LoadCombinationChangeDelegate;
//  public ksDocument3DNotify_BeginDeleteMaterialEventHandler m_BeginDeleteMaterialDelegate;
//  public ksDocument3DNotify_DeleteMaterialEventHandler m_DeleteMaterialDelegate;
//  public ksDocument3DNotify_BeginDeletePropertyEventHandler m_BeginDeletePropertyDelegate;
//  public ksDocument3DNotify_DeletePropertyEventHandler m_DeletePropertyDelegate;
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

//  public virtual bool BeginCreatePartFromFile([In] bool obj0, [In] entity obj1)
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

//  internal ksDocument3DNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginRebuildDelegate = (ksDocument3DNotify_BeginRebuildEventHandler) null;
//    this.m_RebuildDelegate = (ksDocument3DNotify_RebuildEventHandler) null;
//    this.m_BeginChoiceMaterialDelegate = (ksDocument3DNotify_BeginChoiceMaterialEventHandler) null;
//    this.m_ChoiceMaterialDelegate = (ksDocument3DNotify_ChoiceMaterialEventHandler) null;
//    this.m_BeginChoiceMarkingDelegate = (ksDocument3DNotify_BeginChoiceMarkingEventHandler) null;
//    this.m_ChoiceMarkingDelegate = (ksDocument3DNotify_ChoiceMarkingEventHandler) null;
//    this.m_BeginSetPartFromFileDelegate = (ksDocument3DNotify_BeginSetPartFromFileEventHandler) null;
//    this.m_BeginCreatePartFromFileDelegate = (ksDocument3DNotify_BeginCreatePartFromFileEventHandler) null;
//    this.m_CreateEmbodimentDelegate = (ksDocument3DNotify_CreateEmbodimentEventHandler) null;
//    this.m_DeleteEmbodimentDelegate = (ksDocument3DNotify_DeleteEmbodimentEventHandler) null;
//    this.m_ChangeCurrentEmbodimentDelegate = (ksDocument3DNotify_ChangeCurrentEmbodimentEventHandler) null;
//    this.m_BeginChoicePropertyDelegate = (ksDocument3DNotify_BeginChoicePropertyEventHandler) null;
//    this.m_ChoicePropertyDelegate = (ksDocument3DNotify_ChoicePropertyEventHandler) null;
//    this.m_BeginRollbackFeaturesDelegate = (ksDocument3DNotify_BeginRollbackFeaturesEventHandler) null;
//    this.m_RollbackFeaturesDelegate = (ksDocument3DNotify_RollbackFeaturesEventHandler) null;
//    this.m_BedinLoadCombinationChangeDelegate = (ksDocument3DNotify_BedinLoadCombinationChangeEventHandler) null;
//    this.m_LoadCombinationChangeDelegate = (ksDocument3DNotify_LoadCombinationChangeEventHandler) null;
//    this.m_BeginDeleteMaterialDelegate = (ksDocument3DNotify_BeginDeleteMaterialEventHandler) null;
//    this.m_DeleteMaterialDelegate = (ksDocument3DNotify_DeleteMaterialEventHandler) null;
//    this.m_BeginDeletePropertyDelegate = (ksDocument3DNotify_BeginDeletePropertyEventHandler) null;
//    this.m_DeletePropertyDelegate = (ksDocument3DNotify_DeletePropertyEventHandler) null;
//  }
//}
