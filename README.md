# G2GPullSync

This project is used to sync the pull request between Github and Gerrit.

## How to use
developed  in .netcore3.1 
```sh
git clone https://github.com/juteman/G2GPullSync.git
cd G2GPullSync
dotnet build
```

Create a json file `Profile.json` in the binary directory
format like this
```json
{
    "bot_name": "[your bot name]",
    "token": "[your bot token]",
    "owner_name": "[the owner of repos name]",
    "is_user": [true if repos owner is user, false if repos owner is org]
}
```
the program will read the information from the json file for authenticator