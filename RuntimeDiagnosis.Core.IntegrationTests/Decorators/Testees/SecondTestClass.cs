namespace RuntimeDiagnosis.Core.IntegrationTests.Decorators.Testees;

public class SecondTestClass : ISecondTestClass
{
    private readonly ITestClass _testClass;

    public SecondTestClass(ITestClass testClass) => 
        _testClass = testClass;

    public bool SecondTestProperty
    {
        get => _testClass.TestProperty;
        set => _testClass.TestProperty = value;
    }
}