using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HPF.SharePointAPI.Controllers;
using HPF.SharePointAPI.BusinessEntity;
using System.IO;

namespace HPF.SharePointAPIsDemo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {            
            ConselingSummaryInfo conselingSummary = new ConselingSummaryInfo();
            conselingSummary.CompletedDate = DateTime.Now;
            conselingSummary.Delinquency = HPF.SharePointAPI.Enum.Delinquency.From30To59;
            conselingSummary.File = File.ReadAllBytes(this.textBoxFilePath.Text);
            conselingSummary.ForeclosureSaleDate = DateTime.Now;
            conselingSummary.LoanNumber = 1;
            conselingSummary.Name = Path.GetFileName(this.textBoxFilePath.Text);
            conselingSummary.ReviewStatus = HPF.SharePointAPI.Enum.ReviewStatus.PendingReview;
            conselingSummary.Servicer = "HPF Servicer";
            DocumentCenterController.Upload(conselingSummary);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxFilePath.Text = this.openFileDialog1.FileName;
            }
        }
    }
}
