namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

using static IDirectionValueDiagnosis;

// TODO: Write Summaries
public interface IDirectionValueDefinition
{
    Type OwnerType { get; }
    string MemberName { get; }
    ValueDirectionType ValueDirection { get; }
}