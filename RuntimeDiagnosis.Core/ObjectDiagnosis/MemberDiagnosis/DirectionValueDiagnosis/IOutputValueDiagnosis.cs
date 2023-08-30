namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

// TODO: Write Summaries
public interface IOutputValueDiagnosis : IDirectionValueDiagnosis
{
    bool UpdateWhenInputValueChanged { get; set; }
    bool UpdateOriginalValueWhenDiagnosisActive { get; set; }
}

public interface IOutputValueDiagnosis<TMemberValueType> : IOutputValueDiagnosis, IDirectionValueDiagnosis<TMemberValueType?>
{
    internal TMemberValueType? Value { get; }
}

public interface IOutputValueDiagnosis<TOwnerType, TMemberValueType> : 
    IOutputValueDiagnosis<TMemberValueType?>, IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{ }