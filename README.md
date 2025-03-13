# BlockLuster

Jeffrey Hastings
jeffhastings1@gmail.com

Classic Video Rental Application

This was created using Visual Studio 2022 and VSCode

The database is Microsoft SqlServer 2019

# Setup:

under Clients in the function app, add the following to local.settings.json and add your database connection string

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  },
  "ConnectionStrings": {
    "Database": "yourdatabaseconnectionstring;"
  },
  "Host": {
    "CORS": "*"
  }
}
```

Under database folder, BlockLuster.DbUp, open appsettings.json and add your connection string there

```json
{
  "ConnectionStrings": {
    "Database": "Server=localhost;Database=BlockLuster;User Id=sa;Password=Password1!;"
  }
}
```

Run DbUp project to populate database with tables and admin user:
Seeded admin user: Admin@Admin.com:Password1!

Open the test folder, in the TestDataSeeder project open DatabaseTestSeeder file

Under the second test method there is a tag [DataRow(0)]
replace the 0 with as many random movie records you'd like to add.

Open the blockluster-Ui fold in VSCode

make sure you have node and angular cli installed.

run npm i
run ng serve
and then run the FunctionApp in Visual studio

navigate the localhost:4200
