using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class SecondTestClassDiagnosable
{
    public new bool SecondTestProperty
    {
        get => _objectDiagnosis.GetCurrentOutputMemberValue(GetOriginalOutputValueOfSecondTestProperty);
        set => _objectDiagnosis.SetOriginalInputMemberValue(SetCurrentInputValueOfSecondTestProperty, value);
    }

    private bool GetOriginalOutputValueOfSecondTestProperty() => 
        base.SecondTestProperty;
    
    private void SetCurrentInputValueOfSecondTestProperty(bool value) => 
        base.SecondTestProperty = value;

    private static IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForSecondTestProperty => 
        Array.Empty<DirectionValueDefinition>();
    
    private static IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForSecondTestProperty => 
        Array.Empty<DirectionValueDefinition>();
    
    private IMemberDiagnosis CreateMemberDiagnosisForSecondTestProperty(
        ObjectDiagnosis<SecondTestClassDiagnosable> objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(SecondTestProperty), 
            InputCallerDefinitionsForSecondTestProperty, 
            OutputCallerDefinitionsForSecondTestProperty,
            GetOriginalOutputValueOfSecondTestProperty, SetCurrentInputValueOfSecondTestProperty);
}