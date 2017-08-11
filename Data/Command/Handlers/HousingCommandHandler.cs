using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;
using WebApp.Models;

namespace Data.Command.Handlers
{
    public class HousingCommandHandler: 
        ICommandHandler<UpdateHousingCommand>,
        ICommandHandler<DeleteHousingCommand>,
        ICommandHandler<AddHousingCommand>
    {
        public async Task ExecuteAsync(ApplicationDbContext context, UpdateHousingCommand command)
        {
            var dbItem = command.Housing;
            if (context.Entry(dbItem).State == EntityState.Detached)
            {
                context.Housing.Attach(dbItem);
                context.Entry(dbItem).State = EntityState.Modified;
            }
            
            context.Update(dbItem);
            await context.SaveChangesAsync();
        }

        public Task ExecuteAsync(ApplicationDbContext context, DeleteHousingCommand command)
        {
            var item = new Housing(){Id = command.Id};
            context.Set<Housing>().Attach(item);
            context.Housing.Remove(item);
            return context.SaveChangesAsync();
        }

        public Task ExecuteAsync(ApplicationDbContext context, AddHousingCommand command)
        {
            context.Housing.Add(command.Item);
            return context.SaveChangesAsync();
        }
    }
}
