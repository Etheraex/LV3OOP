using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabV3OOP
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (StreamWriter file = new StreamWriter("../../../data/info.txt"))
            {
                file.WriteLine(txtRedovi.Text);
                file.WriteLine(txtKolone.Text);
                file.WriteLine(txtParovi.Text);
                file.WriteLine(txtSlike.Text);
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
