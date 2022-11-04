using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Web.Contracts
{
    public interface ISecurityService
    {
        string HashString(string text, string salt = "");
        bool isEqualPassword(string password, string storedPassword);
    }
}
