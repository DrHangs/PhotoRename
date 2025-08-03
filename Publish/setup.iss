#define MyAppVersion GetVersionNumbersString("single\PhotoRename.exe")

[Setup]
AppName=PhotoRename
AppVersion={#MyAppVersion}
AppPublisher=DrHangs
DefaultDirName={localappdata}\PhotoRename
OutputDir=installer
OutputBaseFilename=PhotoRenameInstaller
SetupIconFile="..\icon.ico"
Compression=lzma2
SolidCompression=yes
DisableDirPage=yes
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
UninstallDisplayIcon={app}\PhotoRename.exe

WizardStyle=modern
; LicenseFile="..\LICENSE"

[Files]
Source: "single\PhotoRename.exe"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\PhotoRename"; Filename: "{app}\PhotoRename.exe";  Tasks: startmenuicon
Name: "{userdesktop}\PhotoRename"; Filename: "{app}\PhotoRename.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Desktop-Verknüpfung erstellen"; GroupDescription: "Verknüpfungen:"; Flags: unchecked
Name: "startmenuicon"; Description: "Startmenü-Verknüpfung erstellen"; GroupDescription: "Verknüpfungen:"
