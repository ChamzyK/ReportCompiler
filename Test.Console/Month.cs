namespace Test.Console
{
    internal enum Month
    {
        January = 0,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    internal static class MonthMethods
    {
        public static string GetName(this Month month)
        {
            if (month == Month.January)
            {
                return "Январь";
            }
            else if (month == Month.February)
            {
                return "Февраль";
            }
            else if (month == Month.March)
            {
                return "Март";
            }
            else if (month == Month.April)
            {
                return "Апрель";
            }
            else if (month == Month.May)
            {
                return "Май";
            }
            else if (month == Month.June)
            {
                return "Июнь";
            }
            else if (month == Month.July)
            {
                return "Июль";
            }
            else if (month == Month.August)
            {
                return "Август";
            }
            else if (month == Month.September)
            {
                return "Сентябрь";
            }
            else if (month == Month.October)
            {
                return "Октябрь";
            }
            else if (month == Month.November)
            {
                return "Ноябрь";
            }
            else if (month == Month.December)
            {
                return "Декабрь";
            }

            throw new Exception("Некорректный месяц в перечислении.");
        }
    }
}
