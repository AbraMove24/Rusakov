using System;
public enum Frequency { Weekly, Monthly, Yearly }
namespace _01
{
    class Person
    {
        private string fname;
        private string lname;
        private DateTime birthday;

        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public int Birthyear
        {
            get => birthday.Year; set
            {
                birthday = new DateTime(value, birthday.Month, birthday.Day);
            }
        }
        public override string ToString()
        {
            return String.Concat(fname, " ", lname, " ", birthday.ToString());
        }
        virtual public string toShortString()
        {
            return String.Concat(fname, " ", lname);
        }

        public Person(string fname, string lname, DateTime birthday)
        {
            this.fname = fname;
            this.lname = lname;
            this.birthday = birthday;
        }
        public Person()
        {
            this.fname = "Arthur";
            this.lname = "Morgan";
            this.birthday = DateTime.Parse("30/11/2001");
        }
    }
    class Magazine
    {
        private string name;
        private Frequency freq;
        private DateTime printdate;
        private int copies;
        private Article[] alist;

        public Magazine()
        {
            this.Name = "GoodMagazine";
            this.Freq = Frequency.Monthly;
            this.Printdate = DateTime.Parse("01/01/2020");
            this.Copies = 3000;
            this.Alist = new Article[3] { new Article(), new Article(), new Article() };
        }

        public Magazine(string name, Frequency freq, DateTime printdate, int copies, Article[] alist)
        {
            this.Name = name;
            this.Freq = freq;
            this.Printdate = printdate;
            this.Copies = copies;
            this.Alist = alist;
        }
        public double MeanRating
        {
            get
            {
                double sum = 0;
                foreach (Article a in alist)
                {
                    sum += a.Rating;
                }
                return sum / alist.Length;
            }
        }
        public bool this[Frequency freq]
        {
            get { return this.freq == freq; }
        }
        public void AddArticles(params Article[] newlist)
        {
            Article[] tlist = new Article[alist.Length + newlist.Length];
            int i = 0;
            for (i = 0; i < alist.Length; i++)
            {
                tlist[i] = alist[i];
            }
            for (int j = 0; j < newlist.Length; j++)
            {
                tlist[i++] = newlist[j];
            }
            alist = tlist;
        }
        public override string ToString()
        {
            string ans;
            ans = String.Concat(name, " ", freq.ToString(), " ", printdate.ToString(), " ", copies.ToString(), " ");
            foreach (Article a in alist)
            {
                ans = String.Concat(ans, " ", a.ToString());
            }
            return ans;
        }
        public virtual string toShortString()
        {
            string ans;
            ans = String.Concat(name, " ", freq.ToString(), " ", printdate.ToString(), " ", copies.ToString(), " ", MeanRating.ToString());
            return ans;
        }

        public string Name { get => name; set => name = value; }
        public DateTime Printdate { get => printdate; set => printdate = value; }
        public int Copies { get => copies; set => copies = value; }
        internal Frequency Freq { get => freq; set => freq = value; }
        internal Article[] Alist { get => alist; set => alist = value; }
    }
    class Article
    {
        public Article(Person person, string title, double rating)
        {
            Person = person;
            Title = title;
            Rating = rating;
        }
        public Article()
        {
            Person = new Person();
            Title = "Some Article";
            Rating = 4.8;
        }
        public Person Person { get; private set; }
        public string Title { get; private set; }
        public double Rating { get; private set; }
        public override string ToString()
        {
            return String.Concat(Person.ToString() + " " + Title.ToString() + " " + Rating.ToString()); ;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Magazine m = new Magazine();
            Console.Out.WriteLine(m.toShortString());
            Console.Out.WriteLine(m[Frequency.Weekly].ToString(), m[Frequency.Monthly].ToString(), m[Frequency.Yearly].ToString());
            m.Name = "NewName";
            m.Freq = Frequency.Yearly;
            m.Copies = 200;
            m.Printdate = DateTime.Parse("10/10/1003");
            Console.Out.WriteLine(m.toShortString());
            m.AddArticles(new Article(), new Article());
            Console.Out.WriteLine(m.ToString());

        }
    }
}