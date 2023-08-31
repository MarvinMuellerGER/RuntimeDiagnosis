using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Subclasses.Testees;

public partial class TestClassDiagnosable
{
    private static readonly IEnumerable<IDirectionValueDefinition> InputCallerDefinitionsForTestField = 
        Enumerable.Empty<IDirectionValueDefinition>();
    
    private static readonly IEnumerable<IDirectionValueDefinition> OutputCallerDefinitionsForTestField = 
        Enumerable.Empty<IDirectionValueDefinition>();

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