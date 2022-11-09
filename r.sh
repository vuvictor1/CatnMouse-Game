#!/bin/bash

#;*******************************************************************************************
#; Author: Victor V. Vu
#: Section: 223N-01
#; Descrption: BASH compilation file
#;
#; Copyright (C) 2022 Victor V. Vu
#; This program is free software: you can redistribute it and/or modify it under the terms
#; of the GNU General Public License version 3 as published by the Free Software Foundation.
#; This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
#; without even the implied Warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
#; See the GNU General Public License for more details. A copy of the GNU General Public
#; License v3 is available here:  <https://www.gnu.org/licenses/>.
#;*******************************************************************************************
#; Programmed in Ubuntu-based Linux Platform.
#; To run program, type in terminal: "sh r.sh"
#********************************************************************************************

echo Removing old binary files
rm *.dll
rm *.exe

echo Viewing list of source files
ls -l

echo Compiling C-Sharp file to create new dlls
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:CatnMouseui.dll CatnMouseui.cs

echo Compiling the driver file and linking them with the previously created dll files, creating a new executable.
mcs -r:System -r:System.Windows.Forms -r:CatnMouseui.dll -out:CatnMouse.exe CatnMousemain.cs

echo Viewing current list of source files
ls -l

echo Running the executable file.
./CatnMouse.exe

echo The script has terminated.
