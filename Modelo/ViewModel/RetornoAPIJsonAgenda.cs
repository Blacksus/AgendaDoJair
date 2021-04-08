using System;
using System.Collections.Generic;

namespace Domain.ViewModel
{
    public class RetornoAPIJsonAgenda
    {
        public bool hasAppointment { get; set; }
        public DateTime datetime { get; set; }
        public bool isSelected { get; set; }
        public string weekday { get; set; }
        public int day { get; set; }

        public List<Compromisso> items { get; set; }
    }

    public class Compromisso
    {
        public string title { get; set; }
        public DateTime datetime { get; set; }
        public string start { get; set; }
        public string href { get; set; }
        public string location { get; set; }
    }
}
