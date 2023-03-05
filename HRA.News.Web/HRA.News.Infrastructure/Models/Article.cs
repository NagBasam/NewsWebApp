using System;
using System.Collections.Generic;
using System.Text;

namespace HRA.News.Infrastructure.Models
{
    public class Article
    {
        public int Id { get; set; }
        public Statuses Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public string PublishedAt { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }

    }

    public enum Statuses
    {
        Ok = 0,
        Error = 1
    }

    public class LanguagesDrp
    {
        public string Text { get; set; }
        public string Value { get; set; }

    }

    public enum Languages
    {
        AF = 0,
        AN = 1,
        AR = 2,
        AZ = 3,
        BG = 4,
        BN = 5,
        BR = 6,
        BS = 7,
        CA = 8,
        CS = 9,
        CY = 10,
        DA = 11,
        DE = 12,
        EL = 13,
        EN = 14,
        EO = 15,
        ES = 16,
        ET = 17,
        EU = 18,
        FA = 19,
        FI = 20,
        FR = 21,
        GL = 22,
        HE = 23,
        HI = 24,
        HR = 25,
        HT = 26,
        HU = 27,
        HY = 28,
        ID = 29,
        IS = 30,
        IT = 31,
        JP = 32,
        JV = 33,
        KK = 34,
        KO = 35,
        LA = 36,
        LB = 37,
        LT = 38,
        LV = 39,
        MG = 40,
        MK = 41,
        ML = 42,
        MR = 43,
        MS = 44,
        NL = 45,
        NN = 46,
        NO = 47,
        OC = 48,
        PL = 49,
        PT = 50,
        RO = 51,
        RU = 52,
        SH = 53,
        SK = 54,
        SL = 55,
        SQ = 56,
        SR = 57,
        SV = 58,
        SW = 59,
        TA = 60,
        TE = 61,
        TH = 62,
        TL = 63,
        TR = 64,
        UK = 65,
        UR = 66,
        VI = 67,
        VO = 68,
        ZH = 69
    }
}
