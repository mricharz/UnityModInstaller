# UnityAssetReplacer

## Development

You need .NET 4.7.1 installed.

Then checkout the project and open the *UnityModInstaller.sln* with *Visual Studio 2022*.
Install NuGet-Packages and Build the Project.

Thats it.

## Usage

You can get the finsihed *install.exe* from the Release-Page.

This installer needs to be configured with a *settings.yml*.
You can find a demo configuration in *data/demo-mod/*

## Documentation

### settings.yml

* *modName* Name of the mod (string)
* *author* author of the mod (string)
* *license* license this mod depends on (string)
* *selectFolderDescription* Descriptiontext for Choose-GameFolder-Dialog during installation (string)
* *initialDirectory* Path for preselecting the Game-Folder in Choose-GameFolder-Dialog during installation (string)
* *continueIfInitialDirectoryExists* skip *Choose-GameFolder-Dialog* if *initialDirectory* exsits (boolean)
* *assets* all asset tasks (array)
  * *file* target-assets-file filepath (string)
  * *replaceAssetsFromAssets* replace assets from a source-assets-file (array)
    * *file* source-assets-file filepath (string)
    * *replacements* list of replacements to be made in this assets file (array)
      * *source* name of the asset from source-assets-file
      * *target* name of the asset that will be overwritten in target-assets-file
      * *resourceFile* [optional] if provided, it will change the used resource-file for this asset. useful if your resource was newly added to the game. (use *copyFiles* for that)
* *copyFiles* list of copy jobs (array)
  * *source* source filepath relative to install.exe (string)
  * *target* target filepath relative to choosen Game-Folder (string)
  * *overwrite* overwrite if exists (boolean)