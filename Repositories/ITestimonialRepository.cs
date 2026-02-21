using modernportfolio.Models;

namespace modernportfolio.Repositories;

public interface ITestimonialRepository : IGenericRepository<Testimonial>
{
    Task<IEnumerable<Testimonial>> GetActiveTestimonialsAsync();
}
