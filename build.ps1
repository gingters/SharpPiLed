# We require the C++ toolchain to build the native lib
# as well as .NET core to build our library and samples.

Write-Host "Building the native C++ library"
make --directory ./rpi-rgb-led-matrix | Out-Null
New-Item -Path ./rpi-rgb-led-matrix/lib/librgbmatrix.so -ItemType SymbolicLink -Value librgbmatrix.so.1 -Force | Out-Null

Write-Host "Removing existing build artifacts and clearing local nuget cache"
Get-ChildItem .\ -include nugets,bin,obj -Recurse |
	Foreach-Object {
		Remove-Item $_.fullname -Force -Recurse
	}

dotnet nuget locals all --clear | Out-Null

Write-Host "Build SharpPiLed library"
dotnet pack ./src/SharpPiLed -c Release -o ./nugets

Write-Host "Build all example projects"
Get-ChildItem ./src/examples/ *.csproj -Recurse |
	Foreach-Object {
		$project = $_.fullname
		Write-Host "Building $project"
		dotnet publish $project -c Release
	}
