//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksNurbs3dParam
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[Guid("4DDDAEDB-2819-42D9-BDBB-4CCBC98D76DF")]
//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ksNurbs3dParam
//{
//  [DispId(1)]
//  short degree { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(1), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2)]
//  bool close { [DispId(2), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object GetPointCollection();

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object GetKnotCollection();

//  [DispId(5)]
//  bool periodic { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(5), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetNurbsPoints3DParams(
//    [In] bool closed,
//    [MarshalAs(UnmanagedType.Struct)] out object points,
//    [MarshalAs(UnmanagedType.Struct)] out object weights,
//    [MarshalAs(UnmanagedType.Struct)] out object knots);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetMinMaxParameters([In] bool closed, out double tMin, out double tMax);
//}
