using System.Security;
using System.Text;

namespace RailTest.Frames
{
    partial class Kernel
    {

        [SuppressUnmanagedCodeSecurity]
        unsafe public static string ReadASCIIString(FileReader reader, int length)
        {
            StringBuilder text = new();
            fixed (byte* bytes = reader.ReadBytes(length))
            {
                int index = 0;
                while (index < length && bytes[index] != 0)
                {
                    switch (bytes[index])
                    {
                        case 0x20: text.Append(' '); break;
                        case 0x21: text.Append('!'); break;
                        case 0x22: text.Append('\"'); break;
                        case 0x23: text.Append('#'); break;
                        case 0x24: text.Append('$'); break;
                        case 0x25: text.Append('%'); break;
                        case 0x26: text.Append('&'); break;
                        case 0x27: text.Append('\''); break;
                        case 0x28: text.Append('('); break;
                        case 0x29: text.Append(')'); break;
                        case 0x2A: text.Append('*'); break;
                        case 0x2B: text.Append('+'); break;
                        case 0x2C: text.Append(','); break;
                        case 0x2D: text.Append('-'); break;
                        case 0x2E: text.Append('.'); break;
                        case 0x2F: text.Append('/'); break;

                        case 0x30: text.Append('0'); break;
                        case 0x31: text.Append('1'); break;
                        case 0x32: text.Append('2'); break;
                        case 0x33: text.Append('3'); break;
                        case 0x34: text.Append('4'); break;
                        case 0x35: text.Append('5'); break;
                        case 0x36: text.Append('6'); break;
                        case 0x37: text.Append('7'); break;
                        case 0x38: text.Append('8'); break;
                        case 0x39: text.Append('9'); break;
                        case 0x3A: text.Append(':'); break;
                        case 0x3B: text.Append(';'); break;
                        case 0x3C: text.Append('<'); break;
                        case 0x3D: text.Append('='); break;
                        case 0x3E: text.Append('>'); break;
                        case 0x3F: text.Append('?'); break;

                        case 0x40: text.Append('@'); break;
                        case 0x41: text.Append('A'); break;
                        case 0x42: text.Append('B'); break;
                        case 0x43: text.Append('C'); break;
                        case 0x44: text.Append('D'); break;
                        case 0x45: text.Append('E'); break;
                        case 0x46: text.Append('F'); break;
                        case 0x47: text.Append('G'); break;
                        case 0x48: text.Append('H'); break;
                        case 0x49: text.Append('I'); break;
                        case 0x4A: text.Append('J'); break;
                        case 0x4B: text.Append('K'); break;
                        case 0x4C: text.Append('L'); break;
                        case 0x4D: text.Append('M'); break;
                        case 0x4E: text.Append('N'); break;
                        case 0x4F: text.Append('O'); break;

                        case 0x50: text.Append('P'); break;
                        case 0x51: text.Append('Q'); break;
                        case 0x52: text.Append('R'); break;
                        case 0x53: text.Append('S'); break;
                        case 0x54: text.Append('T'); break;
                        case 0x55: text.Append('U'); break;
                        case 0x56: text.Append('V'); break;
                        case 0x57: text.Append('W'); break;
                        case 0x58: text.Append('X'); break;
                        case 0x59: text.Append('Y'); break;
                        case 0x5A: text.Append('Z'); break;
                        case 0x5B: text.Append('['); break;
                        case 0x5C: text.Append('\\'); break;
                        case 0x5D: text.Append(']'); break;
                        case 0x5E: text.Append('^'); break;
                        case 0x5F: text.Append('_'); break;

                        case 0x60: text.Append('`'); break;
                        case 0x61: text.Append('a'); break;
                        case 0x62: text.Append('b'); break;
                        case 0x63: text.Append('c'); break;
                        case 0x64: text.Append('d'); break;
                        case 0x65: text.Append('e'); break;
                        case 0x66: text.Append('f'); break;
                        case 0x67: text.Append('g'); break;
                        case 0x68: text.Append('h'); break;
                        case 0x69: text.Append('i'); break;
                        case 0x6A: text.Append('j'); break;
                        case 0x6B: text.Append('k'); break;
                        case 0x6C: text.Append('l'); break;
                        case 0x6D: text.Append('m'); break;
                        case 0x6E: text.Append('n'); break;
                        case 0x6F: text.Append('o'); break;

                        case 0x70: text.Append('p'); break;
                        case 0x71: text.Append('q'); break;
                        case 0x72: text.Append('r'); break;
                        case 0x73: text.Append('s'); break;
                        case 0x74: text.Append('t'); break;
                        case 0x75: text.Append('u'); break;
                        case 0x76: text.Append('v'); break;
                        case 0x77: text.Append('w'); break;
                        case 0x78: text.Append('x'); break;
                        case 0x79: text.Append('y'); break;
                        case 0x7A: text.Append('z'); break;
                        case 0x7B: text.Append('{'); break;
                        case 0x7C: text.Append('|'); break;
                        case 0x7D: text.Append('}'); break;
                        case 0x7E: text.Append('~'); break;
                        case 0x7F: text.Append(' '); break;

                        case 0x80: text.Append('Ђ'); break;
                        case 0x81: text.Append('Ѓ'); break;
                        case 0x82: text.Append('‚'); break;
                        case 0x83: text.Append('ѓ'); break;
                        case 0x84: text.Append('„'); break;
                        case 0x85: text.Append('…'); break;
                        case 0x86: text.Append('†'); break;
                        case 0x87: text.Append('‡'); break;
                        case 0x88: text.Append('€'); break;
                        case 0x89: text.Append('‰'); break;
                        case 0x8A: text.Append('Љ'); break;
                        case 0x8B: text.Append('‹'); break;
                        case 0x8C: text.Append('Њ'); break;
                        case 0x8D: text.Append('Ќ'); break;
                        case 0x8E: text.Append('Ћ'); break;
                        case 0x8F: text.Append('Џ'); break;

                        case 0x90: text.Append('ђ'); break;
                        case 0x91: text.Append('‘'); break;
                        case 0x92: text.Append('’'); break;
                        case 0x93: text.Append('“'); break;
                        case 0x94: text.Append('”'); break;
                        case 0x95: text.Append('•'); break;
                        case 0x96: text.Append('–'); break;
                        case 0x97: text.Append('—'); break;
                        case 0x98: text.Append(' '); break;
                        case 0x99: text.Append('™'); break;
                        case 0x9A: text.Append('љ'); break;
                        case 0x9B: text.Append('›'); break;
                        case 0x9C: text.Append('њ'); break;
                        case 0x9D: text.Append('ќ'); break;
                        case 0x9E: text.Append('ћ'); break;
                        case 0x9F: text.Append('џ'); break;

                        case 0xA0: text.Append(' '); break;
                        case 0xA1: text.Append('Ў'); break;
                        case 0xA2: text.Append('ў'); break;
                        case 0xA3: text.Append('Ј'); break;
                        case 0xA4: text.Append('¤'); break;
                        case 0xA5: text.Append('Ґ'); break;
                        case 0xA6: text.Append('¦'); break;
                        case 0xA7: text.Append('§'); break;
                        case 0xA8: text.Append('Ё'); break;
                        case 0xA9: text.Append('©'); break;
                        case 0xAA: text.Append('Є'); break;
                        case 0xAB: text.Append('«'); break;
                        case 0xAC: text.Append('¬'); break;
                        case 0xAD: text.Append(' '); break;
                        case 0xAE: text.Append('®'); break;
                        case 0xAF: text.Append('Ї'); break;

                        case 0xB0: text.Append('°'); break;
                        case 0xB1: text.Append('±'); break;
                        case 0xB2: text.Append('І'); break;
                        case 0xB3: text.Append('і'); break;
                        case 0xB4: text.Append('ґ'); break;
                        case 0xB5: text.Append('µ'); break;
                        case 0xB6: text.Append('¶'); break;
                        case 0xB7: text.Append('·'); break;
                        case 0xB8: text.Append('ё'); break;
                        case 0xB9: text.Append('№'); break;
                        case 0xBA: text.Append('є'); break;
                        case 0xBB: text.Append('»'); break;
                        case 0xBC: text.Append('ј'); break;
                        case 0xBD: text.Append('Ѕ'); break;
                        case 0xBE: text.Append('ѕ'); break;
                        case 0xBF: text.Append('ї'); break;

                        case 0xC0: text.Append('А'); break;
                        case 0xC1: text.Append('Б'); break;
                        case 0xC2: text.Append('В'); break;
                        case 0xC3: text.Append('Г'); break;
                        case 0xC4: text.Append('Д'); break;
                        case 0xC5: text.Append('Е'); break;
                        case 0xC6: text.Append('Ж'); break;
                        case 0xC7: text.Append('З'); break;
                        case 0xC8: text.Append('И'); break;
                        case 0xC9: text.Append('Й'); break;
                        case 0xCA: text.Append('К'); break;
                        case 0xCB: text.Append('Л'); break;
                        case 0xCC: text.Append('М'); break;
                        case 0xCD: text.Append('Н'); break;
                        case 0xCE: text.Append('О'); break;
                        case 0xCF: text.Append('П'); break;

                        case 0xD0: text.Append('Р'); break;
                        case 0xD1: text.Append('С'); break;
                        case 0xD2: text.Append('Т'); break;
                        case 0xD3: text.Append('У'); break;
                        case 0xD4: text.Append('Ф'); break;
                        case 0xD5: text.Append('Х'); break;
                        case 0xD6: text.Append('Ц'); break;
                        case 0xD7: text.Append('Ч'); break;
                        case 0xD8: text.Append('Ш'); break;
                        case 0xD9: text.Append('Щ'); break;
                        case 0xDA: text.Append('Ъ'); break;
                        case 0xDB: text.Append('Ы'); break;
                        case 0xDC: text.Append('Ь'); break;
                        case 0xDD: text.Append('Э'); break;
                        case 0xDE: text.Append('Ю'); break;
                        case 0xDF: text.Append('Я'); break;

                        case 0xE0: text.Append('а'); break;
                        case 0xE1: text.Append('б'); break;
                        case 0xE2: text.Append('в'); break;
                        case 0xE3: text.Append('г'); break;
                        case 0xE4: text.Append('д'); break;
                        case 0xE5: text.Append('е'); break;
                        case 0xE6: text.Append('ж'); break;
                        case 0xE7: text.Append('з'); break;
                        case 0xE8: text.Append('и'); break;
                        case 0xE9: text.Append('й'); break;
                        case 0xEA: text.Append('к'); break;
                        case 0xEB: text.Append('л'); break;
                        case 0xEC: text.Append('м'); break;
                        case 0xED: text.Append('н'); break;
                        case 0xEE: text.Append('о'); break;
                        case 0xEF: text.Append('п'); break;

                        case 0xF0: text.Append('р'); break;
                        case 0xF1: text.Append('с'); break;
                        case 0xF2: text.Append('т'); break;
                        case 0xF3: text.Append('у'); break;
                        case 0xF4: text.Append('ф'); break;
                        case 0xF5: text.Append('х'); break;
                        case 0xF6: text.Append('ц'); break;
                        case 0xF7: text.Append('ч'); break;
                        case 0xF8: text.Append('ш'); break;
                        case 0xF9: text.Append('щ'); break;
                        case 0xFA: text.Append('ъ'); break;
                        case 0xFB: text.Append('ы'); break;
                        case 0xFC: text.Append('ь'); break;
                        case 0xFD: text.Append('э'); break;
                        case 0xFE: text.Append('ю'); break;
                        case 0xFF: text.Append('я'); break;

                        default: text.Append(' '); break;
                    }
                    ++index;
                }
            }
            return text.ToString();
        }

        
        [SuppressUnmanagedCodeSecurity]
        unsafe public static void WriteASCIIString(FileWriter writer, string value, int length, bool isLastNull)
        {
            byte[] bytes = new byte[length];
            fixed (byte* pBytes = bytes)
            {
                if (value != null)
                {
                    char[] chars = value.ToCharArray();
                    int copyLength = chars.Length;
                    if (copyLength > 0)
                    {
                        if (copyLength > length)
                        {
                            copyLength = length;
                        }
                        fixed (char* pChars = chars)
                        {
                            for (int i = 0; i != copyLength; ++i)
                            {
                                pBytes[i] = pChars[i] switch
                                {
                                    ' ' => 0x20,
                                    '!' => 0x21,
                                    '\"' => 0x22,
                                    '#' => 0x23,
                                    '$' => 0x24,
                                    '%' => 0x25,
                                    '&' => 0x26,
                                    '\'' => 0x27,
                                    '(' => 0x28,
                                    ')' => 0x29,
                                    '*' => 0x2A,
                                    '+' => 0x2B,
                                    ',' => 0x2C,
                                    '-' => 0x2D,
                                    '.' => 0x2E,
                                    '/' => 0x2F,
                                    '0' => 0x30,
                                    '1' => 0x31,
                                    '2' => 0x32,
                                    '3' => 0x33,
                                    '4' => 0x34,
                                    '5' => 0x35,
                                    '6' => 0x36,
                                    '7' => 0x37,
                                    '8' => 0x38,
                                    '9' => 0x39,
                                    ':' => 0x3A,
                                    ';' => 0x3B,
                                    '<' => 0x3C,
                                    '=' => 0x3D,
                                    '>' => 0x3E,
                                    '?' => 0x3F,
                                    '@' => 0x40,
                                    'A' => 0x41,
                                    'B' => 0x42,
                                    'C' => 0x43,
                                    'D' => 0x44,
                                    'E' => 0x45,
                                    'F' => 0x46,
                                    'G' => 0x47,
                                    'H' => 0x48,
                                    'I' => 0x49,
                                    'J' => 0x4A,
                                    'K' => 0x4B,
                                    'L' => 0x4C,
                                    'M' => 0x4D,
                                    'N' => 0x4E,
                                    'O' => 0x4F,
                                    'P' => 0x50,
                                    'Q' => 0x51,
                                    'R' => 0x52,
                                    'S' => 0x53,
                                    'T' => 0x54,
                                    'U' => 0x55,
                                    'V' => 0x56,
                                    'W' => 0x57,
                                    'X' => 0x58,
                                    'Y' => 0x59,
                                    'Z' => 0x5A,
                                    '[' => 0x5B,
                                    '\\' => 0x5C,
                                    ']' => 0x5D,
                                    '^' => 0x5E,
                                    '_' => 0x5F,
                                    '`' => 0x60,
                                    'a' => 0x61,
                                    'b' => 0x62,
                                    'c' => 0x63,
                                    'd' => 0x64,
                                    'e' => 0x65,
                                    'f' => 0x66,
                                    'g' => 0x67,
                                    'h' => 0x68,
                                    'i' => 0x69,
                                    'j' => 0x6A,
                                    'k' => 0x6B,
                                    'l' => 0x6C,
                                    'm' => 0x6D,
                                    'n' => 0x6E,
                                    'o' => 0x6F,
                                    'p' => 0x70,
                                    'q' => 0x71,
                                    'r' => 0x72,
                                    's' => 0x73,
                                    't' => 0x74,
                                    'u' => 0x75,
                                    'v' => 0x76,
                                    'w' => 0x77,
                                    'x' => 0x78,
                                    'y' => 0x79,
                                    'z' => 0x7A,
                                    '{' => 0x7B,
                                    '|' => 0x7C,
                                    '}' => 0x7D,
                                    '~' => 0x7E,
                                    'Ђ' => 0x80,
                                    'Ѓ' => 0x81,
                                    '‚' => 0x82,
                                    'ѓ' => 0x83,
                                    '„' => 0x84,
                                    '…' => 0x85,
                                    '†' => 0x86,
                                    '‡' => 0x87,
                                    '€' => 0x88,
                                    '‰' => 0x89,
                                    'Љ' => 0x8A,
                                    '‹' => 0x8B,
                                    'Њ' => 0x8C,
                                    'Ќ' => 0x8D,
                                    'Ћ' => 0x8E,
                                    'Џ' => 0x8F,
                                    'ђ' => 0x90,
                                    '‘' => 0x91,
                                    '’' => 0x92,
                                    '“' => 0x93,
                                    '”' => 0x94,
                                    '•' => 0x95,
                                    '–' => 0x96,
                                    '—' => 0x97,
                                    '™' => 0x99,
                                    'љ' => 0x9A,
                                    '›' => 0x9B,
                                    'њ' => 0x9C,
                                    'ќ' => 0x9D,
                                    'ћ' => 0x9E,
                                    'џ' => 0x9F,
                                    'Ў' => 0xA1,
                                    'ў' => 0xA2,
                                    'Ј' => 0xA3,
                                    '¤' => 0xA4,
                                    'Ґ' => 0xA5,
                                    '¦' => 0xA6,
                                    '§' => 0xA7,
                                    'Ё' => 0xA8,
                                    '©' => 0xA9,
                                    'Є' => 0xAA,
                                    '«' => 0xAB,
                                    '¬' => 0xAC,
                                    '®' => 0xAE,
                                    'Ї' => 0xAF,
                                    '°' => 0xB0,
                                    '±' => 0xB1,
                                    'І' => 0xB2,
                                    'і' => 0xB3,
                                    'ґ' => 0xB4,
                                    'µ' => 0xB5,
                                    '¶' => 0xB6,
                                    '·' => 0xB7,
                                    'ё' => 0xB8,
                                    '№' => 0xB9,
                                    'є' => 0xBA,
                                    '»' => 0xBB,
                                    'ј' => 0xBC,
                                    'Ѕ' => 0xBD,
                                    'ѕ' => 0xBE,
                                    'ї' => 0xBF,
                                    'А' => 0xC0,
                                    'Б' => 0xC1,
                                    'В' => 0xC2,
                                    'Г' => 0xC3,
                                    'Д' => 0xC4,
                                    'Е' => 0xC5,
                                    'Ж' => 0xC6,
                                    'З' => 0xC7,
                                    'И' => 0xC8,
                                    'Й' => 0xC9,
                                    'К' => 0xCA,
                                    'Л' => 0xCB,
                                    'М' => 0xCC,
                                    'Н' => 0xCD,
                                    'О' => 0xCE,
                                    'П' => 0xCF,
                                    'Р' => 0xD0,
                                    'С' => 0xD1,
                                    'Т' => 0xD2,
                                    'У' => 0xD3,
                                    'Ф' => 0xD4,
                                    'Х' => 0xD5,
                                    'Ц' => 0xD6,
                                    'Ч' => 0xD7,
                                    'Ш' => 0xD8,
                                    'Щ' => 0xD9,
                                    'Ъ' => 0xDA,
                                    'Ы' => 0xDB,
                                    'Ь' => 0xDC,
                                    'Э' => 0xDD,
                                    'Ю' => 0xDE,
                                    'Я' => 0xDF,
                                    'а' => 0xE0,
                                    'б' => 0xE1,
                                    'в' => 0xE2,
                                    'г' => 0xE3,
                                    'д' => 0xE4,
                                    'е' => 0xE5,
                                    'ж' => 0xE6,
                                    'з' => 0xE7,
                                    'и' => 0xE8,
                                    'й' => 0xE9,
                                    'к' => 0xEA,
                                    'л' => 0xEB,
                                    'м' => 0xEC,
                                    'н' => 0xED,
                                    'о' => 0xEE,
                                    'п' => 0xEF,
                                    'р' => 0xF0,
                                    'с' => 0xF1,
                                    'т' => 0xF2,
                                    'у' => 0xF3,
                                    'ф' => 0xF4,
                                    'х' => 0xF5,
                                    'ц' => 0xF6,
                                    'ч' => 0xF7,
                                    'ш' => 0xF8,
                                    'щ' => 0xF9,
                                    'ъ' => 0xFA,
                                    'ы' => 0xFB,
                                    'ь' => 0xFC,
                                    'э' => 0xFD,
                                    'ю' => 0xFE,
                                    'я' => 0xFF,
                                    _ => 0x20,
                                };
                            }
                        }
                        if (copyLength < length)
                        {
                            for (int i = copyLength; i != length; ++i)
                            {
                                pBytes[i] = 0;
                            }
                        }
                    }
                }
                if (isLastNull)
                {
                    pBytes[length - 1] = 0;
                }
            }
            writer.Write(bytes, 0, length);
        }
    }
}