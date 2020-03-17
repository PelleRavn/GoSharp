
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /
COPY src/GoSharp.csproj src/
RUN dotnet restore "src/GoSharp.csproj"
COPY src/. src/.
WORKDIR "/src"
RUN dotnet build "GoSharp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoSharp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoSharp.dll"]
