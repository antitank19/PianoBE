using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EnumsAndConsts
{
    public enum KeySignatureEnum
    {
        //https://vi.wikipedia.org/wiki/D%E1%BA%A5u_h%C3%B3a#B%E1%BA%A3ng_t%C3%B3m_t%E1%BA%AFt
        FSharp = -7,//Sol trưởng
        CSharp = -6,//Rê trưởng
        GSharp = -5,//La trưởng
        DSharp = -4,//Mi trưởng
        ASharp = -3,//Si trưởng
        ESharp = -2,//Fa thăng trưởng
        BSharp = -1,//Đô thăng trưởng
        None = 0,
        Bb = 1,     //Fa trưởng
        Eb = 2,     //Si giáng trưởng
        Ab = 3,     //Mi giáng trưởng
        Db = 4,     //La giáng trưởng
        Gb = 5,     //Rê giáng trưởng
        Cb = 6,     //Sol giáng trưởng
        Fb = 7      //Đô giáng trưởng

    }
}
