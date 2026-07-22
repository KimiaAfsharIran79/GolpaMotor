using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly PersianCalendar pc = new PersianCalendar();

        private static readonly string[] WeekDays = new[]
        {
            "یکشنبه",
            "دوشنبه",
            "سه‌شنبه",
            "چهارشنبه",
            "پنجشنبه",
            "جمعه",
            "شنبه"
        };

        private static readonly string[] Months = new[]
        {
            "", // index 0 خالی
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",
            "دی",
            "بهمن",
            "اسفند"
        };

        private static string ToPersianDigits(string input)
        {
            return input
                .Replace("0", "۰")
                .Replace("1", "۱")
                .Replace("2", "۲")
                .Replace("3", "۳")
                .Replace("4", "۴")
                .Replace("5", "۵")
                .Replace("6", "۶")
                .Replace("7", "۷")
                .Replace("8", "۸")
                .Replace("9", "۹");
        }

        // 📅 فقط تاریخ (مثل: دوشنبه 25 خرداد 1405)
        public static string ToPersianDate(this DateTime date)
        {
            int year = pc.GetYear(date);
            int month = pc.GetMonth(date);
            int day = pc.GetDayOfMonth(date);

            string dayName = WeekDays[(int)date.DayOfWeek];

            string result = $"{dayName} {day} {Months[month]} {year}";

            return ToPersianDigits(result);
        }

        // ⏰ تاریخ + زمان (مثل: دوشنبه 25 خرداد 1405 - 12:45:22)
        public static string ToPersianDateTime(this DateTime date)
        {
            int year = pc.GetYear(date);
            int month = pc.GetMonth(date);
            int day = pc.GetDayOfMonth(date);

            string dayName = WeekDays[(int)date.DayOfWeek];

            string time = date.ToString("HH:mm:ss");

            string result = $"{dayName} {day} {Months[month]} {year} - {time}";

            return ToPersianDigits(result);
        }
    }
}
