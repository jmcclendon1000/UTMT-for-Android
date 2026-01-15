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
rmdir /s /q "%ExecutePath%..\UndertaleModToolAvalonia.Android\obj\Any CPU\Debug\net9.0-android\android\.__override__"
ren "%ExecutePath%..\UndertaleModToolAvalonia.Android\obj\Any CPU\Debug\net9.0-android\android\assets" ".__override__"
xcopy "%ExecutePath%..\UndertaleModToolAvalonia\obj\Any CPU\Debug\net9.0" "%ExecutePath%..\UndertaleModToolAvalonia.Android\obj\Any CPU\Debug\net9.0-android\android\.__override__\arm64-v8a" /s /e /y
rmdir /s /q "%ExecutePath%..\UndertaleModToolAvalonia.Android\obj\Any CPU\Debug\net9.0-android\android\.__override__\x86_64"
mkdir "%ExecutePath%assets" 2>nul
"%ExecutePath%7z.exe" a -tzip "%ExecutePath%assets\genouka_patcher.ext" "%ExecutePath%..\UndertaleModToolAvalonia.Android\obj\Any CPU\Debug\net9.0-android\android\.__override__"
"%ExecutePath%7z.exe" a -tzip "%file%" "%ExecutePath%assets" -aoa
del /q /f "%ExecutePath%assets\genouka_patcher.ext" 2>nul
rmdir /s /q "%ExecutePath%assets"
"%ExecutePath%7z.exe" a -tzip "%file%" "%classesfile%" -aoa

set "outputfile=%ExecutePath%..\UndertaleModToolAvalonia.Android\bin\Any CPU\Debug\net9.0-android\output.merged.apk"
del /q /f "%outputfile%" 2>nul
"%ExecutePath%signapk.exe" sign --ks "%ExecutePath%debug.keystore" --ks-key-alias "androiddebugkey" --ks-pass "pass:android" --key-pass "pass:android" --in "%file%" --out "%outputfile%"

endlocal