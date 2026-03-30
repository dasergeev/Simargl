//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IPropertyControl1
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("7D2A79FF-EC54-4480-B9F3-46F75293B558")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IPropertyControl1
//{
//  [DispId(10)]
//  int PredefineNumber { [DispId(10), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(10), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [ComAliasName("stdole.OLE_HANDLE")]
//  [DispId(11)]
//  int UserMenu { [DispId(11), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: ComAliasName("stdole.OLE_HANDLE"), In] set; [DispId(11), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: ComAliasName("stdole.OLE_HANDLE")] get; }

//  [DispId(12)]
//  object ResModule { [DispId(12), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Struct), In] set; [DispId(12), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(13)]
//  object Image { [DispId(13), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Struct), In] set; [DispId(13), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: ComAliasName("stdole.OLE_HANDLE")]
//  int GetHWND();

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddAdditionButton([In] int BtnID, [MarshalAs(UnmanagedType.Struct), In] object Bmp, [MarshalAs(UnmanagedType.BStr), In] string Tips, [MarshalAs(UnmanagedType.BStr), In] string IconFont);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddAdditionCheckButton(
//    [In] int BtnID,
//    [MarshalAs(UnmanagedType.Struct), In] object BmpChecked,
//    [MarshalAs(UnmanagedType.Struct), In] object BmpUnChecked,
//    [MarshalAs(UnmanagedType.Struct), In] object BmpUndefine,
//    [MarshalAs(UnmanagedType.BStr), In] string Tips,
//    [MarshalAs(UnmanagedType.BStr), In] string IconFont);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_AdditionButtonVisible([In] int BtnID, [In] bool Visible);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_AdditionButtonVisible([In] int BtnID);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_AdditionButtonChecked([In] int BtnID, [In] bool Checked);

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_AdditionButtonChecked([In] int BtnID);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_AdditionButtonEnable([In] int BtnID, [In] bool Enable);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_AdditionButtonEnable([In] int BtnID);

//  [DispId(20)]
//  bool HyperLinkNameStyle { [DispId(20), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(20), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(21)]
//  bool NeedMouseEnterLeaveMessages { [DispId(21), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(21), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_AdditionButtonNeedMouseEnterLeaveMessages([In] int BtnID, [In] bool PVal);

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_AdditionButtonNeedMouseEnterLeaveMessages([In] int BtnID);
//}
