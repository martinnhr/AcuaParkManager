using AcuaParkShared;

namespace AcuaParkAPI.Services
{
    public class ConnectionString : IConnectionString
    {
        
        private readonly IConfiguration _configuration;

        public ConnectionString(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GetConectionString()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            return Task.FromResult(connectionString);
        }

    }
}
