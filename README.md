📘 SmartWarehouseAPI

API REST para la plataforma SmartWarehouse, sistema multiplataforma de gestión de pedidos, entregas y facturación.

🚀 Descripción general

SmartWarehouseAPI es el backend que conecta la aplicación de escritorio (C#) y la aplicación móvil Android (Kotlin).
Proporciona servicios REST para gestionar usuarios, pedidos, productos, facturas y rutas de entrega.

La autenticación se realiza mediante JSON Web Tokens (JWT) para garantizar la seguridad y control de acceso según roles:

ADMIN y EMPLEADO (escritorio)

REPARTIDOR y CLIENTE (Android)

🧩 Características principales

🔐 Login seguro con JWT

📦 Gestión de productos, usuarios y pedidos

🚚 Asignación de rutas a repartidores

💸 Generación y consulta de facturas

🌐 Conexión a base de datos MySQL centralizada

🧰 Swagger UI para pruebas rápidas

🔄 Compatible con Visual Studio 2022 / .NET 8.0

⚙️ Tecnologías
Componente	Tecnología
Lenguaje	C# (.NET 8)
Framework	ASP.NET Core Web API
Base de datos	MySQL
ORM	Entity Framework Core (Pomelo)
Seguridad	JWT (Microsoft.AspNetCore.Authentication.JwtBearer)
Documentación	Swagger / OpenAPI
📡 Endpoints principales
Endpoint	Método	Descripción
/api/usuarios/login	POST	Autenticación y generación de token
/api/pedidos	GET / POST / PUT / DELETE	CRUD de pedidos
/api/productos	GET / POST / PUT / DELETE	CRUD de productos
/api/facturas	GET / POST	Facturación y consulta
/api/rutas	GET / POST	Gestión de rutas de entrega
🛠️ Configuración local

Clonar el repositorio:

git clone https://github.com/<tuusuario>/SmartWarehouseAPI.git


Configurar conexión MySQL en appsettings.json.

Instalar dependencias NuGet.

Ejecutar con Ctrl + F5.

Probar endpoints en http://localhost:5000/swagger.

🧠 Autores

Proyecto desarrollado por [Tu Nombre],
para el TFG de Desarrollo de Aplicaciones Multiplataforma (DAM).
