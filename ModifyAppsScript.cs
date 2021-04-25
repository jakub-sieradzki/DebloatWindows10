using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace DebloatWindows10
{
    class ModifyAppsScript
    {
        private BackgroundWorker backgroundWorker;
        private List<AppItem> appsToModify;
        private UpdateLayout updateLayout;
        private string modifyingAppName;
        private string mode;
        private List<Tuple<string, string>> errorList;

        public ModifyAppsScript(List<AppItem> appsToModify, string mode)
        {
            backgroundWorker = new BackgroundWorker();
            this.appsToModify = appsToModify;
            this.mode = mode;

            backgroundWorker.DoWork += RunPowerShellCommands;
            backgroundWorker.ProgressChanged += WorkerProgressChanged;
            backgroundWorker.RunWorkerCompleted += RunWorkerCompleted;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.RunWorkerAsync();

            errorList = new List<Tuple<string, string>>();
        }

        private void RunPowerShellCommands(object sender, DoWorkEventArgs e)
        {
            updateLayout = new UpdateLayout(0, appsToModify.Count);
            updateLayout.SetupPercentText();
            updateLayout.SetupProgressBar();
            updateLayout.SetEnabledModifyButton(false);
            updateLayout.SetEnabledSelectAllButton(false);
            updateLayout.SetEnabledList(false);

            if (appsToModify.Count > 0)
            {
                updateLayout.UpdateProgress(0, appsToModify[0].DisplayName);
            }

            for (int i = 0; i < appsToModify.Count; i++)
            {
                modifyingAppName = appsToModify[i].DisplayName;

                var powerShellProcess = new Process();

                powerShellProcess.StartInfo.UseShellExecute = false;
                powerShellProcess.StartInfo.RedirectStandardOutput = true;
                powerShellProcess.StartInfo.CreateNoWindow = true;

                powerShellProcess.StartInfo.FileName = "powershell.exe";
                powerShellProcess.StartInfo.Arguments = getPowerShellCommand(appsToModify[i]);

                powerShellProcess.Start();
                powerShellProcess.WaitForExit();

                string error = powerShellProcess.StandardOutput.ReadToEnd();
                if(error != "")
                {
                    errorList.Add(new Tuple<string, string>(appsToModify[i].DisplayName, error));
                }

                backgroundWorker.ReportProgress(i + 1);
            }

            updateLayout.UpdateList(mode);
            updateLayout.SetEnabledModifyButton(true);
            updateLayout.SetEnabledSelectAllButton(true);
            updateLayout.SetEnabledList(true);
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            updateLayout.UpdateProgress(e.ProgressPercentage, modifyingAppName);
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(errorList.Count > 0)
            {
                string errorStartText = "";
                if(mode == "uninstall")
                {
                    errorStartText = "There was an error while uninstalling app ";
                }
                else if(mode == "restore")
                {
                    errorStartText = "There was an error while restoring app ";
                }

                string errorText = "";
                for (int i = 0; i < errorList.Count; i++)
                {
                    errorText += errorStartText + errorList[i].Item1 + "\nDetails:\n\n";
                    errorText += errorList[i].Item2 + "\n\n";
                }

                MessageBox.Show(errorText, "Error");
            }
        }

        private string getPowerShellCommand(AppItem package)
        {
            if (mode == "uninstall")
            {
                return "Get-AppxPackage " + package.Name + " | Remove-AppxPackage";
            }
            else if (mode == "restore")
            {
                return "Add-AppxPackage -register \"" + @"$env:homedrive\Program` Files\WindowsApps\" + package.PackageFullName + @"\appxmanifest.xml" + "\" -DisableDevelopmentMode";
            }

            return "";
        }
    }
}
