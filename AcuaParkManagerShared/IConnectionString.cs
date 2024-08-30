namespace AcuaParkShared
{
    public interface IConnectionString
    {
        Task<string> GetConectionString();
    }
}
