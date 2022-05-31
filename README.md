# secondApi
The first exercise in rest API -get-post-put-push-dbaccess-migration


MIGRATIONS
the following command in the terminal will create a pending migration

dotnet ef migrations add nomeMigrazione 

(the first migration is called InitialCreate  dotnet ef migrations add InitialCreate)

only with this command...
dotnet ef database update 

the migration will be applied and history updated

