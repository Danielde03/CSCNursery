;CSCNursery Installer
;Template by Joost Verburg
;Written by jdramer

;--------------------------------
;Include Modern UI

  !include "MUI2.nsh"

;--------------------------------
;General

  ;Name and file
  Name "CSCNursery"
  OutFile "CSCNursery-Installer.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\CSCNursery"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKLM "Software\CSCNursery" "Install_Dir"

  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin
  
  ;Hide details on install by default
  ShowInstDetails hide
  ShowUnInstDetails hide

;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING

;--------------------------------
;Pages

  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  
;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Main Section" SecMain

  SetOutPath "$INSTDIR"
  
  File ..\NurseryAlertServer\bin\Release\NurseryAlertServer.exe
  
  CreateShortCut "$DESKTOP\CSC Nursery.lnk" "$INSTDIR\NurseryAlertServer.exe"

  ;Add Start Menu Shortcuts
  
  ;Store installation folder
  WriteRegStr HKLM "Software\CSCNursery" "Install_Dir" $INSTDIR
  
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

SectionEnd


;--------------------------------
;Uninstaller Section

Section "Uninstall"

  Delete $INSTDIR\NurseryAlertServer.exe
  RMDir "$INSTDIR"

  DeleteRegKey /ifempty HKLM "Software\CSCNursery"

SectionEnd
