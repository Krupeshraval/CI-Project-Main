using CI_Entities.Models;

namespace CI_Project.Models
{
    public class UserCrudViewModel
    {
        public List<User> users { get; set; }   
        //public List<CmsPage> page  { get; set; }   

        public List<Mission> missions { get; set; }

        public List<MissionApplication> missionApplications { get; set; }

        public List<Story> stories { get; set; }

        public List<City> cities { get; set; }

        public List<Country> country { get; set; }

        public long CountryId { get; set; }

        public long CityId { get; set; }


    }
}
