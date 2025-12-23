# 1. Aşama: Derleme
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Dosyaları kopyala (Klasör ismi yok, direkt al)
COPY . .

# Projeyi onar ve derle (Sadece dosya ismini kullanıyoruz)
RUN dotnet restore "KutuphaneYonetim.csproj"
RUN dotnet publish "KutuphaneYonetim.csproj" -c Release -o /app/publish

# 2. Aşama: Çalıştırma
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

# Port Ayarı
ENV ASPNETCORE_URLS=http://+:80

# Başlat
ENTRYPOINT ["dotnet", "KutuphaneYonetim.dll"]