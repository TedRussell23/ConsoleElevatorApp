using System;
using System.Collections.Generic;

namespace ElevatorApp
{
    /// <summary>
    /// !!! DON'T CHANGE ANY CODE HERE !!!
    /// The World-class is the implementation of the virtual elevator world.
    /// This is where things are happening in general on a virtual “minute” basis.
    /// This is also outputting the status to the command line 
    /// including the final results.
    /// </summary>
    public class World : IUpdatable
    {
        private ElevatorModule_IF m_elevatorModule = null;
        private ElevatorOS_IF m_elevatorOS = null;
        private List<User> m_usersAll = null;

        private int m_minutesCur = 0;
        private bool m_done = false;

        /// <summary>
        /// Creates the "world" with all 3 major elements:
        /// users, elevatorOS and elevatorModule
        /// </summary>
        /// <param name="users"></param>
        /// <param name="elevatorOS"></param>
        /// <param name="elevatorModule"></param>
        public World(List<User> users, ElevatorOS_IF elevatorOS, 
                                              ElevatorModule_IF elevatorModule)
        {
            m_elevatorOS = elevatorOS;
            m_elevatorModule = elevatorModule;

            m_usersAll = new List<User>();
            m_usersAll.AddRange(users);
        }        
        
        /// <summary>
        /// Called over and over again. Updating the world and therefore
        /// it's elements and giving feedback on the state.
        /// </summary>
        public void Update()
        {
            Console.WriteLine("");
            Console.WriteLine("[Minute: " + m_minutesCur + "]");
            int floorNow = m_elevatorModule.GetCurrentFloor();
            Console.WriteLine("Elevator is at floor " + floorNow + ".");

            string info = null;

            //user(s) may enter or leave the elevator
            bool allLeft = true;
            foreach (User user in m_usersAll)
            {
                user.Update(m_minutesCur);                
                info = user.GetInfo();
                if(info != null) Console.WriteLine(info);

                if (user.GetState() < User.eState.LEAVING)
                {
                    allLeft = false;
                }
            }

            if (allLeft)
            {
                m_done = true;
                return; //if everyone left no more need to operate!
            }

            //operator system checks where to go to next
            m_elevatorOS.Update();

            //module goes up or down depending on OS input
            m_elevatorModule.Update();
            info = m_elevatorModule.GetInfo();
            if (info != null) Console.WriteLine(info);
            
            m_minutesCur++;
        }

        /// <summary>
        /// Once all users have been served this will return "true".
        /// </summary>
        /// <returns></returns>
        public bool IsDone()
        {
            return m_done;
        }

        /// <summary>
        /// Displays the result, once the program ran.
        /// </summary>
        public void Result()
        {            
            int stillWaiting = 0;
            int minutes = 0;
            foreach (User user in m_usersAll)
            {
                if(user.GetState() < User.eState.LEAVING)
                    ++stillWaiting;

                minutes += user.GetMinutesSpend();
            }

            int moved = m_elevatorModule.GetCntMove();
            
            
            Console.WriteLine("");
            Console.WriteLine("Result:");
            Console.WriteLine("1) " + stillWaiting + " User(s) waiting.");
            Console.WriteLine("2) " + m_usersAll.Count + " User(s) spend overall " + minutes + " minutes to get to their floors or are still waiting.");
            Console.WriteLine("3) Elevator moved " + moved + " times.");
        }
    }
}
