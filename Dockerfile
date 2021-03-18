# https://hub.docker.com/_/microsoft-dotnet
# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY MailService.Grpc/*.csproj ./MailService.Grpc/
COPY MailService.Infrastructure/*.csproj ./MailService.Infrastructure/
COPY MailService.Core/*.csproj ./MailService.Core/
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /source/MailService.Grpc
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MailService.Grpc.dll"]