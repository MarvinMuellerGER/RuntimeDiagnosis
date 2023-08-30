using JetBrains.Annotations;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

public interface IDirectionValueDiagnosesFinder
{
    IEnumerable<IDirectionValueDiagnosis> GetDirectionValuesFromKnownObjectDiagnosesByDefinitions(
        IEnumerable<DirectionValueDefinition> callerDefinitions);

    IEnumerable<IDirectionValueDiagnosis> GetDirectionValuesFromObjectDiagnoseByDefinitions(
        IObjectDiagnosis objectDiagnosis, [NoEnumeration] IEnumerable<DirectionValueDefinition> callerDefinitions);
}