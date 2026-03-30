//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.ITextDocumentSectionsManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("EEB71F69-1C0F-4E73-9D20-523697215E0B")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface ITextDocumentSectionsManager
//{
//  [DispId(2001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  TextDocumentSection AddSection();

//  [DispId(2002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  TextDocumentSection AddSectionAt([In] int Index);

//  [DispId(2003)]
//  int SectionsCount { [DispId(2003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  TextDocumentSection get_Section([In] int Index);

//  [DispId(2005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  TextDocumentSection get_SectionByTextLine([In] int LineIndex);

//  [DispId(2006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool MoveSection([In] int SectionIndex, [In] int NewSectionIndex);

//  [DispId(2007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetSectionLineIndexes([In] int SectionIndex, out int FirstLineIndex, out int LastLineIndex);

//  [DispId(2008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool MoveLinesToSection([MarshalAs(UnmanagedType.Interface), In] TextDocumentSection Section, [In] int FirstLineIndex, [In] int LastLineIndex);
//}
