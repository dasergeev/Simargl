namespace RailTest.Border
{
    /// <summary>
    /// Представляет ось.
    /// </summary>
    public class Axis
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="groups">
        /// Коллекция групп сечений.
        /// </param>
        /// <param name="number">
        /// Номер оси.
        /// </param>
        internal Axis(SectionGroupCollection groups, int number)
        {
            Groups = groups;
            Number = number;
            Worked = false;
            SectionsInfo = new AxisSectionInfo[groups.Count];
            CountSections = 0;
        }

        /// <summary>
        /// Возвращает коллекцию групп сечений.
        /// </summary>
        internal SectionGroupCollection Groups { get; }

        /// <summary>
        /// Возвращает номер оси.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Возвращает количество сечений, о которых есть информация.
        /// </summary>
        public int CountSections { get; private set; }

        /// <summary>
        /// Возвращает информацию о прохождении сечений.
        /// </summary>
        public AxisSectionInfo[] SectionsInfo { get; }

        /// <summary>
        /// Возвращает значение, определяюще отработана ли ось.
        /// </summary>
        public bool Worked { get; private set; }

        /// <summary>
        /// Выполняет работу с осью.
        /// </summary>
        internal void Work()
        {
            for (int i = 0; i != 21; ++i)
            {
                AxisSectionInfo info = SectionsInfo[i];
                if (info is object)
                {
                    AxisSectionInfo prev = null;
                    AxisSectionInfo next = null;

                    if (i > 0)
                    {
                        prev = SectionsInfo[i - 1];
                    }
                    if (i < 20)
                    {
                        next = SectionsInfo[i + 1];
                    }

                    if (prev is object)
                    {
                        if (next is object)
                        {
                            double speed1 = (info.Group.Position - prev.Group.Position) / (info.Time - prev.Time);
                            double speed2 = (next.Group.Position - info.Group.Position) / (next.Time - info.Time);
                            info.Speed = 0.5 * (speed1 + speed2);
                        }
                        else
                        {
                            info.Speed = (info.Group.Position - prev.Group.Position) / (info.Time - prev.Time);
                        }
                    }
                    else
                    {
                        if (next is object)
                        {
                            info.Speed = (next.Group.Position - info.Group.Position) / (next.Time - info.Time);
                        }
                    }
                    lock (Groups.SyncRoot)
                    {
                        info.Work();
                    }
                }
            }

            if (CountSections == Groups.Count)
            {
                Worked = false;
            }
        }

        /// <summary>
        /// Выполняет регистрацию оси на сечении.
        /// </summary>
        /// <param name="sectionIndex">
        /// Индекс сечения.
        /// </param>
        /// <param name="frameNumber">
        /// Номер кадра.
        /// </param>
        /// <param name="beginBlockIndex">
        /// Индекс начального блока.
        /// </param>
        /// <param name="endBlockIndex">
        /// Индекс конечного блока.
        /// </param>
        internal void Registration(int sectionIndex, int frameNumber, int beginBlockIndex, int endBlockIndex)
        {
            SectionsInfo[sectionIndex] = new AxisSectionInfo(frameNumber, beginBlockIndex, endBlockIndex, Groups[sectionIndex]);
            ++CountSections;
        }
    }
}
