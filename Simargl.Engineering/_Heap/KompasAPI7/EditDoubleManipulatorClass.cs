//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.EditDoubleManipulatorClass
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("F942D621-874A-4A51-A651-A435CE0C6304")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComImport]
//public class EditDoubleManipulatorClass : 
//  IEditDoubleManipulator,
//  EditDoubleManipulator,
//  IKompasAPIObject,
//  IBaseManipulator
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern EditDoubleManipulatorClass();

//  [DispId(2001)]
//  public virtual extern double EditValue { [DispId(2001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [DispId(2001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(2002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetValueRange([In] double MinVal, [In] double MaxVal);

//  [DispId(2003)]
//  public virtual extern bool IsEditCreated { [DispId(2003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern IKompasAPIObject Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern IKompasAPIObject IBaseManipulator_Parent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern IApplication IBaseManipulator_Application { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  public virtual extern KompasAPIObjectTypeEnum IBaseManipulator_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int IBaseManipulator_Reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern ksManipulatorTypeEnum ManipulatorType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern int Id { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  public virtual extern Placement3D Placement { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool UpdatePlacement([In] bool Redraw);

//  public virtual extern bool Visible { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  public virtual extern bool Active { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Create();

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Delete();
//}
