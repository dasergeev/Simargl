//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksPropertyManagerNotify_EventProvider
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System;
//using System.Collections;
//using System.Runtime.InteropServices;
//using System.Runtime.InteropServices.ComTypes;
//using System.Threading;

//#nullable disable
//namespace KompasAPI7;

//internal sealed class ksPropertyManagerNotify_EventProvider : 
//  ksPropertyManagerNotify_Event,
//  IDisposable
//{
//  private WeakReference m_wkConnectionPointContainer;
//  private ArrayList m_aEventSinkHelpers;
//  private IConnectionPoint m_ConnectionPoint;

//  private void Init()
//  {
//    IConnectionPoint ppCP = (IConnectionPoint) null;
//    Guid riid = new Guid(new byte[16 /*0x10*/]
//    {
//      (byte) 56,
//      (byte) 197,
//      (byte) 137,
//      (byte) 90,
//      (byte) 221,
//      (byte) 224 /*0xE0*/,
//      (byte) 33,
//      (byte) 76,
//      (byte) 179,
//      (byte) 209,
//      (byte) 59,
//      (byte) 246,
//      (byte) 94,
//      (byte) 235,
//      (byte) 140,
//      (byte) 62
//    });
//    ((IConnectionPointContainer) this.m_wkConnectionPointContainer.Target).FindConnectionPoint(ref riid, out ppCP);
//    this.m_ConnectionPoint = ppCP;
//    this.m_aEventSinkHelpers = new ArrayList();
//  }

//  public virtual void add_ButtonClick(
//    [In] ksPropertyManagerNotify_ButtonClickEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_ButtonClickDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_ButtonClick(
//    [In] ksPropertyManagerNotify_ButtonClickEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_ButtonClickDelegate != null && ((aEventSinkHelper.m_ButtonClickDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_ChangeControlValue(
//    [In] ksPropertyManagerNotify_ChangeControlValueEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_ChangeControlValueDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_ChangeControlValue(
//    [In] ksPropertyManagerNotify_ChangeControlValueEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_ChangeControlValueDelegate != null && ((aEventSinkHelper.m_ChangeControlValueDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_ControlCommand(
//    [In] ksPropertyManagerNotify_ControlCommandEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_ControlCommandDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_ControlCommand(
//    [In] ksPropertyManagerNotify_ControlCommandEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_ControlCommandDelegate != null && ((aEventSinkHelper.m_ControlCommandDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_ButtonUpdate(
//    [In] ksPropertyManagerNotify_ButtonUpdateEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_ButtonUpdateDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_ButtonUpdate(
//    [In] ksPropertyManagerNotify_ButtonUpdateEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_ButtonUpdateDelegate != null && ((aEventSinkHelper.m_ButtonUpdateDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_ProcessActivate(
//    [In] ksPropertyManagerNotify_ProcessActivateEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_ProcessActivateDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_ProcessActivate(
//    [In] ksPropertyManagerNotify_ProcessActivateEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_ProcessActivateDelegate != null && ((aEventSinkHelper.m_ProcessActivateDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_ProcessDeactivate(
//    [In] ksPropertyManagerNotify_ProcessDeactivateEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_ProcessDeactivateDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_ProcessDeactivate(
//    [In] ksPropertyManagerNotify_ProcessDeactivateEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_ProcessDeactivateDelegate != null && ((aEventSinkHelper.m_ProcessDeactivateDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_CommandHelp(
//    [In] ksPropertyManagerNotify_CommandHelpEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_CommandHelpDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_CommandHelp(
//    [In] ksPropertyManagerNotify_CommandHelpEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_CommandHelpDelegate != null && ((aEventSinkHelper.m_CommandHelpDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_SelectItem(
//    [In] ksPropertyManagerNotify_SelectItemEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_SelectItemDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_SelectItem(
//    [In] ksPropertyManagerNotify_SelectItemEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_SelectItemDelegate != null && ((aEventSinkHelper.m_SelectItemDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_CheckItem([In] ksPropertyManagerNotify_CheckItemEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_CheckItemDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_CheckItem([In] ksPropertyManagerNotify_CheckItemEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_CheckItemDelegate != null && ((aEventSinkHelper.m_CheckItemDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_ChangeActiveTab(
//    [In] ksPropertyManagerNotify_ChangeActiveTabEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_ChangeActiveTabDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_ChangeActiveTab(
//    [In] ksPropertyManagerNotify_ChangeActiveTabEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_ChangeActiveTabDelegate != null && ((aEventSinkHelper.m_ChangeActiveTabDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_EditFocus([In] ksPropertyManagerNotify_EditFocusEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_EditFocusDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_EditFocus([In] ksPropertyManagerNotify_EditFocusEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_EditFocusDelegate != null && ((aEventSinkHelper.m_EditFocusDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_UserMenuCommand(
//    [In] ksPropertyManagerNotify_UserMenuCommandEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_UserMenuCommandDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_UserMenuCommand(
//    [In] ksPropertyManagerNotify_UserMenuCommandEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_UserMenuCommandDelegate != null && ((aEventSinkHelper.m_UserMenuCommandDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_LayoutChanged(
//    [In] ksPropertyManagerNotify_LayoutChangedEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_LayoutChangedDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_LayoutChanged(
//    [In] ksPropertyManagerNotify_LayoutChangedEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_LayoutChangedDelegate != null && ((aEventSinkHelper.m_LayoutChangedDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_GetContextMenuType(
//    [In] ksPropertyManagerNotify_GetContextMenuTypeEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_GetContextMenuTypeDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_GetContextMenuType(
//    [In] ksPropertyManagerNotify_GetContextMenuTypeEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_GetContextMenuTypeDelegate != null && ((aEventSinkHelper.m_GetContextMenuTypeDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_FillContextPanel(
//    [In] ksPropertyManagerNotify_FillContextPanelEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_FillContextPanelDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_FillContextPanel(
//    [In] ksPropertyManagerNotify_FillContextPanelEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_FillContextPanelDelegate != null && ((aEventSinkHelper.m_FillContextPanelDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_FillContextIconMenu(
//    [In] ksPropertyManagerNotify_FillContextIconMenuEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_FillContextIconMenuDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_FillContextIconMenu(
//    [In] ksPropertyManagerNotify_FillContextIconMenuEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_FillContextIconMenuDelegate != null && ((aEventSinkHelper.m_FillContextIconMenuDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_EndEditItem(
//    [In] ksPropertyManagerNotify_EndEditItemEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_EndEditItemDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_EndEditItem(
//    [In] ksPropertyManagerNotify_EndEditItemEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_EndEditItemDelegate != null && ((aEventSinkHelper.m_EndEditItemDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_ChangeTabExpanded(
//    [In] ksPropertyManagerNotify_ChangeTabExpandedEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_ChangeTabExpandedDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_ChangeTabExpanded(
//    [In] ksPropertyManagerNotify_ChangeTabExpandedEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_ChangeTabExpandedDelegate != null && ((aEventSinkHelper.m_ChangeTabExpandedDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_DoubleClickItem(
//    [In] ksPropertyManagerNotify_DoubleClickItemEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyManagerNotify_SinkHelper pUnkSink = new ksPropertyManagerNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_DoubleClickItemDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_DoubleClickItem(
//    [In] ksPropertyManagerNotify_DoubleClickItemEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_aEventSinkHelpers == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        ksPropertyManagerNotify_SinkHelper aEventSinkHelper = (ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_DoubleClickItemDelegate != null && ((aEventSinkHelper.m_DoubleClickItemDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (IConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_11;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_12;
//label_11:
//      return;
//label_12:;
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public ksPropertyManagerNotify_EventProvider([In] object obj0)
//  {
//    this.m_wkConnectionPointContainer = new WeakReference((object) (IConnectionPointContainer) obj0, false);
//  }

//  public override void Finalize()
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 < count)
//      {
//        do
//        {
//          this.m_ConnectionPoint.Unadvise(((ksPropertyManagerNotify_SinkHelper) this.m_aEventSinkHelpers[index]).m_dwCookie);
//          ++index;
//        }
//        while (index < count);
//      }
//      Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//    }
//    catch (Exception ex)
//    {
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void Dispose()
//  {
//    this.Finalize();
//    GC.SuppressFinalize((object) this);
//  }
//}
