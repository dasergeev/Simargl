//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.SpecificationBaseObjectsClass
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
//[ComSourceInterfaces("KompasAPI7.ksSpecificationObjectNotify\0\0")]
//[Guid("E2FD0C57-3E8A-4EC2-A1E9-7BEB75E3AB7C")]
//[ComImport]
//public class SpecificationBaseObjectsClass : 
//  ISpecificationBaseObjects,
//  SpecificationBaseObjects,
//  ksSpecificationObjectNotify_Event,
//  IKompasAPIObject,
//  IKompasCollection,
//  IEnumerable
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern SpecificationBaseObjectsClass();

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
//  public virtual extern SpecificationBaseObject this[[MarshalAs(UnmanagedType.Struct), In] object Index] { [DispId(0), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern SpecificationBaseObject Add([In] int SectionNumb, [In] double AttrNumb);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  public virtual extern object GetSpecificationObjectsForGeom(
//    [MarshalAs(UnmanagedType.Struct), In] object PGeom,
//    [In] bool Equal,
//    [In] int SectionNumb,
//    [In] double AttrNumb);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern SpecificationBaseObject CopySpecificationObject(
//    [MarshalAs(UnmanagedType.Interface), In] SpecificationBaseObject SpcObj,
//    [MarshalAs(UnmanagedType.Struct), In] object Geometry);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern SpecificationBaseObject AddWithParam(
//    [MarshalAs(UnmanagedType.Interface), In] SpecificationObjectCreateParam Param,
//    [MarshalAs(UnmanagedType.Interface), In] IPropertyKeeper PropertyObject,
//    bool FillText,
//    [MarshalAs(UnmanagedType.Struct), In] object Geometry);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDelete(
//    [In] ksSpecificationObjectNotify_BeginDeleteEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_BeginDeleteEventHandler BeginDelete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDelete(
//    [In] ksSpecificationObjectNotify_BeginDeleteEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_DeleteEventHandler Delete;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Delete(
//    [In] ksSpecificationObjectNotify_DeleteEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Delete(
//    [In] ksSpecificationObjectNotify_DeleteEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_CellDblClickEventHandler CellDblClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CellDblClick(
//    [In] ksSpecificationObjectNotify_CellDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CellDblClick(
//    [In] ksSpecificationObjectNotify_CellDblClickEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_CellBeginEditEventHandler CellBeginEdit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CellBeginEdit(
//    [In] ksSpecificationObjectNotify_CellBeginEditEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CellBeginEdit(
//    [In] ksSpecificationObjectNotify_CellBeginEditEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_ChangeCurrentEventHandler ChangeCurrent;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeCurrent(
//    [In] ksSpecificationObjectNotify_ChangeCurrentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeCurrent(
//    [In] ksSpecificationObjectNotify_ChangeCurrentEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_DocumentBeginAddEventHandler DocumentBeginAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DocumentBeginAdd(
//    [In] ksSpecificationObjectNotify_DocumentBeginAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DocumentBeginAdd(
//    [In] ksSpecificationObjectNotify_DocumentBeginAddEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_DocumentAddEventHandler DocumentAdd;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DocumentAdd(
//    [In] ksSpecificationObjectNotify_DocumentAddEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DocumentAdd(
//    [In] ksSpecificationObjectNotify_DocumentAddEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_DocumentRemoveEventHandler DocumentRemove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DocumentRemove(
//    [In] ksSpecificationObjectNotify_DocumentRemoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DocumentRemove(
//    [In] ksSpecificationObjectNotify_DocumentRemoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginGeomChange(
//    [In] ksSpecificationObjectNotify_BeginGeomChangeEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_BeginGeomChangeEventHandler BeginGeomChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginGeomChange(
//    [In] ksSpecificationObjectNotify_BeginGeomChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_GeomChange(
//    [In] ksSpecificationObjectNotify_GeomChangeEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_GeomChangeEventHandler GeomChange;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_GeomChange(
//    [In] ksSpecificationObjectNotify_GeomChangeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginProcess(
//    [In] ksSpecificationObjectNotify_BeginProcessEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginProcess(
//    [In] ksSpecificationObjectNotify_BeginProcessEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndProcess(
//    [In] ksSpecificationObjectNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndProcess(
//    [In] ksSpecificationObjectNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CreateObject(
//    [In] ksSpecificationObjectNotify_CreateObjectEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_CreateObjectEventHandler CreateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CreateObject(
//    [In] ksSpecificationObjectNotify_CreateObjectEventHandler obj0);

//  public virtual extern event ksSpecificationObjectNotify_UpdateObjectEventHandler UpdateObject;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_UpdateObject(
//    [In] ksSpecificationObjectNotify_UpdateObjectEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_UpdateObject(
//    [In] ksSpecificationObjectNotify_UpdateObjectEventHandler obj0);

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
