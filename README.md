# PianoBE
How to run
cd PianoBE
dotnet run

How to change db
cd PianoBE
open appsettings.Development.json (appsettings.json for deployment env)
To use emulated database, use "InMemory": "true" 
To use real database, use "InMemory": "false" and change db connection string
