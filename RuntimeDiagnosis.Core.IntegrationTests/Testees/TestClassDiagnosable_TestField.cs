using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class TestClassDiagnosable
{
    public new bool TestField
    {
        get => _objectDiagnosis.GetCurrentOutputMemberValue(GetOriginalOutputValueOfTestField);
        set => _objectDiagnosis.SetOriginalInputMemberValue(SetCurrentInputValueOfTestField, value);
    }

    private bool GetOriginalOutputValueOfTestField() => 
        base.TestField;
    
    private void SetCurrentInputValueOfTestField(bool value) => 
        base.TestField = value;

    private static IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForTestField => 
        Array.Empty<DirectionValueDefinition>();
    
    private static IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForTestField => 
        Array.Empty<DirectionValueDefinition>();
    
    private IMemberDiagnosis CreateMemberDiagnosisForTestField(ObjectDiagnosis<TestClassDiagnosable> objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(TestField),
            InputCallerDefinitionsForTestField, 
            OutputCallerDefinitionsForTestField,
            GetOriginalOutputValueOfTestField, SetCurrentInputValueOfTestField);
}