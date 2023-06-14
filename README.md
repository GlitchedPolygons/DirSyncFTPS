# DirSyncFTPS

## Convert a folder on your Windows system into a dropbox. Kinda.

### _Forked from https://github.com/GlitchedPolygons/DirSyncSFTP_

Windows Explorer integration for FTPS directories. Raw and simple. GPL-3.0 license. Enjoy.

This is a WPF application that can run in the background and synchronize one or more directories on your system with a remote FTPS server's directory.

It behaves kinda like dropbox, except it doesn't handle conflicts at all (the newest writer wins).

Makes use of [WinSCP](https://github.com/winscp/winscp) and its PowerShell interface for contacting the server and synchronizing.

---

![Screenshot](https://api.files.glitchedpolygons.com/api/v1/files/dirsyncftps-screenshot.png)

---

Example setup of an SFTP server + [DirSyncSFTP](https://github.com/GlitchedPolygons/DirSyncSFTP):

(the process is almost the same except for the part where you select the SSH key + passphrase)

https://www.youtube.com/watch?v=G_cSS9fiq_o
