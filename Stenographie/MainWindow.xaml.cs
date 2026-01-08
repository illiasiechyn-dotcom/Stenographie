using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Stenographie
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string s_DateiPfad;
        string s_BMPpfad;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Datei_Auswahl_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();    
            if (openFileDialog.ShowDialog() == true)
            {
                textBoxDateiPfad.Text = openFileDialog.FileName;
                s_DateiPfad = textBoxDateiPfad.Text;
            }
        }

        private void Button_BMP_Auswählen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap images (*.bmp)|*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                textBoxBMPPfad.Text = openFileDialog.FileName;
                s_BMPpfad = textBoxBMPPfad.Text;
                if (BMP.IstGueltigesBmp(s_BMPpfad))
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad; // wichtig!
                    bmp.UriSource = new Uri(openFileDialog.FileName);
                    bmp.EndInit();

                    BMPVorher.Source = bmp;
                }
            }
        }

        private void Button_Verstecken_Click(object sender, RoutedEventArgs e)
        {
            Datei.Verstecken(s_DateiPfad, s_BMPpfad);
            if (BMP.IstGueltigesBmp(s_BMPpfad)) {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad; // wichtig!
                bmp.UriSource = new Uri(s_BMPpfad);
                bmp.EndInit();
                BMPnachher.Source = bmp;
            }
            
        }

        private void Button_Rausholen_Click(object sender, RoutedEventArgs e)
        {
            Datei.Rausholen(s_DateiPfad, s_BMPpfad);
        }

        private void textBoxDateiPfad_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_DateiPfad = textBoxDateiPfad.Text;
        }

        private void textBoxBMPPfad_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_BMPpfad = textBoxBMPPfad.Text;
            if (BMP.IstGueltigesBmp(s_BMPpfad))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad; // wichtig!
                bmp.UriSource = new Uri(s_BMPpfad);
                bmp.EndInit();
                BMPVorher.Source = bmp;
            }
        }
    }
}
