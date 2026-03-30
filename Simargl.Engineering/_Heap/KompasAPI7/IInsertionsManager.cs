//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IInsertionsManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("B43FA13A-9488-422C-A9F3-D279FDA296BB")]
//[ComImport]
//public interface IInsertionsManager
//{
//  [DispId(501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_InsertionDefinitions([In] ksInsertionTypeEnum Type);

//  [DispId(502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  InsertionDefinition get_InsertionDefinition([In] ksInsertionTypeEnum Type, [MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  InsertionDefinition AddDefinition([In] ksInsertionTypeEnum Type, [MarshalAs(UnmanagedType.BStr), In] string Name, [MarshalAs(UnmanagedType.BStr), In] string FileName);

//  [DispId(504)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int get_DefinitionsCount([In] ksInsertionTypeEnum Type);
//}
