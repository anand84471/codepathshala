using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentDashboard.Utilities
{
    public static class MasterUtilities
    {
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
        public static FileInfo GetFileInfo(string FilePath, int FilePathType)
        {
            FileInfo fiLeInfo = null;
            switch (FilePathType)
            {
                case (int)Constants.FilePathTypeId.ABSOLUTE_PATH:
                    {
                        fiLeInfo = new FileInfo(FilePath);
                        break;
                    }
                case (int)Constants.FilePathTypeId.WRT_SERVER:
                    {
                        FilePath = HttpContext.Current.Server.MapPath(FilePath);
                        fiLeInfo = new FileInfo(FilePath);
                        break;
                    }
            }
            return fiLeInfo;
        }
        public static string GetFileExtensionName(string FilePath,int FilePathType)
        {
            return GetFileInfo(FilePath, FilePathType).Extension;
        }
        public static string GetFileName(string FilePath, int FilePathType)
        {
            return GetFileInfo(FilePath, FilePathType).Name;
        }
        public static string GetDirectoryOfFile(string FilePath, int FilePathType)
        {
            return GetFileInfo(FilePath, FilePathType).DirectoryName;
        }
        public static string GetPhysicalPath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }
            return HttpContext.Current.Server.MapPath(path);
        }
        public static string GetDateByDateTime(DateTime? datetime )
        {
            if (datetime != null)
            {
                return ((DateTime)datetime).ToString("d MMM yyyy");
            }
            else
            {
                return "";
            }
        }
        public static string GetDateByDateTimeYYYYMMDD(DateTime? datetime)
        {
            if (datetime != null)
            {
                return ((DateTime)datetime).ToString("yyyy-MM-dd");
            }
            else
            {
                return "";
            }
        }
        public static bool CompareToToday(DateTime? datetime)
        {
            bool result = false;
            try
            {
                if (datetime != null)
                {
                    result=(DateTime)datetime<DateTime.Now;
                }
            }
            catch(Exception Ex)
            {
                
            }
            return result;

        }
        public static TimeScheduleDetails GetTimer(TimeSpan time)
        {
            TimeScheduleDetails timeScheduleDetails = new TimeScheduleDetails();
            try
            {
                timeScheduleDetails.days = (int)time.Days;
                timeScheduleDetails.hours = (int)time.Hours;
                timeScheduleDetails.minutes= (int)time.Minutes;
                timeScheduleDetails.seconds = (int)time.Seconds;
            }
            catch(Exception Ex)
            {
                throw;
            }
            return timeScheduleDetails;
        }
        public static float GetPercentage(float number,float maxNumber)
        {
            return ((int)(100*(100*number / maxNumber)))/100;
        }
    }
}