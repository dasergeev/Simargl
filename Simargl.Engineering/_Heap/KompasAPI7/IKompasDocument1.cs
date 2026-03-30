//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IKompasDocument1
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("58890FE8-E671-4561-994A-600DD29032E4")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IKompasDocument1
//{
//  [DispId(10001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Delete([MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(10002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_Attributes([In] int Key1, [In] int Key2, [In] int Key3, [In] int Key4, [In] double Numb, [MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(10003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_ObjectsByAttr(
//    [In] int Key1,
//    [In] int Key2,
//    [In] int Key3,
//    [In] int Key4,
//    [In] double Numb,
//    [MarshalAs(UnmanagedType.Struct), In] object ObjectType);

//  [DispId(10004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  _Attribute CreateAttr([In] double AttrID, [MarshalAs(UnmanagedType.BStr), In] string Libname, [MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(10005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ViewEditAttr([ComAliasName("stdole.OLE_HANDLE"), In] int HWnd, [MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(10006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IKompasAPIObject GetInterface([In] KompasAPIObjectTypeEnum Type);

//  [DispId(10007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_ExternalFilesNames([In] bool allFiles);

//  [DispId(10008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SaveAsEx([MarshalAs(UnmanagedType.BStr), In] string PathName, [In] int saveMode);

//  [DispId(10009)]
//  double CreationDate { [DispId(10009), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(10010)]
//  double LastChangeDate { [DispId(10010), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(10011)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool GetExternalFilesNamesEx([In] bool allFiles, [MarshalAs(UnmanagedType.Struct)] out object Files, [MarshalAs(UnmanagedType.Struct)] out object FilesTypes);

//  [DispId(10012)]
//  string Organization { [DispId(10012), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(10012), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(10013)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ReportPropertiesMultieditMode([In] bool On, [In] bool UpdateProps);

//  [DispId(10014)]
//  string Metadata { [DispId(10014), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(10014), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(10015)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool WriteMetadataToFile([MarshalAs(UnmanagedType.BStr), In] string FileName);

//  [DispId(10016)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ApplyMetadataFromFile([MarshalAs(UnmanagedType.BStr), In] string FileName);

//  [DispId(10017)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ActivateToolbarSet([MarshalAs(UnmanagedType.BStr), In] string TolbrSetId);

//  [DispId(10018)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool RedrawDocument([In] ksRedrawDocumentModeEnum Mode);

//  [DispId(10019)]
//  string Author { [DispId(10019), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(10019), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(10020)]
//  string Comment { [DispId(10020), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; [DispId(10020), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(10021)]
//  string DocumentTypeId { [DispId(10021), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; [DispId(10021), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

//  [DispId(10022)]
//  int OpenVersion { [DispId(10022), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(10023)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ReplaceExternalFilesNames([In] bool allFiles, [MarshalAs(UnmanagedType.BStr), In] string OldFileName, [MarshalAs(UnmanagedType.BStr), In] string NewFileName);

//  [DispId(10024)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetChanged([In] bool Changed);

//  [DispId(10025)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetObjectId([MarshalAs(UnmanagedType.Interface), In] IKompasAPIObject Object, [MarshalAs(UnmanagedType.Interface), In] IKompasAPIObject Parent);

//  [DispId(10026)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IKompasAPIObject FindObjectById([MarshalAs(UnmanagedType.BStr), In] string Id, [MarshalAs(UnmanagedType.Interface), In] IKompasAPIObject Parent);

//  [DispId(10027)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SaveAsToRasterFormat([MarshalAs(UnmanagedType.BStr), In] string FileName, [MarshalAs(UnmanagedType.Interface), In] RasterConvertParameters Param);
//}
