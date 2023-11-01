using System;
using System.Collections.Generic;
using BetterConsoleTables;

namespace JobHoursPerMonth
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> monthlyData = new Dictionary<string, List<string>>();

            WorkData workData = new WorkData();
            monthlyData = workData.GetData();

            if (monthlyData != null)
            {
                double totalHoursForActualMonth = 0;
                double totalHoursForYear = 0;

                try
                {
                    var table = new BetterConsoleTables.Table("Měsíc", "Celkový počet hodin za měsíc");

                    DateTime currentDate = GlobalVariables.date;
                    string currentMonthName = currentDate.ToString("MMMM", new System.Globalization.CultureInfo("cs-CZ"));

                    foreach (var pair in monthlyData)
                    {
                        string month = pair.Key;
                        List<string> data = pair.Value;


                        double totalHours = 0;
                        foreach (string item in data)
                        {
                            if (double.TryParse(item.Split(' ')[0], out double parsedHours))
                            {
                                totalHours += parsedHours;
                            }
                        }

                        totalHoursForYear += totalHours;

                        if (month.ToLower() == currentMonthName)
                            totalHoursForActualMonth += totalHours;

                        table.AddRow(month, totalHours);
                    }

                    table.Config = TableConfiguration.Unicode();

                    Console.WriteLine("Záznamy za rok: " + GlobalVariables.date.Year);
                    string tableText = table.ToString();
                    Console.WriteLine(tableText);
                    Console.WriteLine($"Celkový počet hodin za tento měsíc: {totalHoursForActualMonth}.");
                    Console.WriteLine($"Celkový počet hodin za celý rok: {totalHoursForYear}.");
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.ToString());
                }

                // --------------------------------------------------------------------------------
                // --------------------------------------------------------------------------------
                // --------------------------------------------------------------------------------

                try
                {
                    string initials = "";
                    Console.Write("\nZadejte svoje Příjmení a Jméno: ");
                    initials = Console.ReadLine();

                    Console.WriteLine();
                    PDFController document = new PDFController();
                    document.CreatePdfDocument(monthlyData, totalHoursForActualMonth, totalHoursForYear, initials);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.ToString());
                }
            }
            else
                Console.WriteLine("Proces byl přerušeno.");

            Console.ReadKey();
        }
    }
}
