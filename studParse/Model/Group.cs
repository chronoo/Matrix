namespace Parser.Model
{
    public class Group
    {
        public int id;
        public string name;
        public string curatorUid;
        public string headUid;
        public bool finishedEducation;
        public string type;
        public class Links
        {
            public Self self;
            public class Self
            {
                public string href;
            }
        }
        public class Embedded
        {
            public Student[] students;
        }
        public Embedded _embedded;
        public string[] curator;
        public string[] head;
    }
}