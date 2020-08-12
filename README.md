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
    "name": "[your name]",
    "token": "[your token]"
}
```
the program will read the information from the json file for authenticator