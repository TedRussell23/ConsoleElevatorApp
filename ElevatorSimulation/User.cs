using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElevatorApp
{
    /// <summary>
    /// !!! DON'T CHANGE ANY CODE HERE !!!
    /// The users are generated in Progam.cs. 
    /// They behave according to the way they have been initialized: 
    /// * request the elevator at a certain time 
    /// * on a certain floor, and then ask to be 
    /// * taken to another floor. 
    /// When the program is running, “your class” - the elevatorOS_Impl.cs - 
    /// is being made aware of user requests via calls to the ReqElevatorAtOrToFloor-function.
    /// So when a user appears on the scene you get a request for the elevator to come to the floor
    /// where the user is currently waiting. Once the elevator is there, you get another request of 
    /// where the user wants to go to. So - as in the real world - your operating system doesn’t know 
    /// about users per se, but through the incoming request the elevatorOS is indirectly aware.
    /// </summary>
    public class User
    {
        public enum eState { 
            NOT_THERE_YET,
            WAITING,
            ENTERS,
            IN_ELEVATOR,
            LEAVING,
            LEFT,
        };

        private ElevatorOS_IF m_elevatorOS = null;
                
        private eState m_eState = eState.NOT_THERE_YET;

        private string m_name = null;
        private int m_MinuteOfRequest = -1;
        private int m_cntMinutesSpend = 0;
        private int m_floorEntering = -1;
        private int m_floorLeaving = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">some random name</param>
        /// <param name="elevatorOs">the elevator OS that the user shall "talk to"</param>
        /// <param name="timeSlotRequest">when is the user coming along?</param>
        /// <param name="floorEntering">on what floor is he coming in? (button outside elevator on floor x)</param>
        /// <param name="floorLeaving">which floor does he want to go to? (buttton inside elevator, labelled "floor x")</param>
        public User(string name, ElevatorOS_IF elevatorOs, int timeSlotRequest, int floorEntering, int floorLeaving)
        {
            m_name = name;
            
            m_elevatorOS = elevatorOs;

            m_MinuteOfRequest = timeSlotRequest;
            m_floorEntering = floorEntering;
            m_floorLeaving = floorLeaving;                   
        }
        
        /// <summary>
        /// Handling of the individual user. Firing requests to the OS where needed.
        /// Updating state of the user.
        /// </summary>
        /// <param name="timeSlotCur"></param>
        public void Update(int timeSlotCur)
        {            
            if (m_eState == eState.NOT_THERE_YET)
            {
                if (timeSlotCur == m_MinuteOfRequest)
                {
                    m_elevatorOS.ReqElevatorAtOrToFloor(m_floorEntering);
                    m_eState = eState.WAITING;
                }
                else
                {
                    return;
                }
            }

            if (m_eState == eState.LEFT)
                return;

            m_cntMinutesSpend++;

            int currentFloor = m_elevatorOS.GetCurrentFloor();
            if (m_eState == eState.WAITING && currentFloor == m_floorEntering)
            {
                m_eState = eState.ENTERS;
                m_elevatorOS.ReqElevatorAtOrToFloor(m_floorLeaving);
            }
            
            if (m_eState == eState.ENTERS && currentFloor != m_floorEntering)
            {
                m_eState = eState.IN_ELEVATOR;                                  
            }
            
            if (m_eState == eState.IN_ELEVATOR && currentFloor == m_floorLeaving)
            {
                m_eState = eState.LEAVING;
                return;
            }
            
            if (m_eState == eState.LEAVING)
            {
                m_eState = eState.LEFT;
            }
        }       

        public eState GetState()
        {
            return m_eState;
        }

        public int GetMinuteOfReq()
        {
            return m_MinuteOfRequest;
        }

        public int GetMinutesSpend()
        {
            return m_cntMinutesSpend;
        }

        /// <summary>
        /// Human-readable output of the state of each user.
        /// If the user is not there yet or has left the output is suppressed.
        /// </summary>
        /// <returns>"null" if user is not there yet or has left.</returns>
        public string GetInfo()
        {
            if (m_eState == eState.NOT_THERE_YET || m_eState == eState.LEFT)
            {
                return null;
            }

            string strState = "in an unknown state.";
            switch (m_eState)
            {
                case eState.NOT_THERE_YET: strState = "not there yet"; break;
                case eState.WAITING: strState = "waiting"; break;
                case eState.ENTERS: strState = "entering"; break;
                case eState.IN_ELEVATOR: strState = "in elevator"; break;
                case eState.LEAVING: strState = "leaving"; break;
                case eState.LEFT: strState = "gone"; break;
                default: break;
            }
            
            string info = m_name + " is " + strState;

            if (m_eState == eState.WAITING || m_eState == eState.ENTERS)
                info += " at floor " + m_floorEntering + " to go to floor " + m_floorLeaving;
            else if (m_eState == eState.LEAVING)
                info += " at floor " + m_floorLeaving;
            else if (m_eState == eState.IN_ELEVATOR)
                info += " to go to floor " + m_floorLeaving;

            info += ".";
            return info;
        }
    }
}
