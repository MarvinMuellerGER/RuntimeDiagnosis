namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;

public interface IInputValue : IDirectionValue
{ }

public interface IInputValue<TMemberValueType> : IInputValue, IDirectionValue<TMemberValueType?>
{ }

public interface IInputValue<TOwnerType, TMemberValueType> : 
    IInputValue<TMemberValueType?>, IDirectionValue<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{ }