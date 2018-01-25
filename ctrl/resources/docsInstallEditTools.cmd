@echo off
setlocal enabledelayedexpansion
for %%a in ("%0") do cd "%%~dpa"
cd ../
call resources\config.cmd

REM MAIN
type resources\head.txt

echo.
echo This will install the following:
echo - Chocolatey windows package manager
echo - Doxygen portable
echo.
echo Confirm [ANY KEY]
pause >NUL
echo.

choco >NUL
IF ERRORLEVEL 2 (
    echo.
    echo Installing Chocolatey
    @"%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -InputFormat None -ExecutionPolicy Bypass -Command "iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))" && SET "PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin"
) ELSE (
    echo Chocolatey is already installed
)

doxygen -v >NUL
IF ERRORLEVEL 2 (
    echo.
    echo Installing Doxygen portable via Chocolatey
    choco install doxygen.portable -y
) ELSE (
    echo Doxygen is already installed
)

echo.
echo Complete [ANY KEY]
pause >NUL
exit 0