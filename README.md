# Overall Idea
This API will call the Genshin API for recipes and food objects that exist in the Genshin Universe, and will return back to the user a random 3 course meal.

To get the HTML page containing the 3 course meal, please launch in production environment or profile and use the link 
```https://localhost:{your_port_number}/Food/get_meal_HTML```

If launched in development environment this is still possible, however you would have to load in all the food first using the *get raw* in Swagger before trying to make a 3 course meal.

## API Name Used
The Genshin Impact API has been used in this project

https://api.genshin.dev/ for the API or https://genshin.dev/ for the website that leads to the API.

### Configuration Task

For the two different configurations task, there is a development profile and a production profile.
Either launch it by specifying the environment, or a profile has been made where in VS you can use the green play button to choose between the two profiles.

The development profile will launch an in memory database, therefore data does not persist.
The production profile will launch with SQlite, therefore data will persist even after application closes.

### Testing

The testing is done in the BackendAPI.Testing folder. It runs tests on the CRUD methods, using Moq and mocking the service and controller, as well as using substitute made up values.

## Answering Section 2 and 3

### Section 2
*Demonstrate an understanding of how these middleware via DI (dependency injection) simplifies your code.*

I have used different folders containing a mixture of different classes and an interface to reduce duplicate code and increase code cleanliness. For example, only my Models folder holds the attributes to my Food object. To implement different methods for CRUD, I have an IFoodService interface which is implemented by FoodService. Currently, in my code there is only one implementation of IFoodService, but if I did want to have more ways of implementing the currently existing methods (eg. Get3CourseMeal, if I wanted it to get the first 3 or last 3 instead of random 3), it allows me to do so easily and without duplicate code. 

There are folders to separate Controllers, Models, and Data to help anyone reading the code understand what code is placed where. HTML code is also placed in a separate folder to avoid cluttering.

### Section 3
*Demonstrate an understanding of why the middleware libraries made your code easier to test.*

Some libraries that were used to help make testing easier, were provided by Moq and NUnit. Libraries enable us to use common operations that were already written by other people, and are mostly guaranteed to be very fast, accurate and easy to use. The assertion libraries make it really simple to test our code as it has many different ways of being used, such as Assert.AreEqual, or Assert.That(..., Is.EqualTo) and so on. This allows for results in our testing to be produced with just one line of code, as opposed to us having to implement it ourselves (perhaps using (if this equals that...)) and taking a lot more time and effort than just using these pre-existing libraries. Moq's libraries also allowed us to easily code in Mock services / controllers, which meant we didn't need to re implement all the code for those things just to test it, and instead we can use a Mock object to do so.


