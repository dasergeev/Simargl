//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IContextPanel
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("480A9539-F347-4B46-BDFB-7323AEA5BB37")]
//[ComImport]
//public interface IContextPanel
//{
//  [DispId(10001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Fill([MarshalAs(UnmanagedType.BStr), In] string ContextPanelID);

//  [DispId(10002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool FillComboBoxImage(
//    [MarshalAs(UnmanagedType.BStr), In] string ComboBoxID,
//    [MarshalAs(UnmanagedType.BStr), In] string CommandsGroupID,
//    [MarshalAs(UnmanagedType.Struct), In] object FilterCommands,
//    [In] int CurrentCommand);

//  [DispId(10003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool FillComboBoxStyle(
//    [MarshalAs(UnmanagedType.BStr), In] string ComboBoxID,
//    [In] ControlTypeEnum StyleType,
//    [MarshalAs(UnmanagedType.Struct), In] object Styles,
//    [In] int CurrentStyleId);
//}
