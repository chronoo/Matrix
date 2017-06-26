using Parser.Model;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using JWT;
using JWT.Serializers;
using System;

namespace Parser.View
{
    public class JSONConverter
    {
        private static IJsonSerializer _serializer = new JsonNetSerializer();
		private string _peopleURL { get; set; }
		private string _groupURL { get; set; }
		public JSONConverter(string peopleURL, string groupURL)
		{
			_peopleURL = peopleURL;
			_groupURL = groupURL;
		}
		public static T GetObject<T>(string JSON)
        {
            if(JSON ==null)
            {
                throw new ArgumentNullException(nameof(JSON));
            }
            T stud = _serializer.Deserialize<T>(JSON);
            return stud;
        }
        public static T GetObjectFromURL<T>(string URL)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(URL);
            T stud = _serializer.Deserialize<T>(json);
            return stud;
        }
        public bool IsStudent(string ID)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(_peopleURL + ID);
            if (json.Contains("Студент"))
                return true;
            else
                return false;
        }
        public Student GetStudentFromID(string ID)
        {
            return GetObjectFromURL<Student>(_peopleURL + ID);
        }
        public Group GetGroupFromID(int ID)
        {
            return GetObjectFromURL<Group>(_groupURL + ID.ToString());
        }
        public Teacher GetTeacherFromID(string ID)
        {
            return GetObjectFromURL<Teacher>(_peopleURL + ID);
        }
        public  List<Group> GetGroupsList()
        {
            List<Group> array = new List<Group>(GetObjectFromURL<GroupList>(_groupURL)._embedded.groups);
            return array;
        }
        public List<Group> GetFirstCourseGroupList()
        {
            List<Group> array = GetGroupsList();
            int currentYear = DateTime.Now.Year%100;
            if(DateTime.Now.Month<9)
            {
                currentYear--;
            }
            return array.FindAll(x=>x.name.Contains(currentYear.ToString()) && x.type=="Бакалавриат");//исправить
        }
	}
}
