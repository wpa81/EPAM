using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        private string lampOnYellow = "Y";
        private string lampOnRed = "R";
        private string lampOff = "O";

        private Regex rx = new Regex("^([0-1][0-9]|2[0-4]):([0-5][0-9]):([0-5][0-9])$");

        public string convertTime(string aTime)
        {
            if(rx.IsMatch(aTime))
            {
                var match = rx.Match(aTime);

                var hour = int.Parse(match.Groups[1].Value);
                var min = int.Parse(match.Groups[2].Value);
                var sec = int.Parse(match.Groups[3].Value);

                return GetSeconds(sec) + "\n" + GetTopHours(hour) + "\n" + GetBottomHours(hour)
                            + "\n" + GetTopMinutes(min) + "\n" + GetBottomMinutes(min);
            }
            else
                throw new FormatException("Format not suported yet.");
            
        }

        /// <summary>
        /// GetSeconds - Return the string for the first line of the clock
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private string GetSeconds(int seconds)
        {
            if (seconds % 2 == 0)
                return lampOnYellow;
            else
                return lampOff;
        }

        /// <summary>
        /// GetTopHours - Return the string for the second line of the clock
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        private string GetTopHours(int hours)
        {
            int onLamps = hours / 5;

            return GetLamps(4, onLamps, lampOnRed);
        }

        /// <summary>
        /// GetBottomHours - Return the string for the third line of the clock
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        private string GetBottomHours(int hours)
        {
            int onLamps = hours % 5 ;

            return GetLamps(4, onLamps, lampOnRed);
        }

        /// <summary>
        /// GetTopMinutes - Return the string for the fourth line of the clock
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        private string GetTopMinutes(int minutes)
        {
            int onLamps = minutes / 5;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < onLamps; i++)
            {
                sb.Append((i + 1) % 3 == 0 ? lampOnRed : lampOnYellow);
            }
            for (int i = 0; i < 11 - onLamps; i++)
            {
                sb.Append(lampOff);
            }

            return sb.ToString();
        }

        /// <summary>
        /// GetBottomMinutes - Return the string for the fifth (last) line of the clock
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        private string GetBottomMinutes(int minutes)
        {
            int onLamps = minutes % 5;

            return GetLamps(4, onLamps, lampOnYellow);
        }

        /// <summary>
        /// GetLamps -  Build a string of lamps based on the parameters
        /// </summary>
        /// <param name="lamps"></param>
        /// <param name="onLamps"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private string GetLamps(int lamps, int onLamps, string color)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < onLamps; i++)
            {
                sb.Append(color);
            }
            for (int i = 0; i < lamps-onLamps; i++)
            {
                sb.Append(lampOff);
            }

            return sb.ToString();
        }
    }
}
