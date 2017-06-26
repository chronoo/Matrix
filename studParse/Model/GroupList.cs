using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Model
{
    public class GroupList
    {
        public class Embedded
        {
            public Group[] groups;
        }
        public Embedded _embedded;
    }
}