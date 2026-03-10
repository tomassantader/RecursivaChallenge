# Challenge Recursiva

## Enunciado

1. Objetivo

Generar una aplicación API REST (en el lenguaje que mejor maneje).
El objetivo de la aplicación es, a partir de los datos ingresados por el
usuario (nombre de usuario, email, fecha de nacimiento y password),
mostrar el horóscopo del día actual, según corresponda, el signo
zodiacal del usuario e informando la cantidad de días que restan
para su próximo cumpleaños.
Se debe construir un backend que se conecte a la API indicada a
continuación.
Además, requiere que la aplicación muestre la estadística del signo
más buscado y un historial de consultas.

2. Arquitectura

La solución propuesta deberá considerar los siguientes componentes:
- Aplicación Backend
- Persistencia: cualquier motor de base datos

3. Especificación
Para obtener el contenido del horóscopo utilizar la siguiente API
URL: https://newastro.vercel.app
Method: POST
Body Ejemplo:
{
"date": "2020-01-01",
"lang": "es",
"sign": "Libra"
}
Repo: https://github.com/DerSarco/newastro
Una vez que el usuario se registró puede usar las funcionalidades del
sistema mediante un login desde una API de autenticación.
Además se requiere que el usuario pueda acceder a los datos de su
perfil y modificar los datos que realizó en el registro a excepción de su
nombre de usuario.
Es deseable además minimizar las llamadas a la API de horóscopo
cuando una misma solicitud va a generar el mismo resultado.

4. Forma de entrega
A través de un repositorio GIT compartido.
Se puede utilizar cualquier lenguaje de programación orientado a
objetos.
Es deseable también tener tests unitarios de uno o más casos que se
consideren críticos en el negocio.
Adjuntar, en caso de considerar necesario, un readme con
instrucciones de compilación/uso.

---

## Cómo ejecutar el proyecto con Docker

### Requisitos previos

Asegurate de tener instalado:

- **Docker Desktop** o **Docker Engine**  
https://www.docker.com/products/docker-desktop


### Ejecutar la aplicación

Desde la raíz del proyecto ejecutar:

```bash
docker-compose up --build
```

## Acceso a la aplicación

Una vez iniciados los contenedores puede ingresar a Swagger para realizar pruebas de la API:

**API**
http://localhost:5000/swagger


### Usuario inicial (Seed)

Al iniciar la aplicación por primera vez, se crea automáticamente un usuario de prueba para facilitar el acceso a la API.

**Credenciales**

- **Username** user
- **Password** 123456
- **Email**  user@test.com
- **BirthDate** 1995-05-10

### Autenticación con JWT

Para acceder a los endpoints protegidos es necesario autenticarse.

1. Realizar login utilizando el endpoint: POST /api/auth/login

Ejemplo de body:

```json
{
  "username": "user",
  "password": "123456"
}
```

La respuesta devolverá un token JWT.

### Usar el token en Swagger

1. Copiar el **token** recibido en la respuesta del login.
2. En Swagger hacer click en el botón **Authorize**.
3. Ingresar el token. ( **Ejemplo** eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9... )
4. Presionar **Authorize**.

Una vez autorizado, podrás acceder a los endpoints protegidos de la API.

---

## Endpoints

| Endpoint | Método | Descripción | Autenticación |
|----------|--------|-------------|---------------|
| `/api/user/profile` | GET | Obtiene el perfil del usuario autenticado | Requerida |
| `/api/user/profile` | PUT | Permite actualizar los datos del perfil del usuario autenticado (email y fecha de nacimiento) | Requerida |
| `/api/horoscope` | GET | Devuelve el horóscopo del día, el signo zodiacal del usuario y los días restantes para su próximo cumpleaños | Requerida |
| `/api/horoscope/horoscopeStats` | GET | Devuelve estadísticas de consultas, incluyendo el historial de consultas y el signo más consultado | Requerida |

---

## Estructura del proyecto

- **`HoroscopeChallenge.Api`** → Punto de entrada de la aplicación. Expone los endpoints HTTP y configura la aplicación.

Se organiza en:

- **Controllers** → Definen los endpoints de la API (`AuthController`, `HoroscopeController`, `UserController`).
- **Middleware** → Manejo global de excepciones mediante `ExceptionMiddleware`.
- **Program.cs** → Configuración de servicios, middlewares y pipeline de la aplicación.
- **appsettings.json** → Configuración de la aplicación (conexión a base de datos y JWT).

---


- **`HoroscopeChallenge.Application`** → Contiene la **lógica de aplicación y los casos de uso** siguiendo el patrón **CQRS**.

Se organiza en:

- **Commands** → Operaciones que modifican estado (ej: login, actualización de perfil). Incluyen command, handler, validator y response.
- **Queries** → Operaciones de solo lectura.
- **DTOs** → Objetos utilizados para transferir datos hacia la API.
- **Services** → Servicios de aplicación como `UserService` y `TokenService`.
- **Common/Behaviours** → Comportamientos transversales como validación y logging.
- **Exceptions** → Excepciones específicas de la capa de aplicación.
- **DependencyInjection** → Registro de dependencias de la capa.

---

- **`HoroscopeChallenge.Domain`** → Contiene el **modelo de dominio y las reglas centrales del negocio**.

Se organiza en:

- **Entities** → Entidades del dominio como `User`, `HoroscopeQuery` y `HoroscopeCache`, junto con clases base (`Entity`).
- **Interfaces** → Contratos para servicios del dominio como `IHoroscopeService`.
- **Repositories** → Interfaces de repositorios (`IUserRepository`, `IHoroscopeQueryRepository`, `IHoroscopeCacheRepository`) que definen las operaciones de persistencia.

---


- **`HoroscopeChallenge.Infrastructure`** → Contiene las **implementaciones técnicas** de persistencia y servicios externos.

Se organiza en:

- **AppDbContext** → Contexto de Entity Framework para acceder a la base de datos.
- **Repositories** → Implementaciones de los repositorios definidos en Domain (`UserRepository`, `HoroscopeQueryRepository`, `HoroscopeCacheRepository`).
- **Services** → Implementación de servicios externos como `HoroscopeService`, encargado de consumir la API de horóscopo.
- **Migrations** → Migraciones de Entity Framework para la creación y actualización del esquema de base de datos.
- **DataSeeder** → Inicialización de datos de prueba en la base de datos.
- **DependencyInjection** → Registro de dependencias de infraestructura en el contenedor de servicios.

---

- **`HoroscopeChallenge.Test`** → Contiene las **pruebas unitarias de la aplicación**, enfocadas en validar los casos de negocio más importantes.

Se organiza en:

- **LoginCommandHandlerTests** → Verifica el flujo de autenticación:
  - Usuario inexistente devuelve `Unauthorized`.
  - Password incorrecta devuelve `Unauthorized`.
  - Credenciales válidas generan un `JWT token`.

- **GetHoroscopeQueryHandlerTests** → Valida la lógica principal de obtención del horóscopo:
  - Uso de **cache** cuando el horóscopo ya fue consultado.
  - Llamada a la **API externa** cuando no existe cache.
  - Manejo de respuesta `NotFound` cuando no se obtiene horóscopo.

- **GetHoroscopeStatsQueryHandlerTests** → Verifica el cálculo de estadísticas:
  - Obtención del **signo más buscado**.
  - Construcción del **historial de consultas**.

  ---

## Decisiones técnicas y por qué

- **Separación por capas (API / Application / Domain / Infrastructure)**  
  Permite aislar responsabilidades, mejorar la mantenibilidad y facilitar las pruebas.  
  La lógica de negocio permanece independiente del framework web y de la base de datos.

- **Arquitectura inspirada en Clean Architecture + CQRS**  
  - Uso de `Commands` y `Queries` para separar operaciones de escritura y lectura.
  - Cada caso de uso se implementa mediante `Command/Query + Handler + Validator`.

- **MediatR y Pipeline Behaviours**  
  Se utiliza MediatR para desacoplar los controladores de la lógica de aplicación.  
  Los **Behaviours** (`ValidationBehaviour`, `LoggingBehaviour`) permiten aplicar lógica transversal como validación o logging sin duplicar código en cada handler.

- **FluentValidation**  
  Permite definir reglas de validación de forma declarativa y reutilizable, manteniendo la validación separada de la lógica de negocio.

- **Entity Framework Core**  
  - `AppDbContext` centraliza el acceso a datos.
  - Se utilizan **migraciones** para versionar el esquema de base de datos.
  - `DataSeeder` inicializa datos de prueba para facilitar el testing de la API.

- **Inyección de dependencias**  
  Las dependencias se registran mediante extensiones (`ServiceCollectionExtensions`), permitiendo desacoplar interfaces de implementaciones y facilitando el testing.

- **Manejo global de excepciones**  
  `ExceptionMiddleware` captura excepciones no controladas y devuelve respuestas de error consistentes, evitando exponer detalles internos de la aplicación.

---
