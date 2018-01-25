@echo off
setlocal enabledelayedexpansion
pushd %~dp0
    call resources\config.cmd

    REM MAIN
    type resources\head.txt

    echo "Deleting %PATH_DEPLOYED_HOOKS%\*"
    DEL "%PATH_DEPLOYED_HOOKS%\*"

    echo.
    echo Complete [ANY KEY]
    pause >NUL
popd
exit 0