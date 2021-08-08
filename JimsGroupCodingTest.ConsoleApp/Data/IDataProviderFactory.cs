namespace JimsGroupCodingTest.ConsoleApp.Data
{
    public interface IDataProviderFactory
    {
        IDataProvider Create(string type);
    }
}