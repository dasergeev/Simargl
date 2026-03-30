//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.INurbsSurface
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
//[Guid("5DAFC690-B3A7-4771-8E98-F83B5BF0D454")]
//[ComImport]
//public interface INurbsSurface : IModelObject
//{
//  [DispId(1000)]
//  new IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  new IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  new KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  new int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(501)]
//  new string Name { [DispId(501), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(501), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(502)]
//  new bool Hidden { [DispId(502), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(502), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool Update();

//  [DispId(504)]
//  new bool Valid { [DispId(504), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(505)]
//  new Part7 Part { [DispId(505), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(506)]
//  new ksObj3dTypeEnum ModelObjectType { [DispId(506), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(507)]
//  new IFeature7 Owner { [DispId(507), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetNurbsParams(
//    [In] bool ClosedV,
//    [In] bool ClosedU,
//    out int DegreeV,
//    out int DegreeU,
//    out int NPV,
//    out int NPU,
//    [MarshalAs(UnmanagedType.Struct)] out object Points,
//    [MarshalAs(UnmanagedType.Struct)] out object Weights,
//    [MarshalAs(UnmanagedType.Struct)] out object KnotsV,
//    [MarshalAs(UnmanagedType.Struct)] out object KnotsU);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetNurbsParams(
//    [In] bool ClosedV,
//    [In] bool ClosedU,
//    [In] int DegreeV,
//    [In] int DegreeU,
//    [In] int NPV,
//    [In] int NPU,
//    [MarshalAs(UnmanagedType.Struct), In] object Points,
//    [MarshalAs(UnmanagedType.Struct), In] object Weights,
//    [MarshalAs(UnmanagedType.Struct), In] object KnotsV,
//    [MarshalAs(UnmanagedType.Struct), In] object KnotsU);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool InitParamByFace([MarshalAs(UnmanagedType.Interface), In] IModelObject Face);

//  [DispId(4)]
//  bool ClosedU { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(5)]
//  bool ClosedV { [DispId(5), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(5), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(6)]
//  int BoundaryCount { [DispId(6), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddBoundary(
//    [In] bool UV,
//    [In] bool Closed,
//    [In] int Degree,
//    [MarshalAs(UnmanagedType.Struct), In] object Points,
//    [MarshalAs(UnmanagedType.Struct), In] object Weights,
//    [MarshalAs(UnmanagedType.Struct), In] object Knots,
//    [In] double TMin,
//    [In] double TMax);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetBoundary(
//    [In] bool UV,
//    [In] bool Closed,
//    [In] int BoundaryIndex,
//    out int Degree,
//    [MarshalAs(UnmanagedType.Struct)] out object Points,
//    [MarshalAs(UnmanagedType.Struct)] out object Weights,
//    [MarshalAs(UnmanagedType.Struct)] out object Knots,
//    out double TMin,
//    out double TMax);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteBoundary([In] int BoundaryIndex);
//}
