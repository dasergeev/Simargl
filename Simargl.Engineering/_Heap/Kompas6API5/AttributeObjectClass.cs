//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.AttributeObjectClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[Guid("FA93AA26-9B3D-11D6-95CE-00C0262D30E3")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class AttributeObjectClass : ksAttributeObject, AttributeObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern AttributeObjectClass();

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksCreateAttrType([MarshalAs(UnmanagedType.IDispatch)] object attrType, [MarshalAs(UnmanagedType.BStr)] string libName);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteAttrType(double attrID, [MarshalAs(UnmanagedType.BStr)] string libName, [MarshalAs(UnmanagedType.BStr)] string password);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetAttrType(double attrID, [MarshalAs(UnmanagedType.BStr)] string libName, [MarshalAs(UnmanagedType.IDispatch)] object attrType);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksSetAttrType(
//    double attrID,
//    [MarshalAs(UnmanagedType.BStr)] string libName,
//    [MarshalAs(UnmanagedType.IDispatch)] object attrType,
//    [MarshalAs(UnmanagedType.BStr)] string password);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksChoiceAttrTypes([MarshalAs(UnmanagedType.BStr)] string libName);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCreateAttr(int pObj, [MarshalAs(UnmanagedType.IDispatch)] object attr, double attrID, [MarshalAs(UnmanagedType.BStr)] string libName);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteAttr(int pObj, int pAttr, [MarshalAs(UnmanagedType.BStr)] string password);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetAttrValue(
//    int pAttr,
//    int rowNumb,
//    int columnNumb,
//    [MarshalAs(UnmanagedType.IDispatch)] object flagVisible,
//    [MarshalAs(UnmanagedType.IDispatch)] object columnKeys,
//    [MarshalAs(UnmanagedType.IDispatch)] object value);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetAttrValue(
//    int pAttr,
//    int rowNumb,
//    int columnNumb,
//    [MarshalAs(UnmanagedType.IDispatch)] object flagVisible,
//    [MarshalAs(UnmanagedType.IDispatch)] object columnKeys,
//    [MarshalAs(UnmanagedType.IDispatch)] object value,
//    [MarshalAs(UnmanagedType.BStr)] string password);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetAttrRow(
//    int pAttr,
//    int rowNumb,
//    [MarshalAs(UnmanagedType.IDispatch)] object flagVisible,
//    [MarshalAs(UnmanagedType.IDispatch)] object columnKeys,
//    [MarshalAs(UnmanagedType.IDispatch)] object value);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetAttrRow(
//    int pAttr,
//    int rowNumb,
//    [MarshalAs(UnmanagedType.IDispatch)] object flagVisible,
//    [MarshalAs(UnmanagedType.IDispatch)] object columnKeys,
//    [MarshalAs(UnmanagedType.IDispatch)] object value,
//    [MarshalAs(UnmanagedType.BStr)] string password);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAddAttrRow(
//    int pAttr,
//    int rowNumb,
//    [MarshalAs(UnmanagedType.IDispatch)] object flagVisible,
//    [MarshalAs(UnmanagedType.IDispatch)] object value,
//    [MarshalAs(UnmanagedType.BStr)] string password);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteAttrRow(int pAttr, int rowNumb, [MarshalAs(UnmanagedType.BStr)] string password);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSizeAttrValue([In] int pAttr, [In] int columnNumb, out int count);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSizeAttrRow([In] int pAttr, out int count);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetAttrKeysInfo(
//    [In] int pAttr,
//    out int key1,
//    out int key2,
//    out int key3,
//    out int key4,
//    out double numb);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetAttrColumnInfo(int pAttr, int columnNumb, [MarshalAs(UnmanagedType.IDispatch)] object columnInfo);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetAttrTabInfo([In] int pAttr, out int rowsCount, out int columnsCount);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksViewEditAttr(int pAttr, int type, [MarshalAs(UnmanagedType.BStr)] string password);

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksViewEditAttrType(
//    [MarshalAs(UnmanagedType.BStr)] string libName,
//    int type,
//    double attrID,
//    [MarshalAs(UnmanagedType.BStr)] string password);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksChoiceAttr(int pObj);

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetLibraryAttrTypesArray([MarshalAs(UnmanagedType.BStr)] string libName);

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Attribute3D ksCreateAttr3D(
//    [MarshalAs(UnmanagedType.IDispatch)] object pObj,
//    [MarshalAs(UnmanagedType.IDispatch)] object attr,
//    double attrID,
//    [MarshalAs(UnmanagedType.BStr)] string libName);

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteAttr3D([MarshalAs(UnmanagedType.IDispatch), In] object pObj, [MarshalAs(UnmanagedType.Interface), In] Attribute3D pAttr, [MarshalAs(UnmanagedType.BStr), In] string password);

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Attribute3D ksChoiceAttr3D([MarshalAs(UnmanagedType.IDispatch)] object pObj);

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Attribute3D ksCreateAttr3DEx(
//    [MarshalAs(UnmanagedType.IDispatch)] object pObj,
//    [MarshalAs(UnmanagedType.IDispatch)] object pSourcePart,
//    [MarshalAs(UnmanagedType.IDispatch)] object attr,
//    double attrID,
//    [MarshalAs(UnmanagedType.BStr)] string libName);

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSizeAttrValueW([In] int pAttr, [In] int columnNumb, out int count);

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSizeAttrRowW([In] int pAttr, out int count);
//}
