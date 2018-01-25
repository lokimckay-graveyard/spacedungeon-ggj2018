@echo off
setlocal enabledelayedexpansion
call resources\config.cmd

REM MAIN
type resources\head.txt

echo.
echo Opening docs
start chrome %PATH_DOCS_OPEN%

echo.
echo Complete
exit 0