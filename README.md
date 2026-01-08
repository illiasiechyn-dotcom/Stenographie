# Stenographie

**Stenographie** ist ein WPF-Tool zur Steganographie, mit dem beliebige Dateien in 24-Bit-BMP-Bildern versteckt und wieder extrahiert werden können.  
Zur besseren Kapazitätsausnutzung werden die eingebetteten Daten mittels **Run-Length-Encoding (RLE)** komprimiert und anschließend per **Least Significant Bit (LSB)** im Pixelbereich gespeichert.


## Features

✔ Verstecken beliebiger Dateien in BMP-Bildern  
✔ Wiederherstellen der Originaldateien  
✔ Verlustfreie RLE-Kompression  
✔ LSB-Steganographie auf Byte-Ebene  
✔ `.bak`-Backup der Original-BMP  
✔ Visuelle Vorher/Nachher-Vorschau  
✔ Fehler- und Kapazitätsprüfungen  


## Funktionsweise

1. Datei einlesen
2. RLE-Kompressionsverfahren anwenden
3. Länge + Daten in Bits umwandeln
4. Bits in LSB der Pixeldaten schreiben
5. Dateiextraktion umgekehrt (LSB → Bits → Bytes → Dekompression)

Die Daten werden so gespeichert:
[4 Bytes Länge] + [komprimierte Nutzdaten]


## Technologien

| Komponente         | Einsatz       |
|--------------------|---------------|
| **Sprache**        | C#            |
| **Framework**      | .NET **9**    |
| **UI**             | WPF (XAML)    |
| **IDE**            | Visual Studio |
| **Steganographie** | LSB           |
| **Kompression**    | RLE           |
| **Bildformat**     | BMP (24-Bit)  |


## Projektstruktur

Stenographie/
│
├── MainWindow.xaml → UI Layout
├── MainWindow.xaml.cs → UI Logik
│
├── Datei.cs → Ein-/Ausgabe + Backup + Workflow
├── RLE.cs → Run-Length-Encoding
├── LSB.cs → Einbettung/Extraktion via LSB
├── BMP.cs → BMP-Prüfung & Kapazitätsberechnung
│
└── Stenographie.csproj



## Installation

Voraussetzungen:

- Windows
- .NET 9 SDK
- Visual Studio 2022 (oder neuer)
sd
Clone Repository:

bash: `git clone https://github.com/illiasiechyn-dotcom/Stenographie.git`

## Anwendung starten
In Visual Studio:

Build → Starten
oder via CLI:
`dotnet build`
`dotnet run`


## Bedienung

1. Datei auswählen → beliebige Datei
2. BMP auswählen → nur 24-Bit-BMP unterstützt
3. Verstecken drücken → Datei wird eingebettet
4. Rausholen drücken → Datei wird extrahiert

Das Original-BMP wird als: `<bild>.bmp.bak` gesichert.


## Beschränkungen

- Unterstützt nur BMP 24-Bit
- Kapazität abhängig von Dateigröße
- Keine Verschlüsselung (nur Steganographie)
- Kein Schutz vor Bildmanipulation
