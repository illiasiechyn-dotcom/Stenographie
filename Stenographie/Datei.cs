using System;
using System.IO;
using System.Windows;

namespace Stenographie
{
    public static class Datei //Datei-Ein- und -Ausgabe
    {
        private static byte[] Lesen(string s_Dateipfad) //Datei lesen und deren Inhalt als Byte-Array zurückgeben
        {
            FileStream fsRead = new FileStream(s_Dateipfad, FileMode.Open, FileAccess.Read); //Datei im Lesemodus öffnen
            BinaryReader br = new BinaryReader(fsRead); //Objectder Klasse BinaryReader erstellen, um Binärdaten zu lesen

            byte[] aby_DateiBytes = new byte[fsRead.Length]; //ein Byte-Array mit der gleichen Länge wie die Datei erstellen
            for (int i = 0; i < fsRead.Length; i++)
            {
                aby_DateiBytes[i] = br.ReadByte();
            } //Datei Byte für Byte lesen und in das Array speichern

            br.Close();
            fsRead.Close();
            return aby_DateiBytes;
        }

        private static void Schreiben(byte[] daten, string zielpfad) //ein Byte-Array in eine Datei schreiben
        {
            if (!File.Exists(zielpfad))
            {
                File.CreateText(zielpfad).Close();
            }
            FileStream fsWrite = new FileStream(zielpfad, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fsWrite);

            for (int i = 0; i < daten.Length; i++)
            {
                bw.Write(daten[i]);
            } //alle Bytes einzeln in die Datei schreiben

            bw.Flush(); //Schreibpuffer leeren und Datei sichern
            bw.Close();
            fsWrite.Close();
        }

        public static void Verstecken(string s_Dateipfad, string s_BMPpfad)
        {
            if (s_Dateipfad != s_BMPpfad)
            {
                if (BMP.IstGueltigesBmp(s_BMPpfad))
                {
                    if (!File.Exists(s_Dateipfad))
                    {
                        MessageBox.Show("Fehler: Dateipfad ist nicht gültig", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        byte[] aby_BMPDaten = Lesen(s_BMPpfad);
                        Datei.Schreiben(aby_BMPDaten, s_BMPpfad + ".bak"); //eine Sicherungskopie der BMP-Datei erstellen
                        byte[] aby_DateiBytes = Lesen(s_Dateipfad);
                        byte[] aby_komprimiert = RLE.Komprimieren(aby_DateiBytes); //die Datei mit den RLE(Run-Length-Encoding)-Verfahren komprimieren
                        if (aby_komprimiert.Length > BMP.KapazitaetBerechnen(s_BMPpfad))
                        {
                            MessageBox.Show("Fehler: BMP ist nicht groß genug.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            byte[] aby_komprverschl = LSB.Verschuesseln(aby_BMPDaten, aby_komprimiert); //LSB - Least Significant Bit
                            Datei.Schreiben(aby_komprverschl, s_BMPpfad);
                            MessageBox.Show("Erfolg: Datei wurde Erfolgreich versteckt", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Fehler: BMP-Datei ist identisch mit der Queldatei.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public static void Rausholen(string s_DateiPfad, string s_BMPpfad)
        {

            if (s_DateiPfad == null || s_DateiPfad == "")
            {
                MessageBox.Show("Fehler: Dateipfad ist nicht gültig.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (s_DateiPfad == s_BMPpfad)
                {
                    MessageBox.Show("Fehler: Datei kann nicht rausgeholt werden.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (BMP.IstGueltigesBmp(s_BMPpfad))
                {
                    byte[] aby_BMPDaten = Lesen(s_BMPpfad);
                    byte[] aby_komprentschl = LSB.Entschluesseln(aby_BMPDaten);
                    byte[] aby_rausgeholt = RLE.Dekomprimieren(aby_komprentschl);
                    Datei.Schreiben(aby_rausgeholt, s_DateiPfad);
                    MessageBox.Show("Erfolg: Datei wurde Erfolgreich rausgeholt", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
