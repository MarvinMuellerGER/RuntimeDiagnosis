namespace RuntimeDiagnosis.Core.IntegrationTests.Decorators.Testees;

public interface ITestClassDiagnosable :
    ITestClass, IDiagnosableObjectInternal<TestClassDiagnosisDecorator>
{ }