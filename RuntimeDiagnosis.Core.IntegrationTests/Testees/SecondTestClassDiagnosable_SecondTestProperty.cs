using RuntimeDiagnosis.Core.ObjectDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class SecondTestClassDiagnosable
{
    public new bool SecondTestProperty
    {
        get => _objectDiagnose.GetCurrentOutputMemberValue(GetOriginalOutputValueOfSecondTestProperty);
        set => _objectDiagnose.SetOriginalInputMemberValue(SetCurrentInputValueOfSecondTestProperty, value);
    }

    private bool GetOriginalOutputValueOfSecondTestProperty() => 
        base.SecondTestProperty;
    
    private void SetCurrentInputValueOfSecondTestProperty(bool value) => 
        base.SecondTestProperty = value;

    private static IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForSecondTestProperty => 
        Array.Empty<DirectionValueDefinition>();
    
    private static IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForSecondTestProperty => 
        Array.Empty<DirectionValueDefinition>();
    
    private IMemberDiagnose CreateMemberDiagnosisForSecondTestProperty(
        ObjectDiagnose<SecondTestClassDiagnosable> objectDiagnose) =>
        objectDiagnose.CreateMemberDiagnosis(nameof(SecondTestProperty), 
            InputCallerDefinitionsForSecondTestProperty, 
            OutputCallerDefinitionsForSecondTestProperty,
            GetOriginalOutputValueOfSecondTestProperty, SetCurrentInputValueOfSecondTestProperty);
}