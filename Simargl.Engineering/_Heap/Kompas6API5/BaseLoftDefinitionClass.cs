//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.BaseLoftDefinitionClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[Guid("DEEFEFEC-C3E2-11D6-8734-00C0262CDD2C")]
//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[ComImport]
//public class BaseLoftDefinitionClass : ksBaseLoftDefinition, BaseLoftDefinition
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern BaseLoftDefinitionClass();

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetLoftParam(out bool closed, out bool flipVertex, out bool autoPath);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetLoftParam(bool closed, bool flipVertex, bool autoPath);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetThinParam(
//    out bool thin,
//    out short thinType,
//    out double normalThickness,
//    out double reverseTthickness);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetThinParam(
//    bool thin,
//    short thinType = 0,
//    double normalThickness = 1.0,
//    double reverseThickness = 1.0);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ThinParam();

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object Sketchs();
//}
