# PDF Invoice Generator

<p align="center">
  <a href="https://github.com/DavIT-Futures/Pdf_Invoice_generator/actions/workflows/ci.yml">
    <img src="https://github.com/DavIT-Futures/Pdf_Invoice_generator/actions/workflows/ci.yml/badge.svg" alt="Build and Test Status" />
  </a>
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/C%23-12-239120?style=flat&logo=csharp&logoColor=white" alt="C# 12" />
  <img src="https://img.shields.io/badge/UI-WPF%20%2F%20XAML-0078D6?style=flat" alt="WPF XAML" />
  <img src="https://img.shields.io/badge/Platform-Windows-0078D6?style=flat&logo=windows&logoColor=white" alt="Platform: Windows" />
  <img src="https://img.shields.io/badge/Tests-16%20Passed-brightgreen.svg" alt="Tests" />
  <img src="https://img.shields.io/badge/License-MIT-green.svg" alt="MIT License" />
</p>

---

### Language / Język
**[🇬🇧 English](#english)** | **[🇵🇱 Polski](#polski)**

---

<a id="english"></a>
## 🇬🇧 English

### Overview
**PDF Invoice Generator** is a lightweight, modern Windows desktop application built with **WPF** and **.NET 8**. Designed specifically for software contractors, IT consultants, freelancers, and small businesses, it generates clean, standardized, bilingual (English/Polish) B2B invoices in PDF format with a single click.

It eliminates repetitive billing overhead by automating hourly/daily rate calculations, VAT rates (standard 23% vs. reverse charge / out-of-scope for foreign entities), client presets, invoice numbering, and strict file naming conventions.

---

### ✨ Key Features
- **🌐 Bilingual Invoices (EN / PL)**: Side-by-side labels for international clients and Polish tax compliance (*e.g., "Invoice receiver [podpis osoby uprawnionej do odbioru faktury]"*).
- **⏱️ Flexible Billing Modes**:
  - **Hourly rate** (hourly rate × billable hours).
  - **Daily rate** (daily rate × billable days).
  - **Flat / Single lump sum** (fixed service fee).
- **💱 Multi-Currency Support**:
  - Native support for **PLN**, **EUR**, and **GBP**.
  - Dynamic assignment of dedicated domestic or international IBAN bank accounts.
- **📊 Automated VAT & Tax Calculations**:
  - Automatic 23% VAT calculation with Net, VAT, and Gross breakdown.
  - Reverse charge / non-taxable export (*"np" / "n/a"*) support for EU and UK clients.
- **🔢 Smart Invoice & File Naming**:
  - Deterministic invoice number formatting based on issue date/time or manual override.
  - Automatic, standardized output file naming: `YYYY-MM-DD - sale [sprzedaz] - ClientName.pdf`.
- **🏢 Client Presets**:
  - Quick-switch radio buttons for frequent contracting clients, pre-filling VAT rates, payment units, default currencies, and contract reference numbers.
- **🖼️ Logo & Branding**:
  - Embedded vector-aligned company logo rendering directly from project resources.

---

### 🛠️ Tech Stack & Architecture

| Component | Technology | Description |
|---|---|---|
| **Platform / Runtime** | **.NET 8.0** (`net8.0-windows`) | Modern high-performance .NET runtime |
| **UI Framework** | **WPF (Windows Presentation Foundation)** | XAML-based native Windows user interface |
| **Language** | **C# 12** | Nullable reference types enabled, switch expressions, modern patterns |
| **PDF Generation** | **`itextsharp.LGPLv2.Core` (1.7.4)** | Clean, dependency-light PDF vector generation |
| **Cryptography** | **`BouncyCastle.Cryptography` (2.7.0)** | Security & crypto primitives required for PDF packaging |
| **Unit Testing** | **xUnit (2.4.1)** + `Microsoft.NET.Test.Sdk` | Automated test suite for calculations, parsing, and naming |
| **CI/CD** | **GitHub Actions** | Automated build and test verification pipeline on Windows |

---

### 📂 Project Structure
```text
Pdf_Invoice_generator/
├── .github/
│   └── workflows/
│       └── ci.yml                     # GitHub Actions CI workflow (Windows build & test)
├── PdfDavITFutures/                   # Main WPF Application
│   ├── Properties/
│   │   ├── AssemblyInfo.cs            # Assembly metadata
│   │   ├── Resources.resx             # Embedded logo & graphics resources
│   │   └── Settings.settings          # Application settings
│   ├── Service/
│   │   ├── Extensions.cs              # Helper extensions
│   │   ├── PdfHelpers.cs              # Parsing logic, file & invoice numbering helpers
│   │   └── PdfService.cs              # Core PDF layout, tables, VAT logic, seller & buyer profiles
│   ├── App.xaml / App.xaml.cs         # Application entry point
│   ├── MainWindow.xaml / .xaml.cs     # Main user interface & form bindings
│   ├── DavIT_Futures2.jpg             # Neutral placeholder logo (replaces company logo)
│   └── PdfDavITFutures.csproj         # Modern .NET 8 WPF project file
├── tests/
│   └── PdfDavITFutures.Tests/         # Unit Test Suite (xUnit)
│       ├── GenerateFileNameTests.cs   # Naming convention test cases
│       ├── GenerateInvoiceNoTests.cs  # Invoice number formatting test cases
│       ├── PdfHelpersParsingTests.cs  # Decimal & integer safe parsing tests
│       ├── PdfServiceValueTests.cs    # Hourly/daily billing calculation tests
│       └── PdfDavITFutures.Tests.csproj
├── LICENSE                            # MIT License
├── PdfDavITFutures.sln                # Visual Studio / .NET Solution
└── README.md                          # Bilingual documentation
```

---

### 🚀 Getting Started

#### Prerequisites
- **Operating System**: Windows 10 / 11 (or Windows Server 2019+)
- **SDK**: [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or newer
- **IDE** (optional): Visual Studio 2022 (with *.NET desktop development* workload), JetBrains Rider, or VS Code.

#### 1. Clone the repository
```bash
git clone https://github.com/DavIT-Futures/Pdf_Invoice_generator.git
cd Pdf_Invoice_generator
```

#### 2. Build the solution
```bash
dotnet build PdfDavITFutures.sln
```

#### 3. Run the unit tests
```bash
dotnet test PdfDavITFutures.sln
```

#### 4. Run the desktop application
```bash
dotnet run --project PdfDavITFutures/PdfDavITFutures.csproj
```

---

### ⚙️ Customization & Configuration

1. **Seller Information & Bank Accounts**:
   Open [`PdfDavITFutures/Service/PdfService.cs`](PdfDavITFutures/Service/PdfService.cs) and update the `Seller()` method with your company details (Name, NIP / Tax ID, REGON, address, and bank accounts for PLN, EUR, and GBP).
2. **Company Logo**:
   Replace [`PdfDavITFutures/DavIT_Futures2.jpg`](PdfDavITFutures/DavIT_Futures2.jpg) with your own company logo.
3. **Clients / Contractors**:
   Customize the predefined buyer entries in `Buyer()` method inside [`PdfDavITFutures/Service/PdfService.cs`](PdfDavITFutures/Service/PdfService.cs) or add new radio buttons in [`MainWindow.xaml`](PdfDavITFutures/MainWindow.xaml).

---

### 🧪 Testing
The test project (`PdfDavITFutures.Tests`) covers:
- Invariant & localized decimal/int string parsing (`PdfHelpers.ParseDecimalSafe`, `ParseIntSafe`).
- File name generation for all supported client profiles (`PdfHelpers.GenerateFileName`).
- Time-stamped invoice number formatting (`PdfHelpers.GenerateInvoiceNo`).
- Multiplication and rounding of hourly/daily rates against billable hours/days (`PdfServiceValueTests`).

Run all tests from the CLI:
```bash
dotnet test --logger "console;verbosity=detailed"
```

---

<a id="polski"></a>
## 🇵🇱 Polski

### Opis projektu
**PDF Invoice Generator** to lekka, nowoczesna aplikacja desktopowa dla systemu Windows, stworzona w technologii **WPF** w oparciu o platformę **.NET 8**. Narzędzie powstało z myślą o programistach, konsultantach IT, freelancerach oraz jednoosobowych działalnościach gospodarczych świadczących usługi B2B dla klientów w Polsce i za granicą.

Aplikacja pozwala błyskawicznie wygenerować profesjonalną, dwujęzyczną (polsko-angielską) fakturę VAT w formacie PDF. Automatyzuje wyliczenia stawek godzinowych i dziennych, podatku VAT (23% lub "np" / odwrotne obciążenie dla kontrahentów zagranicznych), numerację faktur oraz standaryzację nazw generowanych plików.

---

### ✨ Główne funkcjonalności
- **🌐 Dwujęzyczne faktury (PL / EN)**: Podwójne etykiety na dokumencie ułatwiające rozliczenia z klientami międzynarodowymi przy pełnej zgodności z polskimi przepisami.
- **⏱️ Elastyczne modele rozliczeń**:
  - **Stawka godzinowa** (stawka × liczba przepracowanych godzin).
  - **Stawka dzienna** (stawka × liczba przepracowanych dni roboczych).
  - **Kwota ryczałtowa / jednorazowa** (stałe wynagrodzenie).
- **💱 Obsługa wielu walut**:
  - Wsparcie dla **PLN**, **EUR** oraz **GBP**.
  - Automatyczny dobór właściwego numeru rachunku bankowego (krajowego lub IBAN/SWIFT).
- **📊 Precyzyjne kalkulacje podatkowe i VAT**:
  - Automatyczne wyliczanie kwot netto, podatku VAT (23%) oraz kwoty brutto.
  - Obsługa transakcji poza zakresem krajowego VAT (*"np" / odwrotne obciążenie*) dla kontrahentów z UE i Wielkiej Brytanii.
- **🔢 Automatyczna numeracja i standaryzacja plików**:
  - Unikalny numer faktury generowany na podstawie daty i czasu lub możliwość wpisania własnego numeru.
  - Nazwa pliku PDF tworzona wg schematu: `RRRR-MM-DD - sale [sprzedaz] - Klient.pdf`.
- **🏢 Predefiniowane profile kontrahentów**:
  - Szybki wybór klienta jednym kliknięciem z automatycznym ustawieniem waluty, stawki referencyjnej, jednostki rozliczeniowej i numeru zamówienia (PO).
- **🖼️ Logo i estetyka**:
  - Wbudowane renderowanie wektorowego nagłówka z logo firmy ładowanym z zasobów.

---

### 🛠️ Stos technologiczny (Tech Stack)

| Element | Technologia | Zastosowanie |
|---|---|---|
| **Środowisko uruchomieniowe** | **.NET 8.0** (`net8.0-windows`) | Nowoczesna, wydajna platforma .NET |
| **Interfejs użytkownika** | **WPF (Windows Presentation Foundation)** | Deklaratywny interfejs XAML |
| **Język programowania** | **C# 12** | Bezpieczeństwo typów nullowalnych, nowoczesna składnia |
| **Generowanie PDF** | **`itextsharp.LGPLv2.Core` (1.7.4)** | Wydajne, bezpośrednie tworzenie plików PDF |
| **Kryptografia** | **`BouncyCastle.Cryptography` (2.7.0)** | Biblioteka kryptograficzna wymagana przez silnik PDF |
| **Testy jednostkowe** | **xUnit (2.4.1)** + `Microsoft.NET.Test.Sdk` | Pokrycie testami logiki parsowania i obliczeń |
| **CI/CD** | **GitHub Actions** | Automatyczna weryfikacja kompilacji i testów na Windows |

---

### 🚀 Uruchomienie i instalacja

#### Wymagania wstępne
- System operacyjny: Windows 10 lub Windows 11
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Opcjonalnie: Visual Studio 2022, JetBrains Rider lub VS Code

#### Komendy deweloperskie
```bash
# Sklonowanie repozytorium
git clone https://github.com/DavIT-Futures/Pdf_Invoice_generator.git
cd Pdf_Invoice_generator

# Kompilacja rozwiązania
dotnet build PdfDavITFutures.sln

# Uruchomienie testów jednostkowych
dotnet test PdfDavITFutures.sln

# Uruchomienie aplikacji
dotnet run --project PdfDavITFutures/PdfDavITFutures.csproj
```

---

### ⚙️ Konfiguracja danych sprzedawcy
1. **Dane firmy i konta bankowe**:
   W pliku [`PdfDavITFutures/Service/PdfService.cs`](PdfDavITFutures/Service/PdfService.cs) w metodzie `Seller()` uzupełnij dane swojej firmy (nazwę, NIP, REGON, adres, telefon, email oraz numery rachunków bankowych).
2. **Podmiana logotypu**:
   Zastąp plik [`PdfDavITFutures/DavIT_Futures2.jpg`](PdfDavITFutures/DavIT_Futures2.jpg) własnym plikiem graficznym.
3. **Modyfikacja kontrahentów**:
   Profile klientów można edytować w metodzie `Buyer()` w klasie `PdfService.cs` oraz w pliku `MainWindow.xaml`.

---

### 📄 Licencja
Projekt jest udostępniany na zasadach licencji [MIT](LICENSE).
