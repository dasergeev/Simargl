using Simargl.Designing;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Platform.TcpClientReceiver
{

    /// <summary>
    /// Представляет класс получения данных Tcp клиента. Оболочка для всех регистраторов.
    /// </summary>
    public class TcpReceiver
    {
        /// <summary>
        /// Константа порции данных.
        /// </summary>
        private readonly int _PortionSize;

        /// <summary>
        /// Представляет размера буфера.
        /// </summary>
        private readonly int _BufferSize;

        /// <summary>
        /// Представляет буфер промежуточного копирования.
        /// </summary>
        private readonly byte[] _Portion;

        /// <summary>
        /// Представляет время ожидания данных в милисекундах.
        /// </summary>
        private readonly TimeSpan _Timeout;

        /// <summary>
        /// Представляет массив данных от регистратора.
        /// </summary>
        private readonly byte[] Buffer;

        /// <summary>
        /// Представляет индекс буфера на свободный элемент.
        /// </summary>
        private long FreeIndex = 0;


        /// <summary>
        /// Инициализирует объект класса.
        /// </summary>
        /// <param name="bufferSize">Размер буфера</param>
        /// <param name="portionSize">Размер порции.</param>
        /// <param name="time">Время ожидания буфера данных.</param>
        /// <exception cref="ArgumentNullException">Передан параметр вне диапазона (отрацательный).</exception>
        public TcpReceiver(int bufferSize,int portionSize, int time)
        {
            //  Проверка параметра
            IsNotLess(bufferSize,1,nameof(bufferSize));

            //  Проверка параметра
            IsNotLess(portionSize, 1, nameof(portionSize));

            //  Проверка параметра
            IsNotLess(time, 1, nameof(time));

            //  Установка размера буфера.
            _BufferSize = bufferSize;

            //  Установка размера порции данных.
            _PortionSize = portionSize;

            //  Установка интервала
            _Timeout = new(0, 0, 0, 0, time);

            //  Инициализация буфера.
            Buffer = new byte[_BufferSize];

            //  Инициализация буфера.
            _Portion = new byte[_PortionSize];
        }


        /// <summary>
        /// Представляет функцию получения буфера данных.
        /// </summary>
        /// <param name="stream">Поток.</param>
        /// <param name="token">Токен отмены.</param>
        public async Task ReceiveBufferAsync(NetworkStream stream, CancellationToken token)
        {

            //  Создание потока буфера.
            using var memory = new MemoryStream(Buffer);
            //  Изменение позиции согласно предыдущей операции.
            memory.Position = FreeIndex;

            //  Создание писателя
            using var writer = new BinaryWriter(memory, Encoding.UTF8, true);

            //  Установка времени начала операций чтения
            DateTime lastReceiveTime = DateTime.Now;

            //  Выполнять пока есть данные и интервал не истек.
            while (((DateTime.Now - lastReceiveTime) < _Timeout) && token.IsCancellationRequested == false)
            {
                if ((memory.Position + _PortionSize) >= _BufferSize)
                {
                    break;
                }

                if (stream.DataAvailable)
                {
                    //  Чтение порции данных из потока соединения.
                    var count = await stream.ReadAsync(_Portion, token).ConfigureAwait(false);

                    //  Запись в буффер полученые данные
                    writer.Write(_Portion, 0, count);

                    //  Продолжение цикла.
                    continue;
                }

                //  Ожидание.
                await Task.Delay(50, token).ConfigureAwait(false);
            }

            //  Установка нового значение длины данных
            FreeIndex = memory.Position;

        }


        /// <summary>
        /// Представляет перераспределение массива данных, на основе идекса остатка.
        /// </summary>
        /// <param name="busyIndex">Индекс остатка.</param>
        public void FinallyShift(long busyIndex)
        {
            //  Проверка что нет данных.
            if(FreeIndex == busyIndex || FreeIndex == 0)
            {
                FreeIndex = 0;
                return;
            }

            //  Подсчет длинны.
            long length = FreeIndex - busyIndex;

            //  Создание массива
            var data = new byte[length];

            //  Копирование обработанных данных.
            Array.Copy(Buffer, busyIndex, data, 0, length);

            //  Установка нового счетчика
            FreeIndex = length;

            //  Перенос остатка в начало буфера
            Array.Copy(data, 0, Buffer, 0, length);

        }

        /// <summary>
        /// Представляет получение законченного массива данных.
        /// </summary>
        /// <returns>Массив данных.</returns>
        public byte[] GetData()
        {

            //  Инициализация массива.
            var data = new byte[FreeIndex];

            //  Копирование.
            Array.Copy(Buffer, data, FreeIndex);

            //  Возврат массива.
            return data;
        }
    }
}
