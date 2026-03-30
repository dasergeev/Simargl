//// Decompiled with JetBrains decompiler
//// Type: KompasLibrary.ksKompasLibrary
//// Assembly: KompasLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: D93B1318-9A0B-4DD9-9B08-EF0E46C31A2E
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasLibrary.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasLibrary;

//[Guid("C222614E-AB59-4FEE-9F4A-1EAB7C1D4C5E")]
//[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
//[TypeLibType(TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ksKompasLibrary
//{
//  [DispId(16001)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetLibraryName();

//  [DispId(16002)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string DisplayLibraryName();

//  [DispId(16003)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetHelpFile();

//  [DispId(16004)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  short GetProtectNumber();

//  [DispId(16005)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void ExternalRunCommand([In] short Command, [In] short mode, [MarshalAs(UnmanagedType.IDispatch)] object ApplicationInterface);

//  [DispId(16006)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool IsOnApplication7();

//  [DispId(16007)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool LibInterfaceNotifyEntry([MarshalAs(UnmanagedType.IDispatch), In] object ApplicationInterface);

//  [DispId(16008)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool LibInterfaceNotifyDisconnect();

//  [DispId(16009)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: ComAliasName("stdole.OLE_HANDLE")]
//  int ExternalGetMenu();

//  [DispId(16010)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string ExternalMenuItem([In] short Index, out short ItemType, out short Command);

//  [DispId(16011)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object GetIKompasConverter();

//  [DispId(16012)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool LibraryCommandState([In] int Command, out bool Enable, out int Checked);

//  [DispId(16013)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetDisableReason([In] int Command);

//  [DispId(16014)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool FillContextPanel([MarshalAs(UnmanagedType.IDispatch), In] object ContextPanel);

//  [DispId(16015)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ContextPanelStyleComboChanged([MarshalAs(UnmanagedType.BStr), In] string StyleComboID, [In] int styleType, [In] int newValue);

//  [DispId(16016)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CreateMacroFromSample([In] int MacroReference);
//}
