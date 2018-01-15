using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorApp
{
    /// <summary>
    ///
    /// !!! DON'T CHANGE ANY CODE HERE !!!
    /// 
    /// The ElevatorOS sits between the user(s) and the ElevatorModule.
    /// It handles requests coming in from the user(s), and operates
    /// the elevator by firing requests to it's low-level functionality:
    /// go one up, or go one down.
    /// </summary>
    public interface ElevatorOS_IF : IUpdatable
    {
        /// Think of 
        /// a) a button on each floor, and 
        /// b) a button for each floor inside the elevator.
        /// Pressing a button (a) or (b) will result in a call to this function
        /// The virtual users are therefore calling this function.
        void ReqElevatorAtOrToFloor(int floor);

        /// all operations (e.g. requests to the elevatorModule) go here 
        new void Update();

        /// forwarding "current floor"-info from the elevator module
        int GetCurrentFloor();
    }
}
