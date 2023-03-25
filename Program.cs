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
    class Student
    {
        private int id;
        private string famil;
        private string name;
        private DateTime birth;
        private Address address;
        private string phone;

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
        public Student(string name, string famil, DateTime birth, Address address, string phone = "???")
        {
            setName(name);
            setFamil(famil);
            setBirth(birth);
            setAddress(address);

            zachet = new List<int>();
            dz = new List<int>();
            ekz = new List<int>();

            id += ++ID;

        }

        public int IDProper
        {
            get { return id; }
            set { id = value; }
        }
        public string familProper
        {
            get { return famil; }
            set { famil = value; }
        }
        public string nameProper
        {
            get { return name; }
            set { name = value; }
        }
        public DateTime birthProper
        {
            get { return birth; }
            set { birth = value; }
        }
        public Address addressProper
        {
            get { return address; }
            set { address = value; }
        }
        public string phoneProper
        {
            get { return phone; }
            set { phone = value; }
        }




        public int getID()
        {
            return id;
        }

        public string getName()
        {
            return name;
        }

        public string getFamil()
        {
            return famil;
        }

        public DateTime getBirth()
        {
            return birth;
        }

        public Address getAddress()
        {
            return address;
        }

        public string getPhone()
        {
            return phone;
        }

        public void setName(string name)
        {
            if (name.Length <= 1)
            {
                throw new Exception("Имя должно быть больше 1 символа!");
            }
            this.name = name;
        }
        public void setFamil(string famil)
        {
            if (famil.Length <= 1)
            {
                throw new Exception("Фамилия должна быть больше 1 символа!");
            }
            this.famil = famil;
        }
        public void setBirth(DateTime birth)
        {
            this.birth = birth;
        }
        public void setAddress(Address address)
        {
            this.address = address;
        }
        public void setPhone(string phone)
        {
            if (phone.Length < 9)
            {
                throw new Exception("Номер должен быть больше 9 символов!");
            }
            this.phone = phone;
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
            Console.WriteLine("Имя: " + name);
            Console.WriteLine("Фамилия: " + famil);
            Console.WriteLine("День рождения: " + birth.Day, " ", birth.Month, " ", birth.Year);
            Console.WriteLine("Адрес: " + address);
            Console.WriteLine("Телефон: " + phone);

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
            return name.name == name2.name;
        }

        public static bool operator !=(Student name,Student name2)
        {
            return !(name.name == name2.name);
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
                names[i] = stud[i].getName() + " - " + stud[i].getFamil() + " - " + stud[i].getID();
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
            foreach (Student stud in stud)
            {
                if (stud.getID() == id)
                {
                    return stud;
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

            Student stud4 = new Student("Name1", "Famil1");

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
                stud4.setZachet(rand.Next(1, 12));
            }

            for (int i = 0; i < 3; i++)
            {
                stud4.setDZ(rand.Next(1, 12));
            }

            for (int i = 0; i < 3; i++)
            {
                stud4.setEkz(rand.Next(1, 12));
            }

            Group group = new Group("G1", "Povar", 2, stud);
            group.sortStud();
            group.addStud(stud4);
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

            if (stud1.getName() == stud2.getName())
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
        }
    }
}

