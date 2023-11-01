using System.Collections.Generic;

namespace JobHoursPerMonth
{
    internal class WorkData
    {
        public Dictionary<string, List<string>> yearData;

        public WorkData()
        {
            yearData = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> GetData()
        {
            int date = GlobalVariables.date.Year;

            if (date == 2023)
                return getDataFrom2023();

            else if (date == 2024)
                return getDataFrom2024();

            else return null;
        }

        Dictionary<string, List<string>> getDataFrom2023()
        {
            yearData["Leden"] = new List<string> { };
            yearData["Únor"] = new List<string> { };
            yearData["Březen"] = new List<string> { };
            yearData["Duben"] = new List<string> { };
            yearData["Květen"] = new List<string> { };
            yearData["Červen"] = new List<string> { };
            yearData["Červenen"] = new List<string> { };
            yearData["Srpen"] = new List<string> { };
            yearData["Září"] = new List<string> { };

            yearData["Říjen"] = new List<string>
            {
                "2,5 hodin", "2 hodiny", "1 hodina", "3 hodiny", "4,5 hodiny",
                "3 hodiny", "2 hodiny", "3 hodiny", "1 hodina", "4 hodiny",
                "1,5 hodina"
            };

            yearData["Listopad"] = new List<string>
            {
                "2 hodiny"
            };

            yearData["Prosinec"] = new List<string> { };

            return yearData;
        }

        // Záznamy za rok 2024
        Dictionary<string, List<string>> getDataFrom2024()
        {
            yearData["Leden"] = new List<string> { };
            yearData["Únor"] = new List<string> { };
            yearData["Březen"] = new List<string> { };
            yearData["Duben"] = new List<string> { };
            yearData["Květen"] = new List<string> { };
            yearData["Červen"] = new List<string> { };
            yearData["Červenen"] = new List<string> { };
            yearData["Srpen"] = new List<string> { };
            yearData["Září"] = new List<string> { };
            yearData["Říjen"] = new List<string> { };
            yearData["Listopad"] = new List<string> { };
            yearData["Prosinec"] = new List<string> { };

            return yearData;
        }
    }
}
