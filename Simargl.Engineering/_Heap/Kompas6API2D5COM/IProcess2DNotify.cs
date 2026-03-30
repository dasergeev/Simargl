//// Decompiled with JetBrains decompiler
//// Type: Kompas6API2D5COM.IProcess2DNotify
//// Assembly: Kompas6API2D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: D8B7A040-4B5D-44BB-964F-4529697D4B2C
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API2D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API2D5COM;

//[Guid("225DDE9A-A442-4CD5-8428-87612BC0677A")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface IProcess2DNotify : IKompasNotify
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool IsNotifyProcess(int notifyType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool PlacementChange(double x, double y, double angle, bool dynamic);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ExecuteCommand(int command);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Run();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Stop();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Activate();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Deactivate();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EndProcess();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetMouseEnterLeavePoint([MarshalAs(UnmanagedType.IUnknown)] object Control, int btnID, int pointIndex, [MarshalAs(UnmanagedType.IUnknown)] object parameters);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AbortProcess();
//}
