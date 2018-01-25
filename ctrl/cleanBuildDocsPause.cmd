@echo off
setlocal enabledelayedexpansion
call resources\config.cmd

REM MAIN
type resources\head.txt

echo. Purging:
echo '%PATH_DOCS_BUILD%'
RD /S /Q %PATH_DOCS_BUILD%

echo.
echo Building docs to:
echo '%PATH_DOCS_BUILD%'
pushd %PATH_DOCS_SRC%

doxygen Doxyfile

echo.
echo Complete [ANY KEY]
pause >NUL
exit 0