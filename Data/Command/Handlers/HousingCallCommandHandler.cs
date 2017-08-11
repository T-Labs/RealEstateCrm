using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace Data.Command.Handlers
{
    public class HousingCallCommandHandler: ICommandHandler<AddHousingCallCommand>
    {
        public Task ExecuteAsync(ApplicationDbContext context, AddHousingCallCommand command)
        {
            context.HousingCalls.Add(command.HousingCall);
            return context.SaveChangesAsync();
        }
    }
}
