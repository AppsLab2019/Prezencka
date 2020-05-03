using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Prezencka.Models;
using SQLite;

namespace Prezencka.Services
{
    public sealed class WorkDayStore
    {
        private readonly SQLiteAsyncConnection _connection;
        private IDictionary<YearMonth, List<WorkDay>> _workDays;

        public WorkDayStore()
        {
            var startPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = System.IO.Path.Combine(startPath, "Prezencka.db3");

            _connection = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitAsync()
        {
            await _connection.CreateTableAsync<WorkDay>();

            var unsortedWorkDays = await _connection.Table<WorkDay>().ToArrayAsync();
            var dateInfo = unsortedWorkDays.Select(CreateYearMonth).Distinct();

            var dict = new Dictionary<YearMonth, List<WorkDay>>();

            foreach (var info in dateInfo)
            {
                var selectedDays = unsortedWorkDays.Where(day => day.Date.Year == info.Year && day.Date.Month == info.Month);
                dict[info] = selectedDays.ToList();
            }

            _workDays = dict;
        }

        public WorkDay GetWorkDay(DateTime date) =>
            GetWorkDay(date.Year, date.Month, date.Day);

        public WorkDay GetWorkDay(int year, int month, int day)
        {
            var yearMonth = new YearMonth(year, month);

            if (!_workDays.ContainsKey(yearMonth))
                return null;

            var list = _workDays[yearMonth];

            return list.FirstOrDefault(workDay => workDay.Date.Day == day);
        }

        public IReadOnlyList<WorkDay> GetWorkDays(int year, int month) =>
            GetWorkDays(new YearMonth(year, month));

        public IReadOnlyList<WorkDay> GetWorkDays(YearMonth yearMonth)
        {
            if (!_workDays.ContainsKey(yearMonth))
                return null;

            return _workDays[yearMonth];
        }

        public Task AddAsync(WorkDay day)
        {
            if (day is null)
                throw new ArgumentNullException(nameof(day));

            var yearMonth = CreateYearMonth(day);
            var list = CreateWorkDayListIfMissing(yearMonth);

            var conflictCount = list.Count(workDay => workDay.Date.Day == day.Date.Day);
            if (conflictCount > 0)
                throw new Exception();

            list.Add(day);
            return _connection.InsertAsync(day);
        }

        public Task UpdateAsync(WorkDay day)
        {
            if (day is null)
                throw new ArgumentNullException(nameof(day));

            return _connection.UpdateAsync(day);
        }

        public Task AddOrUpdateAsync(WorkDay day)
        {
            if (day is null)
                throw new ArgumentNullException(nameof(day));

            if (day.Id != default)
                return UpdateAsync(day);
            else
                return AddAsync(day);
        }

        public Task RemoveAsync(WorkDay day)
        {
            if (day is null)
                throw new ArgumentNullException(nameof(day));

            var yearMonth = CreateYearMonth(day);

            if (!_workDays.ContainsKey(yearMonth))
                return Task.CompletedTask;

            var list = _workDays[yearMonth];

            if (!list.Contains(day))
                return Task.CompletedTask;

            list.Remove(day);

            if (!list.Any())
                _workDays.Remove(yearMonth);

            return _connection.DeleteAsync(day);
        }

        private YearMonth CreateYearMonth(WorkDay day) =>
            new YearMonth(day.Date.Year, day.Date.Month);

        private List<WorkDay> CreateWorkDayListIfMissing(YearMonth yearMonth)
        {
            if (_workDays.ContainsKey(yearMonth))
                return _workDays[yearMonth];

            return _workDays[yearMonth] = new List<WorkDay>();
        }
    }
}
