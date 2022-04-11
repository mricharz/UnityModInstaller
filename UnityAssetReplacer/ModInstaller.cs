using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityModInstaller.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace UnityModInstaller {
    public partial class ModInstaller : Form {

        private string finishedText = "Close";

        private string SettingsYamlFile = Path.Combine(Application.StartupPath, "settings.yml");

        private Settings settings;

        public ModInstaller() {
            InitializeComponent();

            // load settings.yml
            try {
                loadSettingsYaml();
                this.Text = settings.modName + " - Mod-Installer";

                listBox1.Items.Insert(0, "--------------------------");
                listBox1.Items.Insert(0, "");
                if (settings.license != null) {
                    listBox1.Items.Insert(0, $"License of Mod: {settings.license}");
                }
                if (settings.author != null) {
                    listBox1.Items.Insert(0, $"Author of Mod: {settings.author}");
                }
                listBox1.Items.Insert(0, "");
                listBox1.Items.Insert(0, "--------------------------");
                listBox1.Items.Insert(0, "");
                listBox1.Items.Insert(0, $"Click \"Install\" to install \"{settings.modName}\"-Mod.");
                
            } catch (Exception ex) {
                Console.WriteLine($"Could not load Settings from YAML: {ex.Message}");
                MessageBox.Show("Could not load Settings from YAML!");
                this.Shown += new EventHandler(CloseOnStart);
            }
        }

        private void CloseOnStart(object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            if(button1.Text == finishedText) {
                Application.Exit();
                return;
            }

            string workingDir = getWorkingDirectoryDialog();
            if (workingDir == null) {
                return;
            }

            var addLogEntry = new Progress<string>(message => {
                Console.WriteLine(message);
                listBox1.Items.Add(message);
                // scroll to bottom of listBox
                listBox1.TopIndex = listBox1.Items.Count - 1;
            });

            var updateProgressBar = new Progress<int>(i => {
                progressBar1.PerformStep();
            });
            var increaseProgressBarMax = new Progress<int>(amount => {
                progressBar1.Maximum += amount;
            });

            execute(workingDir, addLogEntry, updateProgressBar, increaseProgressBarMax);
        }

        private void loadSettingsYaml() {
            if (!File.Exists(SettingsYamlFile)) {
                SettingsYamlFile = getFilePathDialog("YAML file (*.yaml;*.yml)|*.yaml;*.yml|All files (*.*)|*.*", Application.StartupPath, "settings.yml");
            }
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            settings = deserializer.Deserialize<Settings>(File.ReadAllText(SettingsYamlFile));
        }

        private string getFilePathDialog(string filter = "unity assets files (*.assets)|*.assets|All files (*.*)|*.*", string initialDirectory = null, string initialSelectedFile = null) {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = filter;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = initialSelectedFile;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    return openFileDialog.FileName;
                }
            }
            return null;
        }

        private string getWorkingDirectoryDialog() {
            // skip dialog if initialDirectory exists and "continueIfInitialDirectoryExists" = true
            if (settings.continueIfInitialDirectoryExists && Directory.Exists(settings.initialDirectory)) {
                return settings.initialDirectory;
            }

            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog()) {
                dialog.InitialDirectory = settings.initialDirectory;
                dialog.Title = settings.selectFolderDescription;
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                    return Path.GetDirectoryName(dialog.FileName);
                }
            }

            return null;
        }
         
        private async void execute(string workingDir, IProgress<string> addLogEntry, IProgress<int> updateProgressBar, IProgress<int> increaseProgressBarMax) {
            button1.Enabled = false;
            progressBar1.Maximum = 1;
            settings.setReportHandler(addLogEntry, updateProgressBar, increaseProgressBarMax);
            await Task.Run(() => settings.execute(workingDir, new FileInfo(SettingsYamlFile).Directory.FullName));
            progressBar1.PerformStep();
            button1.Text = finishedText;
            button1.Enabled = true;
        }
    }
}
