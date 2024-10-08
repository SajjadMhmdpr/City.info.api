using City.info.api.DbContexts;
using City.info.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace City.info.api.Repositories
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoDbContext _context;
        public CityInfoRepository(CityInfoDbContext context) 
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }


        public async Task<bool> CityExistAsync(int cityId)
        {
            return await  _context.Cities.AnyAsync(c=>c.Id == cityId);
        }

        public async Task<IEnumerable<Entities.City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Entities.City?> GetCityAsync(int cityId,bool includePointOfInterest)
        {
            if(includePointOfInterest)
            return await _context.Cities.Include(c=>c.PointOfInterest).FirstOrDefaultAsync(c=>c.Id==cityId);
            else
                return await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);

        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterest)
        {
            return await _context.PointsOfInterest.FirstOrDefaultAsync(p=>p.CityId==cityId&&p.Id==pointOfInterest);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest.Where(p=>p.CityId==cityId).ToListAsync();
        }

        public async Task AddPointOfInterestAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId,false);
            if (city != null)
            {
                city.PointOfInterest.Add(pointOfInterest);
            }
            
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> PointOfInterestExistAsync(int cityId, int pointId)
        {
            return await _context.PointsOfInterest.AnyAsync(p => p.Id==pointId&&p.CityId== cityId);
        }

        public async Task<PointOfInterest> UpdatePointOfInterestAsync(int cityId, int pointId,
            PointOfInterest pointOfInterest)
        {
            var point = await GetPointOfInterestForCityAsync(cityId,pointId);
            point.Name = pointOfInterest.Name ?? "change to null";
            point.Description = pointOfInterest.Description;

            return point;
        }

        public void DeletePointOfInterestAsync(PointOfInterest point)
        {
             _context.PointsOfInterest.Remove(point);
        }
    }
}
