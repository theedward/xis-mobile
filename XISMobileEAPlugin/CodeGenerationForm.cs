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
using System.Reflection;

namespace XISMobileEAPlugin
{
    public partial class CodeGenerationForm : Form
    {
        string platformType = null;
        const string noPath = "Select a folder...";
        private EA.Repository repository;
        private bool GENERATE = true;

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

            if (textBoxPath.Text != noPath)
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
            }

            if (valid)
            {
                EA.Project project = repository.GetProjectInterface();
                EA.Package package = (EA.Package)repository.Models.GetAt(0);
                string[] res = repository.ConnectionString.Split('\\');
                string projectName = res[res.Length - 1].Split('.')[0];
                string xmiPath = textBoxPath.Text + "\\" + projectName + ".xmi";
                string umlPath = textBoxPath.Text + "\\" + projectName + ".uml";

                if (GENERATE)
                {
                    project.ExportPackageXMI(package.PackageGUID, EA.EnumXMIType.xmiEA21, 1, -1, 1, 0, xmiPath);
                    string exePath = "\"" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    ExecuteCommand(exePath + "\\XMLParser.jar\" " + xmiPath + " " + projectName);

                    switch (platformType)
                    {
                        case "Android":
                            ExecuteCommand(exePath + "\\AndroidGenerator.jar\" " + umlPath + " " + textBoxPath.Text + "\\src-gen");
                            break;
                        case "Windows Phone":
                            //ExecuteCommand(exePath + "\\WPGenerator.jar\" " + umlPath + " " + textBoxPath.Text + "\\src-gen");
                            break;
                        case "iOS":
                            ExecuteCommand(exePath + "\\iOSGenerator.jar\" " + umlPath + " " + textBoxPath.Text + "\\src-gen");
                            break;
                        case "All":
                            ExecuteCommand(exePath + "\\AndroidGenerator.jar\" " + umlPath + " " + textBoxPath.Text + "\\android\\src-gen");
                            //ExecuteCommand(exePath + "\\WPGenerator.jar\" " + umlPath + " " + textBoxPath.Text + "\\wp\\src-gen");
                            ExecuteCommand(exePath + "\\iOSGenerator.jar\" " + umlPath + " " + textBoxPath.Text + "\\ios\\src-gen");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Code Generation disabled for now. It is being updated for the next version!",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Close();
            }
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

            MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            process.Close();
        }
    }
}
