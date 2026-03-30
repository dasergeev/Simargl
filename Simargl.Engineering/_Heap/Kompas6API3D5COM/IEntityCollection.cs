//// Decompiled with JetBrains decompiler
//// Type: Kompas6API3D5COM.IEntityCollection
//// Assembly: Kompas6API3D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 98DBB410-35A6-4482-8352-058793489E25
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API3D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API3D5COM;

//[Guid("7AA0E540-0303-11D4-A30E-00C026EE094F")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface IEntityCollection
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int Refresh();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetCount();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IEntity First();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IEntity Last();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IEntity Next();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IEntity Prev();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IEntity GetByName([MarshalAs(UnmanagedType.LPWStr)] string name, int testFullName, int testIgnoreCase);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IEntity GetByIndex(int index);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SelectByPoint(double x, double y, double z);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int Add([MarshalAs(UnmanagedType.Interface)] IEntity entity);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddAt([MarshalAs(UnmanagedType.Interface)] IEntity entity, int index);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddBefore([MarshalAs(UnmanagedType.Interface)] IEntity entity, [MarshalAs(UnmanagedType.Interface)] IEntity @base);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int DetachByIndex(int index);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int DetachByBody([MarshalAs(UnmanagedType.Interface)] IEntity entity);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int Clear();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetByIndex([MarshalAs(UnmanagedType.Interface)] IEntity entity, int index);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  uint FindIt([MarshalAs(UnmanagedType.Interface)] IEntity entity);
//}
