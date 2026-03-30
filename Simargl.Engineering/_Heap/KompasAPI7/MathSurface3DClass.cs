//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.MathSurface3DClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("D6DA79A3-CF5D-432B-900F-429403741DDD")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class MathSurface3DClass : IMathSurface3D, MathSurface3D, IKompasAPIObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern MathSurface3DClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetGabarit(
//    out double X1,
//    out double Y1,
//    out double Z1,
//    out double X2,
//    out double Y2,
//    out double Z2);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetPoint(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetNormal(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetTangentVectorU(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetTangentVectorV(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeU(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeV(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeUU(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeVV(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeUV(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeUUU(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeVVV(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeUVV(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetDerivativeUUV(
//    [In] double ParamU,
//    [In] double ParamV,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(15)]
//  public virtual extern double ParamUMin { [DispId(15), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(16 /*0x10*/)]
//  public virtual extern double ParamUMax { [DispId(16 /*0x10*/), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(17)]
//  public virtual extern double ParamVMin { [DispId(17), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(18)]
//  public virtual extern double ParamVMax { [DispId(18), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(19)]
//  public virtual extern bool ClosedU { [DispId(19), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(20)]
//  public virtual extern bool ClosedV { [DispId(20), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(21)]
//  public virtual extern ksMathSurface3DTypeEnum Surface3DType { [DispId(21), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double GetArea([In] ksLengthUnitsEnum BitVector);

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool NearPointProjection(
//    [In] double X,
//    [In] double Y,
//    [In] double Z,
//    out double ParamU,
//    out double ParamV,
//    [In] bool Ext);

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetBoundaryUVNurbs(
//    [In] bool UV,
//    [In] bool Closed,
//    [In] int LoopIndex,
//    [In] int EdgeIndex,
//    out int Degree,
//    [MarshalAs(UnmanagedType.Struct)] out object Points,
//    [MarshalAs(UnmanagedType.Struct)] out object Weights,
//    [MarshalAs(UnmanagedType.Struct)] out object Knots,
//    out double TMin,
//    out double TMax);

//  [DispId(25)]
//  public virtual extern int BoundaryCount { [DispId(25), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int GetEdgesCount([In] int LoopIndex);

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool NearDirectPointProjection(
//    [In] double X,
//    [In] double Y,
//    [In] double Z,
//    [In] double VX,
//    [In] double VY,
//    [In] double VZ,
//    [In] bool Extended,
//    out double U,
//    out double V);

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  public virtual extern object PointProjection(
//    [In] double X,
//    [In] double Y,
//    [In] double Z,
//    [In] double VX,
//    [In] double VY,
//    [In] double VZ,
//    [In] bool Extended);

//  [DispId(29)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double GetAreaEx([In] ksLengthUnitsEnum Unit, [In] double AngleTolerance);

//  [DispId(30)]
//  public virtual extern bool IsPlanar { [DispId(30), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(31 /*0x1F*/)]
//  public virtual extern bool IsCone { [DispId(31 /*0x1F*/), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(32 /*0x20*/)]
//  public virtual extern bool IsCylinder { [DispId(32 /*0x20*/), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(33)]
//  public virtual extern bool IsTorus { [DispId(33), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(34)]
//  public virtual extern bool IsSphere { [DispId(34), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(35)]
//  public virtual extern bool IsNurbsSurface { [DispId(35), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(36)]
//  public virtual extern bool IsRevolved { [DispId(36), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(37)]
//  public virtual extern bool IsSwept { [DispId(37), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(38)]
//  public virtual extern Placement3D Placement { [DispId(38), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(39)]
//  public virtual extern double Radius { [DispId(39), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(40)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetConeParam(out double Height, out double Angle, out double Radius);

//  [DispId(41)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetTorusParam(out double Radius, out double GeneratrixRadius);

//  [DispId(42)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetNurbsParams(
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

//  [DispId(43)]
//  public virtual extern ksMathSurface3DTypeEnum BaseSurface3DType { [DispId(43), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(44)]
//  public virtual extern ksSurfaceSalientTypeEnum IsSalient { [DispId(44), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
