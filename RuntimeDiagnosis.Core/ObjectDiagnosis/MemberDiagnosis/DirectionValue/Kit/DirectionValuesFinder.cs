using JetBrains.Annotations;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;

public static class DirectionValuesFinder
{
    public static IEnumerable<IDirectionValue> GetDirectionValuesFromKnownObjectDiagnosesByDefinitions(
        IEnumerable<DirectionValueDefinition> callerDefinitions)
    {
        var output = new List<IDirectionValue>();

        foreach (var objectDiagnosis in ObjectDiagnosesManager.ObjectDiagnoses)
            output.AddRange(GetDirectionValuesFromObjectDiagnoseByDefinitions(
                objectDiagnosis, callerDefinitions));

        return output;
    }

    public static IEnumerable<IDirectionValue> GetDirectionValuesFromObjectDiagnoseByDefinitions(
        IObjectDiagnosis objectDiagnosis, [NoEnumeration] IEnumerable<DirectionValueDefinition> callerDefinitions) =>
        from callerDefinition in callerDefinitions
        where objectDiagnosis.OwnerBaseType == callerDefinition.OwnerType
        from memberDiagnosis in objectDiagnosis.MemberDiagnoses
        where memberDiagnosis.MemberName == callerDefinition.MemberName
        from directionValue in memberDiagnosis.DirectionValues
        where directionValue.ValueDirection == callerDefinition.ValueDirection
        select directionValue;
}