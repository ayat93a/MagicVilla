using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repositry.IRepositry;

namespace MagicVilla_VillaAPI.Repositry
{
    public class VillaNoRepositry : Repositry<VillaNumber>, IVillaNoRepositry
    {
        private readonly AppDBContext _db;

        public VillaNoRepositry(AppDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            _db.VillaNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
