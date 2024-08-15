using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ModelViews.Users
{
    public class UserPageDto
    {
        public List<UserDto> Users { get; set; }
        public int TotalPage { get; set; }
        public int PageNum { get; set; }
    }
}
