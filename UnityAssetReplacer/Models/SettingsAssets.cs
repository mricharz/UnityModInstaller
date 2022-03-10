using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityModInstaller.Models {
    public class SettingsAssets : Assets {
        public List<ReplaceAssetsFromAssets> replaceAssetsFromAssets;

        public void execute(string workingDir, string applicationDir) {
            loadAssetsFile(workingDir);
            log($"{file} has {replaceAssetsFromAssets.Count()} sources to get new assets from:");
            increaseProgressItems(replaceAssetsFromAssets.Count() + 1);
            replaceAssetsFromAssets.ForEach(item => {
                try {
                    item.setReportHandler(addLogEntry, updateProgressBar, increaseProgressBarMax);
                    item.execute(workingDir, applicationDir, sourceAssetFile);
                } catch(Exception ex) {
                    error($"Source \"{item.file}\" failed!");
                }
                performStep();
            });
            am.UnloadAll();

            var tmpAssetsFilePath = sourceAssetFile.path + ".tmp";
            if (File.Exists(tmpAssetsFilePath)) {
                int backupCounter = 0;
                while(File.Exists(sourceAssetFile.path + ".backup." + backupCounter)) {
                    backupCounter++;
                }
                File.Move(sourceAssetFile.path, sourceAssetFile.path + ".backup." + backupCounter);
                log($"Created a backup of old assets-file at {sourceAssetFile.path + ".backup." + backupCounter}");
                File.Move(tmpAssetsFilePath, sourceAssetFile.path);
                log($"Successfully overwritten assets-file {sourceAssetFile.path}");
            }
            performStep();
        }
    }
}
