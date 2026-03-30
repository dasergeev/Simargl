//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IMultiThicknessGroupsManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("4FB26621-6A86-43FF-BB61-AF1B04AFD22B")]
//[ComImport]
//public interface IMultiThicknessGroupsManager
//{
//  [DispId(7001)]
//  bool MultiThick { [DispId(7001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(7001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(7002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddMultiThicknessGroup([In] ksMultiThicknessGroupTypeEnum Type, [MarshalAs(UnmanagedType.Struct), In] object Objects, [In] double Thickness);

//  [DispId(7003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int get_MultiThicknessGroupsCount([In] ksMultiThicknessGroupTypeEnum Type);

//  [DispId(7004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_MultiThicknessGroupsObjects([In] ksMultiThicknessGroupTypeEnum Type, [In] int Index);

//  [DispId(7004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_MultiThicknessGroupsObjects(
//    [In] ksMultiThicknessGroupTypeEnum Type,
//    [In] int Index,
//    [MarshalAs(UnmanagedType.Struct), In] object Result);

//  [DispId(7005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double get_MultiThicknessGroupsThickness([In] ksMultiThicknessGroupTypeEnum Type, [In] int Index);

//  [DispId(7005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_MultiThicknessGroupsThickness(
//    [In] ksMultiThicknessGroupTypeEnum Type,
//    [In] int Index,
//    [In] double Result);

//  [DispId(7006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteMultiThicknessGroup([In] ksMultiThicknessGroupTypeEnum Type, [In] int Index);

//  [DispId(7007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DestroyMultiThicknessGroup([In] ksMultiThicknessGroupTypeEnum Type, [In] int Index);

//  [DispId(7008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ExcludeMultiThicknessGroupObjects([MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(7009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ClearMultiThicknessGroups();
//}
