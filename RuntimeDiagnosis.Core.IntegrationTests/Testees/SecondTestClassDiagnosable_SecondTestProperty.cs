using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class SecondTestClassDiagnosable
{
    public new bool SecondTestProperty
    {
        get => _objectDiagnosis.GetCurrentOutputMemberValue(() => base.SecondTestProperty);
        set => _objectDiagnosis.SetOriginalInputMemberValue(() => base.SecondTestProperty, value);
    }

    private static IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForSecondTestProperty => 
        Array.Empty<DirectionValueDefinition>();
    
    private static IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForSecondTestProperty => 
        Array.Empty<DirectionValueDefinition>();
    
    private IMemberDiagnosis CreateMemberDiagnosisForSecondTestProperty(
        ObjectDiagnosis<SecondTestClassDiagnosable> objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(SecondTestProperty),
            () => base.SecondTestProperty,
            InputCallerDefinitionsForSecondTestProperty, 
            OutputCallerDefinitionsForSecondTestProperty);
}