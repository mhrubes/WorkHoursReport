using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JobHoursPerMonth
{
    public class PDFController
    {
        public Document CreatePdfDocument(Dictionary<string, List<string>> monthlyData, double totalHoursForActualMonth, double totalHoursForYear, string initials)
        {
            Document document = new Document();
            document.Info.Title = "Pracovní výkaz";

            Section section = document.AddSection();

            // Přidání odstavce pro nadpis
            Paragraph title = section.AddParagraph($"Výpis Hodin za rok {GlobalVariables.date.Year}");
            title.Format.Font.Size = 20; // Velikost písma
            title.Format.Alignment = ParagraphAlignment.Center; // Zarovnání na střed

            // Přidání čáry
            Paragraph line = section.AddParagraph();
            line.Format.SpaceAfter = "0.5cm";

            // Přidání tabulky
            MigraDoc.DocumentObjectModel.Tables.Table pdfTable = section.AddTable();
            pdfTable.Borders.Width = 0.75;

            // Rozdělení sloupců rovnoměrně na šířku stránky
            pdfTable.AddColumn(Unit.FromCentimeter(8)); // První sloupec
            pdfTable.AddColumn(Unit.FromCentimeter(8)); // Druhý sloupec

            // Hlavička tabulky
            Row headerRow = pdfTable.AddRow();
            headerRow.Shading.Color = Colors.LightGray;
            headerRow.Cells[0].AddParagraph("Měsíc");
            headerRow.Cells[1].AddParagraph("Celkový počet hodin za měsíc");

            // Přidání dat do tabulky
            foreach (var pair in monthlyData)
            {
                string month = pair.Key;
                List<string> data = pair.Value;

                double totalHours = 0;
                foreach (string item in data)
                    if (double.TryParse(item.Split(' ')[0], out double parsedHours))
                        totalHours += parsedHours;

                Row dataRow = pdfTable.AddRow();
                dataRow.Cells[0].AddParagraph(month);
                dataRow.Cells[1].AddParagraph(totalHours != 0 ? totalHours.ToString() : "-");

                if (DateTime.TryParseExact(month, "MMMM", null, System.Globalization.DateTimeStyles.None, out DateTime monthDate))
                    if (monthDate.Month == GlobalVariables.date.Month && monthDate.Year == GlobalVariables.date.Year)
                        dataRow.Shading.Color = Colors.LightGreen;
            }

            // Přidání prádného paragrafu
            Paragraph empty = section.AddParagraph();

            // Přidání textu s celkovým počtem hodin za aktuální měsíc
            Paragraph totalHoursPerMonthText = section.AddParagraph();
            totalHoursPerMonthText.AddFormattedText("Celkový počet hodin za aktuální měsíc: ", TextFormat.Bold);
            totalHoursPerMonthText.AddText($"{totalHoursForActualMonth} hodin.");
            totalHoursPerMonthText.Format.Alignment = ParagraphAlignment.Right; // Zarovnání napravo

            // Přidání textu s celkovým počtem hodin za rok
            Paragraph totalHoursText = section.AddParagraph();
            totalHoursText.AddFormattedText("Celkový počet hodin za celý rok: ", TextFormat.Bold);
            totalHoursText.AddText($"{totalHoursForYear} hodin.");
            totalHoursText.Format.Alignment = ParagraphAlignment.Right; // Zarovnání napravo

            // Přidání prádného paragrafu
            empty = section.AddParagraph();
            empty.Format.SpaceAfter = "15cm";

            // Paragraf na Konci Stránky
            Paragraph endParagraph = section.AddParagraph();
            endParagraph.Format.Alignment = ParagraphAlignment.Right; // Zarovnání napravo
            endParagraph.AddDateField();
            if (initials != "")
                endParagraph.AddFormattedText($"\n{initials}");

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Uložit PDF soubor";
                saveFileDialog.FileName = $"WorkReport_" +
                    $"{GlobalVariables.date.Day}_" +
                    $"{GlobalVariables.date.Month}_" +
                    $"{GlobalVariables.date.Year}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pdfFileName = saveFileDialog.FileName;

                    PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                    renderer.Document = document;
                    renderer.RenderDocument();

                    renderer.PdfDocument.Save(pdfFileName);
                    Console.WriteLine("PDF soubor uložen jako " + pdfFileName);
                }
                else
                    Console.WriteLine("Uložení PDF souboru bylo zrušeno.");
            }

            return document;
        }
    }
}
