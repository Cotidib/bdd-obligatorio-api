# Proyecto UCU Salud
### Base de Datos 1 - 2° Semestre 2023
La intención de este proyecto es aplicar el conocimiento obtenido durante el curso en cuanto a las características de las bases de datos, la implementación de las mismas y el acceso a las funcionalidades que nos permite.

## Planteo del problema

Se requiere una aplicación que permita mantener actualizada la información sobre el carnet de salud de los funcionarios de la empresa.

## Implementación

* Mediante una aplicación desarrollada con las tecnologías Angular, TypeScript, C#, Python y MySQL.
* Registro y actualización de datos de los funcionarios.
  * Posibilidad de adjuntar el carnet de salud como archivos en formato JPG, JPEG o PDF.
  * Validación de existencia de los funcionarios en la base de datos (Con posibilidad de actualizar la información en caso afirmativo).
  * Sistema de agenda electrónica para los funcionarios cuyo carnet de salud no se encuentra vigente o no poseen.
  * Funcionalidad para especificar un período de actualización de carnet de salud.
    * En caso de encontrarse fuera del período, se bloquea la posibilidad de actualizar/registrar los datos.
* Sistema de notificación diaria a usuarios que no han cargado su carnet de salud mediante correo electrónico.

# Instrucciones
1. Tener abierto `Docker Desktop` 
2. Usar el comando:
```
docker compose -f "docker-compose.yml" up -d --build 
```
3. Entrar a http://localhost:4200

> [!NOTE]
> La UI demora unos segundos en levantar. Se puede ver en los Logs de su imagen en docker desktop.

#### Alumnos:
Conde L., Dibueno C., Kucharski M.
## Librerias
[MySQL Connector]("https://mysqlconnector.net/tutorials/connect-to-mysql/")
