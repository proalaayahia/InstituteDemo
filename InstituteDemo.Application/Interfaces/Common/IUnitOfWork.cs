using InstituteDemo.Domain.Entities;
using System.Threading.Tasks;

namespace InstituteDemo.Application.Interfaces.Common
{
    public interface IUnitOfWork
    {
        IRepository<Student> studentRepository { get; }
        IRepository<Course> courseRepository { get; }
        Task<int> CompletedAsync(System.Threading.CancellationToken cancellationToken);
    }
}
