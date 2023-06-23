namespace RuntimeDiagnosis.Core.IntegrationTests.Testee;

public partial class TestClassDiagnosable
{
    public new bool TestField
    {
        get => ObjectDiagnose.GetCurrentOutputMemberValue(base.TestField);
        set => ObjectDiagnose.SetOriginalInputMemberValue(SetCurrentInputValueOfTestField, value);
    }

    private bool GetOriginalOutputValueOfTestField() => 
        base.TestField;
    
    private void SetCurrentInputValueOfTestField(bool value) => 
        base.TestField = value;
    
    private void AddTestFieldToDiagnose() =>
        ObjectDiagnose.AddMember(nameof(TestField),
            GetOriginalOutputValueOfTestField, SetCurrentInputValueOfTestField);
}