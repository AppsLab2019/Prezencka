using SQLite;
using System;

namespace Prezencka.Models
{
    public sealed class WorkDay
    {
        [AutoIncrement]
        [PrimaryKey, NotNull, Unique]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan ArriveTime { get; set; }
        public TimeSpan RestStartTime { get; set; }
        public TimeSpan RestStopTime { get; set; }
        public TimeSpan LeaveTime { get; set; }
        public string Comment { get; set; }
    }
}
