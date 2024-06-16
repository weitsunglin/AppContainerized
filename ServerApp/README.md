# Docker Compose

這個 `docker-compose.yml` 文件定義了三個服務：`myapp`、`sqlserver` 和 `jenkins`。
這些服務共享一個名為 `mynetwork` 的 Docker 網路，以及兩個卷（volume）：`shared_data` 和 `sqlserver_data`。

## 架構圖示

```plaintext

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