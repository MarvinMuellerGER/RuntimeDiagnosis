﻿using JetBrains.Annotations;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

// TODO: Implement Unit Tests
public sealed class DirectionValueDiagnosesFinder : IDirectionValueDiagnosesFinder
{
    private readonly IObjectDiagnosesManagerInternal _objectDiagnosesManager;

    public DirectionValueDiagnosesFinder(IObjectDiagnosesManagerInternal objectDiagnosesManager) => 
        _objectDiagnosesManager = objectDiagnosesManager;

    public IEnumerable<IDirectionValueDiagnosis> GetDirectionValuesFromKnownObjectDiagnosesByDefinitions(
        IEnumerable<IDirectionValueDefinition> callerDefinitions)
    {
        var output = new List<IDirectionValueDiagnosis>();

        foreach (var objectDiagnosis in _objectDiagnosesManager.ObjectDiagnoses)
            output.AddRange(GetDirectionValuesFromObjectDiagnoseByDefinitions(
                objectDiagnosis, callerDefinitions));

        return output;
    }

    public IEnumerable<IDirectionValueDiagnosis> GetDirectionValuesFromObjectDiagnoseByDefinitions(
        IObjectDiagnosis objectDiagnosis, [NoEnumeration] IEnumerable<IDirectionValueDefinition> callerDefinitions) =>
        from callerDefinition in callerDefinitions
        where objectDiagnosis.OwnerBaseType == callerDefinition.OwnerType
        from memberDiagnosis in objectDiagnosis.MemberDiagnoses
        where memberDiagnosis.MemberName == callerDefinition.MemberName
        from directionValue in memberDiagnosis.DirectionValues
        where directionValue.ValueDirection == callerDefinition.ValueDirection
        select directionValue;
}