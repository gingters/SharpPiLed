# SharpPiLed MatrixRainExample

.NET Core port of the original [matrix-rain](https://github.com/hzeller/rpi-rgb-led-matrix/blob/master/bindings/c%23/examples/matrix-rain.cs) example.

## Shows a green matrix rain

Arguments:

none

## Run (example for *my* display)

`sudo ./bin/Debug/netcoreapp3.1/linux-arm/publish/MatrixRainExample --led-rows=16 --led-row-addr-type=2 --led-multiplexing=5 --led-chain=2`

Displays a green matrix rain. Uses my panel (16x32 P10 Outdoor panel) in a chain of 2.
