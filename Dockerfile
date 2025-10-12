# Stage 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Копіюємо проект і відновлюємо залежності
COPY ["HotelBookingApp/HotelBookingApp.csproj", "HotelBookingApp/"]
RUN dotnet restore "HotelBookingApp/HotelBookingApp.csproj"

# Копіюємо всі файли і будуємо додаток
COPY . .
WORKDIR /source/HotelBookingApp
RUN dotnet publish -c Release -o /app

# Stage 2: Final image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0


WORKDIR /app

# Копіюємо додаток з етапу побудови
COPY --from=build /app .

# Запускаємо додаток
ENTRYPOINT ["dotnet", "HotelBookingApp.dll"]