using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

public interface IDirectionValueDiagnosis : IProvidesCurrentValueString, IEquatable<IDirectionValueDiagnosis?>
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
    ITrackableValueAlwaysEditable<bool> DiagnoseActive { get; }
    ITrackableValue OriginalValue { get; }
    ITrackableValueEditable DiagnoseValue { get; }
    ITrackableValue CurrentValue { get; }
    delegate void JustCalledEventHandler(IDirectionValueDiagnosis sender, IDirectionValueDiagnosis? caller);
    event JustCalledEventHandler? JustCalled;
    bool SetDiagnoseValueAgain();
}

public interface IDirectionValueDiagnosis<TMemberValueType> : 
    IDirectionValueDiagnosis
{
    new IMemberDiagnosis<TMemberValueType?> MemberDiagnosis { get; }
    new ITrackableValueAlwaysEditable<TMemberValueType?, bool> DiagnoseActive { get; }
    new ITrackableValueEditable<TMemberValueType?, TMemberValueType?> DiagnoseValue { get; }
    new ITrackableValue<TMemberValueType?, TMemberValueType?> OriginalValue { get; }
    new ITrackableValue<TMemberValueType?, TMemberValueType?> CurrentValue { get; }
    bool Equals(IDirectionValueDiagnosis<TMemberValueType?>? other); 
    bool Equals(TMemberValueType? value);
}

public interface IDirectionValueDiagnosis<TOwnerType, TMemberValueType> : IDirectionValueDiagnosis<TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    new IMemberDiagnosis<TOwnerType, TMemberValueType?> MemberDiagnosis { get; }
    new ITrackableValueAlwaysEditable<TOwnerType, TMemberValueType?, bool> DiagnoseActive { get; }
    new ITrackableValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?> DiagnoseValue { get; }
    new ITrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?> OriginalValue { get; }
    new ITrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?> CurrentValue { get; }
}