﻿/*
    DirSyncFTPS
    Copyright (C) 2023  Raphael Beck

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.IO;
using System.Windows;
using GlitchedPolygons.ExtensionMethods;
using Microsoft.Win32;

namespace DirSyncFTPS
{
    /// <summary>
    /// Interaction logic for SelectWinScpExeDialog.xaml
    /// </summary>
    public partial class SelectWinScpAssemblyDialog : Window
    {
        public string AssemblyFilePath { get; private set; } = string.Empty;
        
        public SelectWinScpAssemblyDialog()
        {
            InitializeComponent();
        }

        private void ButtonPickWinScpAssemblyFile_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Multiselect = false,
                DefaultExt = ".dll",
                Title = "Select WinSCPnet.dll",
                Filter = "Dynamically linked library|*.dll",
            };

            if (openFileDialog.ShowDialog() == true)
            {
                AssemblyFilePath =  TextBoxWinScpExeFilePath.Text = openFileDialog.FileName;
                ButtonConfirm.IsEnabled = AssemblyFilePath.NotNullNotEmpty() && File.Exists(AssemblyFilePath);
            }
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            AssemblyFilePath = string.Empty;
            DialogResult = false;
            Close();
        }

        private void ButtonConfirm_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}