//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ILoadCombination
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("E7F1FD16-B641-4264-859C-D47217DA8B04")]
//[ComImport]
//public interface ILoadCombination
//{
//  [DispId(6001)]
//  bool CompletelyLoaded { [DispId(6001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(6002)]
//  object LoadCombinations { [DispId(6002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(6003)]
//  int CurrentIndex { [DispId(6003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(6003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(6004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int Create([MarshalAs(UnmanagedType.BStr), In] string CombinationName);

//  [DispId(6005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Delete([MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex);

//  [DispId(6006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Apply([MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex);

//  [DispId(6007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteEx([MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex, [MarshalAs(UnmanagedType.BStr), In] string Password, [In] bool DeleteDependant);

//  [DispId(6008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ApplyEx([MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex, [MarshalAs(UnmanagedType.BStr), In] string Password);

//  [DispId(6009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetPassword(
//    [MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex,
//    [MarshalAs(UnmanagedType.BStr), In] string OldPassword,
//    [MarshalAs(UnmanagedType.BStr), In] string Password,
//    [In] bool UnprotectUsers = false);

//  [DispId(6010)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool UpdateByModel([MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex, [MarshalAs(UnmanagedType.BStr), In] string Password);

//  [DispId(6011)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetLoadCombinationComment([MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex, [MarshalAs(UnmanagedType.BStr), In] string NewVal, [MarshalAs(UnmanagedType.BStr), In] string Password);

//  [DispId(6012)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetLoadCombinationComment([MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex);

//  [DispId(6013)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetLoadCombinationName([MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex, [MarshalAs(UnmanagedType.BStr), In] string NewVal, [MarshalAs(UnmanagedType.BStr), In] string Password);

//  [DispId(6014)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetLoadCombinationName([MarshalAs(UnmanagedType.Struct), In] object LoadCombinationIndex);

//  [DispId(6015)]
//  object ProtectedFlags { [DispId(6015), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }
//}
