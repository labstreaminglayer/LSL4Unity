# LSL4Unity
A integration approach of the LabStreamingLayer Framework for Unity3D providing the following features.

* Simple Editor Integration to lookup LSL streams. 
* Provides a ready to use Marker stream implementation.
* Basic implementations and examples for LSL inlets and outlets.
* Build hooks to copy correct platform library to the build directory

See the [Project Wiki](https://github.com/xfleckx/LSL4Unity/wiki) to get more details and installation instructions.
Also, see the [Tips](https://github.com/xfleckx/LSL4Unity/wiki/Tips-for-using-LSL4Unity)!

This is still under active development, so if something did not work properly stay calm, create an issue on github.
Contributions are welcome!

**LSL ships a C# wrapper for the LSL lib - why should I use an additional wrapper?**

Good question - LSL4Unity tries to provide an enhanced user experience within Unity.
It is intented to solve several issues,
* instable framerates results in irregular sampling intervalls
* plattform dependent compilation

when using an Game Engine - in this case Unity - as a data provider within your experiments.

We also try to provide an easy start with predefined implementations which supports a integration into the EEGLAB, BCILAB and MoBILAB ecosystem. **Far from finished :X**
 


# Compatibility info
Currently LSL ~~works only with x64 builds~~ might work x64 and x86 builds of Unity3D projects!
I got the whole thing running on both platforms under Windows. 
 
Linux and MacOS X Support seems to be working at least in the editor but more testing is necessary.
Contributions are welcome! Just try to build the example scene on your platform and report potential errors as issues! 

# Dependencies
In the current Version, the Asset package ships a sligthly modified version of the C# LSL API and the plugin binaries.
The LabStreaming Layer is original created by SCCN und could be found at <https://github.com/sccn/labstreaminglayer>.

It's highly recommended to read the section about the [Time Synchronization](https://github.com/sccn/labstreaminglayer/wiki/TimeSynchronization.wiki) before building your own experiments!

