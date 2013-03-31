:SoftwareRequired
	:: Microsoft.NET v2.0						http://www.filehippo.com/download_dotnet_framework_2/
	:: Microsoft.NET v3.5						http://www.filehippo.com/download_dotnet_framework_3/
	:: TortoiseSVN (+command line tools)		http://www.filehippo.com/download_tortoisesvn/
	:: 7zip 32bit								http://www.filehippo.com/download_7zip_32/
	:: 7zip 64bit								http://www.filehippo.com/download_7-zip_64/
	:: NSIS										http://www.filehippo.com/download_nsis/
	:: SharpDevelop v3.2.1.6466					http://sourceforge.net/projects/sharpdevelop/files/SharpDevelop%203.x/3.2/SharpDevelop_3.2.1.6466_Setup.msi/download
	:: HTML workshop							http://www.microsoft.com/en-us/download/details.aspx?id=21138

:DownloadLink
	:: GoogleCode: https://code.google.com/p/chesscup/downloads/list
	:: SourceForge: http://sourceforge.net/projects/chesscup/files/
	
:BuildEnvironment
	@echo off
	pushd "%~dp0"
	set SOLUTION=ChessCup.sln
	set INSTALLER=ChessCup.nsi
	set REPO=https://chesscup.googlecode.com/svn
	set CONFIG=Release
	set BASEPATH=%~dp0

:Paths
	set SAR="lib\sar\sar.exe"
	set ZIP="%PROGRAMFILES%\7-Zip\7zG.exe" a -tzip

:Build
	echo "VERSION.MAJOR.MINOR.BUILD".
	set /p VERSION="> "

	svn cleanup
	svn update

	%SAR% -f.bsd \src\*.cs "Kevin Boronka"
	%SAR% -assy.ver \src\AssemblyInfo.* %VERSION%
	%SAR% -r %INSTALLER% ((PRODUCT_VERSION)\s\"\d+\.\d+\.\d+\.\d+\") "PRODUCT_VERSION \"%VERSION%\""

	%SAR% -b.net 3.5 %SOLUTION% /p:Configuration=%CONFIG% /p:Platform=\"Any CPU\"
	if "%ERRORLEVEL%" NEQ "0" goto BuildFailed
	
	copy src\ChessCup\bin\%CONFIG%\*.exe release\*.exe
	copy src\ChessCup\bin\%CONFIG%\*.dll release\*.dll
	copy license.txt release\license.txt	
	%SAR% -sky.gen SkyUpdate.info release\ChessCup.exe https://chesscup.googlecode.com/svn/trunk/release/ChessCup.exe

	%SAR% -b.nsis src\Installer\%INSTALLER%
	if "%ERRORLEVEL%" NEQ "0" goto BuildFailed
	move "src\Installer\ChessCup %VERSION% Install.exe" "ChessCup %VERSION% Install.exe"

:BuildComplete
	svn commit -m "version %VERSION%"
	svn copy %REPO%/trunk %REPO%/tags/%VERSION% -m "Tagging the %VERSION% version release of the project"
	svn update
	
	cd lib\skylib-source
	svn commit -m "sar version %VERSION%"
	svn update
	cd BASEPATH
	
	echo build completed, trunk has been tagged
	popd
	exit /b 0

:BuildFailed
	echo build failed
	pause
	popd
	exit /b 1


