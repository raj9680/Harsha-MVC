namespace X_Unit
{
    public class UnitTest1
    {
        // [Fact]
        public void Test1()
        {
            // Arrange: Declaration of variables & collecting inputs
            MyMath mm = new MyMath();
            int input1 = 10, input2 = 5;
            int expected = 15;

            // Act: Calling methods which we want to test
            int actual = mm.Add(input1, input2);

            // Assert: Comparing the expected value with actual value
            Assert.Equal(expected, actual);
        }
    }
}