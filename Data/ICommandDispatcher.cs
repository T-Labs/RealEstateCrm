using System.Threading.Tasks;

namespace Data
{
    public interface ICommandDispatcher
    {
        Task ExecuteAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}