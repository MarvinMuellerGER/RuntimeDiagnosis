namespace RuntimeDiagnosis.Core.IntegrationTests.Testee;

public partial class TestClassDiagnosable
{
    public new bool TestProperty
    {
        get => ObjectDiagnose.GetCurrentOutputMemberValue(base.TestProperty);
        set => ObjectDiagnose.SetOriginalInputMemberValue(SetCurrentInputValueOfTestProperty, value);
    }

    private bool GetOriginalOutputValueOfTestProperty() => 
        base.TestProperty;
    
    private void SetCurrentInputValueOfTestProperty(bool value) => 
        base.TestProperty = value;
    
    private void AddTestPropertyToDiagnose() =>
        ObjectDiagnose.AddMember(nameof(TestProperty),
            GetOriginalOutputValueOfTestProperty, SetCurrentInputValueOfTestProperty);
}