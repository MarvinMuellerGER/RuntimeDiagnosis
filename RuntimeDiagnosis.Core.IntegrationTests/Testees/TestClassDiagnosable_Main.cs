using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnosis;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class TestClassDiagnosable : TestClass, IDiagnosableObject<TestClassDiagnosable>
{
    private ObjectDiagnosis<TestClassDiagnosable> _objectDiagnosis = null!;

    IObjectDiagnosis IDiagnosableObject.ObjectDiagnosis => ObjectDiagnosis;

    public IObjectDiagnosis<TestClassDiagnosable> ObjectDiagnosis => _objectDiagnosis;

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public TestClassDiagnosable() => 
        Initialize();

    private void Initialize() => 
        _objectDiagnosis = 
            new ObjectDiagnosis<TestClassDiagnosable>(this, InvokePropertyChanged,
                CreateMemberDiagnosisForTestProperty, 
                CreateMemberDiagnosisForTestField);

    private void InvokePropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}