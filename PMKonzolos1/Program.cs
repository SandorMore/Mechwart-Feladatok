namespace PMKonzolos1
{
    internal class Program
    {
        static List<Versenyzo> versenyzok = new List<Versenyzo>();
        static void Main(string[] args)
        {
            versenyzok = File.ReadAllLines("tour.csv").Skip(1).Select(x => new Versenyzo(x)).ToList();
            feladat1(versenyzok);
            feladat2(versenyzok);
            feladat3(versenyzok);
        }
        static void feladat1(List<Versenyzo> list)
        {
            Console.Write("1. feladat: \t");
            Console.WriteLine(list.Count(x => x.szakasz == 5));
        }
        static void feladat2(List<Versenyzo> list)
        {
            Console.Write("2. feladat: \t");
            double avg = list.Average(x => x.get_seconds());
            Console.Write(avg + "\t");
            list.Where(x => x.get_seconds() < avg).ToList().ForEach(x => Console.WriteLine(x.nev));
        }
        static void feladat3(List<Versenyzo> list)
        {
            Console.WriteLine("3. feladat: Csapatonként az átlagidő:");
            var averages = list
                .GroupBy(v => v.csapat)
                .Select(g => new { Team = g.Key, AvgSeconds = g.Average(v => v.get_seconds()) })
                .OrderBy(x => x.Team);

            foreach (var a in averages)
            {
                var ts = TimeSpan.FromSeconds(a.AvgSeconds);
                Console.WriteLine($"{a.Team}: {ts:hh\\:mm\\:ss}");
            }
        }
        static bool kituntetes(string versenyzo, List<Versenyzo>list)
        {
            return list.Where(x => x.get_seconds() <= 3 * 3600 && x.csapat == "USA").Select(x => x.nev).ToList().Contains(versenyzo); 
        }
        static void feladat4(List<Versenyzo> list)
        {
            var _ = list.Where(x => x.szakasz == 5).OrderByDescending(x => x.get_seconds()).ToList();
            
            for(int i = 0; i < 10; ++i)
            {
                Console.WriteLine(_[i].nev + " " + _[i].ido);
            }
        }
    }
}
