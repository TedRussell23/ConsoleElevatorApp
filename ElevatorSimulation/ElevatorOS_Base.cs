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
    /// See ElevatorOS_IF.cs for explanation
    /// </summary>
    public class ElevatorOS_Base : ElevatorOS_IF
    {
        protected ElevatorModule_IF m_elevatorModule = null;

        /// <summary>
        /// This constructor makes sure you have a reference to the
        /// elevatorModule.  
        /// </summary>
        /// <param name="elevatorModule"></param>
        public ElevatorOS_Base(ElevatorModule_IF elevatorModule)
        {
            m_elevatorModule = elevatorModule;
        }

        /// <summary>
        /// See ElevatorOS_IF.cs for explanation
        /// </summary>
        public virtual void Update() {}

        /// <summary>
        /// See ElevatorOS_IF.cs for explanation
        /// </summary>
        public virtual void ReqElevatorAtOrToFloor(int floor) { }

        /// <summary>
        /// See ElevatorOS_IF.cs for explanation
        /// </summary>
        public int GetCurrentFloor()
        {
            return m_elevatorModule.GetCurrentFloor();
        }
    }
}
