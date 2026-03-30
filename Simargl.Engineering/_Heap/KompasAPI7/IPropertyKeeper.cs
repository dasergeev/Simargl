//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IPropertyKeeper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("AE7377CB-28C6-468E-B667-73117BCDC300")]
//[ComImport]
//public interface IPropertyKeeper
//{
//  [DispId(16000)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetPropertyValue([MarshalAs(UnmanagedType.Interface), In] _Property Property, [MarshalAs(UnmanagedType.Struct)] out object Value, [In] bool BaseUnit, out bool FromSource);

//  [DispId(16001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetPropertyValue([MarshalAs(UnmanagedType.Interface), In] _Property Property, [MarshalAs(UnmanagedType.Struct), In] object Value, [In] bool BaseUnit);

//  [DispId(16002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  UserDataStorage GetPropertyAdditionalStorage(
//    [MarshalAs(UnmanagedType.Interface), In] _Property Property,
//    [In] bool Create,
//    out bool FromSource);

//  [DispId(16003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  UserDataStoragesMng GetUserDataStoragesManager([In] bool FromSource);

//  [DispId(16004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool InsertHypertextReference(
//    [MarshalAs(UnmanagedType.Interface), In] _Property Property,
//    [MarshalAs(UnmanagedType.Interface), In] IKompasAPIObject Object,
//    [In] ksHypertextTypeEnum Type,
//    [In] bool Brackets,
//    [In] int TextLineIndex,
//    [In] int Precision,
//    [In] double PropertyId);

//  [DispId(16005)]
//  string Properties { [DispId(16005), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(16005), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(16006)]
//  string UniqueMetaObjectKey { [DispId(16006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(16007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool IsComplexPropertyValue([MarshalAs(UnmanagedType.Interface), In] _Property Property);

//  [DispId(16008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetComplexPropertyValue([MarshalAs(UnmanagedType.Interface), In] _Property Property, out bool FromSource);

//  [DispId(16009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetComplexPropertyValue([MarshalAs(UnmanagedType.Interface), In] _Property Property, [MarshalAs(UnmanagedType.BStr), In] string Value);
//}
