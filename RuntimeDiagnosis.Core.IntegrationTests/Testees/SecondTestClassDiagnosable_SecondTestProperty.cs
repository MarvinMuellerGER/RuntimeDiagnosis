using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class SecondTestClassDiagnosable
{
    public new bool SecondTestProperty
    {
        get => _objectDiagnosis.GetCurrentOutputMemberValue(() => BaseSecondTestProperty);
        set => _objectDiagnosis.SetOriginalInputMemberValue(() => BaseSecondTestProperty, value);
    }

    private bool BaseSecondTestProperty
    {
        get => base.SecondTestProperty;
        [UsedImplicitly]
        set => base.SecondTestProperty = value;
    }

    private static IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForSecondTestProperty => 
        Array.Empty<DirectionValueDefinition>();
    
    private static IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForSecondTestProperty => 
        Array.Empty<DirectionValueDefinition>();
    
    private IMemberDiagnosis CreateMemberDiagnosisForSecondTestProperty(
        ObjectDiagnosis<SecondTestClassDiagnosable> objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(SecondTestProperty),
            () => BaseSecondTestProperty,
            InputCallerDefinitionsForSecondTestProperty, 
            OutputCallerDefinitionsForSecondTestProperty);
}