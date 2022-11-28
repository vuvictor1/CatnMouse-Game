/*******************************************************************************
   Author Information:
   Name: Victor V. Vu
   Email: vuvictor@csu.fullerton.edu

   Program Information:
   Program Name: Mouse Chase
   This File: CatnMousemain.cs
   Description: Manager file that will call on UI file to display graphical output

   Copyright (C) 2022 Victor V. Vu
   This program is free software: you can redistribute it and/or modify it under
   the terms of the GNU General Public License version 3 as published by the
   Free Software Foundation. This program is distributed in the hope that it
   will be useful, but WITHOUT ANY WARRANTY without even the implied Warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
   Public License for more details. A copy of the GNU General Public License v3
   is available here: <https://www.gnu.org/licenses/>.

   Programmed in Ubuntu-based Linux Platform.
   To run bash script, type in terminal: "sh r.sh"
*******************************************************************************/

// Required system files
using System;
using System.Windows.Forms;

// Main class to call ui function
public class CatnMouse {
  static void Main(string[] args) {
    System.Console.WriteLine("Welcome to the driver file of the Mouse Chase game.");
    CatnMouseui CatnMouseapp = new CatnMouseui();
    Application.Run(CatnMouseapp);
    System.Console.WriteLine("The Mouse Chase game will now terminate.");
  }
}
