using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Exceptions
{
    public class NotfoundException(string resourseType,string resourseidentifier) 
        : Exception($"{resourseType} with id:{resourseidentifier} doesn't exist")
    {

    }
    
}
