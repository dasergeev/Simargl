//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IPLMObjectsManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("95615543-48E9-4738-9B01-D8ABC2C9001B")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IPLMObjectsManager
//{
//  [DispId(30001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetPLMChangesAttrAvailability([In] bool Available, [In] bool Enabled);

//  [DispId(30002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetPLMStatusAttrAvailability([In] bool Available, [In] bool Enabled);

//  [DispId(30003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_PLMStatus([MarshalAs(UnmanagedType.BStr), In] string FileName, [In] ksPLMStatusEnum PVal);

//  [DispId(30003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  ksPLMStatusEnum get_PLMStatus([MarshalAs(UnmanagedType.BStr), In] string FileName);

//  [DispId(30004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_PLMChange([MarshalAs(UnmanagedType.BStr), In] string FileName, [In] ksPLMChangesEnum PVal);

//  [DispId(30004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  ksPLMChangesEnum get_PLMChange([MarshalAs(UnmanagedType.BStr), In] string FileName);

//  [DispId(30005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetPLMPropertyAvailability(
//    [In] ksPLMPropertyEnum Property,
//    [In] bool Available,
//    [In] bool Enabled,
//    [MarshalAs(UnmanagedType.Struct), In] object ValueList);

//  [DispId(30006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_PLMPropertyValue([In] ksPLMPropertyEnum Property, [MarshalAs(UnmanagedType.BStr), In] string FileName, [MarshalAs(UnmanagedType.Struct), In] object PVal);

//  [DispId(30006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_PLMPropertyValue([In] ksPLMPropertyEnum Property, [MarshalAs(UnmanagedType.BStr), In] string FileName);
//}
