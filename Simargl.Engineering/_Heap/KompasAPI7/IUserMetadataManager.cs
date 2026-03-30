//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IUserMetadataManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("DC982411-CF10-4C00-946B-B5338448568A")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IUserMetadataManager
//{
//  [DispId(20001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CreateStorage([MarshalAs(UnmanagedType.BStr), In] string ApplicationIID, [MarshalAs(UnmanagedType.BStr), In] string ApplicationDescription, [MarshalAs(UnmanagedType.BStr), In] string Version);

//  [DispId(20002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ExistStorage([MarshalAs(UnmanagedType.BStr), In] string ApplicationIID);

//  [DispId(20003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteStorage([MarshalAs(UnmanagedType.BStr), In] string ApplicationIID);

//  [DispId(20004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object GetAllFilenames([MarshalAs(UnmanagedType.BStr), In] string ApplicationIID);

//  [DispId(20005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_StorageInfo([MarshalAs(UnmanagedType.BStr), In] string ApplicationIID, [MarshalAs(UnmanagedType.BStr), In] string ParameterName);

//  [DispId(20005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_StorageInfo([MarshalAs(UnmanagedType.BStr), In] string ApplicationIID, [MarshalAs(UnmanagedType.BStr), In] string ParameterName, [MarshalAs(UnmanagedType.BStr), In] string Result);

//  [DispId(20006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteFile([MarshalAs(UnmanagedType.BStr), In] string ApplicationIID, [MarshalAs(UnmanagedType.BStr), In] string StorageFileName);

//  [DispId(20007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool IsExistFile([MarshalAs(UnmanagedType.BStr), In] string ApplicationIID, [MarshalAs(UnmanagedType.BStr), In] string StorageFileName);

//  [DispId(20008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddFile(
//    [MarshalAs(UnmanagedType.BStr), In] string ApplicationIID,
//    [MarshalAs(UnmanagedType.BStr), In] string SrcFileName,
//    [MarshalAs(UnmanagedType.BStr), In] string DestFileName,
//    [In] bool AllowReplacement,
//    [In] bool Compress);

//  [DispId(20009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ExtractFile(
//    [MarshalAs(UnmanagedType.BStr), In] string ApplicationIID,
//    [MarshalAs(UnmanagedType.BStr), In] string SrcFileName,
//    [MarshalAs(UnmanagedType.BStr), In] string DestFileName,
//    [In] bool AllowReplacement);
//}
