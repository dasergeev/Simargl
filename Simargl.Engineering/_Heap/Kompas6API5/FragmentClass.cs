//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.FragmentClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[Guid("D06C9106-98CA-11D6-8732-00C0262CDD2C")]
//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[ComImport]
//public class FragmentClass : ksFragment, Fragment
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern FragmentClass();

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksFragmentDefinition([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.BStr)] string comment, short insertType);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksInsertFragment(int p, bool curentLayer, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReadFragment([MarshalAs(UnmanagedType.BStr)] string fileName, bool curentLayer, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReadFragmentToGroup([MarshalAs(UnmanagedType.BStr)] string fileName, bool curentLayer, [MarshalAs(UnmanagedType.IDispatch)] object par);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReadFragmentToGroupEx(
//    [MarshalAs(UnmanagedType.BStr)] string fileName,
//    bool curentLayer,
//    [MarshalAs(UnmanagedType.IDispatch)] object par,
//    bool scaleProjLinesSize);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksWriteFragment(
//    int gr,
//    [MarshalAs(UnmanagedType.BStr)] string fileName,
//    [MarshalAs(UnmanagedType.BStr)] string comment,
//    double xb,
//    double yb);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksLocalFragmentDefinition([MarshalAs(UnmanagedType.BStr)] string comment);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCloseLocalFragmentDefinition();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksInsertFragmentEx(
//    int p,
//    bool curentLayer,
//    [MarshalAs(UnmanagedType.IDispatch)] object par,
//    bool scaleProjLinesSize);
//}
