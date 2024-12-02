namespace TestProject;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [TestCase(new int []{5,10}, 15)]
    public void Sum(int[] numbers, int expectedResult)
    {
        var sum = numbers[0] + numbers[1];
        
        Assert.That(sum.Equals(expectedResult)); 
    }
}
