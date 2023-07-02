## Set up

Se creó un minimal api en .Net Core para proveer el listado de rutas al front, la solución se creó en Visual Studio Community 2022,
el api cuenta con las siguientes especificaciones:

1. Swagger que expone un único método para proveer las posibles rutas dadas un origen y un destino
2. EntityFramewok in Memory para simular una base de datos y así evitar múltiples llamadas al api externo que provee las rutas
3. Logs de aplicaciones con Serilop
4. Middleware para el control de excepciones
5. Inyección de dependencias
6. AutoMapper

Ejecución

1. Compilar la solución para instalación correcta de Nuget Packages

al ejecutar en debug desde VS , verificar que el debug se haga con el protocolo http.
