REM This file should not be run directly.
REM It is run by the Visual Studio buld process.

SET NUGET=..\packages\NuGet.CommandLine.3.4.3\tools\NuGet.exe
SET VER=%1

@ECHO ====== Creating NuGet Package to %OUTDIR% from %cd%
%NUGET% pack Rhythm.Core.csproj -outputdirectory ..\..\dist -version %VER%