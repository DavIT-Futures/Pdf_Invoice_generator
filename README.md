# Pdf_Invoice_generator
Windows desktop app (WPF, .NET 8) to generate invoices in PDF.

Seller details, bank accounts and the logo are neutral placeholders: fill in your own
data in `PdfDavITFutures/Service/PdfService.cs` (`Seller()` method) and replace
`PdfDavITFutures/DavIT_Futures2.jpg` with your logo.

The amount field starts empty; hover over it to see an example value.

## Build and test
```
dotnet build
dotnet test
```
