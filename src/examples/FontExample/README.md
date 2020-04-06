# SharpPiLed FontExample

.NET Core port of the original [font](https://github.com/hzeller/rpi-rgb-led-matrix/blob/master/bindings/c%23/examples/font-example.cs) example.

Arguments:

* `-f | --font` Font file to use. Default: `6x9.bdf`
* `-t | --text` Text to render. Default: `Hello World!`
* `-x | --x-origin` X origin where to render the text
* `-y | --y-origin` Y origin where to render the text
* `-s | --spacing` Spacing in pixel between letters

## Run (example for *my* display)

`sudo ./bin/Debug/netcoreapp3.1/linux-arm/publish/FontExample -c 255,0,0 -f 6x9.bdf -x 1 -y 4 -s 2 -t "Hi there" --led-rows=16 --led-row-addr-type=2 --led-multiplexing=5 --led-chain=2`

Displays a green "Hi there" with the 6x9 font and a 2 pixel spacing at position 1,4. Uses my panel (16x32 P10 Outdoor panel) in a chain of 2.
