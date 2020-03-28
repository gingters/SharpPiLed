# We require the C++ toolchain to build the native lib
# as well as .NET core to build our library and samples.

make --directory ./rpi-rgb-led-matrix all
New-Item -Path ./rpi-rgb-led-matrix/lib/librgbmatrix.so -ItemType SymbolicLink -Value librgbmatrix.so.1 -ErrorAction SilentlyContinue

dotnet pack ./src/SharpPiLed/SharpPiLed.csproj -c Release -o ./nugets
dotnet build ./src/examples/Matrix/Matrix.csproj -c Release
