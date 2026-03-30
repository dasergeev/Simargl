//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.PropertyManagerClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[ComSourceInterfaces("KompasAPI7.ksPropertyManagerNotify\0\0")]
//[Guid("1B9CBAB1-99DA-433E-8D4E-6761D1AB9B8A")]
//[ComImport]
//public class PropertyManagerClass : 
//  IPropertyManager,
//  PropertyManager,
//  ksPropertyManagerNotify_Event,
//  IKompasAPIObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern PropertyManagerClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1)]
//  public virtual extern bool Visible { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2)]
//  public virtual extern string Caption { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(3)]
//  public virtual extern PropertyManagerLayout Layout { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(4)]
//  public virtual extern PropertyTabs PropertyTabs { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void SetGabaritRect([In] int Left, [In] int Top, [In] int Right, [In] int Bottom);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void GetGabaritRect(
//    out int Left,
//    out int Top,
//    out int Right,
//    out int Bottom);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ShowTabs();

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool UpdateTabs();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool HideTabs();

//  [DispId(10)]
//  public virtual extern SpecPropertyToolBarEnum SpecToolbar { [DispId(10), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(10), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool RepeatCommand();

//  [DispId(12)]
//  public virtual extern bool AutoHideMode { [DispId(12), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(12), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(13)]
//  public virtual extern object ResModule { [DispId(13), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Struct), In] set; [DispId(13), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(14)]
//  public virtual extern int SpecToolbarEx { [DispId(14), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(14), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool AddSpecToolbarButton(
//    [In] int BtnID,
//    [MarshalAs(UnmanagedType.Struct), In] object Bmp,
//    [MarshalAs(UnmanagedType.BStr), In] string Tips,
//    [MarshalAs(UnmanagedType.BStr), In] string IconFont);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool AddSetupMenuCommand([MarshalAs(UnmanagedType.BStr), In] string Title, [In] int Command, [In] bool Checable);

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetSetupMenuCommandState(
//    [In] int Command,
//    [In] bool Visible,
//    [In] bool Enable,
//    [In] bool Checked);

//  [DispId(18)]
//  public virtual extern ksEnterButtonIconTypeEnum EnterButtonIconType { [DispId(18), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(18), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(19)]
//  public virtual extern string StatusMessage { [DispId(19), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; [DispId(19), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(20)]
//  public virtual extern string Label { [DispId(20), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; [DispId(20), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ClearSpecToolbar();

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetIcon([In] int Icon, [MarshalAs(UnmanagedType.BStr), In] string IconFont);

//  [DispId(23)]
//  public virtual extern bool ActivateOnCreate { [DispId(23), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(23), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(24)]
//  public virtual extern bool Active { [DispId(24), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(24), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern event ksPropertyManagerNotify_ButtonClickEventHandler ButtonClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ButtonClick(
//    [In] ksPropertyManagerNotify_ButtonClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ButtonClick(
//    [In] ksPropertyManagerNotify_ButtonClickEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_ChangeControlValueEventHandler ChangeControlValue;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeControlValue(
//    [In] ksPropertyManagerNotify_ChangeControlValueEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeControlValue(
//    [In] ksPropertyManagerNotify_ChangeControlValueEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ControlCommand(
//    [In] ksPropertyManagerNotify_ControlCommandEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_ControlCommandEventHandler ControlCommand;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ControlCommand(
//    [In] ksPropertyManagerNotify_ControlCommandEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_ButtonUpdateEventHandler ButtonUpdate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ButtonUpdate(
//    [In] ksPropertyManagerNotify_ButtonUpdateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ButtonUpdate(
//    [In] ksPropertyManagerNotify_ButtonUpdateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ProcessActivate(
//    [In] ksPropertyManagerNotify_ProcessActivateEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_ProcessActivateEventHandler ProcessActivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ProcessActivate(
//    [In] ksPropertyManagerNotify_ProcessActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ProcessDeactivate(
//    [In] ksPropertyManagerNotify_ProcessDeactivateEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_ProcessDeactivateEventHandler ProcessDeactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ProcessDeactivate(
//    [In] ksPropertyManagerNotify_ProcessDeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CommandHelp(
//    [In] ksPropertyManagerNotify_CommandHelpEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_CommandHelpEventHandler CommandHelp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CommandHelp(
//    [In] ksPropertyManagerNotify_CommandHelpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SelectItem(
//    [In] ksPropertyManagerNotify_SelectItemEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_SelectItemEventHandler SelectItem;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SelectItem(
//    [In] ksPropertyManagerNotify_SelectItemEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CheckItem([In] ksPropertyManagerNotify_CheckItemEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_CheckItemEventHandler CheckItem;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CheckItem([In] ksPropertyManagerNotify_CheckItemEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeActiveTab(
//    [In] ksPropertyManagerNotify_ChangeActiveTabEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_ChangeActiveTabEventHandler ChangeActiveTab;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeActiveTab(
//    [In] ksPropertyManagerNotify_ChangeActiveTabEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EditFocus([In] ksPropertyManagerNotify_EditFocusEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_EditFocusEventHandler EditFocus;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EditFocus([In] ksPropertyManagerNotify_EditFocusEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_UserMenuCommand(
//    [In] ksPropertyManagerNotify_UserMenuCommandEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_UserMenuCommandEventHandler UserMenuCommand;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_UserMenuCommand(
//    [In] ksPropertyManagerNotify_UserMenuCommandEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_LayoutChangedEventHandler LayoutChanged;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_LayoutChanged(
//    [In] ksPropertyManagerNotify_LayoutChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_LayoutChanged(
//    [In] ksPropertyManagerNotify_LayoutChangedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_GetContextMenuType(
//    [In] ksPropertyManagerNotify_GetContextMenuTypeEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_GetContextMenuTypeEventHandler GetContextMenuType;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_GetContextMenuType(
//    [In] ksPropertyManagerNotify_GetContextMenuTypeEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_FillContextPanelEventHandler FillContextPanel;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_FillContextPanel(
//    [In] ksPropertyManagerNotify_FillContextPanelEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_FillContextPanel(
//    [In] ksPropertyManagerNotify_FillContextPanelEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_FillContextIconMenu(
//    [In] ksPropertyManagerNotify_FillContextIconMenuEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_FillContextIconMenuEventHandler FillContextIconMenu;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_FillContextIconMenu(
//    [In] ksPropertyManagerNotify_FillContextIconMenuEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_EndEditItemEventHandler EndEditItem;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndEditItem(
//    [In] ksPropertyManagerNotify_EndEditItemEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndEditItem(
//    [In] ksPropertyManagerNotify_EndEditItemEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeTabExpanded(
//    [In] ksPropertyManagerNotify_ChangeTabExpandedEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_ChangeTabExpandedEventHandler ChangeTabExpanded;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeTabExpanded(
//    [In] ksPropertyManagerNotify_ChangeTabExpandedEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DoubleClickItem(
//    [In] ksPropertyManagerNotify_DoubleClickItemEventHandler obj0);

//  public virtual extern event ksPropertyManagerNotify_DoubleClickItemEventHandler DoubleClickItem;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DoubleClickItem(
//    [In] ksPropertyManagerNotify_DoubleClickItemEventHandler obj0);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
