@echo off
setlocal enabledelayedexpansion
call resources\config.cmd

REM MAIN
type resources\head.txt

echo.
echo Adding %PATH_DOCS_BUILD% to commit
pushd %PATH_PROJ_ROOT%
    git add -A %PATH_DOCS_BUILD%
popd

echo.
echo Complete
exit 0