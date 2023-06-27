using static RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.IDirectionValue;
using static RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.IDirectionValue.ValueDirectionType;

namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.Kit;

public readonly record struct DirectionValueDefinition(
    Type OwnerType, string MemberName, ValueDirectionType ValueDirection = Input);