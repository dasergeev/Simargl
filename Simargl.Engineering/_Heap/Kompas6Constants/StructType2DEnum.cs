//// Decompiled with JetBrains decompiler
//// Type: Kompas6Constants.StructType2DEnum
//// Assembly: Kompas6Constants, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 149B6212-B704-4141-A62B-2837362A2965
//// Assembly location: D:\Shared\SDK\Samples\CSharp\Common\Kompas6Constants.dll

//using System.Runtime.InteropServices;

//#nullable disable
//namespace Kompas6Constants;

//[Guid("9637E1BB-CF0F-4DC7-8425-3F3957EB313B")]
//public enum StructType2DEnum
//{
//  ko_Type1 = 1,
//  ko_Type2 = 2,
//  ko_Type3 = 3,
//  ko_Type5 = 4,
//  ko_Type6 = 5,
//  ko_Phantom = 6,
//  ko_PlacementParam = 7,
//  ko_ViewParam = 8,
//  ko_LayerParam = 9,
//  ko_RequestInfo = 10, // 0x0000000A
//  ko_LineSegParam = 11, // 0x0000000B
//  ko_ArcByAngleParam = 12, // 0x0000000C
//  ko_ArcByPointParam = 13, // 0x0000000D
//  ko_MathPointParam = 14, // 0x0000000E
//  ko_RectParam = 15, // 0x0000000F
//  ko_PointParam = 16, // 0x00000010
//  ko_BezierPointParam = 17, // 0x00000011
//  ko_NurbsPointParam = 18, // 0x00000012
//  ko_BezierParam = 19, // 0x00000013
//  ko_CircleParam = 20, // 0x00000014
//  ko_LineParam = 21, // 0x00000015
//  ko_EllipseParam = 22, // 0x00000016
//  ko_EllipsArcParam = 23, // 0x00000017
//  ko_EllipsArcParam1 = 24, // 0x00000018
//  ko_EquidParam = 25, // 0x00000019
//  ko_HatchParam = 26, // 0x0000001A
//  ko_ParagraphParam = 27, // 0x0000001B
//  ko_TextParam = 28, // 0x0000001C
//  ko_TextLineParam = 29, // 0x0000001D
//  ko_TextItemFont = 30, // 0x0000001E
//  ko_TextItemParam = 31, // 0x0000001F
//  ko_StandartSheet = 32, // 0x00000020
//  ko_SheetSize = 33, // 0x00000021
//  ko_SheetPar = 34, // 0x00000022
//  ko_DocumentParam = 35, // 0x00000023
//  ko_ColumnInfoParam = 36, // 0x00000024
//  ko_AttributeType = 37, // 0x00000025
//  ko_Attribute = 38, // 0x00000026
//  ko_LibraryAttrTypeParam = 39, // 0x00000027
//  ko_TAN = 40, // 0x00000028
//  ko_CON = 41, // 0x00000029
//  ko_DimText = 42, // 0x0000002A
//  ko_LDimSource = 43, // 0x0000002B
//  ko_DimDrawing = 44, // 0x0000002C
//  ko_LDimParam = 45, // 0x0000002D
//  ko_LBreakDimSource = 46, // 0x0000002E
//  ko_BreakDimDrawing = 47, // 0x0000002F
//  ko_LBreakDimParam = 48, // 0x00000030
//  ko_ADimSource = 49, // 0x00000031
//  ko_ADimParam = 50, // 0x00000032
//  ko_ABreakDimParam = 51, // 0x00000033
//  ko_RDimSource = 52, // 0x00000034
//  ko_RDimDrawing = 53, // 0x00000035
//  ko_RDimParam = 54, // 0x00000036
//  ko_RBreakDrawing = 55, // 0x00000037
//  ko_RBreakDimParam = 56, // 0x00000038
//  ko_RoughPar = 57, // 0x00000039
//  ko_ShelfPar = 58, // 0x0000003A
//  ko_RoughParam = 59, // 0x0000003B
//  ko_LeaderParam = 60, // 0x0000003C
//  ko_PosLeaderParam = 61, // 0x0000003D
//  ko_BrandLeaderParam = 62, // 0x0000003E
//  ko_MarkerLeaderParam = 63, // 0x0000003F
//  ko_BaseParam = 64, // 0x00000040
//  ko_CutLineParam = 65, // 0x00000041
//  ko_ViewPointerParam = 66, // 0x00000042
//  ko_ToleranceBranch = 67, // 0x00000043
//  ko_ToleranceParam = 68, // 0x00000044
//  ko_CurvePattern = 69, // 0x00000045
//  ko_CurvePicture = 70, // 0x00000046
//  ko_CurvePatternEx = 71, // 0x00000047
//  ko_CurveStyleParam = 72, // 0x00000048
//  ko_DimensionPartsParam = 73, // 0x00000049
//  ko_TextStyleParam = 74, // 0x0000004A
//  ko_ConicArcParam = 75, // 0x0000004B
//  ko_PolylineParam = 76, // 0x0000004C
//  ko_LibStyle = 77, // 0x0000004D
//  ko_TechnicalDemandParam = 78, // 0x0000004E
//  ko_SpecRoughParam = 79, // 0x0000004F
//  ko_DimensionOptions = 80, // 0x00000050
//  ko_SpcColumnParam = 81, // 0x00000051
//  ko_LibraryStyleParam = 82, // 0x00000052
//  ko_InertiaParam = 83, // 0x00000053
//  ko_MassInertiaParam = 84, // 0x00000054
//  ko_VariableParam = 85, // 0x00000055
//  ko_SnapOptions = 86, // 0x00000056
//  ko_NurbsParam = 87, // 0x00000057
//  ko_InsertFragmentParam = 88, // 0x00000058
//  ko_ConstraintParam = 89, // 0x00000059
//  ko_CornerParam = 90, // 0x0000005A
//  ko_RectangleParam = 91, // 0x0000005B
//  ko_RegularPolygonParam = 92, // 0x0000005C
//  ko_CentreParam = 93, // 0x0000005D
//  ko_DocAttachSpcParam = 94, // 0x0000005E
//  ko_SpcObjParam = 95, // 0x0000005F
//  ko_RasterParam = 96, // 0x00000060
//  ko_RecordTypeAttrParam = 97, // 0x00000061
//  ko_NumberTypeAttrParam = 98, // 0x00000062
//  ko_SpcStyleColumnParam = 99, // 0x00000063
//  ko_SpcStyleSectionParam = 100, // 0x00000064
//  ko_SpcSubSectionParam = 101, // 0x00000065
//  ko_SpcTuningSectionParam = 102, // 0x00000066
//  ko_SpcTuningStyleParam = 103, // 0x00000067
//  ko_SpcStyleParam = 104, // 0x00000068
//  ko_SpcDescrParam = 105, // 0x00000069
//  ko_QualityItemParam = 106, // 0x0000006A
//  ko_QualityContensParam = 107, // 0x0000006B
//  ko_LtVariant = 108, // 0x0000006C
//  ko_ContourParam = 109, // 0x0000006D
//  ko_DoubleValue = 110, // 0x0000006E
//  ko_Char255 = 111, // 0x0000006F
//  ko_UserParam = 112, // 0x00000070
//  ko_HatchLineParam = 113, // 0x00000071
//  ko_HatchStyleParam = 114, // 0x00000072
//  ko_OrdinatedSourceParam = 115, // 0x00000073
//  ko_OrdinatedDrawingParam = 116, // 0x00000074
//  ko_OrdinatedDimParam = 117, // 0x00000075
//  ko_SheetOptions = 118, // 0x00000076
//  ko_InsertFragmentParamEx = 119, // 0x00000077
//  ko_TreeNodeParam = 120, // 0x00000078
//  ko_ViewColorParam = 121, // 0x00000079
//  ko_AssociationViewParam = 122, // 0x0000007A
//  ko_AxisLineParam = 123, // 0x0000007B
//  ko_TextDocumentParam = 124, // 0x0000007C
//  ko_RemoteElementParam = 125, // 0x0000007D
//  ko_CopyObjectParam = 126, // 0x0000007E
//  ko_OverlapObjectOptions = 127, // 0x0000007F
//  ko_ChangeLeaderParam = 128, // 0x00000080
//  ko_ParametrisationParam = 9000, // 0x00002328
//}
