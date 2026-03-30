//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IEmbodimentsManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("C1948CE8-0933-4D75-9446-3E143928D045")]
//[ComImport]
//public interface IEmbodimentsManager
//{
//  [DispId(8001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object GetEmbodimentsTree(
//    [In] ksVariantMarkingTypeEnum MarkingType,
//    [In] bool AddSystemDelimer,
//    [In] bool AddSpaces);

//  [DispId(8002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetCurrentEmbodimentMarking([In] ksVariantMarkingTypeEnum MarkingType, [In] bool AddSystemDelimer);

//  [DispId(8003)]
//  int CurrentEmbodimentIndex { [DispId(8003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(8004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetCurrentEmbodiment([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(8005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddEmbodiment(
//    [MarshalAs(UnmanagedType.Struct), In] object ParentIndex,
//    [In] bool Depended,
//    [MarshalAs(UnmanagedType.BStr), In] string BaseMarking,
//    [MarshalAs(UnmanagedType.BStr), In] string EmbodimentNumber,
//    [MarshalAs(UnmanagedType.BStr), In] string AdditionalNumber);

//  [DispId(8006)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteEmbodiment([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(8007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetEmbodimentMarking(
//    [In] int Index,
//    [In] ksVariantMarkingTypeEnum MarkingType,
//    [In] bool AddSystemDelimer);

//  [DispId(8008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetEmbodimentMarking([In] int Index, [In] ksVariantMarkingTypeEnum MarkingType, [MarshalAs(UnmanagedType.BStr), In] string Marking);

//  [DispId(8009)]
//  int EmbodimentCount { [DispId(8009), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(8010)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Embodiment get_Embodiment([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(8011)]
//  Embodiment TopEmbodiment { [DispId(8011), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(8012)]
//  Embodiment CurrentEmbodiment { [DispId(8012), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(8013)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddMirrorEmbodiment(
//    [MarshalAs(UnmanagedType.Struct), In] object ParentIndex,
//    [MarshalAs(UnmanagedType.BStr), In] string BaseMarking,
//    [MarshalAs(UnmanagedType.BStr), In] string EmbodimentNumber,
//    [MarshalAs(UnmanagedType.BStr), In] string AdditionalNumber);

//  [DispId(8014)]
//  string EmbodimentAdditionalNumber { [DispId(8014), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(8014), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

//  [DispId(8015)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Embodiment FindEmbodiment([MarshalAs(UnmanagedType.BStr), In] string UniqueMetaObjectKey);

//  [DispId(8016)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddVariant([MarshalAs(UnmanagedType.Struct), In] object ParentIndex, [MarshalAs(UnmanagedType.BStr), In] string VariantName);

//  [DispId(8017)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool IsVariant([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(8018)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object GetEmbodimentsVariants([MarshalAs(UnmanagedType.Struct), In] object ParentIndex);
//}
