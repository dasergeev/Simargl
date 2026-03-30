//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IToleranceRecalc
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("07C35E20-5554-4C10-B0D4-5B07DD9CE842")]
//[ComImport]
//public interface IToleranceRecalc : IKompasAPIObject
//{
//  [DispId(1000)]
//  new IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  new IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  new KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  new int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1)]
//  string Name { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(2)]
//  bool Current { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3)]
//  int ItemsCount { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IKompasAPIObject get_Item([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddItems([MarshalAs(UnmanagedType.Interface), In] IKompasAPIObject Object);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddAllVariables();

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteItem([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_RecalcType([MarshalAs(UnmanagedType.Struct), In] object Index, [In] ksToleranceRecalcsEnum PVal);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  ksToleranceRecalcsEnum get_RecalcType([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_RecalcUserType([MarshalAs(UnmanagedType.Struct), In] object Index, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_RecalcUserType([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_Coefficient([MarshalAs(UnmanagedType.Struct), In] object Index, [In] double PVal);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double get_Coefficient([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(11)]
//  int Id { [DispId(11), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
