using System;
using System.Collections.Generic;
using System.Text;

namespace CarTest
{
    class Productcheck : Ipro
    {
        public bool runninglow(int item)
        {
            if (item < 50)
                return true;
            return false;
        }

    }
}
