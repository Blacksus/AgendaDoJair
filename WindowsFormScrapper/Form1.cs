using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormScrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime diaParaPegarAgenda = new DateTime(2021, 1, 1).Date;
            DateTime hoje = DateTime.Now.Date;
            while (diaParaPegarAgenda < hoje)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.gov.br/planalto/pt-br/acompanhe-o-planalto/agenda-do-presidente-da-republica/json/{diaParaPegarAgenda.ToString("yyyy-MM-dd")}");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var dadosAgendaDia = JsonSerializer.Deserialize<List<RetornoAPIJsonAgenda>>(content);

                //InserirDadosDia(dadosAgendaDia.Where(d => d.isSelected && d.hasAppointment));

                diaParaPegarAgenda.AddDays(1);
            }
        }
    }
}
