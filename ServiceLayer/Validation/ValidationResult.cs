﻿using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validation
{
    public class ValidationResult
    {
        public bool IsValid => !ErrorList.Any() && !ErrorMap.Any();
        public List<string> ErrorList { get; } = new List<string>();
        public Dictionary<string, string> ErrorMap { get; } = new Dictionary<string, string>();
        public void Add(string error, string? errorType = "Exception")
        {
            ErrorList.Add(error);
            ErrorMap.Add(errorType, error);
        }

        #region song
        public void Validate(SongCreateDto input)
        {
            if (String.IsNullOrWhiteSpace(input.Genre))
            {
                Add("Missing genre", nameof(input.Genre));
            }
            if (String.IsNullOrWhiteSpace(input.Title))
            {
                Add("Missing title", nameof(input.Title));
            }
            if (String.IsNullOrWhiteSpace(input.Composer))
            {
                Add("Missing composer", nameof(input.Composer));
            }
        }
        #endregion
        public void Validate(SheetSymbolCreateDto input)
        {
            if (input.TopSignature <= 0) 
            {
                
            }
            if (String.IsNullOrWhiteSpace(input.Symbols))
            {
                Add("Missing symbols", nameof (input.Symbols));
            } else {

            }
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
