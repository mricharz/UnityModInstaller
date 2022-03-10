using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityModInstaller.Models {
    public class Replacements {
        public string source;
        public string target;
        public string resourceFile;
    }

    public class ReplaceAssetsFromAssets : Assets {
        public List<Replacements> replacements;

        public void execute(string workingDir, string applicationDir, AssetsFileInstance targetAssetFile) {
            loadAssetsFile(applicationDir);

            log($"{replacements.Count()} replacements in \"{targetAssetFile.path}\"");

            var replacers = replacements.Select(replacement => {
                try {
                    // get target
                    var targetAssetInfo = targetAssetFile.table.GetAssetInfo(replacement.target);
                    if (targetAssetInfo == null) throw new KeyNotFoundException($"\"{replacement.target}\" not found in target!");

                    // get source
                    var sourceAssetInfo = sourceAssetFile.table.GetAssetInfo(replacement.source);
                    if (sourceAssetInfo == null) throw new KeyNotFoundException($"\"{replacement.source}\" not found in source!");

                    var sourceBaseField = am.GetTypeInstance(sourceAssetFile, sourceAssetInfo).GetBaseField();
                    // rename source to target name 
                    sourceBaseField.Get("m_Name").GetValue().Set(replacement.target);
                    // rename resource-file
                    if (replacement.resourceFile != null) {
                        sourceBaseField.Get("m_Resource").Get("m_Source").GetValue().Set(replacement.resourceFile);
                    }
                    // get source as byte[]
                    var sourceBytes = sourceBaseField.WriteToByteArray();

                    log($"Replacing \"{replacement.target}\" with \"{replacement.source}\"");
                    return new AssetsReplacerFromMemory(0, targetAssetInfo.index, (int)targetAssetInfo.curFileType, 0xffff, sourceBytes) as AssetsReplacer;
                } catch(Exception ex) {
                    error(ex.Message);
                    return null;
                }
            }).Where(item => item != null).ToList();

            writeToAssetsFile(targetAssetFile, replacers);

            am.UnloadAll();
        }

        private void writeToAssetsFile(AssetsFileInstance targetAssetFile, List<AssetsReplacer> replacers) {
            var targetFilePath = targetAssetFile.path + ".tmp";
            try {
                using (var stream = File.OpenWrite(targetFilePath))
                using (var writer = new AssetsFileWriter(stream)) {
                    targetAssetFile.file.Write(writer, 0, replacers, 0);
                }
                log($"Replaced {replacers.Count()} assets in {targetFilePath} from {file}");
            } catch (Exception ex) {
                error($"Could not write to file {targetFilePath}");
                throw ex;
            }
        }
    }
}
