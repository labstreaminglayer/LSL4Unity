# LSL4Unity

LSL4Unity was originally created by @xfleckx several years ago. That version has been [archived](https://github.com/labstreaminglayer/LSL4Unity/releases/tag/archive).

LSL4Unity is reborn as a [Unity Custom Package](https://docs.unity3d.com/Manual/CustomPackages.html).

The package comprises:

* [liblsl-Csharp](https://github.com/labstreaminglayer/liblsl-Csharp) as a [Unity Native plug-in](https://docs.unity3d.com/Manual/NativePlugins.html).
    * Has LSL.cs and shared object files for various platforms.
* General assets to facilitate using liblsl in Unity.
* Simple Editor Integration to lookup LSL streams. 
* Samples to show basic functionality, including continuous inlets, outlets, and event outlets.

## Adding to your project

### Option 1 - From the Editor

Open the Package Manager Window, click on the `+` dropdown, and [choose `Add package from git URL...`](https://docs.unity3d.com/Manual/upm-ui-giturl.html). Enter the followingURL: 

`https://github.com/labstreaminglayer/LSL4Unity.git`

### Option 2 - Modify Project Package Manifest

Edit `<your project folder>/Packages/manifest.json` and [add the following to your dependencies section](https://docs.unity3d.com/2020.3/Documentation/Manual/upm-git.html):

`"com.LSL.LSL4Unity": "https://github.com/labstreaminglayer/LSL4Unity.git"`

The advantage of this approach is that it will allow you to specify this package as a dependency of another package.

### (Future) Option 3 - Using OpenUPM

TODO - https://openupm.com/docs/

## Using LSL4Unity

The easiest way is to use the Package Manager window, select the LSL4Unity package, and choose to install one or more Samples. Inspect the Samples to learn more.
