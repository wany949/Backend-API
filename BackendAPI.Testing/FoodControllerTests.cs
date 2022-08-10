namespace BackendAPI.Testing
{
    public class Tests
    {


        [TestFixture]
        public class FoodControllerTests
        {
            private FoodController _foodController;

            [SetUp]
            public void Setup()
            {
                var mockClientFactory = new Mock<IHttpClientFactory>();
                var mockService = new Mock<IFoodService>();
                Food food1 = new() { Name = "Eggs", Description = "Fluffy eggs", Effect = "Boosts ATK by 150", Rarity = 2, Type = "Boosts ATK" };
                Food food2 = new() { Name = "Pancake", Description = "Fluffy pancakes", Effect = "Boosts DEF by 150", Rarity = 3, Type = "Boosts DEF" };
                Food food3 = new() { Name = "Pork Bao", Description = "Steaming pork bao", Effect = "Boosts CRIT by 150", Rarity = 4, Type = "Boosts CRIT" };
                Food food4 = new() { Name = "Dumplings", Description = "Doughy dumplings", Effect = "Boosts ATK by 100", Rarity = 3, Type = "Boosts ATK" };
                Food food5 = new() { Name = "Potato", Description = "Home grown potato", Effect = "Boosts DEF by 50", Rarity = 1, Type = "Boosts DEF" };
                Food food6 = new() { Name = "Lasagna", Description = "Mum's homemade lasagna", Effect = "Boosts ATK by 300", Rarity = 4, Type = "Boosts ATK" };

                IList<Food> foodList = new List<Food> { food1, food2, food3, food4, food5, food6 };

                mockService.Setup(service => service.GetAllFood()).Returns(foodList);
                mockService.Setup(service => service.GetFoodByName(It.IsAny<string>())).Returns((string name) => foodList.Where(x => x.Name == name).First());
                mockService.Setup(service => service.AddFood(It.IsAny<Food>())).Callback<Food>((f) => foodList.Add(f));
                mockService.Setup(service => service.RemoveFood(It.IsAny<Food>())).Callback<Food>((f) => foodList.Remove(f));
                mockService.Setup(service => service.UpdateFood(It.IsAny<Food>())).Callback<Food>((f) =>
                {
                    Food existingFood = foodList.FirstOrDefault(f1 => f1.Name == f.Name);
                    var index = foodList.IndexOf(existingFood);
                    foodList[index] = f;
                });

                List<Food> meal = new();
                if (foodList.Count() != 0)
                {
                    int totalFoodCount = foodList.Count();
                    Random random = new();
                    for (int i = 0; i < 3; i++)
                    {
                        int randomInRange = random.Next(0, totalFoodCount);
                        Food f = foodList.Skip(randomInRange).Take(1).First();
                        meal.Add(f);
                    }
                }
                mockService.Setup(service => service.Get3CourseMeal()).Returns(meal);

                var httpClient = new HttpClient();
                mockClientFactory.Setup(httpClient => httpClient.CreateClient("genshin")).Returns(httpClient);

                IHttpClientFactory clientFactory = mockClientFactory.Object;
                IFoodService foodService = mockService.Object;
                _foodController = new FoodController(clientFactory, foodService);

            }

            private static IEnumerable<TestCaseData> FoodData
            {
                get
                {
                    Food food1 = new() { Name = "Eggs", Description = "Fluffy eggs", Effect = "Boosts ATK by 150", Rarity = 2, Type = "Boosts ATK" };
                    Food food2 = new() { Name = "Pancake", Description = "Fluffy pancakes", Effect = "Boosts DEF by 150", Rarity = 3, Type = "Boosts DEF" };
                    Food food3 = new() { Name = "Pork Bao", Description = "Steaming pork bao", Effect = "Boosts CRIT by 150", Rarity = 4, Type = "Boosts CRIT" };
                    Food food4 = new() { Name = "Dumplings", Description = "Doughy dumplings", Effect = "Boosts ATK by 100", Rarity = 3, Type = "Boosts ATK" };
                    Food food5 = new() { Name = "Potato", Description = "Home grown potato", Effect = "Boosts DEF by 50", Rarity = 1, Type = "Boosts DEF" };
                    Food food6 = new() { Name = "Lasagna", Description = "Mum's homemade lasagna", Effect = "Boosts ATK by 300", Rarity = 4, Type = "Boosts ATK" };
                    yield return new TestCaseData("Eggs", food1.Name, food1.Description);
                    yield return new TestCaseData("Pancake", food2.Name, food2.Description);
                    yield return new TestCaseData("Pork Bao", food3.Name, food3.Description);
                    yield return new TestCaseData("Dumplings", food4.Name, food4.Description);
                    yield return new TestCaseData("Potato", food5.Name, food5.Description);
                    yield return new TestCaseData("Lasagna", food6.Name, food6.Description);
                }
            }



            [Test(Description = "Checking special diacritics and spaces / hyphens are replaced correctly")]
            [TestCase("Adventurer's Breakfast Sandwich", ExpectedResult = "adventurers-breakfast-sandwich")]
            [TestCase("Berry & Mint Burst", ExpectedResult = "berry-mint-burst")]
            [TestCase("Sautéed Matsutake", ExpectedResult = "sauteed-matsutake")]
            [TestCase("Lantern Rite Special Triple-Layered Consommé", ExpectedResult = "lantern-rite-special-triple-layered-consomme")]
            public string TestReplace(string name)
            {
                return _foodController.ReplaceName(name);
            }

            [Test]
            public void TestGetAllFood()
            {
                IEnumerable<Food> foodList = _foodController.GetAll();

                Assert.That(foodList, Is.InstanceOf<IEnumerable<Food>>());
                
                // Prints out each food name
                if (foodList is IEnumerable<Food>)
                {
                    foreach (Food food in foodList)
                    {
                        TestContext.WriteLine(food.Name);
                    }
                }
            }

            [Test, TestCaseSource(nameof(FoodData))]
            public void TestGet(string name, string expectedName, string expectedDesc)
            {
                ActionResult<Food> actionFood = _foodController.Get(name);
                var resultFood = actionFood.Result as OkObjectResult;
                var food = actionFood.Value;

                Assert.Multiple(() =>
                {
                    Assert.That(food, Is.InstanceOf<Food>());
                    Assert.That(food.Name, Is.EqualTo(expectedName));
                    Assert.That(food.Description, Is.EqualTo(expectedDesc));
                });  
            }

            [Test]
            public void TestCreate()
            {
                Food newFood = new() { Name = "Steak", Description = "Sizzling steak", Effect = "Boosts ATK by 200", Rarity = 3, Type = "Boosts ATK" };
                _foodController.Create(newFood);

                ActionResult<Food> actionFood = _foodController.Get("Steak");
                var resultFood = actionFood.Result as OkObjectResult;
                var food = actionFood.Value;

                Assert.Multiple(() =>
                {
                    Assert.That(food, Is.InstanceOf<Food>());
                    Assert.That(newFood.Description, Is.EqualTo(food.Description));
                }); 
            }

            [Test]
            public void TestDelete()
            {
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(6, _foodController.GetAll().Count());
                    _foodController.Delete("Eggs");
                    Assert.AreEqual(5, _foodController.GetAll().Count());
                }); 
            }

            [Test]
            public void TestUpdate()
            {
                Food updateFood = new() { Name = "Potato", Description = "Home grown potato, big and round", Effect = "Boosts DEF by 100", Rarity = 2, Type = "Boosts DEF" };
                _foodController.Update(updateFood.Name, updateFood);

                ActionResult<Food> actionFood = _foodController.Get("Potato");
                var resultFood = actionFood.Result as OkObjectResult;
                var food = actionFood.Value;

                Assert.Multiple(() =>
                {
                    Assert.That(updateFood.Description, Is.EqualTo(food.Description));
                    Assert.That(updateFood.Effect, Is.EqualTo(food.Effect));
                    Assert.That(updateFood.Rarity, Is.EqualTo(food.Rarity));
                });
                
            }

            [Test]
            public void Test3RandomFood()
            {
                List<Food> meal = _foodController.Get3CourseMeal();
                foreach (Food food in meal)
                {
                    TestContext.WriteLine(food.Name);
                }
                Assert.That(meal.Count, Is.EqualTo(3));
            }
        }
    }
}