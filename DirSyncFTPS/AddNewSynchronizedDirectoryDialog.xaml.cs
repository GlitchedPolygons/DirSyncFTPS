/*
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
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using GlitchedPolygons.ExtensionMethods;
using Microsoft.Win32;

namespace DirSyncFTPS;

public partial class AddNewSynchronizedDirectoryDialog : Window
{
    public SynchronizedDirectory SynchronizedDirectory { get; private set; } = new();

    public AddNewSynchronizedDirectoryDialog()
    {
        InitializeComponent();

        TextBoxHostName.Focus();
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void TextBox_OnGotFocus(object sender, RoutedEventArgs e)
    {
        (sender as TextBox)?.SelectAll();
        (sender as PasswordBox)?.SelectAll();
    }

    private void ButtonSelectLocalDirectory_OnClick(object sender, RoutedEventArgs e)
    {
        using var dialog = new System.Windows.Forms.FolderBrowserDialog();

        dialog.ShowNewFolderButton = true;
        dialog.Description = "Select the local directory on your system that you want to be in sync with the remote.";

        if (dialog.ShowDialog() is not System.Windows.Forms.DialogResult.OK)
        {
            return;
        }

        string selectedDir = dialog.SelectedPath;

        if (!Directory.Exists(selectedDir))
        {
            MessageBox.Show("ERROR: The directory you selected does not exist. Please pick another!");
        }
        else
        {
            TextBoxLocalDirectory.Text = dialog.SelectedPath;
        }
    }

    private void ButtonClearRemoteDirectoryField_OnClick(object sender, RoutedEventArgs e)
    {
        TextBoxRemoteDirectory.Text = string.Empty;
    }

    private void ButtonClearHostNameField_OnClick(object sender, RoutedEventArgs e)
    {
        TextBoxHostName.Text = string.Empty;
    }

    private void ButtonClearPortNumberField_OnClick(object sender, RoutedEventArgs e)
    {
        TextBoxPortNumber.Text = "22";
    }

    private void ButtonClearUsernameField_OnClick(object sender, RoutedEventArgs e)
    {
        TextBoxUsername.Text = string.Empty;
    }

    private void ButtonClearPasswordField_OnClick(object sender, RoutedEventArgs e)
    {
        PasswordBoxPassword.Password = string.Empty;
    }

    private void RadioButtonExplicitFTPSMode_OnChecked(object sender, RoutedEventArgs e)
    {
        SynchronizedDirectory.FtpsModeImplicit = 0;

        if (RadioButtonImplicitFTPSMode is not null)
            RadioButtonImplicitFTPSMode.IsChecked = false;
    }

    private void RadioButtonImplicitFTPSMode_OnChecked(object sender, RoutedEventArgs e)
    {
        SynchronizedDirectory.FtpsModeImplicit = 1;

        if (RadioButtonExplicitFTPSMode is not null)
            RadioButtonExplicitFTPSMode.IsChecked = false;
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void ButtonConfirm_OnClick(object sender, RoutedEventArgs e)
    {
        SynchronizedDirectory.Host = TextBoxHostName.Text;
        SynchronizedDirectory.Port = ushort.TryParse(TextBoxPortNumber.Text, out ushort port) ? port : (ushort)22;

        SynchronizedDirectory.Username = TextBoxUsername.Text;
        SynchronizedDirectory.Password = PasswordBoxPassword.Password;

        SynchronizedDirectory.LocalDirectory = TextBoxLocalDirectory.Text.ToLowerInvariant();
        SynchronizedDirectory.RemoteDirectory = TextBoxRemoteDirectory.Text;

        SynchronizedDirectory.FtpsModeImplicit = (RadioButtonImplicitFTPSMode.IsChecked is true)
            ? (ushort)1
            : (ushort)0;

        DialogResult = true;
        Close();
    }

    private void ButtonHelp_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show
        (
            @"
To synchronize an FTPS server's remote directory with a local one, please compile the form and enter the necessary credentials in order to proceed.

Synchronized directories behave kinda like Dropbox, OneDrive, etc... in that they are always kept in sync with their remote counterpart.

Please keep in mind though that at the moment, DirSyncFTPS is very raw and simple: it does not handle file conflicts at all and has no ignore-list feature.

It's not guaranteed that this works flawlessly. Until it's battle-tested thoroughly, ALWAYS KEEP A SEPARATE BACKUP OF YOUR DATA!

_________________________________________________________

Example config:

Host: 
ftps.example.org

Port: 
22

Username: 
myFTPSUserOnTheServer

Password: 
Sup3rS4fePa$$W0rd_omfgPleaseTakeCare!

Local path: 
C:\Users\MyUserProfile\SomeFolder

Remote path: 
/mnt/nas/users/myFTPSUserOnTheServer/SomeFolder

",
            "Detailed information about DirSyncFTPS"
        );
    }
}