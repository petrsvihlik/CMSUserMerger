# CMSUserMerger
A dumb application for merging user accounts in [Kentico](https://www.kentico.com). It generates a SQL script that will update all FK references to a given UserID with a new NewUserID.

The application has been built using [.NET Core RC2](https://blogs.msdn.microsoft.com/dotnet/2016/05/16/announcing-net-core-rc2/) just out of curiosity to try out whether my dev environment is set up correctly for writing Core apps.

It uses [Dapper](https://github.com/StackExchange/dapper-dot-net) - the best object mapper for .NET!

## Running the app
 1. Get [.NET Core](https://www.microsoft.com/net/core#windows)
 2. Adjust `config.json` - fill in your `ConnectionString`, `OldUserID` and `NewUserID`
 3. Run in VS or from a command line by typing `> dotnet CMSUserMerger.dll`
 
 
## More about .NET Core
 * https://dotnet.github.io/
