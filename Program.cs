using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ДЗ_с_студентом_СиШарп_
{
    public class Address
    {
        private string country;
        private string city;
        private string street;
        private int house;

        private static List<string> strana = new List<string>();

        private static void stranaList()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);
                if (!(strana.Contains(region.EnglishName)))
                {
                    strana.Add(region.EnglishName);
                }
            }
        }

        public Address() : this("Ukraine", "Odessa", "", 0) { }

        public Address(string country, string city, string street, int houseNum)
        {
            stranaList();
            setStrana(country);
            setCity(city);
            setStreet(street);
            setHouse(houseNum);
        }

        public void setStrana(string country)
        {
            this.country = (strana.Contains(country)) ? country : "Ukraine";
        }
        public void setCity(string city)
        {
            this.city = (city.Length >= 3) ? city : "Odessa";
        }
        public void setStreet(string street)
        {
            this.street = (street.Length >= 3) ? street : "(???)";
        }
        public void setHouse(int houseN)
        {
            this.house = (houseN > 0 && houseN <= 1000) ? houseN : 0;
        }
        public string AllInfo()
        {
            return (city + ", " + country + ", st." + street + " " + house);
        }
    }

    public class Person
    {
        private string name;
        private string famil;
        private DateTime birth;
        private Address address;
        private string phone;

        public Person() : this("Name1", "Famil1")
        { }
        public Person(string name, string famil) : this(name, famil, new DateTime(), new Address())
        {

        }
        public Person(string name, string famil, DateTime birth, Address address, string phone = "")
        {
            Name = name;
            Famil = famil;
            Birth = birth;
            Address = address;
            Phone = phone;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Famil
        {
            get { return famil; }
            set { famil = value; }
        }
        public DateTime Birth
        {
            get { return birth; }
            set { birth = value; }
        }
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public void infoPers()
        {
            Console.WriteLine("Name: " + name + "\nFamil: " + famil + 
                "\nBirth: " + birth.Day + "." + birth.Month + "." + birth.Year + 
                "\nAddress: " + address + "\nPhone: " + phone);
        }
    }

    public class Student:Person,IComparable
    {
        private int id;

        private List<int> zachet;
        private List<int> dz;
        private List<int> ekz;

        private static int ID = 0;

        public Student() : this("Name1", "Famil1")
        {
        }

        public Student(string name, string famil) : this(name, famil, DateTime.Now, new Address())
        {
        }
        public Student(string name, string famil, DateTime birth, Address address, string phone = "???") : base(name, famil,birth,address,phone)
        {

            zachet = new List<int>();
            dz = new List<int>();
            ekz = new List<int>();

            id += ++ID;

        }

        public int IDProper
        {
            get { return id; }
        }
        public DateTime birthProper
        {
            get { return base.Birth; }
            set { base.Birth = new DateTime(base.Birth.Year, base.Birth.Month, base.Birth.Year); }
        }

        public int getID()
        {
            return id;
        }
        public void setZachet(int zachet1)
        {
            zachet.Add(zachet1);
        }
        public void setDZ(int dz1)
        {
            dz.Add(dz1);
        }
        public void setEkz(int ekz1)
        {
            ekz.Add(ekz1);
        }

        public int sredZachet()
        {
            int result = 0;
            foreach (int i in zachet)
            {
                result += i;
            }
            return result / zachet.Count;
        }
        public int sredDZ()
        {
            int result = 0;
            foreach (int i in dz)
            {
                result += i;
            }
            return result / dz.Count;
        }
        public int sredEkz()
        {
            int result = 0;
            foreach (int i in ekz)
            {
                result += i;
            }
            return result / ekz.Count;
        }

        public void AllInfo()
        {
            Console.WriteLine("Информация о студенте: ");
            Console.WriteLine("Айди: " + id);

            Console.WriteLine("Оценки за зачет: ");
            foreach (int zachet1 in zachet)
            {
                Console.WriteLine(zachet1);
            }
            Console.WriteLine("Оценки за ДЗ: ");
            foreach (int dz1 in dz)
            {
                Console.WriteLine(dz1);
            }
            Console.WriteLine("Оценки за экзамен: ");
            foreach (int ekz1 in ekz)
            {
                Console.WriteLine(ekz1);
            }
        }
        public static bool operator ==(Student name,Student name2)
        {
            return name.Name == name2.Name;
        }

        public static bool operator !=(Student name,Student name2)
        {
            return !(name.Name == name2.Name);
        }

        public int CompareTo(object o)
        {
            Student s = o as Student;
            if (sredDZ() < s.sredDZ())
            {
                return -1;
            }
            if (sredDZ() > s.sredDZ())
            {
                return 1;
            }
            return 0;
        }
    }

    class Group
    {
        private List<Student> stud = new List<Student>();
        private string title;
        private string spec;
        private int kurs;

        private void studGen()
        {
            for (int i = 0; i < 5; i++)
            {
                Address address = new Address();
                stud.Add(new Student(Faker.NameFaker.Name(),
                    Faker.NameFaker.LastName(), new DateTime(), address, Faker.PhoneFaker.Phone()));
            }
        }
        public void studCopy(List<Student> stud1)
        {
            foreach (Student stud in stud1)
                this.stud.Add(stud);
        }
        public void getNames(string[] names)
        {
            for (int i = 0; i < stud.Count; i++)
            {
                names[i] = stud[i].Name + " - " + stud[i].Famil + " - " + stud[i].getID();
            }
        }
        public void studDel(Student stud1)
        {
            stud.Remove(stud1);
        }
        public Group(string title, string spec, int kurs)
        {
            setTitle(title);
            setSpec(spec);
            setKurs(kurs);
        }
        public Group() : this("Title1", "Spec1", 1)
        {
            studGen();
        }
        public Group(List<Student> students) : this("Title1", "Spec1", 1, students)
        {
        }
        public Group(Group group) : this(group.getTitle(), group.getSpec(), group.getKurs(), group.getStud())
        {
        }
        public Group(string title, string spec, int kurs, List<Student> students) : this(title, spec, kurs)
        {
            studCopy(students);
        }

        public string titleProper
        {
            get { return title; }
            set { title = value; }
        }
        public string specProper
        {
            get { return spec; }
            set { spec = value; }
        }
        public int kursProper
        {
            get { return kurs; }
            set { kurs = value; }
        }
        public List<Student> studProper
        {
            get { return stud; }
        }


        public string getTitle()
        {
            return title;
        }
        public string getSpec()
        {
            return spec;
        }
        public int getKurs()
        {
            return kurs;
        }
        public List<Student> getStud()
        {
            return stud;
        }

        public void setTitle(string title)
        {
            if (title.Length < 2)
            {
                throw new ArgumentException("Название группы должно быть больше 2 символа!");
            }
            this.title = title;
        }
        public void setSpec(string spec)
        {
            if (spec.Length < 3)
            {
                throw new ArgumentException("Специализация группы должна быть больше чем 3 символа!");
            }
            this.spec = spec;
        }
        public void setKurs(int kurs)
        {
            if (kurs < 1 || kurs > 5)
            {
                throw new ArgumentException("Курс не может быть отрицательным числом или больше 5!");
            }
            this.kurs = kurs;
        }

        public void Info()
        {
            Console.WriteLine("Название: " + title);
            Console.WriteLine("Специализация: " + spec);
            Console.WriteLine("Курс: " + kurs);
        }
        public void sortStud()
        {
            string[] names = new string[stud.Count];
            getNames(names);
            Array.Sort(names);

            Info();
            for (int i = 0; i < stud.Count; i++)
            {
                Console.WriteLine(i + 1 + "." + names[i]);
            }
            Console.WriteLine();
        }
        public void allStudInfo()
        {
            foreach (Student stud in stud)
            {
                Console.WriteLine(stud);
            }
            Console.WriteLine();
        }
        public void addStud(Student student)
        {
            stud.Add(student);
        }
        public Student studById(int id)
        {
            foreach (Student stud1 in stud)
            {
                if (stud1.getID() == id)
                {
                    return stud1;
                }
            }
            throw new Exception("Студента с айди " + id + " не существует");
        }

        public void studIntoGroup(int id, Group group)
        {
            Student stud = studById(id);
            group.addStud(stud);
            studDel(stud);
        }

        public void studDelEkz()
        {
            List<Student> delStud = new List<Student>();
            foreach (Student stud in stud)
            {
                if (stud.sredEkz() < 20)
                {
                    delStud.Add(stud);
                }
            }
            foreach (Student stud in delStud)
            {
                studDel(stud);
            }

        }
        public void badStud()
        {
            if (stud.Count > 0)
            {
                Student loxStud = stud[0];
                int result = (loxStud.sredZachet() + loxStud.sredDZ() + loxStud.sredEkz()) / 3;
                int result1 = 0;
                for (int i = 0; i < stud.Count; i++)
                {
                    result1 = stud[i].sredZachet() + stud[i].sredDZ() + stud[i].sredEkz() / 3;
                    if (result1 < result)
                    {
                        result = result1;
                        loxStud = stud[i];
                    }
                }
                studDel(loxStud);
            }
        }

        public static bool operator ==(Group title, Group title2)
        {
            return title.title == title2.title;
        }

        public static bool operator !=(Group title, Group title2)
        {
            return !(title.title == title2.title);
        }

        public Student this[int ID]
        {
            get { return studById(ID); }
        }
    }
    public class Aspirant:Student
    {
        private string dissert;
        public Aspirant(string name, string famil, string dissert) : this(name, famil, dissert, new DateTime(), new Address())
        {
        }
        public Aspirant(string name, string famil, string dissert, DateTime birth, Address address, string phone = ""):base(name,famil,birth,address,phone)
        {
            Dissert = dissert;
        }
        public string Dissert
        {
            get { return dissert; }
            set { dissert = value; }
        }
        public void dissertInfo()
        {
            Console.WriteLine("Dissertation: " + dissert);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> stud = new List<Student>();
            Random rand = new Random();
            Student stud1 = new Student();
            Student stud2 = new Student("Nikita", "Prigolovkin");
            Student stud3 = new Student("Nikita", "Prigolovkin", new DateTime(2005, 10, 28), new Address(), "12344321");

            Aspirant aspir4 = new Aspirant("AspirantName1", "AspirantFamil1", "Cool Dissertation");

            stud.Add(stud1);
            stud.Add(stud2);
            stud.Add(stud3);

            for (int i = 0; i < 3; i++)
            {
                stud1.setZachet(rand.Next(1, 12));
            }
            for (int i = 0; i < 3; i++)
            {
                stud1.setDZ(rand.Next(1, 12));
            }
            for (int i = 0; i < 3; i++)
            {
                stud1.setEkz(rand.Next(1, 12));
            }

            for (int i = 0; i < 3; i++)
            {
                stud2.setZachet(rand.Next(1, 12));
            }
            for (int i = 0; i < 3; i++)
            {
                stud2.setDZ(rand.Next(1, 12));
            }
            for (int i = 0; i < 3; i++)
            {
                stud2.setEkz(rand.Next(1, 12));
            }

            for (int i = 0; i < 3; i++)
            {
                stud3.setZachet(rand.Next(1, 12));
            }
            for (int i = 0; i < 3; i++)
            {
                stud3.setDZ(rand.Next(1, 12));
            }
            for (int i = 0; i < 3; i++)
            {
                stud3.setEkz(rand.Next(1, 12));
            }

            for (int i = 0; i < 3; i++)
            {
                aspir4.setZachet(rand.Next(1, 12));
            }

            for (int i = 0; i < 3; i++)
            {
                aspir4.setDZ(rand.Next(1, 12));
            }

            for (int i = 0; i < 3; i++)
            {
                aspir4.setEkz(rand.Next(1, 12));
            }

            Group group = new Group("G1", "Povar", 2, stud);
            group.sortStud();
            group.addStud(aspir4);
            group.sortStud();
            Student izmenaStud = group.studById(1);
            group.sortStud();
            Console.WriteLine("\n");


            Group group2 = new Group("G2", "Svarshik", 3);
            group2.sortStud();

            group.studIntoGroup(2, group2);
            group.sortStud();
            group2.sortStud();

            group.studDelEkz();
            group.sortStud();

            group.badStud();
            group.sortStud();

            Student stud5 = new Student();
            try
            {
                stud5 = new Student("Name2", "Famil2", new DateTime(2024, 99, 99), new Address(), "0508885556");
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message + "\n");
            }


            Student stud6 = null;
            try
            {
                group.addStud(stud6);
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message + "\n");
            }

            if (stud1.Name == stud2.Name)
            {
                Console.WriteLine("Имена студентов одинаковы!");
            }
            else
            {
                Console.WriteLine("Имена студентов не одинаковы!");
            }

            if (group.getTitle() == group2.getTitle())
            {
                Console.WriteLine("Имена групп одинаковы!");
            }
            else
            {
                Console.WriteLine("Имена групп не одинаковы!");
            }


            Console.WriteLine("\nИндекс студента: ");
            try
            {
                Console.WriteLine(group2[1]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            List<Student> studs2 = new List<Student>
            {
                new Student("Nikita", "Prigolovkin"),
                new Student("Sasha", "Lopatov"),
                new Student("Vasyan", "Vasyanich"),
                new Student("Fedya", "Babaikin"),
                new Student("Inokentii", "Popygaevich")
            };
            Student stud23 = new Student();
            Console.WriteLine(stud23);
            foreach (Student studLoc in studs2)
            {
                for (int i = 0; i < studs2.Count; i++)
                {
                    studLoc.setDZ(rand.Next(1, 12));
                }
            }
            foreach (Student studloc in studs2)
            {
                Console.WriteLine(studloc);
                Console.WriteLine("DZ: " + studloc.sredDZ());
            }
            Console.WriteLine("\nПосле сортировки: \n");
            studs2.Sort();
            foreach (Student studloc in studs2)
            {
                Console.WriteLine(studloc);
                Console.WriteLine("DZ: " + studloc.sredDZ());
            }
        }
    }
}

