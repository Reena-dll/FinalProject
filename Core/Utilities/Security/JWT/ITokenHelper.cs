using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {// Eğer şifre ve ismi doğru yazdıysa buraya gelecek.

        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
