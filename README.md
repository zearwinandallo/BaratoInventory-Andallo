# Add MSSQL Docker

## Docker

**Terminal:**

1. `docker pull mcr.microsoft.com/mssql/server:2022-latest`
2. `docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=123qweASD!' -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest`
3. In solution, go to **Terminal** and run:

```bash
docker compose up -d
```

This assumes a `docker-compose.yml` (or `docker-compose.yaml`) is present in your solution root that defines the services you want to start.

> **Security note:** The `SA_PASSWORD` above is an example. Use a strong, unique password in production.

---

# Application overview screenshots

## Web View

<img width="1092" height="397" alt="image" src="https://github.com/user-attachments/assets/5e435d1e-72ac-434e-82ce-f3e81540e000" />

<img width="1892" height="746" alt="image" src="https://github.com/user-attachments/assets/db8b37c2-b34a-4bc7-b84d-83d1b814c459" />

<img width="1886" height="802" alt="image" src="https://github.com/user-attachments/assets/a356d312-a002-4368-b87c-477e949b7907" />

<img width="1877" height="999" alt="image" src="https://github.com/user-attachments/assets/2d217d79-74b5-4e74-bd4b-b324783704e3" />


---



