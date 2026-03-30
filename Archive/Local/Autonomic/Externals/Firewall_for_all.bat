@rem This batchfile sets the Windows firewall to let the scan for all executables through.
@rem You'll need administrator rights to run this batch.

@echo off

rem ------ check for admin rights ------
NET FILE 1>NUL 2>NUL & if ERRORLEVEL 1 (
  echo.
  echo You need to have administrator rights ^(elevation^) to run this batch. Exiting...
  echo.
  goto MY_EXIT:
)

cd /D "%~dp0"

ver | findstr /C:" 5.">nul && (
  echo Windows XP not supported by this batch!
) || (
  rem ----- check rule -----
  netsh advfirewall firewall show rule name="HBM Scan Ports" >nul
  if ERRORLEVEL 1 (
    rem ----- rule does not yet exist so add it -----
    netsh advfirewall firewall add rule name="HBM Scan Ports" direction=in action=allow enable=yes profile=any localport=1200,1201,1300,1301,31416,31417 protocol=UDP edge=yes
  ) else (
    echo "HBM Scan Ports" already registered in firewall
  )
)

:MY_EXIT
rem ------ pause when started with doubleclick rather than from command line ------
setlocal ENABLEDELAYEDEXPANSION
set testL=%cmdcmdline:"=%
set testR=!testL:%~nx0=!
if not "%testL%" == "%testR%" pause
endlocal
