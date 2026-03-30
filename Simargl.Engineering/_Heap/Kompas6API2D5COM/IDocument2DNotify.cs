//// Decompiled with JetBrains decompiler
//// Type: Kompas6API2D5COM.IDocument2DNotify
//// Assembly: Kompas6API2D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: D8B7A040-4B5D-44BB-964F-4529697D4B2C
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API2D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API2D5COM;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[Guid("3A1D1701-BA12-4D88-9C29-7C0FAF7A2800")]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface IDocument2DNotify : IKompasNotify
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool IsNotifyProcess(int notifyType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginRebuild();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Rebuild();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginChoiceMaterial();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChoiceMaterial([MarshalAs(UnmanagedType.LPStr)] string material, double density);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginInsertFragment();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool LocalFragmentEdit(int pDoc, bool newFrw);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginChoiceProperty(int objRef, double propID);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChoiceProperty(int objRef, double propID);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginDeleteProperty(int objRef, double propID);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteProperty(int objRef, double propID);
//}
