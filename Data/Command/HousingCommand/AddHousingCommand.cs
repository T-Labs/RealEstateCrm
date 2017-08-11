using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Entities;

namespace Data.Command
{
    public class AddHousingCommand: ICommand
    {
        public AddHousingCommand(Housing item)
        {
            Item = item;
        }

        public Housing Item { get; }
    }
}
