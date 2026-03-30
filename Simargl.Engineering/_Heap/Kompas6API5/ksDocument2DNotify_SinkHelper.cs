//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksDocument2DNotify_SinkHelper
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//public sealed class ksDocument2DNotify_SinkHelper : ksDocument2DNotify
//{
//  public ksDocument2DNotify_BeginRebuildEventHandler m_BeginRebuildDelegate;
//  public ksDocument2DNotify_RebuildEventHandler m_RebuildDelegate;
//  public ksDocument2DNotify_BeginChoiceMaterialEventHandler m_BeginChoiceMaterialDelegate;
//  public ksDocument2DNotify_ChoiceMaterialEventHandler m_ChoiceMaterialDelegate;
//  public ksDocument2DNotify_BeginInsertFragmentEventHandler m_BeginInsertFragmentDelegate;
//  public ksDocument2DNotify_LocalFragmentEditEventHandler m_LocalFragmentEditDelegate;
//  public ksDocument2DNotify_BeginChoicePropertyEventHandler m_BeginChoicePropertyDelegate;
//  public ksDocument2DNotify_ChoicePropertyEventHandler m_ChoicePropertyDelegate;
//  public ksDocument2DNotify_BeginDeletePropertyEventHandler m_BeginDeletePropertyDelegate;
//  public ksDocument2DNotify_DeletePropertyEventHandler m_DeletePropertyDelegate;
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

//  public virtual bool BeginInsertFragment()
//  {
//    return this.m_BeginInsertFragmentDelegate != null && this.m_BeginInsertFragmentDelegate();
//  }

//  public virtual bool LocalFragmentEdit([In] object obj0, [In] bool obj1)
//  {
//    return this.m_LocalFragmentEditDelegate != null && this.m_LocalFragmentEditDelegate(obj0, obj1);
//  }

//  public virtual bool BeginChoiceProperty([In] int obj0, [In] double obj1)
//  {
//    return this.m_BeginChoicePropertyDelegate != null && this.m_BeginChoicePropertyDelegate(obj0, obj1);
//  }

//  public virtual bool ChoiceProperty([In] int obj0, [In] double obj1)
//  {
//    return this.m_ChoicePropertyDelegate != null && this.m_ChoicePropertyDelegate(obj0, obj1);
//  }

//  public virtual bool BeginDeleteProperty([In] int obj0, [In] double obj1)
//  {
//    return this.m_BeginDeletePropertyDelegate != null && this.m_BeginDeletePropertyDelegate(obj0, obj1);
//  }

//  public virtual bool DeleteProperty([In] int obj0, [In] double obj1)
//  {
//    return this.m_DeletePropertyDelegate != null && this.m_DeletePropertyDelegate(obj0, obj1);
//  }

//  internal ksDocument2DNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginRebuildDelegate = (ksDocument2DNotify_BeginRebuildEventHandler) null;
//    this.m_RebuildDelegate = (ksDocument2DNotify_RebuildEventHandler) null;
//    this.m_BeginChoiceMaterialDelegate = (ksDocument2DNotify_BeginChoiceMaterialEventHandler) null;
//    this.m_ChoiceMaterialDelegate = (ksDocument2DNotify_ChoiceMaterialEventHandler) null;
//    this.m_BeginInsertFragmentDelegate = (ksDocument2DNotify_BeginInsertFragmentEventHandler) null;
//    this.m_LocalFragmentEditDelegate = (ksDocument2DNotify_LocalFragmentEditEventHandler) null;
//    this.m_BeginChoicePropertyDelegate = (ksDocument2DNotify_BeginChoicePropertyEventHandler) null;
//    this.m_ChoicePropertyDelegate = (ksDocument2DNotify_ChoicePropertyEventHandler) null;
//    this.m_BeginDeletePropertyDelegate = (ksDocument2DNotify_BeginDeletePropertyEventHandler) null;
//    this.m_DeletePropertyDelegate = (ksDocument2DNotify_DeletePropertyEventHandler) null;
//  }
//}
