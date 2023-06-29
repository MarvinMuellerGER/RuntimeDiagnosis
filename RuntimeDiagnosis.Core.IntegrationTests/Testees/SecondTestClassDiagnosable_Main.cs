using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class SecondTestClassDiagnosable : SecondTestClass, IDiagnosableObject<SecondTestClassDiagnosable>
{
    private ObjectDiagnosis<SecondTestClassDiagnosable> _objectDiagnosis = null!;

    IObjectDiagnosis IDiagnosableObject.ObjectDiagnosis => ObjectDiagnosis;

    public IObjectDiagnosis<SecondTestClassDiagnosable> ObjectDiagnosis => _objectDiagnosis;

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public SecondTestClassDiagnosable(TestClassDiagnosable testClass) : base(testClass) => 
        Initialize();

    private void Initialize() => 
        _objectDiagnosis = 
            new ObjectDiagnosis<SecondTestClassDiagnosable>(this, CreateMemberDiagnoses, InvokePropertyChanged);

    private IEnumerable<IMemberDiagnosis> CreateMemberDiagnoses(
        ObjectDiagnosis<SecondTestClassDiagnosable> objectDiagnosis) =>
        new[]
        {
            CreateMemberDiagnosisForSecondTestProperty(objectDiagnosis)
        };

    private void InvokePropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}