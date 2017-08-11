using System.Threading.Tasks;
using WebApp.Models;

namespace Data
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task ExecuteAsync(ApplicationDbContext context, TCommand command);
    }
}