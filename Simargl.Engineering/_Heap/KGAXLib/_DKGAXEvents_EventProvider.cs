//// Decompiled with JetBrains decompiler
//// Type: KGAXLib._DKGAXEvents_EventProvider
//// Assembly: KGAXLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: E31777E8-3D29-4A2D-9394-6416A05AC4DD
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KGAXLib.dll

//using System;
//using System.Collections;
//using System.Runtime.InteropServices;
//using System.Threading;

//#nullable disable
//namespace KGAXLib;

//internal sealed class _DKGAXEvents_EventProvider : _DKGAXEvents_Event, IDisposable
//{
//  private UCOMIConnectionPointContainer m_ConnectionPointContainer;
//  private ArrayList m_aEventSinkHelpers;
//  private UCOMIConnectionPoint m_ConnectionPoint;

//  private void Init()
//  {
//    UCOMIConnectionPoint ppCP = (UCOMIConnectionPoint) null;
//    Guid riid = new Guid(new byte[16 /*0x10*/]
//    {
//      (byte) 106,
//      (byte) 116,
//      (byte) 79,
//      (byte) 70,
//      (byte) 109,
//      (byte) 172,
//      (byte) 25,
//      (byte) 73,
//      (byte) 130,
//      (byte) 233,
//      (byte) 167,
//      (byte) 54,
//      (byte) 62,
//      (byte) 102,
//      (byte) 30,
//      (byte) 207
//    });
//    this.m_ConnectionPointContainer.FindConnectionPoint(ref riid, out ppCP);
//    this.m_ConnectionPoint = ppCP;
//    this.m_aEventSinkHelpers = new ArrayList();
//  }

//  public virtual void add_OnKgKeyPress([In] _DKGAXEvents_OnKgKeyPressEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgKeyPressDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgKeyPress([In] _DKGAXEvents_OnKgKeyPressEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgKeyPressDelegate != null && ((aEventSinkHelper.m_OnKgKeyPressDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgKeyUp([In] _DKGAXEvents_OnKgKeyUpEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgKeyUpDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgKeyUp([In] _DKGAXEvents_OnKgKeyUpEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgKeyUpDelegate != null && ((aEventSinkHelper.m_OnKgKeyUpDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgKeyDown([In] _DKGAXEvents_OnKgKeyDownEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgKeyDownDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgKeyDown([In] _DKGAXEvents_OnKgKeyDownEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgKeyDownDelegate != null && ((aEventSinkHelper.m_OnKgKeyDownDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgErrorLoadDocument(
//    [In] _DKGAXEvents_OnKgErrorLoadDocumentEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgErrorLoadDocumentDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgErrorLoadDocument(
//    [In] _DKGAXEvents_OnKgErrorLoadDocumentEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgErrorLoadDocumentDelegate != null && ((aEventSinkHelper.m_OnKgErrorLoadDocumentDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgAddGabatit([In] _DKGAXEvents_OnKgAddGabatitEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgAddGabatitDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgAddGabatit([In] _DKGAXEvents_OnKgAddGabatitEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgAddGabatitDelegate != null && ((aEventSinkHelper.m_OnKgAddGabatitDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgCreateGLList([In] _DKGAXEvents_OnKgCreateGLListEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgCreateGLListDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgCreateGLList([In] _DKGAXEvents_OnKgCreateGLListEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgCreateGLListDelegate != null && ((aEventSinkHelper.m_OnKgCreateGLListDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgPaint([In] _DKGAXEvents_OnKgPaintEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgPaintDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgPaint([In] _DKGAXEvents_OnKgPaintEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgPaintDelegate != null && ((aEventSinkHelper.m_OnKgPaintDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgCreate([In] _DKGAXEvents_OnKgCreateEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgCreateDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgCreate([In] _DKGAXEvents_OnKgCreateEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgCreateDelegate != null && ((aEventSinkHelper.m_OnKgCreateDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgStopCurrentProcess(
//    [In] _DKGAXEvents_OnKgStopCurrentProcessEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgStopCurrentProcessDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgStopCurrentProcess(
//    [In] _DKGAXEvents_OnKgStopCurrentProcessEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgStopCurrentProcessDelegate != null && ((aEventSinkHelper.m_OnKgStopCurrentProcessDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgMouseDblClick([In] _DKGAXEvents_OnKgMouseDblClickEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgMouseDblClickDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgMouseDblClick([In] _DKGAXEvents_OnKgMouseDblClickEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgMouseDblClickDelegate != null && ((aEventSinkHelper.m_OnKgMouseDblClickDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgMouseUp([In] _DKGAXEvents_OnKgMouseUpEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgMouseUpDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgMouseUp([In] _DKGAXEvents_OnKgMouseUpEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgMouseUpDelegate != null && ((aEventSinkHelper.m_OnKgMouseUpDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void add_OnKgMouseDown([In] _DKGAXEvents_OnKgMouseDownEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        this.Init();
//      _DKGAXEvents_SinkHelper pUnkSink = new _DKGAXEvents_SinkHelper();
//      int pdwCookie = 0;
//      this.m_ConnectionPoint.Advise((object) pUnkSink, out pdwCookie);
//      pUnkSink.m_dwCookie = pdwCookie;
//      pUnkSink.m_OnKgMouseDownDelegate = obj0;
//      this.m_aEventSinkHelpers.Add((object) pUnkSink);
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void remove_OnKgMouseDown([In] _DKGAXEvents_OnKgMouseDownEventHandler obj0)
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 >= count)
//        return;
//      do
//      {
//        _DKGAXEvents_SinkHelper aEventSinkHelper = (_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index];
//        if (aEventSinkHelper.m_OnKgMouseDownDelegate != null && ((aEventSinkHelper.m_OnKgMouseDownDelegate.Equals((object) obj0) ? 1 : 0) & (int) byte.MaxValue) != 0)
//        {
//          this.m_aEventSinkHelpers.RemoveAt(index);
//          this.m_ConnectionPoint.Unadvise(aEventSinkHelper.m_dwCookie);
//          if (count <= 1)
//          {
//            Marshal.ReleaseComObject((object) this.m_ConnectionPoint);
//            this.m_ConnectionPoint = (UCOMIConnectionPoint) null;
//            this.m_aEventSinkHelpers = (ArrayList) null;
//            return;
//          }
//          goto label_8;
//        }
//        ++index;
//      }
//      while (index < count);
//      goto label_9;
//label_8:
//      return;
//label_9:;
//    }
//    finally
//    {
//      Monitor.Exit((object) this);
//    }
//  }

//  public _DKGAXEvents_EventProvider([In] object obj0)
//  {
//    this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer) obj0;
//  }

//  public override void Finalize()
//  {
//    Monitor.Enter((object) this);
//    try
//    {
//      if (this.m_ConnectionPoint == null)
//        return;
//      int count = this.m_aEventSinkHelpers.Count;
//      int index = 0;
//      if (0 < count)
//      {
//        do
//        {
//          this.m_ConnectionPoint.Unadvise(((_DKGAXEvents_SinkHelper) this.m_aEventSinkHelpers[index]).m_dwCookie);
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
//      Monitor.Exit((object) this);
//    }
//  }

//  public virtual void Dispose()
//  {
//    this.Finalize();
//    GC.SuppressFinalize((object) this);
//  }
//}
