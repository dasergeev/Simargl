//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IKompasDocument3D1
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("92AB02F7-2C68-4A74-9E74-70C51E015FEF")]
//[ComImport]
//public interface IKompasDocument3D1
//{
//  [DispId(5029)]
//  bool HideLayoutGeometry { [DispId(5029), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5029), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5030)]
//  IFeature7 EditObject { [DispId(5030), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Interface), In] set; [DispId(5030), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5031)]
//  Document3DManager Document3DManager { [DispId(5031), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5032)]
//  SpecRough3D SpecRough { [DispId(5032), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5033)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ClearUndo();

//  [DispId(5034)]
//  IMateConstraints3D MateConstraints { [DispId(5034), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5035)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ExcludeObjects([MarshalAs(UnmanagedType.Struct), In] object Objects, [In] bool Excl);

//  [DispId(5036)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ExecuteProcessOfInsertComponentFromFile([MarshalAs(UnmanagedType.BStr), In] string FileName, [In] ProcessTypeEnum ProcessType);

//  [DispId(5037)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Process3D get_LibProcess([In] ksProcess3DTypeEnum ProcessType);

//  [DispId(5038)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Struct)]
//  object PickModelObjects([MarshalAs(UnmanagedType.Struct), In] object PickRay, [MarshalAs(UnmanagedType.Interface), In] FindObject3DParameters FilterParam);

//  [DispId(5039)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IModelObject FindNearestObject([MarshalAs(UnmanagedType.Struct), In] object PickRay, [MarshalAs(UnmanagedType.Interface), In] FindObject3DParameters FilterParam);

//  [DispId(5040)]
//  object IntervalVariables { [DispId(5040), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(5041)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 AddIntervalVariable([MarshalAs(UnmanagedType.BStr), In] string Name, [In] double FirstValue, [In] double SecondValue);

//  [DispId(5042)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 get_IntervalVariable([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(5043)]
//  object UserFuncVariables { [DispId(5043), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

//  [DispId(5044)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 AddUserFuncVariable([MarshalAs(UnmanagedType.BStr), In] string Name, [MarshalAs(UnmanagedType.BStr), In] string Expression);

//  [DispId(5045)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  Variable7 get_UserFuncVariable([MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(5046)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IKompasAPIObject FindObjectByAssociationGeometry([MarshalAs(UnmanagedType.Interface), In] IKompasAPIObject Geometry);

//  [DispId(5047)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetHiddenObjects([MarshalAs(UnmanagedType.Struct), In] object Objects, [In] bool Visible);

//  [DispId(5048)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool CompleteRebuildDocument();

//  [DispId(5049)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ConvertToAdditionFormat([MarshalAs(UnmanagedType.BStr), In] string FileName, [MarshalAs(UnmanagedType.Interface), In] AdditionConvertParameters Param);

//  [DispId(5050)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ConvertFromAdditionFormat([MarshalAs(UnmanagedType.BStr), In] string FileName, [MarshalAs(UnmanagedType.Interface), In] AdditionConvertParameters Param);

//  [DispId(5051)]
//  bool TreeNeedRebuild { [DispId(5051), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5051), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5052)]
//  bool TreeNeedUpdateItems { [DispId(5052), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5052), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5053)]
//  bool WindowNeedRebuild { [DispId(5053), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(5053), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5054)]
//  ComponentPositioner7 ComponentPositioner { [DispId(5054), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(5055)]
//  ViewProjectionManager ViewProjectionManager { [DispId(5055), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }
//}
