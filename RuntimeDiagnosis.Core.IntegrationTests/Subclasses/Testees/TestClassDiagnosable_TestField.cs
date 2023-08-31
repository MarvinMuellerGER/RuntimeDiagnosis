using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Subclasses.Testees;

public partial class TestClassDiagnosable
{
    private static readonly IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForTestField = 
        Array.Empty<DirectionValueDefinition>();
    
    private static readonly IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForTestField = 
        Array.Empty<DirectionValueDefinition>();

    public new bool TestField
    {
        get => _objectDiagnosis.GetMemberValue(() => BaseTestField);
        set => _objectDiagnosis.SetMemberValue(() => BaseTestField, value);
    }

    private bool BaseTestField
    {
        get => base.TestField;
        [UsedImplicitly]
        set => base.TestField = value;
    }

    private IMemberDiagnosis CreateMemberDiagnosisForTestField(IObjectDiagnosisInternal objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(TestField),
            () => BaseTestField,
            InputCallerDefinitionsForTestField, 
            OutputCallerDefinitionsForTestField);
}