using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class SecondTestClassDiagnosable :
    SecondTestClass, IDiagnosableObjectInternal<SecondTestClassDiagnosable>
{
    private IObjectDiagnosisInternal<SecondTestClassDiagnosable> _objectDiagnosis = null!;
    IObjectDiagnosis IDiagnosableObject.ObjectDiagnosis => _objectDiagnosis;
    public IObjectDiagnosis<SecondTestClassDiagnosable> ObjectDiagnosis => _objectDiagnosis;

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public SecondTestClassDiagnosable(TestClassDiagnosable testClass) : base(testClass) => 
        Initialize();

    private void Initialize()
    {
       
        _objectDiagnosis = ServiceProvider.Instance.GetObjectDiagnosis<SecondTestClassDiagnosable>();
        _objectDiagnosis.Initialize(this);
    }

    IEnumerable<Func<IObjectDiagnosisInternal, IMemberDiagnosis>> IDiagnosableObjectInternal.CreateMemberDiagnosisActions =>
        new[]
        {
            CreateMemberDiagnosisForSecondTestProperty
        };

    void IDiagnosableObjectInternal.InvokePropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}