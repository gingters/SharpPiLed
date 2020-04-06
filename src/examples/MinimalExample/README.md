# SharpPiLed MinimalExample

.NET Core port of the original [minimal_example](https://github.com/hzeller/rpi-rgb-led-matrix/blob/master/bindings/c%23/examples/minimal-example.cs) example.

## Displays a minimal sample with some changes

Arguments:

none

## Run (example for *my* display)

`sudo ./bin/Debug/netcoreapp3.1/linux-arm/publish/MinimalExample --led-rows=16 --led-row-addr-type=2 --led-multiplexing=5 --led-chain=2`

Displays a minimal example. Uses my panel (16x32 P10 Outdoor panel) in a chain of 2.
