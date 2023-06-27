using RuntimeDiagnosis.Core.ObjectDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class TestClassDiagnosable
{
    public new bool TestField
    {
        get => _objectDiagnose.GetCurrentOutputMemberValue(GetOriginalOutputValueOfTestField);
        set => _objectDiagnose.SetOriginalInputMemberValue(SetCurrentInputValueOfTestField, value);
    }

    private bool GetOriginalOutputValueOfTestField() => 
        base.TestField;
    
    private void SetCurrentInputValueOfTestField(bool value) => 
        base.TestField = value;

    private static IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForTestField => 
        Array.Empty<DirectionValueDefinition>();
    
    private static IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForTestField => 
        Array.Empty<DirectionValueDefinition>();
    
    private IMemberDiagnose CreateMemberDiagnosisForTestField(ObjectDiagnose<TestClassDiagnosable> objectDiagnose) =>
        objectDiagnose.CreateMemberDiagnosis(nameof(TestField),
            InputCallerDefinitionsForTestField, 
            OutputCallerDefinitionsForTestField,
            GetOriginalOutputValueOfTestField, SetCurrentInputValueOfTestField);
}