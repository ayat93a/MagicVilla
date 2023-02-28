using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repositry.IRepositry
{
    public interface IVillaNoRepositry : IRepositry<VillaNumber> 
    {

        Task <VillaNumber> UpdateAsync(VillaNumber entity);


    }
}
