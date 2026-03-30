//// Decompiled with JetBrains decompiler
//// Type: Kompas6API2D5COM.IPropertyManagerNotify
//// Assembly: Kompas6API2D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: D8B7A040-4B5D-44BB-964F-4529697D4B2C
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API2D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API2D5COM;

//[Guid("9AB42E3B-7437-407E-960A-4F509812AB11")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface IPropertyManagerNotify : IKompasNotify
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  new bool IsNotifyProcess(int notifyType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ButtonClick(int buttonID);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeControlValue([MarshalAs(UnmanagedType.IUnknown)] object ctrl);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ControlCommand([MarshalAs(UnmanagedType.IUnknown)] object ctrl, int buttonID);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ButtonUpdate([In] int buttonID, [In, Out] ref int check, [In, Out] ref bool _enable);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ProcessActivate();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ProcessDeactivate();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CommandHelp(int Id);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SelectItem([MarshalAs(UnmanagedType.IUnknown)] object Control, int Index, bool Select);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CheckItem([MarshalAs(UnmanagedType.IUnknown)] object Control, int Index, bool check);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeActiveTab(int TabIndex);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EditFocus([MarshalAs(UnmanagedType.IUnknown)] object ctrl, bool Set);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool UserMenuCommand([MarshalAs(UnmanagedType.IUnknown)] object ctrl, int menuID);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool LayoutChanged();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetContextMenuType([In] int LX, [In] int LY, [In, Out] ref int ContextMenuType);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool FillContextPanel([MarshalAs(UnmanagedType.IUnknown)] object ContextPanel);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool FillContextIconMenu([MarshalAs(UnmanagedType.IUnknown)] object menu);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EndEditItem([MarshalAs(UnmanagedType.IUnknown)] object ctrl, int Index);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeTabExpanded(int TabIndex);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DoubleClickItem([MarshalAs(UnmanagedType.IUnknown)] object Control, int Index);
//}
