FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "KutuphaneYonetim.csproj"
RUN dotnet publish "KutuphaneYonetim.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# İŞTE ÇÖZÜM: Klasörü zorla oluşturuyoruz
RUN mkdir -p wwwroot/uploads

ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "KutuphaneYonetim.dll"]