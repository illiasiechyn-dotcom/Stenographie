using System.Collections.Generic;

namespace Stenographie
{
    public static class RLE
    {
        public static byte[] Komprimieren(byte[] aby_data)
        {
            List<byte> li_Ergebnis = new List<byte>();

            int i_Index = 0;
            while (i_Index < aby_data.Length)
            {
                byte by_AktuellesByte = aby_data[i_Index];
                int i_Anzahl = 1;

                while (i_Index + i_Anzahl < aby_data.Length &&
                       aby_data[i_Index + i_Anzahl] == by_AktuellesByte &&
                       i_Anzahl < 255)
                {
                    i_Anzahl++;
                }

                if (i_Anzahl > 1)
                {
                    li_Ergebnis.Add((byte)i_Anzahl);
                    li_Ergebnis.Add(by_AktuellesByte);
                }
                else
                {
                    li_Ergebnis.Add(0);
                    li_Ergebnis.Add(by_AktuellesByte);
                }

                i_Index += i_Anzahl;
            }

            return li_Ergebnis.ToArray();
        }

        public static byte[] Dekomprimieren(byte[] aby_data)
        {
            List<byte> li_Ergebnis = new List<byte>();

            for (int i = 0; i < aby_data.Length; i += 2)
            {
                byte by_Anzahl = aby_data[i];
                byte by_Wert = aby_data[i + 1];

                if (by_Anzahl == 0)
                {
                    li_Ergebnis.Add(by_Wert);
                }
                else
                {
                    for (int j = 0; j < by_Anzahl; j++)
                        li_Ergebnis.Add(by_Wert);
                }
            }

            return li_Ergebnis.ToArray();
        }
    }
}
