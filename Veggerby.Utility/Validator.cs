using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Veggerby.Utility
{
    /// <summary>
    /// Summary description for Validate.
    /// </summary>
    public static class Validator
    {

        /// <summary>
        /// Regular expression to validate a string as a GUID
        /// </summary>
        public const string RegexGuid = @"^\{?[a-fA-F0-9]{8}\-[a-fA-F0-9]{4}\-[a-fA-F0-9]{4}\-[a-fA-F0-9]{4}\-[a-fA-F0-9]{12}\}?$";

        /// <summary>
        /// Regular expression to validate a string as a valid IP address
        /// </summary>
        public const string RegexIp = @"^([0-1]?[0-9]{1,2}|2[0-4][1-9]|25[0-5])\.([0-1]?[0-9]{1,2}|2[0-4][1-9]|25[0-5])\.([0-1]?[0-9]{1,2}|2[0-4][1-9]|25[0-5])\.([0-1]?[0-9]{1,2}|2[0-4][1-9]|25[0-5])?$";

        /// <summary>
        /// Regular expression to validate a string as a valid Url (http or https)
        /// </summary>
        public const string RegexUrl = @"^(?:http|https)://[a-zA-Z0-9\.\-]+(?:\:\d{1,5})?(?:[A-Za-z0-9\.\;\:\@\&\=\+\$\,\?/]|%u[0-9A-Fa-f]{4}|%[0-9A-Fa-f]{2})*$";

        /// <summary>
        /// Regular expression to validate a string as a valid email address (pragmatic)
        /// </summary>
        public const string RegexEmail = @"^([a-zA-Z0-9_\-\.\+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// Validates if a value is an integer
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>True if the value validates, otherwise false</returns>
        public static bool IsInt32(object value)
        {
            return AsInt32(value) != null;
        }

        /// <summary>
        /// Rounds a value and validates if it is an integer
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>True if the value validates, otherwise false</returns>
        public static bool IsRoundedInt32(object value)
        {
            return AsRoundedInt32(value) != null;
        }

        /// <summary>
        /// Validates a value as an integer in a specified range
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="min">The minimum value (inclusive)</param>
        /// <param name="max">The maximum value (inclusive)</param>
        /// <returns>True if the value validates, otherwise false</returns>
        public static bool IsInt32(object value, int min, int max)
        {
            return AsInt32(value, min, max) != null;
        }

        /// <summary>
        /// Validates a number as a positive integer (> 0)
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>True if the value validates, otherwise false</returns>
        public static bool IsPositiveInt32(object value)
        {
            return AsPositiveInt32(value) != null;
        }

        /// <summary>
        /// Validates a number as a negative integer (&lt; 0)
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>True if the value validates, otherwise false</returns>
        public static bool IsNegativeInt32(object value)
        {
            return AsNegativeInt32(value) != null;
        }

        /// <summary>
        /// Validates a value as a double (floating point in general)
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>True if the value validates, otherwise false</returns>
        public static bool IsDouble(object value)
        {
            return AsDouble(value) != null;
        }

        /// <summary>
        /// Validates a value as a double within a specified range
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="min">The minimum value (inclusive)</param>
        /// <param name="max">The maximum value (inclusive)</param>
        /// <returns>True if the value validates, otherwise false</returns>
        public static bool IsDouble(object value, double min, double max)
        {
            return AsDouble(value, min, max) != null;
        }

        /// <summary>
        /// Validates a value as a double and rounds it to a predefined number of decimals
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static bool IsDouble(object value, int decimals)
        {
            return AsDouble(value, decimals) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static bool IsDateTime(object value)
        {
            return AsDateTime(value) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool IsDateTime(object value, string format)
        {
            return AsDateTime(value, format) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static bool IsDateTime(object value, CultureInfo culture)
        {
            return AsDateTime(value, culture) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static bool IsTimeSpan(object value)
        {
            return AsTimeSpan(value) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static bool IsBool(object value)
        {
            return AsBool(value) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        /// <returns></returns>
        public static bool IsBool(object value, string trueValue, string falseValue)
        {
            return AsBool(value, trueValue, falseValue) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static bool IsAlpha(object value)
        {
            return AsAlpha(value) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(object value)
        {
            return AsAlphaNumeric(value) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static bool IsGuid(object value)
        {
            return AsGuid(value) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static bool IsUrl(object value)
        {
            return AsUrl(value) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static bool IsExistingUrl(object value)
        {
            return AsExistingUrl(value) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static bool IsEmail(object value)
        {
            return AsEmail(value) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool IsMatching(object value, string regex)
        {
            if (value == null)
            {
                return false;
            }

            if (value is string)
            {
                return Regex.IsMatch(value as string, regex);
            }

            return IsMatching(value.ToString(), regex);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="regex"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsMatching(object value, string regex, RegexOptions options)
        {
            if (value == null)
            {
                return false;
            }

            if (value is string)
            {
                return Regex.IsMatch(value as string, regex, options);
            }

            return IsMatching(value.ToString(), regex, options);
        }

        #region Blind-filtering

        /// <summary>
        /// Validates the value as an integer.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>The value represented as an integer if possible/valid</returns>
        public static int? AsInt32(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is int)
            {
                return (int)value;
            }

            if (value is string)
            {
                int result;
                if (int.TryParse(value as string, out result))
                {
                    return result;
                }
                return null;
            }

            return AsInt32(value.ToString());
        }

        /// <summary>
        /// Validates the value as an integer if, the number is decimal round it.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>The value represented as an integer if possible/valid</returns>
        public static int? AsRoundedInt32(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is int)
            {
                return (int)value;
            }

            if ((value is double) || (value is float) || (value is decimal))
            {
                return (int)Math.Round((double)value);
            }

            if (value is string)
            {
                double result;
                if (double.TryParse(value as string, out result))
                {
                    return (int)Math.Round(result);
                }
                return null;
            }

            return AsRoundedInt32(value.ToString());
        }

        /// <summary>
        /// Validates the value as an integer if lying within a specified range, both boundaries included.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="min">The minimum boundary the value is allowed to have</param>
        /// <param name="max">The maximum boundary the value is allowed to have</param>
        /// <returns>The value represented as an integer within the boundaries, if possible/valid</returns>
        public static int? AsInt32(object value, int min, int max)
        {
            int? result = AsInt32(value);
            if (result >= min && result <= max)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Validates the value an a positive integer.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>The value represented as a positive integer if possible/valid</returns>
        public static int? AsPositiveInt32(object value)
        {
            int? i = AsInt32(value);
            if (i > 0)
            {
                return i;
            }

            return null;
        }

        /// <summary>
        /// Validated the value as a negative integer.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>The value represented as a negative integer if possible/valid</returns>
        public static int? AsNegativeInt32(object value)
        {
            int? i = AsInt32(value);
            if (i < 0)
            {
                return i;
            }

            return null;
        }

        /// <summary>
        /// Validates the value as an double (floating point).
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>The value represented as an double if possible/valid</returns>
        public static double? AsDouble(object value)
        {
            if (value == null)
            {
                return null;
            }

            if ((value is double) || (value is float) || (value is decimal))
            {
                return (double)value;
            }

            if (value is string)
            {
                double result;
                if (double.TryParse(value as string, out result))
                {
                    return result;
                }
                return null;
            }

            return AsDouble(value.ToString());
        }

        /// <summary>
        /// Validates the value as an double if lying within a specified range, both boundaries included.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="min">The minimum boundary the value is allowed to have</param>
        /// <param name="max">The maximum boundary the value is allowed to have</param>
        /// <returns>The value represented as an double within the boundaries, if possible/valid</returns>
        public static double? AsDouble(object value, double min, double max)
        {
            double? result = AsDouble(value);
            if (result >= min && result <= max)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Validates the value as an double.
        /// If the value validates, the resulting value is rounded to a specified number of decimals.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="decimals">The number of decimals the result should be rounded t0</param>
        /// <returns>The value represented as an double rounded to the specified number of decimals</returns>
        public static double? AsDouble(object value, int decimals)
        {
            double? result = AsDouble(value);
            return (double)Math.Round(Convert.ToDecimal(result), decimals);
        }

        /// <summary>
        /// Validates the value as a date and time. If the value is specified as a string, the string must
        /// conform to a formats as specified in the input locale.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>The value represented as a DateTime if possible/valid</returns>
        public static DateTime? AsDateTime(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is DateTime)
            {
                return (DateTime)value;
            }

            if (value is string)
            {
                DateTime result;
                if (DateTime.TryParse(value as string, out result))
                {
                    return result;
                }
                return null;
            }

            return AsDateTime(value.ToString());
        }

        /// <summary>
        /// Validates the value as a date and time against a specific format.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="format">The allowed format</param>
        /// <returns>The value represented as a DateTime if possible/valid</returns>
        public static DateTime? AsDateTime(object value, string format)
        {

            var dateFormat = new DateTimeFormatInfo {FullDateTimePattern = format};

            if (value == null)
            {
                return null;
            }

            if (value is DateTime)
            {
                return (DateTime)value;
            }

            if (value is string)
            {
                DateTime result;
                if (DateTime.TryParse(value as string, dateFormat, DateTimeStyles.None, out result))
                {
                    return result;
                }
                return null;
            }

            return AsDateTime(value.ToString());
        }

        /// <summary>
        /// Validates the value as a date and time against a specific format.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="culture">The "culture" to convert from</param>
        /// <returns>The value represented as a DateTime if possible/valid</returns>
        public static DateTime? AsDateTime(object value, CultureInfo culture)
        {

            if (value == null)
            {
                return null;
            }

            if (value is DateTime)
            {
                return (DateTime)value;
            }

            if (value is string)
            {
                DateTime result;
                if (DateTime.TryParse(value as string, culture.DateTimeFormat, DateTimeStyles.None, out result))
                {
                    return result;
                }
            }

            return AsDateTime(value.ToString());
        }

        /// <summary>
        /// Validates the value as a timespan. If the value is specified as a string, the string must
        /// conform to a formats as specified in the input locale.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>The value represented as a DateTime if possible/valid</returns>
        public static TimeSpan? AsTimeSpan(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is TimeSpan)
            {
                return (TimeSpan)value;
            }

            if (value is string)
            {
                TimeSpan result;
                if (TimeSpan.TryParse(value as string, out result))
                {
                    return result;
                }
                return null;
            }

            return AsTimeSpan(value.ToString());
        }

        /// <summary>
        /// Validates the value as a boolean.
        /// Allowed values are true/false and 0/1
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>The value represented as a boolean if possible/valid</returns>
        public static bool? AsBool(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is bool)
            {
                return (bool)value;
            }

            if ((value is string) || (value is int))
            {
                if (value.ToString().Trim() == "1")
                {
                    return true;
                }

                if (value.ToString().Trim() == "0")
                {
                    return false;
                }

                bool result;
                if (bool.TryParse(value.ToString().ToLower(), out result))
                {
                    return result;
                }
                return null;
            }

            return AsBool(value.ToString());
        }

        /// <summary>
        /// Validates the value as a boolean. 
        /// Allowed values are true/false, 0/1 and the specified values for true and false as strings
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="trueValue">value which returns true</param>
        /// <param name="falseValue">value which returns false</param>
        /// <returns>The value represented as a boolean if possible/valid</returns>
        public static bool? AsBool(object value, string trueValue, string falseValue)
        {
            if (value == null)
            {
                return null;
            }

            if (value.ToString() == trueValue)
            {
                return true;
            }

            if (value.ToString() == falseValue)
            {
                return false;
            }

            return AsBool(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static string AsString(object value)
        {
            if (value == null)
            {
                return null;
            }
            return value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static string AsAlpha(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string)
            {
                if (!Regex.IsMatch(value as string, @"^[a-zA-Z ]*$"))
                {
                    return null;
                }

                return value as string;
            }

            return AsAlpha(value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static string AsAlphaNumeric(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string)
            {
                if (!Regex.IsMatch(value as string, @"^[a-zA-Z0-9 ]*$"))
                {
                    return null;
                }

                return value as string;
            }

            return AsAlphaNumeric(value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static Guid? AsGuid(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is Guid)
            {
                return (Guid)value;
            }

            if (value is string)
            {
                try
                {
                    return new Guid(value as string);
                }
                catch (Exception)
                {
                    return null;
                }

            }

            return AsGuid(value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static string AsUrl(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string)
            {
                if (!Regex.IsMatch(value as string, @"^(?:http|https|ftp)://[a-zA-Z0-9\.\-]+(?:\:\d{1,5})?(?:[A-Za-z0-9\.\;\:\@\&\=\+\$\,\?/]|%u[0-9A-Fa-f]{4}|%[0-9A-Fa-f]{2})*$"))
                {
                    return null;
                }
                return value as string;
            }

            return AsUrl(value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static string AsExistingUrl(object value)
        {
            string result = AsUrl(value);

            var url = new Uri(result);
            System.Net.WebResponse response = null;
            try
            {
                System.Net.WebRequest request = System.Net.WebRequest.Create(url);
                response = request.GetResponse();
            }
            catch
            {
                return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns></returns>
        public static string AsEmail(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string)
            {
                if (!Regex.IsMatch(value as string, @"[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$"))
                {
                    return null;
                }
                return value as string;
            }

            return AsEmail(value.ToString());
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="validItems"></param>
        /// <returns></returns>
        public static string FromList(object value, params string[] validItems)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string)
            {
                return validItems.FirstOrDefault(x => value.Equals(x));
            }

            return FromList(value.ToString(), validItems);
        }
    }

}
