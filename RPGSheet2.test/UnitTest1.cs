using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace RPGSheet2.test
{
    [TestClass]
    public class Hashgen_conflicts
    {
        readonly int maxID;
        Queue<Tuple<int, string>> generated;
        Queue<Tuple<int, string, int, string>> conflicts;

        public Hashgen_conflicts()
        {
            maxID = 1000000;
        }
        void checkConflicts()
        {
            while (generated.Count > 0)
            {
                Tuple<int, string> A = generated.Dequeue();
                foreach (var B in generated)
                {
                    if (A.Item2.Equals(B.Item2) && A.Item1 != B.Item1)
                    {
                        conflicts.Enqueue(new Tuple<int, string, int, string>(A.Item1, A.Item2, B.Item1, B.Item2));
                    }
                }
                Debug.WriteLineIf(A.Item1 % 10000 == 0, $"{(A.Item1/((float)maxID))*100}% complete.");
            }
        }
        [TestMethod]
        public void HasConflict()
        {
            generated = new Queue<Tuple<int, string>>();
            conflicts = new Queue<Tuple<int, string, int, string>>();
            Debug.WriteLine("Starting Test For Unique IDs");
            for (int ID = 0; ID <= maxID; ID++)
            {
                string gen = Extensions.HashID.GenHash(ID);
                generated.Enqueue(new Tuple<int, string>(ID, gen.ToString()));
            }
            Debug.WriteLine("Checking Conflicts");
            checkConflicts();
            bool result = conflicts.Count == 0;
            Debug.WriteLine($"{conflicts.Count} conflicts found for under {maxID}");

            Assert.IsTrue(result, "Conflict exists.");
        }
    }
}
