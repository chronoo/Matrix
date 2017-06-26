namespace Parser.Model
{
    public class Teacher
    {
        public string sn;
        public string givenName;
        public string initials;
        public string cn;
        public string mail;
        public string[] title;
        public string uid;        
        public string displayName;
        public bool isActive;
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
