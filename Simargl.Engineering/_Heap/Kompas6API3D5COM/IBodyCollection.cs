//// Decompiled with JetBrains decompiler
//// Type: Kompas6API3D5COM.IBodyCollection
//// Assembly: Kompas6API3D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 98DBB410-35A6-4482-8352-058793489E25
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API3D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API3D5COM;

//[Guid("64CBC7CB-005D-47DF-8B3E-53FD974C5A32")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface IBodyCollection
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int Refresh();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetCount();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IBody First();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IBody Last();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IBody Next();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IBody Prev();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IBody GetByIndex(int index);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  uint FindIt([MarshalAs(UnmanagedType.Interface)] IBody entity);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int Clear();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int Add([MarshalAs(UnmanagedType.Interface)] IBody entity);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddAt([MarshalAs(UnmanagedType.Interface)] IBody entity, int index);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddBefore([MarshalAs(UnmanagedType.Interface)] IBody entity, [MarshalAs(UnmanagedType.Interface)] IBody @base);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int DetachByIndex(int index);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int DetachByBody([MarshalAs(UnmanagedType.Interface)] IBody entity);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetByIndex([MarshalAs(UnmanagedType.Interface)] IBody entity, int index);
//}
