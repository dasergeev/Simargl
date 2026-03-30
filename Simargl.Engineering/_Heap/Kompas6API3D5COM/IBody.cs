//// Decompiled with JetBrains decompiler
//// Type: Kompas6API3D5COM.IBody
//// Assembly: Kompas6API3D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 98DBB410-35A6-4482-8352-058793489E25
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API3D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API3D5COM;

//[Guid("BE70EEE5-1767-483E-9D18-79BCEC5AB837")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface IBody
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetGabarit(
//    ref double x1,
//    ref double y1,
//    ref double z1,
//    ref double x2,
//    ref double y2,
//    ref double z2);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IFaceCollection FaceCollection();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int IsSolid();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IMassInertiaParam CalcMassInertiaProperties(uint bitVector);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int CurveIntersection([MarshalAs(UnmanagedType.Interface)] ICurve3D curve, [MarshalAs(UnmanagedType.Interface)] IFaceCollection faces, [MarshalAs(UnmanagedType.Interface)] ICoordinate3dCollection points);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IIntersectionResult CheckIntersectionWithBody([MarshalAs(UnmanagedType.Interface)] IBody otherBody, int checkTangent);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetMultiBodyParts();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IFeature GetFeature();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetIntersectionFacesWithBody(
//    [MarshalAs(UnmanagedType.Interface), In] IBody otherBody,
//    [MarshalAs(UnmanagedType.Struct), In, Out] ref object intersectionFaces1,
//    [MarshalAs(UnmanagedType.Struct), In, Out] ref object intersectionFaces2,
//    [MarshalAs(UnmanagedType.Struct), In, Out] ref object connectedFaces1,
//    [MarshalAs(UnmanagedType.Struct), In, Out] ref object connectedFaces2);
//}
