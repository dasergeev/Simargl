//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.TexturesParamClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("B0AA22BA-3F8B-4DC7-97FF-688AE467179D")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class TexturesParamClass : ITexturesParam, TexturesParam, IKompasAPIObject
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern TexturesParamClass();

//  [DispId(6501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string get_TextureFileName([In] ksTextureTypeEnum Type);

//  [DispId(6501)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_TextureFileName([In] ksTextureTypeEnum Type, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(6502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double get_TextureHeight([In] ksTextureTypeEnum Type);

//  [DispId(6502)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_TextureHeight([In] ksTextureTypeEnum Type, [In] double PVal);

//  [DispId(6503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double get_TextureWidth([In] ksTextureTypeEnum Type);

//  [DispId(6503)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_TextureWidth([In] ksTextureTypeEnum Type, [In] double PVal);

//  [DispId(6504)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double get_TextureDx([In] ksTextureTypeEnum Type);

//  [DispId(6504)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_TextureDx([In] ksTextureTypeEnum Type, [In] double PVal);

//  [DispId(6505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double get_TextureDy([In] ksTextureTypeEnum Type);

//  [DispId(6505)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_TextureDy([In] ksTextureTypeEnum Type, [In] double PVal);

//  [DispId(6506)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double get_TextureAngle([In] ksTextureTypeEnum Type);

//  [DispId(6506)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void set_TextureAngle([In] ksTextureTypeEnum Type, [In] double PVal);

//  [DispId(6507)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Update();

//  public virtual extern IKompasAPIObject Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
//}
