using SummerCamp_XF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp_XF.Data
{
    interface ICamperRepository
    {
        Task<List<Camper>> GetCampers();
        Task<Camper> GetCamper(int ID);
        Task<List<Camper>> GetCampersByCompound(int CompoundID);
        Task AddCamper(Camper camperToAdd);
        Task UpdateCamper(Camper camperToUpdate);
        Task DeleteCamper(Camper camperToDelete);
    }
}
