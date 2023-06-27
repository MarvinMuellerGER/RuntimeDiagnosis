using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

public interface IMemberDiagnosis : IProvidesCurrentValueString
{
    IObjectDiagnosis ObjectDiagnosis { get; }
    string MemberName { get; }
    IInputValueDiagnosis InputValueDiagnosis { get; }
    IOutputValueDiagnosis OutputValueDiagnosis { get; }
    IEnumerable<IDirectionValueDiagnosis> DirectionValues { get; }
}

public interface IMemberDiagnosis<TMemberValueType> : IMemberDiagnosis
{
    new IInputValueDiagnosis<TMemberValueType?> InputValueDiagnosis { get; }
    new IOutputValueDiagnosis<TMemberValueType?> OutputValueDiagnosis { get; }
    new IEnumerable<IDirectionValueDiagnosis<TMemberValueType?>> DirectionValues { get; }
}

public interface IMemberDiagnosis<TOwnerType, TMemberValueType> : IMemberDiagnosis<TMemberValueType>
    where TOwnerType : IDiagnosableObject
{
    new IObjectDiagnosis<TOwnerType> ObjectDiagnosis { get; }
    new IInputValueDiagnosis<TOwnerType, TMemberValueType?> InputValueDiagnosis { get; }
    new IOutputValueDiagnosis<TOwnerType, TMemberValueType?> OutputValueDiagnosis { get; }
    new IEnumerable<IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>> DirectionValues { get; }
}