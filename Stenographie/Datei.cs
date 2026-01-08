using System;
using System.IO;
using System.Windows;

namespace Stenographie
{
    public static class Datei
    {
        private static byte[] Lesen(string s_Dateipfad)
        {
            FileStream fsRead = new FileStream(s_Dateipfad, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fsRead);
            
            byte[] aby_DateiBytes = new byte[fsRead.Length];
            for (int i = 0; i < fsRead.Length; i++)
            {
                aby_DateiBytes[i] = br.ReadByte();
            }

            br.Close();
            fsRead.Close();
            return aby_DateiBytes;
        }

        private static void Schreiben(byte[] daten, string zielpfad) 
        {
            if (!File.Exists(zielpfad)) {
                File.CreateText(zielpfad).Close();
            }
            FileStream fsWrite = new FileStream(zielpfad, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fsWrite);

            for (int i = 0; i < daten.Length; i++)
            {
                bw.Write(daten[i]);
            }

            bw.Flush();
            bw.Close();
            fsWrite.Close();
        }

        public static void Verstecken(string s_Dateipfad, string s_BMPpfad)
        {
            if (!BMP.IstGueltigesBmp(s_BMPpfad))
            {
                
            }
            else
            {
                byte[] aby_BMPDaten = Lesen(s_BMPpfad);
                Datei.Schreiben(aby_BMPDaten, s_BMPpfad + ".bak");
                if (!File.Exists(s_Dateipfad)) {
                    MessageBox.Show("Fehler: Dateipfad ist nicht gültig", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                } 
                else { 
                byte[] aby_DateiBytes = Lesen(s_Dateipfad);
                byte[] aby_komprimiert = RLE.Komprimieren(aby_DateiBytes);
                if(aby_komprimiert.Length > BMP.KapazitaetBerechnen(s_BMPpfad))
                {
                    MessageBox.Show("Fehler: BMP ist nicht groß genug.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else {
                    byte[] aby_komprverschl = LSB.Verschuesseln(aby_BMPDaten, aby_komprimiert);
                    Datei.Schreiben(aby_komprverschl, s_BMPpfad);
                    MessageBox.Show("Erfolg: Datei wurde Erfolgreich versteckt", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            } 
        }


        public static void Rausholen(string s_DateiPfad, string s_BMPpfad)
        {
            if (!BMP.IstGueltigesBmp(s_BMPpfad))
            {
               
            }
            else
            {
                byte[] aby_BMPDaten = Lesen(s_BMPpfad);
                byte[] aby_komprentschl = LSB.Entschluesseln(aby_BMPDaten);
                byte[] aby_rausgeholt = RLE.Dekomprimieren(aby_komprentschl);
                if (s_DateiPfad == null)
                {
                    MessageBox.Show("Fehler: Dateipfad ist nicht gültig", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Datei.Schreiben(aby_rausgeholt, s_DateiPfad);
                    MessageBox.Show("Erfolg: Datei wurde Erfolgreich rausgeholt", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
