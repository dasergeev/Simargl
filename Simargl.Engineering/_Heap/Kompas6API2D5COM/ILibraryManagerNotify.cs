//// Decompiled with JetBrains decompiler
//// Type: Kompas6API2D5COM.ILibraryManagerNotify
//// Assembly: Kompas6API2D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: D8B7A040-4B5D-44BB-964F-4529697D4B2C
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API2D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API2D5COM;

//[Guid("30265782-7631-4957-AF51-458CAA9A76EC")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface ILibraryManagerNotify : IKompasNotify
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool IsNotifyProcess(int notifyType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginAttach([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Attach([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginDetach([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Detach([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginExecute([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EndExecute([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SystemControlStop([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SystemControlStart([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddLibraryDescription([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteLibraryDescription([MarshalAs(UnmanagedType.IUnknown)] object PLibrary);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddInsert([MarshalAs(UnmanagedType.IUnknown)] object PInsert, bool create);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteInsert([MarshalAs(UnmanagedType.IUnknown)] object PInsert);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EditInsert([MarshalAs(UnmanagedType.IUnknown)] object PLibrary, int pDoc, bool newFrw);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool TryExecute([MarshalAs(UnmanagedType.IUnknown)] object PLibrary, int commandId);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginInsertDocument([MarshalAs(UnmanagedType.IUnknown)] object PLibrary, int InsertionType, [MarshalAs(UnmanagedType.BStr)] string Insertion);
//}
