# docker-compose-template
for build c# service, data base and jenkins

Docker Compose 是一個用於定義和運行多容器 Docker 應用的工具，它允許通過一個簡單的 YAML 文件配置應用程序的各個服務，並使用一個命令來啟動、停止和管理所有服務。

# 使用 Docker Compose 建立 C# 服務、資料庫和 Jenkins

本指南將說明如何使用 Docker Compose 來建立 C# 服務、SQL Server 資料庫和 Jenkins 持續集成服務。

## 1. 建立 C# 專案

首先，您需要使用 `dotnet` CLI 建立一個新的 C# 專案。以下是建立 ASP.NET Core MVC 應用程式的步驟：

```sh
dotnet new mvc -o MyCSharpApp
cd MyCSharpApp
dotnet new sln -n MyCSharpApp
dotnet sln add MyCSharpApp.csproj
```

這些命令會在 `MyCSharpApp` 目錄中建立一個新的 ASP.NET Core MVC 專案，並為專案建立一個解決方案文件。

## 2. 編寫 Dockerfile

在 `MyCSharpApp` 目錄中創建一個名為 `Dockerfile` 的文件，內容如下：

```Dockerfile
# 使用官方的 .NET SDK 映像來建置應用程式
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# 複製 csproj 並恢復依賴項作為獨立層
COPY *.sln .
COPY MyCSharpApp/*.csproj ./MyCSharpApp/
RUN dotnet restore

# 複製所有其他文件並建置應用程式
COPY . .
WORKDIR /source/MyCSharpApp
RUN dotnet publish -c Release -o /app

# 使用官方的 ASP.NET 執行環境映像
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MyCSharpApp.dll"]
```

## 3. 創建 docker-compose.yml 文件

在專案的根目錄中創建一個 `docker-compose.yml` 文件，內容如下：

```yaml
version: '3.8'

services:
  # C# 服務
  csharp-service:
    build:
      context: ./MyCSharpApp
      dockerfile: Dockerfile
    container_name: csharp-service
    ports:
      - "5000:80"
    depends_on:
      - db

  # 資料庫
  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    container_name: sqlserver
    environment:
      SA_PASSWORD: "YourStrong!Password"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    volumes:
      - ./data:/var/opt/mssql

  # Jenkins
  jenkins:
    image: jenkins/jenkins:lts
    container_name: jenkins
    ports:
      - "8080:8080"
      - "50000:50000"
    volumes:
      - jenkins_home:/var/jenkins_home
    environment:
      JENKINS_OPTS: --prefix=/jenkins

volumes:
  jenkins_home:
```

## 使用步驟

### 創建 C# 專案

按照上述步驟創建您的 C# 專案。

### 編寫 Dockerfile

在專案目錄中創建 `Dockerfile` 文件，按上述內容填寫。

### 創建 Docker Compose 文件

在專案根目錄中創建 `docker-compose.yml` 文件，按上述內容填寫。

### 運行 Docker Compose

在 `docker-compose.yml` 文件所在目錄執行以下命令：

```sh
docker-compose up -d
```

## 訪問服務

- **C# 服務**：`http://localhost:5000`
- **Jenkins**：`http://localhost:8080/jenkins`
- **資料庫**：使用連接字符串 `Server=localhost,1433;User=sa;Password=YourStrong!Password;`

這樣，您就可以使用 Docker Compose 從源代碼建置並運行 C# 服務，同時配置資料庫和 Jenkins 服務。如果有任何問題或需要進一步的幫助，請告訴我。
