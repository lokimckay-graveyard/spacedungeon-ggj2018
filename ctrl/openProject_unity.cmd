@echo off
setlocal enabledelayedexpansion
call resources\config.cmd

REM MAIN
type resources\head.txt

echo.
echo Opening project using Unity:
echo '%PATH_PROJ%'
%PATH_UNITY_EXE% -projectPath %PATH_PROJ%

echo.
echo Complete
exit 0