# Guía de Configuración - MiAgendaWeb

Este proyecto es un gestor de contactos desarrollado en ASP.NET Core con SQL Server.

## 1. Configuración de la Base de Datos (SQL Server)
1. Abre **SQL Server Management Studio (SSMS)**.
2. Crea una nueva base de datos llamada `AgendaDB`.
3. Ejecuta el script de creación de tablas (ubicado en `/Data/script.sql`):
   - Asegúrate de incluir las tablas `Usuarios`, `Categorias` y `Contactos`.
   - Verifica que la tabla `Contactos` tenga las columnas `Apellido` (NVARCHAR) y `FechaRegistro` (DATETIME).

## 2. Configuración de la Conexión (appsettings.json)
1. Abre el archivo `appsettings.json` en Visual Studio.
2. Actualiza la cadena de conexión `DefaultConnection`:
   - Cambia `Server=TU_SERVIDOR` por el nombre de tu instancia local.
   - Asegúrate de que `Database=AgendaDB` y `Trusted_Connection=True`.

## 3. Instalación de Dependencias y Túneles
Para que el proyecto pueda comunicarse con la base de datos y gestionar las migraciones, instala estos dos paquetes fundamentales desde la terminal o la Consola de Administrador de Paquetes:

* **Soporte para SQL Server:** `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
* **Herramientas de Diseño:** `dotnet add package Microsoft.EntityFrameworkCore.Design`

Luego, ejecuta `dotnet restore` para asegurar que el entorno esté listo.

## 4. Acceso al Sistema (Login)
El proyecto implementa un sistema de autenticación basado en **Sesiones de ASP.NET Core** para proteger las rutas privadas.

### Credenciales de Acceso (Modo Desarrollo)
Para las pruebas de evaluación, se han configurado las siguientes credenciales temporales:

* **Usuario:** `admin@1.com`
* **Contraseña:** `admin123`

