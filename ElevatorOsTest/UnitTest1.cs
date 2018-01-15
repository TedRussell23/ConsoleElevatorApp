using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElevatorApp;
using FakeItEasy;


namespace ElevatorOsTest
{
    [TestClass]
    public class ElevatorOsImplTest
    {
        [TestMethod]
        public void ElevatorReturnsTheFloorThePreviousUserRequestedAndArrivedTo()
        {
            //Arrange
            int mExpectedFloor = 4;
            int mRequestedFloor = 4;
            int mMaxFloor = 10;

            //Act
            ElevatorOS_Impl tempElevatorOsImpl = new ElevatorOS_Impl(new ElevatorModule_Impl(mMaxFloor));
            tempElevatorOsImpl.ReqElevatorAtOrToFloor(mRequestedFloor);
            tempElevatorOsImpl.Update();

            //Assert
            Assert.AreEqual(mExpectedFloor, tempElevatorOsImpl._mElevatorModuleImpl.GetCurrentFloor());
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "The floor requested does not exist")]
        public void UserRequestsFloorThatDoesNotExist()
        {
            //Arrange
            int mMaxFloor = 3;
            int mRequestedFloor = 10;

            //Act
            ElevatorOS_Impl tempElevatorOsImpl = new ElevatorOS_Impl(new ElevatorModule_Impl(mMaxFloor));
            tempElevatorOsImpl.ReqElevatorAtOrToFloor(mRequestedFloor);
            tempElevatorOsImpl.Update();
        }

        [TestMethod]
        public void AddsCommandToTheBlockingChainAndIsEqualToFloorRequested()
        {
            //Arrange
            int mExpectedFloor = 3;
            int mRequestedFloor = 3;

            //Act
            ElevatorOS_Impl tempElevatorOsImpl = new ElevatorOS_Impl(new ElevatorModule_Impl(5));
            tempElevatorOsImpl.ReqElevatorAtOrToFloor(mRequestedFloor);

            //Assert
            Assert.IsNotNull(tempElevatorOsImpl._mFloorReq.Count);
            Assert.AreEqual(mExpectedFloor, tempElevatorOsImpl._mFloorReq.Take());
        }

        [TestMethod]
        public void TestingTheFakeItEasyMockFrameworkMethods()
        {
            //Fakes class for test
            var _mFakeElevatorOsImpl = A.Fake<ElevatorOS_Impl>();

            //Set Method Return Value
            A.CallTo(() => _mFakeElevatorOsImpl._mElevatorModuleImpl.GetCurrentFloor()).Returns(4);

            //Arrange
            int mRequestedFloor = 4;

            //Act
            _mFakeElevatorOsImpl.ReqElevatorAtOrToFloor(mRequestedFloor);
            _mFakeElevatorOsImpl.Update();

            //Assert
            Assert.AreEqual(mRequestedFloor, _mFakeElevatorOsImpl._mElevatorModuleImpl.GetCurrentFloor());
        }
    }
}
