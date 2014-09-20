;CSCNursery Installer
;Template by Joost Verburg
;Written by jdramer

;--------------------------------
;Include Modern UI

  !include "MUI2.nsh"

;--------------------------------
;Variables

  Var StartMenuFolder

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
  
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\CSCNursery" 
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
  !insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder
  
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
  File ..\NurseryAlertServer\baby_cry_short.wav
  
  ;Store installation folder
  WriteRegStr HKLM "Software\CSCNursery" "Install_Dir" $INSTDIR
  
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    
    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
	CreateShortCut "$SMPROGRAMS\$StartMenuFolder\CSC Nursery.lnk" "$INSTDIR\NurseryAlertServer.exe"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
	CreateShortCut "$DESKTOP\CSC Nursery.lnk" "$INSTDIR\NurseryAlertServer.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_END
  
SectionEnd


;--------------------------------
;Uninstaller Section

Section "Uninstall"

  Delete $INSTDIR\NurseryAlertServer.exe
  Delete $INSTDIR\baby_cry_short.wav
  Delete "$INSTDIR\Uninstall.exe"
  RMDir "$INSTDIR"
  
  !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
    
  Delete "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\CSC Nursery.lnk"
  RMDir "$SMPROGRAMS\$StartMenuFolder"
  
  DeleteRegKey /ifempty HKLM "Software\CSCNursery"

SectionEnd
