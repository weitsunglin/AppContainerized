# 專案簡介

這個專案包含三個主要服務：`myapp`、`sqlserver` 和 `jenkins`。這些服務通過 Docker Compose 進行管理和協作，共享一個名為 `mynetwork` 的 Docker 網路，以及兩個卷（volume）：`shared_data` 和 `sqlserver_data`。

## 使用 Docker Compose 建立並啟動服務

首先，確保已經安裝了 Docker 和 Docker Compose。然後，可以使用以下指令來建立並啟動所有服務：

```sh
docker-compose up --build -d

```

## 資料庫初始化

啟動服務後，可以通過以下 SQL 指令來建立資料庫和資料表，並插入一些初始資料：

``` sql
CREATE DATABASE MyDatabase;
GO

USE MyDatabase;
GO

CREATE TABLE YourTable (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL
);
GO

INSERT INTO YourTable (Name, Age) VALUES ('John Doe', 30);
GO

SELECT TOP (1000) [ID], [Name], [Age] FROM [MyDatabase].[dbo].[YourTable];
GO
```

## 架構圖示

```
+-------------------+      +-------------------+      +------------------+
|                   |      |                   |      |                  |
|      myapp        |      |     sqlserver     |      |      jenkins     |
|                   |      |                   |      |                  |
|   Ports:          |      |   Ports:          |      |   Ports:         |
|    5000:80        |      |    1433:1433      |      |    8081:8080     |
|    5001:5001      |      |                   |      |    50000:50000   |
|                   |      |   Volumes:        |      |                  |
|   Volumes:        |<---->|   /var/opt/mssql  |<---->|   jenkins_home   |
|    shared_data    |      |   shared_data     |      |   shared_data    |
|                   |      |                   |      |                  |
|   Network:        |      |   Network:        |      |   Network:       |
|    mynetwork      |      |    mynetwork      |      |    mynetwork     |
+-------------------+      +-------------------+      +------------------+
```


## 如何使用

### server

1. 建立並啟動服務：運行 docker-compose up --build -d 命令來建立並啟動所有服務。
2. 初始化資料庫：啟動服務後，通過 SQL Server Management Studio 或任何其他 SQL 客戶端執行上面的 SQL 指令來初始化資料庫和資料表，並插入初始資料。
3. 訪問服務：
myapp 服務通過 http://localhost:5000 和 http://localhost:5001 訪問。
sqlserver 服務通過 localhost:1433 訪問。
jenkins 服務通過 http://localhost:8081 和 http://localhost:50000 訪問。
4. jenkins 會備份資料庫跟jenkins到 /c/Users/User/Desktop
5. 重新調整myapp，只要將myapp刪除再重建即可，不要連db都刪除了!!

### client

1. 進入client專案
    cd C:\Users\User\Desktop\work_space\docker-compose-template\ClientExample
2. 建構client 專案
    dotnet build -c Debug
    dotnet build -c Release
3. 找到應用程式並執行它
    C:\Users\User\Desktop\work_space\docker-compose-template\ClientExample\bin\Debug\net8.0
    C:\Users\User\Desktop\work_space\docker-compose-template\ClientExample\bin\Release\net8.0


### demo

### service面板

![services](https://github.com/weitsunglin/docker-compose-template/blob/main/demo/services%20panel.jpg)

### mssql 連 docker sqlserver

![mssql_to_docker_sqlserver](https://github.com/weitsunglin/docker-compose-template/blob/main/demo/mssql%20to%20docker%20sqlserver.jpg)

### jenkins 備份

![backup](https://github.com/weitsunglin/docker-compose-template/blob/main/demo/jenkins%20%20backup1.jpg)

![backup](https://github.com/weitsunglin/docker-compose-template/blob/main/demo/jenkins%20%20backup2.jpg)

### client 端介面

![client](https://github.com/weitsunglin/docker-compose-template/blob/main/demo/client.jpg)

© 2024 weitusnglin. All rights reserved.
