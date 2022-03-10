using AssetsTools.NET.Extra;
using System;
using System.IO;

namespace UnityModInstaller.Models {
    public class Assets : Reportable {

        protected AssetsManager am = new AssetsManager();
        protected AssetsFileInstance sourceAssetFile;

        public string file;

        public void loadAssetsFile(string workingDir) {
            string filePath = Path.Combine(workingDir, file);
            try {
                Stream classdataStream = new MemoryStream(Properties.Resources.classdata);
                am.LoadClassPackage(classdataStream);
                sourceAssetFile = am.LoadAssetsFile(filePath, true);
                am.LoadClassDatabaseFromPackage(sourceAssetFile.file.typeTree.unityVersion);
            } catch(Exception ex) {
                error($"Could not load {filePath}");
                throw ex;
            }
        }

    }
}
