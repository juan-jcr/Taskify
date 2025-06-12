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

   ### aplicacación
   https://taskifyone.netlify.app/

### Clonar utilizando git 
```
git clone https://github.com/juan-jcr/Taskify.git
```

### Instalación

```
npm install
```
Ejecutar y crear la imagen sqlserver donde está el archivo docker-compose
```
docker-compose up sqlserver -d
```
### Crear migraciones

```
dotnet ef migrations add FirstMigration --project ../Infrastructure --startup-project . --output-dir Persistence/Migrations
```
```
dotnet ef database update --project ../Infrastructure --startup-project .
```
### Construir las imagenes que faltan
```
docker-compose up --build
```

#### Autor

### Juan Antonio Castañeda Ramirez </h2>

