# =========================
# Build stage
# =========================

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY QuickKart.slnx .

COPY QuickKart/QuickKart.API.csproj QuickKart/
COPY QuickKart.Application/QuickKart.Application.csproj QuickKart.Application/
COPY QuickKart.Domain/QuickKart.Domain.csproj QuickKart.Domain/
COPY QuickKart.Infrastructure/QuickKart.Infrastructure.csproj QuickKart.Infrastructure/

RUN dotnet restore QuickKart/QuickKart.API.csproj

COPY . .

RUN dotnet publish QuickKart/QuickKart.API.csproj -c Release -o /app/publish --no-restore


# =========================
# Runtime stage
# =========================

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "QuickKart.API.dll"]