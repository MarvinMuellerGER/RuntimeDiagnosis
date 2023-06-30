using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.IDirectionValueDiagnosis.ValueDirectionType;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class TestClassDiagnosable
{
    public new bool TestProperty
    {
        get => _objectDiagnosis.GetCurrentOutputMemberValue(() => BaseTestProperty);
        set => _objectDiagnosis.SetOriginalInputMemberValue(() => BaseTestProperty, value);
    }

    private bool BaseTestProperty
    {
        get => base.TestProperty;
        [UsedImplicitly]
        set => base.TestProperty = value;
    }

    private static IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForTestProperty => new[]
    {
        new DirectionValueDefinition(typeof(SecondTestClass), nameof(SecondTestClass.SecondTestProperty))
    };
    
    private static IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForTestProperty => new[]
    {
        new DirectionValueDefinition(typeof(SecondTestClass), nameof(SecondTestClass.SecondTestProperty), Output)
    };
    
    private IMemberDiagnosis CreateMemberDiagnosisForTestProperty(ObjectDiagnosis<TestClassDiagnosable> objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(TestProperty),
            () => BaseTestProperty, 
            InputCallerDefinitionsForTestProperty, 
            OutputCallerDefinitionsForTestProperty);
}