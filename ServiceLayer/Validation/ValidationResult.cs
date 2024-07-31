using ServiceLayer.DTOs;
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
                ErrorMap[errorType] = ErrorMap[errorType] + ". " + error;
            }
            else
            {
                ErrorMap.Add(errorType, error);
            }
        }

        #region song
        public void Validate(SongCreateDto input)
        {
            //if (String.IsNullOrWhiteSpace(input.Genre))
            //{
            //    AddError("Missing genre", nameof(input.Genre));
            //}
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
            if (!await services.Songs.IsExistAsync(input.SongId))
            {
                AddError("Invalid top signature", nameof(input.TopSignature));
            }
            if (!await services.Instruments.IsExistAsync(input.InstrumentId))
            {
                AddError("Invalid top signature", nameof(input.TopSignature));
            }
            if (input.TopSignature <= 0)
            {
                AddError("Invalid top signature", nameof(input.TopSignature));

            }
            if (input.BottomSignature < input.TopSignature)
            {
                AddError("Invalid bottom signature", nameof(input.BottomSignature));
            }
            if (String.IsNullOrWhiteSpace(input.RightSymbol))
            {
                AddError("Missing symbols", nameof(input.RightSymbol));
            }
            else
            {
                string[] measureStrings = input.RightSymbol.Split(new char[] { '/' });
                for (int i = 0; i < measureStrings.Length; i++)
                {
                    try
                    {
                        string[] chordStrings = measureStrings[i].Split(new char[] { ' ' });
                        if (chordStrings[0].Length == 1)
                        {
                            chordStrings = chordStrings.Skip(1).ToArray();
                        }
                        double totalDuration = chordStrings.Select(chordString => double.Parse(chordString.Split('_')[1].Split('-')[0])).Sum();
                        bool isGoodBeatNum = ValiddateMeasureBeats(totalDuration, input.TopSignature, input.BottomSignature);
                        if (!isGoodBeatNum)
                        {
                            AddError($"Right hand measure {i + 1} has invalid number of beats", nameof(input.RightSymbol));
                        }
                    }
                    catch (Exception ex)
                    {
                        AddError($"Right hand measure {i + 1} {ex.Message}", nameof(input.RightSymbol));
                    }
                }

            }
            if (!String.IsNullOrWhiteSpace(input.LeftSymbol))
            {
                string[] measureStrings = input.LeftSymbol.Split(new char[] { '/' });
                for (int i = 0; i < measureStrings.Length; i++)
                {
                    try
                    {
                        string[] chordStrings = measureStrings[i].Split(new char[] { ' ' });
                        if (chordStrings[0].Length == 1)
                        {
                            chordStrings = chordStrings.Skip(1).ToArray();
                        }
                        double totalDuration = chordStrings.Select(chordString => double.Parse(chordString.Split('_')[1].Split('-')[0])).Sum();
                        bool isGoodBeatNum = ValiddateMeasureBeats(totalDuration, input.TopSignature, input.BottomSignature);
                        if (!isGoodBeatNum)
                        {
                            AddError($"Left hand measure {i + 1} has invalid number of beats", nameof(input.RightSymbol));
                        }
                    }
                    catch (Exception ex)
                    {
                        AddError($"Left hand measure {i + 1} {ex.Message}", nameof(input.RightSymbol));
                    }
                }
            }
        }

        public async Task ValidateAsync(SheetCreateDto input, IServiceWrapper services)
        {
            if (!await services.Songs.IsExistAsync(input.SongId))
            {
                AddError("Invalid top signature", nameof(input.TopSignature));
            }
            if (!await services.Instruments.IsExistAsync(input.InstrumentId))
            {
                AddError("Invalid top signature", nameof(input.TopSignature));
            }
            if (input.TopSignature <= 0)
            {
                AddError("Invalid top signature", nameof(input.TopSignature));

            }
            if (input.BottomSignature < input.TopSignature)
            {
                AddError("Invalid bottom signature", nameof(input.BottomSignature));
            }
            if (!input.RightMeasures.Any())
            {
                AddError("Missing measures", nameof(input.RightMeasures));
            }
            else
            {
                for (int i = 0; i < input.RightMeasures.Count; i++)
                {
                    var measures = input.RightMeasures.ToArray();
                    var totalDuration = measures[i].Chords.Select(c => c.Duration).Sum();
                    bool isGoodBeatNum = ValiddateMeasureBeats(totalDuration, input.TopSignature, input.BottomSignature);
                    if (!isGoodBeatNum)
                    {
                        AddError($"Measure {i + 1} has invalid number of beats", nameof(input.RightMeasures));
                    }
                }
            }
        }

        public async Task ValidateAsync(SheetMidiCreateDto input, IServiceWrapper services)
        {
            if (!await services.Songs.IsExistAsync(input.SongId))
            {
                AddError("Invalid top signature", nameof(input.TopSignature));
            }
            if (!await services.Instruments.IsExistAsync(input.InstrumentId))
            {
                AddError("Invalid top signature", nameof(input.TopSignature));
            }
            if (input.TopSignature <= 0)
            {
                AddError("Invalid top signature", nameof(input.TopSignature));

            }
            if (input.BottomSignature < input.TopSignature)
            {
                AddError("Invalid bottom signature", nameof(input.BottomSignature));
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
