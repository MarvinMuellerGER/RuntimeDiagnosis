namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;

public interface IOutputValue : IDirectionValue
{ }

public interface IOutputValue<TMemberValueType> : IOutputValue, IDirectionValue<TMemberValueType?>
{ }

public interface IOutputValue<TOwnerType, TMemberValueType> : 
    IOutputValue<TMemberValueType?>, IDirectionValue<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{ }