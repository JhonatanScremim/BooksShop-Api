#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BooksShop.Basket/BooksShop.Basket.API/BooksShop.Basket.API.csproj", "BooksShop.Basket/BooksShop.Basket.API/"]
COPY ["BooksShop.Basket/BooksShop.Basket.Domain/BooksShop.Basket.Domain.csproj", "BooksShop.Basket/BooksShop.Basket.Domain/"]
COPY ["BooksShop.Basket/BooksShop.Basket.Services/BooksShop.Basket.Services.csproj", "BooksShop.Basket/BooksShop.Basket.Services/"]
RUN dotnet restore "BooksShop.Basket/BooksShop.Basket.API/BooksShop.Basket.API.csproj"
COPY . .
WORKDIR "/src/BooksShop.Basket/BooksShop.Basket.API"
RUN dotnet build "BooksShop.Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BooksShop.Basket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BooksShop.Basket.API.dll"]