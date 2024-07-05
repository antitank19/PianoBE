﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EnumsAndConsts
{

    public enum ChromaticEnum
    {
        Flat = 0,
        Natural,
        Sharp
    }
    public static class ChromaticConst
    {
        public static string Flat = "b";
        public static string Sharp = "#";
        public static string Natural = "n";
    }
}
