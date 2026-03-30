//// Decompiled with JetBrains decompiler
//// Type: Kompas6API2D5COM.ISpecificationNotify
//// Assembly: Kompas6API2D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: D8B7A040-4B5D-44BB-964F-4529697D4B2C
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API2D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API2D5COM;

//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[Guid("46D9F0CA-C094-41C8-B851-F86CF565481E")]
//[ComImport]
//public interface ISpecificationNotify : IKompasNotify
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool IsNotifyProcess(int notifyType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool TuningSpcStyleBeginChange([MarshalAs(UnmanagedType.LPStr)] string libName, int numb);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool TuningSpcStyleChange([MarshalAs(UnmanagedType.LPStr)] string libName, int numb, bool isOk);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeCurrentSpcDescription([MarshalAs(UnmanagedType.LPStr)] string libName, int numb);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SpcDescriptionAdd([MarshalAs(UnmanagedType.LPStr)] string libName, int numb);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SpcDescriptionRemove([MarshalAs(UnmanagedType.LPStr)] string libName, int numb);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SpcDescriptionBeginEdit([MarshalAs(UnmanagedType.LPStr)] string libName, int numb);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SpcDescriptionEdit([MarshalAs(UnmanagedType.LPStr)] string libName, int numb, bool isOk);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SynchronizationBegin();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Synchronization();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginCalcPositions();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CalcPositions();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginCreateObject(int typeObj);
//}
