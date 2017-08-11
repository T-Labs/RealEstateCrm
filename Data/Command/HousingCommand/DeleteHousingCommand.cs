using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Command
{
    public class DeleteHousingCommand: ICommand
    {
        public DeleteHousingCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
