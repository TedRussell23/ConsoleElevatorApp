namespace ElevatorApp
{
    /// <summary>
    /// 
    /// !!! DON'T CHANGE ANY CODE HERE !!!
    ///
    /// This is the low level representation of the elevator.
    /// All the module knows how to do is to go up or down one floor at a time.
    /// This needs to be requested, and will be executed when the module gets updated.
    /// </summary>
    public interface ElevatorModule_IF : IUpdatable
    {
        /// <summary>
        /// This function is called by the world and will move the module up or down, 
        /// if previously requested (e.g. by your elevatorOS_Impl implementation).
        /// </summary>
        new void Update();

        /// <summary>
        /// Call this function if you want the elevator to go up on the next call to elevatorModule_Impl.Update().
        /// </summary>
        void ReqToGoUp();

        /// <summary>
        /// Call this function if you want the elevator to go down on the next call to elevatorModule_Impl.Update().
        /// </summary>
        void ReqToGoDown();

        /// <summary>
        /// Any time you’d like to know where the elevator currently is, call this function to get the answer.
        /// (This function is also called when users interrogate the ElevatorOS-interface to see where the elevator is.)
        /// </summary>
        /// <returns>the current floor</returns>
        int GetCurrentFloor();

        /// <summary>
        /// For certain implementations it can be useful to know what the top floor is.
        /// But if you never use this function, you should not be worried at all.
        /// </summary>
        /// <returns>the highest floor (NOT the number of floors!)</returns>
        int GetMaxFloor();

        /// <summary>
        /// This function reveals how many times the elevator has moved. It is used for the final result. Calling it elsewhere is unlikely to have any value to your implementation.
        /// </summary>
        /// <returns>number of times the elevator moved</returns>
        int GetCntMove();

        /// <summary>
        /// Provides a human-readable information about the current state of the elevator module.
        /// It is used by "world" for minute-per-minute-output. Calling it elsewhere is unlikely to have any value to your implementation.
        /// </summary>
        /// <returns>information about the current state of the elevator module.</returns>
        string GetInfo();
    }
}
