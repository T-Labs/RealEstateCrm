using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Entities;

namespace Data.Command
{
    public class UpdateHousingCommand: ICommand
    {
        public Housing Housing { get; }

        public UpdateHousingCommand(Housing housing)
        {
            Housing = housing;
        }
    }
}
