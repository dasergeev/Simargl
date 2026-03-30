//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IFrameTreesManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("B9019350-FF1D-4161-B41B-CD1B020ECF36")]
//[ComImport]
//public interface IFrameTreesManager
//{
//  [DispId(2001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object AddTab([MarshalAs(UnmanagedType.BStr), In] string TabCaption, [MarshalAs(UnmanagedType.BStr), In] string OcxClassID);

//  [DispId(2002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool RemoveTab([MarshalAs(UnmanagedType.IDispatch), In] object Ocx);

//  [DispId(2003)]
//  object ActiveTab { [DispId(2003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.IDispatch)] get; [DispId(2003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.IDispatch), In] set; }

//  [DispId(2004)]
//  bool TabsVisible { [DispId(2004), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(2004), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(2005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_TreeCaption([MarshalAs(UnmanagedType.IDispatch), In] object Ocx);

//  [DispId(2005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_TreeCaption([MarshalAs(UnmanagedType.IDispatch), In] object Ocx, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(2006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object AddTabEx(
//    [MarshalAs(UnmanagedType.BStr), In] string TabCaption,
//    [MarshalAs(UnmanagedType.BStr), In] string TreeCaption,
//    [MarshalAs(UnmanagedType.BStr), In] string OcxClassID,
//    [In] bool Active,
//    [In] PropertyManagerLayout Layout);

//  [DispId(2007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_TabVisible([MarshalAs(UnmanagedType.IDispatch), In] object Ocx);

//  [DispId(2007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_TabVisible([MarshalAs(UnmanagedType.IDispatch), In] object Ocx, [In] bool PVal);
//}
