//// Decompiled with JetBrains decompiler
//// Type: Kompas6API3D5COM.ISplineDefinition
//// Assembly: Kompas6API3D5COM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 98DBB410-35A6-4482-8352-058793489E25
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API3D5COM.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API3D5COM;

//[Guid("7AA0E540-0402-11D4-A30E-00C026EE094F")]
//[TypeLibType(TypeLibTypeFlags.FHidden)]
//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[ComImport]
//public interface ISplineDefinition
//{
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetCountVertex();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddVertex(double x, double y, double z, double radius);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int InsertVertex(int index, double x, double y, double z, double radius);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int DeleteVertex(int index);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetParamVertex(int index, ref double x, ref double y, ref double z, ref double weight);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int Flush();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetClosed(int cls);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetClosed();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int ReadFromFile([MarshalAs(UnmanagedType.LPWStr)] string fileName);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int WriteToFile([MarshalAs(UnmanagedType.LPWStr)] string fileName);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetSplineOnPoles(int splineOnPoles);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetSplineOnPoles();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetDegree(int degree);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int GetDegree();

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddVertexAndAssociation(int index, [MarshalAs(UnmanagedType.Interface)] IEntity obj, double weight);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetAssociation(int index, [MarshalAs(UnmanagedType.Interface)] IEntity obj);

//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  IEntity GetAssociation(int index);
//}
