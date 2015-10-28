; ��� ����������
#define   Name       "GEMC"
; ������ ����������
#define   Version    "0.0.1"
; �����-�����������
#define   Publisher  "S.GLEB"
; ���� ����� ������������
#define   URL        "http://shpytagleb@gmail.com"
; ��� ������������ ������
#define   ExeName    "GEMC.exe"

[Setup]
AppId={{60E848ED-F41D-418D-8501-942FC7C95DDE}

AppName={#Name}
AppVersion={#Version}
AppPublisher={#Publisher}
AppPublisherURL={#URL}
AppSupportURL={#URL}
AppUpdatesURL={#URL}

; ���� ��������� ��-���������
DefaultDirName={pf}\{#Name}
; ��� ������ � ���� "����"
DefaultGroupName={#Name}

; �������, ���� ����� ������� ��������� setup � ��� ������������ �����
OutputDir=..\..\..\test-setup
OutputBaseFileName=test-setup

; ��������� ������
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "License_ENG.txt"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"; LicenseFile: "License_RUS.txt"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; ����������� ����
Source: "..\..\..\GEMC\bin\Release\GEMC.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "dotNetFx45_Full_setup.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall; Check: not IsRequiredDotNetDetected

; ������������� �������
Source: "..\..\..\GEMC\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Code]
function IsDotNetDetected(version: string; release: cardinal): boolean;
var 
    reg_key: string; // ��������������� ��������� ���������� �������
    success: boolean; // ���� ������� ������������� ������ .NET
    release45: cardinal; // ����� ������ ��� ������ 4.5.x
    key_value: cardinal; // ����������� �� ������� �������� �����
    sub_key: string;

begin
    success := false;
    reg_key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\';

     // ������ 4.5
     if Pos('v4.5', version) = 1 then
      begin
          sub_key := 'v4.5\Full';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'Release', release45);
          success := success and (release45 >= release);
      end;
        
    result := success;

end;

function IsRequiredDotNetDetected(): boolean;
begin
    result := IsDotNetDetected('v4.5 Full Profile', 0);
end;

function InitializeSetup(): boolean;
begin

  // ���� ��� ��������� ������ .NET ������� ��������� � ���, ��� �����������
  // ���������� ���������� � �� ������ ���������
  if not IsDotNetDetected('v4.5 Full Profile', 0) then
    begin
      MsgBox('{#Name} requires Microsoft .NET Framework 4.5 Full Profile.'#13#13
             'The installer will attempt to install it', mbInformation, MB_OK);
    end;   

  result := true;
end;
[Run]
;------------------------------------------------------------------------------
;   ������ ������� ����� �����������
;------------------------------------------------------------------------------
Filename: {tmp}\dotNetFx45_Full_setup.exe; Parameters: "/q:a /c:""install /l /q"""; Check: not IsRequiredDotNetDetected; StatusMsg: Microsoft Framework 4.5 is installed. Please wait...