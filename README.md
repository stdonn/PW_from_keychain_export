1) compile C# source code or download from Releases
2) adept apple script insertPasswort and replace "YourPasswordHere" with your password
3) run security dump-keychain -d login.keychain >> keychains.txt
4) run insertPasswort script and change focus to password request (script starts typing after 1 sec delay)
	Maybe you have to permit the script for the first usage in your systemsettings. Restart the script again after permitting.
5) run PW_from_keychain_export.exe from the build dir with the arguments keychains.txt and keychains.csv
6) insert the keychains.csv file to KeePass[XC]
7) enjoy
