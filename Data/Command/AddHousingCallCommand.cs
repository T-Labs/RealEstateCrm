using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Entities;

namespace Data.Command
{
    public class AddHousingCallCommand: ICommand
    {
        public int HousingId { get; }
        public HousingCall HousingCall { get; }

        public AddHousingCallCommand(HousingCall housingCall, int housingId)
        {
            HousingCall = housingCall;
            HousingId = housingId;
        }   
    }
}
