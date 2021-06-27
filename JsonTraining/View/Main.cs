using CoronaDetails.Business;
using System;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoronaDetails
{
    public partial class Form1 : Form
    {
        string lstUrls = "https://api.codebazan.ir/corona/";
        Root CasesData = new Root();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bgwGetJson.RunWorkerAsync();
        }

        private void fncFillConuntries()
        {
            cmbCountries.Items.Add("...انتخاب کشور");
            for (int i = 0; i < CasesData.entries.Count; i++)
            {
                cmbCountries.Items.Add(CasesData.entries[i].country);
            }
        }

        private void bgwGetJson_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {               
                string strResult = httpClient.GetStringAsync(lstUrls).Result;
                CasesData = JsonConvert.DeserializeObject<Root>(strResult);
            }
        }

        private void bgwGetJson_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            fncFillConuntries();
            label2.Visible = false;
            cmbCountries.Visible = true;
            label1.Visible = true;
            groupBox1.Visible = true;
            cmbCountries.SelectedIndex = 0;
        }

        private void cmbCountries_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbCountries.SelectedIndex != 0)
            {
                lblDeaths.Text = CasesData.entries[cmbCountries.SelectedIndex + 1].deaths;
                lblPatiant.Text = CasesData.entries[cmbCountries.SelectedIndex + 1].cases;
                lblRecoverd.Text = CasesData.entries[cmbCountries.SelectedIndex + 1].recovered;
            }
            else
            {
                lblDeaths.Text = "0";
                lblPatiant.Text = "0";
                lblRecoverd.Text = "0";
            } 

            
        }
    }
}
