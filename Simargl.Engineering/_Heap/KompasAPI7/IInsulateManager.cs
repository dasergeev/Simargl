//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IInsulateManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("E025E302-A36C-40B6-B0FA-0B73545F0D1E")]
//[ComImport]
//public interface IInsulateManager
//{
//  [DispId(9001)]
//  bool IsInsulateMode { [DispId(9001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(9002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool StartInsulateMode([MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(9003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool StopInsulateMode();

//  [DispId(9004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddInsulate([MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(9005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteInsulate([MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(9006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_IsInsulate([MarshalAs(UnmanagedType.Interface), In] IKompasAPIObject Object);

//  [DispId(9007)]
//  object InsulateObjects { [DispId(9007), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }
//}
