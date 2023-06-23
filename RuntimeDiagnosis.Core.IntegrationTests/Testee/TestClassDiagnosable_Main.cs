using System.ComponentModel;
using System.Diagnostics;
using RuntimeDiagnosis.Core.ObjectDiagnose;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testee;

[DebuggerDisplay($"{nameof(TestClass)} object with Object Diagnose")]
public partial class TestClassDiagnosable : TestClass, IDiagnosableObject
{
    public ObjectDiagnose<IDiagnosableObject> ObjectDiagnose { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public TestClassDiagnosable()
    {
        ObjectDiagnose = new(this, InvokePropertyChanged);
        AddMembersToDiagnose();
    }

    private void AddMembersToDiagnose()
    {
        AddTestPropertyToDiagnose();
        AddTestFieldToDiagnose();
    }

    private void InvokePropertyChanged(string propertyName) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}