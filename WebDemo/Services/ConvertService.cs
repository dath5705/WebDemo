using Microsoft.AspNetCore.Mvc;
using WebDemo.Models;
using WebDemo.Result;

namespace WebDemo.Services
{
    public class ConvertService
    {
        public List<UserResult> ConvertUser(List<User> users)
        {

            List<UserResult> result = users.Select(x =>
            {
                UserResult user = new()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    RoleId = x.RoleId,
                    Name = x.Name,
                    SexId = x.SexId,
                    DateOfBirth = x.DateOfBirth,
                    InformationResult = x.Informations?.Select(e =>
                    {
                        InformationResult infor = new()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Number = e.Number,
                            Address = e.Address,
                            UserId = e.UserId,
                        };
                        return infor;
                    }).ToList(),
                };
                return user;
            }).ToList();
            return result;
        }
    }
}
