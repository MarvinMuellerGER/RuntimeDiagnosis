using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

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
            ObjectDiagnosesManager.CreateNewObjectDiagnose(this, CreateMemberDiagnoses, InvokePropertyChanged);

    private IEnumerable<IMemberDiagnosis> CreateMemberDiagnoses(ObjectDiagnosis<TestClassDiagnosable> objectDiagnosis) =>
        new[]
        {
            CreateMemberDiagnosisForTestProperty(objectDiagnosis),
            CreateMemberDiagnosisForTestField(objectDiagnosis)
        };

    private void InvokePropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}