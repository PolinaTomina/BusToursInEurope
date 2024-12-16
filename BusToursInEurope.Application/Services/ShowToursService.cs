using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusToursInEurope.Application.Services
{
    public class ShowToursService : IShowTours
    {
        public Task<List<Tour>> GetTopToursAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Tour>> GetToursAsync(ToursFilter toursFilter)
        {
            throw new NotImplementedException();
        }
    }
}
