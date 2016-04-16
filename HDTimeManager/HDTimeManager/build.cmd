if %1 == Debug (
	start /wait taskkill /t /f /im HearthstoneDeckTracker.exe
	if exist "C:\Users\EricAlejandro\Documents\GitHub\Hearthstone-Deck-Tracker\Hearthstone Deck Tracker\bin\Debug\Plugins\HDTimeManager.dll" (
		del "C:\Users\EricAlejandro\Documents\GitHub\Hearthstone-Deck-Tracker\Hearthstone Deck Tracker\bin\Debug\Plugins\HDTimeManager.dll"
	)
	copy %2 "C:\Users\EricAlejandro\Documents\GitHub\Hearthstone-Deck-Tracker\Hearthstone Deck Tracker\bin\Debug\Plugins\HDTimeManager.dll"
)