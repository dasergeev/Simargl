//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksBody
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[Guid("03EFC9DD-E05A-4277-BC7C-4FD499A252DE")]
//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[ComImport]
//public interface ksBody
//{
//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetGabarit(
//    out double x1,
//    out double y1,
//    out double z1,
//    out double x2,
//    out double y2,
//    out double z2);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object FaceCollection();

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool IsSolid();

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object CalcMassInertiaProperties(uint bitVector);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CurveIntersection([MarshalAs(UnmanagedType.IDispatch)] object curve, [MarshalAs(UnmanagedType.IDispatch)] object fases, [MarshalAs(UnmanagedType.IDispatch)] object points);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object CheckIntersectionWithBody([MarshalAs(UnmanagedType.IDispatch)] object otherBody, bool checkTangent);

//  [DispId(7)]
//  bool MultiBodyParts { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(7), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object GetFeature();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetIntersectionFacesWithBody(
//    [MarshalAs(UnmanagedType.IDispatch), In] object otherBody,
//    [MarshalAs(UnmanagedType.Struct)] out object intersectionFaces1,
//    [MarshalAs(UnmanagedType.Struct)] out object intersectionFaces2,
//    [MarshalAs(UnmanagedType.Struct)] out object connectedFaces1,
//    [MarshalAs(UnmanagedType.Struct)] out object connectedFaces2);
//}
