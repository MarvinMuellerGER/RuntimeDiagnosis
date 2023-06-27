using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class TestClassDiagnosable : TestClass, IDiagnosableObject<TestClassDiagnosable>
{
    private ObjectDiagnose<TestClassDiagnosable> _objectDiagnose = null!;

    IObjectDiagnose IDiagnosableObject.ObjectDiagnose => ObjectDiagnose;

    public IObjectDiagnose<TestClassDiagnosable> ObjectDiagnose => _objectDiagnose;

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public TestClassDiagnosable() => 
        Initialize();

    private void Initialize() => 
        _objectDiagnose = 
            ObjectDiagnosesManager.CreateNewObjectDiagnose(this, CreateMemberDiagnoses, InvokePropertyChanged);

    private IEnumerable<IMemberDiagnose> CreateMemberDiagnoses(ObjectDiagnose<TestClassDiagnosable> objectDiagnose) =>
        new[]
        {
            CreateMemberDiagnosisForTestProperty(objectDiagnose),
            CreateMemberDiagnosisForTestField(objectDiagnose)
        };

    private void InvokePropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}