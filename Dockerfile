# Stage 1: Build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy file csproj và restore các NuGet packages
COPY ["ThuongMaiDienTuBE/ThuongMaiDienTuBE.csproj", "ThuongMaiDienTuBE/"]
RUN dotnet restore "ThuongMaiDienTuBE/ThuongMaiDienTuBE.csproj"

# Copy toàn bộ mã nguồn còn lại
COPY . .
WORKDIR "/src/ThuongMaiDienTuBE"
RUN dotnet build "ThuongMaiDienTuBE.csproj" -c Release -o /app/build

# Stage 2: Publish ứng dụng
FROM build AS publish
RUN dotnet publish "ThuongMaiDienTuBE.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime image chạy trên Render
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .

# Định nghĩa lệnh chạy ứng dụng và lắng nghe port của Render (port 8080)
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ThuongMaiDienTuBE.dll"]