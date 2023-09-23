namespace Restaurant.Helper.Common
{
    public static class Common
    {
        public static string GetDisplayDate(DateTime date)
        {
            return $"{date.Hour}:{date.Minute} {date.Day}/{date.Month}/{date.Year}";
        }
    }
}
