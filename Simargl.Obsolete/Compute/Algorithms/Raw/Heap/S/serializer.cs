#pragma warning disable CS3001
#pragma warning disable CS3002
#pragma warning disable CS8625
#pragma warning disable CS8618
#pragma warning disable CS8600
#pragma warning disable CS1591

#if ALGLIB_USE_SIMD
#define _ALGLIB_ALREADY_DEFINED_SIMD_ALIASES
using Sse2 = System.Runtime.Intrinsics.X86.Sse2;
using Avx2 = System.Runtime.Intrinsics.X86.Avx2;
using Fma  = System.Runtime.Intrinsics.X86.Fma;
using Intrinsics = System.Runtime.Intrinsics;
#endif

#if ALGLIB_USE_SIMD && !_ALGLIB_ALREADY_DEFINED_SIMD_ALIASES
#define _ALGLIB_ALREADY_DEFINED_SIMD_ALIASES
using Sse2 = System.Runtime.Intrinsics.X86.Sse2;
using Avx2 = System.Runtime.Intrinsics.X86.Avx2;
using Fma  = System.Runtime.Intrinsics.X86.Fma;
using Intrinsics = System.Runtime.Intrinsics;
#endif

namespace Simargl.Algorithms.Raw;

/********************************************************************
serializer object (should not be used directly)
********************************************************************/
public class serializer
{
    enum SMODE { DEFAULT, ALLOC, TO_STRING, FROM_STRING, TO_STREAM, FROM_STREAM };
    private const int SER_ENTRIES_PER_ROW = 5;
    private const int SER_ENTRY_LENGTH = 11;

    private SMODE mode;
    private int entries_needed;
    private int entries_saved;
    private int bytes_asked;
    private int bytes_written;
    private int bytes_read;
    private char[] out_str;
    private char[] in_str;
    private System.IO.Stream io_stream;

    // local temporaries
    private char[] entry_buf_char;
    private byte[] entry_buf_byte;

    public serializer()
    {
        mode = SMODE.DEFAULT;
        entries_needed = 0;
        bytes_asked = 0;
        entry_buf_byte = new byte[SER_ENTRY_LENGTH + 2];
        entry_buf_char = new char[SER_ENTRY_LENGTH + 2];
    }

    public void clear_buffers()
    {
        out_str = null;
        in_str = null;
        io_stream = null;
    }

    public void alloc_start()
    {
        entries_needed = 0;
        bytes_asked = 0;
        mode = SMODE.ALLOC;
    }

    public void alloc_entry()
    {
        if (mode != SMODE.ALLOC)
            throw new alglibexception("ALGLIB: internal error during (un)serialization");
        entries_needed++;
    }

    public void alloc_byte_array(byte[] a)
    {
        if (mode != SMODE.ALLOC)
            throw new alglibexception("ALGLIB: internal error during (un)serialization");
        int n = ap.len(a);
        n = n / 8 + (n % 8 > 0 ? 1 : 0);
        entries_needed += 1 + n;
    }

    private int get_alloc_size()
    {
        int rows, lastrowsize, result;

        // check and change mode
        if (mode != SMODE.ALLOC)
            throw new alglibexception("ALGLIB: internal error during (un)serialization");

        // if no entries needes (degenerate case)
        if (entries_needed == 0)
        {
            bytes_asked = 4; /* a pair of chars for \r\n, one for space, one for dot */
            return bytes_asked;
        }

        // non-degenerate case
        rows = entries_needed / SER_ENTRIES_PER_ROW;
        lastrowsize = SER_ENTRIES_PER_ROW;
        if (entries_needed % SER_ENTRIES_PER_ROW != 0)
        {
            lastrowsize = entries_needed % SER_ENTRIES_PER_ROW;
            rows++;
        }

        // calculate result size
        result = ((rows - 1) * SER_ENTRIES_PER_ROW + lastrowsize) * SER_ENTRY_LENGTH;  /* data size */
        result += (rows - 1) * (SER_ENTRIES_PER_ROW - 1) + (lastrowsize - 1);            /* space symbols */
        result += rows * 2;                                                       /* newline symbols */
        result += 1;                                                            /* trailing dot */
        bytes_asked = result;
        return result;
    }

    public void sstart_str()
    {
        int allocsize = get_alloc_size();

        // clear input/output buffers which may hold pointers to unneeded memory
        // NOTE: it also helps us to avoid errors when data are written to incorrect location
        clear_buffers();

        // check and change mode
        if (mode != SMODE.ALLOC)
            throw new alglibexception("ALGLIB: internal error during (un)serialization");
        mode = SMODE.TO_STRING;

        // other preparations
        out_str = new char[allocsize];
        entries_saved = 0;
        bytes_written = 0;
    }

    public void sstart_stream(System.IO.Stream o_stream)
    {
        // clear input/output buffers which may hold pointers to unneeded memory
        // NOTE: it also helps us to avoid errors when data are written to incorrect location
        clear_buffers();

        // check and change mode
        if (mode != SMODE.ALLOC)
            throw new alglibexception("ALGLIB: internal error during (un)serialization");
        mode = SMODE.TO_STREAM;
        io_stream = o_stream;
    }

    public void ustart_str(string s)
    {
        // clear input/output buffers which may hold pointers to unneeded memory
        // NOTE: it also helps us to avoid errors when data are written to incorrect location
        clear_buffers();

        // check and change mode
        if (mode != SMODE.DEFAULT)
            throw new alglibexception("ALGLIB: internal error during (un)serialization");
        mode = SMODE.FROM_STRING;

        in_str = s.ToCharArray();
        bytes_read = 0;
    }

    public void ustart_stream(System.IO.Stream i_stream)
    {
        // clear input/output buffers which may hold pointers to unneeded memory
        // NOTE: it also helps us to avoid errors when data are written to incorrect location
        clear_buffers();

        // check and change mode
        if (mode != SMODE.DEFAULT)
            throw new alglibexception("ALGLIB: internal error during (un)serialization");
        mode = SMODE.FROM_STREAM;
        io_stream = i_stream;
    }

    private void serialize_value(bool v0, int v1, double v2, ulong v3, int val_idx)
    {
        // prepare serialization
        char[] arr_out = null;
        int cnt_out = 0;
        if (mode == SMODE.TO_STRING)
        {
            arr_out = out_str;
            cnt_out = bytes_written;
        }
        else if (mode == SMODE.TO_STREAM)
        {
            arr_out = entry_buf_char;
            cnt_out = 0;
        }
        else
            throw new alglibexception("ALGLIB: internal error during serialization");

        // serialize
        if (val_idx == 0)
            bool2str(v0, arr_out, ref cnt_out);
        else if (val_idx == 1)
            int2str(v1, arr_out, ref cnt_out);
        else if (val_idx == 2)
            double2str(v2, arr_out, ref cnt_out);
        else if (val_idx == 3)
            ulong2str(v3, arr_out, ref cnt_out);
        else
            throw new alglibexception("ALGLIB: internal error during serialization");
        entries_saved++;
        if (entries_saved % SER_ENTRIES_PER_ROW != 0)
        {
            arr_out[cnt_out] = ' ';
            cnt_out++;
        }
        else
        {
            arr_out[cnt_out + 0] = '\r';
            arr_out[cnt_out + 1] = '\n';
            cnt_out += 2;
        }

        // post-process
        if (mode == SMODE.TO_STRING)
        {
            bytes_written = cnt_out;
            return;
        }
        else if (mode == SMODE.TO_STREAM)
        {
            for (int k = 0; k < cnt_out; k++)
                entry_buf_byte[k] = (byte)entry_buf_char[k];
            io_stream.Write(entry_buf_byte, 0, cnt_out);
            return;
        }
        else
            throw new alglibexception("ALGLIB: internal error during serialization");
    }

    private void unstream_entry_char()
    {
        if (mode != SMODE.FROM_STREAM)
            throw new alglibexception("ALGLIB: internal error during unserialization");
        int c;
        for (; ; )
        {
            c = io_stream.ReadByte();
            if (c < 0)
                throw new alglibexception("ALGLIB: internal error during unserialization");
            if (c != ' ' && c != '\t' && c != '\n' && c != '\r')
                break;
        }
        entry_buf_char[0] = (char)c;
        for (int k = 1; k < SER_ENTRY_LENGTH; k++)
        {
            c = io_stream.ReadByte();
            entry_buf_char[k] = (char)c;
            if (c < 0 || c == ' ' || c == '\t' || c == '\n' || c == '\r')
                throw new alglibexception("ALGLIB: internal error during unserialization");
        }
        entry_buf_char[SER_ENTRY_LENGTH] = (char)0;
    }

    public void serialize_bool(bool v)
    {
        serialize_value(v, 0, 0, 0, 0);
    }

    public void serialize_int(int v)
    {
        serialize_value(false, v, 0, 0, 1);
    }

    public void serialize_double(double v)
    {
        serialize_value(false, 0, v, 0, 2);
    }

    public void serialize_ulong(ulong v)
    {
        serialize_value(false, 0, 0, v, 3);
    }

    public void serialize_byte_array(byte[] v)
    {
        int chunk_size = 8;

        // save array length
        int n = ap.len(v);
        serialize_int(n);

        // determine entries count
        int entries_count = n / chunk_size + (n % chunk_size > 0 ? 1 : 0);
        for (int eidx = 0; eidx < entries_count; eidx++)
        {
            int elen = n - eidx * chunk_size;
            elen = elen > chunk_size ? chunk_size : elen;
            ulong tmp = 0x0;
            for (int i = 0; i < elen; i++)
                tmp = tmp | (((ulong)v[eidx * chunk_size + i]) << (8 * i));
            serialize_ulong(tmp);
        }
    }

    public bool unserialize_bool()
    {
        if (mode == SMODE.FROM_STRING)
            return str2bool(in_str, ref bytes_read);
        if (mode == SMODE.FROM_STREAM)
        {
            unstream_entry_char();
            int dummy = 0;
            return str2bool(entry_buf_char, ref dummy);
        }
        throw new alglibexception("ALGLIB: internal error during (un)serialization");
    }

    public int unserialize_int()
    {
        if (mode == SMODE.FROM_STRING)
            return str2int(in_str, ref bytes_read);
        if (mode == SMODE.FROM_STREAM)
        {
            unstream_entry_char();
            int dummy = 0;
            return str2int(entry_buf_char, ref dummy);
        }
        throw new alglibexception("ALGLIB: internal error during (un)serialization");
    }

    public double unserialize_double()
    {
        if (mode == SMODE.FROM_STRING)
            return str2double(in_str, ref bytes_read);
        if (mode == SMODE.FROM_STREAM)
        {
            unstream_entry_char();
            int dummy = 0;
            return str2double(entry_buf_char, ref dummy);
        }
        throw new alglibexception("ALGLIB: internal error during (un)serialization");
    }

    public ulong unserialize_ulong()
    {
        if (mode == SMODE.FROM_STRING)
            return str2ulong(in_str, ref bytes_read);
        if (mode == SMODE.FROM_STREAM)
        {
            unstream_entry_char();
            int dummy = 0;
            return str2ulong(entry_buf_char, ref dummy);
        }
        throw new alglibexception("ALGLIB: internal error during (un)serialization");
    }

    public byte[] unserialize_byte_array()
    {
        int chunk_size = 8;

        // read array length, allocate output
        int n = unserialize_int();
        byte[] result = new byte[n];

        // determine entries count
        int entries_count = n / chunk_size + (n % chunk_size > 0 ? 1 : 0);
        for (int eidx = 0; eidx < entries_count; eidx++)
        {
            int elen = n - eidx * chunk_size;
            elen = elen > chunk_size ? chunk_size : elen;
            ulong tmp = unserialize_ulong();
            for (int i = 0; i < elen; i++)
                result[eidx * chunk_size + i] = unchecked((byte)(tmp >> (8 * i)));
        }

        // done
        return result;
    }

    public void stop()
    {
        if (mode == SMODE.TO_STRING)
        {
            out_str[bytes_written] = '.';
            bytes_written++;
            return;
        }
        if (mode == SMODE.FROM_STRING)
        {
            //
            // because input string may be from pre-3.11 serializer,
            // which does not include trailing dot, we do not test
            // string for presence of "." symbol. Anyway, because string
            // is not stream, we do not have to read ALL trailing symbols.
            //
            return;
        }
        if (mode == SMODE.TO_STREAM)
        {
            io_stream.WriteByte((byte)'.');
            return;
        }
        if (mode == SMODE.FROM_STREAM)
        {
            for (; ; )
            {
                int c = io_stream.ReadByte();
                if (c == ' ' || c == '\t' || c == '\n' || c == '\r')
                    continue;
                if (c == '.')
                    break;
                throw new alglibexception("ALGLIB: internal error during unserialization");
            }
            return;
        }
        throw new alglibexception("ALGLIB: internal error during unserialization");
    }

    public string get_string()
    {
        if (mode != SMODE.TO_STRING)
            throw new alglibexception("ALGLIB: internal error during (un)serialization");
        return new string(out_str, 0, bytes_written);
    }


    /************************************************************************
    This function converts six-bit value (from 0 to 63)  to  character  (only
    digits, lowercase and uppercase letters, minus and underscore are used).

    If v is negative or greater than 63, this function returns '?'.
    ************************************************************************/
    private static char[] _sixbits2char_tbl = new char[64]{
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'A', 'B', 'C', 'D', 'E', 'F',
            'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
            'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
            'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l',
            'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z', '-', '_' };
    private static char sixbits2char(int v)
    {
        if (v < 0 || v > 63)
            return '?';
        return _sixbits2char_tbl[v];
    }

    /************************************************************************
    This function converts character to six-bit value (from 0 to 63).

    This function is inverse of ae_sixbits2char()
    If c is not correct character, this function returns -1.
    ************************************************************************/
    private static int[] _char2sixbits_tbl = new int[128] {
        -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, 62, -1, -1,
         0,  1,  2,  3,  4,  5,  6,  7,
         8,  9, -1, -1, -1, -1, -1, -1,
        -1, 10, 11, 12, 13, 14, 15, 16,
        17, 18, 19, 20, 21, 22, 23, 24,
        25, 26, 27, 28, 29, 30, 31, 32,
        33, 34, 35, -1, -1, -1, -1, 63,
        -1, 36, 37, 38, 39, 40, 41, 42,
        43, 44, 45, 46, 47, 48, 49, 50,
        51, 52, 53, 54, 55, 56, 57, 58,
        59, 60, 61, -1, -1, -1, -1, -1 };
    private static int char2sixbits(char c)
    {
        return (c >= 0 && c < 127) ? _char2sixbits_tbl[c] : -1;
    }

    /************************************************************************
    This function converts three bytes (24 bits) to four six-bit values 
    (24 bits again).

    src         array
    src_offs    offset of three-bytes chunk
    dst         array for ints
    dst_offs    offset of four-ints chunk
    ************************************************************************/
    private static void threebytes2foursixbits(byte[] src, int src_offs, int[] dst, int dst_offs)
    {
        dst[dst_offs + 0] = src[src_offs + 0] & 0x3F;
        dst[dst_offs + 1] = (src[src_offs + 0] >> 6) | ((src[src_offs + 1] & 0x0F) << 2);
        dst[dst_offs + 2] = (src[src_offs + 1] >> 4) | ((src[src_offs + 2] & 0x03) << 4);
        dst[dst_offs + 3] = src[src_offs + 2] >> 2;
    }

    /************************************************************************
    This function converts four six-bit values (24 bits) to three bytes
    (24 bits again).

    src         pointer to four ints
    src_offs    offset of the chunk
    dst         pointer to three bytes
    dst_offs    offset of the chunk
    ************************************************************************/
    private static void foursixbits2threebytes(int[] src, int src_offs, byte[] dst, int dst_offs)
    {
        dst[dst_offs + 0] = (byte)(src[src_offs + 0] | ((src[src_offs + 1] & 0x03) << 6));
        dst[dst_offs + 1] = (byte)((src[src_offs + 1] >> 2) | ((src[src_offs + 2] & 0x0F) << 4));
        dst[dst_offs + 2] = (byte)((src[src_offs + 2] >> 4) | (src[src_offs + 3] << 2));
    }

    /************************************************************************
    This function serializes boolean value into buffer

    v           boolean value to be serialized
    buf         buffer, at least 11 characters wide
    offs        offset in the buffer
    
    after return from this function, offs points to the char's past the value
    being read.
    ************************************************************************/
    private static void bool2str(bool v, char[] buf, ref int offs)
    {
        char c = v ? '1' : '0';
        int i;
        for (i = 0; i < SER_ENTRY_LENGTH; i++)
            buf[offs + i] = c;
        offs += SER_ENTRY_LENGTH;
    }

    /************************************************************************
    This function unserializes boolean value from buffer

    buf         buffer which contains value; leading spaces/tabs/newlines are 
                ignored, traling spaces/tabs/newlines are treated as  end  of
                the boolean value.
    offs        offset in the buffer
    
    after return from this function, offs points to the char's past the value
    being read.

    This function raises an error in case unexpected symbol is found
    ************************************************************************/
    private static bool str2bool(char[] buf, ref int offs)
    {
        bool was0, was1;
        string emsg = "ALGLIB: unable to read boolean value from stream";

        was0 = false;
        was1 = false;
        while (buf[offs] == ' ' || buf[offs] == '\t' || buf[offs] == '\n' || buf[offs] == '\r')
            offs++;
        while (buf[offs] != ' ' && buf[offs] != '\t' && buf[offs] != '\n' && buf[offs] != '\r' && buf[offs] != 0)
        {
            if (buf[offs] == '0')
            {
                was0 = true;
                offs++;
                continue;
            }
            if (buf[offs] == '1')
            {
                was1 = true;
                offs++;
                continue;
            }
            throw new alglibexception(emsg);
        }
        if ((!was0) && (!was1))
            throw new alglibexception(emsg);
        if (was0 && was1)
            throw new alglibexception(emsg);
        return was1 ? true : false;
    }

    /************************************************************************
    This function serializes integer value into buffer

    v           integer value to be serialized
    buf         buffer, at least 11 characters wide 
    offs        offset in the buffer
    
    after return from this function, offs points to the char's past the value
    being read.

    This function raises an error in case unexpected symbol is found
    ************************************************************************/
    private static void int2str(int v, char[] buf, ref int offs)
    {
        int i;
        byte[] _bytes = System.BitConverter.GetBytes((int)v);
        byte[] bytes = new byte[9];
        int[] sixbits = new int[12];
        byte c;

        //
        // copy v to array of bytes, sign extending it and 
        // converting to little endian order. Additionally, 
        // we set 9th byte to zero in order to simplify 
        // conversion to six-bit representation
        //
        if (!System.BitConverter.IsLittleEndian)
            System.Array.Reverse(_bytes);
        c = v < 0 ? (byte)0xFF : (byte)0x00;
        for (i = 0; i < sizeof(int); i++)
            bytes[i] = _bytes[i];
        for (i = sizeof(int); i < 8; i++)
            bytes[i] = c;
        bytes[8] = 0;

        //
        // convert to six-bit representation, output
        //
        // NOTE: last 12th element of sixbits is always zero, we do not output it
        //
        threebytes2foursixbits(bytes, 0, sixbits, 0);
        threebytes2foursixbits(bytes, 3, sixbits, 4);
        threebytes2foursixbits(bytes, 6, sixbits, 8);
        for (i = 0; i < SER_ENTRY_LENGTH; i++)
            buf[offs + i] = sixbits2char(sixbits[i]);
        offs += SER_ENTRY_LENGTH;
    }

    /************************************************************************
    This function unserializes integer value from string

    buf         buffer which contains value; leading spaces/tabs/newlines are 
                ignored, traling spaces/tabs/newlines are treated as  end  of
                the integer value.
    offs        offset in the buffer
    
    after return from this function, offs points to the char's past the value
    being read.

    This function raises an error in case unexpected symbol is found
    ************************************************************************/
    private static int str2int(char[] buf, ref int offs)
    {
        string emsg = "ALGLIB: unable to read integer value from stream";
        string emsg3264 = "ALGLIB: unable to read integer value from stream (value does not fit into 32 bits)";
        int[] sixbits = new int[12];
        byte[] bytes = new byte[9];
        byte[] _bytes = new byte[sizeof(int)];
        int sixbitsread, i;
        byte c;

        // 
        // 1. skip leading spaces
        // 2. read and decode six-bit digits
        // 3. set trailing digits to zeros
        // 4. convert to little endian 64-bit integer representation
        // 5. check that we fit into int
        // 6. convert to big endian representation, if needed
        //
        sixbitsread = 0;
        while (buf[offs] == ' ' || buf[offs] == '\t' || buf[offs] == '\n' || buf[offs] == '\r')
            offs++;
        while (buf[offs] != ' ' && buf[offs] != '\t' && buf[offs] != '\n' && buf[offs] != '\r' && buf[offs] != 0)
        {
            int d;
            d = char2sixbits(buf[offs]);
            if (d < 0 || sixbitsread >= SER_ENTRY_LENGTH)
                throw new alglibexception(emsg);
            sixbits[sixbitsread] = d;
            sixbitsread++;
            offs++;
        }
        if (sixbitsread == 0)
            throw new alglibexception(emsg);
        for (i = sixbitsread; i < 12; i++)
            sixbits[i] = 0;
        foursixbits2threebytes(sixbits, 0, bytes, 0);
        foursixbits2threebytes(sixbits, 4, bytes, 3);
        foursixbits2threebytes(sixbits, 8, bytes, 6);
        c = (bytes[sizeof(int) - 1] & 0x80) != 0 ? (byte)0xFF : (byte)0x00;
        for (i = sizeof(int); i < 8; i++)
            if (bytes[i] != c)
                throw new alglibexception(emsg3264);
        for (i = 0; i < sizeof(int); i++)
            _bytes[i] = bytes[i];
        if (!System.BitConverter.IsLittleEndian)
            System.Array.Reverse(_bytes);
        return System.BitConverter.ToInt32(_bytes, 0);
    }


    /************************************************************************
    This function serializes double value into buffer

    v           double value to be serialized
    buf         buffer, at least 11 characters wide 
    offs        offset in the buffer
    
    after return from this function, offs points to the char's past the value
    being read.
    ************************************************************************/
    private static void double2str(double v, char[] buf, ref int offs)
    {
        int i;
        int[] sixbits = new int[12];
        byte[] bytes = new byte[9];

        //
        // handle special quantities
        //
        if (System.Double.IsNaN(v))
        {
            buf[offs + 0] = '.';
            buf[offs + 1] = 'n';
            buf[offs + 2] = 'a';
            buf[offs + 3] = 'n';
            buf[offs + 4] = '_';
            buf[offs + 5] = '_';
            buf[offs + 6] = '_';
            buf[offs + 7] = '_';
            buf[offs + 8] = '_';
            buf[offs + 9] = '_';
            buf[offs + 10] = '_';
            offs += SER_ENTRY_LENGTH;
            return;
        }
        if (System.Double.IsPositiveInfinity(v))
        {
            buf[offs + 0] = '.';
            buf[offs + 1] = 'p';
            buf[offs + 2] = 'o';
            buf[offs + 3] = 's';
            buf[offs + 4] = 'i';
            buf[offs + 5] = 'n';
            buf[offs + 6] = 'f';
            buf[offs + 7] = '_';
            buf[offs + 8] = '_';
            buf[offs + 9] = '_';
            buf[offs + 10] = '_';
            offs += SER_ENTRY_LENGTH;
            return;
        }
        if (System.Double.IsNegativeInfinity(v))
        {
            buf[offs + 0] = '.';
            buf[offs + 1] = 'n';
            buf[offs + 2] = 'e';
            buf[offs + 3] = 'g';
            buf[offs + 4] = 'i';
            buf[offs + 5] = 'n';
            buf[offs + 6] = 'f';
            buf[offs + 7] = '_';
            buf[offs + 8] = '_';
            buf[offs + 9] = '_';
            buf[offs + 10] = '_';
            offs += SER_ENTRY_LENGTH;
            return;
        }

        //
        // process general case:
        // 1. copy v to array of chars
        // 2. set 9th byte to zero in order to simplify conversion to six-bit representation
        // 3. convert to little endian (if needed)
        // 4. convert to six-bit representation
        //    (last 12th element of sixbits is always zero, we do not output it)
        //
        byte[] _bytes = System.BitConverter.GetBytes((double)v);
        if (!System.BitConverter.IsLittleEndian)
            System.Array.Reverse(_bytes);
        for (i = 0; i < sizeof(double); i++)
            bytes[i] = _bytes[i];
        for (i = sizeof(double); i < 9; i++)
            bytes[i] = 0;
        threebytes2foursixbits(bytes, 0, sixbits, 0);
        threebytes2foursixbits(bytes, 3, sixbits, 4);
        threebytes2foursixbits(bytes, 6, sixbits, 8);
        for (i = 0; i < SER_ENTRY_LENGTH; i++)
            buf[offs + i] = sixbits2char(sixbits[i]);
        offs += SER_ENTRY_LENGTH;
    }


    /************************************************************************
    This function serializes ulong value into buffer

    v           ulong value to be serialized
    buf         buffer, at least 11 characters wide 
    offs        offset in the buffer
    
    after return from this function, offs points to the char's past the value
    being read.
    ************************************************************************/
    private static void ulong2str(ulong v, char[] buf, ref int offs)
    {
        int i;
        int[] sixbits = new int[12];
        byte[] bytes = new byte[9];

        //
        // process general case:
        // 1. copy v to array of chars
        // 2. set 9th byte to zero in order to simplify conversion to six-bit representation
        // 3. convert to little endian (if needed)
        // 4. convert to six-bit representation
        //    (last 12th element of sixbits is always zero, we do not output it)
        //
        byte[] _bytes = System.BitConverter.GetBytes((ulong)v);
        if (!System.BitConverter.IsLittleEndian)
            System.Array.Reverse(_bytes);
        for (i = 0; i < sizeof(ulong); i++)
            bytes[i] = _bytes[i];
        for (i = sizeof(ulong); i < 9; i++)
            bytes[i] = 0;
        threebytes2foursixbits(bytes, 0, sixbits, 0);
        threebytes2foursixbits(bytes, 3, sixbits, 4);
        threebytes2foursixbits(bytes, 6, sixbits, 8);
        for (i = 0; i < SER_ENTRY_LENGTH; i++)
            buf[offs + i] = sixbits2char(sixbits[i]);
        offs += SER_ENTRY_LENGTH;
    }

    /************************************************************************
    This function unserializes double value from string

    buf         buffer which contains value; leading spaces/tabs/newlines are 
                ignored, traling spaces/tabs/newlines are treated as  end  of
                the double value.
    offs        offset in the buffer
    
    after return from this function, offs points to the char's past the value
    being read.

    This function raises an error in case unexpected symbol is found
    ************************************************************************/
    private static double str2double(char[] buf, ref int offs)
    {
        string emsg = "ALGLIB: unable to read double value from stream";
        int[] sixbits = new int[12];
        byte[] bytes = new byte[9];
        byte[] _bytes = new byte[sizeof(double)];
        int sixbitsread, i;


        // 
        // skip leading spaces
        //
        while (buf[offs] == ' ' || buf[offs] == '\t' || buf[offs] == '\n' || buf[offs] == '\r')
            offs++;


        //
        // Handle special cases
        //
        if (buf[offs] == '.')
        {
            string s = new string(buf, offs, SER_ENTRY_LENGTH);
            if (s == ".nan_______")
            {
                offs += SER_ENTRY_LENGTH;
                return System.Double.NaN;
            }
            if (s == ".posinf____")
            {
                offs += SER_ENTRY_LENGTH;
                return System.Double.PositiveInfinity;
            }
            if (s == ".neginf____")
            {
                offs += SER_ENTRY_LENGTH;
                return System.Double.NegativeInfinity;
            }
            throw new alglibexception(emsg);
        }

        // 
        // General case:
        // 1. read and decode six-bit digits
        // 2. check that all 11 digits were read
        // 3. set last 12th digit to zero (needed for simplicity of conversion)
        // 4. convert to 8 bytes
        // 5. convert to big endian representation, if needed
        //
        sixbitsread = 0;
        while (buf[offs] != ' ' && buf[offs] != '\t' && buf[offs] != '\n' && buf[offs] != '\r' && buf[offs] != 0)
        {
            int d;
            d = char2sixbits(buf[offs]);
            if (d < 0 || sixbitsread >= SER_ENTRY_LENGTH)
                throw new alglibexception(emsg);
            sixbits[sixbitsread] = d;
            sixbitsread++;
            offs++;
        }
        if (sixbitsread != SER_ENTRY_LENGTH)
            throw new alglibexception(emsg);
        sixbits[SER_ENTRY_LENGTH] = 0;
        foursixbits2threebytes(sixbits, 0, bytes, 0);
        foursixbits2threebytes(sixbits, 4, bytes, 3);
        foursixbits2threebytes(sixbits, 8, bytes, 6);
        for (i = 0; i < sizeof(double); i++)
            _bytes[i] = bytes[i];
        if (!System.BitConverter.IsLittleEndian)
            System.Array.Reverse(_bytes);
        return System.BitConverter.ToDouble(_bytes, 0);
    }

    /************************************************************************
    This function unserializes ulong value from string

    buf         buffer which contains value; leading spaces/tabs/newlines are 
                ignored, traling spaces/tabs/newlines are treated as  end  of
                the ulong value.
    offs        offset in the buffer
    
    after return from this function, offs points to the char's past the value
    being read.

    This function raises an error in case unexpected symbol is found
    ************************************************************************/
    private static ulong str2ulong(char[] buf, ref int offs)
    {
        string emsg = "ALGLIB: unable to read ulong value from stream";
        int[] sixbits = new int[12];
        byte[] bytes = new byte[9];
        byte[] _bytes = new byte[sizeof(ulong)];
        int sixbitsread, i;


        // 
        // skip leading spaces
        //
        while (buf[offs] == ' ' || buf[offs] == '\t' || buf[offs] == '\n' || buf[offs] == '\r')
            offs++;

        // 
        // 1. read and decode six-bit digits
        // 2. check that all 11 digits were read
        // 3. set last 12th digit to zero (needed for simplicity of conversion)
        // 4. convert to 8 bytes
        // 5. convert to big endian representation, if needed
        //
        sixbitsread = 0;
        while (buf[offs] != ' ' && buf[offs] != '\t' && buf[offs] != '\n' && buf[offs] != '\r' && buf[offs] != 0)
        {
            int d;
            d = char2sixbits(buf[offs]);
            if (d < 0 || sixbitsread >= SER_ENTRY_LENGTH)
                throw new alglibexception(emsg);
            sixbits[sixbitsread] = d;
            sixbitsread++;
            offs++;
        }
        if (sixbitsread != SER_ENTRY_LENGTH)
            throw new alglibexception(emsg);
        sixbits[SER_ENTRY_LENGTH] = 0;
        foursixbits2threebytes(sixbits, 0, bytes, 0);
        foursixbits2threebytes(sixbits, 4, bytes, 3);
        foursixbits2threebytes(sixbits, 8, bytes, 6);
        for (i = 0; i < sizeof(ulong); i++)
            _bytes[i] = bytes[i];
        if (!System.BitConverter.IsLittleEndian)
            System.Array.Reverse(_bytes);
        return System.BitConverter.ToUInt64(_bytes, 0);
    }
}
