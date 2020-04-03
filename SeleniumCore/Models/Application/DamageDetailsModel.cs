using SeleniumCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCore.Models.Application
{
    public class DamageDetailsModel
    {
        public string Station { get; set; } = Randomize.GenerateNumberWithNumberOfDigitsAndNumberOfDecimals(2, 2).ToString();
        public string Timecode { get; set; } = Randomize.GenerateNumberWithNumberOfDigits(8).ToString();
        public string Frame { get; set; } = Randomize.GenerateNumberWithNumberOfDigits(8).ToString();
        public string Videozaehler { get; set; } = Randomize.GenerateNumberWithNumberOfDigits(5).ToString();
        public string InspektionsKode { get; set; } = Randomize.GenerateRandomStringWithMaxLength(10);
        public string Charakterisierung1 { get; set; } = Randomize.GenerateRandomStringWithMaxLength(10);
        public string Charakterisierung2 { get; set; } = Randomize.GenerateRandomStringWithMaxLength(10);
        public string Verbindung { get; set; } = Randomize.GenerateNumberWithATopLimit(2).ToString();
        public string Quantifizierung1Numerisch { get; set; } = Randomize.GenerateNumberWithNumberOfDigitsAndNumberOfDecimals(3, 2).ToString();
        public string Quantifizierung2Numerisch { get; set; } = Randomize.GenerateNumberWithNumberOfDigitsAndNumberOfDecimals(3, 2).ToString();
        public string Streckenschaden { get; set; } = Randomize.GenerateRandomStringWithMaxLength(3);
        public string StreckenschadenLfdNr { get; set; } = Randomize.GenerateNumberWithNumberOfDigits(3).ToString();
        public string GrundAbbruch { get; set; } = Randomize.GenerateRandomStringWithMaxLength(2);
        public string PositionVon { get; set; } = "";
        public string PositionBis { get; set; } = "";
        public string BezeichnungSanierung { get; set; } = Randomize.GenerateRandomStringWithMaxLength(5);
        public string BAKZustandSanierung { get; set; } = Randomize.GenerateRandomStringWithMaxLength(2);
        public string BALZustandSanierung { get; set; } = Randomize.GenerateRandomStringWithMaxLength(2);
        public string QZustandSanierung { get; set; } = Randomize.GenerateNumberWithNumberOfDigits(4).ToString();
        public string RVerfahrenSanierung { get; set; } = "-";
        public string Kommentar { get; set; } = "This damage was created by an automated test";

    }
}
