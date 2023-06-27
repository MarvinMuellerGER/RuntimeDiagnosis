using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;
using static RuntimeDiagnosis.Kit.EventAttacher;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue;

public sealed class OutputValue<TOwnerType, TMemberValueType> : 
    DirectionValue<TOwnerType, TMemberValueType?>, IOutputValue<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action _invokeOwnerPropertyChanged;
    private readonly Func<TMemberValueType?> _getOriginalValue;

    TMemberValueType? IOutputValue<TMemberValueType?>.Value
    {
        get
        {
            InvokeJustCalled();
            UpdateValue();
            return CurrentValue.Value;
        }
    }

    public bool UpdateWhenInputValueChanged { get; set; } = true;
    
    public bool UpdateOriginalValueWhenDiagnosisActive { get; set; } = true;

    public OutputValue(IMemberDiagnosis<TOwnerType, TMemberValueType?> memberDiagnosis, 
        IEnumerable<DirectionValueDefinition> callerDefinitions,
        Action invokeOwnerPropertyChanged,
        Func<TMemberValueType?> getOriginalOutputValue,
        Action<EventHandler> attachToInputValueChanged) :
        base(memberDiagnosis, callerDefinitions)
    {
        _invokeOwnerPropertyChanged = invokeOwnerPropertyChanged;
        _getOriginalValue = getOriginalOutputValue;

        AttachEventHandlers(attachToInputValueChanged);
    }
    
    private void AttachEventHandlers(Action<EventHandler> attachToInputValueChanged)
    {
        AttachToInputValueChanged(attachToInputValueChanged);
        
        CurrentValue.ValueChangedUnified += OnCurrentValueChanged;
    }

    private void AttachToInputValueChanged(Action<EventHandler> attachToInputValueChanged) =>
        AttachToEvent(attachToInputValueChanged, OnInputValueChanged);

    private void OnInputValueChanged(object? sender, EventArgs _)
    {
        if (UpdateWhenInputValueChanged)
            UpdateValue();
    }

    private void OnCurrentValueChanged(object? sender, EventArgs e) => _invokeOwnerPropertyChanged();
    
    private void UpdateValue()
    {
        if (DiagnoseActive.Value && !UpdateOriginalValueWhenDiagnosisActive)
            return;
        OriginalValueInternal.Value = _getOriginalValue();
    }
}