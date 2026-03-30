//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IExternalTessellationManager
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("DC21F3D3-FFA6-4274-A976-79F34729B866")]
//[ComImport]
//public interface IExternalTessellationManager
//{
//  [DispId(7501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  ExternalTessellationObject Add();

//  [DispId(7502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  ExternalTessellationObject get_TessellationObject([In] int Id);

//  [DispId(7503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_ObjectsVisible([MarshalAs(UnmanagedType.Struct), In] object Ids, [MarshalAs(UnmanagedType.Interface), In] DocumentFrame Frame, [In] bool _param3);

//  [DispId(7504)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteObjects([MarshalAs(UnmanagedType.Struct), In] object Id);

//  [DispId(7505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Clear();

//  [DispId(7506)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  ExternalGDIObject AddGDIObject();

//  [DispId(7507)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  ExternalGDIObject get_GDIObject([In] int Id);

//  [DispId(7508)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int CreateTextureImage(
//    [In] int Width,
//    [In] int Heigh,
//    [In] bool RGBA,
//    [In] bool WrapMode,
//    [In] bool FiltMode,
//    [MarshalAs(UnmanagedType.Struct), In] object ImageData);

//  [DispId(7509)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DeleteTextureImage([In] int TexImgId);

//  [DispId(7510)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool PickObjects(
//    [MarshalAs(UnmanagedType.Interface), In] DocumentFrame Frame,
//    [MarshalAs(UnmanagedType.Struct), In] object PickRay,
//    [In] bool Visible,
//    [MarshalAs(UnmanagedType.Struct)] out object PickedObjs,
//    [MarshalAs(UnmanagedType.Struct)] out object PickedPars);

//  [DispId(7511)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool DisableModelDrawing([In] int ForElements);

//  [DispId(7512)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool EnableModelDrawing();

//  [DispId(7513)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool IsModelDrawingEnabled([In] int ForElements);

//  [DispId(7514)]
//  bool DisableModelRotation { [DispId(7514), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(7514), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
