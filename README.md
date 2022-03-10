# UnityAssetReplacer

## Development

You need [.NET 4.7.1](https://support.microsoft.com/de-de/topic/microsoft-net-framework-4-7-1-offline-installer-f%C3%BCr-windows-2a7d0d5e-92f2-b12d-aed4-4f5d14c8ef0c) installed.

Then checkout the project and open the *UnityModInstaller.sln* with [*Visual Studio 2022*](https://visualstudio.microsoft.com/de/downloads/).
Install NuGet-Packages and Build the Project.

Thats it.

## Usage

You can get the finsihed *install.exe* from the [Release-Page](https://github.com/mricharz/UnityModInstaller/releases).

This installer needs to be configured with a *settings.yml*.
You can find a demo configuration in [*data/demo-mod/*](https://github.com/mricharz/UnityModInstaller/tree/main/data/demo-mod)

## Documentation

### settings.yml

* *modName* - (string) - Name of the mod
* *author* - (string) - author of the mod
* *license* - (string) - license this mod depends on
* *selectFolderDescription* - (string) - Descriptiontext for Choose-GameFolder-Dialog during installation
* *initialDirectory* - (string) - Path for preselecting the Game-Folder in Choose-GameFolder-Dialog during installation
* *continueIfInitialDirectoryExists* - (boolean) - skip *Choose-GameFolder-Dialog* if *initialDirectory* exsits
* *assets* - (array) - all asset tasks
  * *file* - (string) - target-assets-file filepath
  * *replaceAssetsFromAssets* - (array) - replace assets from a source-assets-file
    * *file* - (string) - source-assets-file filepath
    * *replacements* - (array) - list of replacements to be made in this assets file
      * *source* - (string) - name of the asset from source-assets-file
      * *target* - (string) - name of the asset that will be overwritten in target-assets-file
      * *resourceFile* - (string)[optional] - if provided, it will change the used resource-file for this asset. useful if your resource was newly added to the game. (use *copyFiles* for that)
* *copyFiles* - (array) - list of copy jobs
  * *source* - (string) - source filepath relative to install.exe
  * *target* - (string) - target filepath relative to choosen Game-Folder
  * *overwrite* - (boolean) - overwrite if exists