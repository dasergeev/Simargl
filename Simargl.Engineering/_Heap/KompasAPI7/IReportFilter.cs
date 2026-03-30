//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IReportFilter
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("9D111C31-1629-4A0B-89E5-8461CDFA2157")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IReportFilter
//{
//  [DispId(100)]
//  int ConditionCount { [DispId(100), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(101)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetCondition(
//    [In] int Index,
//    [MarshalAs(UnmanagedType.Struct)] out object UniqId,
//    out ksReportFiltersTypeEnum Type,
//    [MarshalAs(UnmanagedType.Struct)] out object Val);

//  [DispId(102)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetCondition([In] int Index, [MarshalAs(UnmanagedType.Struct), In] object UniqId, [In] ksReportFiltersTypeEnum Type, [MarshalAs(UnmanagedType.Struct), In] object Val);

//  [DispId(103)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool RemoveCondition([In] int Index);

//  [DispId(104)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Clear();
//}
