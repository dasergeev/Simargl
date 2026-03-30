using System;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет значение, определяющее битовые флаги, которые используются для задания свойств пиксельного буфера.
    /// </summary>
    [Flags]
    internal enum PixelFormatDescriptorFlag : long
    {
        /// <summary>
        /// Буфер имеет двойную буферизацию. Этот флаг и <see cref="SupportGdi"/> являются взаимоисключающими в текущей универсальной реализации.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_DOUBLEBUFFER.
        /// </remarks>
        DoubleBuffer = 0x00000001,

        /// <summary>
        /// Буфер стереоскопичен. Этот флаг не поддерживается в текущей универсальной реализации.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_STEREO.
        /// </remarks>
        Stereo = 0x00000002,

        /// <summary>
        /// Буфер может отображаться на поверхности окна или устройства.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_DRAW_TO_WINDOW.
        /// </remarks>
        DrawToWindow = 0x00000004,

        /// <summary>
        /// Буфер может отображаться на растровое изображение в памяти.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_DRAW_TO_BITMAP.
        /// </remarks>
        DrawToBitmap = 0x00000008,

        /// <summary>
        /// Буфер поддерживает рисование GDI. Этот флаг и <see cref="DoubleBuffer"/> являются взаимоисключающими в текущей универсальной реализации.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_SUPPORT_GDI.
        /// </remarks>
        SupportGdi = 0x00000010,

        /// <summary>
        /// Буфер поддерживает рисование OpenGL.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_SUPPORT_OPENGL.
        /// </remarks>
        SupportOpenGL = 0x00000020,

        /// <summary>
        /// Формат пикселей поддерживается программной реализацией GDI, которая также известна как универсальная реализация.
        /// Если этот бит очищен, то формат пикселя поддерживается драйвером устройства или аппаратным обеспечением.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_GENERIC_FORMAT.
        /// </remarks>
        GenericFormat = 0x00000040,

        /// <summary>
        /// The buffer uses RGBA pixels on a palette-managed device. A logical palette is required to achieve the best results for this pixel type. Colors in the palette should be specified according to the values of the cRedBits, cRedShift, cGreenBits, cGreenShift, cBluebits, and cBlueShift members. The palette should be created and realized in the device context before calling wglMakeCurrent.
        /// Буфер использует пиксели RGBA на устройстве, управляемом палитрой.
        /// Для достижения наилучших результатов для данного типа пикселей требуется логическая палитра.
        /// Цвета в палитре должны быть указаны в соответствии со значениями элементов
        /// <see cref="PixelFormatDescriptor.RedBits"/>, <see cref="PixelFormatDescriptor.RedShift"/>,
        /// <see cref="PixelFormatDescriptor.GreenBits"/>, <see cref="PixelFormatDescriptor.GreenShift"/>,
        /// <see cref="PixelFormatDescriptor.BlueBits"/>, <see cref="PixelFormatDescriptor.BlueShift"/>.
        /// Палитра должна быть создана и реализована в контексте устройства перед вызовом wglMakeCurrent.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_NEED_PALETTE.
        /// </remarks>
        NeedPalette = 0x00000080,

        /// <summary>
        /// Defined in the pixel format descriptors of hardware that supports one hardware palette in 256-color mode only. For such systems to use hardware acceleration, the hardware palette must be in a fixed order (for example, 3-3-2) when in RGBA mode or must match the logical palette when in color-index mode.When this flag is set, you must call SetSystemPaletteUse in your program to force a one-to-one mapping of the logical palette and the system palette. If your OpenGL hardware supports multiple hardware palettes and the device driver can allocate spare hardware palettes for OpenGL, this flag is typically clear.
        /// This flag is not set in the generic pixel formats.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_NEED_SYSTEM_PALETTE.
        /// </remarks>
        NeedSystemPalette = 0x00000100,

        /// <summary>
        /// Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap. Swapping the color buffers causes the content of the back buffer to be copied to the front buffer. The content of the back buffer is not affected by the swap. PFD_SWAP_COPY is a hint only and might not be provided by a driver.
        /// With the glAddSwapHintRectWIN extension function, two new flags are included for the PIXELFORMATDESCRIPTOR pixel format structure.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_SWAP_COPY.
        /// </remarks>
        SwapCopy = 0x00000400,

        /// <summary>
        /// Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap. Swapping the color buffers causes the exchange of the back buffer's content with the front buffer's content. Following the swap, the back buffer's content contains the front buffer's content before the swap. PFD_SWAP_EXCHANGE is a hint only and might not be provided by a driver.
        /// With the glAddSwapHintRectWIN extension function, two new flags are included for the PIXELFORMATDESCRIPTOR pixel format structure.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_SWAP_EXCHANGE.
        /// </remarks>
        SwapExchange = 0x00000200,

        /// <summary>
        /// Indicates whether a device can swap individual layer planes with pixel formats that include double-buffered overlay or underlay planes. Otherwise all layer planes are swapped together as a group. When this flag is set, wglSwapLayerBuffers is supported.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_SWAP_LAYER_BUFFERS.
        /// </remarks>
        SwapLayerBuffers = 0x00000800,

        /// <summary>
        /// The pixel format is supported by a device driver that accelerates the generic implementation. If this flag is clear and the PFD_GENERIC_FORMAT flag is set, the pixel format is supported by the generic implementation only.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_GENERIC_ACCELERATED.
        /// </remarks>
        GenericAccelerated = 0x00001000,

        /// <summary>
        /// The requested pixel format can either have or not have a depth buffer. To select a pixel format without a depth buffer, you must specify this flag. The requested pixel format can be with or without a depth buffer. Otherwise, only pixel formats with a depth buffer are considered.
        /// You can specify the following bit flags when calling ChoosePixelFormat.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_DEPTH_DONTCARE.
        /// </remarks>
        DepthDontCare = 0x20000000,

        /// <summary>
        /// The requested pixel format can be either single- or double-buffered.
        /// You can specify the following bit flags when calling ChoosePixelFormat.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_DOUBLEBUFFER_DONTCARE.
        /// </remarks>
        DoubleBufferDontCare = 0x40000000,

        /// <summary>
        /// The requested pixel format can be either monoscopic or stereoscopic.
        /// You can specify the following bit flags when calling ChoosePixelFormat.
        /// </summary>
        /// <remarks>
        /// Исходный синтаксис: PFD_STEREO_DONTCARE.
        /// </remarks>
        StereoDontCare = 0x80000000,
    }
}
