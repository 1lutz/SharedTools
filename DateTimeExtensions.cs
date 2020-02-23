using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedTools
{
    public static class DateTimeExtensions
    {
        private static SortedDictionary<double, Func<double, string>> zeitNamen = new SortedDictionary<double, Func<double, string>>()
        {
            { 0.75, _ => "weniger als einer Minute" },
            { 1.5, _ => "ungefähr einer Minute" },
            { 45, min => $"{min:0} Minuten" },
            { 90, _ => "ungefähr einer Stunde" },
            { 60 * 24, min => $"ungefähr {min/60:0} Stunden" },
            { 60 * 48, _ => "einem Tag" },
            { 60 * 24 * 30, min => $"{Math.Floor(min/1440)} Tagen" },
            { 60 * 24 * 60, _ => "ungefähr einem Monat" },
            { 60 * 24 * 365, min => $"{Math.Floor(min/43200)} Monaten" },
            { 60 * 24 * 365 * 2, _ => "ungefähr einem Jahr" },
            { double.MaxValue, min => $"{Math.Floor(min/525600)} Jahren" }
        };

        public static string ToRelativeDate(this DateTime zeit)
        {
            double vergangeneMinuten = (DateTime.Now - zeit).TotalMinutes;
            string suffix = vergangeneMinuten < 0 ? "in " : "vor ";
            vergangeneMinuten = Math.Abs(vergangeneMinuten);
            Func<double, string> formatierer = zeitNamen.First(o => vergangeneMinuten < o.Key).Value;
            return suffix + formatierer(vergangeneMinuten);
        }
    }
}
