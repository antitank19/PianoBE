﻿using Microsoft.AspNetCore.Http;

namespace ServiceLayer.DTOs.Sheet
{
    public class SheetMidiCreateDto
    {
        public int SongId { get; set; }
        public int InstrumentId { get; set; }
        public int TopSignature { get; set; }
        public int BottomSignature { get; set; }
        public IFormFile MidiFile { get; set; }
    }
}
