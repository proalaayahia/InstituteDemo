#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["InstituteDemo.Api/InstituteDemo.Api.csproj", "InstituteDemo.Api/"]
COPY ["InstituteDemo.Application/InstituteDemo.Application.csproj", "InstituteDemo.Application/"]
COPY ["InstituteDemo.Infrastructure/InstituteDemo.Infrastructure.csproj", "InstituteDemo.Infrastructure/"]
COPY ["InstituteDemo.Domain/InstituteDemo.Domain.csproj", "InstituteDemo.Domain/"]
RUN dotnet restore "InstituteDemo.Api/InstituteDemo.Api.csproj"
COPY . .
WORKDIR "/src/InstituteDemo.Api"
RUN dotnet build "InstituteDemo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstituteDemo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstituteDemo.Api.dll"]