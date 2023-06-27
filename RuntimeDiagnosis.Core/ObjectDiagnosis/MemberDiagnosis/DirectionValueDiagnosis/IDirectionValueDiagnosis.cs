using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.SingleValue;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

public interface IDirectionValueDiagnosis : 
    IProvidesNameWithoutGenericArity, IProvidesCurrentValueString, IEquatable<IDirectionValueDiagnosis?>
{
    enum ValueDirectionType
    {
        Input,
        Output
    }
    ValueDirectionType ValueDirection { get; }
    IMemberDiagnosis MemberDiagnosis { get; }
    IEnumerable<IDirectionValueDiagnosis> Callers { get; }
    IDirectionValueDiagnosis? LastCaller { get; }
    ISingleValueAlwaysEditable<bool> DiagnoseActive { get; }
    ISingleValue OriginalValue { get; }
    ISingleValueEditable DiagnoseValue { get; }
    ISingleValue CurrentValue { get; }
    delegate void JustCalledEventHandler(IDirectionValueDiagnosis sender, IDirectionValueDiagnosis? caller);
    event JustCalledEventHandler? JustCalled;
    bool SetDiagnoseValueAgain();
}

public interface IDirectionValueDiagnosis<TMemberValueType> : 
    IDirectionValueDiagnosis
{
    new IMemberDiagnosis<TMemberValueType?> MemberDiagnosis { get; }
    new ISingleValueAlwaysEditable<TMemberValueType?, bool> DiagnoseActive { get; }
    new ISingleValueEditable<TMemberValueType?, TMemberValueType?> DiagnoseValue { get; }
    new ISingleValue<TMemberValueType?, TMemberValueType?> OriginalValue { get; }
    new ISingleValue<TMemberValueType?, TMemberValueType?> CurrentValue { get; }
    bool Equals(IDirectionValueDiagnosis<TMemberValueType?>? other); 
    bool Equals(TMemberValueType? value);
}

public interface IDirectionValueDiagnosis<TOwnerType, TMemberValueType> : IDirectionValueDiagnosis<TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    new IMemberDiagnosis<TOwnerType, TMemberValueType?> MemberDiagnosis { get; }
    new ISingleValueAlwaysEditable<TOwnerType, TMemberValueType?, bool> DiagnoseActive { get; }
    new ISingleValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?> DiagnoseValue { get; }
    new ISingleValue<TOwnerType, TMemberValueType?, TMemberValueType?> OriginalValue { get; }
    new ISingleValue<TOwnerType, TMemberValueType?, TMemberValueType?> CurrentValue { get; }
}