using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebDemo.Services
{
    public class ChangeInformationService
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        public ChangeInformationService(WebDemoDatabase database, JwtTokenService jwtService)
        {
            this.database = database;
            this.jwtService = jwtService;
        }

        public string ChangeName(string name)
        {
            var id = jwtService.GetId();
            var user = database.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return "No have this user";
            }
            user.Name = name;
            database.Users.Update(user);
            database.SaveChanges();
            return "Changed Complete";
        }
        public string ChangeSex(int sexId)
        {
            var id = jwtService.GetId();
            var user = database.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return "No have this user";
            }
            user.SexId = sexId;
            database.Users.Update(user);
            database.SaveChanges();
            return "Changed Complete";
        }

        public string ChangeDateOfBirth(DateTime date)
        {
            var id = jwtService.GetId();
            var user = database.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return "No have this user";
            }
            user.DateOfBirth = date;
            database.Users.Update(user);
            database.SaveChanges();
            return "Changed Complete";
        }
        public string ChangeNameInfor(string name, int id)
        {
            var userId = jwtService.GetId();
            var inforList = database.Informations.Where(x => x.UserId == userId).ToList();
            if (inforList == null)
            {
                return "You dont have address. please add your address";
            }
            var infor = inforList.Skip(id - 1).Take(1).FirstOrDefault();
            if (infor == null)
            {
                return "No have this user";
            }
            infor.Name = name;
            database.Informations.Update(infor);
            database.SaveChanges();
            return "Change Complete";
        }
        public string ChangeNumber(string number, int id)
        {
            var userId = jwtService.GetId();
            var inforList = database.Informations.Where(x => x.UserId == userId).ToList();
            if (inforList == null)
            {
                return "You dont have address. please add your address";
            }
            var infor = inforList.Skip(id - 1).Take(1).FirstOrDefault();
            if (infor == null)
            {
                return "No have this user";
            }
            infor.Number = number;
            database.Informations.Update(infor);
            database.SaveChanges();
            return "Change Complete";
        }
        public string ChangeAddress(string address, int id)
        {
            var userId = jwtService.GetId();
            var inforList = database.Informations.Where(x => x.UserId == userId).ToList();
            if (inforList == null)
            {
                return "You dont have address. please add your address";
            }
            var infor = inforList.Skip(id - 1).Take(1).FirstOrDefault();
            if (infor == null)
            {
                return "No have this user";
            }
            infor.Address = address;
            database.Informations.Update(infor);
            database.SaveChanges();
            return "Change Complete";
        }
    }
}
