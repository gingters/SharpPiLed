# SharpPiLed

An alternative take on [hzeller](https://github.com/hzeller)'s [rpi-rgb-led-matrix](https://github.com/hzeller/rpi-rgb-led-matrix) C# binding.

We use (a fork of) his library as a submodule, but add our own interpretation of a C# binding for .NET Core on top.

**First of all, please make sure you read and understand the documentation of  the original native library [rpi-rgb-led-matrix](https://github.com/hzeller/rpi-rgb-led-matrix).**

This project is simply a (slightly opinionated) .NET Core wrapper around that library, so I strongly suggest to get the native examples running first to make sure your LED panel works as expected and you figured out your `led-row-addr-type` and `led-multiplexing` settings as well as potentially required muxer settings for your panel type **before** attempting to control your panel from .NET with this wrapper.

## Using SharpPiLed

Adding **SharpPiLed** is as easy as `dotnet add package SharpPiLed` to your .NET Core project.

Then you create a `LedMatrix` instance, configured with your `LedMatrixOptions`, get hold of your `Canvas` and draw onto it:

```C#
var matrix = new LedMatrix(new LedMatrixOptions(), args);
var canvas = matrix.CreateOffscreenCanvas();

// Red line starting top left going downwards and to the right
canvas.DrawLine(1, 1, 16, 16, new Color(255, 0,0));

// At the time of writing not yet sure what this does. Need to
// figure that out ;) But its required for the panel to show sth.
matrix.SwapOnVsync(canvas);
```
## Examples

See the [Examples](./src/examples/README.md) documentation.

### Configuration

The `LedMatrixOptions` object exposes all settings of the library. The settings can also be configured via command line arguments. See the native libraries documentation for a list of the settings.

### BdfFont and fonts

The NuGet package contains several bitmap fonts (.bdf) that are suitable for writing on the display.

These are:

* 4x6
* 5x7, 5x8
* 6x9, 6x10, 6x12, 6x13, 6x13B, 6x13O, clR6x12
* 7x13, 7x13B, 7x13O, 7x14, 7x14B
* 8x13, 8x13B, 8x13O
* 9x15, 9x15B, 9x18, 9x18B
* helvR12
* texgyre-27
* tom-thumb

When building your project the NuGet package will copy all these fonts as a .bdf file into the output directory of your application. The library will look here as a standard location.

Loading and using a font is easy:

```C#
var font = new BdfFont("6x13.bdf");

// Write a red "Thank you!" starting at position 1, 20
canvas.DrawText(font, 1, 20, new Color(0, 255, 0), "Thank you!");
```
Of course you can also use other bdf fonts. I found a collection of different bdf fonts (under various licenses) here: [olikraus/u8g2](https://github.com/olikraus/u8g2/tree/master/tools/font/bdf).

## Run your programs (or the samples) as root

The `rpi-rgb-led-matrix` library needs to initialize some low-level hardware that is required for pulse control / timings and it needs to set the rendering thread priority to high to be fast enough to not produce flicker. *These initializations require root permissions.* After initializiation, it is typically not desirable to stay in this role, so the library then drops the privileges.

Further information can be found in the [original docs](https://github.com/hzeller/rpi-rgb-led-matrix/#running-as-root).

See also the [Examples](./src/examples/README.md) documentation.

## Building this library and examples

After cloning this repo, please make sure to initialize the submodule with the native library:  
`git submodule update --init --recursive`.

Since cross-compiling the native library is difficult, especially for a non-C++ dev like me 😉, this library is meant to be **compiled on a Raspberry Pi.**

### Installing the prerequisites

We need:

1. A C++ compiler toolchain and make.  
Raspbian Buster (full) had everything for this pre-installed.
2. The .NET Core SDK 3.1
3. Powershell Core

The shell-script `install-prereqs.sh` will download the .NET Core SDK, the ASP.NET Core Runtime and Powershell Core and install it in your user's home folder.
It will also put `dotnet` and `pwsh` on your path in your `.profile` file.
Make sure you source that file or create a new login shell for the path changes to apply.

### Building

Run `pwsh ./build.ps1` from the root of this project.
This will build the native library, build the SharpPiLed library and create a NuGet package in the `./nugets` folder and then build all example projects in the `src/examples` folder.
