//// Decompiled with JetBrains decompiler
//// Type: KompasLibrary.IKompasLibrary
//// Assembly: KompasLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: D93B1318-9A0B-4DD9-9B08-EF0E46C31A2E
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasLibrary.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasLibrary;

//[Guid("025A21B0-0192-4A7C-A3F0-CA54AAA4FADB")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IKompasLibrary
//{
//  [DispId(17001)]
//  int Version { [DispId(17001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(17002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool get_IsFunctionEnable([In] ksKompasLibraryFunctionEnum FunctionID);

//  [DispId(17003)]
//  string LibraryName { [DispId(17003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(17004)]
//  string DisplayLibraryName { [DispId(17004), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(17005)]
//  string LibraryHelpFile { [DispId(17005), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(17006)]
//  int ProtectNumber { [DispId(17006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(17007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int RunLibraryCommand([In] int Command, [In] int DemoMode);

//  [DispId(17008)]
//  bool IsOnApplication7 { [DispId(17008), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(17009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool InitLibrary([MarshalAs(UnmanagedType.IDispatch), In] object ApplicationInterface);

//  [DispId(17010)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool BeginUnloadLibrary();

//  [DispId(17011)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool FillLibraryMenu([MarshalAs(UnmanagedType.Interface), In] IKompasLibraryMenu Menu);

//  [DispId(17012)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetLibraryCommandState([In] int Command, out bool Enable, out int Checked);

//  [DispId(17013)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetDisableReason([In] int Command);

//  [DispId(17014)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool FillContextPanel([MarshalAs(UnmanagedType.IDispatch), In] object ContextPanel);

//  [DispId(17015)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ContextPanelStyleComboChanged([MarshalAs(UnmanagedType.BStr), In] string StyleComboID, [In] int styleType, [In] int newValue);

//  [DispId(17016)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  object GetKompasConverter();

//  [DispId(17017)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CreateMacroFromSample([In] int MacroReference);
//}
