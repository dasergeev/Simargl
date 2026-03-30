//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksPropertyManagerNotify_Event
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ComVisible(false)]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ComEventInterface(typeof (ksPropertyManagerNotify), typeof (ksPropertyManagerNotify_EventProvider))]
//public interface ksPropertyManagerNotify_Event
//{
//  event ksPropertyManagerNotify_ButtonClickEventHandler ButtonClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ButtonClick(
//    [In] ksPropertyManagerNotify_ButtonClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ButtonClick(
//    [In] ksPropertyManagerNotify_ButtonClickEventHandler obj0);

//  event ksPropertyManagerNotify_ChangeControlValueEventHandler ChangeControlValue;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeControlValue(
//    [In] ksPropertyManagerNotify_ChangeControlValueEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeControlValue(
//    [In] ksPropertyManagerNotify_ChangeControlValueEventHandler obj0);

//  event ksPropertyManagerNotify_ControlCommandEventHandler ControlCommand;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ControlCommand(
//    [In] ksPropertyManagerNotify_ControlCommandEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ControlCommand(
//    [In] ksPropertyManagerNotify_ControlCommandEventHandler obj0);

//  event ksPropertyManagerNotify_ButtonUpdateEventHandler ButtonUpdate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ButtonUpdate(
//    [In] ksPropertyManagerNotify_ButtonUpdateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ButtonUpdate(
//    [In] ksPropertyManagerNotify_ButtonUpdateEventHandler obj0);

//  event ksPropertyManagerNotify_ProcessActivateEventHandler ProcessActivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ProcessActivate(
//    [In] ksPropertyManagerNotify_ProcessActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ProcessActivate(
//    [In] ksPropertyManagerNotify_ProcessActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ProcessDeactivate(
//    [In] ksPropertyManagerNotify_ProcessDeactivateEventHandler obj0);

//  event ksPropertyManagerNotify_ProcessDeactivateEventHandler ProcessDeactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ProcessDeactivate(
//    [In] ksPropertyManagerNotify_ProcessDeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CommandHelp(
//    [In] ksPropertyManagerNotify_CommandHelpEventHandler obj0);

//  event ksPropertyManagerNotify_CommandHelpEventHandler CommandHelp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CommandHelp(
//    [In] ksPropertyManagerNotify_CommandHelpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_SelectItem(
//    [In] ksPropertyManagerNotify_SelectItemEventHandler obj0);

//  event ksPropertyManagerNotify_SelectItemEventHandler SelectItem;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_SelectItem(
//    [In] ksPropertyManagerNotify_SelectItemEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_CheckItem([In] ksPropertyManagerNotify_CheckItemEventHandler obj0);

//  event ksPropertyManagerNotify_CheckItemEventHandler CheckItem;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_CheckItem([In] ksPropertyManagerNotify_CheckItemEventHandler obj0);

//  event ksPropertyManagerNotify_ChangeActiveTabEventHandler ChangeActiveTab;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeActiveTab(
//    [In] ksPropertyManagerNotify_ChangeActiveTabEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeActiveTab(
//    [In] ksPropertyManagerNotify_ChangeActiveTabEventHandler obj0);

//  event ksPropertyManagerNotify_EditFocusEventHandler EditFocus;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EditFocus([In] ksPropertyManagerNotify_EditFocusEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EditFocus([In] ksPropertyManagerNotify_EditFocusEventHandler obj0);

//  event ksPropertyManagerNotify_UserMenuCommandEventHandler UserMenuCommand;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_UserMenuCommand(
//    [In] ksPropertyManagerNotify_UserMenuCommandEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_UserMenuCommand(
//    [In] ksPropertyManagerNotify_UserMenuCommandEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_LayoutChanged(
//    [In] ksPropertyManagerNotify_LayoutChangedEventHandler obj0);

//  event ksPropertyManagerNotify_LayoutChangedEventHandler LayoutChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_LayoutChanged(
//    [In] ksPropertyManagerNotify_LayoutChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_GetContextMenuType(
//    [In] ksPropertyManagerNotify_GetContextMenuTypeEventHandler obj0);

//  event ksPropertyManagerNotify_GetContextMenuTypeEventHandler GetContextMenuType;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_GetContextMenuType(
//    [In] ksPropertyManagerNotify_GetContextMenuTypeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_FillContextPanel(
//    [In] ksPropertyManagerNotify_FillContextPanelEventHandler obj0);

//  event ksPropertyManagerNotify_FillContextPanelEventHandler FillContextPanel;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_FillContextPanel(
//    [In] ksPropertyManagerNotify_FillContextPanelEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_FillContextIconMenu(
//    [In] ksPropertyManagerNotify_FillContextIconMenuEventHandler obj0);

//  event ksPropertyManagerNotify_FillContextIconMenuEventHandler FillContextIconMenu;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_FillContextIconMenu(
//    [In] ksPropertyManagerNotify_FillContextIconMenuEventHandler obj0);

//  event ksPropertyManagerNotify_EndEditItemEventHandler EndEditItem;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_EndEditItem(
//    [In] ksPropertyManagerNotify_EndEditItemEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_EndEditItem(
//    [In] ksPropertyManagerNotify_EndEditItemEventHandler obj0);

//  event ksPropertyManagerNotify_ChangeTabExpandedEventHandler ChangeTabExpanded;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_ChangeTabExpanded(
//    [In] ksPropertyManagerNotify_ChangeTabExpandedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_ChangeTabExpanded(
//    [In] ksPropertyManagerNotify_ChangeTabExpandedEventHandler obj0);

//  event ksPropertyManagerNotify_DoubleClickItemEventHandler DoubleClickItem;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void add_DoubleClickItem(
//    [In] ksPropertyManagerNotify_DoubleClickItemEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void remove_DoubleClickItem(
//    [In] ksPropertyManagerNotify_DoubleClickItemEventHandler obj0);
//}
