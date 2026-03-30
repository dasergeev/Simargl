//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ISystemTemplatesSettings
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("409CE413-1FFA-4E20-B79A-62BFD87EFAD4")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ISystemTemplatesSettings
//{
//  [DispId(3001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_TemplatePath([In] DocumentTypeEnum DocumentType, [MarshalAs(UnmanagedType.BStr), In] string DocumentTypeId, [MarshalAs(UnmanagedType.BStr), In] string Result);

//  [DispId(3001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_TemplatePath([In] DocumentTypeEnum DocumentType, [MarshalAs(UnmanagedType.BStr), In] string DocumentTypeId);

//  [DispId(3002)]
//  bool AddGroupSpecificationTemplate { [DispId(3002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(3002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(3003)]
//  int GroupSpecificationEmbodimentCount { [DispId(3003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(3003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(3004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_SpecificationTemplatePath([In] ksSpecificationVariantEnum SpcType, [MarshalAs(UnmanagedType.BStr), In] string Result);

//  [DispId(3004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_SpecificationTemplatePath([In] ksSpecificationVariantEnum SpcType);
//}
