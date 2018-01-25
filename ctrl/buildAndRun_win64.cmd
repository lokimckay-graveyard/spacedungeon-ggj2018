@echo off
for /F "delims=" %%i in ("%~f0") do set filename="%%~nxi"
CALL resources\\elevate.cmd "%filename%"