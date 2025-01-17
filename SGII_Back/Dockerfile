#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SGII_Back/SGII_Back.csproj", "SGII_Back/"]
RUN dotnet restore "SGII_Back/SGII_Back.csproj"
COPY . .
WORKDIR "/src/SGII_Back"
RUN dotnet build "SGII_Back.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGII_Back.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGII_Back.dll"]