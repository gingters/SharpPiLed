# SharpPiLed ClockExample

.NET Core port of the original [clock](https://github.com/hzeller/rpi-rgb-led-matrix/blob/master/examples-api-use/clock.cc) example.

## Shows a clock

Arguments:

* `-f | --font` Font file to use. Default: `8x13.bdf`
* `-b | --brightness` Brightness (0 - 100). Default: 100
* `-x | --x-origin` X origin where to render the clock
* `-y | --y-origin` Y origin where to render the clock
* `-s | --spacing` Spacing in pixel between letters
* `-fmt | --format` Format string for a .NET `DateTime` value. Default: `hh:mm:ss`
* `-c | --color` Text color. Default: yellow `255,255,0`
* `-bc | --background-color` Background color. Default: black `0,0,0`

## Run (example for *my* display)

`sudo ./bin/Debug/netcoreapp3.1/linux-arm/publish/ClockExample -c 0,255,0 -f 6x9.bdf -x 1 -y 4 -s 2 -b 75 --led-rows=16 --led-row-addr-type=2 --led-multiplexing=5 --led-chain=2`

Displays a green clock with the 6x9 font and a 2 pixel spacing at position 1,4 with a brightness of 75%. Uses my panel (16x32 P10 Outdoor panel) in a chain of 2.
