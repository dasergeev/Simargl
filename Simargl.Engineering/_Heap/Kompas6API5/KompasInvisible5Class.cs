//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.KompasInvisible5Class
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[TypeLibType(TypeLibTypeFlags.FCanCreate)]
//[Guid("FBE002A6-1E06-4703-AEC5-9AD8A10FA1FA")]
//[ClassInterface(ClassInterfaceType.None)]
//[ComSourceInterfaces("Kompas6API5.ksKompasObjectNotify\0\0")]
//[ComImport]
//public class KompasInvisible5Class : KompasObject, KompasInvisible5, ksKompasObjectNotify_Event
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public extern KompasInvisible5Class();

//  [DispId(1)]
//  public virtual extern bool Visible { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(99)]
//  public virtual extern int lookStyle { [DispId(99), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(99), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(137)]
//  public virtual extern string currentDirectory { [DispId(137), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(137), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr)] set; }

//  [DispId(2)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object Document3D();

//  [DispId(3)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ActiveDocument3D();

//  [DispId(4)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object Document2D();

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ActiveDocument2D();

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object DataBaseObject();

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetIterator();

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetMathematic2D();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetParamStruct(short structType);

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object SpcDocument();

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object SpcActiveDocument();

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksMessage([MarshalAs(UnmanagedType.BStr)] string s);

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksError([MarshalAs(UnmanagedType.BStr)] string s);

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksEnableTaskAccess(int enabl = 1);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReturnResult();

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksResultNULL();

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsEnableTaskAccess();

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksMessageBoxResult();

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawSlide(int HWindow, int sldID);

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksStrResult();

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetHWindow();

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetWorkWindowColor();

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksPumpWaitingMessages();

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetCriticalProcess();

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksChoiceFile([MarshalAs(UnmanagedType.BStr)] string ext, [MarshalAs(UnmanagedType.BStr)] string filter, bool preview);

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawBitmap(int HWindow, int sldID);

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksYesNo([MarshalAs(UnmanagedType.BStr)] string s);

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawSlideFromFile(int HWindow, [MarshalAs(UnmanagedType.BStr)] string fileName);

//  [DispId(29)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksWriteSlide([MarshalAs(UnmanagedType.BStr)] string fileName, int iD, double x, double y);

//  [DispId(30)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSlideBackground(int color);

//  [DispId(31 /*0x1F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReadDouble(
//    [MarshalAs(UnmanagedType.BStr)] string mess,
//    double defValue,
//    double min,
//    double max,
//    ref double value);

//  [DispId(32 /*0x20*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksReadInt([MarshalAs(UnmanagedType.BStr)] string mess, int defValue, int min, int max, ref int value);

//  [DispId(33)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksReadString([MarshalAs(UnmanagedType.BStr)] string mess, [MarshalAs(UnmanagedType.BStr)] string value);

//  [DispId(34)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksFullFileName([MarshalAs(UnmanagedType.BStr)] string oldName);

//  [DispId(35)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksUniqueFileName();

//  [DispId(36)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksRemoveUniqueFile([MarshalAs(UnmanagedType.BStr)] string fileName);

//  [DispId(37)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSystemControlStop();

//  [DispId(38)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSystemControlStart([MarshalAs(UnmanagedType.BStr)] string menuCommand);

//  [DispId(39)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetDynamicArray(int type);

//  [DispId(40)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksChoiceFileAppointedDir(
//    [MarshalAs(UnmanagedType.BStr)] string ext,
//    [MarshalAs(UnmanagedType.BStr)] string filter,
//    bool preview,
//    int typeDir);

//  [DispId(41)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksChoiceFiles([MarshalAs(UnmanagedType.BStr)] string ext, [MarshalAs(UnmanagedType.BStr)] string filter, [MarshalAs(UnmanagedType.IDispatch)] object p, bool preview);

//  [DispId(42)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksSaveFile([MarshalAs(UnmanagedType.BStr)] string ext, [MarshalAs(UnmanagedType.BStr)] string oldName, [MarshalAs(UnmanagedType.BStr)] string filter, bool preview);

//  [DispId(43)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetAttributeObject();

//  [DispId(44)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksRefreshActiveWindow();

//  [DispId(45)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksSystemPath(int pathType);

//  [DispId(46)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetRelativePathFromSystemPath([MarshalAs(UnmanagedType.BStr)] string sourcePath, int pathType);

//  [DispId(47)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetFullPathFromSystemPath([MarshalAs(UnmanagedType.BStr)] string relativePath, int pathType);

//  [DispId(48 /*0x30*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetRelativePathFromFullPath([MarshalAs(UnmanagedType.BStr)] string mainFilePath, [MarshalAs(UnmanagedType.BStr)] string sourcePath);

//  [DispId(49)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetFullPathFromRelativePath(
//    [MarshalAs(UnmanagedType.BStr)] string mainFilePath,
//    [MarshalAs(UnmanagedType.BStr)] string relativePath);

//  [DispId(50)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSystemVersion(
//    out int iMajor,
//    out int iMinor,
//    out int iRelease,
//    out int iBuild);

//  [DispId(51)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetSystemProfileString([MarshalAs(UnmanagedType.BStr)] string lpSection, [MarshalAs(UnmanagedType.BStr)] string lpKey);

//  [DispId(52)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksOpenHelpFile([MarshalAs(UnmanagedType.BStr)] string file, int command, int iD);

//  [DispId(53)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetSysOptions(int optionsType, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(54)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSysOptions(int optionsType, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(55)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPrintPreviewWindow([MarshalAs(UnmanagedType.IDispatch)] object docsArr, int inquiry);

//  [DispId(56)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetLibraryStylesArray([MarshalAs(UnmanagedType.BStr)] string libraryName, short libraryType);

//  [DispId(57)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern double ksViewGetDensity(int HWindow);

//  [DispId(58)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCalculate([MarshalAs(UnmanagedType.BStr)] string s, out double rez);

//  [DispId(59)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksCalculateReset();

//  [DispId(60)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExecuteLibraryCommand([MarshalAs(UnmanagedType.BStr)] string fileName, int command);

//  [DispId(61)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetQualityNames(
//    [MarshalAs(UnmanagedType.IDispatch)] object names,
//    double dimValue,
//    double high,
//    double low,
//    short system,
//    short withLimitation);

//  [DispId(62)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetQualityDefects(
//    [MarshalAs(UnmanagedType.BStr), In] string name,
//    [In] double dimValue,
//    out double high,
//    out double low,
//    [In] short inMM);

//  [DispId(63 /*0x3F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetQualityContensParam([MarshalAs(UnmanagedType.BStr)] string name, [MarshalAs(UnmanagedType.IDispatch)] object param, short inMM);

//  [DispId(64 /*0x40*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGet3dDocumentFromRef(int doc);

//  [DispId(65)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetModelLibrary();

//  [DispId(66)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetFragmentLibrary();

//  [DispId(67)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsLibraryEnabled([MarshalAs(UnmanagedType.BStr)] string libName);

//  [DispId(68)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsModuleSpecificationActive();

//  [DispId(69)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksModuleSpecification(bool attach);

//  [DispId(70)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksMaterialDlg(
//    int HWindow,
//    out int res,
//    out double plt,
//    out double kod_size_1,
//    out double kod_size_2,
//    out double kod_size_3,
//    out double kod_size_4,
//    [MarshalAs(UnmanagedType.BStr), In] string kod_tip);

//  [DispId(71)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksExecDialPredefinedText([In] int HWindow, out int res);

//  [DispId(72)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawSlideEx(int HWindow, int sldID, int hInst);

//  [DispId(73)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksViewGetDensityAndMaterial(out double density, [In] int HWindow);

//  [DispId(74)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksConvertLangStr([MarshalAs(UnmanagedType.BStr)] string src);

//  [DispId(75)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksConvertLangWindow(int hWnd);

//  [DispId(76)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksConvertLangMenu(int hMenu);

//  [DispId(77)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksEditTextLine([In] int HWindow, out int res, [MarshalAs(UnmanagedType.BStr), In] string str);

//  [DispId(78)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetDocOptions(int optionsType, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(79)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksAttachKompasLibrary([MarshalAs(UnmanagedType.BStr)] string libName);

//  [DispId(80 /*0x50*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDetachKompasLibrary(int libId);

//  [DispId(81)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExecuteKompasLibraryCommand(int libId, int command);

//  [DispId(82)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void Quit();

//  [DispId(83)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPrintKompasDocument([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.BStr)] string toFile, double scale);

//  [DispId(84)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawKompasDocument(int HWindow, [MarshalAs(UnmanagedType.BStr)] string docFileName);

//  [DispId(85)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSetFlagDisableLockApp(bool setDisableLockApp);

//  [DispId(86)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ActivateControllerAPI();

//  [DispId(87)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object DocumentTxt();

//  [DispId(88)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ActiveDocumentTxt();

//  [DispId(89)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksGetLibraryTreeStruct([MarshalAs(UnmanagedType.BStr)] string libName, [MarshalAs(UnmanagedType.IDispatch)] object p);

//  [DispId(90)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetDocOptions(int optionsType, [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(91)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetDocumentType(int doc = 0);

//  [DispId(92)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawKompasDocumentByReference(int HWindow, int pDoc);

//  [DispId(93)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksExecDialPredefinedTextEx(int HWindow);

//  [DispId(94)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetDocumentTypeByName([MarshalAs(UnmanagedType.BStr)] string fileName);

//  [DispId(95)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetDocumentByReference(int docRef);

//  [DispId(96 /*0x60*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksConvertLangStrEx(int hInstance, int strID);

//  [DispId(97)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksConvertLangWindowEx(int hWnd, int hInstance, [MarshalAs(UnmanagedType.BStr)] string dlgID);

//  [DispId(98)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool LoadDSK();

//  [DispId(100)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetObjectsFilter3D();

//  [DispId(101)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExecuteKompasLibraryCommandEx(
//    int libId,
//    int command,
//    [MarshalAs(UnmanagedType.IDispatch)] object external);

//  [DispId(102)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetExternaldispinterface();

//  [DispId(103)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object ksGetApplication7();

//  [DispId(104)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawBitmapEx(int HWindow, int bmpID, int hInst);

//  [DispId(105)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetSystemControlStartResult();

//  [DispId(106)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsModule3DActive();

//  [DispId(107)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksModule3D(bool attach);

//  [DispId(108)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  public virtual extern object TransferInterface([MarshalAs(UnmanagedType.IUnknown)] object obj, int apiNewType, int objNewType);

//  [DispId(109)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksExecuteKompasCommand(int commandID, bool post);

//  [DispId(110)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksIsKompasCommandEnable(int commandID);

//  [DispId(111)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsKompasCommandCheck(int commandID);

//  [DispId(112 /*0x70*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  public virtual extern object TransferReference(int obj, int docRef);

//  [DispId(113)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawKompasText(int HWindow, [MarshalAs(UnmanagedType.BStr)] string text);

//  [DispId(114)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksPrintKompasDocumentEx(
//    [MarshalAs(UnmanagedType.BStr)] string fileName,
//    [MarshalAs(UnmanagedType.BStr)] string toFile,
//    double scale,
//    bool FKompasPrinter);

//  [DispId(115)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExecDialSpecialSymbol(int HWindow);

//  [DispId(116)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksExecDialSymbol([In] int HWindow, out int symb, [MarshalAs(UnmanagedType.BStr), In] string font);

//  [DispId(117)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksSetDebugMessagesMode(bool debugMode);

//  [DispId(118)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksClearFileCache();

//  [DispId(119)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksLockFileCache(bool @lock);

//  [DispId(120)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksIsHomeVersion();

//  [DispId(121)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksIsModule2DActive();

//  [DispId(122)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExecDialPointStyleSelect(int HWindow, int style);

//  [DispId(123)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksExecDialLineStyleSelect(int HWindow, [MarshalAs(UnmanagedType.BStr)] string caption, int style);

//  [DispId(124)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksExecQualityDialog(
//    int HWindow,
//    [MarshalAs(UnmanagedType.BStr)] string curQual,
//    ref double dimValue,
//    int inMM,
//    [MarshalAs(UnmanagedType.IDispatch)] object param);

//  [DispId(125)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksLockPumpMessages(bool @lock);

//  [DispId(126)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksIsStudyVersion();

//  [DispId(127 /*0x7F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksIsSpdsVersion();

//  [DispId(128 /*0x80*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksKompasVariant();

//  [DispId(129)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawBitmapEx2(int HWindow, int bmpID, [MarshalAs(UnmanagedType.Struct)] object hInst);

//  [DispId(130)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDrawSlideEx2(int HWindow, int sldID, [MarshalAs(UnmanagedType.Struct)] object hInst);

//  [DispId(131)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksConvertLangStrEx2([MarshalAs(UnmanagedType.Struct)] object hInstance, int strID);

//  [DispId(132)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksConvertLangWindowEx2(int hWnd, [MarshalAs(UnmanagedType.Struct)] object hInstance, [MarshalAs(UnmanagedType.BStr)] string dlgID);

//  [DispId(133)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksSelectD3Model(bool onlyDetail, bool showAddNum);

//  [DispId(134)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetSelectedEmbodimentMarking();

//  [DispId(135)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetSelectedEmbodimentAdditionalNumber();

//  [DispId(136)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ksEnableKompasInvisible([MarshalAs(UnmanagedType.BStr)] string key, [MarshalAs(UnmanagedType.BStr)] string signature);

//  [DispId(138)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.BStr)]
//  public virtual extern string ksGetDocumentTypeByNameEx(
//    [MarshalAs(UnmanagedType.BStr)] string fileName,
//    ref int docType,
//    ref int errorId);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CreateDocument(
//    [In] ksKompasObjectNotify_CreateDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_CreateDocumentEventHandler CreateDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CreateDocument(
//    [In] ksKompasObjectNotify_CreateDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginOpenDocument(
//    [In] ksKompasObjectNotify_BeginOpenDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginOpenDocumentEventHandler BeginOpenDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginOpenDocument(
//    [In] ksKompasObjectNotify_BeginOpenDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_OpenDocument([In] ksKompasObjectNotify_OpenDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_OpenDocumentEventHandler OpenDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_OpenDocument([In] ksKompasObjectNotify_OpenDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeActiveDocument(
//    [In] ksKompasObjectNotify_ChangeActiveDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_ChangeActiveDocumentEventHandler ChangeActiveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeActiveDocument(
//    [In] ksKompasObjectNotify_ChangeActiveDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_ApplicationDestroyEventHandler ApplicationDestroy;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ApplicationDestroy(
//    [In] ksKompasObjectNotify_ApplicationDestroyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ApplicationDestroy(
//    [In] ksKompasObjectNotify_ApplicationDestroyEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCreate([In] ksKompasObjectNotify_BeginCreateEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginCreateEventHandler BeginCreate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCreate([In] ksKompasObjectNotify_BeginCreateEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginOpenFileEventHandler BeginOpenFile;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginOpenFile(
//    [In] ksKompasObjectNotify_BeginOpenFileEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginOpenFile(
//    [In] ksKompasObjectNotify_BeginOpenFileEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCloseAllDocument(
//    [In] ksKompasObjectNotify_BeginCloseAllDocumentEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginCloseAllDocumentEventHandler BeginCloseAllDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCloseAllDocument(
//    [In] ksKompasObjectNotify_BeginCloseAllDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_KeyDown([In] ksKompasObjectNotify_KeyDownEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_KeyDownEventHandler KeyDown;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_KeyDown([In] ksKompasObjectNotify_KeyDownEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_KeyUp([In] ksKompasObjectNotify_KeyUpEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_KeyUpEventHandler KeyUp;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_KeyUp([In] ksKompasObjectNotify_KeyUpEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_KeyPressEventHandler KeyPress;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_KeyPress([In] ksKompasObjectNotify_KeyPressEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_KeyPress([In] ksKompasObjectNotify_KeyPressEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginReguestFilesEventHandler BeginReguestFiles;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginReguestFiles(
//    [In] ksKompasObjectNotify_BeginReguestFilesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginReguestFiles(
//    [In] ksKompasObjectNotify_BeginReguestFilesEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginChoiceMaterial(
//    [In] ksKompasObjectNotify_BeginChoiceMaterialEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginChoiceMaterialEventHandler BeginChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginChoiceMaterial(
//    [In] ksKompasObjectNotify_BeginChoiceMaterialEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_ChoiceMaterialEventHandler ChoiceMaterial;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChoiceMaterial(
//    [In] ksKompasObjectNotify_ChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChoiceMaterial(
//    [In] ksKompasObjectNotify_ChoiceMaterialEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_IsNeedConvertToSavePrevious(
//    [In] ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler IsNeedConvertToSavePrevious;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_IsNeedConvertToSavePrevious(
//    [In] ksKompasObjectNotify_IsNeedConvertToSavePreviousEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginConvertToSavePrevious(
//    [In] ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler BeginConvertToSavePrevious;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginConvertToSavePrevious(
//    [In] ksKompasObjectNotify_BeginConvertToSavePreviousEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_EndConvertToSavePreviousEventHandler EndConvertToSavePrevious;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndConvertToSavePrevious(
//    [In] ksKompasObjectNotify_EndConvertToSavePreviousEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndConvertToSavePrevious(
//    [In] ksKompasObjectNotify_EndConvertToSavePreviousEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ChangeTheme([In] ksKompasObjectNotify_ChangeThemeEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_ChangeThemeEventHandler ChangeTheme;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ChangeTheme([In] ksKompasObjectNotify_ChangeThemeEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginDragOpenFiles(
//    [In] ksKompasObjectNotify_BeginDragOpenFilesEventHandler obj0);

//  public virtual extern event ksKompasObjectNotify_BeginDragOpenFilesEventHandler BeginDragOpenFiles;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginDragOpenFiles(
//    [In] ksKompasObjectNotify_BeginDragOpenFilesEventHandler obj0);
//}
