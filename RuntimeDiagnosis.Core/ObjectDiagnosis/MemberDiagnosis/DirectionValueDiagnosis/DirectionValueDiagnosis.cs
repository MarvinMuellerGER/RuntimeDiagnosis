using System.Diagnostics;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;
using RuntimeDiagnosis.Kit;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.IDirectionValueDiagnosis;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.IDirectionValueDiagnosis.ValueDirectionType;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;

[DebuggerDisplay("{ToString()} ({ToShortCurrentValueString()})")]
public abstract class DirectionValueDiagnosis<TOwnerType, TMemberValueType> : IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly List<DirectionValueDefinition> _callerDefinitions;
    private readonly List<IDirectionValueDiagnosis> _callers = new();
    private readonly TrackableValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?> _diagnoseValue;
    private readonly TrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?> _currentValueInternal;
    
    private protected readonly TrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?> OriginalValueInternal;
    
    private IDirectionValueDiagnosis? _currentCaller;

    public ValueDirectionType ValueDirection { get; }

    IMemberDiagnosis IDirectionValueDiagnosis.MemberDiagnosis => MemberDiagnosis;

    IMemberDiagnosis<TMemberValueType?> IDirectionValueDiagnosis<TMemberValueType?>.MemberDiagnosis => MemberDiagnosis;

    IMemberDiagnosis<TOwnerType, TMemberValueType?> IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>.MemberDiagnosis => 
        MemberDiagnosis;

    public IMemberDiagnosis<TOwnerType, TMemberValueType?> MemberDiagnosis { get; }

    public IEnumerable<IDirectionValueDiagnosis> Callers => _callers;

    public IDirectionValueDiagnosis? LastCaller { get; private set; }

    ITrackableValueAlwaysEditable<bool> IDirectionValueDiagnosis.DiagnoseActive => 
        DiagnoseActive;

    ITrackableValueAlwaysEditable<TMemberValueType?, bool> IDirectionValueDiagnosis<TMemberValueType?>.DiagnoseActive => 
        DiagnoseActive;
    
    public ITrackableValueAlwaysEditable<TOwnerType, TMemberValueType?, bool> DiagnoseActive { get; }

    ITrackableValueEditable IDirectionValueDiagnosis.DiagnoseValue => DiagnoseValue;

    ITrackableValueEditable<TMemberValueType?, TMemberValueType?> IDirectionValueDiagnosis<TMemberValueType?>.DiagnoseValue => 
        DiagnoseValue;

    public ITrackableValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?> DiagnoseValue => _diagnoseValue;
    
    ITrackableValue IDirectionValueDiagnosis.OriginalValue => OriginalValue;

    ITrackableValue<TMemberValueType?, TMemberValueType?> IDirectionValueDiagnosis<TMemberValueType?>.OriginalValue => 
        OriginalValue;

    public ITrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?> OriginalValue => OriginalValueInternal;

    ITrackableValue IDirectionValueDiagnosis.CurrentValue => CurrentValue;

    ITrackableValue<TMemberValueType?, TMemberValueType?> IDirectionValueDiagnosis<TMemberValueType?>.CurrentValue => 
        CurrentValue;
    
    public ITrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?> CurrentValue => _currentValueInternal;
    
    private IDirectionValueDiagnosis? CurrentCaller
    {
        get => _currentCaller;
        set => LastCaller = _currentCaller = value;
    }

    public event JustCalledEventHandler? JustCalled;
    
    protected DirectionValueDiagnosis(IMemberDiagnosis<TOwnerType, TMemberValueType?> memberDiagnosis, 
        IEnumerable<DirectionValueDefinition> callerDefinitions)
    {
        ValueDirection = this is IInputValueDiagnosis ? Input : Output;
        
        DiagnoseActive = 
            new TrackableValueAlwaysEditable<TOwnerType, TMemberValueType?, bool>(this, nameof(DiagnoseActive));
        _diagnoseValue = 
            new TrackableValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?>(this, nameof(DiagnoseValue));
        OriginalValueInternal = 
            new TrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?>(this, nameof(OriginalValue));
        _currentValueInternal = 
            new TrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?>(this, nameof(CurrentValue));
        
        MemberDiagnosis = memberDiagnosis;
        _callerDefinitions = callerDefinitions.ToList();

        AttachEventHandlers();
        GetCallersFromKnownObjectDiagnosesByCallerDefinitions();
    }
    
    public static bool operator ==(DirectionValueDiagnosis<TOwnerType, TMemberValueType> obj1, IDirectionValueDiagnosis? obj2) =>
        obj1.Equals(obj2);

    public static bool operator !=(DirectionValueDiagnosis<TOwnerType, TMemberValueType> obj1, IDirectionValueDiagnosis? obj2) => 
        !(obj1 == obj2);
    
    public static bool operator ==(
        DirectionValueDiagnosis<TOwnerType, TMemberValueType> obj1, IDirectionValueDiagnosis<TMemberValueType?>? obj2) =>
        obj1.Equals(obj2);

    public static bool operator !=(
        DirectionValueDiagnosis<TOwnerType, TMemberValueType> obj1, IDirectionValueDiagnosis<TMemberValueType?>? obj2) => 
        !(obj1 == obj2);
    
    public static bool operator ==(DirectionValueDiagnosis<TOwnerType, TMemberValueType> obj, TMemberValueType? value) =>
        obj.Equals(value);

    public static bool operator !=(DirectionValueDiagnosis<TOwnerType, TMemberValueType> obj, TMemberValueType? value) => 
        !(obj == value);

    public bool Equals(IDirectionValueDiagnosis? other) =>
        other != null && CurrentValue.Equals(other.CurrentValue);

    public bool Equals(IDirectionValueDiagnosis<TMemberValueType?>? other) =>
        other != null && CurrentValue.Equals(other.CurrentValue);

    public bool Equals(TMemberValueType? value) =>
        CurrentValue.Equals(value);

    public override bool Equals(object? obj) =>
        obj switch
        {
            IDirectionValueDiagnosis<TMemberValueType?> other2 => Equals(other2),
            IDirectionValueDiagnosis other1 => Equals(other1),
            TMemberValueType value => Equals(value),
            _ => false
        };

    public override int GetHashCode() => 
        HashCode.Combine(MemberDiagnosis, GetType());
    
    public override string ToString() =>
        $"{this.GetTypeNameWithoutGenericArity()} for {MemberDiagnosis.MemberName} of " +
        $"{MemberDiagnosis.ObjectDiagnosis.GetOwnerTypeString()}";

    public string ToCurrentValueString() =>
        $"{this.GetTypeNameWithoutGenericArity()}: {{{ToShortCurrentValueString()}}}";
    
    public bool SetDiagnoseValueAgain()
    {
        if (!DiagnoseActive.Value)
            return false;
        SetCurrentValue(DiagnoseValue.Value, true);
        return true;
    }

    protected virtual void SetCurrentValue(TMemberValueType? value, bool setAgainEvenIfNotChanged = false) =>
        _currentValueInternal.SetValue(value, setAgainEvenIfNotChanged);
    
    private string ToShortCurrentValueString() =>
        $"{CurrentValue.ToCurrentValueString()}, {DiagnoseActive.ToCurrentValueString()}";

    private void AttachEventHandlers()
    {
        ObjectDiagnosesManager.NewObjectDiagnoseCreated += OnNewObjectDiagnoseCreated;
        
        OriginalValue.ValueChangedUnified += OnValueChanged;
        DiagnoseActive.ValueChangedUnified += OnValueChanged;
        DiagnoseValue.ValueChangedUnified += OnValueChanged;
        DiagnoseActive.ValueChanged += DiagnoseActiveOnValueChanged;
    }

    private void OnNewObjectDiagnoseCreated(object? sender, IObjectDiagnosis objectDiagnosis)
    {
        if (objectDiagnosis == MemberDiagnosis.ObjectDiagnosis) 
            return;
        
        GetCallersFromObjectDiagnoseByCallerDefinitions(objectDiagnosis);
    }

    private void GetCallersFromKnownObjectDiagnosesByCallerDefinitions() => 
        AddNewCallers(
            DirectionValueDiagnosesFinder.GetDirectionValuesFromKnownObjectDiagnosesByDefinitions(
                _callerDefinitions));
    
    private void GetCallersFromObjectDiagnoseByCallerDefinitions(IObjectDiagnosis objectDiagnosis) => 
        AddNewCallers(
            DirectionValueDiagnosesFinder.GetDirectionValuesFromObjectDiagnoseByDefinitions(objectDiagnosis,
                _callerDefinitions));

    private void AddNewCallers(IEnumerable<IDirectionValueDiagnosis> newCallers)
    {
        var newCallersArray = newCallers.ToArray();
        
        if (!newCallersArray.Any())
            return;
        
        _callers.AddRange(newCallersArray);
        foreach (var caller in newCallersArray)
            caller.JustCalled += OnCallerJustCalled;
    }

    private void OnCallerJustCalled(IDirectionValueDiagnosis caller, IDirectionValueDiagnosis? _) => 
        CurrentCaller = caller;

    private void DiagnoseActiveOnValueChanged(object? sender, bool diagnoseActive) => 
        _diagnoseValue.EditingCurrentlyAllowed = diagnoseActive;

    private void OnValueChanged(object? sender, EventArgs _) => 
        SetCurrentValue(DiagnoseActive.Value ? DiagnoseValue.Value : OriginalValue.Value);

    protected void InvokeJustCalled()
    {
        JustCalled?.Invoke(this, CurrentCaller);
        CurrentCaller = null;
    }
}