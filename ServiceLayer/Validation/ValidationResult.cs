﻿using ServiceLayer.DTOs;
using ServiceLayer.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get { return !ErrorList.Any() && !ErrorMap.Any(); } }
        public List<string> ErrorList { get; } = new List<string>();
        public Dictionary<string, string> ErrorMap { get; } = new Dictionary<string, string>();
        public void AddError(string error, string? errorType = "Exception")
        {
            ErrorList.Add(error);
            if (ErrorMap.ContainsKey(errorType)) 
            { 
                string old = ErrorMap[errorType];   
                ErrorMap[errorType] = ErrorMap[errorType] + ". " +  error;
            }
            else
            {
                ErrorMap.Add(errorType, error);
            }
        }

        #region song
        public void Validate(SongCreateDto input)
        {
            if (String.IsNullOrWhiteSpace(input.Genre))
            {
                AddError("Missing genre", nameof(input.Genre));
            }
            if (String.IsNullOrWhiteSpace(input.Title))
            {
                AddError("Missing title", nameof(input.Title));
            }
            if (String.IsNullOrWhiteSpace(input.Composer))
            {
                AddError("Missing composer", nameof(input.Composer));
            }
        }
        #endregion
        public async Task ValidateAsync(SheetSymbolCreateDto input, IServiceWrapper services)
        {
            if(!await services.Songs.IsExistAsync(input.SongId))
            {
                AddError("Invalid top signature", nameof(input.TopSignature));
            }
            //if (!await services.Instruments.IsExistAsync(input.InstrumentId))
            //{
            //    AddError("Invalid top signature", nameof(input.TopSignature));
            //}
            if (input.TopSignature <= 0)
            {
                AddError("Invalid top signature", nameof(input.TopSignature));

            }
            if (input.BottomSignature < input.TopSignature)
            {
                AddError("Invalid bottom signature", nameof(input.BottomSignature));

            }
            if (String.IsNullOrWhiteSpace(input.Symbols))
            {
                AddError("Missing symbols", nameof(input.Symbols));
            }
            else
            {
                string[] measureStrings = input.Symbols.Split(new char[] { '/' });
                for (int i = 0; i < measureStrings.Length; i++)
                {
                    string[] chordStrings = measureStrings[i].Split(new char[] { ' ' });
                    double totalDuration = chordStrings.Select(chordString => double.Parse(chordString.Split('_')[1])).Sum();
                    bool isGoodBeatNum = ValiddateMeasureBeats(totalDuration, input.TopSignature, input.BottomSignature);
                    if (!isGoodBeatNum)
                    {
                        AddError($"Measure {i + 1} has invalid number of beats", nameof(input.Symbols));
                    }
                }


            }
        }

        public void Validate(SheetCreateDto input)
        {
            if (input.TopSignature <= 0)
            {

            }
            //if (String.IsNullOrWhiteSpace(input.Symbols))
            //{
            //    AddError("Missing symbols", nameof(input.Symbols));
            //}
            //else
            //{
            //    string[] measureStrings = input.Symbols.Split(new char[] { '/' });
            //    for (int i = 0; i < measureStrings.Length; i++)
            //    {
            //        string[] chordStrings = measureStrings[i].Split(new char[] { ' ' });
            //        double totalDuration = chordStrings.Select(chordString => double.Parse(chordString.Split('_')[1])).Sum();
            //        bool isGoodBeatNum = ValiddateMeasureBeats(totalDuration, input.TopSignature, input.BottomSignature);
            //        if (!isGoodBeatNum)
            //        {
            //            AddError($"Measure {i + 1} has invalid number of beats", nameof(input.Symbols));
            //        }
            //    }


            //}
        }

        private bool ValiddateMeasureBeats(double totalDuration, int topSignature, int bottomSignature)
        {
            double expectedTotalValue = topSignature * (4.0 / bottomSignature); // For 5/8, it's simply 5 eighth notes
            return Math.Abs(totalDuration - expectedTotalValue) < 0.0001; // Allow small floating-point error

        }

        #region sheet
        #endregion

    }
    public static class ValidateErrType
    {
        public static string Exception = "Exception";
        public static string Role = "Role";
        public static string Unauthorized = "Unauthorized";
        public static string IdNotMatch = "IdNotMatch";

        public static string NotFound = "NotFound";
    }
}
