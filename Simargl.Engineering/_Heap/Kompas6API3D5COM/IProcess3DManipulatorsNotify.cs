//// Decompiled with JetBrains decompiler
//// Type: Kompas6API3D5COM.IProcess3DManipulatorsNotify
//// Assembly: Kompas6API3D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 98DBB410-35A6-4482-8352-058793489E25
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API3D5COM.dll

//using Kompas6API2D5COM;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API3D5COM;

//[Guid("45B82B5C-D0B7-4AC5-965C-26B09612CBF6")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface IProcess3DManipulatorsNotify : IKompasNotify
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool IsNotifyProcess(int notifyType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool RotateManipulator(
//    [In] int ManipulatorId,
//    [In] double X0,
//    [In] double Y0,
//    [In] double Z0,
//    [In] double AxisZX,
//    [In] double AxisZXY,
//    [In] double AxisZZ,
//    [In] double angle,
//    [In] bool FromEdit);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool MoveManipulator(
//    [In] int ManipulatorId,
//    [In] double VX,
//    [In] double VY,
//    [In] double VZ,
//    [In] double Delta,
//    [In] bool FromEdit);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ClickManipulatorPrimitive([In] int ManipulatorId, [In] int PrimitiveType, [In] bool DoubleClick);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginDragManipulator([In] int ManipulatorId, [In] int PrimitiveType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EndDragManipulator([In] int ManipulatorId, [In] int PrimitiveType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CreateManipulatorEdit([In] int ManipulatorId, [In] int PrimitiveType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DestroyManipulatorEdit([In] int ManipulatorId);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeManipulatorValue([In] int ManipulatorId, [In] int PrimitiveType, [In] double newValue);
//}
