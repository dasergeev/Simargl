//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IKompasDocument2D1
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("FB57F1C4-44FE-4C73-9B15-87241E8735B5")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IKompasDocument2D1
//{
//  [DispId(11001)]
//  MacroObject EditMacroObject { [DispId(11001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(11002)]
//  DrawingGroups DrawingGroups { [DispId(11002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(11003)]
//  DrawingGroups NamedGroups { [DispId(11003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(11004)]
//  DrawingGroup CurrentGroup { [DispId(11004), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(11005)]
//  SelectionManager SelectionManager { [DispId(11005), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(11006)]
//  ChooseManager ChooseManager { [DispId(11006), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(11007)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object CopyObjects([MarshalAs(UnmanagedType.Struct), In] object Objects, [MarshalAs(UnmanagedType.Interface), In] ICopyObjectParam Params);

//  [DispId(11008)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object get_Variables([In] bool ExternalOnly);

//  [DispId(11009)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 get_Variable([In] bool External, [MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(11010)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int get_VariablesCount([In] bool External);

//  [DispId(11011)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool UpdateVariables();

//  [DispId(11012)]
//  VariableTable VariableTable { [DispId(11012), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(11013)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool IsVariableNameValid([MarshalAs(UnmanagedType.BStr), In] string Name);

//  [DispId(11014)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 AddVariable([MarshalAs(UnmanagedType.BStr), In] string Name, [In] double Value, [MarshalAs(UnmanagedType.BStr), In] string Note);

//  [DispId(11015)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool RebuildDocument();

//  [DispId(11016)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CreateHyperLink(
//    [MarshalAs(UnmanagedType.Struct), In] object Objects,
//    [In] ksHyperLinkTypeEnum Type,
//    [MarshalAs(UnmanagedType.BStr), In] string Text,
//    [MarshalAs(UnmanagedType.Interface), In] IDrawingObject LinkObject,
//    [In] int Level);

//  [DispId(11017)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object GetHyperLinkObjects(
//    [In] ksHyperLinkTypeEnum Type,
//    [MarshalAs(UnmanagedType.Interface), In] IDrawingObject LinkObject,
//    [In] int Level,
//    [MarshalAs(UnmanagedType.BStr), In] string Text);

//  [DispId(11018)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteHyperLinks([MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(11019)]
//  bool EditMacroVisibleRegime { [DispId(11019), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(11020)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IDrawingObject GetObjectById([In] long Id);

//  [DispId(11021)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Process2D get_LibProcess([In] ksProcess2DTypeEnum ProcessType);

//  [DispId(11022)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IDrawingObject FindObject([In] double X, [In] double Y, [In] double Limit, [MarshalAs(UnmanagedType.Interface), In] FindObjectParameters Param);

//  [DispId(11023)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object FindObjects([In] double X, [In] double Y, [In] double Limit, [MarshalAs(UnmanagedType.Interface), In] FindObjectParameters Param);

//  [DispId(11024)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object SelectObjects(
//    [In] ksRegionTypeEnum RegionType,
//    [In] double XMin,
//    [In] double YMin,
//    [In] double XMax,
//    [In] double YMax);

//  [DispId(11025)]
//  object IntervalVariables { [DispId(11025), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(11026)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 AddIntervalVariable([MarshalAs(UnmanagedType.BStr), In] string Name, [In] double FirstValue, [In] double SecondValue);

//  [DispId(11027)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 get_IntervalVariable([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(11028)]
//  object UserFuncVariables { [DispId(11028), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(11029)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 AddUserFuncVariable([MarshalAs(UnmanagedType.BStr), In] string Name, [MarshalAs(UnmanagedType.BStr), In] string Expression);

//  [DispId(11030)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 get_UserFuncVariable([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(11031)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ClearUndo();

//  [DispId(11032)]
//  bool EnableUndo { [DispId(11032), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(11032), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(11033)]
//  bool UndoContainer { [DispId(11033), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(11033), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(11034)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CompleteRebuildDocument();

//  [DispId(11035)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DestroyObjects([MarshalAs(UnmanagedType.Struct), In] object Objects);

//  [DispId(11036)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ChangeObjectsOrder([MarshalAs(UnmanagedType.Struct), In] object Objects, [MarshalAs(UnmanagedType.Interface), In] IDrawingObject OrderObject, [In] ksChangeOrderType OrderType);
//}
