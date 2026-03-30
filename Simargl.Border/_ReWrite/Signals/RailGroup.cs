//using RailTest.Algebra;
//using System.Collections.Generic;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет группу сигналов на одном рельсе, в одном сечении.
//    /// </summary>
//    public class RailGroup
//    {
//        private readonly double[] _PFactors;
//        private readonly double[] _FFactors;
//        private readonly double[] _MFactors;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="section">
//        /// Номер сечения.
//        /// </param>
//        /// <param name="rail">
//        /// Значение, определяющее рельс.
//        /// </param>
//        internal RailGroup(int section, Rail rail)
//        {
//            Section = section;
//            Rail = rail;

//            Continuous = new Signal("PC" + Rail.ToString()[0] + section.ToString("00"), "kN", true);

//            Vertical = new Signal("P" + Rail.ToString()[0] + section.ToString("00"), "kN", true);
//            Lateral = new Signal("F" + Rail.ToString()[0] + section.ToString("00"), "kN", true);
//            Moment = new Signal("M" + Rail.ToString()[0] + section.ToString("00"), "N*m", true);

//            External = new SideGroup(section, rail, Side.External);
//            Internal = new SideGroup(section, rail, Side.Internal);

//            Signals = new List<Signal>
//            {
//                Continuous,
//                Vertical,
//                Lateral,
//                Moment
//            };
//            Signals.AddRange(External.Signals);
//            Signals.AddRange(Internal.Signals);

//            _PFactors = new double[4];
//            _FFactors = new double[4];
//            _MFactors = new double[4];

//            if (rail == Rail.Right)
//            {
//                _PFactors[0] = -0.007756567;
//                _PFactors[1] = -0.006143675;
//                _PFactors[2] = -0.008389439;
//                _PFactors[3] = -0.004532694;

//                _FFactors[0] = -0.000999496;
//                _FFactors[1] = 0.003178214;
//                _FFactors[2] = 0.001283404;
//                _FFactors[3] = -0.00309948;

//                _MFactors[0] = 0.152464116;
//                _MFactors[1] = 0.106417953;
//                _MFactors[2] = -0.17670708;
//                _MFactors[3] = -0.119681737;
//            }
//            else
//            {
//                _PFactors[0] = -0.008503084;
//                _PFactors[1] = -0.005796226;
//                _PFactors[2] = -0.008051301;
//                _PFactors[3] = -0.004032223;

//                _FFactors[0] = -0.001116288;
//                _FFactors[1] = 0.003273377;
//                _FFactors[2] = 0.000966514;
//                _FFactors[3] = -0.003198094;

//                _MFactors[0] = 0.184084533;
//                _MFactors[1] = 0.118443871;
//                _MFactors[2] = -0.162429982;
//                _MFactors[3] = -0.103054066;
//            }

//            ContinuousConstFactor = Factors.GetFactor(rail, section);   // 0.006064901;
//            //if (rail == Rail.Left)
//            //{
//            //    switch (section)
//            //    {
//            //        case 1: ContinuousConstFactor = 0.006235673471; break;
//            //        case 2: ContinuousConstFactor = 0.006569676121; break;
//            //        case 3: ContinuousConstFactor = 0.006492759201; break;
//            //        case 4: ContinuousConstFactor = 0.006452108001; break;
//            //        case 5: ContinuousConstFactor = 0.006891115561; break;
//            //        case 6: ContinuousConstFactor = 0.006391908791; break;
//            //        case 7: ContinuousConstFactor = 0.006888257181; break;
//            //        case 8: ContinuousConstFactor = 0.006479978611; break;
//            //        case 9: ContinuousConstFactor = 0.013406733701; break;
//            //        case 10: ContinuousConstFactor = 0.006375388421; break;
//            //        case 11: ContinuousConstFactor = 0.006937897881; break;
//            //        case 12: ContinuousConstFactor = 0.005121076801; break;
//            //        case 13: ContinuousConstFactor = 0.005250672721; break;
//            //        case 14: ContinuousConstFactor = 0.004967321641; break;
//            //        case 15: ContinuousConstFactor = 0.005246647191; break;
//            //        case 16: ContinuousConstFactor = 0.005046837821; break;
//            //        case 17: ContinuousConstFactor = 0.005254949411; break;
//            //        case 18: ContinuousConstFactor = 0.005221707191; break;
//            //        case 19: ContinuousConstFactor = 0.005170239241; break;
//            //        case 20: ContinuousConstFactor = 0.005009831581; break;
//            //        case 21: ContinuousConstFactor = 0.005445425551; break;
//            //    }
//            //}
//            //else
//            //{
//            //    switch (section)
//            //    {
//            //        case 1: ContinuousConstFactor = 0.006040950521; break;
//            //        case 2: ContinuousConstFactor = 0.005949080111; break;
//            //        case 3: ContinuousConstFactor = 0.006210696721; break;
//            //        case 4: ContinuousConstFactor = 0.005942978861; break;
//            //        case 5: ContinuousConstFactor = 0.005904360831; break;
//            //        case 6: ContinuousConstFactor = 0.005812079371; break;
//            //        case 7: ContinuousConstFactor = 0.004773296811; break;
//            //        case 8: ContinuousConstFactor = 0.006058657361; break;
//            //        case 9: ContinuousConstFactor = 0.006120624521; break;
//            //        case 10: ContinuousConstFactor = 0.006108597661; break;
//            //        case 11: ContinuousConstFactor = 0.006065452251; break;
//            //        case 12: ContinuousConstFactor = 0.004808519151; break;
//            //        case 13: ContinuousConstFactor = 0.004848243531; break;
//            //        case 14: ContinuousConstFactor = 0.004950350431; break;
//            //        case 15: ContinuousConstFactor = 0.004988825961; break;
//            //        case 16: ContinuousConstFactor = 0.004884538511; break;
//            //        case 17: ContinuousConstFactor = 0.004843603181; break;
//            //        case 18: ContinuousConstFactor = 0.004958044851; break;
//            //        case 19: ContinuousConstFactor = 0.004799010011; break;
//            //        case 20: ContinuousConstFactor = 0.004777860791; break;
//            //        case 21: ContinuousConstFactor = 0.005025982911; break;
//            //    }
//            //}
//        }

//        /// <summary>
//        /// Возвращает сигнал вертикальной силы по методу двух сечений.
//        /// </summary>
//        public Signal Continuous { get; }

//        /// <summary>
//        /// Возвращает сигнал вертикальной силы.
//        /// </summary>
//        public Signal Vertical { get; }

//        /// <summary>
//        /// Возвращает сигнал боковой силы.
//        /// </summary>
//        public Signal Lateral { get; }

//        /// <summary>
//        /// Возвращает сигнал момента.
//        /// </summary>
//        public Signal Moment { get; }

//        /// <summary>
//        /// Возвращает номер сечения.
//        /// </summary>
//        public int Section { get; }

//        /// <summary>
//        /// Возвращает значение, определяющее рельс.
//        /// </summary>
//        public Rail Rail { get; }

//        /// <summary>
//        /// Возвращает группу сигналов на внешней стороне.
//        /// </summary>
//        public SideGroup External { get; }

//        /// <summary>
//        /// Возвращает группу сигналов на внутренней стороне.
//        /// </summary>
//        public SideGroup Internal { get; }

//        /// <summary>
//        /// Возвращает все сигналы.
//        /// </summary>
//        public List<Signal> Signals { get; }

//        /// <summary>
//        /// Возвращает масштаб сигнала вертикальной силы по методу двух сечений.
//        /// </summary>
//        public double ContinuousConstFactor { get; }

//        /// <summary>
//        /// Устанавливает ноль.
//        /// </summary>
//        internal void Zero()
//        {
//            External.Zero();
//            Internal.Zero();
//        }
        
//        /// <summary>
//        /// Обновляет данные.
//        /// </summary>
//        /// <param name="blockIndex">
//        /// Индекс чтения.
//        /// </param>
//        internal unsafe void Update(int blockIndex)
//        {
//            External.Update(blockIndex);
//            Internal.Update(blockIndex);

//            RealVector external = External.Signal0.LastData;
//            RealVector @internal = Internal.Signal0.LastData;
//            RealVector result = new RealVector(SectionGroupCollection.BlockSize);

//            if (Rail == Rail.Right)
//            {
//                for (int i = 0; i != SectionGroupCollection.BlockSize; ++i)
//                {
//                    result[i] = @internal[i] - external[i];
//                }
//            }
//            else
//            {
//                for (int i = 0; i != SectionGroupCollection.BlockSize; ++i)
//                {
//                    result[i] = external[i] - @internal[i];
//                }
//            }
//            result.Scale(ContinuousConstFactor);
//            Continuous.Write(blockIndex, (double*)result.Pointer.ToPointer());

//            RealVector signal0 = Internal.Signal1.LastData;
//            RealVector signal1 = Internal.Signal2.LastData;
//            RealVector signal2 = External.Signal1.LastData;
//            RealVector signal3 = External.Signal2.LastData;

//            RealVector vertical = new RealVector(SectionGroupCollection.BlockSize);
//            RealVector lateral = new RealVector(SectionGroupCollection.BlockSize);
//            RealVector moment = new RealVector(SectionGroupCollection.BlockSize);

//            for (int i = 0; i != SectionGroupCollection.BlockSize; ++i)
//            {
//                vertical[i] = _PFactors[0] * signal0[i] + _PFactors[1] * signal1[i]
//                    + _PFactors[2] * signal2[i] + _PFactors[3] * signal3[i];
//                lateral[i] = _FFactors[0] * signal0[i] + _FFactors[1] * signal1[i]
//                    + _FFactors[2] * signal2[i] + _FFactors[3] * signal3[i];
//                moment[i] = _MFactors[0] * signal0[i] + _MFactors[1] * signal1[i]
//                    + _MFactors[2] * signal2[i] + _MFactors[3] * signal3[i];
//            }

//            Vertical.Write(blockIndex, (double*)vertical.Pointer.ToPointer());
//            Lateral.Write(blockIndex, (double*)lateral.Pointer.ToPointer());
//            Moment.Write(blockIndex, (double*)moment.Pointer.ToPointer());
//        }
//    }
//}
