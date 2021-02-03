using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlogers
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, SortedSet<string>>> app =
                new Dictionary<string, Dictionary<string, SortedSet<string>>>();

            //app.Add("user one", new Dictionary<string, SortedSet<string>>());
            //app["user one"].Add("folowing", new SortedSet<string>());
            //app["user one"].Add("followers", new SortedSet<string>());

            string input = Console.ReadLine();

            while (input != "Statistics")
            {
                string[] inputArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (inputArgs.Length == 4)
                {
                    string userName = inputArgs[0];

                    if (!app.ContainsKey(userName))
                    {
                        app.Add(userName, new Dictionary<string, SortedSet<string>>());
                        app[userName].Add("followers", new SortedSet<string>());
                        app[userName].Add("followings", new SortedSet<string>());

                    }

                }
                else if (inputArgs.Length == 3)
                {
                    string follower = inputArgs[0];
                    string following = inputArgs[2];


                    if (app.ContainsKey(follower) && app.ContainsKey(following)
                        && follower != following) //&& (!app[following]["followers"].Contains(follower)))
                    {
                        app[following]["followers"].Add(follower);
                        app[follower]["followings"].Add(following);
                    }

                }
                input = Console.ReadLine();
            }

            Console.WriteLine($"The V-Logger has a total of {app.Count} vloggers in its logs.");

            var sortedApp = app.OrderByDescending(x => x.Value["followers"].Count).
                 ThenBy(x => x.Value["followings"].Count); //ToDictionary(x=>x.Key,x=> x.Value);
            int counter = 1;

            foreach (var user in sortedApp)
            {
                if (counter == 1)
                {
                    Console.WriteLine($"{counter}. {user.Key} : {user.Value["followers"].Count} followers," +
                       $" {user.Value["followings"].Count} following");

                    foreach (var item in user.Value["followers"])
                    {
                        Console.WriteLine($"*  {item}");
                    }
                }
                else
                {

                    Console.WriteLine($"{counter}. {user.Key} : {user.Value["followers"].Count} followers," +
                        $" {user.Value["followings"].Count} following");

                }

                counter++;
            }

        }
    }
}