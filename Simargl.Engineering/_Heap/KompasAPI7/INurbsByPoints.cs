//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.INurbsByPoints
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("7E553465-7C0D-4E35-B630-D7A54CD4FC2F")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface INurbsByPoints : INurbs
//{
//  [DispId(1000)]
//  new IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  new IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  new KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  new int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3000)]
//  new DrawingObjectTypeEnum DrawingObjectType { [DispId(3000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3001)]
//  new int LayerNumber { [DispId(3001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(3001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(3002)]
//  new bool Temp { [DispId(3002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3003)]
//  new bool Valid { [DispId(3003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool Update();

//  [DispId(3005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool Delete();

//  [DispId(3006)]
//  new ksDrawingObjectParamTypeEnum DrawingObjectParamType { [DispId(3006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(3006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(1)]
//  new bool Closed { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2)]
//  new int Degree { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3)]
//  new bool Periodic { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(4)]
//  new int PointsCount { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool SetNurbsParams([MarshalAs(UnmanagedType.Struct), In] object Points, [MarshalAs(UnmanagedType.Struct), In] object Weight, [MarshalAs(UnmanagedType.Struct), In] object Knots, [In] int Degree, [In] bool Closed);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool GetNurbsParams([MarshalAs(UnmanagedType.Struct)] out object Points, [MarshalAs(UnmanagedType.Struct)] out object Weight, [MarshalAs(UnmanagedType.Struct)] out object Knots);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool AddPoint([In] int Index, [In] double X, [In] double Y, [In] double Weight);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool GetPoint([In] int Index, out double X, out double Y, out double Weight);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool DeletePoint([In] int Index);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool Clear();

//  [DispId(11)]
//  new int Style { [DispId(11), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(11), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool ReadFromFile([MarshalAs(UnmanagedType.BStr), In] string FileName);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool WriteToFile([MarshalAs(UnmanagedType.BStr), In] string FileName);

//  [DispId(101)]
//  ksNurbsByPointsBuildingTypeEnum PointsBuildingType { [DispId(101), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(101), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(102)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddPointByParam(
//    [In] int Index,
//    [In] double X,
//    [In] double Y,
//    [In] ksNurbsByPointsPointConstraintsEnum ConstraintsType,
//    [In] ref double DerivativeLenght,
//    [In] ref double DerivativeAngle,
//    [In] ref double Curvature);

//  [DispId(103)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetPointParam(
//    [In] int Index,
//    out double X,
//    out double Y,
//    out ksNurbsByPointsPointConstraintsEnum ConstraintsType,
//    out double DerivativeLenght,
//    out double DerivativeAngle,
//    out double Curvature);

//  [DispId(104)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_PointConstraints([In] int PointIndex, [In] ksNurbsByPointsPointConstraintsEnum PVal);

//  [DispId(104)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  ksNurbsByPointsPointConstraintsEnum get_PointConstraints([In] int PointIndex);

//  [DispId(105)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_DerivativeLenghtInPoint([In] int PointIndex, [In] double PVal);

//  [DispId(105)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double get_DerivativeLenghtInPoint([In] int PointIndex);

//  [DispId(106)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_DerivativeAngleInPoint([In] int PointIndex, [In] double PVal);

//  [DispId(106)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double get_DerivativeAngleInPoint([In] int PointIndex);

//  [DispId(107)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_CurvatureInPoint([In] int PointIndex, [In] double PVal);

//  [DispId(107)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  double get_CurvatureInPoint([In] int PointIndex);
//}
