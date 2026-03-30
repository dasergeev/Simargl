//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Предоставляет масштабы.
//    /// </summary>
//    public static class Factors
//    {
//        /// <summary>
//        /// Время формирования масштабов.
//        /// </summary>
//        public static DateTime BuildTime { get; } = DateTime.Parse("23.08.2020");

//        /// <summary>
//        /// Возвращает масштаб.
//        /// </summary>
//        /// <param name="rail">
//        /// Рельс.
//        /// </param>
//        /// <param name="section">
//        /// Сечение.
//        /// </param>
//        /// <returns>
//        /// Масштаб.
//        /// </returns>
//        public static double GetFactor(Rail rail, int section)
//        {
//            if (rail == Rail.Left)
//            {
//                switch (section)
//                {
//                    case 1: return 0.00642465969553979;
//                    case 2: return 0.00609802968758563;
//                    case 3: return 0.00619245334603426;
//                    case 4: return 0.00604669500120611;
//                    case 5: return 0.00611076110484927;
//                    case 6: return 0.00599931981706688;
//                    case 7: return 0.00612773561194869;
//                    case 8: return 0.00600215995003421;
//                    case 9: return 0.00609104829811871;
//                    case 10: return 0.00600850192286896;
//                    case 11: return 0.00616105847551974;
//                    case 12: return 0.00476125540079505;
//                    case 13: return 0.00484180351811893;
//                    case 14: return 0.00483241945614021;
//                    case 15: return 0.004845200362105;
//                    case 16: return 0.00479832356631879;
//                    case 17: return 0.00471829914674821;
//                    case 18: return 0.00481697856610609;
//                    case 19: return 0.00476188845051569;
//                    case 20: return 0.00483497272274535;
//                    case 21: return 0.00444821048654958;
//                    default: throw new ArgumentOutOfRangeException(nameof(section));
//                }
//            }
//            else
//            {
//                switch (section)
//                {
//                    case 1: return 0.00574570584445737;
//                    case 2: return 0.00583443558784974;
//                    case 3: return 0.00604709775134512;
//                    case 4: return 0.00598274005225692;
//                    case 5: return 0.00594029675598343;
//                    case 6: return 0.00593745878821194;
//                    case 7: return 0.00474233509331535;
//                    case 8: return 0.00595609229448125;
//                    case 9: return 0.00591988756869019;
//                    case 10: return 0.0060428317935835;
//                    case 11: return 0.00596785357623837;
//                    case 12: return 0.00474721099806734;
//                    case 13: return 0.00473857534300707;
//                    case 14: return 0.00470738955880743;
//                    case 15: return 0.00470339179090941;
//                    case 16: return 0.00471633406046639;
//                    case 17: return 0.00476980387848992;
//                    case 18: return 0.00469455019516227;
//                    case 19: return 0.00474016696299772;
//                    case 20: return 0.00472940873884686;
//                    case 21: return 0.0044959278569161;
//                    default: throw new ArgumentOutOfRangeException(nameof(section));
//                }
//            }
//        }
//    }
//}
