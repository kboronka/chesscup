:: Development Enviorment
::
:: HTML workshop							http://www.microsoft.com/en-us/download/details.aspx?id=21138
:: Microsoft.NET v2.0.50727					http://www.microsoft.com/download/en/details.aspx?id=19
:: SharpDevelop v3.2.1.6466					http://sourceforge.net/projects/sharpdevelop/files/SharpDevelop%203.x/3.2/SharpDevelop_3.2.1.6466_Setup.msi/download
:: TortoiseSVN 1.7.4(+command line tools)	https://sourceforge.net/projects/tortoisesvn/files/1.7.4/Application/
:: 7zip										http://www.7-zip.org/download.html

@echo off
pushd "%~dp0"
set SOLUTION=ChessCup.sln
set BASEURL=https://chesscup.googlecode.com/svn
set CONFIG=Release

:: Paths
	set BITS=x86
	if "%PROCESSOR_ARCHITECTURE%" == "AMD64" set BITS=x64
	if "%PROCESSOR_ARCHITEW6432%" == "AMD64" set BITS=x64

	IF %BITS% == x86 (
		echo OS is 32bit
		set MAKENSIS="%PROGRAMFILES%\NSIS\makensis.exe" /V3
		set MSBUILD="%WinDir%\Microsoft.NET\Framework\v2.0.50727\msbuild.exe"
		set REPLACE="lib\sar\sar.exe"
		set ZIP="%PROGRAMFILES%\7-Zip\7zG.exe" a -tzip
	) ELSE (
		echo OS is 64bit
		set MAKENSIS="%PROGRAMFILES(X86)%\NSIS\makensis.exe" /V3
		set MSBUILD="%WinDir%\Microsoft.NET\Framework\v2.0.50727\msbuild.exe"
		set REPLACE="lib\sar\sar.exe"
		set ZIP="%PROGRAMFILES%\7-Zip\7zG.exe" a -tzip
	)

:: Build Soultion
	echo "VERSION.MAJOR.MINOR.BUILD".
	set /p VERSION="> "

	svn update
	%REPLACE% -replace ChessCup.nsi "0.0.0.0" "%VERSION%"
	%REPLACE% -replace AssemblyInfo.cs "0.0.0.0" "%VERSION%"
	%REPLACE% -replace %SOLUTION% "Format Version 10.00" "Format Version 9.00"
	%REPLACE% -replace %SOLUTION% "Visual Studio 2008" "Visual Studio 2005"

	echo building chesscup
	%MSBUILD% "chesscup.sln" /p:Configuration=%CONFIG% /p:Platform="Any CPU"
	if errorlevel 1 goto BuildFailed

	echo creating installer
	%MAKENSIS% "src\Installer\chesscup.nsi"
	if errorlevel 1 goto BuildFailed
	move "src\Installer\ChessCup %VERSION% Install.exe" "ChessCup %VERSION% Install.exe"


:: Build Complete
	copy src\ChessCup\bin\%CONFIG%\*.exe release\*.exe
	copy src\ChessCup\bin\%CONFIG%\*.dll release\*.dll
	copy license.txt release\license.txt
	
	%REPLACE% -replace ChessCup.nsi "%VERSION%" "0.0.0.0"
	%REPLACE% -replace AssemblyInfo.cs "%VERSION%" "0.0.0.0"
	%REPLACE% -replace %SOLUTION% "Format Version 9.00" "Format Version 10.00"
	%REPLACE% -replace %SOLUTION% "Visual Studio 2005" "Visual Studio 2008"
	
	svn commit -m "version %VERSION%"
	svn copy %BASEURL%/trunk %BASEURL%/tags/%VERSION% -m "Tagging the %VERSION% version release of the project"

	echo
	echo
	echo build completed, trunk has been tagged

	popd
	exit /b 0

:: Build Failed
	:BuildFailed
	%REPLACE% -replace ChessCup.nsi "%VERSION%" "0.0.0.0"
	%REPLACE% -replace AssemblyInfo.cs "%VERSION%" "0.0.0.0"
	%REPLACE% -replace ChessCup.sln "Format Version 9.00" "Format Version 10.00"
	%REPLACE% -replace ChessCup.sln "Visual Studio 2005" "Visual Studio 2008"

	echo
	echo
	echo build failed
	pause

	popd
	exit /b 1