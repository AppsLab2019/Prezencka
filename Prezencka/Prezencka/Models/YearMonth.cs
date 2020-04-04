namespace Prezencka.Models
{
    public struct YearMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public YearMonth(int year = default, int month = default)
        {
            Year = year;
            Month = month;
        }
    }
}
