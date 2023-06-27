using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.IDirectionValue;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.IDirectionValue.ValueDirectionType;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;

public readonly record struct DirectionValueDefinition(
    Type OwnerType, string MemberName, ValueDirectionType ValueDirection = Input);