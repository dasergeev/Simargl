//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.SpecificationDescriptionsClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Collections;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;
//using System.Runtime.InteropServices.CustomMarshalers;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[ComSourceInterfaces("KompasAPI7.ksSpecificationDescriptionNotify\0\0")]
//[Guid("77168B66-5A17-4694-ADDF-848E314EE0D1")]
//[ComImport]
//public class SpecificationDescriptionsClass : 
//  ISpecificationDescriptions,
//  SpecificationDescriptions,
//  ksSpecificationDescriptionNotify_Event,
//  IKompasAPIObject,
//  IKompasCollection,
//  IEnumerable
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern SpecificationDescriptionsClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(-4)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (EnumeratorToEnumVariantMarshaler))]
//  public virtual extern IEnumerator GetEnumerator();

//  [DispId(2000)]
//  public virtual extern int Count { [DispId(2000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(0)]
//  public virtual extern SpecificationDescription this[[MarshalAs(UnmanagedType.Struct), In] object Index] { [DispId(0), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern SpecificationDescription Add(
//    [MarshalAs(UnmanagedType.BStr), In] string LayoutName,
//    [In] int StyleID,
//    [MarshalAs(UnmanagedType.BStr), In] string SpcName);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern SpecificationDescription get_Description([MarshalAs(UnmanagedType.BStr), In] string LayoutName, [In] int StyleID);

//  [DispId(3)]
//  public virtual extern SpecificationDescription Active { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(4)]
//  public virtual extern SpecificationDescription ActiveFromLibStyle { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

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

//  public virtual extern event ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler ChangeCurrentSpcDescription;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeCurrentSpcDescription(
//    [In] ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeCurrentSpcDescription(
//    [In] ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler SpcDescriptionAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionAdd(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionAdd(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler SpcDescriptionRemove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionRemove(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionRemove(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionBeginEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler SpcDescriptionBeginEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SpcDescriptionBeginEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SpcDescriptionEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler SpcDescriptionEdit;

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

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CalcPositions(
//    [In] ksSpecificationDescriptionNotify_CalcPositionsEventHandler obj0);

//  public virtual extern event ksSpecificationDescriptionNotify_CalcPositionsEventHandler CalcPositions;

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

//  public virtual extern IKompasAPIObject IKompasCollection_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasCollection_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasCollection_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasCollection_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (EnumeratorToEnumVariantMarshaler))]
//  public virtual extern IEnumerator IKompasCollection_GetEnumerator();

//  public virtual extern int IKompasCollection_Count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
