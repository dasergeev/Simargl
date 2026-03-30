//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksSpecificationDescriptionNotify_EventProvider
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

//internal sealed class ksSpecificationDescriptionNotify_EventProvider : 
//  ksSpecificationDescriptionNotify_Event,
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
//      (byte) 230,
//      (byte) 20,
//      (byte) 218,
//      (byte) 160 /*0xA0*/,
//      (byte) 146,
//      (byte) 79,
//      (byte) 24,
//      (byte) 77,
//      (byte) 140,
//      (byte) 209,
//      (byte) 43,
//      (byte) 186,
//      (byte) 182,
//      (byte) 149,
//      (byte) 206,
//      (byte) 19
//    });
//    ((IConnectionPointContainer) this.m_wkConnectionPointContainer.Target).FindConnectionPoint(ref riid, out ppCP);
//    this.m_ConnectionPoint = ppCP;
//    this.m_aEventSinkHelpers = new ArrayList();
//  }

//  public virtual void add_TuningSpcStyleBeginChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_TuningSpcStyleBeginChangeDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_TuningSpcStyleBeginChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleBeginChangeEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_TuningSpcStyleBeginChangeDelegate != null && ((aEventSinkHelper.m_TuningSpcStyleBeginChangeDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_TuningSpcStyleChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_TuningSpcStyleChangeDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_TuningSpcStyleChange(
//    [In] ksSpecificationDescriptionNotify_TuningSpcStyleChangeEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_TuningSpcStyleChangeDelegate != null && ((aEventSinkHelper.m_TuningSpcStyleChangeDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_ChangeCurrentSpcDescription(
//    [In] ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_ChangeCurrentSpcDescriptionDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_ChangeCurrentSpcDescription(
//    [In] ksSpecificationDescriptionNotify_ChangeCurrentSpcDescriptionEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_ChangeCurrentSpcDescriptionDelegate != null && ((aEventSinkHelper.m_ChangeCurrentSpcDescriptionDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_SpcDescriptionAdd(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_SpcDescriptionAddDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_SpcDescriptionAdd(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionAddEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_SpcDescriptionAddDelegate != null && ((aEventSinkHelper.m_SpcDescriptionAddDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_SpcDescriptionRemove(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_SpcDescriptionRemoveDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_SpcDescriptionRemove(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionRemoveEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_SpcDescriptionRemoveDelegate != null && ((aEventSinkHelper.m_SpcDescriptionRemoveDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_SpcDescriptionBeginEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_SpcDescriptionBeginEditDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_SpcDescriptionBeginEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionBeginEditEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_SpcDescriptionBeginEditDelegate != null && ((aEventSinkHelper.m_SpcDescriptionBeginEditDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_SpcDescriptionEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_SpcDescriptionEditDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_SpcDescriptionEdit(
//    [In] ksSpecificationDescriptionNotify_SpcDescriptionEditEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_SpcDescriptionEditDelegate != null && ((aEventSinkHelper.m_SpcDescriptionEditDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_SynchronizationBegin(
//    [In] ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_SynchronizationBeginDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_SynchronizationBegin(
//    [In] ksSpecificationDescriptionNotify_SynchronizationBeginEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_SynchronizationBeginDelegate != null && ((aEventSinkHelper.m_SynchronizationBeginDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_Synchronization(
//    [In] ksSpecificationDescriptionNotify_SynchronizationEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_SynchronizationDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_Synchronization(
//    [In] ksSpecificationDescriptionNotify_SynchronizationEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_SynchronizationDelegate != null && ((aEventSinkHelper.m_SynchronizationDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_BeginCalcPositions(
//    [In] ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_BeginCalcPositionsDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_BeginCalcPositions(
//    [In] ksSpecificationDescriptionNotify_BeginCalcPositionsEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_BeginCalcPositionsDelegate != null && ((aEventSinkHelper.m_BeginCalcPositionsDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_CalcPositions(
//    [In] ksSpecificationDescriptionNotify_CalcPositionsEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_CalcPositionsDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_CalcPositions(
//    [In] ksSpecificationDescriptionNotify_CalcPositionsEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_CalcPositionsDelegate != null && ((aEventSinkHelper.m_CalcPositionsDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_BeginCreateObject(
//    [In] ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksSpecificationDescriptionNotify_SinkHelper pUnkSink = new ksSpecificationDescriptionNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_BeginCreateObjectDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_BeginCreateObject(
//    [In] ksSpecificationDescriptionNotify_BeginCreateObjectEventHandler obj0)
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
//        ksSpecificationDescriptionNotify_SinkHelper aEventSinkHelper = (ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_BeginCreateObjectDelegate != null && ((aEventSinkHelper.m_BeginCreateObjectDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public ksSpecificationDescriptionNotify_EventProvider([In] object obj0)
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
//          this.m_ConnectionPoint.Unadvise(((ksSpecificationDescriptionNotify_SinkHelper) this.m_aEventSinkHelpers[index]).m_dwCookie);
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
