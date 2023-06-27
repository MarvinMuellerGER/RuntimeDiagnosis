using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.SingleValue;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;

public interface IDirectionValue : 
    IProvidesNameWithoutGenericArity, IProvidesCurrentValueString, IEquatable<IDirectionValue?>
{
    enum ValueDirectionType
    {
        Input,
        Output
    }
    ValueDirectionType ValueDirection { get; }
    IMemberDiagnose MemberDiagnose { get; }
    IEnumerable<IDirectionValue> Callers { get; }
    IDirectionValue? LastCaller { get; }
    ISingleValueAlwaysEditable<bool> DiagnoseActive { get; }
    ISingleValue OriginalValue { get; }
    ISingleValueEditable DiagnoseValue { get; }
    ISingleValue CurrentValue { get; }
    delegate void JustCalledEventHandler(IDirectionValue sender, IDirectionValue? caller);
    event JustCalledEventHandler? JustCalled;
    bool SetDiagnoseValueAgain();
}

public interface IDirectionValue<TMemberValueType> : 
    IDirectionValue
{
    new IMemberDiagnose<TMemberValueType?> MemberDiagnose { get; }
    new ISingleValueAlwaysEditable<TMemberValueType?, bool> DiagnoseActive { get; }
    new ISingleValueEditable<TMemberValueType?, TMemberValueType?> DiagnoseValue { get; }
    new ISingleValue<TMemberValueType?, TMemberValueType?> OriginalValue { get; }
    new ISingleValue<TMemberValueType?, TMemberValueType?> CurrentValue { get; }
    bool Equals(IDirectionValue<TMemberValueType?>? other); 
    bool Equals(TMemberValueType? value);
}

public interface IDirectionValue<TOwnerType, TMemberValueType> : IDirectionValue<TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    new IMemberDiagnose<TOwnerType, TMemberValueType?> MemberDiagnose { get; }
    new ISingleValueAlwaysEditable<TOwnerType, TMemberValueType?, bool> DiagnoseActive { get; }
    new ISingleValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?> DiagnoseValue { get; }
    new ISingleValue<TOwnerType, TMemberValueType?, TMemberValueType?> OriginalValue { get; }
    new ISingleValue<TOwnerType, TMemberValueType?, TMemberValueType?> CurrentValue { get; }
}