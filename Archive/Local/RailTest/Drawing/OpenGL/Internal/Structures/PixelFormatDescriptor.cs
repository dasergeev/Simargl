using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет описатель формата пикселя графической поверхности.
    /// </summary>
    /// <remarks>
    /// Исходный синтаксис:
    /// typedef struct tagPIXELFORMATDESCRIPTOR
    /// {
    ///     WORD nSize;
    ///     WORD nVersion;
    ///     DWORD dwFlags;
    ///     BYTE iPixelType;
    ///     BYTE cColorBits;
    ///     BYTE cRedBits;
    ///     BYTE cRedShift;
    ///     BYTE cGreenBits;
    ///     BYTE cGreenShift;
    ///     BYTE cBlueBits;
    ///     BYTE cBlueShift;
    ///     BYTE cAlphaBits;
    ///     BYTE cAlphaShift;
    ///     BYTE cAccumBits;
    ///     BYTE cAccumRedBits;
    ///     BYTE cAccumGreenBits;
    ///     BYTE cAccumBlueBits;
    ///     BYTE cAccumAlphaBits;
    ///     BYTE cDepthBits;
    ///     BYTE cStencilBits;
    ///     BYTE cAuxBuffers;
    ///     BYTE iLayerType;
    ///     BYTE bReserved;
    ///     DWORD dwLayerMask;
    ///     DWORD dwVisibleMask;
    ///     DWORD dwDamageMask;
    /// } PIXELFORMATDESCRIPTOR, * PPIXELFORMATDESCRIPTOR, FAR * LPPIXELFORMATDESCRIPTOR;
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct PixelFormatDescriptor
    {
        /// <summary>
        /// Постоянная, определяющая стандартный размер структуры.
        /// </summary>
        public const int StandardSize = 40;

        /// <summary>
        /// Постоянная, определяющая стандартную версию данных структуры.
        /// </summary>
        public const int StandardVersion = 1;

        /// <summary>
        /// Поле для хранения размера этой структуры данных.
        /// Это значение должно быть равно <see cref="StandardSize"/>.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: WORD nSize.
        /// </remarks>
        private readonly ushort _Size;

        /// <summary>
        /// Поле для хранения версии данных структуры.
        /// Это значение должно быть равно <see cref="StandardVersion"/>.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: WORD nVersion.
        /// </remarks>
        private readonly ushort _Version;

        /// <summary>
        /// Поле для хранения набора битовых флагов, которые определяют свойства пиксельного буфера.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: DWORD dwFlags.
        /// </remarks>
        private uint _Flags;

        /// <summary>
        /// Поле для хранения типа данных пикселей.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE iPixelType.
        /// </remarks>
        private byte _PixelType;

        /// <summary>
        /// Поле для хранения количества цветовых битовых плоскостей в каждом цветовом буфере.
        /// Для типов пикселей RGBA это размер цветового буфера, исключая альфа-битовые плоскости.
        /// Для пикселей с цветовым индексом это размер буфера с цветовым индексом.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cColorBits.
        /// </remarks>
        private byte _ColorBits;

        /// <summary>
        /// Поле для хранения количества красных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cRedBits.
        /// </remarks>
        private byte _RedBits;

        /// <summary>
        /// Поле для хранения сдвига для красных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cRedShift.
        /// </remarks>
        private byte _RedShift;

        /// <summary>
        /// Поле для хранения количества зелёных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cGreenBits.
        /// </remarks>
        private byte _GreenBits;

        /// <summary>
        /// Поле для хранения сдвига для зелёных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cGreenShift.
        /// </remarks>
        private byte _GreenShift;

        /// <summary>
        /// Поле для хранения количества синих битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cBlueBits.
        /// </remarks>
        private byte _BlueBits;

        /// <summary>
        /// Поле для хранения сдвига для синих битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cBlueShift.
        /// </remarks>
        private byte _BlueShift;

        /// <summary>
        /// Поле для хранения количества битовых плоскостей альфа-канала в каждом цветовом буфере RGBA.
        /// Альфа-битпланы не поддерживаются.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cAlphaBits.
        /// </remarks>
        private readonly byte _AlphaBits;

        /// <summary>
        /// Поле для хранения сдвига для битовых плоскостей альфа-канала в каждом цветовом буфере RGBA.
        /// Альфа-битпланы не поддерживаются.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cAlphaShift.
        /// </remarks>
        private readonly byte _AlphaShift;

        /// <summary>
        /// Поле для хранения общего количества битовых плоскостей в буфере накопления.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cAccumBits.
        /// </remarks>
        private byte _AccumBits;

        /// <summary>
        /// Поле для хранения количества красных битовых плоскостей в буфере накопления.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cAccumRedBits.
        /// </remarks>
        private byte _AccumRedBits;

        /// <summary>
        /// Поле для хранения количества зелёных битовых плоскостей в буфере накопления.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cAccumGreenBits.
        /// </remarks>
        private byte _AccumGreenBits;

        /// <summary>
        /// Поле для хранения количества синих битовых плоскостей в буфере накопления.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cAccumBlueBits.
        /// </remarks>
        private byte _AccumBlueBits;

        /// <summary>
        /// Поле для хранения количества битовых плоскостей альфа-канала в буфере накопления.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cAccumAlphaBits.
        /// </remarks>
        private byte _AccumAlphaBits;

        /// <summary>
        /// Поле для хранения величины буфера глубины.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cDepthBits.
        /// </remarks>
        private byte _DepthBits;

        /// <summary>
        /// Поле для хранения глубина буфера трафарета.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cStencilBits.
        /// </remarks>
        private byte _StencilBits;

        /// <summary>
        /// Поле для хранения количества вспомогательных буферов.
        /// Вспомогательные буферы не поддерживаются.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE cAuxBuffers.
        /// </remarks>
        private readonly byte _AuxBuffers;

        /// <summary>
        /// Игнорируется. Более ранние реализации OpenGL использовали этот элемент, но он больше не используется.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE iLayerType.
        /// </remarks>
        private readonly byte _LayerType;

        /// <summary>
        /// Поле для хранения количества плоскостей наложения и подложки.
        /// Биты с 0 по 3 задают до 15 плоскостей наложения, а биты с 4 по 7 определяют до 15 плоскостей наложения.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: BYTE bReserved.
        /// </remarks>
        private byte _Reserved;

        /// <summary>
        /// Игнорируется. Более ранние реализации OpenGL использовали этот элемент, но он больше не используется.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: DWORD dwLayerMask.
        /// </remarks>
        private readonly uint _LayerMask;

        /// <summary>
        /// Поле для хранения прозрачного цвета или индекса плоскости подложки.
        /// Когда тип пикселя RGBA, - это прозрачное значение цвета RGB.
        /// Когда типом пикселя является индекс цвета, это прозрачное значение индекса.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: DWORD dwVisibleMask.
        /// </remarks>
        private uint _VisibleMask;

        /// <summary>
        /// Игнорируется. Более ранние реализации OpenGL использовали этот элемент, но он больше не используется.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: DWORD dwDamageMask.
        /// </remarks>
        private readonly uint _DamageMask;

        /// <summary>
        /// Инициализирует новый экземпляр структуры.
        /// </summary>
        /// <param name="flags">
        /// Набор битовых флагов, которые определяют свойства пиксельного буфера.
        /// </param>
        public PixelFormatDescriptor(PixelFormatDescriptorFlag flags) :
            this(flags, false, 0, 0)
        {

        }

        /// <summary>
        /// Инициализирует новый экземпляр структуры.
        /// </summary>
        /// <param name="flags">
        /// Набор битовых флагов, которые определяют свойства пиксельного буфера.
        /// </param>
        /// <param name="colorBits">
        /// Количество цветовых битовых плоскостей в каждом цветовом буфере.
        /// </param>
        /// <param name="depthBits">
        /// Величина буфера глубины.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="colorBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="colorBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="depthBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="depthBits"/> передано значение большее 255.
        /// </exception>
        public PixelFormatDescriptor(PixelFormatDescriptorFlag flags, int colorBits, int depthBits) :
            this(flags, false, colorBits, depthBits)
        {

        }

        /// <summary>
        /// Инициализирует новый экземпляр структуры.
        /// </summary>
        /// <param name="flags">
        /// Набор битовых флагов, которые определяют свойства пиксельного буфера.
        /// </param>
        /// <param name="isColorIndex">
        /// Значение, определяющее используется ли индексация цветов.
        /// </param>
        /// <param name="colorBits">
        /// Количество цветовых битовых плоскостей в каждом цветовом буфере.
        /// </param>
        /// <param name="depthBits">
        /// Величина буфера глубины.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="colorBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="colorBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="depthBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="depthBits"/> передано значение большее 255.
        /// </exception>
        public PixelFormatDescriptor(PixelFormatDescriptorFlag flags, bool isColorIndex, int colorBits, int depthBits) :
            this(flags, isColorIndex, colorBits, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, depthBits, 0, 0, Color.Black)
        {

        }

        /// <summary>
        /// Инициализирует новый экземпляр структуры.
        /// </summary>
        /// <param name="flags">
        /// Набор битовых флагов, которые определяют свойства пиксельного буфера.
        /// </param>
        /// <param name="isColorIndex">
        /// Значение, определяющее используется ли индексация цветов.
        /// </param>
        /// <param name="colorBits">
        /// Количество цветовых битовых плоскостей в каждом цветовом буфере.
        /// </param>
        /// <param name="redBits">
        /// Количество красных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </param>
        /// <param name="redShift">
        /// Сдвиг для красных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </param>
        /// <param name="greenBits">
        /// Количество зелёных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </param>
        /// <param name="greenShift">
        /// Сдвиг для зелёных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </param>
        /// <param name="blueBits">
        /// Количество синих битовых плоскостей в каждом цветовом буфере RGBA.
        /// </param>
        /// <param name="blueShift">
        /// Сдвиг для синих битовых плоскостей в каждом цветовом буфере RGBA.
        /// </param>
        /// <param name="accumBits">
        /// Общее количество битовых плоскостей в буфере накопления.
        /// </param>
        /// <param name="accumRedBits">
        /// Количество красных битовых плоскостей в буфере накопления.
        /// </param>
        /// <param name="accumGreenBits">
        /// Количество зелёных битовых плоскостей в буфере накопления.
        /// </param>
        /// <param name="accumBlueBits">
        /// Количество синих битовых плоскостей в буфере накопления.
        /// </param>
        /// <param name="accumAlphaBits">
        /// Количество битовых плоскостей альфа-канала в буфере накопления.
        /// </param>
        /// <param name="depthBits">
        /// Величина буфера глубины.
        /// </param>
        /// <param name="stencilBits">
        /// Глубина буфера трафарета.
        /// </param>
        /// <param name="overlayAndUnderlayPlanes">
        /// Количество плоскостей наложения и подложки.
        /// </param>
        /// <param name="visibleMask">
        /// Прозрачный цвет или индекс плоскости подложки.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="colorBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="colorBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="redBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="redBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="redShift"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="redShift"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="greenBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="greenBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="greenShift"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="greenShift"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="blueBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="blueBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="blueShift"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="blueShift"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="accumBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="accumBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="accumRedBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="accumRedBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="accumGreenBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="accumGreenBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="accumBlueBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="accumBlueBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="accumAlphaBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="accumAlphaBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="depthBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="depthBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="stencilBits"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="stencilBits"/> передано значение большее 255.
        /// - или -
        /// В параметре <paramref name="overlayAndUnderlayPlanes"/> передано отрицательное значение.
        /// - или -
        /// В параметре <paramref name="overlayAndUnderlayPlanes"/> передано значение большее 255.
        /// </exception>
        public PixelFormatDescriptor(PixelFormatDescriptorFlag flags, bool isColorIndex,
            int colorBits, int redBits, int redShift, int greenBits, int greenShift, int blueBits, int blueShift,
            int accumBits, int accumRedBits, int accumGreenBits, int accumBlueBits, int accumAlphaBits,
            int depthBits, int stencilBits, int overlayAndUnderlayPlanes, Color visibleMask)
        {
            _Size = StandardSize;
            _Version = StandardVersion;
            _Flags = 0;
            _PixelType = 0;
            _ColorBits = 0;
            _RedBits = 0;
            _RedShift = 0;
            _GreenBits = 0;
            _GreenShift = 0;
            _BlueBits = 0;
            _BlueShift = 0;
            _AlphaBits = 0;
            _AlphaShift = 0;
            _AccumBits = 0;
            _AccumRedBits = 0;
            _AccumGreenBits = 0;
            _AccumBlueBits = 0;
            _AccumAlphaBits = 0;
            _DepthBits = 0;
            _StencilBits = 0;
            _AuxBuffers = 0;
            _LayerType = 0;
            _Reserved = 0;
            _LayerMask = 0;
            _VisibleMask = 0;
            _DamageMask = 0;
            
            Flags = flags;
            IsColorIndex = isColorIndex;
            ColorBits = colorBits;
            RedBits = redBits;
            RedShift = redShift;
            GreenBits = greenBits;
            GreenShift = greenShift;
            BlueBits  =blueBits;
            BlueShift = blueShift;
            AccumBits = accumBits;
            AccumRedBits = accumRedBits;
            AccumGreenBits = accumGreenBits;
            AccumBlueBits = accumBlueBits;
            AccumAlphaBits = accumAlphaBits;
            DepthBits = depthBits;
            StencilBits = stencilBits;
            OverlayAndUnderlayPlanes = overlayAndUnderlayPlanes;
            VisibleMask = visibleMask;
        }

        /// <summary>
        /// Возвращает или задаёт набор битовых флагов, которые определяют свойства пиксельного буфера.
        /// </summary>
        public PixelFormatDescriptorFlag Flags
        {
            get
            {
                return (PixelFormatDescriptorFlag)_Flags;
            }
            set
            {
                _Flags = (uint)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт значение, определяющее используется ли индексация цветов.
        /// </summary>
        public bool IsColorIndex
        {
            get
            {
                return _PixelType != 0;
            }
            set
            {
                if (value)
                {
                    _PixelType = 1;
                }
                else
                {
                    _PixelType = 0;
                }
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество цветовых битовых плоскостей в каждом цветовом буфере.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int ColorBits
        {
            get
            {
                return _ColorBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("colorBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("colorBits", "Передано значение большее 255.");
                }
                _ColorBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество красных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int RedBits
        {
            get
            {
                return _RedBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("redBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("redBits", "Передано значение большее 255.");
                }
                _RedBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт сдвиг для красных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int RedShift
        {
            get
            {
                return _RedShift;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("redShift", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("redShift", "Передано значение большее 255.");
                }
                _RedShift = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество зелёных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int GreenBits
        {
            get
            {
                return _GreenBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("greenBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("greenBits", "Передано значение большее 255.");
                }
                _GreenBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт сдвиг для зелёных битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int GreenShift
        {
            get
            {
                return _GreenShift;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("greenShift", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("greenShift", "Передано значение большее 255.");
                }
                _GreenShift = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество синих битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int BlueBits
        {
            get
            {
                return _BlueBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("blueBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("blueBits", "Передано значение большее 255.");
                }
                _BlueBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт сдвиг для синих битовых плоскостей в каждом цветовом буфере RGBA.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int BlueShift
        {
            get
            {
                return _BlueShift;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("blueShift", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("blueShift", "Передано значение большее 255.");
                }
                _BlueShift = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт общее количество битовых плоскостей в буфере накопления.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int AccumBits
        {
            get
            {
                return _AccumBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("accumBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("accumBits", "Передано значение большее 255.");
                }
                _AccumBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество красных битовых плоскостей в буфере накопления.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int AccumRedBits
        {
            get
            {
                return _AccumRedBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("accumRedBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("accumRedBits", "Передано значение большее 255.");
                }
                _AccumRedBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество зелёных битовых плоскостей в буфере накопления.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int AccumGreenBits
        {
            get
            {
                return _AccumGreenBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("accumGreenBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("accumGreenBits", "Передано значение большее 255.");
                }
                _AccumGreenBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество синих битовых плоскостей в буфере накопления.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int AccumBlueBits
        {
            get
            {
                return _AccumBlueBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("accumBlueBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("accumBlueBits", "Передано значение большее 255.");
                }
                _AccumBlueBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество битовых плоскостей альфа-канала в буфере накопления.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int AccumAlphaBits
        {
            get
            {
                return _AccumAlphaBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("accumAlphaBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("accumAlphaBits", "Передано значение большее 255.");
                }
                _AccumAlphaBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт величину буфера глубины.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int DepthBits
        {
            get
            {
                return _DepthBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("depthBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("depthBits", "Передано значение большее 255.");
                }
                _DepthBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт глубину буфера трафарета.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int StencilBits
        {
            get
            {
                return _StencilBits;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("stencilBits", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("stencilBits", "Передано значение большее 255.");
                }
                _StencilBits = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество плоскостей наложения и подложки.
        /// Биты с 0 по 3 задают до 15 плоскостей наложения, а биты с 4 по 7 определяют до 15 плоскостей наложения.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Передано отрицательное значение.
        /// - или -
        /// Передано значение большее 255.
        /// </exception>
        public int OverlayAndUnderlayPlanes
        {
            get
            {
                return _Reserved;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("overlayAndUnderlayPlanes", "Передано отрицательное значение.");
                }
                if (value > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("overlayAndUnderlayPlanes", "Передано значение большее 255.");
                }
                _Reserved = (byte)value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт прозрачный цвет или индекс плоскости подложки.
        /// </summary>
        public Color VisibleMask
        {
            get
            {
                return Color.FromArgb(
                    (byte)((_VisibleMask >> 24) & 0xff),
                    (byte)(_VisibleMask & 0xff),
                    (byte)((_VisibleMask >> 8) & 0xff),
                    (byte)((_VisibleMask >> 16) & 0xff));
            }
            set
            {
                _VisibleMask = value.R | (((uint)value.G) << 8) | (((uint)value.B) << 16) | (((uint)value.A) << 24);
            }
        }
    }
}
