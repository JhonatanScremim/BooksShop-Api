#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BooksShop.Discount.Grpc/BooksShop.Discount.Grpc.API/BooksShop.Discount.Grpc.API.csproj", "BooksShop.Discount.Grpc/BooksShop.Discount.Grpc.API/"]
COPY ["BooksShop.Discount.Grpc/BooksShop.Discount.Grpc.Repository/BooksShop.Discount.Grpc.Repository.csproj", "BooksShop.Discount.Grpc/BooksShop.Discount.Grpc.Repository/"]
RUN dotnet restore "BooksShop.Discount.Grpc/BooksShop.Discount.Grpc.API/BooksShop.Discount.Grpc.API.csproj"
COPY . .
WORKDIR "/src/BooksShop.Discount.Grpc/BooksShop.Discount.Grpc.API"
RUN dotnet build "BooksShop.Discount.Grpc.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BooksShop.Discount.Grpc.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BooksShop.Discount.Grpc.API.dll"]