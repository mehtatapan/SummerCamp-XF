using SummerCamp_XF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp_XF.Data
{
    interface ICompoundRepository
    {
        Task<List<Compound>> GetCompounds();
        Task<Compound> GetCompound(int ID);
        Task AddCompound(Compound CompoundToAdd);
        Task UpdateCompound(Compound CompoundToUpdate);
        Task DeleteCompound(Compound CompoundToDelete);
    }
}
