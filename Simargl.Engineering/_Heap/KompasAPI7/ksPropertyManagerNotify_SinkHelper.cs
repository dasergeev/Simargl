//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksPropertyManagerNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksPropertyManagerNotify_SinkHelper : ksPropertyManagerNotify
//{
//  public ksPropertyManagerNotify_ButtonClickEventHandler m_ButtonClickDelegate;
//  public ksPropertyManagerNotify_ChangeControlValueEventHandler m_ChangeControlValueDelegate;
//  public ksPropertyManagerNotify_ControlCommandEventHandler m_ControlCommandDelegate;
//  public ksPropertyManagerNotify_ButtonUpdateEventHandler m_ButtonUpdateDelegate;
//  public ksPropertyManagerNotify_ProcessActivateEventHandler m_ProcessActivateDelegate;
//  public ksPropertyManagerNotify_ProcessDeactivateEventHandler m_ProcessDeactivateDelegate;
//  public ksPropertyManagerNotify_CommandHelpEventHandler m_CommandHelpDelegate;
//  public ksPropertyManagerNotify_SelectItemEventHandler m_SelectItemDelegate;
//  public ksPropertyManagerNotify_CheckItemEventHandler m_CheckItemDelegate;
//  public ksPropertyManagerNotify_ChangeActiveTabEventHandler m_ChangeActiveTabDelegate;
//  public ksPropertyManagerNotify_EditFocusEventHandler m_EditFocusDelegate;
//  public ksPropertyManagerNotify_UserMenuCommandEventHandler m_UserMenuCommandDelegate;
//  public ksPropertyManagerNotify_LayoutChangedEventHandler m_LayoutChangedDelegate;
//  public ksPropertyManagerNotify_GetContextMenuTypeEventHandler m_GetContextMenuTypeDelegate;
//  public ksPropertyManagerNotify_FillContextPanelEventHandler m_FillContextPanelDelegate;
//  public ksPropertyManagerNotify_FillContextIconMenuEventHandler m_FillContextIconMenuDelegate;
//  public ksPropertyManagerNotify_EndEditItemEventHandler m_EndEditItemDelegate;
//  public ksPropertyManagerNotify_ChangeTabExpandedEventHandler m_ChangeTabExpandedDelegate;
//  public ksPropertyManagerNotify_DoubleClickItemEventHandler m_DoubleClickItemDelegate;
//  public int m_dwCookie;

//  public virtual bool ButtonClick([In] int obj0)
//  {
//    return this.m_ButtonClickDelegate != null && this.m_ButtonClickDelegate(obj0);
//  }

//  public virtual bool ChangeControlValue([In] IPropertyControl obj0)
//  {
//    return this.m_ChangeControlValueDelegate != null && this.m_ChangeControlValueDelegate(obj0);
//  }

//  public virtual bool ControlCommand([In] IPropertyControl obj0, [In] int obj1)
//  {
//    return this.m_ControlCommandDelegate != null && this.m_ControlCommandDelegate(obj0, obj1);
//  }

//  public virtual bool ButtonUpdate([In] int obj0, [In] ref int obj1, [In] ref bool obj2)
//  {
//    return this.m_ButtonUpdateDelegate != null && this.m_ButtonUpdateDelegate(obj0, ref obj1, ref obj2);
//  }

//  public virtual bool ProcessActivate()
//  {
//    return this.m_ProcessActivateDelegate != null && this.m_ProcessActivateDelegate();
//  }

//  public virtual bool ProcessDeactivate()
//  {
//    return this.m_ProcessDeactivateDelegate != null && this.m_ProcessDeactivateDelegate();
//  }

//  public virtual bool CommandHelp([In] int obj0)
//  {
//    return this.m_CommandHelpDelegate != null && this.m_CommandHelpDelegate(obj0);
//  }

//  public virtual bool SelectItem([In] IPropertyControl obj0, [In] int obj1, [In] bool obj2)
//  {
//    return this.m_SelectItemDelegate != null && this.m_SelectItemDelegate(obj0, obj1, obj2);
//  }

//  public virtual bool CheckItem([In] IPropertyControl obj0, [In] int obj1, [In] bool obj2)
//  {
//    return this.m_CheckItemDelegate != null && this.m_CheckItemDelegate(obj0, obj1, obj2);
//  }

//  public virtual bool ChangeActiveTab([In] int obj0)
//  {
//    return this.m_ChangeActiveTabDelegate != null && this.m_ChangeActiveTabDelegate(obj0);
//  }

//  public virtual bool EditFocus([In] IPropertyControl obj0, [In] bool obj1)
//  {
//    return this.m_EditFocusDelegate != null && this.m_EditFocusDelegate(obj0, obj1);
//  }

//  public virtual bool UserMenuCommand([In] IPropertyControl obj0, [In] int obj1)
//  {
//    return this.m_UserMenuCommandDelegate != null && this.m_UserMenuCommandDelegate(obj0, obj1);
//  }

//  public virtual bool LayoutChanged()
//  {
//    return this.m_LayoutChangedDelegate != null && this.m_LayoutChangedDelegate();
//  }

//  public virtual bool GetContextMenuType([In] int obj0, [In] int obj1, [In] ref int obj2)
//  {
//    return this.m_GetContextMenuTypeDelegate != null && this.m_GetContextMenuTypeDelegate(obj0, obj1, ref obj2);
//  }

//  public virtual bool FillContextPanel([In] IProcessContextPanel obj0)
//  {
//    return this.m_FillContextPanelDelegate != null && this.m_FillContextPanelDelegate(obj0);
//  }

//  public virtual bool FillContextIconMenu([In] IProcessContextIconMenu obj0)
//  {
//    return this.m_FillContextIconMenuDelegate != null && this.m_FillContextIconMenuDelegate(obj0);
//  }

//  public virtual bool EndEditItem([In] IPropertyControl obj0, [In] int obj1)
//  {
//    return this.m_EndEditItemDelegate != null && this.m_EndEditItemDelegate(obj0, obj1);
//  }

//  public virtual bool ChangeTabExpanded([In] int obj0)
//  {
//    return this.m_ChangeTabExpandedDelegate != null && this.m_ChangeTabExpandedDelegate(obj0);
//  }

//  public virtual bool DoubleClickItem([In] IPropertyControl obj0, [In] int obj1)
//  {
//    return this.m_DoubleClickItemDelegate != null && this.m_DoubleClickItemDelegate(obj0, obj1);
//  }

//  internal ksPropertyManagerNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_ButtonClickDelegate = (ksPropertyManagerNotify_ButtonClickEventHandler) null;
//    this.m_ChangeControlValueDelegate = (ksPropertyManagerNotify_ChangeControlValueEventHandler) null;
//    this.m_ControlCommandDelegate = (ksPropertyManagerNotify_ControlCommandEventHandler) null;
//    this.m_ButtonUpdateDelegate = (ksPropertyManagerNotify_ButtonUpdateEventHandler) null;
//    this.m_ProcessActivateDelegate = (ksPropertyManagerNotify_ProcessActivateEventHandler) null;
//    this.m_ProcessDeactivateDelegate = (ksPropertyManagerNotify_ProcessDeactivateEventHandler) null;
//    this.m_CommandHelpDelegate = (ksPropertyManagerNotify_CommandHelpEventHandler) null;
//    this.m_SelectItemDelegate = (ksPropertyManagerNotify_SelectItemEventHandler) null;
//    this.m_CheckItemDelegate = (ksPropertyManagerNotify_CheckItemEventHandler) null;
//    this.m_ChangeActiveTabDelegate = (ksPropertyManagerNotify_ChangeActiveTabEventHandler) null;
//    this.m_EditFocusDelegate = (ksPropertyManagerNotify_EditFocusEventHandler) null;
//    this.m_UserMenuCommandDelegate = (ksPropertyManagerNotify_UserMenuCommandEventHandler) null;
//    this.m_LayoutChangedDelegate = (ksPropertyManagerNotify_LayoutChangedEventHandler) null;
//    this.m_GetContextMenuTypeDelegate = (ksPropertyManagerNotify_GetContextMenuTypeEventHandler) null;
//    this.m_FillContextPanelDelegate = (ksPropertyManagerNotify_FillContextPanelEventHandler) null;
//    this.m_FillContextIconMenuDelegate = (ksPropertyManagerNotify_FillContextIconMenuEventHandler) null;
//    this.m_EndEditItemDelegate = (ksPropertyManagerNotify_EndEditItemEventHandler) null;
//    this.m_ChangeTabExpandedDelegate = (ksPropertyManagerNotify_ChangeTabExpandedEventHandler) null;
//    this.m_DoubleClickItemDelegate = (ksPropertyManagerNotify_DoubleClickItemEventHandler) null;
//  }
//}
