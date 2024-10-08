using City.info.api.Models;

namespace City.info.api
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesDataStore current { get; set; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>
            {
                new CityDto(){

                    Id=1,
                    Name="amol",
                    Description="this is a city",
                    PointOfInterest=new List<PointOfInterestDto>(){ 
                       new PointOfInterestDto(){
                           Id=1,
                           Name="this is point 1",
                           Description="Description for point 1"
                       } ,
                       new PointOfInterestDto(){
                           Id=2,
                           Name="this is point 2",
                           Description="Description for point 2"
                       }
                    },

                },
                new CityDto(){
                    Id=2,
                    Name="babol",
                    Description="this is a city",
                    PointOfInterest=new List<PointOfInterestDto>(){
                       new PointOfInterestDto(){
                           Id=3,
                           Name="this is point 3",
                           Description="Description for point 3"
                       },
                       new PointOfInterestDto(){
                           Id=4,
                           Name="this is point 4",
                           Description="Description for point 4"
                       }
                    },


                },
                new CityDto(){
                    Id=3,
                    Name="noshahr",
                    Description="this is a city" ,
                    PointOfInterest=new List<PointOfInterestDto>(){
                       new PointOfInterestDto(){
                           Id=5,
                           Name="this is point 5",
                           Description="Description for point 5"
                       },
                       new PointOfInterestDto(){
                           Id=6,
                           Name="this is point 6",
                           Description="Description for point 6"
                       }
                    },
                },
            };
        }
    }
}
