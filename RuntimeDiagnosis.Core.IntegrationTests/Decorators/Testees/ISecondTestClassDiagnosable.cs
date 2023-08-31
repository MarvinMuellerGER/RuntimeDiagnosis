namespace RuntimeDiagnosis.Core.IntegrationTests.Decorators.Testees;

public interface ISecondTestClassDiagnosable :
    ISecondTestClass, IDiagnosableObjectInternal<SecondTestClassDiagnosisDecorator>
{ }