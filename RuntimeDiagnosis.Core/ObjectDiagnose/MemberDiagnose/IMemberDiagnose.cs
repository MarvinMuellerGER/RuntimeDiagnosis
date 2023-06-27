using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;

namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;

public interface IMemberDiagnose : IProvidesCurrentValueString
{
    IObjectDiagnose ObjectDiagnose { get; }
    string MemberName { get; }
    IInputValue InputValue { get; }
    IOutputValue OutputValue { get; }
    IEnumerable<IDirectionValue> DirectionValues { get; }
}

public interface IMemberDiagnose<TMemberValueType> : IMemberDiagnose
{
    new IInputValue<TMemberValueType?> InputValue { get; }
    new IOutputValue<TMemberValueType?> OutputValue { get; }
    new IEnumerable<IDirectionValue<TMemberValueType?>> DirectionValues { get; }
}

public interface IMemberDiagnose<TOwnerType, TMemberValueType> : IMemberDiagnose<TMemberValueType>
    where TOwnerType : IDiagnosableObject
{
    new IObjectDiagnose<TOwnerType> ObjectDiagnose { get; }
    new IInputValue<TOwnerType, TMemberValueType?> InputValue { get; }
    new IOutputValue<TOwnerType, TMemberValueType?> OutputValue { get; }
    new IEnumerable<IDirectionValue<TOwnerType, TMemberValueType?>> DirectionValues { get; }
}