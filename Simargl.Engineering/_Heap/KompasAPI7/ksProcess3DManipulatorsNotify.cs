//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksProcess3DManipulatorsNotify
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("45B82B5C-D0B7-4AC5-965C-26B09612CBF6")]
//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[ComImport]
//public interface ksProcess3DManipulatorsNotify
//{
//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool RotateManipulator(
//    [In] int ManipulatorId,
//    [In] double X0,
//    [In] double Y0,
//    [In] double Z0,
//    [In] double AxisZX,
//    [In] double AxisZXY,
//    [In] double AxisZZ,
//    [In] double Angle,
//    [In] bool FromEdit);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool MoveManipulator(
//    [In] int ManipulatorId,
//    [In] double VX,
//    [In] double VY,
//    [In] double VZ,
//    [In] double Delta,
//    [In] bool FromEdit);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ClickManipulatorPrimitive(
//    [In] int ManipulatorId,
//    [In] ksManipulatorPrimitiveEnum PrimitiveType,
//    [In] bool DoubleClick);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginDragManipulator([In] int ManipulatorId, [In] ksManipulatorPrimitiveEnum PrimitiveType);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EndDragManipulator([In] int ManipulatorId, [In] ksManipulatorPrimitiveEnum PrimitiveType);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CreateManipulatorEdit([In] int ManipulatorId, [In] ksManipulatorPrimitiveEnum PrimitiveType);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DestroyManipulatorEdit([In] int ManipulatorId);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeManipulatorValue(
//    [In] int ManipulatorId,
//    [In] ksManipulatorPrimitiveEnum PrimitiveType,
//    [In] double newValue);
//}
