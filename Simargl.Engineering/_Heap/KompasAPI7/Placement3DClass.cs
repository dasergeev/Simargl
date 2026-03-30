//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.Placement3DClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("D30E2696-B263-4283-BACA-04471DF8311E")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class Placement3DClass : IPlacement3D, Placement3D, IKompasAPIObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern Placement3DClass();

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
//  public virtual extern bool GetOrigin(out double X, out double Y, out double Z);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetOrigin([In] double X, [In] double Y, [In] double Z);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetVector(
//    [In] ksObj3dTypeEnum Axis,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetVector([In] ksObj3dTypeEnum Axis, [In] double X, [In] double Y, [In] double Z);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool InitByMatrix3D([MarshalAs(UnmanagedType.Struct), In] object mtr);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  public virtual extern object GetMatrix3D();

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetPointProjectionToXY(
//    [In] double XIn,
//    [In] double YIn,
//    [In] double ZIn,
//    out double XOut,
//    out double YOut);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetPoint3D(
//    [In] double XIn,
//    [In] double YIn,
//    out double XOut,
//    out double YOut,
//    out double ZOut);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Rotate(
//    [In] double X0,
//    [In] double Y0,
//    [In] double Z0,
//    [In] double AxisZX,
//    [In] double AxisZXY,
//    [In] double AxisZZ,
//    [In] double Angle);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
