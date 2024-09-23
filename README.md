# Marvel Application

## Descripción

**Marvel Application** es una prueba técnica desarrollada en **ASP.NET WebForms (.NET Framework 4.8)**. El proyecto consiste en obtener **200 personajes** de la [API de Marvel](https://developer.marvel.com/) y mostrar **50 de manera aleatoria** en la interfaz de usuario. 

## Tabla de Contenidos

1. [Características](#características)
2. [Tecnologías Utilizadas](#tecnologías-utilizadas)
3. [Instalación](#instalación)
4. [Uso](#uso)

## Características

- **Filtrado de Personajes sin Imágenes:** La aplicación no muestra personajes que no tengan imágenes asociadas.
- **Descripción de Personajes:** Si un personaje no tiene descripción, se muestra un texto indicando que no hay descripción disponible.
- **Visualización Aleatoria:** Los personajes se muestran de manera aleatoria cada vez que se carga la aplicación.
- **Barra de Búsqueda:** Permite buscar y filtrar personajes específicos mediante una barra de búsqueda integrada.

## Tecnologías Utilizadas

- **Lenguajes y Frameworks:**
    - C#
    - ASP.NET WebForms (.NET Framework 4.8)
    - JavaScript
    - CSS

- **Librerías y Herramientas:**
    - [Moq](https://github.com/moq/moq4) (versión 4.18.4) - Para realizar pruebas unitarias.
    - [Unity](https://github.com/unitycontainer/unity) (versión 5.11.10) - Contenedor de inyección de dependencias.
    - [Newtonsoft.Json](https://www.newtonsoft.com/json) (versión 13.0.3) - Para el manejo de JSON.
    - [MSTest](https://docs.microsoft.com/es-es/dotnet/core/testing/unit-testing-with-mstest) (versión 2.2.10) - Framework de pruebas unitarias.

- **Entorno de Desarrollo:**
    - Visual Studio 2022

## Instalación

Sigue estos pasos para instalar y configurar el proyecto en tu entorno local:

1. **Clonar el Repositorio:**
   ```bash
   git clone https://github.com/JuanJSalzar/Marvel_Application.git

### Abrir el Proyecto

1. **Abre Visual Studio 2022.**
2. **Selecciona** `Archivo` > `Abrir` > `Proyecto/Solución` y navega hasta la carpeta clonada.
3. **Abre el archivo** `Marvel_Application.sln`.

### Configurar Claves de la API de Marvel

1. **Navega al archivo** `web.config` **en el proyecto.**
2. **Localiza las siguientes líneas:**

    ```xml
    <add key="MarvelPublicKey" value="YOUR_PUBLIC_KEY_HERE" />
    <add key="MarvelPrivateKey" value="YOUR_PRIVATE_KEY_HERE" />
    ```

3. **Reemplaza** `YOUR_PUBLIC_KEY_HERE` **y** `YOUR_PRIVATE_KEY_HERE` **con tus claves pública y privada obtenidas de [Marvel Developer Portal](https://developer.marvel.com/).**

### Instalar Dependencias

- **Asegúrate de que todas las dependencias listadas en la sección [Tecnologías Utilizadas](#tecnologías-utilizadas) estén instaladas.** Visual Studio generalmente manejará esto automáticamente mediante **NuGet**.

### Ejecutar el Proyecto

1. **Presiona** `F5` **o haz clic en** `Iniciar` **para ejecutar la aplicación desde Visual Studio.**

## Uso

Una vez que la aplicación esté en funcionamiento:

### Visualización de Personajes

1. **Al cargar la página principal, se mostrarán 50 personajes seleccionados aleatoriamente de los 200 obtenidos de la API de Marvel.**
2. **Solo se mostrarán personajes que tengan una imagen asociada.**

### Descripción de Personajes

- **Cada personaje tendrá su descripción visible.** Si un personaje no dispone de una descripción, se mostrará un texto indicando que no hay descripción disponible.

### Búsqueda de Personajes

- Utiliza la barra de búsqueda para filtrar y encontrar personajes específicos ingresando el nombre correspondiente.
