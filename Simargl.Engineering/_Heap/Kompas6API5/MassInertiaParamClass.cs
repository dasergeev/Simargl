//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.MassInertiaParamClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ClassInterface(ClassInterfaceType.None)]
//[Guid("4693323B-42A7-42CC-902E-7123DD916FB4")]
//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[ComImport]
//public class MassInertiaParamClass : ksMassInertiaParam, MassInertiaParam
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern MassInertiaParamClass();

//  [DispId(1)]
//  public virtual extern double xc { [DispId(1), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2)]
//  public virtual extern double yc { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(2), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3)]
//  public virtual extern double zc { [DispId(3), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(4)]
//  public virtual extern double lx { [DispId(4), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5)]
//  public virtual extern double ly { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(5), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(6)]
//  public virtual extern double lz { [DispId(6), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(7)]
//  public virtual extern double jx { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(7), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(8)]
//  public virtual extern double jy { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(8), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(9)]
//  public virtual extern double jz { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(9), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(10)]
//  public virtual extern double jxy { [DispId(10), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(11)]
//  public virtual extern double jxz { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(11), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(12)]
//  public virtual extern double jyz { [DispId(12), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(13)]
//  public virtual extern double jx0z { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(13), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(14)]
//  public virtual extern double jy0z { [DispId(14), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(15)]
//  public virtual extern double jx0y { [DispId(15), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(16 /*0x10*/)]
//  public virtual extern double lxy { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(16 /*0x10*/), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(17)]
//  public virtual extern double lxz { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(17), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(18)]
//  public virtual extern double lyz { [DispId(18), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(19)]
//  public virtual extern double r { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(19), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(20)]
//  public virtual extern double m { [DispId(20), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(21)]
//  public virtual extern double v { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(21), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetBitVectorValue(int val, bool setState);

//  [DispId(23)]
//  public virtual extern double F { [DispId(23), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(24)]
//  public virtual extern double jx0 { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(24), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(25)]
//  public virtual extern double jy0 { [DispId(25), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(26)]
//  public virtual extern double jz0 { [DispId(26), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetAxisX(out double x, out double y, out double z);

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetAxisY(out double x, out double y, out double z);

//  [DispId(29)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetAxisZ(out double x, out double y, out double z);
//}
