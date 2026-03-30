//// Decompiled with JetBrains decompiler
//// Type: Kompas6API5.Document3DClass
//// Assembly: Kompas6API5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 7DC79A90-5904-4611-83C4-D5C1F1F81B44
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6API5.dll

//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6API5;

//[ClassInterface(ClassInterfaceType.None)]
//[ComSourceInterfaces("Kompas6API5.ksDocumentFileNotify\0\0")]
//[Guid("111CEFE3-A0A7-11D6-95CE-00C0262D30E3")]
//[ComImport]
//public class Document3DClass : ksDocument3D, Document3D, ksDocumentFileNotify_Event
//{
//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  internal extern Document3DClass();

//  [DispId(1)]
//  public virtual extern string fileName { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr)] set; }

//  [DispId(2)]
//  public virtual extern string comment { [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(2), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr)] set; }

//  [DispId(3)]
//  public virtual extern string author { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr)] set; }

//  [DispId(39)]
//  public virtual extern int reference { [DispId(39), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(39), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(40)]
//  public virtual extern bool perspective { [DispId(40), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(40), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(41)]
//  public virtual extern int drawMode { [DispId(41), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(41), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(50)]
//  public virtual extern bool hideAllPlanes { [DispId(50), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(50), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(51)]
//  public virtual extern bool hideAllAxis { [DispId(51), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(51), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(52)]
//  public virtual extern bool hideAllSketches { [DispId(52), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(52), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(53)]
//  public virtual extern bool hideAllPlaces { [DispId(53), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(53), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(54)]
//  public virtual extern bool hideAllSurfaces { [DispId(54), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(54), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(55)]
//  public virtual extern bool hideAllThreads { [DispId(55), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(55), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(63 /*0x3F*/)]
//  public virtual extern bool shadedWireframe { [DispId(63 /*0x3F*/), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(63 /*0x3F*/), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(68)]
//  public virtual extern bool dismantleMode { [DispId(68), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(68), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(70)]
//  public virtual extern bool treeNeedRebuild { [DispId(70), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(70), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(75)]
//  public virtual extern bool enableRollBackFeaturesInCollections { [DispId(75), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(75), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(77)]
//  public virtual extern bool hideAllCurves { [DispId(77), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(77), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(78)]
//  public virtual extern bool hideAllControlPoints { [DispId(78), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(78), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(79)]
//  public virtual extern bool hideAllDimensions { [DispId(79), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(79), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(80 /*0x50*/)]
//  public virtual extern bool hideAllDesignations { [DispId(80 /*0x50*/), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(80 /*0x50*/), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(81)]
//  public virtual extern bool hideAllAuxiliaryGeom { [DispId(81), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(81), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(82)]
//  public virtual extern bool hideInComponentsMode { [DispId(82), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(82), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(84)]
//  public virtual extern bool windowNeedRebuild { [DispId(84), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(84), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(85)]
//  public virtual extern bool hideLayoutGeometry { [DispId(85), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(85), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(90)]
//  public virtual extern bool matesMultiCreatedMode { [DispId(90), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [DispId(90), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] set; }

//  [DispId(4)]
//  public virtual extern bool invisibleMode { [TypeLibFunc(TypeLibFuncFlags.FBindable | TypeLibFuncFlags.FDisplayBind | TypeLibFuncFlags.FDefaultBind), DispId(4), MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

//  [DispId(5)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Create(bool invisible = false, bool typeDoc = true);

//  [DispId(6)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsDetail();

//  [DispId(7)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetPart(int type);

//  [DispId(8)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object MateConstraintCollection();

//  [DispId(9)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool UpdateDocumentParam();

//  [DispId(10)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsEditMode();

//  [DispId(11)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ChangeObjectInLibRequest();

//  [DispId(12)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool StopLibRequest();

//  [DispId(13)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetSpecification();

//  [DispId(14)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetRequestInfo([MarshalAs(UnmanagedType.IDispatch)] object part);

//  [DispId(15)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool UserGetPlacementAndEntity(int entityCount);

//  [DispId(16 /*0x10*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetActive();

//  [DispId(17)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool IsActive();

//  [DispId(18)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Open([MarshalAs(UnmanagedType.BStr)] string fileName, bool invisible = false);

//  [DispId(19)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool Save();

//  [DispId(20)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SaveAs([MarshalAs(UnmanagedType.BStr)] string fileName);

//  [DispId(21)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool close();

//  [DispId(22)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool UserGetCursor(
//    [MarshalAs(UnmanagedType.BStr), In] string prompt,
//    out double x,
//    out double y,
//    out double z);

//  [DispId(23)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetPartFromFile([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object part, bool externalFile = true);

//  [DispId(24)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object EntityCollection(short objType = 0, bool checkEntity = true);

//  [DispId(25)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object UserSelectEntity(
//    [MarshalAs(UnmanagedType.IDispatch)] object filterObject,
//    [MarshalAs(UnmanagedType.BStr)] string methodName,
//    [MarshalAs(UnmanagedType.BStr)] string prompt,
//    int hInst = 0,
//    [IDispatchConstant, MarshalAs(UnmanagedType.IDispatch), Optional] object val);

//  [DispId(26)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool RemoveMateConstraint(int constraintType, [MarshalAs(UnmanagedType.IDispatch)] object obj1, [MarshalAs(UnmanagedType.IDispatch)] object obj2);

//  [DispId(27)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool AddMateConstraint(
//    int constraintType,
//    [MarshalAs(UnmanagedType.IDispatch)] object obj1,
//    [MarshalAs(UnmanagedType.IDispatch)] object obj2,
//    short direction = 0,
//    short @fixed = 0,
//    double val = 0.0);

//  [DispId(28)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksDeleteObj(int @ref);

//  [DispId(29)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object PartCollection(bool refresh);

//  [DispId(30)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool CreatePartFromFile([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object part, [MarshalAs(UnmanagedType.IDispatch)] object plane);

//  [DispId(31 /*0x1F*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object CreatePartInAssembly([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object plane);

//  [DispId(32 /*0x20*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool RebuildDocument();

//  [DispId(33)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ZoomPrevNextOrAll(short type);

//  [DispId(34)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetMateConstraint();

//  [DispId(35)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SaveAsToRasterFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object rasterPar);

//  [DispId(36)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object RasterFormatParam();

//  [DispId(37)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SaveAsToAdditionFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object additionPar);

//  [DispId(38)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object AdditionFormatParam();

//  [DispId(42)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetViewProjectionCollection();

//  [DispId(43)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool DeleteObject([MarshalAs(UnmanagedType.IDispatch)] object obj);

//  [DispId(44)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetSelectionMng();

//  [DispId(45)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetChooseMng();

//  [DispId(46)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int GetObjectType([MarshalAs(UnmanagedType.IDispatch)] object obj);

//  [DispId(47)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksGetObjParam(int @ref, [MarshalAs(UnmanagedType.IDispatch)] object param, int parType = -1);

//  [DispId(48 /*0x30*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksSetObjParam(int referObj, [MarshalAs(UnmanagedType.IDispatch)] object param, int parType = -1);

//  [DispId(49)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SaveAsToUncompressedRasterFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object rasterPar);

//  [DispId(56)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object AddImportedSurfaces([MarshalAs(UnmanagedType.BStr)] string fileName, bool together);

//  [DispId(57)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IUnknown)]
//  public virtual extern object GetDocument3DNotify();

//  [DispId(58)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Attribute3DCollection AttributeCollection(
//    int key1,
//    int key2,
//    int key3,
//    int key4,
//    double numb,
//    [MarshalAs(UnmanagedType.IDispatch)] object pObj);

//  [DispId(59)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Kompas6API5.FeatureCollection FeatureCollection(
//    int key1,
//    int key2,
//    int key3,
//    int key4,
//    double numb,
//    int objType);

//  [DispId(60)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetPartFromFileEx(
//    [MarshalAs(UnmanagedType.BStr)] string fileName,
//    [MarshalAs(UnmanagedType.IDispatch)] object part,
//    bool externalFile = true,
//    bool redraw = false);

//  [DispId(61)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern Kompas6API5.ComponentPositioner ComponentPositioner();

//  [DispId(62)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.Interface)]
//  public virtual extern placement DefaultPlacement();

//  [DispId(64 /*0x40*/)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern int ksIsSlaveSpcOpened();

//  [DispId(65)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetEditMacroObject();

//  [DispId(66)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SaveAsEx([MarshalAs(UnmanagedType.BStr)] string fileName, int SaveMode);

//  [DispId(67)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetInterface(int o3dType);

//  [DispId(69)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object CopyPart([MarshalAs(UnmanagedType.IDispatch)] object sourcePart, [MarshalAs(UnmanagedType.IDispatch)] object newPlacement);

//  [DispId(71)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object UserSelectEntityEx(
//    [MarshalAs(UnmanagedType.IDispatch)] object filterObject,
//    [MarshalAs(UnmanagedType.BStr)] string methodName,
//    [MarshalAs(UnmanagedType.BStr)] string prompt,
//    int hInst = 0,
//    [IDispatchConstant, MarshalAs(UnmanagedType.IDispatch), Optional] object val,
//    [IUnknownConstant, MarshalAs(UnmanagedType.IUnknown), Optional] object processParam);

//  [DispId(72)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool PlaceFeatureAfter([MarshalAs(UnmanagedType.IDispatch)] object obj, [MarshalAs(UnmanagedType.IDispatch)] object afterObj);

//  [DispId(73)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool SetRollBackFeature([MarshalAs(UnmanagedType.IDispatch)] object obj);

//  [DispId(74)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetRollBackFeature();

//  [DispId(76)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool ExcludeFeaturesAfter([MarshalAs(UnmanagedType.IDispatch)] object obj, bool exclude);

//  [DispId(83)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetDocument3DNotifyResult();

//  [DispId(86)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object UserSelectEntityEx2(
//    [MarshalAs(UnmanagedType.IDispatch)] object filterObject,
//    [MarshalAs(UnmanagedType.BStr)] string methodName,
//    [MarshalAs(UnmanagedType.BStr)] string prompt,
//    [MarshalAs(UnmanagedType.Struct)] object hInst,
//    [IDispatchConstant, MarshalAs(UnmanagedType.IDispatch), Optional] object val,
//    [IUnknownConstant, MarshalAs(UnmanagedType.IUnknown), Optional] object processParam);

//  [DispId(87)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool RunTakeCreateObjectProc(
//    int processType,
//    [MarshalAs(UnmanagedType.IDispatch)] object takeObject,
//    bool needCreateTakeObj,
//    bool lostTakeObj);

//  [DispId(88)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern bool LoadFromAdditionFormat([MarshalAs(UnmanagedType.BStr)] string fileName, [MarshalAs(UnmanagedType.IDispatch)] object additionPar);

//  [DispId(89)]
//  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  [return: MarshalAs(UnmanagedType.IDispatch)]
//  public virtual extern object GetLastFeature();

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginCloseDocument(
//    [In] ksDocumentFileNotify_BeginCloseDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginCloseDocumentEventHandler BeginCloseDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginCloseDocument(
//    [In] ksDocumentFileNotify_BeginCloseDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_CloseDocument(
//    [In] ksDocumentFileNotify_CloseDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_CloseDocumentEventHandler CloseDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_CloseDocument(
//    [In] ksDocumentFileNotify_CloseDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginSaveDocument(
//    [In] ksDocumentFileNotify_BeginSaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginSaveDocumentEventHandler BeginSaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginSaveDocument(
//    [In] ksDocumentFileNotify_BeginSaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_SaveDocument([In] ksDocumentFileNotify_SaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_SaveDocumentEventHandler SaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_SaveDocument([In] ksDocumentFileNotify_SaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_ActivateEventHandler Activate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Activate([In] ksDocumentFileNotify_ActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Activate([In] ksDocumentFileNotify_ActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_Deactivate([In] ksDocumentFileNotify_DeactivateEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_DeactivateEventHandler Deactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_Deactivate([In] ksDocumentFileNotify_DeactivateEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginSaveAsDocumentEventHandler BeginSaveAsDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginSaveAsDocument(
//    [In] ksDocumentFileNotify_BeginSaveAsDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginSaveAsDocument(
//    [In] ksDocumentFileNotify_BeginSaveAsDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_DocumentFrameOpenEventHandler DocumentFrameOpen;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_DocumentFrameOpen(
//    [In] ksDocumentFileNotify_DocumentFrameOpenEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_DocumentFrameOpen(
//    [In] ksDocumentFileNotify_DocumentFrameOpenEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_ProcessActivateEventHandler ProcessActivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ProcessActivate(
//    [In] ksDocumentFileNotify_ProcessActivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ProcessActivate(
//    [In] ksDocumentFileNotify_ProcessActivateEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_ProcessDeactivateEventHandler ProcessDeactivate;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_ProcessDeactivate(
//    [In] ksDocumentFileNotify_ProcessDeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_ProcessDeactivate(
//    [In] ksDocumentFileNotify_ProcessDeactivateEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginProcess([In] ksDocumentFileNotify_BeginProcessEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginProcessEventHandler BeginProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginProcess([In] ksDocumentFileNotify_BeginProcessEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_EndProcessEventHandler EndProcess;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_EndProcess([In] ksDocumentFileNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_EndProcess([In] ksDocumentFileNotify_EndProcessEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_BeginAutoSaveDocument(
//    [In] ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler BeginAutoSaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_BeginAutoSaveDocument(
//    [In] ksDocumentFileNotify_BeginAutoSaveDocumentEventHandler obj0);

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void add_AutoSaveDocument(
//    [In] ksDocumentFileNotify_AutoSaveDocumentEventHandler obj0);

//  public virtual extern event ksDocumentFileNotify_AutoSaveDocumentEventHandler AutoSaveDocument;

//  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
//  public virtual extern void remove_AutoSaveDocument(
//    [In] ksDocumentFileNotify_AutoSaveDocumentEventHandler obj0);
//}
