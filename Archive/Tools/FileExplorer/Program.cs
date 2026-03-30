using Simargl.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

const string source = "C:\\Users\\root\\Desktop\\4440.02.12.000.2.SP";
const string target = "C:\\Users\\root\\Desktop\\Data.csv";

CyrillicEncoding encoding = new();
StringBuilder output = new();

output.AppendLine("#;Byte;Char;Text");

//  Загрузка данных.
byte[] data = File.ReadAllBytes(source);

for (int i = 0; i < data.Length; i++)
{
    output.Append(i);
    output.Append(';');
    output.Append(data[i]);
    output.Append(';');
    output.Append(getChar(i));
    output.Append(';');
    output.Append(getText(i));


    output.AppendLine();
}




//  Сохранение данных.
File.WriteAllText(target, output.ToString(), encoding);

//foreach (var item in map)
//{
//    Console.WriteLine($"0x{item.Key:X2} -> {item.Value}");
//}

string getText(int index)
{
    string text = string.Empty;
    for (int i = index; i < data.Length; i++)
    {
        if (data[i] == 0)
        {
            break;
        }
        text += getChar(i);
    }
    return text;
}

string getChar(int index)
{
    return data[index] switch
    {
        0x00 => "[NUL]",
        0x01 => "[SOH]",
        0x02 => "[STX]",
        0x03 => "[ETX]",
        0x04 => "[EOT]",
        0x05 => "[ENQ]",
        0x06 => "[ACK]",
        0x07 => "[BEL]",
        0x08 => "[BS]",
        0x09 => "[HT]",
        0x0a => "[LF]",
        0x0b => "[VT]",
        0x0c => "[FF]",
        0x0d => "[CR]",
        0x0e => "[SO]",
        0x0f => "[SI]",
        0x10 => "[DLE]",
        0x11 => "[DC1]",
        0x12 => "[DC2]",
        0x13 => "[DC3]",
        0x14 => "[DC4]",
        0x15 => "[NAK]",
        0x16 => "[SYN]",
        0x17 => "[ETB]",
        0x18 => "[CAN]",
        0x19 => "[EM]",
        0x1a => "[SUB]",
        0x1b => "[ESC]",
        0x1c => "[FS]",
        0x1d => "[GS]",
        0x1e => "[RS]",
        0x1f => "[US]",
        0x20 => "[SPACE]",
        0x7f => "[DEL]",
        0x3b => "[SCOL]",
        _ => CyrillicEncoding.GetChar(data[index]).ToString(),
    };
}

//static string getChar(Encoding encoding, byte[] data, int index, int maxSize)
//{
//    for (int size = 1; size <= maxSize; size++)
//    {
//        try
//        {
//            return new string(encoding.GetChars(data, index, size), 0, 1);
//        } catch { }
//    }
//    return "none";
//}
