using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

            //monthlyData["Leden"] = new List<string> { };
            //monthlyData["Únor"] = new List<string> { };
            //monthlyData["Březen"] = new List<string> { };
            //monthlyData["Duben"] = new List<string> { };
            //monthlyData["Květen"] = new List<string> { };
            //monthlyData["Červen"] = new List<string> { };
            //monthlyData["Červenen"] = new List<string> { };
            //monthlyData["Srpen"] = new List<string> { };
            //monthlyData["Září"] = new List<string> { };

            //monthlyData["Říjen"] = new List<string>
            //{
            //    "2,5 hodin",
            //    "2 hodiny",
            //    "1 hodina",
            //    "3 hodiny",
            //    "4,5 hodiny",
            //    "3 hodiny",
            //    "2 hodiny",
            //    "3 hodiny",
            //    "1 hodina",
            //    "4 hodiny",
            //    "1,5 hodina"
            //};

            //monthlyData["Listopad"] = new List<string> { };
            //monthlyData["Prosinec"] = new List<string> { };

            if (monthlyData != null)
            {
                var table = new BetterConsoleTables.Table("Měsíc", "Celkový počet hodin za měsíc");

                DateTime currentDate = GlobalVariables.date;
                string currentMonthName = currentDate.ToString("MMMM", new System.Globalization.CultureInfo("cs-CZ"));

                double totalHoursForActualMonth = 0;
                double totalHoursForYear = 0;
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

                // --------------------------------------------------------------------------------
                // --------------------------------------------------------------------------------
                // --------------------------------------------------------------------------------

                string initials = "";
                Console.Write("\nZadejte svoje Příjmení a Jméno: ");
                initials = Console.ReadLine();

                Console.WriteLine();
                PDFController document = new PDFController();
                document.CreatePdfDocument(monthlyData, totalHoursForActualMonth, totalHoursForYear, initials);
            }
            else
                Console.WriteLine("Něco se pokazilo");

            Console.ReadKey();
        }
    }
}
