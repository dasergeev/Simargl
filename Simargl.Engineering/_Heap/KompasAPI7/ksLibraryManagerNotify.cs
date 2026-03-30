//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksLibraryManagerNotify
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[Guid("9B9CC387-E217-4EED-BCE9-9E1D645B49EE")]
//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ksLibraryManagerNotify
//{
//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginAttach([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Attach([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginDetach([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Detach([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginExecute([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EndExecute([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SystemControlStop([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SystemControlStart([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddLibraryDescription([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteLibraryDescription([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddInsert([MarshalAs(UnmanagedType.Interface), In] Insert PInsert, [In] bool Create);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteInsert([MarshalAs(UnmanagedType.Interface), In] Insert PInsert);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EditInsert([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary, [MarshalAs(UnmanagedType.Interface), In] IKompasDocument PDoc, [In] bool NewFrw);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool TryExecute([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary, [In] int CommandID);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginInsertDocument([MarshalAs(UnmanagedType.Interface), In] ILibrary PLibrary, [In] int InsertionType, [MarshalAs(UnmanagedType.BStr), In] string Insertion);
//}
