# Stenographie

**Stenographie** ist ein WPF-Tool zur Steganographie, mit dem beliebige Dateien in 24-Bit-BMP-Bildern versteckt und wieder extrahiert werden können.  
Zur besseren Kapazitätsausnutzung werden die eingebetteten Daten mittels **Run-Length-Encoding (RLE)** komprimiert und anschließend per **Least Significant Bit (LSB)** im Pixelbereich gespeichert.


## Features

1. Dateiversteckung - Einbetten beliebiger Dateien in 24-Bit-BMP-Bilder
2. Datenwiederherstellung – Verlustfreie Extraktion der Originaldateien
3. RLE-Kompression – Run-Length Encoding für optimierte Kapazitätsauslastung
4. LSB-Steganographie – Least Significant Bit-Verfahren auf Byte-Ebene
5. Automatisches Backup – Sicherung des Original-BMP als .bak-Datei
6. Visuelle Vorschau – Vorher/Nachher-Darstellung der BMP-Dateien
7. Integrierte Validierung – Formatprüfung und Kapazitätskontrolle 


## Funktionsweise

1. Kompression – Quelldatei wird mit RLE-Algorithmus komprimiert
2. Datenaufbereitung – Länge (4 Byte) + komprimierte Daten in Bitstream konvertiert
3. LSB-Einbettung – Bits werden in die niederwertigen Bits der RGB-Pixel geschrieben
4. Speicherung – Modifiziertes BMP wird gespeichert, Original als Backup erhalten
5. Extraktion – Umgekehrter Prozess: LSB-Auslesen → Dekompression → Originaldatei

Datenstruktur: [4-Byte-Längenfeld] + [RLE-komprimierte Nutzdaten]


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
├── MainWindow.xaml              # WPF-Benutzeroberfläche
├── MainWindow.xaml.cs           # UI-Ereignisbehandlung und Steuerung
├── Datei.cs                     # Datei-E/A, Backup-Logik und Workflow-Management
├── RLE.cs                       # RLE-Kompressions- und Dekompressionsroutine
├── LSB.cs                       # LSB-Einbettungs- und Extraktionsengine
├── BMP.cs                       # BMP-Validierung und Kapazitätsberechnung
└── Stenographie.csproj          .NET-Projektkonfiguration



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

1. Datei auswählen – Beliebiges Dateiformat zum Verstecken auswählen
2. BMP-Bild auswählen – 24-Bit BMP als Trägerdatei auswählen
3. "Verstecken" – Datei in BMP einbetten (erzeugt <bild>.bmp.bak-Backup)
4. "Rausholen" – Versteckte Datei aus modifiziertem BMP extrahieren

Das Original-BMP wird als: `<bild>.bmp.bak` gesichert.


## Beschränkungen

- Unterstützt nur BMP 24-Bit
- Kapazität abhängig von Dateigröße
- Keine Verschlüsselung (nur Steganographie)
- Kein Schutz vor Bildmanipulation
