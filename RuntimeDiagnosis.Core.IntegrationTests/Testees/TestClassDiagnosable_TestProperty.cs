using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.IDirectionValue.ValueDirectionType;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class TestClassDiagnosable
{
    public new bool TestProperty
    {
        get => _objectDiagnosis.GetCurrentOutputMemberValue(GetOriginalOutputValueOfTestProperty);
        set => _objectDiagnosis.SetOriginalInputMemberValue(SetCurrentInputValueOfTestProperty, value);
    }

    private bool GetOriginalOutputValueOfTestProperty() => 
        base.TestProperty;
    
    private void SetCurrentInputValueOfTestProperty(bool value) => 
        base.TestProperty = value;

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
            InputCallerDefinitionsForTestProperty, 
            OutputCallerDefinitionsForTestProperty,
            GetOriginalOutputValueOfTestProperty, SetCurrentInputValueOfTestProperty);
}