Migrations are stored in `Feedz.Data.Database.Migrations`, the `DbContext` is set in `Feedz.Web` and `Feedz.Worker`

To create and run migrations, specific options need to be set during the migration related CLI calls (called from `Feedz.Web` project)

```
dotnet ef migrations add -o ../Feedz.Data/Database/Migrations --namespace Database.Migrations -p ../Feedz.Data initial # add a migration
dotnet ef database update -p ../Feedz.Data/ # Run the migrations
```