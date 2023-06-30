using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class TestClassDiagnosable
{
    public new bool TestField
    {
        get => _objectDiagnosis.GetCurrentOutputMemberValue(() => base.TestField);
        set => _objectDiagnosis.SetOriginalInputMemberValue(() => base.TestField, value);
    }

    private static IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForTestField => 
        Array.Empty<DirectionValueDefinition>();
    
    private static IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForTestField => 
        Array.Empty<DirectionValueDefinition>();
    
    private IMemberDiagnosis CreateMemberDiagnosisForTestField(ObjectDiagnosis<TestClassDiagnosable> objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(TestField),
            () => base.TestField,
            InputCallerDefinitionsForTestField, 
            OutputCallerDefinitionsForTestField);
}