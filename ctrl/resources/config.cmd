REM GGJ2018 build/deploy config file
REM change these vars to point to correct locations

REM CONFIGURABLE
REM You'll need to modify these
REM --------------------------------------------------------

REM Path to your Unity executable
set PATH_UNITY_EXE=C:\Programs\Unity\Editor\Unity.exe

REM Path to the root folder of the project
set PATH_PROJ_ROOT=C:\root\repo\ggj2018


REM OTHER
REM You shouldn't need to touch these
REM --------------------------------------------------------
set PATH_PROJ=%PATH_PROJ_ROOT%\unity
set PATH_PROJ_OUT=%PATH_PROJ_ROOT%\out
set PATH_PROJ_OUT_TEMP=%PATH_PROJ_OUT%\temp
set PATH_PROJ_MULTITHREAD=%PATH_PROJ_OUT_TEMP%\DontEditMe
set PATH_BUILD_DIR=%PATH_PROJ_OUT%\build
set PATH_LOG_DIR=%PATH_PROJ_OUT%\log
set PATH_BUILD_FILE=%PATH_BUILD_DIR%\win64.exe
set PATH_LOG_FILE=%PATH_LOG_DIR%\buildscript.log
set PATH_DOCS_ROOT=%PATH_PROJ_ROOT%\docs
set PATH_DOCS_SRC=%PATH_DOCS_ROOT%\src
set PATH_DOCS_BUILD=%PATH_DOCS_ROOT%\build
set PATH_DOCS_OPEN=%PATH_DOCS_BUILD%\html\index.html
set PATH_HOOKS=%PATH_PROJ_ROOT%\ctrl\hooks
set PATH_DEPLOYED_HOOKS=%PATH_PROJ_ROOT%\.git\hooks
set PATH_HOOKS_RELPATH=../../ctrl/hooks