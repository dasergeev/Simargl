//// Decompiled with JetBrains decompiler
//// Type: Kompas6API3D5COM.ICutLoftDefinition
//// Assembly: Kompas6API3D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 98DBB410-35A6-4482-8352-058793489E25
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API3D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API3D5COM;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[Guid("7AA0E540-0363-11D4-A30E-00C026EE094F")]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface ICutLoftDefinition
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetLoftParam(ref int closed, ref int flipVertex, ref int autoPath);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void SetLoftParam(int closed, int flipVertex, int autoPath);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetThinParam(
//    ref int thin,
//    ref byte thinType,
//    ref double normalThickness,
//    ref double reverseThickness);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void SetThinParam(int thin, byte thinType, double normalThickness, double reverseThickness);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IThinParam ThinParam();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IEntityCollection Sketchs();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetCut();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetCut(int cut);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IChooseBodies ChooseBodies();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IEntity GetDirectionalLine();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetDirectionalLine([MarshalAs(UnmanagedType.Interface)] IEntity sketch);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IChooseParts ChooseParts();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetChooseType(int val);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetChooseType();
//}
