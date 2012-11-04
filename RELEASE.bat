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
set INSTALLER=ChessCup.nsi
set REPO=https://chesscup.googlecode.com/svn
set CONFIG=Release

:: Paths
	set SAR="lib\sar\sar.exe"
	set ZIP="%PROGRAMFILES%\7-Zip\7zG.exe" a -tzip

:: Build Soultion
	echo "VERSION.MAJOR.MINOR.BUILD".
	set /p VERSION="> "

	svn update

	%SAR% -r AssemblyInfo.* ((Version)\(\"\d+\.\d+\.\d+\.\d+\"\)) "Version(\"%VERSION%\")"
	%SAR% -r %INSTALLER% ((PRODUCT_VERSION)\s\"\d+\.\d+\.\d+\.\d+\") "PRODUCT_VERSION \"%VERSION%\""
	%SAR% -r %SOLUTION% "Format Version 10.00" "Format Version 9.00"
	%SAR% -r %SOLUTION% "Visual Studio 2008" "Visual Studio 2005"

	echo building chesscup
	%SAR% -b.net 2.0 %SOLUTION% /p:Configuration=%CONFIG% /p:Platform=\"Any CPU\"
	if errorlevel 1 goto BuildFailed

	echo creating installer
	%SAR% -b.nsis src\Installer\%INSTALLER%
	if errorlevel 1 goto BuildFailed
	
:: Build Complete
	move "src\Installer\ChessCup %VERSION% Install.exe" "ChessCup %VERSION% Install.exe"
	copy src\ChessCup\bin\%CONFIG%\*.exe release\*.exe
	copy src\ChessCup\bin\%CONFIG%\*.dll release\*.dll
	copy license.txt release\license.txt
	
	%SAR% -r %SOLUTION% "Format Version 9.00" "Format Version 10.00"
	%SAR% -r %SOLUTION% "Visual Studio 2005" "Visual Studio 2008"
	
	svn commit -m "version %VERSION%"
	svn copy %REPO%/trunk %REPO%/tags/%VERSION% -m "Tagging the %VERSION% version release of the project"

	echo
	echo
	echo build completed, trunk has been tagged

	popd
	exit /b 0

:: Build Failed
	:BuildFailed
	%SAR% -r ChessCup.sln "Format Version 9.00" "Format Version 10.00"
	%SAR% -r ChessCup.sln "Visual Studio 2005" "Visual Studio 2008"

	echo
	echo
	echo build failed
	pause

	popd
	exit /b 1