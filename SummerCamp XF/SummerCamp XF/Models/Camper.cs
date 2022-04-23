﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SummerCamp_XF.Models
{
    public class Camper
    {
        public int ID { get; set; }

        [JsonIgnore]
        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }

        [JsonIgnore]
        public string FormalName
        {
            get
            {
                return LastName + ", " + FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? "" :
                        (" " + (char?)MiddleName[0] + ".").ToUpper());
            }
        }

        [JsonIgnore]
        public string Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int a = today.Year - DOB.Year
                    - ((today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0));
                return a.ToString(); /*Note: You could add .PadLeft(3) but spaces disappear in a web page. */
            }
        }

        [JsonIgnore]
        public string PhoneFormatted
        {
            get
            {
                return "(" + Phone.Substring(0, 3) + ") " + Phone.Substring(3, 3) + "-" + Phone.Substring(6);
            }
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public byte[] RowVersion { get; set; }

        public int CompoundID { get; set; }
        public Compound Compound { get; set; }

    }
}
