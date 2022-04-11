using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace UnityModInstaller.Models {
    public class Settings : Reportable {
        public string modName = "NoName";
        public string author;
        public string license;
        public string selectFolderDescription;
        public string initialDirectory;
        public bool continueIfInitialDirectoryExists;
        public List<SettingsAssets> assets;
        public List<CopyFiles> copyFiles;

        public void execute(string workingDir, string applicationDir) {
            increaseProgressItems(assets.Count());
            increaseProgressItems(copyFiles.Count());

            log($"{assets.Count()} assets to modify:");
            assets.ForEach(item => {
                try {
                    item.setReportHandler(addLogEntry, updateProgressBar, increaseProgressBarMax);
                    item.execute(workingDir, applicationDir);
                } catch (IOException ioEx) {
                    error($"Modifing assets in \"{item.file}\" failed! {ioEx.Message}");
                } catch (Exception ex) {
                    error($"Modifing assets in \"{item.file}\" failed! {ex.Message}");
                    Console.WriteLine(ex);
                }
                performStep();
            });

            log($"{copyFiles.Count()} files to copy:");
            copyFiles.ForEach(item => {
                try {
                    item.setReportHandler(addLogEntry, updateProgressBar, increaseProgressBarMax);
                    item.execute(workingDir, applicationDir);
                } catch (IOException ioEx) {
                    error($"Copying file \"{item.source}\" failed! {ioEx.Message}");
                } catch (Exception ex) {
                    error($"Copying file \"{item.source}\" failed! {ex.Message}");
                    Console.WriteLine(ex);
                }
                performStep();
            });
        }
    }
}
