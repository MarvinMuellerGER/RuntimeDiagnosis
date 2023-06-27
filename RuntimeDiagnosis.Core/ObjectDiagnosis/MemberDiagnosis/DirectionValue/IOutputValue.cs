namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue;

public interface IOutputValue : IDirectionValue
{
    bool UpdateWhenInputValueChanged { get; set; }
    bool UpdateOriginalValueWhenDiagnosisActive { get; set; }
}

public interface IOutputValue<TMemberValueType> : IOutputValue, IDirectionValue<TMemberValueType?>
{
    internal TMemberValueType? Value { get; }
}

public interface IOutputValue<TOwnerType, TMemberValueType> : 
    IOutputValue<TMemberValueType?>, IDirectionValue<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{ }