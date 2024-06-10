using Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Token
{
    public interface IToken
    {
        public string CreateToken(AppUser user);
    }
}
