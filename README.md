# Challenge Recursiva

## Enunciado del Challenge

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
  [https://www.docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop)

---

### Ejecución con Docker

Desde la raíz del proyecto, ejecutá:

```bash
docker-compose up --build
```

---
