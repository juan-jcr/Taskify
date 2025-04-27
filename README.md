<h1 align="center">Challenge✒️ |Task management app✒️</h1>


<p>
 DESCRIPCIÓN
  
Este desafío es un sistema de gestión de tareas (CRUD) desarrollado con C#, ASP.NET CORE, y arquitectura limpia. Se utiliza MediatR para la medición de comandos y consultas.
<p/>

<h2>Tecnologías Utilizadas</h2>

**Backen**: C#, ASP.NET Core, MediaTr, Entity Framework Core
<br>
**Bases de datos**: SQL Server
<br>
**Arquitectura limpia**: Clean Arquitecture

<h2>Características principales</h2>

1. Crear, listar, editar y eliminar tareas.
2. Aplicación de patrones CQRS con MediatR
3. Validación con FluentValidation
4. Separación de capas bajo los principios de Clean Arquitecture.
5. API RESTful y consumo desde Angular


<h2>Mejoras futuras</h2>

1. Implementar paginación en la lista de tareas
2. Validar el correo registrado por el usuario
3. Agregar test unitarios

<h2>Pruebas de la API</h2>

La API está desplegada en Somee.com y puede probarse usando Postman

```bash
https://taskifyapi.somee.com/api
```
[Descargar colección Postman](./postman/Taskify.postman_collection.json)

### Instrucciones

1. Descargar el archivo .json de la colección
2. Importarlo en Postman
3. Configura la variable {{base_url}}

Endpoints principales

### Auth

| Methodo| Ruta          | Descripción         | 
| ------ | ------------- | ------------------- |
| POST   | /auth/register| Registrar usuario   |
| POST   | /auth/login   | Obtener el token    |


### Tareas

| Methodo| Ruta          | Descripción         | 
| ------ | ------------- | ------------------- |
| GET   | /tasks   | Obtener todas las tareas    |
| GET   |  /tasks/{id} | Obtener una tarea por ID |
| POST   | /tasks    | Crear una tarea   |
| DELETE   | /tasks/{id}| Actualizar una tarea existente   |
| PUT   | /tasks/{id} | Eliminar una tarea   |


#### Autor

### Juan Antonio Castañeda Ramirez </h2>

