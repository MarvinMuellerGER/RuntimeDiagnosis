namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

// TODO: Write Summaries
public interface IInputValueDiagnosis : IDirectionValueDiagnosis
{ }

public interface IInputValueDiagnosis<TMemberValueType> : IInputValueDiagnosis, IDirectionValueDiagnosis<TMemberValueType?>
{
    internal TMemberValueType? Value { set; }
}

public interface IInputValueDiagnosis<TOwnerType, TMemberValueType> : 
    IInputValueDiagnosis<TMemberValueType?>, IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{ }