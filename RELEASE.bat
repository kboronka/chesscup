:SoftwareRequired
	:: Microsoft.NET v2.0						http://www.filehippo.com/download_dotnet_framework_2/
	:: Microsoft.NET v3.5						http://www.filehippo.com/download_dotnet_framework_3/
	:: TortoiseSVN (+command line tools)		http://www.filehippo.com/download_tortoisesvn/
	:: 7zip 32bit								http://www.filehippo.com/download_7zip_32/
	:: 7zip 64bit								http://www.filehippo.com/download_7-zip_64/
	:: NSIS										http://www.filehippo.com/download_nsis/
	:: SharpDevelop v3.2.1.6466					http://sourceforge.net/projects/sharpdevelop/files/SharpDevelop%203.x/3.2/SharpDevelop_3.2.1.6466_Setup.msi/download
	:: HTML workshop							http://www.microsoft.com/en-us/download/details.aspx?id=21138

:BuildEnvironment
	@echo off
	pushd "%~dp0"
	set SOLUTION=ChessCup.sln
	set INSTALLER=ChessCup.nsi
	set REPO=https://chesscup.googlecode.com/svn
	set CONFIG=Release

:Paths
	set SAR="lib\sar\sar.exe"
	set ZIP="%PROGRAMFILES%\7-Zip\7zG.exe" a -tzip

:Build
	echo "VERSION.MAJOR.MINOR.BUILD".
	set /p VERSION="> "

	svn update

	%SAR% -r AssemblyInfo.* ((Version)\(\"\d+\.\d+\.\d+\.\d+\"\)) "Version(\"%VERSION%\")"
	%SAR% -r %INSTALLER% ((PRODUCT_VERSION)\s\"\d+\.\d+\.\d+\.\d+\") "PRODUCT_VERSION \"%VERSION%\""

	echo building chesscup
	%SAR% -b.net 3.5 %SOLUTION% /p:Configuration=%CONFIG% /p:Platform=\"Any CPU\"
	if errorlevel 1 goto BuildFailed

	echo creating installer
	%SAR% -b.nsis src\Installer\%INSTALLER%
	if errorlevel 1 goto BuildFailed
	
:BuildComplete
	move "src\Installer\ChessCup %VERSION% Install.exe" "ChessCup %VERSION% Install.exe"
	copy src\ChessCup\bin\%CONFIG%\*.exe release\*.exe
	copy src\ChessCup\bin\%CONFIG%\*.dll release\*.dll
	copy license.txt release\license.txt
	
	svn commit -m "version %VERSION%"
	svn copy %REPO%/trunk %REPO%/tags/%VERSION% -m "Tagging the %VERSION% version release of the project"
	svn update
	
	echo build completed, trunk has been tagged
	popd
	exit /b 0

:BuildFailed
	echo build failed
	pause
	popd
	exit /b 1