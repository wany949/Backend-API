namespace BackendAPI.Testing
{
    public class Tests
    {
        

        [TestFixture]
        public class FoodServiceTest
        {
            private FoodController _foodController;

            [SetUp]
            public void Setup()
            {
                var mockClientFactory = new Mock<IHttpClientFactory>();
                var mockService = new Mock<IFoodService>();
                Food food1 = new Food() { Name = "Eggs", Description = "Fluffy eggs", Effect = "Boosts ATK by 150", Rarity = 2, Type = "Boosts ATK" };
                Food food2 = new Food() { Name = "Pancake", Description = "Fluffy pancakes", Effect = "Boosts DEF by 150", Rarity = 3, Type = "Boosts DEF" };
                Food food3 = new Food() { Name = "Pork Bao", Description = "Steaming pork bao", Effect = "Boosts CRIT by 150", Rarity = 4, Type = "Boosts CRIT" };
                Food food4 = new Food() { Name = "Dumplings", Description = "Doughy dumplings", Effect = "Boosts ATK by 100", Rarity = 3, Type = "Boosts ATK" };
                Food food5 = new Food() { Name = "Potato", Description = "Home grown potato", Effect = "Boosts DEF by 50", Rarity = 1, Type = "Boosts DEF" };
                Food food6 = new Food() { Name = "Lasagna", Description = "Mum's homemade lasagna", Effect = "Boosts ATK by 300", Rarity = 4, Type = "Boosts ATK" };

                IEnumerable<Food> foodList = new List<Food> { food1, food2, food3, food4, food5, food6 };
                foreach (Food food in foodList)
                {
                    mockService.Setup(service => service.AddFood(food));
                }
                
                var httpClient = new HttpClient();
                mockClientFactory.Setup(httpClient => httpClient.CreateClient("genshin")).Returns(httpClient);

                IHttpClientFactory clientFactory = mockClientFactory.Object;
                IFoodService foodService = mockService.Object;
                _foodController = new FoodController(clientFactory, foodService);

                //mockService.Setup(service => service.GetAllFood()).Returns(foodList);
            }

            [Test(Description = "Check if the GetAll in Controller works as expected with substitute values")]
            public void TestGetAll()
            {
                Food food1 = new Food() { Name = "Eggs", Description = "Fluffy eggs", Effect = "Boosts ATK by 150", Rarity = 2, Type = "Boosts ATK" };
                Food food2 = new Food() { Name = "Pancake", Description = "Fluffy pancakes", Effect = "Boosts DEF by 150", Rarity = 3, Type = "Boosts DEF" };
                Food food3 = new Food() { Name = "Pork Bao", Description = "Steaming pork bao", Effect = "Boosts CRIT by 150", Rarity = 4, Type = "Boosts CRIT" };
                Food food4 = new Food() { Name = "Dumplings", Description = "Doughy dumplings", Effect = "Boosts ATK by 100", Rarity = 3, Type = "Boosts ATK" };
                Food food5 = new Food() { Name = "Potato", Description = "Home grown potato", Effect = "Boosts DEF by 50", Rarity = 1, Type = "Boosts DEF" };
                Food food6 = new Food() { Name = "Lasagna", Description = "Mum's homemade lasagna", Effect = "Boosts ATK by 300", Rarity = 4, Type = "Boosts ATK" };

                ActionResult<IEnumerable<Food>> expectedFoodList = new List<Food> { food1, food2, food3, food4, food5, food6 };

                ActionResult<IEnumerable<Food>> controllerFoodList = _foodController.GetAll();
                Assert.That(expectedFoodList, Is.EqualTo(controllerFoodList));

            }

            [Test]
            public void TestGet()
            {
                ActionResult<Food> expectedFood = new Food() { Name = "Pork Bao", Description = "Steaming pork bao", Effect = "Boosts CRIT by 150", Rarity = 4, Type = "Boosts CRIT" };
                ActionResult<Food> controllerFood = _foodController.Get("Pork bao");
                Assert.That(expectedFood.ToString(), Is.EqualTo(controllerFood.ToString()));
            }

            [Test(Description = "Checking special diacritics and spaces / hyphens are replaced correctly")]
            [TestCase("Adventurer's Breakfast Sandwich", ExpectedResult="adventurers-breakfast-sandwich")]
            [TestCase("Berry & Mint Burst", ExpectedResult = "berry-mint-burst")]
            [TestCase("Sautéed Matsutake", ExpectedResult = "sauteed-matsutake")]
            [TestCase("Lantern Rite Special Triple-Layered Consommé", ExpectedResult = "lantern-rite-special-triple-layered-consomme")]
            public string TestReplace(string name)
            {
                return _foodController.replaceName(name);
            }

            [Test]
            public void TestPost()
            {
                Food expectedFood = new Food() { Name = "Rice", Description = "Steamy rice", Effect = "Boosts DEF by 10", Rarity = 1, Type = "Boosts DEF" };
                _foodController.Create(expectedFood);

                ActionResult<Food> controllerFood = _foodController.Get("Rice");
                Assert.That(expectedFood.ToString(), Is.EqualTo(controllerFood.ToString()));
            }
        }

        
    }
}