using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Users.Dtos;

namespace Template.Application.Users.Queries.UserDetails
{
    public class GetUserDetailsByIdQuery(string Id):IQuery<UserDetailedDto>
    {
        public string Id { get; set; } = Id;
    }
}
