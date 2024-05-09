namespace WebDemo.Services
{
    public class ChangeNameService
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        public ChangeNameService(WebDemoDatabase database , JwtTokenService jwtService)
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
        //public string ChangeAddress(string address)
        //{
        //    var id = jwtService.GetId();
        //    var user = database.Users.FirstOrDefault(x => x.Id == id);
        //    if (user == null)
        //    {
        //        return "No have this user";
        //    }
        //    user.Address = address;
        //    database.Users.Update(user);
        //    database.SaveChanges();
        //    return "Changed Complete";
        //}
    }
}
