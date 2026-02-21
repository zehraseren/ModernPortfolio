using Dapper;
using Npgsql;
using modernportfolio.Models;

namespace modernportfolio.Repositories;

public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
{
    public TestimonialRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<Testimonial>> GetActiveTestimonialsAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM Testimonials WHERE IsActive = @IsActive";
        var result = await connection.QueryAsync<Testimonial>(sql, new { IsActive = true });
        return result;
    }
}
