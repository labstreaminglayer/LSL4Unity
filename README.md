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

This option is preferred if this package will be a dependency of another package.

Edit `<your project folder>/Packages/manifest.json` and [add the following to your dependencies section](https://docs.unity3d.com/2020.3/Documentation/Manual/upm-git.html):

`"com.LSL.LSL4Unity": "https://github.com/labstreaminglayer/LSL4Unity.git"`

### (Future) Option 3 - Using OpenUPM

TODO - https://openupm.com/docs/

## Using LSL4Unity

The easiest way is to use the Package Manager window, select the LSL4Unity package, and choose to Import one or more of the Samples.

The samples appear in `Assets/labstreaminglayer for Unity/1.16.0/{sample name}`. Open the sample's scene.

### Simple Inlet Scale Object

This scene uses LSL.cs only, and not any of the Utilitis or helpers that come with LSL4Unity. Thus it demonstrates the minimum required steps to resolve, open, and pull data from an LSL Inlet in Unity.

You will need a separate process running an LSL outlet with at least 3 float channels. In an active Python environment with [`pylsl`](https://github.com/labstreaminglayer/liblsl-Python) installed, run `python -m pylsl.examples.SendDataAdvanced`.

The cylinder will update its scale according to the data coming in from the found stream.

### Simple Physics Event Outlet

This scene uses LSL.cs only, and shows the minimal amount of code to setup a Markers stream and send events.

A sphere oscillates back and forth. Whenever it enters or exits the collider of a nearby cube, an event is transmitted.

Note that this sample is designed to send out physics events (collisions) and thus the events are timestamped when the collision is detected in code.

If you wanted to instead timestamp stimulus events then it would be better to send out the Marker on `WaitForEndOfFrame`. There is an example of this in the Complex Sample scene.

### Complex Outlet Inlet Event

A capsule continuously streams its pose over an outlet. Occassionally (every 2 seconds by default), the position is reset and new velocities are applied to the capsule.

A cube has its pose set (+ a static offset) by the values coming in from an inlet, the very same stream that the capsule is sending out.

Thus the cube should move the same as the capsule, except delayed by the capsule outlet -> cube inlet LSL transmission.

A plane changes its colours occassionally (every ~3.4 seconds). The colour change events also stream out a marker string of the new colour.
The marker is sent out using WaitForEndOfFrame, so its timestamp should be as close as possible to the actual colour change on-screen.
