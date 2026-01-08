using System;
using System.Collections.Generic;

namespace Stenographie
{
    public static class LSB
    {
        private const int HEADER_GROESSE = 54;
        public static byte[] Verschuesseln(byte[] aby_BmpDaten, byte[] aby_ZuVerstecken)
        {
            byte[] aby_Laenge = BitConverter.GetBytes(aby_ZuVerstecken.Length);

            // Alle zu versteckenden Bytes: Länge + Daten
            byte[] aby_GesamtDaten = new byte[4 + aby_ZuVerstecken.Length];
            Array.Copy(aby_Laenge, 0, aby_GesamtDaten, 0, 4);
            Array.Copy(aby_ZuVerstecken, 0, aby_GesamtDaten, 4, aby_ZuVerstecken.Length);

            // In Bits umwandeln
            List<int> li_Bits = BytesZuBits(aby_GesamtDaten);

            // Bits in LSB der Pixeldaten schreiben
            int i_BitIndex = 0;
            for (int i = HEADER_GROESSE; i < aby_BmpDaten.Length && i_BitIndex < li_Bits.Count; i++)
            {
                // LSB auf 0 setzen und dann Datenbit einfügen
                aby_BmpDaten[i] = (byte)((aby_BmpDaten[i] & 0xFE) | li_Bits[i_BitIndex]);
                i_BitIndex++;
            }

            return aby_BmpDaten;
        }

        public static byte[] Entschluesseln(byte[] ar_BmpDaten)
        {
            // Zuerst die Länge lesen (erste 4 Bytes = 32 Bits)
            List<int> li_LaengeBits = new List<int>();
            for (int i = HEADER_GROESSE; i < HEADER_GROESSE + 32; i++)
            {
                // LSB auslesen
                li_LaengeBits.Add(ar_BmpDaten[i] & 0x01);
            }

            byte[] aby_LaengeBytes = BitsZuBytes(li_LaengeBits);
            int i_DatenLaenge = BitConverter.ToInt32(aby_LaengeBytes, 0);

            // Jetzt die eigentlichen Daten lesen
            int i_BenoetigteBits = i_DatenLaenge * 8;
            List<int> li_DatenBits = new List<int>();

            int i_StartPosition = HEADER_GROESSE + 32; // Nach den Längen-Bytes
            for (int i = i_StartPosition; i < i_StartPosition + i_BenoetigteBits; i++)
            {
                li_DatenBits.Add(ar_BmpDaten[i] & 0x01);
            }

            byte[] ar_Ergebnis = BitsZuBytes(li_DatenBits);

            return ar_Ergebnis;
        }
        private static List<int> BytesZuBits(byte[] ar_Bytes)
        {
            List<int> li_Bits = new List<int>();

            for (int i = 0; i < ar_Bytes.Length; i++)
            {
                byte by_Aktuell = ar_Bytes[i];

                // 8 Bits pro Byte (LSB zuerst)
                for (int j = 0; j < 8; j++)
                {
                    li_Bits.Add((by_Aktuell >> j) & 0x01);
                }
            }

            return li_Bits;
        }

        private static byte[] BitsZuBytes(List<int> li_Bits)
        {
            // Anzahl Bytes = Bits / 8
            int i_AnzahlBytes = li_Bits.Count / 8;
            byte[] ar_Bytes = new byte[i_AnzahlBytes];

            for (int i = 0; i < i_AnzahlBytes; i++)
            {
                byte by_Wert = 0;

                // 8 Bits zu einem Byte zusammenbauen (LSB zuerst)
                for (int j = 0; j < 8; j++)
                {
                    int i_BitIndex = i * 8 + j;
                    if (li_Bits[i_BitIndex] == 1)
                    {
                        by_Wert |= (byte)(1 << j);
                    }
                }

                ar_Bytes[i] = by_Wert;
            }

            return ar_Bytes;
        }
    }
}
