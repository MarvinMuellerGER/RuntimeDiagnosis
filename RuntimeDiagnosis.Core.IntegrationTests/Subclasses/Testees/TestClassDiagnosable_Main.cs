using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

namespace RuntimeDiagnosis.Core.IntegrationTests.Subclasses.Testees;

public partial class TestClassDiagnosable :
    TestClass, IDiagnosableObjectInternal<TestClassDiagnosable>
{
    private IObjectDiagnosisInternal<TestClassDiagnosable> _objectDiagnosis = null!;
    IObjectDiagnosis IDiagnosableObject.ObjectDiagnosis => _objectDiagnosis;
    public IObjectDiagnosis<TestClassDiagnosable> ObjectDiagnosis => _objectDiagnosis;

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public TestClassDiagnosable() => 
        Initialize();

    private void Initialize()
    {
        _objectDiagnosis = ServiceProvider.Instance.GetObjectDiagnosis<TestClassDiagnosable>();
        _objectDiagnosis.Initialize(this);
    }

    IEnumerable<Func<IObjectDiagnosisInternal, IMemberDiagnosis>> IDiagnosableObjectInternal.CreateMemberDiagnosisActions =>
        new[]
        {
            CreateMemberDiagnosisForTestProperty,
            CreateMemberDiagnosisForTestField
        };

    void IDiagnosableObjectInternal.InvokePropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}