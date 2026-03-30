//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IPropertyToolBar
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("C2523B7E-EB4B-45DB-8E3B-9D6CCED99333")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IPropertyToolBar
//{
//  [DispId(501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ButtonChecked([In] int BtnID, [In] bool PVal = false);

//  [DispId(501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_ButtonChecked([In] int BtnID);

//  [DispId(502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ButtonEnable([In] int BtnID, [In] bool PVal = true);

//  [DispId(502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_ButtonEnable([In] int BtnID);

//  [DispId(503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void AddButton([In] int BtnID, [MarshalAs(UnmanagedType.Struct), In] object Bmp, [In] int InsertAt = -1);

//  [DispId(504)]
//  object ResModule { [DispId(504), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Struct), In] set; [DispId(504), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ButtonTips([In] int BtnID, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_ButtonTips([In] int BtnID);

//  [DispId(506)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ButtonHint([In] int BtnID, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(506)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_ButtonHint([In] int BtnID);

//  [DispId(507)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ButtonType([In] int BtnID, [In] ButtonTypeEnum PVal);

//  [DispId(507)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  ButtonTypeEnum get_ButtonType([In] int BtnID);

//  [DispId(508)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ButtonVisible([In] int BtnID, [In] bool PVal = true);

//  [DispId(508)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_ButtonVisible([In] int BtnID);

//  [DispId(509)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ButtonIconFont([In] int BtnID, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(509)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_ButtonIconFont([In] int BtnID);
//}
