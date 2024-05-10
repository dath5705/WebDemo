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
    }
}
