@echo off
setlocal enabledelayedexpansion

:: Define file name
set "file=../Gyroscope Testing/Library/PackageCache/com.google.xr.cardboard/Runtime/CardboardReticlePointer.cs"
set "newline=        if (Google.XR.Cardboard.Api.IsTriggerPressed || UnityEngine.Input.GetMouseButtonDown(0)) "

:: Use PowerShell to modify the file
powershell -Command "(Get-Content '%file%') | ForEach-Object -Begin {$i=0} -Process {if (++$i -eq 184) {'%newline%'} else {$_}} | Set-Content '%file%'"

echo Line 184 updated successfully.
pause
