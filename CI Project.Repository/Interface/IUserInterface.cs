using CI_Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Project.Repository.Interface
{
    public interface IUserInterface
    {
        public User UserByEmail(String mail);
        public User UserByEmail_Password(String mail,String Password);

        public PasswordReset ResetPass(string email, string token);

        public List<Mission> missionlist();
        public List<User> users();
        public List<MissionRating> missionratingslist();

        public List<City> cities();
        public List<Country> countries();
        public List<MissionTheme> themes();
        public List<Skill> skills();
        public List<GoalMission> goalMissions();
        public List<MissionRating> missionRatings();
        public List<FavoriteMission> favoriteMissions ();
        public List<MissionApplication> missionApplications (); 
        public List<Comment> comments(); 

        public void adduser (User user);
        public void comment (Comment comment);


        public void apply(long missionid, long userid);
        public List<FavoriteMission> Addfavorite(long missionid, long id);
        public List<FavoriteMission> removefavorite(long missionid, long id);

        public void AddUserSkills(long SkillId, int UserId);

        public void updateuser(User user);

        public List<UserSkill> skilllist(int userid);
        public ContactU addContactUs(string subject, string message, string username, string email);

        public User UserExist(string Email);

        public User addUser(string avtar, string firstName, string lastName, string email, string password, string empid, string department, long cityId, long countryId, string ProfileText, int status);
        public User updateUser(string avtar,string firstName, string lastName, string email, string password, string empid, string department, long cityId, long countryId, string ProfileText, int status, long userId);

        public Mission addMission(string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill);

        public Mission updateMission(long missionId, string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill);

        //public List<PasswordReset> passwordreset();
        //public List<User> Users();
    }
}
