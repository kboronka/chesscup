@echo off
pushd "%~dp0"
set SOLUTION=ChessCup.sln
set BASEURL=https://chesscup.googlecode.com/svn/
set CONFIG=Release

:: Paths
	set MSBUILD="%WinDir%\Microsoft.NET\Framework\v2.0.50727\msbuild.exe"
	set REPLACE="lib\sar\sar.exe"
	set ZIP="%PROGRAMFILES%\7-Zip\7zG.exe" a -tzip

	set BITS=x86
	if "%PROCESSOR_ARCHITECTURE%" == "AMD64" set BITS=x64
	if "%PROCESSOR_ARCHITEW6432%" == "AMD64" set BITS=x64

	IF %BITS% == x86 (
		echo OS is 32bit
		set MAKENSIS="%PROGRAMFILES%\NSIS\makensis.exe" /V3
	) ELSE (
		echo OS is 64bit
		set MAKENSIS="%PROGRAMFILES(X86)%\NSIS\makensis.exe" /V3
	)

:: Build Soultion
	echo "VERSION.MAJOR.MINOR.BUILD".
	set /p VERSION="> "

	svn update
	%REPLACE% ChessCup.nsi "0.0.0.0" "%VERSION%"
	%REPLACE% AssemblyInfo.cs "0.0.0.0" "%VERSION%"
	%REPLACE% %SOLUTION% "Format Version 10.00" "Format Version 9.00"
	%REPLACE% %SOLUTION% "Visual Studio 2008" "Visual Studio 2005"

	echo building chesscup
	%MSBUILD% "src\chesscup.sln" /p:Configuration=%CONFIG% /p:Platform="Any CPU"
	if errorlevel 1 goto BuildFailed

	echo creating installer
	%MAKENSIS% "src\Installer\chesscup.nsi"
	if errorlevel 1 goto BuildFailed
	move "src\Installer\ChessCup %VERSION% Install.exe" "ChessCup %VERSION% Install.exe"


:: Build Complete
	%REPLACE% ChessCup.nsi "%VERSION%" "0.0.0.0"
	%REPLACE% AssemblyInfo.cs "%VERSION%" "0.0.0.0"
	%REPLACE% %SOLUTION% "Format Version 9.00" "Format Version 10.00"
	%REPLACE% %SOLUTION% "Visual Studio 2005" "Visual Studio 2008"
	
	svn commit -m "version %VERSION%"
	echo svn copy %BASEURL%/trunk BASEURL/tags/%VERSION%  "Tagging the %VERSION% release of the project"
	svn copy %BASEURL%/trunk BASEURL/tags/%VERSION% -m "Tagging the %VERSION% release of the project"
	:: svn copy http://host_name/repos/project/trunk http://host_name/repos/project/tags/0.1.0 -m "Tagging the 0.1.0 release of the project"
	pause

	echo
	echo
	echo build completed

	popd
	exit /b 0

:: Build Failed
	:BuildFailed
	%REPLACE% ChessCup.nsi "%VERSION%" "0.0.0.0"
	%REPLACE% AssemblyInfo.cs "%VERSION%" "0.0.0.0"
	%REPLACE% ChessCup.sln "Format Version 9.00" "Format Version 10.00"
	%REPLACE% ChessCup.sln "Visual Studio 2005" "Visual Studio 2008"

	echo
	echo
	echo build failed
	pause

	popd
	exit /b 1