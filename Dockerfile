# Stage 1: Build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy file csproj và restore các gói NuGet (vì csproj nằm ngay ở gốc)
COPY ["Ecommerce.csproj", "./"]
RUN dotnet restore "Ecommerce.csproj"

# Copy toàn bộ mã nguồn còn lại
COPY . .
RUN dotnet build "Ecommerce.csproj" -c Release -o /app/build

# Stage 2: Publish ứng dụng
FROM build AS publish
RUN dotnet publish "Ecommerce.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime image chạy trên Render
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Ecommerce.dll"]