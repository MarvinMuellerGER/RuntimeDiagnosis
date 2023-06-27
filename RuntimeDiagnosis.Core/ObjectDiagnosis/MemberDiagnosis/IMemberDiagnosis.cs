using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

public interface IMemberDiagnosis : IProvidesCurrentValueString
{
    IObjectDiagnosis ObjectDiagnosis { get; }
    string MemberName { get; }
    IInputValue InputValue { get; }
    IOutputValue OutputValue { get; }
    IEnumerable<IDirectionValue> DirectionValues { get; }
}

public interface IMemberDiagnosis<TMemberValueType> : IMemberDiagnosis
{
    new IInputValue<TMemberValueType?> InputValue { get; }
    new IOutputValue<TMemberValueType?> OutputValue { get; }
    new IEnumerable<IDirectionValue<TMemberValueType?>> DirectionValues { get; }
}

public interface IMemberDiagnosis<TOwnerType, TMemberValueType> : IMemberDiagnosis<TMemberValueType>
    where TOwnerType : IDiagnosableObject
{
    new IObjectDiagnosis<TOwnerType> ObjectDiagnosis { get; }
    new IInputValue<TOwnerType, TMemberValueType?> InputValue { get; }
    new IOutputValue<TOwnerType, TMemberValueType?> OutputValue { get; }
    new IEnumerable<IDirectionValue<TOwnerType, TMemberValueType?>> DirectionValues { get; }
}