using static RuntimeDiagnosis.Kit.EventAttacher;

namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;

public sealed class OutputValue<TOwnerType, TMemberValueType> : 
    DirectionValue<TOwnerType, TMemberValueType?>, IOutputValue<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action _invokeOwnerPropertyChanged;
    private readonly Func<TMemberValueType?> _getOriginalValue;
    
    public OutputValue(IMemberDiagnose<TOwnerType, TMemberValueType?> memberDiagnose,
        Action invokeOwnerPropertyChanged,
        Func<TMemberValueType?> getOriginalOutputValue,
        Action<EventHandler> attachToOriginalValueMightChanged) :
        base(memberDiagnose)
    {
        _invokeOwnerPropertyChanged = invokeOwnerPropertyChanged;
        _getOriginalValue = getOriginalOutputValue;

        AttachEventHandlers(attachToOriginalValueMightChanged);
    }
    
    private void AttachEventHandlers(Action<EventHandler> attachToOriginalValueMightChanged)
    {
        AttachToOriginalValueMightChanged(attachToOriginalValueMightChanged);
        
        CurrentValue.ValueChangedUnified += OnCurrentValueChanged;
    }

    private void AttachToOriginalValueMightChanged(Action<EventHandler> attachToOriginalValueMightChanged) =>
        AttachToEvent(attachToOriginalValueMightChanged, OnOriginalValueMightChanged);

    private void OnOriginalValueMightChanged(object? sender, EventArgs _) =>
        OriginalValueInternal.Value = _getOriginalValue();

    private void OnCurrentValueChanged(object? sender, EventArgs e) => _invokeOwnerPropertyChanged();
}