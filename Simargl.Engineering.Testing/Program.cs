using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;

namespace Simargl.Engineering.Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //HashSet<string> files = [];
                //foreach (var file in new DirectoryInfo("D:\\Nextcloud\\МПТ\\200 КО\\135 ВСМ\\107 Комплект КД с ИУЛ\\101 КД").GetFiles("*", SearchOption.AllDirectories))
                //{
                //    string result = Regex.Replace(file.Name[..^file.Extension.Length], @"\s+", " ");
                //    files.Add(result);
                //}
                //foreach (var file in files)
                //{
                //    Console.WriteLine(file);
                //}

                ////  Создание приложения.
                //Application application = new()
                //{
                //    Visible = true,
                //};

                //Console.WriteLine($"Name = {application.GetName()}");

                ////// Теперь можно работать с документами, если нужно:
                ////dynamic documents = kompas.Documents;

                //// Например, открыть файл
                //string filePath = @"C:\Users\User\Desktop\Чертеж.cdw";
                //dynamic iDocument2D = application.ComObject.Open(filePath, true, false);

                ////  Подключим описание интерфейсов API5
                //Type kompas5Type = Type.GetTypeFromProgID("KOMPAS.Application.5")!;
                //dynamic kompas6_api5_module = Activator.CreateInstance(kompas5Type)!;

                //dynamic iStamp = iDocument2D.GetStamp();

                //iStamp.ksOpenStamp();
                //iStamp.ksColumnNumber(143);


                /*




iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "1"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(153)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "2"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(163)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "3"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(173)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "4"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(183)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "5"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(142)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "6"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(152)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "7"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(162)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "8"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(172)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "9"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(182)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "10"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(141)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "11"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(151)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "12"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(161)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "13"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(171)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "14"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(181)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "15"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(140)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "16"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(150)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "17"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(160)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "18"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(170)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "19"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(180)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "20"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(110)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "21"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(120)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "22"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(130)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "23"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(111)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "24"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(121)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "25"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(131)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "26"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(112)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "27"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(122)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "28"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(132)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "29"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(10)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "30"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(113)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "31"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(123)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "32"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(133)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "33"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(114)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "34"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(124)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "35"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(134)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "36"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(115)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "37"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(125)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "38"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(135)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "39"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(2)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32769
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "40"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 7
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(1)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32770
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "41"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 10
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(3)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32771
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "42"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 7
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(40)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32772
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "43"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(41)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32772
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "44"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(42)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32772
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "45"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(5)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32772
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "46"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(9)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32771
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "47"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 7
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksCloseStamp()

iStamp = iDocument2D.GetStamp()

iStamp.ksOpenStamp()
iStamp.ksColumnNumber(19)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "48"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(200)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "49"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(201)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "50"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(21)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "51"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(22)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "52"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(230)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "53"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(231)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "54"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksCloseStamp()



*/


                /*

# -*- coding: utf-8 -*-
#|Test

import pythoncom
from win32com.client import Dispatch, gencache

import LDefin2D
import MiscellaneousHelpers as MH

#  Подключим константы API Компас
kompas6_constants = gencache.EnsureModule("{75C9F5D0-B5B8-4526-8681-9903C567D2ED}", 0, 1, 0).constants
kompas6_constants_3d = gencache.EnsureModule("{2CAF168C-7961-4B90-9DA2-701419BEEFE3}", 0, 1, 0).constants

#  Подключим описание интерфейсов API5
kompas6_api5_module = gencache.EnsureModule("{0422828C-F174-495E-AC5D-D31014DBBE87}", 0, 1, 0)
kompas_object = kompas6_api5_module.KompasObject(Dispatch("Kompas.Application.5")._oleobj_.QueryInterface(kompas6_api5_module.KompasObject.CLSID, pythoncom.IID_IDispatch))
MH.iKompasObject  = kompas_object

#  Подключим описание интерфейсов API7
kompas_api7_module = gencache.EnsureModule("{69AC2981-37C0-4379-84FD-5DD2F3C0A520}", 0, 1, 0)
application = kompas_api7_module.IApplication(Dispatch("Kompas.Application.7")._oleobj_.QueryInterface(kompas_api7_module.IApplication.CLSID, pythoncom.IID_IDispatch))
MH.iApplication  = application


Documents = application.Documents
#  Получим активный документ
kompas_document = application.ActiveDocument
kompas_document_2d = kompas_api7_module.IKompasDocument2D(kompas_document)
iDocument2D = kompas_object.ActiveDocument2D()

iStamp = iDocument2D.GetStamp()

iStamp.ksOpenStamp()
iStamp.ksColumnNumber(143)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "1"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(153)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "2"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(163)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "3"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(173)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "4"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(183)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "5"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(142)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "6"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(152)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "7"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(162)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "8"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(172)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "9"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(182)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "10"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(141)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "11"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(151)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "12"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(161)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "13"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(171)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "14"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(181)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "15"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(140)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "16"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(150)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "17"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(160)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "18"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(170)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "19"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(180)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "20"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(110)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "21"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(120)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "22"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(130)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "23"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(111)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "24"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(121)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "25"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(131)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "26"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(112)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "27"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(122)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "28"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(132)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "29"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(10)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "30"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(113)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "31"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(123)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "32"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(133)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "33"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(114)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "34"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(124)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "35"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(134)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "36"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(115)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "37"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(125)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "38"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(135)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "39"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(2)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32769
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "40"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 7
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(1)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32770
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "41"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 10
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(3)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32771
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "42"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 7
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(40)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32772
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "43"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(41)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32772
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "44"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(42)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32772
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "45"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(5)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32772
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "46"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(9)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32771
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "47"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 7
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksCloseStamp()

iStamp = iDocument2D.GetStamp()

iStamp.ksOpenStamp()
iStamp.ksColumnNumber(19)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "48"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(200)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "49"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(201)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "50"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(21)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "51"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(22)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "52"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(230)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "53"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksColumnNumber(231)

iTextLineParam = kompas6_api5_module.ksTextLineParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextLineParam))
iTextLineParam.Init()
iTextLineParam.style = 32768
iTextItemArray = kompas_object.GetDynamicArray(LDefin2D.TEXT_ITEM_ARR)
iTextItemParam = kompas6_api5_module.ksTextItemParam(kompas_object.GetParamStruct(kompas6_constants.ko_TextItemParam))
iTextItemParam.Init()
iTextItemParam.iSNumb = 0
iTextItemParam.s = "54"
iTextItemParam.type = 0
iTextItemFont = kompas6_api5_module.ksTextItemFont(iTextItemParam.GetItemFont())
iTextItemFont.Init()
iTextItemFont.bitVector = 4096
iTextItemFont.color = 0
iTextItemFont.fontName = "GOST type A"
iTextItemFont.height = 3.5
iTextItemFont.ksu = 1
iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
iTextLineParam.SetTextItemArr(iTextItemArray)

iStamp.ksTextLine(iTextLineParam)
iStamp.ksCloseStamp()



                */

                Console.WriteLine("КОМПАС запущен и документ открыт.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex);
            }

            //const string progId = "KOMPAS.Application.5";
            //Type? kompasType = Type.GetTypeFromProgID(progId);

            //if (kompasType is null)
            //{
            //    Console.WriteLine("КОМПАС не установлен.");
            //    return;
            //}
            //else
            //{
            //    Console.WriteLine("КОМПАС установлен!!!");
            //}

            //dynamic kompas;
            //try
            //{
            //    // в .NET 9 напрямую GetActiveObject не работает → сразу CreateInstance
            //    kompas = Activator.CreateInstance(kompasType)!;
            //    Console.WriteLine("Создали новый экземпляр КОМПАС.");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Ошибка запуска: {ex.Message}");
            //    return;
            //}

            //kompas.Visible = true;
            //Console.WriteLine("КОМПАС поднят и доступен.");
        }


        //[DllImport("ole32.dll")]
        //private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable pprot);

        //[DllImport("ole32.dll")]
        //private static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);

        //static object? GetActiveObject(string progId)
        //{
        //    GetRunningObjectTable(0, out var rot);
        //    CreateBindCtx(0, out var ctx);

        //    rot.EnumRunning(out var enumMoniker);
        //    enumMoniker.Reset();

        //    IMoniker[] moniker = new IMoniker[1];
        //    while (enumMoniker.Next(1, moniker, IntPtr.Zero) == 0)
        //    {
        //        moniker[0].GetDisplayName(ctx, null, out var name);
        //        if (name.Contains(progId, StringComparison.OrdinalIgnoreCase))
        //        {
        //            rot.GetObject(moniker[0], out var obj);
        //            return obj;
        //        }
        //    }
        //    return null;
        //}
    }
}
