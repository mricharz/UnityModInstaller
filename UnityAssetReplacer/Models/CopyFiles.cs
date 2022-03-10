using System;
using System.IO;

namespace UnityModInstaller.Models {
    public class CopyFiles : Reportable {
        public string source;
        public string target;
        public bool overwrite;

        public void execute(string workingDir, string applicationDir) {
            var sourceFilePath = Path.Combine(applicationDir, source);
            var targetFilePath = Path.Combine(workingDir, target);
            if (File.Exists(targetFilePath)) {
                if (!overwrite) {
                    info($"Did not copy {target} because it already exists and is not set as \"overwrite:true\"");
                    return;
                }
                int backupCounter = 0;
                while (File.Exists(targetFilePath + ".backup." + backupCounter)) {
                    backupCounter++;
                }
                File.Move(targetFilePath, targetFilePath + ".backup." + backupCounter);
            }
            File.Copy(sourceFilePath, targetFilePath);
            log($"Copied {source} to {target}");
        }
    }
}