//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ILibItemSettings
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("C474582A-2AD4-4ED5-A86A-A169C4DA5D54")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ILibItemSettings
//{
//  [DispId(500)]
//  int ItemCount { [DispId(500), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double GetItem([MarshalAs(UnmanagedType.Struct), In] object Index, out bool Use);

//  [DispId(502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetItem([MarshalAs(UnmanagedType.Struct), In] object Index, [In] bool Use);

//  [DispId(503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetItems([MarshalAs(UnmanagedType.Struct)] out object UniqIds, [MarshalAs(UnmanagedType.Struct)] out object Uses);

//  [DispId(504)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetItemsEx([MarshalAs(UnmanagedType.Struct)] out object UniqIds, [MarshalAs(UnmanagedType.Struct)] out object Uses, [MarshalAs(UnmanagedType.Struct)] out object ItemNames, [MarshalAs(UnmanagedType.Struct)] out object FileNames);

//  [DispId(505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetItems([MarshalAs(UnmanagedType.Struct), In] object UniqIds, [MarshalAs(UnmanagedType.Struct), In] object Uses);
//}
