# Levantar Proyecto
Tener abierto Docker Desktop
Usar el comando:
```
docker compose -f "docker-compose.yml" up -d --build 
```

Nota: La UI demora unos segundos en levantar. Se puede ver en los Logs de su imagen en docker desktop.

# bdd-obligatorio-api
Test Request: GET http://localhost:5000/WeatherForecast

## Flujo Sig In (Persona no registrada)
Formulario Registrarse > Formulario datos personales (Funcionario) > Dashboard (¿Carnet de Salud o Agenda?) 
## Flujo Log in (Persona ya registrada)
Formulario Login > Autenticado > Dashboard (¿Carnet de Salud o Agenda?) 

## Librerias
[MySQL Connector]("https://mysqlconnector.net/tutorials/connect-to-mysql/")