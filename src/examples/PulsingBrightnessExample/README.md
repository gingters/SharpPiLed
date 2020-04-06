# SharpPiLed MinimalExample

.NET Core port of the original [pulsing_brightness](https://github.com/hzeller/rpi-rgb-led-matrix/blob/master/bindings/c%23/examples/pulsing-brightness.cs) example.

## Displays a pulsing brightness sample in red, green, blue and white

Arguments:

none

## Run (example for *my* display)

`sudo ./bin/Debug/netcoreapp3.1/linux-arm/publish/PulsingBrightnessExample --led-rows=16 --led-row-addr-type=2 --led-multiplexing=5 --led-chain=2`

Displays a pulsing brightness example. Uses my panel (16x32 P10 Outdoor panel) in a chain of 2.
