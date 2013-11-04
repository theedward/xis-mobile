using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace XISMobileEAPlugin
{
    public partial class CodeGenerationForm : Form
    {
        string platformType = null;
        const string noPath = "Select a folder...";
        private EA.Repository repository;

        private delegate string ExecuteCommandDelegate(string command);

        public CodeGenerationForm(EA.Repository repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                textBoxPath.Text = folderBrowserDialog1.SelectedPath;
                errorProvider.SetError(textBoxPath, string.Empty);
            }
        }

        private void comboBoxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            platformType = comboBoxTarget.SelectedItem as String;
            errorProvider.SetError(comboBoxTarget, string.Empty);
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            bool valid = true;

            /*if (textBoxPath.Text != noPath)
            {
                errorProvider.SetError(textBoxPath, string.Empty);
            }
            else
            {
                errorProvider.SetError(textBoxPath, "A Destination folder must be specified!");
                valid = false;
            }

            if (!string.IsNullOrEmpty(platformType))
            {
                errorProvider.SetError(comboBoxTarget, string.Empty);
            }
            else
            {
                errorProvider.SetError(comboBoxTarget, "A Target must be specified!");
                valid = false;
            }*/
            
            textBoxPath.Text = "C:\\Users\\User\\Desktop";

            if (valid)
            {
                EA.Project project = repository.GetProjectInterface();
                EA.Package package = (EA.Package)repository.Models.GetAt(0);
                string[] res = repository.ConnectionString.Split('\\');
                string projectName = res[res.Length - 1].Split('.')[0];
                string xmiPath = textBoxPath.Text + "\\" + projectName + ".xmi";
                string umlPath = textBoxPath.Text + "\\" + projectName + ".uml";

                project.ExportPackageXMI(package.PackageGUID, EA.EnumXMIType.xmiEA21, 1, -1, 1, 0, xmiPath);

                //ExecuteCommandDelegate del = new ExecuteCommandDelegate(StringToUpperFirst);
                //del.BeginInvoke("test", PrintResult, null);

                ExecuteCommand("C:\\Users\\User\\Desktop\\XMLParser.jar " + xmiPath + " " + projectName);
                ExecuteCommand("C:\\Users\\User\\Desktop\\generator.jar " + umlPath + " " + textBoxPath.Text + "\\src-gen2");
                Close();
            }
        }

        private void PrintResult(IAsyncResult result)
        {
            ExecuteCommandDelegate del = (ExecuteCommandDelegate)((AsyncResult)result).AsyncDelegate;
            string s = del.EndInvoke(result);
            MessageBox.Show("Result: " + s);
            Close();
        }

        private string StringToUpperFirst(string s)
        {
            return s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length-1);
        }

        private void ExecuteCommand(string command)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("java.exe", "-jar " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            Process process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            int exitCode = process.ExitCode;

            MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            //Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
            process.Close();
        }
    }
}
