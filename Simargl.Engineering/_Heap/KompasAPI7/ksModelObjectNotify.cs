//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksModelObjectNotify
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[Guid("7B8E86E1-4304-449D-9CEC-1B14F74A16FB")]
//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ksModelObjectNotify
//{
//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginDelete([MarshalAs(UnmanagedType.Struct), In] object Obj);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Delete([MarshalAs(UnmanagedType.Struct), In] object Obj);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Excluded([MarshalAs(UnmanagedType.Struct), In] object Obj, [In] bool Excluded);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Hidden([MarshalAs(UnmanagedType.Struct), In] object Obj, [In] bool _Hidden);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginPropertyChanged([MarshalAs(UnmanagedType.Struct), In] object Obj);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool PropertyChanged([MarshalAs(UnmanagedType.Struct), In] object Obj);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginPlacementChanged([MarshalAs(UnmanagedType.Struct), In] object Obj);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool PlacementChanged([MarshalAs(UnmanagedType.Struct), In] object Obj);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginProcess([In] int PType, [MarshalAs(UnmanagedType.Struct), In] object PObj);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EndProcess([In] int PType);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CreateObject([MarshalAs(UnmanagedType.Struct), In] object Obj);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool UpdateObject([MarshalAs(UnmanagedType.Struct), In] object Obj);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginLoadStateChange([MarshalAs(UnmanagedType.Struct), In] object Obj, int LoadState);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool LoadStateChange([MarshalAs(UnmanagedType.Struct), In] object Obj, int LoadState);
//}
