using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLocalTest
{
    class Program
    {
        private static ConcurrentDictionary<string, AsyncLocal<string>> aa = new ConcurrentDictionary<string, AsyncLocal<string>>();
        static void Main(string[] args)
        {
            var task1 = Task.Run(() =>
            {
                var async = new AsyncLocal<string>();
                async.Value = "100";
                aa.GetOrAdd("abc", async);

                AsyncLocal<string> bb = null;
                var result = aa.TryGetValue("abc", out bb);
                Console.Write(bb.Value);

                return aa;
            });
            var task2 = Task.Run(() =>
            {
                var async = new AsyncLocal<string>();
                async.Value = "200";
                aa.GetOrAdd("abc", async);

                AsyncLocal<string> bb = null;
                var result = aa.TryGetValue("abc", out bb);
                Console.Write(bb.Value);

                return aa;
            });
            var task3 = Task.Run(() =>
            {
                var async = new AsyncLocal<string>();
                async.Value = "300";
                aa.GetOrAdd("abc", async);

                AsyncLocal<string> bb = null;
                var result = aa.TryGetValue("abc", out bb);
                Console.Write(bb.Value);

                return aa;
            });


            AsyncLocal<string> cc = null;
            var result2 = task1.Result.TryGetValue("abc", out cc);
            Console.Write("Task1:" + cc.Value);

            result2 = task3.Result.TryGetValue("abc", out cc);
            Console.Write("Task3:" + cc.Value);

            result2 = task2.Result.TryGetValue("abc", out cc);
            Console.Write("Task2:" + cc.Value);

            Console.ReadLine();
        }
    }
}
