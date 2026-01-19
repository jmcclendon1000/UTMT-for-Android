@echo off
:: This Script is by Genouka
:: Licensed under MPL2.0

set "ExecutePath=%~dp0"
setlocal enabledelayedexpansion

set "file=%ExecutePath%..\UndertaleModToolAvalonia.Android\bin\Any CPU\Debug\net9.0-android\com.genouka.qiuutmtv4-Signed.apk"
set "classesfile=%ExecutePath%classes.dex"

:: Prebuild Resources only for github actions because local build will automatically prebuild resources
call "%ExecutePath%prebuild_resources.bat"
msbuild "%ExecutePath%..\UndertaleModToolAvalonia\UndertaleModToolAvalonia.csproj" /p:Configuration=Debug /p:Platform="Any CPU"
msbuild "%ExecutePath%..\UndertaleModToolAvalonia.Android\UndertaleModToolAvalonia.Android.csproj" /t:SignAndroidPackage /p:Configuration=Debug /p:Platform="Any CPU"

endlocal