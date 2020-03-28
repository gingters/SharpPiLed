#!/bin/bash

# go home
cd ~

echo "Download .NET Core SDK, ASP.NET Core Runtime and Powershell Core"
wget https://download.visualstudio.microsoft.com/download/pr/ccbcbf70-9911-40b1-a8cf-e018a13e720e/03c0621c6510f9c6f4cca6951f2cc1a4/dotnet-sdk-3.1.201-linux-arm.tar.gz
wget https://download.visualstudio.microsoft.com/download/pr/b68cde83-05c7-4421-ad9a-3e6f2cc53824/876dbfc9b4521d3ca89a226c6438ffc1/aspnetcore-runtime-3.1.3-linux-arm.tar.gz
wget https://github.com/PowerShell/PowerShell/releases/download/v7.0.0/powershell-7.0.0-linux-arm32.tar.gz

echo "Unpacking the SDK, runtime and powershell"
mkdir dotnet-arm32
tar zxf dotnet-sdk-3.1.201-linux-arm.tar.gz -C $HOME/dotnet-arm32
tar zxf aspnetcore-runtime-3.1.3-linux-arm.tar.gz -C $HOME/dotnet-arm32

mkdir powershell
tar zxf powershell-7.0.0-linux-arm32.tar.gz -C $HOME/powershell

echo "Cleanup downloaded files"
rm dotnet-sdk-3.1.201-linux-arm.tar.gz
rm aspnetcore-runtime-3.1.3-linux-arm.tar.gz
rm powershell-7.0.0-linux-arm32.tar.gz

# set .NET Core SDK and Runtime path
export DOTNET_ROOT=$HOME/dotnet-arm32
export PATH=$PATH:$HOME/dotnet-arm32;$HOME/powershell

echo "Add DOTNET_ROOT variable and dotnet to path in .profile"
echo "
# set PATH so it includes dotnet runtime if it exists
if [ -d \"\$HOME/dotnet-arm32\" ] ; then
   DOTNET_ROOT=\"\$HOME/dotnet-arm32\"
   PATH=\"\$PATH:\$HOME/dotnet-arm32\"
fi
" >> .profile

echo "Add powershell to path in .profile"
echo "
if [ -d \"\$HOME/powershell\" ] ; then
   PATH=\"\$PATH:\$HOME/powershell\"
fi
" >> .profile

echo "Create a new shell, there you should be able to use dotnet and pwsh"
