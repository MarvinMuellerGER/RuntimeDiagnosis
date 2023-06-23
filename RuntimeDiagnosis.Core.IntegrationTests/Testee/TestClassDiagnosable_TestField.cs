namespace RuntimeDiagnosis.Core.IntegrationTests.Testee;

public partial class TestClassDiagnosable
{
    public new bool TestField
    {
        get => ObjectDiagnose.GetCurrentMemberValue(base.TestField);
        set => ObjectDiagnose.SetOriginalMemberValue(SetCurrentInputValueOfTestField, value);
    }

    private bool GetOriginalOutputValueOfTestField() => 
        base.TestField;
    
    private void SetCurrentInputValueOfTestField(bool value) => 
        base.TestField = value;
    
    private void AddTestFieldToDiagnose() =>
        ObjectDiagnose.AddMember(nameof(TestField),
            GetOriginalOutputValueOfTestField, SetCurrentInputValueOfTestField);
}