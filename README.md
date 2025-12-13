# 🚀 RedArbor Employe API



> **API RESTful** para gestión de empleados (CRUD completo) con autenticación JWT, construida con .NET 8, siguiendo Clean Architecture, CQRS, SOLID y buenas prácticas de desarrollo.



 Características

- **CRUD Completo** de empleados
- **Autenticación JWT** Bearer Token
- **Clean Architecture** (4 capas)
- **CQRS** (Commands y Queries separados)
- **Entity Framework Core** para escrituras
- **Dapper** para lecturas (optimización)
- **AutoMapper** para mapeo de objetos
- **FluentValidation** para validaciones
- ** Tests Unitarios** (xUnit, Moq, FluentAssertions)
- **Swagger/OpenAPI** documentación tecnica
- **Docker & Docker Compose**
- **Principios SOLID** aplicados
- **Clean Code** en toda la solución


 Patrones Implementados

- **CQRS** - Separación de Commands (escrituras) y Queries (lecturas)
- **Repository Pattern** - Abstracción de acceso a datos
- **Dependency Injection** - Inyección de dependencias en todo el proyecto
- **Factory Pattern** - DatabaseConnectionFactory para Dapper
- **DTO Pattern** - Transferencia de datos entre capas

---

## 🛠️ Tecnologías

| Categoría | Tecnología | Versión |
|-----------|-----------|---------|
| **Framework** | .NET | 8.0 |
| **Lenguaje** | C# | 12 |
| **Web API** | ASP.NET Core | 8.0 |
| **ORM (Escrituras)** | Entity Framework Core | 8.0 |
| **Micro-ORM (Lecturas)** | Dapper |
| **Base de Datos** | SQL Server | 2022 |
| **Mapeo** | AutoMapper |
| **Validaciones** | FluentValidation | 
| **Autenticación** | JWT Bearer | 
| **Testing** | xUnit + Moq + FluentAssertions | 
| **Documentación** | Swagger/OpenAPI | 
| **Containerización** | Docker + Docker Compose | 

---

## 🚀 Inicio Rápido

### Opción 1: Con Docker (Recomendado) 🐳

**Prerequisitos:**
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

**Pasos:**

```bash
# 1. Clonar el repositorio
git clone <repo-url>
cd RedArbor.Solution

# 2. Iniciar con Docker Compose
docker-compose up --build

#3. Ejecutar el script manualmente por si no se ejecuta automanticamente
docker exec -it redarbor-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "RedArbor2024!" -d master -C -i /docker-entrypoint-initdb.d/init-db.sql

# 4. Acceder a Swagger
# Abre tu navegador en: http://localhost:5000
```

¡Listo! La API y SQL Server están corriendo.

---

### Swagger UI

Accede a la documentación interactiva en:
```
http://localhost:5000
```

## 🔐 Credenciales de Prueba

```
Usuario Admin:
- Username: admin
- Password: admin123

Usuario Test:
- Username: test1
- Password: test
```


##  Autor

**Cristian David Cortés Salazar**


**Made with ❤️ using .NET 8**
