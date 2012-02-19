@echo off
pushd "%~dp0"

set SOLUTION=ChessCup.sln
set CONFIG=Release

set BITS=x86
if "%PROCESSOR_ARCHITECTURE%" == "AMD64" set BITS=x64
if "%PROCESSOR_ARCHITEW6432%" == "AMD64" set BITS=x64

IF %BITS% == x86 (
	rem OS is 32bit
	set NSIS="%PROGRAMFILES%\NSIS\makensis.exe" /V3
) ELSE (
	rem OS is 64bit
	set NSIS="%PROGRAMFILES(X86)%\NSIS\makensis.exe" /V3
)

set ZIP="%PROGRAMFILES%\7-Zip\7zG.exe" a -tzip
set MSBUILD="%WinDir%\Microsoft.NET\Framework\v2.0.50727\msbuild.exe"
set REPLACE="lib\sar\sar.exe"


echo VERSION.MAJOR.MINOR.BUILD
set /p VERSION="> "

%REPLACE% ChessCup.nsi "0.0.0.0" "%VERSION%"
%REPLACE% AssemblyInfo.cs "0.0.0.0" "%VERSION%"
%REPLACE% %SOLUTION% "Format Version 10.00" "Format Version 9.00"
%REPLACE% %SOLUTION% "Visual Studio 2008" "Visual Studio 2005"

echo building chesscup
%MSBUILD% "src\chesscup.sln" /p:Configuration=%CONFIG% /p:Platform="Any CPU"
if errorlevel 1 goto BuildFailed

echo creating installer
%NSIS% "src\Installer\chesscup.nsi"
if errorlevel 1 goto BuildFailed
move "src\Installer\ChessCup %VERSION% Install.exe" "ChessCup %VERSION% Install.exe"

rem -----------------------------------------------------------------------
rem Build Complete
rem -----------------------------------------------------------------------

%REPLACE% ChessCup.nsi "%VERSION%" "0.0.0.0"
%REPLACE% AssemblyInfo.cs "%VERSION%" "0.0.0.0"
%REPLACE% %SOLUTION% "Format Version 9.00" "Format Version 10.00"
%REPLACE% %SOLUTION% "Visual Studio 2005" "Visual Studio 2008"

echo build completed

popd
exit /b 0

rem -----------------------------------------------------------------------
rem Build Failed
rem -----------------------------------------------------------------------

:BuildFailed

%REPLACE% ChessCup.nsi "%VERSION%" "0.0.0.0"
%REPLACE% AssemblyInfo.cs "%VERSION%" "0.0.0.0"
%REPLACE% ChessCup.sln "Format Version 9.00" "Format Version 10.00"
%REPLACE% ChessCup.sln "Visual Studio 2005" "Visual Studio 2008"

echo build failed
pause

popd
exit /b 1