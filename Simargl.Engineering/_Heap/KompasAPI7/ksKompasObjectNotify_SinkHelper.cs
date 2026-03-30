//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ksKompasObjectNotify_SinkHelper
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6API5;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[ClassInterface(ClassInterfaceType.None)]
//public sealed class ksKompasObjectNotify_SinkHelper : ksKompasObjectNotify
//{
//  public ksKompasObjectNotify_CreateDocumentEventHandler m_CreateDocumentDelegate;
//  public ksKompasObjectNotify_BeginOpenDocumentEventHandler m_BeginOpenDocumentDelegate;
//  public ksKompasObjectNotify_OpenDocumentEventHandler m_OpenDocumentDelegate;
//  public ksKompasObjectNotify_ChangeActiveDocumentEventHandler m_ChangeActiveDocumentDelegate;
//  public ksKompasObjectNotify_ApplicationDestroyEventHandler m_ApplicationDestroyDelegate;
//  public ksKompasObjectNotify_BeginCreateEventHandler m_BeginCreateDelegate;
//  public ksKompasObjectNotify_BeginOpenFileEventHandler m_BeginOpenFileDelegate;
//  public ksKompasObjectNotify_BeginCloseAllDocumentEventHandler m_BeginCloseAllDocumentDelegate;
//  public ksKompasObjectNotify_KeyDownEventHandler m_KeyDownDelegate;
//  public ksKompasObjectNotify_KeyUpEventHandler m_KeyUpDelegate;
//  public ksKompasObjectNotify_KeyPressEventHandler m_KeyPressDelegate;
//  public ksKompasObjectNotify_BeginReguestFilesEventHandler m_BeginReguestFilesDelegate;
//  public ksKompasObjectNotify_BeginChoiceMaterialEventHandler m_BeginChoiceMaterialDelegate;
//  public ksKompasObjectNotify_ChoiceMaterialEventHandler m_ChoiceMaterialDelegate;
//  public ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler m_IsNeedConvertToSavePreviousDelegate;
//  public ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler m_BeginConvertToSavePreviousDelegate;
//  public ksKompasObjectNotify_EndConvertToSavePreviousEventHandler m_EndConvertToSavePreviousDelegate;
//  public ksKompasObjectNotify_ChangeThemeEventHandler m_ChangeThemeDelegate;
//  public ksKompasObjectNotify_BeginDragOpenFilesEventHandler m_BeginDragOpenFilesDelegate;
//  public int m_dwCookie;

//  public virtual bool CreateDocument([In] object obj0, [In] int obj1)
//  {
//    return this.m_CreateDocumentDelegate != null && this.m_CreateDocumentDelegate(obj0, obj1);
//  }

//  public virtual bool BeginOpenDocument([In] string obj0)
//  {
//    return this.m_BeginOpenDocumentDelegate != null && this.m_BeginOpenDocumentDelegate(obj0);
//  }

//  public virtual bool OpenDocument([In] object obj0, [In] int obj1)
//  {
//    return this.m_OpenDocumentDelegate != null && this.m_OpenDocumentDelegate(obj0, obj1);
//  }

//  public virtual bool ChangeActiveDocument([In] object obj0, [In] int obj1)
//  {
//    return this.m_ChangeActiveDocumentDelegate != null && this.m_ChangeActiveDocumentDelegate(obj0, obj1);
//  }

//  public virtual bool ApplicationDestroy()
//  {
//    return this.m_ApplicationDestroyDelegate != null && this.m_ApplicationDestroyDelegate();
//  }

//  public virtual bool BeginCreate([In] int obj0)
//  {
//    return this.m_BeginCreateDelegate != null && this.m_BeginCreateDelegate(obj0);
//  }

//  public virtual bool BeginOpenFile()
//  {
//    return this.m_BeginOpenFileDelegate != null && this.m_BeginOpenFileDelegate();
//  }

//  public virtual bool BeginCloseAllDocument()
//  {
//    return this.m_BeginCloseAllDocumentDelegate != null && this.m_BeginCloseAllDocumentDelegate();
//  }

//  public virtual bool KeyDown([In] ref int obj0, [In] int obj1, [In] bool obj2)
//  {
//    return this.m_KeyDownDelegate != null && this.m_KeyDownDelegate(ref obj0, obj1, obj2);
//  }

//  public virtual bool KeyUp([In] ref int obj0, [In] int obj1, [In] bool obj2)
//  {
//    return this.m_KeyUpDelegate != null && this.m_KeyUpDelegate(ref obj0, obj1, obj2);
//  }

//  public virtual bool KeyPress([In] ref int obj0, [In] bool obj1)
//  {
//    return this.m_KeyPressDelegate != null && this.m_KeyPressDelegate(ref obj0, obj1);
//  }

//  public virtual bool BeginReguestFiles([In] int obj0, [In] ref object obj1)
//  {
//    return this.m_BeginReguestFilesDelegate != null && this.m_BeginReguestFilesDelegate(obj0, ref obj1);
//  }

//  public virtual bool BeginChoiceMaterial([In] int obj0)
//  {
//    return this.m_BeginChoiceMaterialDelegate != null && this.m_BeginChoiceMaterialDelegate(obj0);
//  }

//  public virtual bool ChoiceMaterial([In] int obj0, [In] string obj1, [In] double obj2)
//  {
//    return this.m_ChoiceMaterialDelegate != null && this.m_ChoiceMaterialDelegate(obj0, obj1, obj2);
//  }

//  public virtual bool IsNeedConvertToSavePrevious(
//    [In] object obj0,
//    [In] int obj1,
//    [In] int obj2,
//    [In] object obj3,
//    [In] ref bool obj4)
//  {
//    return this.m_IsNeedConvertToSavePreviousDelegate != null && this.m_IsNeedConvertToSavePreviousDelegate(obj0, obj1, obj2, obj3, ref obj4);
//  }

//  public virtual bool BeginConvertToSavePrevious([In] object obj0, [In] int obj1, [In] int obj2, [In] object obj3)
//  {
//    return this.m_BeginConvertToSavePreviousDelegate != null && this.m_BeginConvertToSavePreviousDelegate(obj0, obj1, obj2, obj3);
//  }

//  public virtual bool EndConvertToSavePrevious([In] object obj0, [In] int obj1, [In] int obj2, [In] object obj3)
//  {
//    return this.m_EndConvertToSavePreviousDelegate != null && this.m_EndConvertToSavePreviousDelegate(obj0, obj1, obj2, obj3);
//  }

//  public virtual bool ChangeTheme([In] int obj0)
//  {
//    return this.m_ChangeThemeDelegate != null && this.m_ChangeThemeDelegate(obj0);
//  }

//  public virtual bool BeginDragOpenFiles([In] ref object obj0, [In] bool obj1, [In] ref bool obj2)
//  {
//    return this.m_BeginDragOpenFilesDelegate != null && this.m_BeginDragOpenFilesDelegate(ref obj0, obj1, ref obj2);
//  }

//  internal ksKompasObjectNotify_SinkHelper()
//  {
//    this.m_dwCookie = 0;
//    this.m_CreateDocumentDelegate = (ksKompasObjectNotify_CreateDocumentEventHandler) null;
//    this.m_BeginOpenDocumentDelegate = (ksKompasObjectNotify_BeginOpenDocumentEventHandler) null;
//    this.m_OpenDocumentDelegate = (ksKompasObjectNotify_OpenDocumentEventHandler) null;
//    this.m_ChangeActiveDocumentDelegate = (ksKompasObjectNotify_ChangeActiveDocumentEventHandler) null;
//    this.m_ApplicationDestroyDelegate = (ksKompasObjectNotify_ApplicationDestroyEventHandler) null;
//    this.m_BeginCreateDelegate = (ksKompasObjectNotify_BeginCreateEventHandler) null;
//    this.m_BeginOpenFileDelegate = (ksKompasObjectNotify_BeginOpenFileEventHandler) null;
//    this.m_BeginCloseAllDocumentDelegate = (ksKompasObjectNotify_BeginCloseAllDocumentEventHandler) null;
//    this.m_KeyDownDelegate = (ksKompasObjectNotify_KeyDownEventHandler) null;
//    this.m_KeyUpDelegate = (ksKompasObjectNotify_KeyUpEventHandler) null;
//    this.m_KeyPressDelegate = (ksKompasObjectNotify_KeyPressEventHandler) null;
//    this.m_BeginReguestFilesDelegate = (ksKompasObjectNotify_BeginReguestFilesEventHandler) null;
//    this.m_BeginChoiceMaterialDelegate = (ksKompasObjectNotify_BeginChoiceMaterialEventHandler) null;
//    this.m_ChoiceMaterialDelegate = (ksKompasObjectNotify_ChoiceMaterialEventHandler) null;
//    this.m_IsNeedConvertToSavePreviousDelegate = (ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler) null;
//    this.m_BeginConvertToSavePreviousDelegate = (ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler) null;
//    this.m_EndConvertToSavePreviousDelegate = (ksKompasObjectNotify_EndConvertToSavePreviousEventHandler) null;
//    this.m_ChangeThemeDelegate = (ksKompasObjectNotify_ChangeThemeEventHandler) null;
//    this.m_BeginDragOpenFilesDelegate = (ksKompasObjectNotify_BeginDragOpenFilesEventHandler) null;
//  }
//}
