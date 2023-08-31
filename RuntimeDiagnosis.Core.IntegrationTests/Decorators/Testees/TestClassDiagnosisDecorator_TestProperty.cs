using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.IDirectionValueDiagnosis.ValueDirectionType;

namespace RuntimeDiagnosis.Core.IntegrationTests.Decorators.Testees;

public partial class TestClassDiagnosisDecorator
{
    private static readonly IEnumerable<IDirectionValueDefinition> InputCallerDefinitionsForTestProperty = new[]
    {
        (IDirectionValueDefinition)new DirectionValueDefinition<ISecondTestClass>(
            nameof(ISecondTestClass.SecondTestProperty))
    };
    
    private static readonly IEnumerable<IDirectionValueDefinition> OutputCallerDefinitionsForTestProperty = new[]
    {
        (IDirectionValueDefinition)new DirectionValueDefinition<ISecondTestClass>(
            nameof(ISecondTestClass.SecondTestProperty), Output)
    };

    public bool TestProperty
    {
        get => _objectDiagnosis.GetMemberValue(() => _testClass.TestProperty);
        set => _objectDiagnosis.SetMemberValue(() => _testClass.TestProperty, value);
    }

    private IMemberDiagnosis CreateMemberDiagnosisForTestProperty(IObjectDiagnosisInternal objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(TestProperty),
            () => _testClass.TestProperty, 
            InputCallerDefinitionsForTestProperty, 
            OutputCallerDefinitionsForTestProperty);
}