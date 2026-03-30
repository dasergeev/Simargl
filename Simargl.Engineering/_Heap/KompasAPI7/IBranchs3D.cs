//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IBranchs3D
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("AFE08AEC-F751-42D9-A6F5-3C004E4D41A6")]
//[ComImport]
//public interface IBranchs3D
//{
//  [DispId(5001)]
//  int BranchCount { [DispId(5001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetBranchBeginPoint(out double X, out double Y, out double Z);

//  [DispId(5003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetBranchBeginPoint([In] double X, [In] double Y, [In] double Z);

//  [DispId(5004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetBranchEndPoint([In] int Index, out double X, out double Y, out double Z);

//  [DispId(5005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetBranchEndPoint([In] int Index, [In] double X, [In] double Y, [In] double Z);

//  [DispId(5006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_BranchPoints([In] int Index, [MarshalAs(UnmanagedType.Struct), In] object PVal);

//  [DispId(5006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_BranchPoints([In] int Index);

//  [DispId(5007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int get_BranchPointsCount([In] int Index);

//  [DispId(5008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddBranch([MarshalAs(UnmanagedType.Struct), In] object Points, [MarshalAs(UnmanagedType.Interface), In] IModelObject Object);

//  [DispId(5009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddBranchByPoint([In] double X, [In] double Y, [In] double Z, [MarshalAs(UnmanagedType.Interface), In] IModelObject Object);

//  [DispId(5010)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteBranch([In] int Index);

//  [DispId(5011)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IModelObject get_BranchObject([In] int Index);

//  [DispId(5012)]
//  object BranchObjects { [DispId(5012), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(5013)]
//  object BranchEndPoints { [DispId(5013), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }
//}
