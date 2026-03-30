//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksSpecificationObjectNotify
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[Guid("1C4DEC41-A8EA-40EE-9AC8-F807232DB874")]
//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ksSpecificationObjectNotify
//{
//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginDelete([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Delete([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CellDblClick([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj, [In] int Number);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CellBeginEdit([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj, [In] int Number);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeCurrent([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DocumentBeginAdd([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DocumentAdd([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj, [MarshalAs(UnmanagedType.BStr), In] string DocName);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DocumentRemove([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj, [MarshalAs(UnmanagedType.BStr), In] string DocName);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginGeomChange([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GeomChange([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginProcess([In] int PType, [MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EndProcess([In] int PType);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CreateObject([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool UpdateObject([MarshalAs(UnmanagedType.Interface), In] ISpecificationObject Obj);
//}
