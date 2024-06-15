# 使用官方的 .NET SDK 映像構建應用
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# 複製 csproj 並還原依賴項
COPY *.csproj ./
RUN dotnet restore

# 複製其餘的文件並構建應用
COPY . ./
RUN dotnet publish -c Release -o out

# 使用官方的 .NET 運行時映像運行應用
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# 設置應用程序入口點
ENTRYPOINT ["dotnet", "MyApp.dll"]
