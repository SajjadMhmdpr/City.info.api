using City.info.api.Entities;

namespace City.info.api.Repositories
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<Entities.City>> GetCitiesAsync();
        Task<bool> CityExistAsync(int cityId);
        Task<Entities.City?> GetCityAsync(int cityId, bool includePointOfInterest);
        Task<IEnumerable<PointOfInterest>> 
            GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?>
            GetPointOfInterestForCityAsync(int cityId,int pointOfInterest);
        Task AddPointOfInterestAsync(int cityId , PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
        Task<bool> PointOfInterestExistAsync(int cityId, int pointId);
        Task<PointOfInterest> UpdatePointOfInterestAsync(int cityId, int pointId,PointOfInterest pointOfInterest);
        void DeletePointOfInterestAsync(PointOfInterest point);

    }
}
