using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.IDirectionValueDiagnosis.ValueDirectionType;

namespace RuntimeDiagnosis.Core.IntegrationTests.Subclasses.Testees;

public partial class TestClassDiagnosable
{
    private static readonly IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForTestProperty = new[]
    {
        new DirectionValueDefinition(typeof(SecondTestClass), nameof(SecondTestClass.SecondTestProperty))
    };
    
    private static readonly IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForTestProperty = new[]
    {
        new DirectionValueDefinition(typeof(SecondTestClass), nameof(SecondTestClass.SecondTestProperty), Output)
    };

    public new bool TestProperty
    {
        get => _objectDiagnosis.GetMemberValue(() => BaseTestProperty);
        set => _objectDiagnosis.SetMemberValue(() => BaseTestProperty, value);
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