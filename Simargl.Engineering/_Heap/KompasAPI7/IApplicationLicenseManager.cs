//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IApplicationLicenseManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("742AA7CC-8019-4854-A8FF-9EE1FFBD0460")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IApplicationLicenseManager
//{
//  [DispId(8001)]
//  int KompasVariant { [DispId(8001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(8002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_KompasModuleActive([In] ksKompasModuleEnum Module, [In] bool PVal);

//  [DispId(8002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_KompasModuleActive([In] ksKompasModuleEnum Module);

//  [DispId(8003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EnableKompasInvisible([MarshalAs(UnmanagedType.BStr), In] string Key, [MarshalAs(UnmanagedType.BStr), In] string Signature);

//  [DispId(8004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_LibraryActive([In] int ProductNumber);

//  [DispId(8004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_LibraryActive([In] int ProductNumber, [In] bool PVal);

//  [DispId(8005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  ksProtectProductStatusEnum get_LibraryStatus([In] int ProductNumber);

//  [DispId(8006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_LibraryTrialStatus([In] int ProductNumber);

//  [DispId(8007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_LibraryLocalStatus([In] int ProductNumber);

//  [DispId(8008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_LibraryProductName([In] int ProductNumber);

//  [DispId(8009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_LibraryProductKeyInfo([In] int ProductNumber);

//  [DispId(8010)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int RegisterLibraryNumber([In] int ProductNumber);

//  [DispId(8011)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool UnRegisterLibraryNumber([In] int ProductNumbUnicueId);
//}
