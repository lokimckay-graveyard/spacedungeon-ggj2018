@echo off
setlocal enabledelayedexpansion

REM VAR
call config.cmd

REM MAIN
type head.txt

echo.
echo Removing:
echo '%PATH_LOG_DIR%'
IF EXIST %PATH_LOG_DIR% ( rd %PATH_LOG_DIR% /S /Q )

echo.
echo Creating: 
echo '%PATH_LOG_DIR%'
echo '%PATH_BUILD_DIR%'
echo '%PATH_PROJ_MULTITHREAD%'
IF NOT EXIST %PATH_LOG_DIR% ( mkdir %PATH_LOG_DIR% )
IF NOT EXIST %PATH_BUILD_DIR% ( mkdir %PATH_BUILD_DIR% )
IF NOT EXIST %PATH_PROJ_MULTITHREAD% ( 
	mkdir %PATH_PROJ_MULTITHREAD% 
	mklink /D "%PATH_PROJ_MULTITHREAD%\Assets" "%PATH_PROJ%\Assets" >NUL
	mklink /D "%PATH_PROJ_MULTITHREAD%\ProjectSettings" "%PATH_PROJ%\ProjectSettings" >NUL
)

echo.
echo Opening LZ
%SystemRoot%\explorer.exe %PATH_BUILD_DIR%

:REBUILD

echo.
echo Building
%PATH_UNITY_EXE% -projectPath %PATH_PROJ_MULTITHREAD% -quit -batchmode -executeMethod BuildScript.DevBuildAndRun_Win64 -logFile %PATH_LOG_FILE% && (
	echo Success
) || (
	echo Failure
	echo See %PATH_LOG_FILE% for details
)

echo.
echo Complete
echo [ANY KEY] = REBUILD
echo [CTRL + C] = EXIT
pause >NUL

cls
type head.txt
GOTO REBUILD