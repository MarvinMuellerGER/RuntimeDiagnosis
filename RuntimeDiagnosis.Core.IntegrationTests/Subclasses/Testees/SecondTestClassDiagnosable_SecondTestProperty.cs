using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Subclasses.Testees;

public partial class SecondTestClassDiagnosable
{
    private static readonly IEnumerable<IDirectionValueDefinition> InputCallerDefinitionsForSecondTestProperty = 
        Enumerable.Empty<IDirectionValueDefinition>();
    
    private static readonly IEnumerable<IDirectionValueDefinition> OutputCallerDefinitionsForSecondTestProperty = 
        Enumerable.Empty<IDirectionValueDefinition>();
    
    public new bool SecondTestProperty
    {
        get => _objectDiagnosis.GetMemberValue<bool>();
        set => _objectDiagnosis.SetMemberValue(value);
    }

    private bool BaseSecondTestProperty
    {
        get => base.SecondTestProperty;
        [UsedImplicitly]
        set => base.SecondTestProperty = value;
    }
    
    private IMemberDiagnosis CreateMemberDiagnosisForSecondTestProperty(IObjectDiagnosisInternal objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(SecondTestProperty),
            () => BaseSecondTestProperty,
            InputCallerDefinitionsForSecondTestProperty, 
            OutputCallerDefinitionsForSecondTestProperty);
}