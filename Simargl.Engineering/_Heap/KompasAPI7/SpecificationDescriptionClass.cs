//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.SpecificationDescriptionClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[Guid("AD076943-BF97-4802-9D4F-D28C6C41E966")]
//[ComSourceInterfaces("KompasAPI7.ksSpecificationDescriptionNotify\0\0")]
//[ComImport]
//public class SpecificationDescriptionClass : 
//  ISpecificationDescription,
//  SpecificationDescription,
//  ksSpecificationDescriptionNotify_Event,
//  IKompasAPIObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern SpecificationDescriptionClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1)]
//  public virtual extern string LayoutName { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(2)]
//  public virtual extern int StyleID { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(3)]
//  public virtual extern string SpecificationDocumentName { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(4)]
//  public virtual extern bool Active { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Update();

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Delete();

//  [DispId(7)]
//  public virtual extern SpecificationStyle SpecificationStyle { [DispId(7), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(8)]
//  public virtual extern SpecificationTuning SpecificationTuning { [DispId(8), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(9)]
//  public virtual extern object Objects { [DispId(9), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(10)]
//  public virtual extern SpecificationBaseObjects BaseObjects { [DispId(10), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(11)]
//  public virtual extern SpecificationCommentObjects CommentObjects { [DispId(11), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(12)]
//  public virtual extern bool ShowOnSheet { [DispId(12), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(12), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(13)]
//  public virtual extern ISpecificationObject CurrentObject { [DispId(13), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; [DispId(13), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Interface), In] set; }

//  [DispId(14)]
//  public virtual extern bool ShowAllObjects { [DispId(14), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(14), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string get_PerformanceName([In] int PerformanceIndex, [In] int BlockIndex);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_PerformanceName([In] int PerformanceIndex, [In] int BlockIndex, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(16 /*0x10*/)]
//  public virtual extern bool DelegateMode { [DispId(16 /*0x10*/), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(16 /*0x10*/), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(17)]
//  public virtual extern bool NeedRebuild { [DispId(17), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(17), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern ksSpecificationStyleDifferenceTypeEnum CompareStyleWithLibStyle();

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string GetPerformanceParam(
//    [In] int DisplayPerformanceIndex,
//    out int PerformanceIndex,
//    out int BlockIndex);

//  [DispId(20)]
//  public virtual extern bool ShowExcludedObjects { [DispId(20), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(20), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(21)]
//  public virtual extern int PerformanceCount { [DispId(21), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(21), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(22)]
//  public virtual extern int PerformanceCountInBlock { [DispId(22), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_TuningSpcStyleBeginChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler TuningSpcStyleBeginChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_TuningSpcStyleBeginChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_TuningSpcStyleChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler TuningSpcStyleChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_TuningSpcStyleChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeCurrentSpcDescription(
//    [In] ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler ChangeCurrentSpcDescription;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeCurrentSpcDescription(
//    [In] ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionAdd(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler SpcDescriptionAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionAdd(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionRemove(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler SpcDescriptionRemove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionRemove(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler SpcDescriptionBeginEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionBeginEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionBeginEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler SpcDescriptionEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler SynchronizationBegin;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SynchronizationBegin(
//    [In] ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SynchronizationBegin(
//    [In] ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Synchronization(
//    [In] ksSpecificationDescriptionNotify_SynchronizationEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SynchronizationEventHandler Synchronization;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Synchronization(
//    [In] ksSpecificationDescriptionNotify_SynchronizationEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler BeginCalcPositions;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCalcPositions(
//    [In] ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCalcPositions(
//    [In] ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_CalcPositionsEventHandler CalcPositions;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CalcPositions(
//    [In] ksSpecificationDescriptionNotify_CalcPositionsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CalcPositions(
//    [In] ksSpecificationDescriptionNotify_CalcPositionsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCreateObject(
//    [In] ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler BeginCreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCreateObject(
//    [In] ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler obj0);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
