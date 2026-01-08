using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Stenographie
{
    public static class BMP
    {
        private const int HEADER_GROESSE = 54;
        public static bool IstGueltigesBmp(string s_DateiPfad)
        {
            if (!File.Exists(s_DateiPfad))
            {
                return false;
            }

            if(KapazitaetBerechnen(s_DateiPfad) <= 0)
            { return false; }

            FileStream fs = new FileStream(s_DateiPfad, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            // prüfe auf BM
            byte by_B = br.ReadByte();  
            byte by_M = br.ReadByte();  

            if (by_B != 0x42 || by_M != 0x4D)  // 0x42 ist hex ins ASCII für B-Zeichen.
            {
                br.Close();
                fs.Close();
                return false;
            }

            fs.Seek(28, SeekOrigin.Begin);
            short sh_BitTiefe = br.ReadInt16();

            br.Close();
            fs.Close();

            return sh_BitTiefe == 24;
        }
        public static int KapazitaetBerechnen(string s_DateiPfad)
        {
            FileInfo fi = new FileInfo(s_DateiPfad);
            long l_DateiGroesse = fi.Length;

            long l_PixelDatenGroesse = l_DateiGroesse - HEADER_GROESSE;

            int i_Kapazitaet = (int)(l_PixelDatenGroesse / 8) - 4;

            return i_Kapazitaet;
        }
    }
}
