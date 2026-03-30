//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksPropertyUserControlNotify_EventProvider
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

//internal sealed class ksPropertyUserControlNotify_EventProvider : 
//  ksPropertyUserControlNotify_Event,
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
//      (byte) 2,
//      (byte) 187,
//      (byte) 49,
//      (byte) 90,
//      (byte) 108,
//      (byte) 2,
//      (byte) 25,
//      (byte) 68,
//      (byte) 160 /*0xA0*/,
//      byte.MaxValue,
//      (byte) 169,
//      (byte) 60,
//      (byte) 245,
//      (byte) 141,
//      (byte) 202,
//      (byte) 153
//    });
//    ((IConnectionPointContainer) this.m_wkConnectionPointContainer.Target).FindConnectionPoint(ref riid, out ppCP);
//    this.m_ConnectionPoint = ppCP;
//    this.m_aEventSinkHelpers = new ArrayList();
//  }

//  public virtual void add_CreateOCX(
//    [In] ksPropertyUserControlNotify_CreateOCXEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyUserControlNotify_SinkHelper pUnkSink = new ksPropertyUserControlNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_CreateOCXDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_CreateOCX(
//    [In] ksPropertyUserControlNotify_CreateOCXEventHandler obj0)
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
//        ksPropertyUserControlNotify_SinkHelper aEventSinkHelper = (ksPropertyUserControlNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_CreateOCXDelegate != null && ((aEventSinkHelper.m_CreateOCXDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public virtual void add_DestroyOCX(
//    [In] ksPropertyUserControlNotify_DestroyOCXEventHandler obj0)
//  {
//    bool lockTaken;
//    try
//    {
//      Monitor.Enter((object) this, ref lockTaken);
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      ksPropertyUserControlNotify_SinkHelper pUnkSink = new ksPropertyUserControlNotify_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_DestroyOCXDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      if (lockTaken)
//        Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_DestroyOCX(
//    [In] ksPropertyUserControlNotify_DestroyOCXEventHandler obj0)
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
//        ksPropertyUserControlNotify_SinkHelper aEventSinkHelper = (ksPropertyUserControlNotify_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_DestroyOCXDelegate != null && ((aEventSinkHelper.m_DestroyOCXDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
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

//  public ksPropertyUserControlNotify_EventProvider([In] object obj0)
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
//          this.m_ConnectionPoint.Unadvise(((ksPropertyUserControlNotify_SinkHelper) this.m_aEventSinkHelpers[index]).m_dwCookie);
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
