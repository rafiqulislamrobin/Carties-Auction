FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY Carties-Auction.sln .

COPY AuctionService/AuctionService.csproj AuctionService/
COPY SearchService/SearchService.csproj SearchService/
COPY GatewayService/GatewayService.csproj GatewayService/
COPY Contracts/Contracts.csproj Contracts/
COPY IdentityService/IdentityService.csproj IdentityService/

# Restore package deps
RUN dotnet restore Carties-Auction.sln

# Copy the app folders over
COPY IdentityService IdentityService
WORKDIR /app/IdentityService
RUN dotnet publish -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "IdentityService.dll" ]