using RuntimeDiagnosis.Core.ObjectDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.Kit;
using static RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.IDirectionValue.ValueDirectionType;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class TestClassDiagnosable
{
    public new bool TestProperty
    {
        get => _objectDiagnose.GetCurrentOutputMemberValue(GetOriginalOutputValueOfTestProperty);
        set => _objectDiagnose.SetOriginalInputMemberValue(SetCurrentInputValueOfTestProperty, value);
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
    
    private IMemberDiagnose CreateMemberDiagnosisForTestProperty(ObjectDiagnose<TestClassDiagnosable> objectDiagnose) =>
        objectDiagnose.CreateMemberDiagnosis(nameof(TestProperty), 
            InputCallerDefinitionsForTestProperty, 
            OutputCallerDefinitionsForTestProperty,
            GetOriginalOutputValueOfTestProperty, SetCurrentInputValueOfTestProperty);
}