using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin
{
    public partial class ModelGenerationForm : Form
    {
        string patternType = null;

        public ModelGenerationForm()
        {
            InitializeComponent();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            bool valid = true;

            if (!string.IsNullOrEmpty(patternType))
            {
                errorProvider.SetError(comboBoxPatterns, string.Empty);
            }
            else
            {
                errorProvider.SetError(comboBoxPatterns, "A Pattern must be specified!");
                valid = false;
            }

            if (valid)
            {
                MessageBox.Show("Valid");
                // TODO: Generate Models
            }
        }

        private void comboBoxPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            patternType = comboBoxPatterns.SelectedItem as String;
            errorProvider.SetError(comboBoxPatterns, string.Empty);
        }
    }
}
