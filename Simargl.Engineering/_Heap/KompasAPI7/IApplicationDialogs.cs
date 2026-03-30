//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IApplicationDialogs
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("C825C801-D6D3-4456-BD37-D48ED799E033")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IApplicationDialogs
//{
//  [DispId(2001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IKompasAPIObject GetDialogParam([In] KompasAPIObjectTypeEnum ParamType);

//  [DispId(2002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SelectThread([ComAliasName("stdole.OLE_HANDLE"), In] int ParentHwnd, [MarshalAs(UnmanagedType.Interface), In] ThreadDialogParam DialogParam);

//  [DispId(2003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ShowContentDialog([ComAliasName("stdole.OLE_HANDLE"), In] int ParentHwnd, [MarshalAs(UnmanagedType.Interface), In] ContentDialogParam DialogParam);

//  [DispId(2004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int WhatsWrongDlg([MarshalAs(UnmanagedType.Struct), In] object Objs);
//}
