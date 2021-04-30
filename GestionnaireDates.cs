using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviFrais
{
    public abstract class GestionnaireDates
    {
        TimeZoneInfo _timeZoneInfo;
        public GestionnaireDates(TimeZoneInfo timeZoneInfo)
        {
            _timeZoneInfo = timeZoneInfo;       
        }



        public static string getMoisPrecedentFromNow()
        {
            var date = DateTime.Now.AddMonths(-1);
            string previousMonth = date.Month.ToString();
            //System.Console.WriteLine("Voici le mois précédent :" + previousMonth);
            if (previousMonth.Length == 2)
                return previousMonth;
            previousMonth = "0" + previousMonth;
            return previousMonth;

        }

        public static string getMoisPrecedent(DateTime uneDate)
        {
            var date = uneDate.AddMonths(-1);
            string previousMonth = date.Month.ToString();
            //System.Console.WriteLine("Voici le mois précédent :" + previousMonth);
            if (previousMonth.Length == 2)
                return previousMonth;
            previousMonth = "0" + previousMonth;
            return previousMonth;
            
        }



        public static string getMoisSuivantFromNow()
        {
            var date = DateTime.Now.AddMonths(1);
            string previousMonth = date.Month.ToString();
            //System.Console.WriteLine("Voici le mois précédent :" + previousMonth);
            if (previousMonth.Length == 2)
                return previousMonth;
            previousMonth = "0" + previousMonth;
            return previousMonth;

        }

        public static string getMoisSuivant(DateTime uneDate)
        {
            var date = uneDate.AddMonths(1);
            string previousMonth = date.Month.ToString();
            //System.Console.WriteLine("Voici le mois précédent :" + previousMonth);
            if (previousMonth.Length == 2)
                return previousMonth;
            previousMonth = "0" + previousMonth;
            return previousMonth;

        }


        public static bool entre( string unJour , string autreJour)
        {
            var date = DateTime.Now.Day.ToString();
            string premierJour = unJour;
            string deuxiemeJour = autreJour;

            int premierJourInt =0;
            int deuxiemeJourInt = 0;
            int dateInt = 0;

            try 
            {
                premierJourInt = Int32.Parse(premierJour);
                deuxiemeJourInt = Int32.Parse(autreJour);
                dateInt = Int32.Parse(date);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }



            return dateInt >= premierJourInt && dateInt <= deuxiemeJourInt;
        }




        public static bool entreSurchage(string unJour, string autreJour, DateTime uneDate)
        {
            var date = uneDate.Day.ToString();
            string premierJour = unJour;
            string deuxiemeJour = autreJour;

            int premierJourInt = 0;
            int deuxiemeJourInt = 0;
            int dateInt = 0;

            try
            {
                premierJourInt = Int32.Parse(premierJour);
                deuxiemeJourInt = Int32.Parse(autreJour);
                dateInt = Int32.Parse(date);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }



            return dateInt >= premierJourInt && dateInt <= deuxiemeJourInt;
        }
    }

}
