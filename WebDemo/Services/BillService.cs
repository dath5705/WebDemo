namespace WebDemo.Services
{
    public class BillService
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        public BillService(WebDemoDatabase database, JwtTokenService jwtService)
        {
            this.database = database;
            this.jwtService = jwtService;
        }
    }
}
