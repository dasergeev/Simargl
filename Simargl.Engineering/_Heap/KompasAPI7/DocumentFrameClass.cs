//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.DocumentFrameClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[ClassInterface(ClassInterfaceType.None)]
//[ComSourceInterfaces("KompasAPI7.ksDocumentFrameNotify\0\0")]
//[Guid("8BFFC9B4-5FE7-4EC5-8BA5-0FF8520D5FF6")]
//[ComImport]
//public class DocumentFrameClass : 
//  IDocumentFrame,
//  DocumentFrame,
//  ksDocumentFrameNotify_Event,
//  IKompasAPIObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern DocumentFrameClass();

//  [DispId(1000)]
//  public virtual extern IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  public virtual extern IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  public virtual extern KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  public virtual extern int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1)]
//  public virtual extern FrameRegimeEnum Regime { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2)]
//  public virtual extern bool Active { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(3)]
//  public virtual extern string Caption { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void Zoom([In] double X1, [In] double Y1, [In] double X2, [In] double Y2);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void ZoomPrevNextOrAll([In] ZoomTypeEnum Type);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void ZoomScale([In] double X, [In] double Y, [In] double Scale);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void GetZoomScale(out double X, out double Y, out double Scale);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: ComAliasName("stdole.OLE_HANDLE")]
//  public virtual extern int GetHWND();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void SetGabaritModifying();

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void RefreshWindow();

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ConvertCoordinates(
//    [In] ConvertCoordTypeEnum Type,
//    [In] int LX,
//    [In] int LY,
//    out double X,
//    out double Y,
//    out double Z);

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ExecuteKompasCommand([In] int CommandID, [In] bool PostMessage = true);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsKompasCommandEnable([In] int CommandID);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int IsKompasCommandCheck([In] int CommandID);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  public virtual extern object GetFPSData();

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetPickRay(
//    [In] int wx,
//    [In] int wy,
//    out double X,
//    out double Y,
//    out double Z,
//    out double zx,
//    out double zy,
//    out double zz);

//  [DispId(17)]
//  public virtual extern bool RoundModeOn { [DispId(17), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(18)]
//  public virtual extern double CurrentCursorStep { [DispId(18), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool GetWorkAreaRect(
//    out int Left,
//    out int Top,
//    out int Right,
//    out int Bottom);

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool UnConvertCoordinates(
//    [In] ConvertCoordTypeEnum Type,
//    [In] double X,
//    [In] double Y,
//    [In] double Z,
//    out int LX,
//    out int LY);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetZoomScale([In] double X, [In] double Y, [In] double Scale, [In] bool ViewParam);

//  public virtual extern event ksDocumentFrameNotify_BeginPaintEventHandler BeginPaint;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginPaint([In] ksDocumentFrameNotify_BeginPaintEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginPaint([In] ksDocumentFrameNotify_BeginPaintEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ClosePaint([In] ksDocumentFrameNotify_ClosePaintEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_ClosePaintEventHandler ClosePaint;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ClosePaint([In] ksDocumentFrameNotify_ClosePaintEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_MouseDownEventHandler MouseDown;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_MouseDown([In] ksDocumentFrameNotify_MouseDownEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_MouseDown([In] ksDocumentFrameNotify_MouseDownEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_MouseUpEventHandler MouseUp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_MouseUp([In] ksDocumentFrameNotify_MouseUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_MouseUp([In] ksDocumentFrameNotify_MouseUpEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_MouseDblClick(
//    [In] ksDocumentFrameNotify_MouseDblClickEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_MouseDblClickEventHandler MouseDblClick;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_MouseDblClick(
//    [In] ksDocumentFrameNotify_MouseDblClickEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginPaintGL(
//    [In] ksDocumentFrameNotify_BeginPaintGLEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_BeginPaintGLEventHandler BeginPaintGL;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginPaintGL(
//    [In] ksDocumentFrameNotify_BeginPaintGLEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_ClosePaintGLEventHandler ClosePaintGL;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ClosePaintGL(
//    [In] ksDocumentFrameNotify_ClosePaintGLEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ClosePaintGL(
//    [In] ksDocumentFrameNotify_ClosePaintGLEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_AddGabaritEventHandler AddGabarit;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_AddGabarit([In] ksDocumentFrameNotify_AddGabaritEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_AddGabarit([In] ksDocumentFrameNotify_AddGabaritEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_ActivateEventHandler Activate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Activate([In] ksDocumentFrameNotify_ActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Activate([In] ksDocumentFrameNotify_ActivateEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_DeactivateEventHandler Deactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Deactivate([In] ksDocumentFrameNotify_DeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Deactivate([In] ksDocumentFrameNotify_DeactivateEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_CloseFrameEventHandler CloseFrame;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CloseFrame([In] ksDocumentFrameNotify_CloseFrameEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CloseFrame([In] ksDocumentFrameNotify_CloseFrameEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_MouseMoveEventHandler MouseMove;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_MouseMove([In] ksDocumentFrameNotify_MouseMoveEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_MouseMove([In] ksDocumentFrameNotify_MouseMoveEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_ShowOcxTreeEventHandler ShowOcxTree;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ShowOcxTree([In] ksDocumentFrameNotify_ShowOcxTreeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ShowOcxTree([In] ksDocumentFrameNotify_ShowOcxTreeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginPaintTmpObjects(
//    [In] ksDocumentFrameNotify_BeginPaintTmpObjectsEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_BeginPaintTmpObjectsEventHandler BeginPaintTmpObjects;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginPaintTmpObjects(
//    [In] ksDocumentFrameNotify_BeginPaintTmpObjectsEventHandler obj0);

//  public virtual extern event ksDocumentFrameNotify_ClosePaintTmpObjectsEventHandler ClosePaintTmpObjects;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ClosePaintTmpObjects(
//    [In] ksDocumentFrameNotify_ClosePaintTmpObjectsEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ClosePaintTmpObjects(
//    [In] ksDocumentFrameNotify_ClosePaintTmpObjectsEventHandler obj0);

//  public virtual extern IKompasAPIObject IKompasAPIObject_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IKompasAPIObject_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IKompasAPIObject_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IKompasAPIObject_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
