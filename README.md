# Debloat Windows 10

Debloat Windows 10 is a WPF (Windows Presentation Foundation) desktop application designed to help users easily remove unnecessary pre-installed Windows 10 applications (bloatware). It also provides a safety net by allowing users to restore previously uninstalled apps.

## Features

* **Uninstall Default Apps:** Easily select and remove suggested Windows 10 bloatware (e.g., 3D Builder, Bing News, Xbox App, etc.) with a clean user interface.
* **Restore Apps:** View a list of uninstalled apps and reinstall them back to your system.
* **Advanced Mode:** An option to display all removable apps on the system. *(Warning: Removing some system apps can cause instability!)*
* **Visual Progress Tracking:** Track the progress of uninstallation and restoration in real-time.

## Installation / Download

You do not need to compile the application yourself to use it. You can download the ready-to-use executable directly from GitHub:

1. Go to the [Releases](../../releases) page of this repository.
2. Download the latest version of the application (`.exe` file).
3. Run the downloaded executable. *(Note: The application requires Administrator privileges to execute PowerShell commands in the background).*

## System Requirements

* **Operating System:** Windows 10 version 2004 (Build 19041) or later.
* **Framework:** .NET 5.0 Desktop Runtime.

## How to Compile / Build from Source

If you prefer to review the code and compile the application yourself, follow these steps:

### Prerequisites
* [Git](https://git-scm.com/)
* [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) 
* Visual Studio 2019/2022 (with the ".NET desktop development" workload) OR the .NET CLI.

### Using .NET CLI
1. Clone the repository:
   ```bash
   git clone https://github.com/jakub-sieradzki/debloatwindows10.git
   ```
2. Navigate into the project directory:
   ```bash
   cd debloatwindows10/DebloatWindows10
   ```
3. Publish the application as a single executable (as configured in the `.csproj`):
   ```bash
   dotnet publish -c Release -r win10-x64 --self-contained
   ```
4. The compiled `.exe` file will be located in the `bin/Release/net5.0-windows10.0.19041.0/win10-x64/publish/` directory.

### Using Visual Studio
1. Clone the repository or download the source code as a ZIP file.
2. Open the `DebloatWindows10.sln` file in Visual Studio.
3. At the top of the Visual Studio window, change the build configuration from `Debug` to `Release`.
4. Go to **Build** > **Build Solution** (or press `Ctrl+Shift+B`).
5. To create a single executable file, right-click the `DebloatWindows10` project in the Solution Explorer, select **Publish**, and follow the wizard using a Folder target.

## How It Works

Under the hood, the application interacts with the Windows package manager by silently executing PowerShell commands:
* **Uninstalling:** Uses `Get-AppxPackage <Name> | Remove-AppxPackage` to remove the selected software.
* **Restoring:** Uses `Add-AppxPackage -register "$env:homedrive\Program Files\WindowsApps\<PackageFullName>\appxmanifest.xml" -DisableDevelopmentMode` to bring the applications back.
