using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public class WeatherForecastRepository
    {
        private readonly string sqlConnectionString;

        public WeatherForecastRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<WeatherForecast> InsertAsync(WeatherForecast weatherForecast)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var environmentId = await sqlConnection.ExecuteAsync("INSERT INTO [WeatherForecast] (Id, TemperatureC, Summary) VALUES (@Id, @TemperatureC, @Summary)", weatherForecast);
                return weatherForecast;
            }
        }

        public async Task<WeatherForecast?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<WeatherForecast>("SELECT * FROM [WeatherForecast] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<WeatherForecast>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<WeatherForecast>("SELECT * FROM [WeatherForecast]");
            }
        }

        public async Task UpdateAsync(WeatherForecast environment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [WeatherForecast] SET " +
                                                 "TemperatureC = @TemperatureC, " +
                                                 "Summary = @Summary"
                                                 , environment);

            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [WeatherForecast] WHERE Id = @Id", new { id });
            }
        }

    }
}