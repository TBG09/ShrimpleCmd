@echo off

:: Define the project directory (same as the location of the batch file)
set PROJECT_DIR=%~dp0

:: Navigate to the project directory
cd /d %PROJECT_DIR%

:: Clean previous builds
echo Cleaning previous builds...
dotnet clean

:: Build for win-x64
echo Building for win-x64...
dotnet publish --configuration Release --runtime win-x64 --output ./bin/Release/win-x64

:: Build for win-x86
echo Building for win-x86...
dotnet publish --configuration Release --runtime win-x86 --output ./bin/Release/win-x86

echo Build completed for win-x64 and win-x86.
pause
