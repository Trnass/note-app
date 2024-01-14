# note-app
Aplikace běží na mé VPS a není třeba spouštět localhost, nicméně pro jistotu sem dám informace o spuštění na localhostu.<br>
Dokumentaci k API naleznete zde: `https://semestralka-prg.trnass.cz/docs`

# Spuštění aplikace na localhostu
1. Instalace Pythonu: Pokud ještě nemáte nainstalovaný Python, stáhněte a nainstalujte nejnovější verzi z oficiální webové stránky Pythonu.
2. Vytvoření virtuálního prostředí: Po instalaci Pythonu otevřete terminál a pomocí příkazu cd se přesuňte do složky API, která je součástí tohoto projektu. Zde vytvořte virtuální prostředí pomocí příkazu `python -m venv venv`.
3. Aktivace virtuálního prostředí: Aktivujte virtuální prostředí pomocí příkazu `venv\Scripts\activate`. Tímto krokem zajistíte, že všechny následné instalace balíčků budou provedeny do tohoto izolovaného prostředí - lépe to pak smažete.
4. Instalace závislostí: V aktivovaném virtuálním prostředí spusťte příkaz `pip install -r requirements.txt` pro instalaci všech potřebných závislostí zapsaných v souboru requirements.txt.
5. Spouštění FastAPI aplikace: Po úspěšné instalaci závislostí spusťte FastAPI aplikaci pomocí příkazu `uvicorn main:app --reload`. Tento příkaz spustí server, který bude automaticky znovu načítat změny v kódu aplikace.
6. Konfigurace WPF aplikace: Nyní je nutné upravit konfigurační soubor tak, aby odkazoval na localhost. Proveďte změny v souboru `ApiUrlAdress.cs`, který se nachází na cestě `WPF/Repositories/`. Změňte všechny adresy tak, aby ukazovaly na localhost, standardně na portu 8000 (http://localhost:8000/api/test/nevim).

Ujistěte se, že po provedení výše uvedených kroků máte spuštěný FastAPI server, to provedete tak, že si zobrazíte dokumentaci k danému API na adrese `http://localhost:8000/docs`
