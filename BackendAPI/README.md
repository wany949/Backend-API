# Overall Idea
This API will call the Genshin API for recipes and food objects that exist in the Genshin Universe, and will return back to the user a random 3 course meal.

To get the HTML page containing the 3 course meal, please launch in production environment or profile and use the link https://localhost:{your_port_number}/Food/meal

If launched in development environment this is still possible, however you would have to load in all the food first using the *get raw* in Swagger before trying to make a 3 course meal.

## API Called
The Genshin Impact API has been used in this project

https://api.genshin.dev/ for the API or https://genshin.dev/ for the website that leads to the API.

### Configuration Task

For the two different configurations task, there is a development profile and a production profile.
Either launch it by specifying the environment, or a profile has been made where in VS you can use the green play button to choose between the two profiles.

The development profile will launch an in memory database, therefore data does not persist.
The production profile will launch with SQlite, therefore data will persist even after application closes.


