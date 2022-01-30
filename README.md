# Demo

```sh
dotnet ef migrations add --project DAL.App --startup-project WebApp Initial
dotnet ef database update --project DAL.App --startup-project WebApp
```

Web Controllers
```shell
cd WebApp
dotnet aspnet-codegenerator controller -name ListItemsController -actions -m Domain.ListItem -dc ApplicationDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
```