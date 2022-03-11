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
* *author* - (string)[optional] - author of the mod
* *license* - (string)[optional] - license this mod depends on
* *selectFolderDescription* - (string) - Descriptiontext for Choose-GameFolder-Dialog during installation
* *initialDirectory* - (string)[optional] - Path for preselecting the Game-Folder in Choose-GameFolder-Dialog during installation
* *continueIfInitialDirectoryExists* - (boolean)[optional] - skip *Choose-GameFolder-Dialog* if *initialDirectory* exsits
* *assets* - (array)[optional] - all asset tasks
  * *file* - (string) - target-assets-file filepath
  * *replaceAssetsFromAssets* - (array) - replace assets from a source-assets-file
    * *file* - (string) - source-assets-file filepath
    * *replacements* - (array) - list of replacements to be made in this assets file
      * *source* - (string) - name of the asset from source-assets-file
      * *target* - (string) - name of the asset that will be overwritten in target-assets-file
      * *resourceFile* - (string)[optional] - if provided, it will change the used resource-file for this asset. useful if your resource was newly added to the game. (use *copyFiles* for that)
* *copyFiles* - (array)[optional] - list of copy jobs
  * *source* - (string) - source filepath relative to install.exe
  * *target* - (string) - target filepath relative to choosen Game-Folder
  * *overwrite* - (boolean)[optional] - overwrite if exists

## New features needed / Bugs found / Refactoring needed?

You're welcome to:
- Open an issue...
- Or do it on your own and send a PR.

## Want to create a mod on your own?

Here are a list of useful links as a starting point:

* [AssetStudio](https://github.com/Perfare/AssetStudio) - Explore Unity Bundles and/or Assets
* [Unity Asset Bundle Extractor](https://community.7daystodie.com/topic/1871-unity-assets-bundle-extractor/) - Extract Unity Assets
* [How to modify Sound Files](https://www.youtube.com/watch?v=5rk8bj7uvws) - Good starting point to understand how to replace assets in unity game

### Questions?

Reach me at discord: [Valtos#3367](https://discordapp.com/channels/Valtos#3367)

## Credits

I'm using the awesome lib from [nesrak1](https://github.com/nesrak1): [AssetsTools.NET](https://github.com/nesrak1/AssetsTools.NET)