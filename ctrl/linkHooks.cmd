@echo off
setlocal enabledelayedexpansion
pushd %~dp0
    call resources\config.cmd

    REM MAIN
    type resources\head.txt

    echo "Symlinking %PATH_DEPLOYED_HOOKS% to %PATH_HOOKS%"
    pushd %PATH_DEPLOYED_HOOKS%
    IF EXIST post-commit ( DEL post-commit )
    MKLINK "post-commit" "%PATH_HOOKS_RELPATH%/post-commit"
    popd

    echo.
    echo Complete [ANY KEY]
    pause >NUL
popd
exit 0