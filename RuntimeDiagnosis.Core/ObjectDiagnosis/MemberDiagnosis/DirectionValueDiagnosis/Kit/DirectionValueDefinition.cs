using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.IDirectionValueDiagnosis;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.IDirectionValueDiagnosis.ValueDirectionType;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

public readonly record struct DirectionValueDefinition<TOwnerType>(
    string MemberName, ValueDirectionType ValueDirection = Input) : IDirectionValueDefinition
{
    public Type OwnerType { get; } = typeof(TOwnerType);
}