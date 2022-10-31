using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    public interface IOutputService
    {

        void Write(object obj);

        void Write(string message);

        void WriteLine(object obj);

        void WriteLine(string message);
    }
}
