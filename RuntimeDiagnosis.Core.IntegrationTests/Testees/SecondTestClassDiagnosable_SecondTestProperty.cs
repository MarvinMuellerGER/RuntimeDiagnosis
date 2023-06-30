using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Testees;

public partial class SecondTestClassDiagnosable
{
    private static readonly IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForSecondTestProperty = 
        Array.Empty<DirectionValueDefinition>();
    
    private static readonly IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForSecondTestProperty = 
        Array.Empty<DirectionValueDefinition>();
    
    public new bool SecondTestProperty
    {
        get => _objectDiagnosis.GetMemberValue(() => BaseSecondTestProperty);
        set => _objectDiagnosis.SetMemberValue(() => BaseSecondTestProperty, value);
    }

    private bool BaseSecondTestProperty
    {
        get => base.SecondTestProperty;
        [UsedImplicitly]
        set => base.SecondTestProperty = value;
    }
    
    private IMemberDiagnosis CreateMemberDiagnosisForSecondTestProperty(
        ObjectDiagnosis<SecondTestClassDiagnosable> objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(SecondTestProperty),
            () => BaseSecondTestProperty,
            InputCallerDefinitionsForSecondTestProperty, 
            OutputCallerDefinitionsForSecondTestProperty);
}