Name "ChessCup"

!define SOURCE_PATH "..\"
!define PRODUCT_VERSION "0.0.0.0"
!define PRODUCT_PUBLISHER "Kevin Boronka"
!define PRODUCT_WEB_SITE "http://sourceforge.net/projects/chesscup/"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)"

!define UNINSTALL_EXE "$INSTDIR\uninstChessCup.exe"

!define BUILDTYPE "Release"



# Includes
!include "MUI.nsh"

# NSIS settings
SetCompressor lzma

#caption on the installer
Caption "$(^Name) ${PRODUCT_VERSION} $(TEXT_SETUP)"

#default installation directory
InstallDir "$PROGRAMFILES\ChessCup\"

# output files
OutFile "ChessCup ${PRODUCT_VERSION} Install.exe"

#show details
ShowInstDetails show
ShowUnInstDetails show
AutoCloseWindow true

!define MUI_ABORTWARNING
!define MUI_ICON "${SOURCE_PATH}\resources\ChessCup.ico"
!define MUI_UNICON "${SOURCE_PATH}\resources\chesscup-uninstall-full.ico"

!define MUI_LANGDLL_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_LANGDLL_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_LANGDLL_REGISTRY_VALUENAME "NSIS:Language"

!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP "${SOURCE_PATH}\resources\chesscup-header.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "${SOURCE_PATH}\resources\chesscup-header.bmp"
!define MUI_WELCOMEFINISHPAGE_BITMAP "${SOURCE_PATH}\resources\chesscup-install-wizard.bmp"
!define MUI_UNWELCOMEFINISHPAGE_BITMAP "${SOURCE_PATH}\resources\chesscup-install-wizard.bmp"
# ---------------------------------------------------------------------------
# INSTALLER PAGES
# ---------------------------------------------------------------------------

	!define MUI_WELCOMEPAGE_TITLE_3LINES
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "..\..\..\license.txt"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
	!define MUI_FINISHPAGE_TITLE_3LINES
	!define MUI_FINISHPAGE_NOAUTOCLOSE
	!define MUI_FINISHPAGE_RUN "$INSTDIR\chesscup.exe"
	!define MUI_FINISHPAGE_RUN_NOTCHECKED
	!define MUI_FINISHPAGE_RUN_TEXT "Launch ChessCup"
	!define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED

!insertmacro MUI_PAGE_FINISH

# ---------------------------------------------------------------------------
# UNINSTALLER PAGES
# ----------------------------------------------------------------------------
# welcome to uninstaller page
!define MUI_WELCOMEPAGE_TITLE_3LINES
!insertmacro MUI_UNPAGE_WELCOME

# uninstall page
!insertmacro MUI_UNPAGE_INSTFILES

# unistall finished page
!define MUI_FINISHPAGE_TITLE_3LINES
!insertmacro MUI_UNPAGE_FINISH

# Language files
!insertmacro MUI_LANGUAGE "English"

#fuction to get the .NET version
Function GetDotNETVersion
  Push $0
  Push $1

  System::Call "mscoree::GetCORVersion(w .r0, i ${NSIS_MAX_STRLEN}, *i) i .r1 ?u"
  StrCmp $1 0 +2
    StrCpy $0 "not found"

  Pop $1
  Exch $0
FunctionEnd

#function to compare version
Function VersionCompare
	!define VersionCompare `!insertmacro VersionCompareCall`

	!macro VersionCompareCall _VER1 _VER2 _RESULT
		Push `${_VER1}`
		Push `${_VER2}`
		Call VersionCompare
		Pop ${_RESULT}
	!macroend

	Exch $1
	Exch
	Exch $0
	Exch
	Push $2
	Push $3
	Push $4
	Push $5
	Push $6
	Push $7

	begin:
	StrCpy $2 -1
	IntOp $2 $2 + 1
	StrCpy $3 $0 1 $2
	StrCmp $3 '' +2
	StrCmp $3 '.' 0 -3
	StrCpy $4 $0 $2
	IntOp $2 $2 + 1
	StrCpy $0 $0 '' $2

	StrCpy $2 -1
	IntOp $2 $2 + 1
	StrCpy $3 $1 1 $2
	StrCmp $3 '' +2
	StrCmp $3 '.' 0 -3
	StrCpy $5 $1 $2
	IntOp $2 $2 + 1
	StrCpy $1 $1 '' $2

	StrCmp $4$5 '' equal

	StrCpy $6 -1
	IntOp $6 $6 + 1
	StrCpy $3 $4 1 $6
	StrCmp $3 '0' -2
	StrCmp $3 '' 0 +2
	StrCpy $4 0

	StrCpy $7 -1
	IntOp $7 $7 + 1
	StrCpy $3 $5 1 $7
	StrCmp $3 '0' -2
	StrCmp $3 '' 0 +2
	StrCpy $5 0

	StrCmp $4 0 0 +2
	StrCmp $5 0 begin newer2
	StrCmp $5 0 newer1
	IntCmp $6 $7 0 newer1 newer2

	StrCpy $4 '1$4'
	StrCpy $5 '1$5'
	IntCmp $4 $5 begin newer2 newer1

	equal:
	StrCpy $0 0
	goto end
	newer1:
	StrCpy $0 1
	goto end
	newer2:
	StrCpy $0 2

	end:
	Pop $7
	Pop $6
	Pop $5
	Pop $4
	Pop $3
	Pop $2
	Pop $1
	Exch $0
FunctionEnd

# ====================================== INSTALLER =======================================================================================

#function callback on initialization
Function .onInit

NETFramework:
  # have to check if .NET framework 2.0  is installed
  Call GetDotNETVersion
  
  Pop $0
  
  StrCmp $0 "not found" NETFrameworkNotInstalled

  # skip "v"
  StrCpy $0 $0 "" 1 

  # compare version
  ${VersionCompare} $0 "2.0" $1
  
  IntCmp $1 1 DirectX

NETFrameworkNotInstalled:
  MessageBox MB_OK "ChessCup requires .NET Framework v2.0 or newer. Please download it from www.microsoft.com"
  Abort

DirectX:

  # set the context to all users ( only valid for administrator users )
  SetShellVarContext all
FunctionEnd

# This section will be executed no matter what
Section -Install
  #set overwrite
  SetOverwrite ifnewer

  SetAutoClose true
SectionEnd


Section "ChessCup" InSTSSection
   DetailPrint "Extracting ChessCup..."

   CreateDirectory "$INSTDIR"
   Delete "$INSTDIR\*.*"
   SetOutPath "$INSTDIR"

   File "${SOURCE_PATH}\bin\${BUILDTYPE}\ChessCup.exe"
   File "${SOURCE_PATH}\..\..\license.txt"
   File "${SOURCE_PATH}\bin\${BUILDTYPE}\SkylaLib.Tools.dll"   
   File "${SOURCE_PATH}\resources\ChessCup.ico"
   
   IfErrors Error

   DetailPrint "Adding references to START menu ... "
   
   SetOutPath "$INSTDIR"
    
   #Create shortcuts in the Start Menu
   
   CreateDirectory "$SMPROGRAMS\ChessCup"
   CreateShortCut "$SMPROGRAMS\ChessCup\ChessCup.lnk" "$INSTDIR\ChessCup.exe"
  
   DetailPrint "ChessCup installation complete."
   Goto End
   
Error:
   Abort "Error encountered while installing ChessCup !!"

End:
SectionEnd

Section -Post
  # Write the unistaller
  WriteUninstaller "${UNINSTALL_EXE}"

  CreateShortCut "$SMPROGRAMS\ChessCup\Uninstall ChessCup.lnk" "${UNINSTALL_EXE}"

  # Write data about the application to registry ( for uninstallation purposes and version verification )
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name) ${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "${UNINSTALL_EXE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\ChessCup.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "InstallLocation" "$INSTDIR"
SectionEnd

# ====================================== UN-INSTALLER =======================================================================================

Function un.onInit
  SetShellVarContext all
FunctionEnd


Function un.onUninstSuccess

FunctionEnd

Section "-un.ChessCup" UnSTSSection
   DetailPrint "Removing ChessCup..."

   Delete "$INSTDIR\chesscup.exe"
   Delete "$INSTDIR\license.txt"
   Delete "$INSTDIR\SkylaLib.Tools.dll"
   Delete "$INSTDIR\chesscup.ico"


   DetailPrint "Removing reference from START menu ..."

   Delete "$SMPROGRAMS\ChessCup\ChessCup.lnk" 

   DetailPrint "ChessCup removed"
SectionEnd

Section "-un.Post"
  #delete the unistailler link
  Delete "$SMPROGRAMS\ChessCup\Uninstall ChessCup.lnk"

  RMDir "$SMPROGRAMS\ChessCup"

  Delete "${UNINSTALL_EXE}"

  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"

  SetAutoClose true
SectionEnd