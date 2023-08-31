using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

namespace RuntimeDiagnosis.Core.IntegrationTests.Decorators.Testees;

public partial class SecondTestClassDiagnosisDecorator : ISecondTestClassDiagnosable
{
    private readonly ISecondTestClass _secondTestClass;
    private readonly IObjectDiagnosisInternal<SecondTestClassDiagnosisDecorator> _objectDiagnosis;
    IObjectDiagnosis IDiagnosableObject.ObjectDiagnosis => _objectDiagnosis;
    public IObjectDiagnosis<SecondTestClassDiagnosisDecorator> ObjectDiagnosis => _objectDiagnosis;

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public SecondTestClassDiagnosisDecorator(ISecondTestClass secondTestClass)
    {
        _secondTestClass = secondTestClass;
        _objectDiagnosis = ServiceProvider.Instance.GetObjectDiagnosis<SecondTestClassDiagnosisDecorator>();
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