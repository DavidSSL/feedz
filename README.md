## Set things up




The tools you will need are:
- Postgres

You can create Postgres using Docker

```shell
docker container run -d --name postgres144 -e POSTGRES_USER=feedz -e POSTGRES_PASSWORD=f33dz -v ~/PostgresData/14.4:/var/lib/postgresql/data -p 5432:5432 --restart always postgres:14.4
```

This should create a new dB `feedz`. However, you will also have to create another dB `feedz-hangfire`

However you'll also need to create a dB

## Running the initial migration

```shell
cd Feedz.Web
dotnet ef database update
```

At this point you can run the project.
