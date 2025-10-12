@echo off

tasklist /FI "IMAGENAME eq Docker Desktop.exe" | find /I "Docker Desktop.exe" >nul
if errorlevel 1 (
    echo Docker Desktop starting ...
    start "" "C:\Program Files\Docker\Docker\Docker Desktop.exe"
    :waitForDocker
    tasklist /FI "IMAGENAME eq Docker Desktop.exe" | find /I "Docker Desktop.exe" >nul
    if errorlevel 1 (
        timeout /t 10 /nobreak >nul
        goto waitForDocker
    )
    timeout /t 2 /nobreak >nul
    echo Docker Desktop successfuly started ...
)

echo
cd "C:\Users\andri\source\repos\HotelBooking"

echo Building backend Docker image...
docker build -t komarandrii/hotel-backend:latest .

echo Docker login...
docker login

echo Tagging image...
docker tag komarandrii/hotel-backend:latest komarandrii/hotel-backend:latest

echo Pushing to Docker Hub...
docker push komarandrii/hotel-backend:latest

echo Backend pushed successfully!
pause
