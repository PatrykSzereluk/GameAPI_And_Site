using System;
using System.Collections.Generic;
using System.Text;

namespace Security
{
    public interface IEncrypter
    {
        string Encrypted(string password);
    }
}
