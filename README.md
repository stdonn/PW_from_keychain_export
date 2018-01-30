# PW_from_keychain_export

Converts an apple keychain into a keepass(XP) compatible csv file.

## Usage

1) You have to adapt tje apple script "insertPassword" and replace "YourPasswordHere" with your password.

2) Run the script from the finder, you have to give permissions to access the gui.

3) In a terminal window run:

```
security dump-keychain -d login.keychain >> keychains.txt
```

4) Run the script "insertPassword" from the finder, and switch back to the password request windows (after 1 second delay, the script starts working)

5) Run the C# application in a terminal with the following command:

```
mono PW_from_keychain_export.exe <path to keychain.txt> <output csv file>
```

Example:

```
mono PW_from_keychain_export.exe keychain.txt keychain.csv
```

## Download the latest Release

You can download the latest release of [PW_from_keychain_export here](https://github.com/stdonn/PW_from_keychain_export/releases/latest)

**!!!ATTENTION!!!** All current versions are still pre-releases and are not build for working in production environments

## Built With

* [VisualStudio](http://www.visualstudio.com/) - C# IDE

## Authors

* **Stephan Donndorf** - [stdonn](https://github.com/stdonn)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
