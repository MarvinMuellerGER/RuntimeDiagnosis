using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.IDirectionValueDiagnosis.ValueDirectionType;

namespace RuntimeDiagnosis.Core.IntegrationTests.Subclasses.Testees;

public partial class TestClassDiagnosable
{
    private static readonly IEnumerable<IDirectionValueDefinition> InputCallerDefinitionsForTestProperty = new[]
    {
        (IDirectionValueDefinition)new DirectionValueDefinition<SecondTestClass>(
            nameof(SecondTestClass.SecondTestProperty))
    };
    
    private static readonly IEnumerable<IDirectionValueDefinition> OutputCallerDefinitionsForTestProperty = new[]
    {
        (IDirectionValueDefinition)new DirectionValueDefinition<SecondTestClass>(
            nameof(SecondTestClass.SecondTestProperty), Output)
    };

    public new bool TestProperty
    {
        get => _objectDiagnosis.GetMemberValue<bool>();
        set => _objectDiagnosis.SetMemberValue(value);
    }

    private bool BaseTestProperty
    {
        get => base.TestProperty;
        [UsedImplicitly]
        set => base.TestProperty = value;
    }

    private IMemberDiagnosis CreateMemberDiagnosisForTestProperty(IObjectDiagnosisInternal objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(TestProperty),
            () => BaseTestProperty, 
            InputCallerDefinitionsForTestProperty, 
            OutputCallerDefinitionsForTestProperty);
}