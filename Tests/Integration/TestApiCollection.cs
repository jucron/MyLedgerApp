namespace Tests.Integration
{
    [CollectionDefinition(IntegrationTestsName)]
    public class TestApiCollection: ICollectionFixture<IntegrationTestsFactory>
    {
        public const string IntegrationTestsName = "ApiTestCollection";
    }
}