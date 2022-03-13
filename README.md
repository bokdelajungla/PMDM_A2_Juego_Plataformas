# PMDM_A2_Juego_Plataformas
Repositorio para la Actividad 2 de Programación Multimedia y Dispositivos móviles

## Enunciado:

Mejorar el juego de plataformas 2D de las masterclass con las siguientes características:

* Escena inicial de introducción al juego.
* Escena de juego:
  - Cambiar los sprites y fondos del juego.
  - Recolección de elementos.
  - Música de fondo.
  - Diferentes efectos de sonido.
  - Retos u obstáculos: enemigos.
  - Sistemas de partículas.
  - HUD.
  - Añadir diferentes tipos de powerups para recoger. Cada uno con puntuación diferente. Opcional.
  - Añadir vidas al player. Opcional.
  - Guardar la puntuación máxima de una partida a otra. Opcional.
* Escena final de juego para mostrar la puntuación total y la oportunidad de volver a jugar.
*	Cambiar el icono del juego.
* Cambiar el splash por defecto de Unity. Opcional.

Deberás entregar la carpeta del proyecto al completo y los archivos del ejecutable generado.
#
## Descripción del Juego
La idea del proyecto era intentar emular el funcionamiento de la saga de videojuegos **"Megaman Zero"** de la consola GameBoy Advance.

Con esto en mente se han empleado los sprites originales extraidos del juego original, obtenidos buscando por internet. Se han implementado algunos de los comportamientos, aunque no todos, del juego original, como el respawn de enemigos o la barra de vida por secciones.

El objetivo del juego es buscar y recolectar todos los *"CyberElfves"* en el menor tiempo posible, evitando los obstáculos.

Entre dichos obstaculos se encuentran los enemigos (aunque sólo se ha implementado un modelo) que mermarán la barra de vida del jugador, las plataformas móviles, y los barrancos de muerte instantánea.

Para facilitar la tarea se han incluído 2 vidas extras al jugador, para no tener que reiniciar el juego en caso de muerte, y no perder su progreso.

En caso de llegar a recolectar todos los *CyberElves* antes de quedarse sin vidas, sonará una fanfarria y se presentará al jugador su resultado en forma de tiempo empleado.
Si el tiempo empleado es menor que el guardado en el Highscore, ne informará al jugador del nuevo record y se guardará el nuevo resultado como Highscore.

Esperamos que os guste, y ¡gracias por jugar! :D

---
## Requisitos de la Actividad:
### Escena inicial de introducción al juego
La escena inicial tiene el nombre de *Title* y contiene un logo, el título y tres botones para iniciar la partida, ver el highscore y salir.
Se ha incluido música de fondo así como sonidos al pasar el cursor por encima de cada uno de los botones. Además cuando se pulsa un botón produce un sonido diferente.

### Escena de juego
#### Cambiar los sprites y fondos del juego
Se han empleado los sprites y fondos del juego original, intentando ser lo más fiel al original (por ejemplo: el personaje principal tiene 9 animaciones).
Se ha usado una imagen fija como fondo mediante el uso de una cámara secundaria que se encarga de renderizar sólo el fondo.
El escenario tiene 5 niveles de layering:
* RearBackground - -1
* Background - 0
* Ground - 1
* Foreground - 3

Los personajes están representados en el layer 2.

### Recolección de elementos
Se ha incluído un elemento a recoger denominado *CyberElf* que consta de una animación de los sprites y una pequeña traslación en forma de 8.
Estos elementos poseen un emisor de partículas que se ha modificado para emitir brillos que cambian con el tiempo, realizando una animación.
Además se han incluido un par de recolectables para regenerar la vida del jugador y otorgar vidas extra.

### Música de fondo
Cada escena tiene incluida una música de fondo diferente.

### Diferentes efectos de sonido
Se han incorporado una decena de sonidos diferentes para los diferentes eventos, como el salto, el ataque, la muerte del jugador...

### Retos u obstáculos: enemigos
Se han añadido un único tipo de enemigo, pero incluye animaciones y colliders de tipo trigger adicionales para simular el ataque, que se activan, desactivan y posicionan correctamente por código (el jugador también posee este tipo de colliders).
Chocar o recibir un ataque de estos enemigos reducirá la vida del jugador (en 2 o en 3 respectivanmente).
También se ha implementado una zona de muerte por caída que produce la muerte instantánea del jugador al atravesarla.

### Sistemas de partículas
Se han implementado dos sistemas de partículas. Uno para el brillo de los cyberelves, y otro para representar la explosión del jugador al morir.
Estos sistemas se basan en sprites que cambian con el tiempo, y para poder usarlos se han tenido que crear "materiales" que se incluyen en su respectiva carpeta.

### HUD
El HUD implementado, muestra los cyberelves restantes por recoger, un cronómetro, una barra de vida de 15 segmentos, el indicador de vidas restantes (que sólo se muestra al inicio/reinicio o cuando se recoge una vida extra), unos toast animados indicando el estado de la misión: start, failed y game over, y un panel que hace un fundido en negro en caso de ganar o perder el juego.

### Añadir diferentes tipos de powerups para recoger. Cada uno con puntuación diferente. Opcional
Además de los *cyberelves* se han incluido dos powerups adicionales:
* 1Up - Proporciona al jugador una vida extra, y muestra el indicador de vidas cuando se coge, además de reproducir un sonido al recogerlo.
* Hp Small Capsule - Regenera 3 puntos del vida del jugador, pero de forma secuencial (mediante una corrutina) a lo largo de lo que tarda en reproducirse el sonido.

### Añadir vidas al player. Opcional
Se ha incluido un sistema de vida y vidas extra al jugador y los powerups correspondientes para su recarga.
Cuando el jugador muere, ya sea porque su vida llegue a cero o se precipite por un barranco, si posee vidas extra, reaparecerá en la posición inicial.

### Guardar la puntuación máxima de una partida a otra. Opcional
Cuando se completa el nivel con éxito, se presentará la escena de WinGame, donde se comprobará si su tiempo es menor que el registrado en PlayerPrefs cn la clave "Highscore". De ser así se indicará mediante la palabra NEW al lado del resultado y se guardará el nuevo record en PlayerPrefs.

### Escena final de juego para mostrar la puntuación total y la oportunidad de volver a jugar
Si el jugador se queda sin vidas antes de recoger todos los *cyberelves* se mostrará el toast de "GAME OVER" y se cargará la escena de GameOver donde se dará la opción de reiniciar, volver al título o salir del juego.
Si por contra, el jugador consigue recoger todos los *cyberelves* sonará una fanfarria y se cargará la escena WinGame donde se mostrará el tiempo empleado y se dará la opción de volver a jugar, volver al título o salir.

### Cambiar el icono del juego
Se ha modificado el icono predeterminado del juego en las opciones del Build->Player Settings.

### Cambiar el splash por defecto de Unity. Opcional
Se ha modificado el splash predeterminado de Unity, y se ha incluído un logo y un fondo difuminado.

---
## Autores ✒️

Los componentes del grupo:

* **Antonio De Gea Velasco**
* **Adrian Rodriguez Montesinos**
* **Jorge Sánchez-Alor Expósito**
