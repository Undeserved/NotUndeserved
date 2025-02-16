﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Common.Extensions {
    public static class FormattingExtensions {
        public static string ToISO8601String(this DateTime date) {
            return date.ToString("yyyy-MM-dd");
        }

        public static string ToISO8601TimeString(this DateTime date) {
            return date.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public static DateTime Rfc5322ToDate(this string date) {
            string dateFormat = "dd/MM/yyyy";
            if (DateTime.TryParseExact(date, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime parsedDate)) {
                return parsedDate;
            }
            throw new FormatException($"The value didn't follow the expected {dateFormat} date format.");
        }

        public static List<string> SplitMessage(this string message) {
            return message.Chunk(500)
                .Select(x => new string(x))
                .ToList();
        }

        public static string MergeContent(this List<string> content) {
            return string.Join(' ', content.ToArray());
        }
    }
}
