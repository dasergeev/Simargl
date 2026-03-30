using RailTest.Algebra;
using System;

namespace RailTest.Border
{
    /// <summary>
    /// Представляет информацию об оси на сечении.
    /// </summary>
    public class AxisSectionInfo
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="frameNumber">
        /// Номер кадра.
        /// </param>
        /// <param name="beginBlockIndex">
        /// Индекс начального блока.
        /// </param>
        /// <param name="endBlockIndex">
        /// Индекс конечного блока.
        /// </param>
        /// <param name="group">
        /// Группа сигналов.
        /// </param>
        internal AxisSectionInfo(int frameNumber, int beginBlockIndex, int endBlockIndex, SectionGroup group)
        {
            Group = group;
            BeginTimeLabel = new TimeLabel(frameNumber, beginBlockIndex);
            EndTimeLabel = new TimeLabel(frameNumber, endBlockIndex);
            if (BeginTimeLabel.BlockIndex > EndTimeLabel.BlockIndex)
            {
                --BeginTimeLabel.FrameNumber;
            }

            BeginTimeLabel.BlockIndex -= 2;
            if (BeginTimeLabel.BlockIndex < 0)
            {
                BeginTimeLabel.BlockIndex += SignalBuilder.BlockCount;
                --BeginTimeLabel.FrameNumber;
            }

            EndTimeLabel.BlockIndex += 2;
            if (EndTimeLabel.BlockIndex >= SignalBuilder.BlockCount)
            {
                EndTimeLabel.BlockIndex -= SignalBuilder.BlockCount;
                ++EndTimeLabel.FrameNumber;
            }

            long ticks = (BeginTimeLabel.Ticks + EndTimeLabel.Ticks) >> 1;
            Time = ticks / 2000.0;
            Speed = double.NaN;

            P = new double[2];
            P[0] = double.NaN;
            P[1] = double.NaN;

            F = new double[2];
            F[0] = double.NaN;
            F[1] = double.NaN;

            M = new double[2];
            M[0] = double.NaN;
            M[1] = double.NaN;

            P2Mean = new double[2];
            P2Mean[0] = double.NaN;
            P2Mean[1] = double.NaN;

            P2Min = new double[2];
            P2Min[0] = double.NaN;
            P2Min[1] = double.NaN;

            P2Max = new double[2];
            P2Max[0] = double.NaN;
            P2Max[1] = double.NaN;

            P2Sko = new double[2];
            P2Sko[0] = double.NaN;
            P2Sko[1] = double.NaN;

            _PData = new RealVector[2];
            _FData = new RealVector[2];
            _MData = new RealVector[2];
            _P2Data = new RealVector[2];

            _Index = new int[2];
        }

        /// <summary>
        /// Возвращает начальную отметку времени.
        /// </summary>
        public TimeLabel BeginTimeLabel { get; }

        /// <summary>
        /// Возвращает конечную отметку времени.
        /// </summary>
        public TimeLabel EndTimeLabel { get; }

        /// <summary>
        /// Возвращает время прохождения.
        /// </summary>
        public double Time { get; }

        /// <summary>
        /// Возвращает скорость движения.
        /// </summary>
        public double Speed { get; internal set; }

        /// <summary>
        /// Возвращает массив вертикальных сил.
        /// </summary>
        public double[] P { get; internal set; }

        /// <summary>
        /// Возвращает массив боковых сил.
        /// </summary>
        public double[] F { get; internal set; }

        /// <summary>
        /// Возвращает массив моментов.
        /// </summary>
        public double[] M { get; internal set; }

        /// <summary>
        /// Возвращает массив средних значений вертикальных сил по методу двух сечений.
        /// </summary>
        public double[] P2Mean { get; internal set; }

        /// <summary>
        /// Возвращает массив минимальных значений вертикальных сил по методу двух сечений.
        /// </summary>
        public double[] P2Min { get; internal set; }

        /// <summary>
        /// Возвращает массив максимальных значений вертикальных сил по методу двух сечений.
        /// </summary>
        public double[] P2Max { get; internal set; }

        /// <summary>
        /// Возвращает массив отклонений вертикальных сил по методу двух сечений.
        /// </summary>
        public double[] P2Sko { get; internal set; }

        /// <summary>
        /// Возвращает группу сигналов.
        /// </summary>
        internal SectionGroup Group { get; }

        /// <summary>
        /// Поле для хранения массива индексов максимальных вертикальных сил.
        /// </summary>
        private readonly int[] _Index;

        /// <summary>
        /// Поле для хранения массива данных вертикальных сил.
        /// </summary>
        private readonly RealVector[] _PData;

        /// <summary>
        /// Поле для хранения массива данных боковых сил.
        /// </summary>
        [CLSCompliant(false)]
        public RealVector[] _FData;

        /// <summary>
        /// Поле для хранения массива данных моментов.
        /// </summary>
        [CLSCompliant(false)]
        public RealVector[] _MData;

        /// <summary>
        /// Поле для хранения массива данных вертикальных сил по методу двух сечений.
        /// </summary>
        [CLSCompliant(false)]
        public RealVector[] _P2Data;

        /// <summary>
        /// Выполняет работу.
        /// </summary>
        internal void Work()
        {
            if (_PData[0] is null)
            {
                _PData[0] = Group.Right.Vertical.Read(BeginTimeLabel.BlockIndex, EndTimeLabel.BlockIndex);
                _Index[0] = _PData[0].IndexMax;
                P[0] = _PData[0][_Index[0]];
            }
            if (_PData[1] is null)
            {
                _PData[1] = Group.Left.Vertical.Read(BeginTimeLabel.BlockIndex, EndTimeLabel.BlockIndex);
                _Index[1] = _PData[1].IndexMax;
                P[1] = _PData[1][_Index[1]];
            }

            if (_FData[0] is null)
            {
                _FData[0] = Group.Right.Lateral.Read(BeginTimeLabel.BlockIndex, EndTimeLabel.BlockIndex);
                F[0] = _FData[0][_Index[0]];
            }
            if (_FData[1] is null)
            {
                _FData[1] = Group.Left.Lateral.Read(BeginTimeLabel.BlockIndex, EndTimeLabel.BlockIndex);
                F[1] = _FData[1][_Index[1]];
            }

            if (_MData[0] is null)
            {
                _MData[0] = Group.Right.Moment.Read(BeginTimeLabel.BlockIndex, EndTimeLabel.BlockIndex);
                M[0] = _MData[0][_Index[0]];
            }
            if (_MData[1] is null)
            {
                _MData[1] = Group.Left.Moment.Read(BeginTimeLabel.BlockIndex, EndTimeLabel.BlockIndex);
                M[1] = _MData[1][_Index[1]];
            }

            if (_P2Data[0] is null)
            {
                _P2Data[0] = Group.Right.Continuous.Read(BeginTimeLabel.BlockIndex, EndTimeLabel.BlockIndex);
            }
            if (_P2Data[1] is null)
            {
                _P2Data[1] = Group.Left.Continuous.Read(BeginTimeLabel.BlockIndex, EndTimeLabel.BlockIndex);
            }

            if (!double.IsNaN(Speed) && Math.Abs(Speed) > 0)
            {
                double range = 0.25 * 0.068;
                int deltaIndex = (int)Math.Round(2000.0 * range / Math.Abs(Speed));
                if (deltaIndex > 3)
                {
                    for (int i = 0; i != 2; i++)
                    {
                        int beginIndex = _Index[i] - deltaIndex;
                        int endIndex = _Index[i] + deltaIndex;
                        if (beginIndex < 0)
                        {
                            beginIndex = 0;
                        }
                        if (endIndex >= _P2Data[i].Length)
                        {
                            endIndex = _P2Data[i].Length - 1;
                        }
                        RealVector vector = new RealVector(endIndex - beginIndex);
                        for (int j = 0; j != vector.Length; ++j)
                        {
                            vector[j] = _P2Data[i][beginIndex + j];
                        }
                        P2Mean[i] = vector.Average;
                        P2Min[i] = vector.Min;
                        P2Max[i] = vector.Max;
                        P2Sko[i] = vector.StandardDeviation;
                    }
                }
            }
        }
    }
}
