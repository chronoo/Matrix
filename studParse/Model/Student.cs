using System.Collections.Generic;

namespace Parser.Model
{
    public class Student
    {
		//public string title;//Это поле может быть как единичным значением, так и массивом. Я не знаю, что с ним делать
		public string uid;
        public string[] mail;
        public string displayName;
        public string sn;
        public string givenName;
        public bool isActive;
        public string birthDate;
        public Links _links;
        public class Links
        {
            public Self self;
            public Self profile;
            public Groups[] groups;
            public class Self
            {
                public string href;
            }
            public class Groups
            {
                public int id;
                public string name;
                public GroupLink links;
                public class GroupLink
                {
                    public string self;
                }
            }
        }
    }
}
