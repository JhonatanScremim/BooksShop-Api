#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BooksShop.Catalog/BooksShop.Catalog.API/BooksShop.Catalog.API.csproj", "BooksShop.Catalog/BooksShop.Catalog.API/"]
COPY ["BooksShop.Catalog/BooksShop.Catalog.Application/BooksShop.Catalog.Application.csproj", "BooksShop.Catalog/BooksShop.Catalog.Application/"]
COPY ["BooksShop.Catalog/BooksShop.Catalog.Repository/BooksShop.Catalog.Repository.csproj", "BooksShop.Catalog/BooksShop.Catalog.Repository/"]
COPY ["BooksShop.Catalog/BooksShop.Catalog.Domain/BooksShop.Catalog.Domain.csproj", "BooksShop.Catalog/BooksShop.Catalog.Domain/"]
COPY ["BooksShop.Catalog/BooksShop.Catalog.Infra/BooksShop.Catalog.Infra.csproj", "BooksShop.Catalog/BooksShop.Catalog.Infra/"]
RUN dotnet restore "BooksShop.Catalog/BooksShop.Catalog.API/BooksShop.Catalog.API.csproj"
COPY . .
WORKDIR "/src/BooksShop.Catalog/BooksShop.Catalog.API"
RUN dotnet build "BooksShop.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BooksShop.Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BooksShop.Catalog.API.dll"]