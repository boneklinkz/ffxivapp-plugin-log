// FFXIVAPP.Plugin.Log
// Translate.cs
// 
// Copyright © 2013 ZAM Network LLC

using System;
using FFXIVAPP.Common.Models;
using FFXIVAPP.Common.Utilities;
using FFXIVAPP.Plugin.Log.Properties;
using FFXIVAPP.Plugin.Log.Views;

namespace FFXIVAPP.Plugin.Log.Utilities
{
    internal static class Translate
    {
        /// <summary>
        /// </summary>
        /// <param name="line"></param>
        /// <param name="isJP"></param>
        /// <param name="resultOnly"></param>
        public static GoogleTranslateResult GetAutomaticResult(string line, bool isJP, bool resultOnly = false)
        {
            var timeStampColor = Settings.Default.TimeStampColor.ToString();
            var player = line.Substring(0, line.IndexOf(":", StringComparison.Ordinal)) + ": ";
            var tmpMessage = line.Substring(line.IndexOf(":", StringComparison.Ordinal) + 1);
            var result = ResolveGoogleTranslateResult(tmpMessage, isJP);
            if (result != null)
            {
                if (result.Translated.Length <= 0 || String.Equals(line, result.Translated, StringComparison.InvariantCultureIgnoreCase))
                {
                    return new GoogleTranslateResult
                    {
                        Original = line
                    };
                }
            }
            if (!resultOnly && result != null)
            {
                Common.Constants.FD.AppendFlow(player, "", result.Translated, new[]
                {
                    timeStampColor, "#EAFF00"
                }, MainView.View.TranslatedFD._FDR);
            }
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="line"></param>
        /// <param name="outLang"></param>
        /// <param name="isJP"></param>
        /// <returns></returns>
        public static GoogleTranslateResult GetManualResult(string line, string outLang, bool isJP)
        {
            return GoogleTranslate.Translate(line, "en", outLang, isJP);
        }

        /// <summary>
        /// </summary>
        /// <param name="line"></param>
        /// <param name="isJP"></param>
        /// <returns></returns>
        private static GoogleTranslateResult ResolveGoogleTranslateResult(string line, bool isJP)
        {
            GoogleTranslateResult result = null;
            var outLang = GoogleTranslate.Offsets[Settings.Default.TranslateTo].ToString();
            if (Settings.Default.TranslateJPOnly)
            {
                if (isJP)
                {
                    result = GoogleTranslate.Translate(line, "ja", outLang, true);
                }
            }
            else
            {
                result = GoogleTranslate.Translate(line, isJP ? "ja" : "en", outLang, true);
            }
            return result;
        }
    }
}
