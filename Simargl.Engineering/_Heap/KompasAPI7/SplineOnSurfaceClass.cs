//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.SplineOnSurfaceClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("FE7D22F2-17ED-4446-A031-67D67AC47F29")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class SplineOnSurfaceClass : ISplineOnSurface, SplineOnSurface, IKompasAPIObject, IModelObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern SplineOnSurfaceClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(501)]
//  public virtual extern string Name { [DispId(501), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(501), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(502)]
//  public virtual extern bool Hidden { [DispId(502), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(502), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Update();

//  [DispId(504)]
//  public virtual extern bool Valid { [DispId(504), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(505)]
//  public virtual extern Part7 Part { [DispId(505), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(506)]
//  public virtual extern ksObj3dTypeEnum ModelObjectType { [DispId(506), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(507)]
//  public virtual extern IFeature7 Owner { [DispId(507), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1)]
//  public virtual extern bool Closed { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2)]
//  public virtual extern bool SplineOnPoles { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3)]
//  public virtual extern int SplineOrder { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(4)]
//  public virtual extern IModelObject SurfaceObject { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Interface), In] set; [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5)]
//  public virtual extern int VertexCount { [DispId(5), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetVertex([In] int Index, out double U, out double V, out double W);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetVertex([In] int Index, [In] double U, [In] double V, [In] double W);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool AddVertex(
//    [In] int IndexAt,
//    [In] double U,
//    [In] double V,
//    [In] double W,
//    [MarshalAs(UnmanagedType.Interface), In] IModelObject AssociationObject);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool DeleteVertex([In] int Index);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Clear();

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetPoint(
//    [In] int Index,
//    out double X,
//    out double Y,
//    out double Z,
//    out double W);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetPoint([In] int Index, [In] double X, [In] double Y, [In] double Z, [In] double W);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool AddPoint(
//    [In] int IndexAt,
//    [In] double X,
//    [In] double Y,
//    [In] double Z,
//    [In] double W,
//    [MarshalAs(UnmanagedType.Interface)] IModelObject AssociationObject);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetVertexParams([MarshalAs(UnmanagedType.Struct)] out object UV, [MarshalAs(UnmanagedType.Struct)] out object Points, [MarshalAs(UnmanagedType.Struct)] out object Weights);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_AssociationObject([In] int Index, [MarshalAs(UnmanagedType.Interface), In] IModelObject PVal);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern IModelObject get_AssociationObject([In] int Index);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_SplineTangent([In] int Index, [In] ksSplineTangentEnum PVal);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern ksSplineTangentEnum get_SplineTangent([In] int Index);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetTangentVector([In] int Index, [In] double X, [In] double Y, [In] double Z);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetTangentVector([In] int Index, out double X, out double Y, out double Z);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Invert([In] int Index);

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_VectorLenght([In] int Index, [In] double PVal);

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double get_VectorLenght([In] int Index);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_TangentCurve([In] int Index, [MarshalAs(UnmanagedType.Interface), In] IModelObject PVal);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern IModelObject get_TangentCurve([In] int Index);

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ClearTangentParameters();

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_Reverse([In] int Index, [In] bool PVal);

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool get_Reverse([In] int Index);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern IKompasAPIObject IModelObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IModelObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IModelObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IModelObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern string IModelObject_Name { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  public virtual extern bool IModelObject_Hidden { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IModelObject_Update();

//  public virtual extern bool IModelObject_Valid { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern Part7 IModelObject_Part { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern ksObj3dTypeEnum IModelObject_ModelObjectType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern IFeature7 IModelObject_Owner { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }
//}
