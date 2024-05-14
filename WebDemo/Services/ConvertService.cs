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
                    Informations = x.Informations?.Select(e =>
                    {
                        InformationResult infor = new()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Number = e.Number,
                            Address = e.Address,
                        };
                        return infor;
                    }).ToList(),
                };
                return user;
            }).ToList();
            return result;
        }
        public UserResult ConvertProduct(User user)
        {
            UserResult results = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                RoleId = user.RoleId,
                Name = user.Name,
                SexId = user.SexId,
                DateOfBirth = user.DateOfBirth,
                Informations = user.Informations?.Select(e =>
                {
                    InformationResult infor = new()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Number = e.Number,
                        Address = e.Address,
                    };
                    return infor;
                }).ToList(),
                Products = user.ProductsInCart?.Select(e =>
                {
                    ProductResult product = new()
                    {
                        
                        ProductId = e.ProductId,
                        ProductName= e.Product?.ProductName,
                        Quantity = e.Quantity,
                        Price = e.Product?.Price,
                    };
                    return product;
                }).ToList(),
            };
            return results;
        }
    }
}
