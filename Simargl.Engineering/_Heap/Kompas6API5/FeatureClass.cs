//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.FeatureClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[Guid("1978BA1C-EE2F-48ED-86D7-B15065B36E4A")]
//[ClassInterface(ClassInterfaceType.None)]
//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[ComImport]
//public class FeatureClass : ksFeature, Feature
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern FeatureClass();

//  [DispId(9)]
//  public virtual extern bool excluded { [DispId(9), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(9), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(1)]
//  public virtual extern string name { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(1), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(2)]
//  public virtual extern uint updateStamp { [DispId(2), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsModified(bool recursive);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object SubFeatureCollection(bool through, bool libObject);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetOwnerFeature();

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsValid();

//  [DispId(7)]
//  public virtual extern short type { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(7), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetObject();

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Attribute3DCollection AttributeCollection(
//    int key1,
//    int key2,
//    int key3,
//    int key4,
//    double numb);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Kompas6API5.EntityCollection EntityCollection(short objType);

//  [DispId(12)]
//  public virtual extern object VariableCollection { [DispId(12), TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object BodyCollection();

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Attribute3DCollection GetAttributeCollectionInSource(
//    int key1,
//    int key2,
//    int key3,
//    int key4,
//    double numb,
//    [MarshalAs(UnmanagedType.Interface)] part sourcePart);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsRollBacked();

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object VariableCollectionEx(bool sourse);

//  [DispId(17)]
//  public virtual extern int objectError { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(17), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
