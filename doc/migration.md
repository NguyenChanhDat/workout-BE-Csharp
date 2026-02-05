After creating new model, entity, please run the cmd below to add new migration script for DB:

```sh
dotnet ef migrations add NAME_IT_YOUR_WAY
```

Then hit this for update current DB:

```sh
dotnet ef database update
```
