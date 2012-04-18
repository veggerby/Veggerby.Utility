using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace Veggerby.Utility.Extensions
{
    public static class StringExtensions
    {
        public static string GetHash(this System.Security.Cryptography.HashAlgorithm algorithm, string value)
        {
            byte[] data = Encoding.ASCII.GetBytes(value);
            return algorithm.GetHash(data);
        }

        public static string GetHash(this System.Security.Cryptography.HashAlgorithm algorithm, byte[] data)
        {
            data = algorithm.ComputeHash(data);
            var hash = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                hash.Append(data[i].ToString("x2").ToLower());
            }

            return hash.ToString();
        }

        public static string MD5(this string value)
        {
            return System.Security.Cryptography.MD5.Create().GetHash(value);         
        }

        public static string MD5(this byte[] value)
        {
            return System.Security.Cryptography.MD5.Create().GetHash(value);
        }

        public static string RIPEMD160(this string value)
        {
            return System.Security.Cryptography.RIPEMD160.Create().GetHash(value);
        }

        public static string RIPEMD160(this byte[] value)
        {
            return System.Security.Cryptography.RIPEMD160.Create().GetHash(value);
        }

        public static string SHA1(this string value)
        {
            return System.Security.Cryptography.SHA1.Create().GetHash(value);
        }

        public static string SHA1(this byte[] value)
        {
            return System.Security.Cryptography.SHA1.Create().GetHash(value);
        }

        public static string SHA256(this string value)
        {
            return System.Security.Cryptography.SHA256.Create().GetHash(value);
        }

        public static string SHA256(this byte[] value)
        {
            return System.Security.Cryptography.SHA256.Create().GetHash(value);
        }

        public static string SHA384(this string value)
        {
            return System.Security.Cryptography.SHA384.Create().GetHash(value);
        }

        public static string SHA384(this byte[] value)
        {
            return System.Security.Cryptography.SHA384.Create().GetHash(value);
        }

        public static string SHA512(this string value)
        {
            return System.Security.Cryptography.SHA512.Create().GetHash(value);
        }

        public static string SHA512(this byte[] value)
        {
            return System.Security.Cryptography.SHA512.Create().GetHash(value);
        }

        public static string Initials(this string name)
        {
            return name.Initials(string.Empty);
        }

        public static string Initials(this string name, string separator)
        {
            return Regex.Replace(name + " ", @"(\w)\w+ ", "$1" + separator).Trim();
        }

        public static string TrimNull(this string val)
        {
            return val != null ? val.Trim() : null;
        }

        public static string ToOrdinalForm(this int num)
        {
            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num.ToString() + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num.ToString() + "st";
                case 2:
                    return num.ToString() + "nd";
                case 3:
                    return num.ToString() + "rd";
                default:
                    return num.ToString() + "th";
            }
        }

        // Source: 
        public static string FormatWith(this string s, params object[] args)
        {
            return string.Format(s, args);
        }

        /// <summary>Serializes an object of type T in to an xml string</summary> 
        /// <typeparam name="T">Any class type</typeparam> 
        /// <param name="obj">Object to serialize</param> 
        /// <returns>A string that represents Xml, empty otherwise</returns> 
        public static string XmlSerialize<T>(this T obj) where T : class, new()
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        /// <summary>Deserializes an xml string in to an object of Type T</summary> 
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="xml">Xml as string to deserialize from</param>
        /// <returns>A new object of type T is successful, null if failed</returns>       
        public static T XmlDeserialize<T>(this string xml) where T : class, new()
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                try
                {
                    return (T)serializer.Deserialize(reader);
                }
                catch
                {
                    return null;
                }
                // Could not be deserialized to this type.
            }
        }

        /// <summary>
        /// Parses a string into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">String value to parse</param>
        /// <returns>The Enum corresponding to the stringExtensions</returns>
        public static T ToEnum<T>(this string value)
        {
            return ToEnum<T>(value, false);
        }

        /// <summary>
        /// Parses a string into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">String value to parse</param>
        /// <param name="ignorecase">Ignore the case of the string being parsed</param>
        /// <returns>The Enum corresponding to the stringExtensions</returns>
        public static T ToEnum<T>(this string value, bool ignorecase)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Value");
            }

            value = value.Trim();

            if (value.Length == 0)
            {
                throw new ArgumentNullException("value", "Must specify valid information for parsing in the string.");
            }

            var t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException("T", "Type provided must be an Enum.");
            }

            return (T)Enum.Parse(t, value, ignorecase);
        }

        /// <summary>
        /// Encryptes a string using the supplied key. Encoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToEncrypt">String that must be encrypted.</param>
        /// <param name="key">Encryptionkey.</param>
        /// <returns>A string representing a byte array separated by a minus sign.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
        public static string Encrypt(this string stringToEncrypt, string key)
        {
            if (string.IsNullOrEmpty(stringToEncrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
            }

            var cspp = new System.Security.Cryptography.CspParameters {KeyContainerName = key};

            var rsa = new System.Security.Cryptography.RSACryptoServiceProvider(cspp) {PersistKeyInCsp = true};

            byte[] bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(stringToEncrypt), true);

            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// Decryptes a string using the supplied key. Decoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToDecrypt"></param>
        /// <param name="key">Decryptionkey.</param>
        /// <returns>The decrypted string or null if decryption failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
        public static string Decrypt(this string stringToDecrypt, string key)
        {
            string result;

            if (string.IsNullOrEmpty(stringToDecrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
            }

            var cspp = new System.Security.Cryptography.CspParameters {KeyContainerName = key};

            var rsa = new System.Security.Cryptography.RSACryptoServiceProvider(cspp) {PersistKeyInCsp = true};

            string[] decryptArray = stringToDecrypt.Split(new[] { "-" }, StringSplitOptions.None);
            byte[] decryptByteArray = Array.ConvertAll(decryptArray, (s => Convert.ToByte(byte.Parse(s, NumberStyles.HexNumber))));


            byte[] bytes = rsa.Decrypt(decryptByteArray, true);

            result = Encoding.UTF8.GetString(bytes);

            return result;
        }

        /// <summary>
        /// Truncates the string to a specified length and replace the truncated to a ...
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength)
        {
            // replaces the truncated string to a ...
            const string suffix = "...";

            if (maxLength <= 0)
            {
                return text;
            }

            int strLength = maxLength - suffix.Length;

            if (strLength <= 0)
            {
                return text;
            }

            if (text == null || text.Length <= maxLength)
            {
                return text;
            }

            return text
                .Substring(0, strLength)
                .TrimEnd() + suffix;
        }

        public static string ExpandPascalCase(this string text)
        {
            // source: http://stackoverflow.com/questions/323314/best-way-to-convert-pascal-case-to-a-sentence
            if (text == null)
            {
                return null;
            }

            return Regex.Replace(text, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }


        public static string ToTitleCase(this string text)
        {
            if (text == null)
            {
                return null;
            }

            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            // TextInfo.ToTitleCase only operates on the string if is all lower case, otherwise it returns the string unchanged.     
            return textInfo.ToTitleCase(text.ToLower());
        }

        public static string ToSlug(this string message)
        {
            // replace space with -
            message = Regex.Replace(message, @"[\s/\\\.,+|_]+", "-");
            // normalize the message 
            message = message.Normalize(NormalizationForm.FormD);
            message = message.Replace("ø", "oe").Replace("Ø", "Oe").Replace("æ", "ae").Replace("Æ", "Ae").Replace("å", "aa").Replace("Å", "Aa");
            var result = new StringBuilder();
            foreach (char t in message)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(t);
                }
            }
            return Regex.Replace(result.ToString().Normalize(NormalizationForm.FormC), @"[^a-zA-Z0-9\-]", "").ToLower();
        }

        private static readonly Regex _Tags = new Regex("<[^>]*(>|$)", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private static readonly Regex _Whitelist = new Regex(@"^</?(b(lockquote)?|code|d(d|t|l|el)|em|h(1|2|3)|i|kbd|li|ol|p(re)?|s(ub|up|trong|trike)?|ul)>$|^<(b|h)r\s?/?>$", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
        private static readonly Regex _WhitelistA = new Regex(@"^<a\shref=""(\#\d+|(https?|ftp)://[-a-z0-9+&@#/%?=~_|!:,.;\(\)]+)""(\stitle=""[^""<>]+"")?\s?>$|^</a>$", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
        private static readonly Regex _WhitelistImg = new Regex(@"^<img\ssrc=""https?://[-a-z0-9+&@#/%?=~_|!:,.;\(\)]+""(\swidth=""\d{1,3}"")?(\sheight=""\d{1,3}"")?(\salt=""[^""<>]*"")?(\stitle=""[^""<>]*"")?\s?/?>$", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

        public static string Sanitize(this string html)
        {
            return html.Sanitize(false);
        }

        /// <summary>
        /// sanitize any potentially dangerous tags from the provided raw HTML input using 
        /// a whitelist based approach, leaving the "safe" HTML tags
        /// http://refactormycode.com/codes/333-sanitize-html
        /// CODESNIPPET:4100A61A-1711-4366-B0B0-144D1179A937
        /// </summary>
        public static string Sanitize(this string html, bool all)
        {
            if (String.IsNullOrEmpty(html)) return html;

            // match every HTML tag in the input
            MatchCollection tags = _Tags.Matches(html);
            for (int i = tags.Count - 1; i > -1; i--)
            {
                Match tag = tags[i];
                string tagname = tag.Value.ToLowerInvariant();

                if (all || !(_Whitelist.IsMatch(tagname) || _WhitelistA.IsMatch(tagname) || _WhitelistImg.IsMatch(tagname)))
                {
                    html = html.Remove(tag.Index, tag.Length);
                }
            }

            return html;
        }

        public static string AsTextList(this IEnumerable<string> list)
        {
            if (list == null)
            {
                return null;
            }

            if (list.Count() >= 2)
            {
                return string.Format("{0} and {1}",
                                     string.Join(", ", list.Take(list.Count() - 1)),
                                     list.Last());
            }

            return list.SingleOrDefault();
        }

        public static string ContentTypeToExtention(this string contentType)
        {
            switch (contentType)
            {
                case "image/jpeg":
                    return ".jpg";
                case "image/tiff":
                    return ".tiff";
                case "image/bmp":
                    return ".bmp";
                case "image/gif":
                    return ".gif";
                case "image/png":
                    return ".png";
                default: return (null);
            }
        }

    }
}
