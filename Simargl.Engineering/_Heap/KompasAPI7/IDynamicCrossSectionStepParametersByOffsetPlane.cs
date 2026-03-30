//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IDynamicCrossSectionStepParametersByOffsetPlane
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using Kompas6Constants3D;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("E5BEB695-4C46-4897-9330-7463ED4D6725")]
//[ComImport]
//public interface IDynamicCrossSectionStepParametersByOffsetPlane : IKompasAPIObject
//{
//  [DispId(1000)]
//  new IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  new IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  new KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  new int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(801)]
//  Plane3DByOffset OffsetPlane { [DispId(801), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(802)]
//  ksCrossSectionPlaneBuildingTypeEnum PlaneBuildingType { [DispId(802), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(802), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

//  [DispId(803)]
//  DynamicCrossSectionStep BaseStep { [DispId(803), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; [DispId(803), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Interface), In] set; }

//  [DispId(804)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ReverseDirection();
//}
