//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.FragmentLibClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[Guid("D06C910C-98CA-11D6-8732-00C0262CDD2C")]
//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[ComImport]
//public class FragmentLibClass : ksFragmentLibrary, FragmentLib
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern FragmentLibClass();

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksChoiceFragmentFromLib([MarshalAs(UnmanagedType.BStr), In] string frwLibFile, out int type);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksFragmentLibraryOperation([MarshalAs(UnmanagedType.BStr)] string libName, int type);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAddFragmentToLibrary([MarshalAs(UnmanagedType.BStr)] string libName, [MarshalAs(UnmanagedType.BStr)] string frwName);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCheckFragmentLibrary([MarshalAs(UnmanagedType.BStr)] string libName, bool possibleMessage);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExistFragmentInLibrary([MarshalAs(UnmanagedType.BStr)] string frwName);
//}
