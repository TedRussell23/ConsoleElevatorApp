using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElevatorApp
{
    /// <summary>
    /// 
    /// !!! DON'T CHANGE ANY CODE HERE !!!
    /// 
    /// See ElevatorModule_IF.cs for explanation
    /// </summary>
    public class ElevatorModule_Impl : ElevatorModule_IF
    {
        enum eDir {
            NONE,
            UP,
            DOWN
        }

        private readonly int m_maxFloor = -1;

        private eDir m_req = 0;
        private int m_floorPrev = 0;
        private int m_floorCur = 0;

        private int m_cntMove = 0;

        /// <summary>
        /// See ElevatorModule_IF.cs for explanation
        /// </summary>
        /// <param name="maxFloor"></param>
        public ElevatorModule_Impl(int maxFloor)
        {
            m_maxFloor = maxFloor;
        }

        /// <summary>
        /// See ElevatorModule_IF.cs for explanation
        /// </summary>
        /// <param name="maxFloor"></param>
        public void Update()
        {
            m_floorPrev = m_floorCur;

            if (m_req == eDir.NONE)
                return;
            if(m_req == eDir.DOWN)
                m_floorCur--;
            if (m_req == eDir.UP)
                m_floorCur++;

            m_req = eDir.NONE;

            if (m_floorCur > m_maxFloor)
            {
                m_floorCur = m_maxFloor;
                return;
            }
            if (m_floorCur < 0)
            {
                m_floorCur = 0;
                return;
            }

            m_cntMove++;
        }

        /// <summary>
        /// See ElevatorModule_IF.cs for explanation
        /// </summary>
        /// <param name="maxFloor"></param>
        public void ReqToGoUp()
        {
            m_req = eDir.UP;
        }

        /// <summary>
        /// See ElevatorModule_IF.cs for explanation
        /// </summary>
        /// <param name="maxFloor"></param>
        public void ReqToGoDown()
        {
            m_req = eDir.DOWN;
        }

        /// <summary>
        /// See ElevatorModule_IF.cs for explanation
        /// </summary>
        /// <param name="maxFloor"></param>
        public int GetCurrentFloor()
        {
            return m_floorCur;
        }

        /// <summary>
        /// See ElevatorModule_IF.cs for explanation
        /// </summary>
        /// <param name="maxFloor"></param>
        public int GetMaxFloor()
        {
            return m_maxFloor;
        }

        /// <summary>
        /// See ElevatorModule_IF.cs for explanation
        /// </summary>
        /// <param name="maxFloor"></param>
        public int GetCntMove()
        {
            return m_cntMove;
        }

        /// <summary>
        /// See ElevatorModule_IF.cs for explanation
        /// </summary>
        /// <param name="maxFloor"></param>
        public string GetInfo()
        {
            if (m_floorCur == m_floorPrev)
            {
                return "Elevator stays at floor " + m_floorCur + ".";
            }
            return "Elevator moves to floor " + m_floorCur + ".";
        }
    }
}
