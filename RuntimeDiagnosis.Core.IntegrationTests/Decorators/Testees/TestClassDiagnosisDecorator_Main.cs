using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

namespace RuntimeDiagnosis.Core.IntegrationTests.Decorators.Testees;

public partial class TestClassDiagnosisDecorator : ITestClassDiagnosable
{
    private readonly ITestClass _testClass;
    private readonly IObjectDiagnosisInternal<TestClassDiagnosisDecorator> _objectDiagnosis;
    IObjectDiagnosis IDiagnosableObject.ObjectDiagnosis => _objectDiagnosis;
    public IObjectDiagnosis<TestClassDiagnosisDecorator> ObjectDiagnosis => _objectDiagnosis;

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public TestClassDiagnosisDecorator(ITestClass testClass)
    {
        _testClass = testClass;
        _objectDiagnosis = ServiceProvider.Instance.GetObjectDiagnosis<TestClassDiagnosisDecorator>();
        _objectDiagnosis.Initialize(this);
    }

    IEnumerable<Func<IObjectDiagnosisInternal, IMemberDiagnosis>> 
        IDiagnosableObjectInternal.CreateMemberDiagnosisActions =>
        new[]
        {
            CreateMemberDiagnosisForTestProperty
        };

    void IDiagnosableObjectInternal.InvokePropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}