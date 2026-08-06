FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["DevOpsPractice.Web/DevOpsPractice.Web.csproj", "DevOpsPractice.Web/"]
RUN dotnet restore "DevOpsPractice.Web/DevOpsPractice.Web.csproj"

COPY . .

WORKDIR "/src/DevOpsPractice.Web"

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DevOpsPractice.Web.dll"]