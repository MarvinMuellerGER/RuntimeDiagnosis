using JetBrains.Annotations;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

// TODO: Write Summaries
public interface IDirectionValueDiagnosesFinder
{
    IEnumerable<IDirectionValueDiagnosis> GetDirectionValuesFromKnownObjectDiagnosesByDefinitions(
        IEnumerable<IDirectionValueDefinition> callerDefinitions);

    IEnumerable<IDirectionValueDiagnosis> GetDirectionValuesFromObjectDiagnoseByDefinitions(
        IObjectDiagnosis objectDiagnosis, [NoEnumeration] IEnumerable<IDirectionValueDefinition> callerDefinitions);
}