using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class SecondTestClassDiagnosable : SecondTestClass, IDiagnosableObject<SecondTestClassDiagnosable>
{
    private ObjectDiagnose<SecondTestClassDiagnosable> _objectDiagnose = null!;

    IObjectDiagnose IDiagnosableObject.ObjectDiagnose => ObjectDiagnose;

    public IObjectDiagnose<SecondTestClassDiagnosable> ObjectDiagnose => _objectDiagnose;

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public SecondTestClassDiagnosable(TestClassDiagnosable testClass) : base(testClass) => 
        Initialize();

    private void Initialize() => 
        _objectDiagnose = 
            ObjectDiagnosesManager.CreateNewObjectDiagnose(this, CreateMemberDiagnoses, InvokePropertyChanged);

    private IEnumerable<IMemberDiagnose> CreateMemberDiagnoses(
        ObjectDiagnose<SecondTestClassDiagnosable> objectDiagnose) =>
        new[]
        {
            CreateMemberDiagnosisForSecondTestProperty(objectDiagnose)
        };

    private void InvokePropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}