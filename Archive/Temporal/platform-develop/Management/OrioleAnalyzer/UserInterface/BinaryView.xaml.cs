using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace Apeiron.Oriole.Analysis.UserInterface
{
    /// <summary>
    /// Логика взаимодействия для BinaryView.xaml
    /// </summary>
    public partial class BinaryView : UserControl
    {
        private readonly ObservableCollection<BinaryRow> _Rows;

        /// <summary>
        /// 
        /// </summary>
        public BinaryView()
        {
            InitializeComponent();
            _Rows = new();
            _ListView.ItemsSource = _Rows;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        public void Load(byte[] buffer)
        {
            _Rows.Clear();
            int length = buffer.Length;
            if (length % 16 != 0)
            {
                length += 16 - (length % 16);
                Array.Resize(ref buffer, length);
            }
            int offset = 0;
            while (offset + 16 <= length)
            {
                _Rows.Add(new(buffer, offset, 16));
                offset += 16;
            }
        }

        private class BinaryRow
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <param name="length"></param>
            public BinaryRow(byte[] buffer, int offset, int length)
            {

                Offset = offset;

                Byte0 = buffer[offset + 0x0];
                Byte1 = buffer[offset + 0x1];
                Byte2 = buffer[offset + 0x2];
                Byte3 = buffer[offset + 0x3];

                Byte4 = buffer[offset + 0x4];
                Byte5 = buffer[offset + 0x5];
                Byte6 = buffer[offset + 0x6];
                Byte7 = buffer[offset + 0x7];

                Byte8 = buffer[offset + 0x8];
                Byte9 = buffer[offset + 0x9];
                ByteA = buffer[offset + 0xA];
                ByteB = buffer[offset + 0xB];

                ByteC = buffer[offset + 0xC];
                ByteD = buffer[offset + 0xD];
                ByteE = buffer[offset + 0xE];
                ByteF = buffer[offset + 0xF];

                StringBuilder text = new(16);
                //char[] chars = Encoding.ASCII.GetChars(buffer, offset, length);
                for (int i = 0; i < 16; i++)
                {
                    byte value = buffer[offset + i];
                    if (value < 0x20) value = 0x2E;
                    if (value >= 0x7F) value = 0x2E;
                    text.Append(Encoding.ASCII.GetChars(new byte[] { value }, 0, 1)[0]);
                }

                Text = text.ToString();
            }

            public int Offset { get; set; }

            public byte Byte0 { get; set; }
            public byte Byte1 { get; set; }
            public byte Byte2 { get; set; }
            public byte Byte3 { get; set; }

            public byte Byte4 { get; set; }
            public byte Byte5 { get; set; }
            public byte Byte6 { get; set; }
            public byte Byte7 { get; set; }

            public byte Byte8 { get; set; }
            public byte Byte9 { get; set; }
            public byte ByteA { get; set; }
            public byte ByteB { get; set; }

            public byte ByteC { get; set; }
            public byte ByteD { get; set; }
            public byte ByteE { get; set; }
            public byte ByteF { get; set; }

            public string Text { get; set; } = null!;
        }
    }
}
