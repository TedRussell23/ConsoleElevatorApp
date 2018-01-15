using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorApp
{    
    /// <summary>
    /// 
    /// !!! DON'T CHANGE ANY CODE HERE !!!
    /// 
    /// The common entry point for any C# program, as you know it.
    /// All objects of the program are instantiated at this level.
    /// Then, only calls to the world-object are being made.
    /// 
    /// </summary>
    class Program
    {
        const int MINUTES_TIMEOUT = 200;
        
        static World m_world = null;

        static int[,] userMatrix = {
            {3,3,2},{5,2,0},{3,1,1},{0,5,3},{0,0,1},
            {5,0,5},{2,0,0},{5,0,5},{1,3,3},{3,3,4},
            {3,0,5},{0,5,1},{3,4,2},{2,4,2},{0,0,4},
            {2,5,4},{1,5,0},{0,5,1},{1,5,2},{0,4,0},
            {1,3,3},{0,1,2},{3,2,1},{1,0,1},{3,0,5},
            {4,5,4},{2,1,2},{3,1,5},{2,1,0},{0,2,4},
            {2,3,4},{5,0,3},{5,5,1},{1,3,3},{1,5,1},
            {3,2,0}
        };

        /// <summary>
        /// Common entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Init();
            Run();
            Finish();
            Console.ReadKey();
        }

        /// <summary>
        /// All objects of the program are instantiated here.
        /// </summary>
        static void Init()
        {
            ///create an elevator with floors 0, 1,..5
            ElevatorModule_IF elevatorModule = new ElevatorModule_Impl(5);
            ///your OS system for the elevator
            ElevatorOS_IF elevatorOS = new ElevatorOS_Impl(elevatorModule);
            ///some imaginative users
            List<User> users = GenerateUsers(elevatorOS);            
            ///creating the "world" with the above elements
            m_world = new World(users, elevatorOS, elevatorModule);
        }

        /// <summary>
        /// This is running the program, which in this cases means updating 
        /// the "world" until things are done or we ran out of time.
        /// </summary>
        static void Run()
        {
            bool done = false;
            
            int minutes = 0;
            while (!done && minutes < MINUTES_TIMEOUT)
            {
                m_world.Update();
                done = m_world.IsDone();
                minutes++;

                Thread.Sleep(100);
                
            }
            if (minutes == MINUTES_TIMEOUT)
                Console.WriteLine("Reached timeout!");
        }

        /// <summary>
        /// Show the results when the program is finished.
        /// </summary>
        static void Finish()
        {
            m_world.Result();
        }

        /// <summary>
        /// Generate some users.
        /// </summary>
        /// <param name="elevatorOS"></param>
        /// <returns>a list of users</returns>
        private static List<User> GenerateUsers(ElevatorOS_IF elevatorOS)
        {
            List<User> users = new List<User>();
            int time = 0;
            for (int i = 0; i < 26; ++i)
            {
                var name = "User" + (char)(65 + i);

                time += userMatrix[i, 0];

                var floorEnter = userMatrix[i, 1];
                var floorLeave = userMatrix[i, 2];

                var user = new User(name, elevatorOS, time, floorEnter, floorLeave);

                users.Add(user);
            }

            return users;
        }

    }
}
