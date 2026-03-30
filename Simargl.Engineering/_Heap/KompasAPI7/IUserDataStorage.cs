//// Decompiled with JetBrains decompiler
//// Type: KompasAPI7.IUserDataStorage
//// Assembly: KompasAPI7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: F94F641F-15B9-43AE-BE0A-DA3530B2CFA7
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\KompasAPI7.dll

//using Kompas6Constants;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace KompasAPI7;

//[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
//[Guid("DD2AF5C4-D5B9-420D-B658-F935F80A8586")]
//[ComImport]
//public interface IUserDataStorage : IKompasAPIObject
//{
//  [DispId(1000)]
//  new IKompasAPIObject Parent { [DispId(1000), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1001)]
//  new IApplication Application { [DispId(1001), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

//  [DispId(1002)]
//  new KompasAPIObjectTypeEnum Type { [DispId(1002), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1003)]
//  new int Reference { [DispId(1003), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string get_Name([MarshalAs(UnmanagedType.BStr), In] string Pass);

//  [DispId(1)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_Name([MarshalAs(UnmanagedType.BStr), In] string Pass, [MarshalAs(UnmanagedType.BStr), In] string PVal);

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool SetPassword([MarshalAs(UnmanagedType.BStr), In] string OldPass, [MarshalAs(UnmanagedType.BStr), In] string NewPass);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int get_Version([MarshalAs(UnmanagedType.BStr), In] string Pass);

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  void set_Version([MarshalAs(UnmanagedType.BStr), In] string Pass, [In] int PVal);

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  string GetObject([MarshalAs(UnmanagedType.Struct), In] object Index, [MarshalAs(UnmanagedType.Struct)] out object Object, out int Numb);

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int SetObject([MarshalAs(UnmanagedType.BStr), In] string Pass, [MarshalAs(UnmanagedType.Struct), In] object Index, [MarshalAs(UnmanagedType.Struct), In] object Object, [MarshalAs(UnmanagedType.BStr), In] string Comment);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  int AddObject([MarshalAs(UnmanagedType.BStr), In] string Pass, [MarshalAs(UnmanagedType.Struct), In] object Object, [MarshalAs(UnmanagedType.BStr), In] string Comment);

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Delete([MarshalAs(UnmanagedType.BStr), In] string Pass, [MarshalAs(UnmanagedType.Struct), In] object Index);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool Clear([MarshalAs(UnmanagedType.BStr), In] string Pass);

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  bool AddObjects([MarshalAs(UnmanagedType.BStr), In] string Pass, [MarshalAs(UnmanagedType.Struct), In] object Objects, [MarshalAs(UnmanagedType.Struct), In] object Comments);
//}
