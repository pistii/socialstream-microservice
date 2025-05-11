@echo off
:menu
cls
echo === Docker Compose Teszt Menedzser ===
echo 1^> docker-compose.test.yml UP
echo 2^> docker-compose.yml UP
echo 3^> docker-compose DOWN
echo 4^> docker-compose DOWN + VOLUME torles (-v)
echo 5^> Tesztek futtatasa
echo 6^> Kilepes
echo ======================================
set /p choice=Valassz egy lehetoseget [1-6]: 

if "%choice%"=="1" (
    echo Inditas: docker-compose -f docker-compose.test.yml up --build
    docker-compose -f docker-compose.test.yml up --build
    pause
    goto menu
)
if "%choice%"=="2" (
    echo Inditas: docker-compose up --build
    docker-compose up --build
    pause
    goto menu
)
if "%choice%"=="3" (
    echo Leallitas: docker-compose down
    docker-compose down
    pause
    goto menu
)
if "%choice%"=="4" (
    echo Leallitas + Volume torles: docker-compose down -v
    docker-compose down -v
    pause
    goto menu
)
if "%choice%"=="5" (
    echo Tesztek futtatasa (pl. dotnet test vagy sajat parancs)
    dotnet test
    pause
    goto menu
)
if "%choice%"=="6" (
    echo Kilepes...
    exit /b
)

echo Ervenytelen valasztas. Probald ujra.
pause
goto menu
