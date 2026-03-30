//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IPropertyMng
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("FD515235-4DBA-4F81-8D5C-6FE87C834562")]
//[ComImport]
//public interface IPropertyMng
//{
//  [DispId(1500)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int get_PropertyCount([MarshalAs(UnmanagedType.Struct), In] object Libname);

//  [DispId(1501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  _Property GetProperty([MarshalAs(UnmanagedType.Struct), In] object Libname, [MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(1502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object GetProperties([MarshalAs(UnmanagedType.Struct), In] object Libname);

//  [DispId(1503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  _Property AddProperty([MarshalAs(UnmanagedType.Struct), In] object Libname, [MarshalAs(UnmanagedType.Struct), In] object Val);

//  [DispId(1504)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool RemoveProperty([MarshalAs(UnmanagedType.Struct), In] object Libname, [MarshalAs(UnmanagedType.Struct), In] object Val);

//  [DispId(1505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Report GetReport([MarshalAs(UnmanagedType.Struct), In] object Document, ksReportTypeEnum Type);
//}
