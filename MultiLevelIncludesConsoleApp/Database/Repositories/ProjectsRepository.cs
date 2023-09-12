using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SpecificationPatternConsoleApp.Database.Entites;

namespace SpecificationPatternConsoleApp.Database.Repositories
{
    public interface IProjectsRepository
    {
        Task<List<ProjectEntity>> GetAllAsync(Expression<Func<ProjectEntity, object>>[] includes = null);
        Task<ProjectEntity> GetAsync(Guid projectId);
    }

    public class ProjectsRepository : IProjectsRepository
    {
        private readonly Context _context;

        public ProjectsRepository()
        {
            var options = new DbContextOptionsBuilder<Context>()
                  .UseInMemoryDatabase("Projects")
                  .Options;

              _context = new Context(options);
        }

        public async Task<List<ProjectEntity>> GetAllAsync(Expression<Func<ProjectEntity, object>>[] includes = null)
        {
            IQueryable<ProjectEntity> projects = _context.Projects.AsNoTracking();

            if ( includes != null )
            {
                foreach (var include in includes)
                {
                    projects = projects.Include(include.AsPath());
                }                            
            }

            return await projects.ToListAsync();
        }

        public async Task<ProjectEntity> GetAsync(Guid projectId)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == projectId);
        }
    }
}