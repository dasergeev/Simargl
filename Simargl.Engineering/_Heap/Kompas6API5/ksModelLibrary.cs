//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksModelLibrary
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[Guid("111CEFE4-A0A7-11D6-95CE-00C0262D30E3")]
//[ComImport]
//public interface ksModelLibrary
//{
//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ModelLibraryOperation([MarshalAs(UnmanagedType.BStr)] string libName, int type);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string ChoiceModelFromLib([MarshalAs(UnmanagedType.BStr), In] string libFile, out int type);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ExistModelInLibrary([MarshalAs(UnmanagedType.BStr)] string name);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddD3DocumentToLibrary([MarshalAs(UnmanagedType.BStr)] string libName, [MarshalAs(UnmanagedType.BStr)] string fileName);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int CheckModelLibrary([MarshalAs(UnmanagedType.BStr)] string libName, bool possibleMessage);
//}
