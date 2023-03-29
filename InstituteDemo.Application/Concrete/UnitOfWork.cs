using InstituteDemo.Application.Interfaces.Common;
using InstituteDemo.Domain.Entities;
using InstituteDemo.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace InstituteDemo.Application.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            courseRepository = new Repository<Course>(context);
            studentRepository = new Repository<Student>(context);
        }
        public IRepository<Student> studentRepository { get; }
        public IRepository<Course> courseRepository { get; }

        public async Task<int> CompletedAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
