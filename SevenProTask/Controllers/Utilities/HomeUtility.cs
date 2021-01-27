using System;
using System.Collections.Generic;
using System.Linq;
using SevenProTask.Models;

namespace SevenProTask.Controllers.Utilities
{
    public static class HomeUtility
    {
        public static IEnumerable<string> GetTimeIntervals()
        {
            var array = new List<string>();
            var currentDay = new DateTime(1, 1, 1, 9, 0, 0).ToShortTimeString();
            for (var i = 0; i < 10; i++)
            {
                array.Add( DateTime.Parse(currentDay).AddMinutes(60 * i).ToShortTimeString());
            }
            return array;
        }

        public static IEnumerable<string> GetDays()
        {
            return new List<string> {"Понедельник", "Вторник", "Среда", "Четверг", "Пятница"};
        }

        public static bool IsCourses(ApplicationContext context)
        {
            var count = context.Courses.Count();
            return count != 0;
        }
    }
}