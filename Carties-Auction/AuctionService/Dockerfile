FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

# Copy the solution file
COPY Carties-Auction.sln .

# Copy the project files
COPY AuctionService/AuctionService.csproj AuctionService/
COPY SearchService/SearchService.csproj SearchService/
COPY GatewayService/GatewayService.csproj GatewayService/
COPY Contracts/Contracts.csproj Contracts/
COPY IdentityService/IdentityService.csproj IdentityService/

# Restore package deps
RUN dotnet restore Carties-Auction.sln

# Copy the app folders over
COPY AuctionService AuctionService
COPY Contracts Contracts
WORKDIR /app/AuctionService
RUN dotnet publish -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "AuctionService.dll" ]