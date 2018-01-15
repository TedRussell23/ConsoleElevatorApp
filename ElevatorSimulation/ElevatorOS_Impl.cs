using System;
using System.Collections.Concurrent;

namespace ElevatorApp
{
    /// <summary>
    /// 
    /// !!! CHANGE CODE HERE !!!
    /// 
    /// See ElevatorOS_IF.cs for explanation
    /// </summary>
    public class ElevatorOS_Impl : ElevatorOS_Base
    {

        public readonly BlockingCollection<int> _mFloorReq = new BlockingCollection<int>();
        public readonly ElevatorModule_IF _mElevatorModuleImpl;
        /// <summary>
        /// See also ElevatorOS_Base.cs
        /// </summary>
        /// <param name="elevatorModule"></param>
        public ElevatorOS_Impl(ElevatorModule_IF elevatorModule)
            : base(elevatorModule)
        {
            _mElevatorModuleImpl = elevatorModule;
        }

        /// <summary>
        /// See ElevatorOS_IF.cs for explanation
        /// </summary>
        override
        public void ReqElevatorAtOrToFloor(int floor)
        {
            //TODO!
            //react to requests of users here...

            //Add the request to the chain of commands (FIFO - Blocking Queue)
            if (floor > _mElevatorModuleImpl.GetMaxFloor())
            {
                //floor = GetCurrentFloor();
                throw new IndexOutOfRangeException();
            }
            _mFloorReq.Add(floor);
        }

        /// <summary>
        /// See ElevatorOS_IF.cs for explanation
        /// </summary>
        override
        public void Update()
        {
            //TODO!
            //operations to be done every virtual minute
            int mCurrentFloor = _mElevatorModuleImpl.GetCurrentFloor();

            if (_mFloorReq.Count > 0)
            {
                //Retrieve the floor from the list of requests - FIFO - First In First Out (BlockingQueue)
                int floor = _mFloorReq.Take();
                //While the current floor is not equal to the destination continue to move towards it
                while (mCurrentFloor != floor)
                {
                    //Retrieve current floor
                    mCurrentFloor = _mElevatorModuleImpl.GetCurrentFloor();

                    //Determine whether lift needs to go up or down
                    if (mCurrentFloor < floor)
                    {
                        _mElevatorModuleImpl.ReqToGoUp();
                    }
                    if (mCurrentFloor > floor)
                    {
                        _mElevatorModuleImpl.ReqToGoDown();
                    }

                    //Process operation
                    _mElevatorModuleImpl.Update();
                }
            }

        }
    }
}
