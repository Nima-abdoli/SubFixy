using System;
using System.IO;

namespace SubFixy
{
    class Decoder
    {

        public string Result { get; set; }

        private string mPath { get; set; }

        private byte[] FileBytes { get; set; }

        public Decoder(string Path)
        {
            // Set mPath to entry path that caller give.
            mPath = Path;
        }

        public string Start()
        {
            ReadFile(mPath);

            foreach (byte item in FileBytes)
            {
                if (item >127)
                {
                    Result += Decoding(item.ToString("x2"));
                }
                else
                {
                    Result += (char)item;
                }
            }

            return Result;
        }

        private void ReadFile(string Path)
        {
            // Read All Bytes From File
            FileBytes = File.ReadAllBytes(mPath);

            // Return all bytes to caller 
            //return FileBytes;
        }

        private string Decoding(string Hex)
        {
            string s = "";

            switch (Hex)
            {
                case "c7":
                    s = "ا"; // Alef - Arabic/Persian character
                    break;
                case "c2":
                    s = "آ"; // Alef With Madda - Arabic/Persian Character
                    break;
                case "c8":
                    s = "ب"; // Beh - Arabic/Persian character
                    break;
                case "81":
                    s = "پ"; // peh - Persian character
                    break;
                case "ca":
                    s = "ت"; // Teh - Arabic/Persian character
                    break;
                case "cb":
                    s = "ث"; // Theh(Seh) - Arabic/Persian character
                    break;
                case "cc":
                    s = "ج"; // Jeem - Arabic/Persian character
                    break;
                case "8d":
                    s = "چ"; // Cheh - Persian character
                    break;
                case "cd":
                    s = "ح"; // Hah(Heh in persian) - Arabic/Persian character 
                    break;
                case "ce":
                    s = "خ"; // Khah(Kheh in Persian) - Arabic/Persian character
                    break;
                case "cf":
                    s = "د"; // Dal - Arabic/Persian character
                    break;
                case "d0":
                    s = "ذ"; // Thal(zal) - Arabic/Persian character
                    break;
                case "d1":
                    s = "ر"; // Reh - Arabic/Persian character
                    break;
                case "d2":
                    s = "ز"; // Zein(Zeh in Persian) - Arabic/Persian character
                    break;
                case "8e":
                    s = "ژ"; // Jeh - Persian Character
                    break;
                case "d3":
                    s = "س"; // Seem - Arabic/Persian character
                    break;
                case "d4":
                    s = "ش"; // Sheen - Arabic/Persian character
                    break;
                case "d5":
                    s = "ص"; // Sad - Arabic/Persian character
                    break;
                case "d6":
                    s = "ض"; // Dad(Zad in persian) - Arabic/Persian character
                    break;
                case "d8":
                    s = "ط"; // Tah - Arabic/Persian character
                    break;
                case "d9":
                    s = "ظ"; // Zah - Arabic/Persian character
                    break;
                case "da":
                    s = "ع"; // Ain - Arabic/Persian character
                    break;
                case "db":
                    s = "غ"; // Ghain - Arabic/Persian character
                    break;
                case "dd":
                    s = "ف"; // Feh - Arabic/Persian character
                    break;
                case "de":
                    s = "ق"; // Qaf - Arabic/Persian character
                    break;
                case "df":
                    s = "ًك"; // kaf - Arabic Character
                    break;
                case "98":
                    s = "ک"; // keh or kaf - Persian Character
                    break;
                case "90":
                    s = "گ"; // Gaf - Persian Character
                    break;
                case "e1":
                    s = "ل"; // Lam - Arabic/Persian character
                    break;
                case "e3":
                    s = "م"; // Meem - Arabic/Persian character
                    break;
                case "e4":
                    s = "ن"; // Noon - Arabic/Persian character
                    break;
                case "e6":
                    s = "و";  // Waw - Arabic/Persian character
                    break;
                case "e5":
                    s = "ه";  // Heh - Arabic/Persian character
                    break;
                case "ec":
                    s = "ی"; // Alef Maksura(Yeh in Persian) - Arabic/Persian Character
                    break;
                case "c3":
                    s = "أ"; // Alef With Hamza Above - Arabic Character(Not common/Not Used In Persian)
                    break;
                case "c5":
                    s = "إ"; // Alef With Hamza Below - Arabic Character(Not common/Not Used In Persian)
                    break;
                case "c4":
                    s = "ؤ"; // Waw With Hamza Above - Arabic Character(Not common/Not Used In Persian)
                    break;
                case "c6":
                    s = "ئ"; // Yeh With Hamza Above - Arabic Character(Not common/Not Used In Persian)
                    break;
                case "c1":
                    s = "ء"; // Hamza - Arabic Character(Not common/Not Used In Persian)
                    break;
                case "ed":
                    s = "ي"; // Yeh - Arabic Character(Not Not Used In Persian)
                    break;
                case "c9":
                    s = "ة"; // Teh marbuta  - Arabic Character
                    break;
                case "21":
                    s = "!"; // Exclamation Mark - Arabic/Persian Character
                    break;
                case "2e":
                    s = "."; //  Full Stop - Arabic/Persian Character
                    break;
                case "3a":
                    s = ":"; // Colon - Arabic/Persian Character
                    break;
                case "a1":
                    s = "،"; // Comma - Arabic/Persian Character
                    break;
                case "ba":
                    s = "؛"; // SemiColon - Arabic/Persian Character
                    break;
                case "bf":
                    s = "؟"; // Question Mark - Arabic/Persian Character
                    break;
                case "ab":
                    s = "«"; // Left-Pointing double angle qu otion Mark - Arabic/Persian Character
                    break;
                case "bb":
                    s = "»"; // Right-Pointing double angle qu otion Mark - Arabic/Persian Character
                    break;
                case "dc":
                    s = "ـ";  // Tatweel(Kasheeda) - Arabic/Persian Character
                    break;
                case "f0":
                    s = "ً"; // Fathatan - Arabic Charcter(Not Common/Not Used In Persian)
                    break;
                case "f1":
                    s = "ٌ"; // Dammatan - Arabic Charcter(Not Common/Not Used In Persian)
                    break;
                case "f2":
                    s = "ٍ"; // kasratan - Arabic Charcter(Not Common/Not Used In Persian)
                    break;
                case "f3":
                    s = "َ"; // Fatha - Arabic Charcter(Not Common/Not Used In Persian)
                    break;
                case "f5":
                    s = "ُ"; // Damma - Arabic Charcter(Not Common/Not Used In Persian)
                    break;
                case "f6":
                    s = "ِ"; // Kasra - Arabic Charcter(Not Common/Not Used In Persian)
                    break;
                case "f8":
                    s = "ّ"; // Shadda - Arabic/Persia Charcter
                    break;
                case "fa":
                    s = "ْ"; // Sukun - Arabic Charcter(Not Common/Not Used In Persian)
                    break;
            }

            return s;
        }

    }

}
