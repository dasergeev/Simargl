//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IProductDataManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("468578BC-BEAF-4053-AED9-4E10C48305C1")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IProductDataManager
//{
//  [DispId(18001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_ProductObjects([In] int Filter);

//  [DispId(18002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IPropertyKeeper get_ProductObject([MarshalAs(UnmanagedType.BStr), In] string UniqueMetaObjectKey);

//  [DispId(18003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IPropertyKeeper AddProductObject(
//    [MarshalAs(UnmanagedType.Interface), In] IPropertyKeeper Parent,
//    [MarshalAs(UnmanagedType.BStr), In] string Name,
//    [In] ksProductObjectTypeEnum ObjectType);

//  [DispId(18004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteProductObject([MarshalAs(UnmanagedType.BStr), In] string UniqueMetaObjectKey);

//  [DispId(18005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_Geometry([MarshalAs(UnmanagedType.Interface), In] IPropertyKeeper PropObject);

//  [DispId(18005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_Geometry([MarshalAs(UnmanagedType.Interface), In] IPropertyKeeper PropObject, [MarshalAs(UnmanagedType.Struct), In] object PVal);

//  [DispId(18006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_ObjectAttachedDocuments([MarshalAs(UnmanagedType.Interface), In] IPropertyKeeper PropObject);

//  [DispId(18006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ObjectAttachedDocuments([MarshalAs(UnmanagedType.Interface), In] IPropertyKeeper PropObject, [MarshalAs(UnmanagedType.Struct), In] object PVal);

//  [DispId(18007)]
//  string MetaProductInfo { [DispId(18007), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(18007), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(18008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_ObjectMetaProductInfo([MarshalAs(UnmanagedType.Interface), In] IPropertyKeeper PropObject);

//  [DispId(18008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ObjectMetaProductInfo([MarshalAs(UnmanagedType.Interface), In] IPropertyKeeper PropObject, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(18009)]
//  string ReferenceData { [DispId(18009), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(18009), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(18010)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_ReferenceDataIds([MarshalAs(UnmanagedType.BStr), In] string ReferenceDataType);

//  [DispId(18011)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_ReferenceDataInfo([MarshalAs(UnmanagedType.BStr), In] string ReferenceDataType, [MarshalAs(UnmanagedType.BStr), In] string ReferenceDataId);

//  [DispId(18011)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ReferenceDataInfo([MarshalAs(UnmanagedType.BStr), In] string ReferenceDataType, [MarshalAs(UnmanagedType.BStr), In] string ReferenceDataId, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(18012)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string AddReferenceData([MarshalAs(UnmanagedType.BStr), In] string ReferenceDataType, [MarshalAs(UnmanagedType.BStr), In] string ReferenceDataInfo);

//  [DispId(18013)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteReferenceData([MarshalAs(UnmanagedType.BStr), In] string ReferenceDataType, [MarshalAs(UnmanagedType.BStr), In] string ReferenceDataId);

//  [DispId(18014)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IPropertyKeeper FindProductObjectByGeometry([MarshalAs(UnmanagedType.Interface), In] IKompasAPIObject Geometry);
//}
