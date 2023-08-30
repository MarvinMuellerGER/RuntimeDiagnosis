using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;
using static RuntimeDiagnosis.Kit.GenericEventAttaching;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

// TODO: Implement Unit Tests
public sealed class OutputValueDiagnosis<TOwnerType, TMemberValueType> : 
    DirectionValueDiagnosis<TOwnerType, TMemberValueType?>, IOutputValueDiagnosisInternal<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    TMemberValueType? IOutputValueDiagnosis<TMemberValueType?>.Value
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

    public OutputValueDiagnosis(
        IObjectDiagnosesManagerInternal objectDiagnosesManager,
        IDirectionValueDiagnosesFinder directionValueDiagnosesFinder,
        ITrackableValueAlwaysEditableInternal<TOwnerType,TMemberValueType?,bool> diagnoseActive,
        ITrackableValueEditableInternal<TOwnerType, TMemberValueType?, TMemberValueType?> diagnoseValue,
        ITrackableValueInternal<TOwnerType,TMemberValueType?,TMemberValueType?> originalValue,
        ITrackableValueInternal<TOwnerType,TMemberValueType?,TMemberValueType?> currentValue) :
        base(objectDiagnosesManager, directionValueDiagnosesFinder, 
            diagnoseActive, diagnoseValue, originalValue, currentValue)
    { }

    public void Initialize(IMemberDiagnosis<TOwnerType, TMemberValueType?> memberDiagnosis, 
        IEnumerable<DirectionValueDefinition> callerDefinitions, Action<EventHandler> attachToInputValueChanged)
    {
        base.Initialize(memberDiagnosis, callerDefinitions);
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

    private void OnCurrentValueChanged(object? sender, EventArgs e) => 
        MemberDiagnosis.ObjectDiagnosis.InvokeOwnerPropertyChanged(MemberDiagnosis.MemberName);
    
    private void UpdateValue()
    {
        if (DiagnoseActive.Value && !UpdateOriginalValueWhenDiagnosisActive)
            return;
        OriginalValueInternal.Value = MemberDiagnosis.MemberValue;
    }
}