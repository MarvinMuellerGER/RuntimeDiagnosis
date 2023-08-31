using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.IntegrationTests.Decorators.Testees;

public partial class SecondTestClassDiagnosisDecorator
{
    private static readonly IEnumerable<DirectionValueDefinition> InputCallerDefinitionsForSecondTestProperty =
        Array.Empty<DirectionValueDefinition>();
    
    private static readonly IEnumerable<DirectionValueDefinition> OutputCallerDefinitionsForSecondTestProperty =
        Array.Empty<DirectionValueDefinition>();

    public bool SecondTestProperty
    {
        get => _objectDiagnosis.GetMemberValue(() => _secondTestClass.SecondTestProperty);
        set => _objectDiagnosis.SetMemberValue(() => _secondTestClass.SecondTestProperty, value);
    }

    private IMemberDiagnosis CreateMemberDiagnosisForTestProperty(IObjectDiagnosisInternal objectDiagnosis) =>
        objectDiagnosis.CreateMemberDiagnosis(nameof(SecondTestProperty),
            () => _secondTestClass.SecondTestProperty, 
            InputCallerDefinitionsForSecondTestProperty, 
            OutputCallerDefinitionsForSecondTestProperty);
}