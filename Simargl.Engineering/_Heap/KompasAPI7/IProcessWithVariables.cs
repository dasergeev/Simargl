//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IProcessWithVariables
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[Guid("23A5DFEE-5E95-4F8D-9CAE-805E707F1EF8")]
//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[ComImport]
//public interface IProcessWithVariables
//{
//  [DispId(2001)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetControlExpression([MarshalAs(UnmanagedType.Interface), In] IPropertyControl Control, [MarshalAs(UnmanagedType.BStr), In] string VariableName, [MarshalAs(UnmanagedType.BStr), In] string Expression);

//  [DispId(2002)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetControlExpression([MarshalAs(UnmanagedType.Interface), In] IPropertyControl Control);

//  [DispId(2003)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetControlVariableName([MarshalAs(UnmanagedType.Interface), In] IPropertyControl Control);

//  [DispId(2004)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool ClearExpressions();

//  [DispId(2005)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool UpdateExpressionsControls();
//}
