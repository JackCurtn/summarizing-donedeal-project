namespace DoneDealProjectTests;
public class ExampleUnitTest
{
    [Fact]
    public void AddTwoToNumber_ShouldEqualThree()
    {
        // Given
        int n = 1;

        // When
        int result = n + 2;

        // Then
        Assert.Equal(3, result);
    }
}