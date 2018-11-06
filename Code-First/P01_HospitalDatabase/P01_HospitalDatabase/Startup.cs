using System;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Initializer;

namespace P01_HospitalDatabase
{
    public class Startup
    {
        public static void Main()
        {
            using (var context = new HospitalContext())
            {
                DatabaseInitializer.InitialSeed(context);
            }
        }
    }
}
