namespace RuntimeDiagnosis.Core.IntegrationTests.Subclasses.Testees;

public class SecondTestClass
{
    private readonly TestClassDiagnosable _testClass;

    public SecondTestClass(TestClassDiagnosable testClass) => 
        _testClass = testClass;

    public bool SecondTestProperty
    {
        get => _testClass.TestProperty;
        set => _testClass.TestProperty = value;
    }
}