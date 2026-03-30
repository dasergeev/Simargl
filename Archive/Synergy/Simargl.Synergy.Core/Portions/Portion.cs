using System;
using System.Collections.Generic;
using System.Text;

namespace Simargl.Synergy.Core.Portions
{
    internal class Portion
    {
    }
}


//using System.IO;
//using System.Runtime.CompilerServices;

//namespace Simargl.Synergy.Core.Portions;

///// <summary>
///// Представляет базовый класс для всех порций данных.
///// </summary>
//internal abstract class Portion :
//    IDisposable
//{
//    /// <summary>
//    /// Поле для хранения блока памяти.
//    /// </summary>
//    protected readonly Block _Block;

//    /// <summary>
//    /// Инициализирует новый экземпляр.
//    /// </summary>
//    /// <param name="block">
//    /// Блок памяти.
//    /// </param>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    internal Portion(Block block)
//    {
//        //  Установка блока памяти.
//        _Block = block;
//    }

//    /// <summary>
//    /// Инициализирует новый экземпляр.
//    /// </summary>
//    /// <param name="format">
//    /// Значение, определяющее формат порции данных.
//    /// </param>
//    /// <param name="size">
//    /// Размер данных.
//    /// </param>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    protected Portion(PortionFormat format, int size) :
//        this(new Block(size))
//    {
//        //  Запись формата.
//        _Block.WriteInt32(0, (int)format);
//    }

//    /// <summary>
//    /// Возвращает блок памяти.
//    /// </summary>
//    public Block Block
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        get => _Block;
//    }

//    /// <summary>
//    /// Возвращает значение, определяющее формат порции данных.
//    /// </summary>
//    public PortionFormat Format
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        get => (PortionFormat)_Block.ReadInt32(0);
//    }

//    /// <summary>
//    /// Разрушает объект.
//    /// </summary>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public void Dispose()
//    {
//        //  Разрушение блока.
//        _Block.Dispose();
//    }

//    /// <summary>
//    /// Извлекает порцию из бллока.
//    /// </summary>
//    /// <param name="block">
//    /// Блок.
//    /// </param>
//    /// <returns>
//    /// Порция.
//    /// </returns>
//    public static Portion FromBlock(Block block)
//    {
//        //  Определение формата.
//        PortionFormat format = (PortionFormat)block.ReadInt32(0);

//        //  Разбор формата.
//        return format switch
//        {
//            PortionFormat.Confirm => new ConfirmPortion(block),
//            _ => throw new InvalidDataException("Неизвестный формат порции данных."),
//        };
//    }
//}
