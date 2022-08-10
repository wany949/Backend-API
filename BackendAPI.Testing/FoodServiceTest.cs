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
            }

            [Test]
            public void Test1()
            {
                
            }
        }

        
    }
}