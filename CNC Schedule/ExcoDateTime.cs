using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using Databases;

namespace CNC_Schedule
{
    class ExcoDateTime
    {
        public List<string> Holidays = new List<string>();

        public void Set_Holidays()
        {
            string query = "select holiday_year, holiday_month, holiday_date from d_holiday_new";
            ExcoODBC database = ExcoODBC.Instance;
            OdbcDataReader reader;
            database.Open(Database.DECADE_MARKHAM);
            reader = database.RunQuery(query);
            while (reader.Read())
            {
                Holidays.Add(reader[1] + "/" + reader[2] + "/" + reader[0]);
            }
            reader.Close();
        }
        
        public DateTime AddWorkdays(DateTime originalDate, int workDays, int direction = 1)
        {
            Set_Holidays();

            DateTime tmpDate = originalDate;
            while (workDays > 0)
            {
                tmpDate = tmpDate.AddDays(1 * direction);
                if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                    tmpDate.DayOfWeek > DayOfWeek.Sunday &&
                    !IsHoliday(tmpDate))
                    workDays--;
            }
            return tmpDate;
        }

        public bool IsHoliday(DateTime originalDate)
        {
            bool holiday = false;
            foreach (string day in Holidays)
            {
                if (originalDate.Month.ToString() + "/" + originalDate.Day.ToString() + "/" + originalDate.Year.ToString() == day)
                    holiday = true;
            }
            return holiday;
        }

        // If due date is 6 days but 2 of the days occur on weekend, actual due date is 4 days which is what this function will return
        public double Get_Actual_Due_Date(DateTime nowDate, double originalDueDate, string Part_Type)
        {
            double newDD = (originalDueDate < 0 ? -1 * originalDueDate : originalDueDate);
            double originalDD = (originalDueDate < 0 ? -1 * originalDueDate : originalDueDate);
            DateTime tmpDate = nowDate;
            while (originalDD > 0)
            {
                if (tmpDate.DayOfWeek == DayOfWeek.Saturday ||
                    tmpDate.DayOfWeek == DayOfWeek.Sunday ||
                    IsHoliday(tmpDate))
                {
                    newDD--;
                }
                else
                {
                }
                tmpDate = tmpDate.AddDays(originalDueDate < 0 ? -1 : 1);
                //if (Part_Type == "M") 
                originalDD--;
            }
            return (originalDueDate < 0 ? -(newDD + (Part_Type == "M" ? 0.5 : 0)) : (newDD + (Part_Type == "M" ? 0.5 : 0)));
        }

    }
}
