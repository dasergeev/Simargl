//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksKompasObjectNotify
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[Guid("C7CB743A-C59D-4C27-8CB6-971C2A393F2F")]
//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ksKompasObjectNotify
//{
//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CreateDocument([MarshalAs(UnmanagedType.IDispatch), In] object newDoc, [In] int docType);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginOpenDocument([MarshalAs(UnmanagedType.BStr), In] string fileName);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool OpenDocument([MarshalAs(UnmanagedType.IDispatch), In] object newDoc, [In] int docType);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeActiveDocument([MarshalAs(UnmanagedType.IDispatch), In] object newDoc, [In] int docType);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ApplicationDestroy();

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginCreate([In] int docType);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginOpenFile();

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginCloseAllDocument();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool KeyDown([In, Out] ref int key, [In] int flags, [In] bool systemKey);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool KeyUp([In, Out] ref int key, [In] int flags, [In] bool systemKey);

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool KeyPress([In, Out] ref int key, [In] bool systemKey);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginReguestFiles([In] int requestID, [MarshalAs(UnmanagedType.Struct), In, Out] ref object files);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginChoiceMaterial([In] int MaterialPropertyId);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChoiceMaterial([In] int MaterialPropertyId, [MarshalAs(UnmanagedType.BStr), In] string material, [In] double density);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool IsNeedConvertToSavePrevious(
//    [MarshalAs(UnmanagedType.IDispatch), In] object pDoc,
//    [In] int docType,
//    [In] int saveVersion,
//    [MarshalAs(UnmanagedType.IDispatch), In] object saveToPreviusParam,
//    [In, Out] ref bool needConvert);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginConvertToSavePrevious(
//    [MarshalAs(UnmanagedType.IDispatch), In] object pDoc,
//    [In] int docType,
//    [In] int saveVersion,
//    [MarshalAs(UnmanagedType.IDispatch), In] object saveToPreviusParam);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EndConvertToSavePrevious(
//    [MarshalAs(UnmanagedType.IDispatch), In] object pDoc,
//    [In] int docType,
//    [In] int saveVersion,
//    [MarshalAs(UnmanagedType.IDispatch), In] object saveToPreviusParam);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeTheme([In] int newTheme);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginDragOpenFiles([MarshalAs(UnmanagedType.Struct), In, Out] ref object filesList, [In] bool insert, [In, Out] ref bool filesListChanged);
//}
