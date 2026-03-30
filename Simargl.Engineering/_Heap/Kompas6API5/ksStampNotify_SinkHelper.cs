//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.ksStampNotify_SinkHelper
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksStampNotify_SinkHelper : ksStampNotify
//{
//  public ksStampNotify_BeginEditStampEventHandler m_BeginEditStampDelegate;
//  public ksStampNotify_EndEditStampEventHandler m_EndEditStampDelegate;
//  public ksStampNotify_StampCellDblClickEventHandler m_StampCellDblClickDelegate;
//  public ksStampNotify_StampCellBeginEditEventHandler m_StampCellBeginEditDelegate;
//  public ksStampNotify_StampBeginClearCellsEventHandler m_StampBeginClearCellsDelegate;
//  public int m_dwCookie;

//  public virtual bool BeginEditStamp()
//  {
//    return this.m_BeginEditStampDelegate != null && this.m_BeginEditStampDelegate();
//  }

//  public virtual bool EndEditStamp([In] bool obj0)
//  {
//    return this.m_EndEditStampDelegate != null && this.m_EndEditStampDelegate(obj0);
//  }

//  public virtual bool StampCellDblClick([In] int obj0)
//  {
//    return this.m_StampCellDblClickDelegate != null && this.m_StampCellDblClickDelegate(obj0);
//  }

//  public virtual bool StampCellBeginEdit([In] int obj0)
//  {
//    return this.m_StampCellBeginEditDelegate != null && this.m_StampCellBeginEditDelegate(obj0);
//  }

//  public virtual bool StampBeginClearCells([In] object obj0)
//  {
//    return this.m_StampBeginClearCellsDelegate != null && this.m_StampBeginClearCellsDelegate(obj0);
//  }

//  internal ksStampNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_BeginEditStampDelegate = (ksStampNotify_BeginEditStampEventHandler) null;
//    this.m_EndEditStampDelegate = (ksStampNotify_EndEditStampEventHandler) null;
//    this.m_StampCellDblClickDelegate = (ksStampNotify_StampCellDblClickEventHandler) null;
//    this.m_StampCellBeginEditDelegate = (ksStampNotify_StampCellBeginEditEventHandler) null;
//    this.m_StampBeginClearCellsDelegate = (ksStampNotify_StampBeginClearCellsEventHandler) null;
//  }
//}
