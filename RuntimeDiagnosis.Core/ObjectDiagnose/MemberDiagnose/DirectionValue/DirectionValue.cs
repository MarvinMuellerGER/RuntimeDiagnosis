using System.Diagnostics;
using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.Kit;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.SingleValue;
using RuntimeDiagnosis.Kit;
using static RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.IDirectionValue;
using static RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.IDirectionValue.ValueDirectionType;

namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;

[DebuggerDisplay("{ToString()} ({ToShortCurrentValueString()})")]
public abstract class DirectionValue<TOwnerType, TMemberValueType> : IDirectionValue<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly List<DirectionValueDefinition> _callerDefinitions;
    private readonly List<IDirectionValue> _callers = new();
    private readonly SingleValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?> _diagnoseValue;
    private readonly SingleValue<TOwnerType, TMemberValueType?, TMemberValueType?> _currentValueInternal;
    
    private protected readonly SingleValue<TOwnerType, TMemberValueType?, TMemberValueType?> OriginalValueInternal;
    
    private IDirectionValue? _currentCaller;

    public ValueDirectionType ValueDirection { get; }

    IMemberDiagnose IDirectionValue.MemberDiagnose => MemberDiagnose;

    IMemberDiagnose<TMemberValueType?> IDirectionValue<TMemberValueType?>.MemberDiagnose => MemberDiagnose;

    IMemberDiagnose<TOwnerType, TMemberValueType?> IDirectionValue<TOwnerType, TMemberValueType?>.MemberDiagnose => 
        MemberDiagnose;

    public IMemberDiagnose<TOwnerType, TMemberValueType?> MemberDiagnose { get; }

    public IEnumerable<IDirectionValue> Callers => _callers;

    public IDirectionValue? LastCaller { get; private set; }

    ISingleValueAlwaysEditable<bool> IDirectionValue.DiagnoseActive => 
        DiagnoseActive;

    ISingleValueAlwaysEditable<TMemberValueType?, bool> IDirectionValue<TMemberValueType?>.DiagnoseActive => 
        DiagnoseActive;
    
    public ISingleValueAlwaysEditable<TOwnerType, TMemberValueType?, bool> DiagnoseActive { get; }

    ISingleValueEditable IDirectionValue.DiagnoseValue => DiagnoseValue;

    ISingleValueEditable<TMemberValueType?, TMemberValueType?> IDirectionValue<TMemberValueType?>.DiagnoseValue => 
        DiagnoseValue;

    public ISingleValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?> DiagnoseValue => _diagnoseValue;
    
    ISingleValue IDirectionValue.OriginalValue => OriginalValue;

    ISingleValue<TMemberValueType?, TMemberValueType?> IDirectionValue<TMemberValueType?>.OriginalValue => 
        OriginalValue;

    public ISingleValue<TOwnerType, TMemberValueType?, TMemberValueType?> OriginalValue => OriginalValueInternal;

    ISingleValue IDirectionValue.CurrentValue => CurrentValue;

    ISingleValue<TMemberValueType?, TMemberValueType?> IDirectionValue<TMemberValueType?>.CurrentValue => 
        CurrentValue;
    
    public ISingleValue<TOwnerType, TMemberValueType?, TMemberValueType?> CurrentValue => _currentValueInternal;
    
    private IDirectionValue? CurrentCaller
    {
        get => _currentCaller;
        set => LastCaller = _currentCaller = value;
    }

    public event JustCalledEventHandler? JustCalled;
    
    protected DirectionValue(IMemberDiagnose<TOwnerType, TMemberValueType?> memberDiagnose, 
        IEnumerable<DirectionValueDefinition> callerDefinitions)
    {
        ValueDirection = this is IInputValue ? Input : Output;
        
        DiagnoseActive = 
            new SingleValueAlwaysEditable<TOwnerType, TMemberValueType?, bool>(this, nameof(DiagnoseActive));
        _diagnoseValue = 
            new SingleValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?>(this, nameof(DiagnoseValue));
        OriginalValueInternal = 
            new SingleValue<TOwnerType, TMemberValueType?, TMemberValueType?>(this, nameof(OriginalValue));
        _currentValueInternal = 
            new SingleValue<TOwnerType, TMemberValueType?, TMemberValueType?>(this, nameof(CurrentValue));
        
        MemberDiagnose = memberDiagnose;
        _callerDefinitions = callerDefinitions.ToList();

        AttachEventHandlers();
        GetCallersFromKnownObjectDiagnosesByCallerDefinitions();
    }
    
    public static bool operator ==(DirectionValue<TOwnerType, TMemberValueType> obj1, IDirectionValue? obj2) =>
        obj1.Equals(obj2);

    public static bool operator !=(DirectionValue<TOwnerType, TMemberValueType> obj1, IDirectionValue? obj2) => 
        !(obj1 == obj2);
    
    public static bool operator ==(
        DirectionValue<TOwnerType, TMemberValueType> obj1, IDirectionValue<TMemberValueType?>? obj2) =>
        obj1.Equals(obj2);

    public static bool operator !=(
        DirectionValue<TOwnerType, TMemberValueType> obj1, IDirectionValue<TMemberValueType?>? obj2) => 
        !(obj1 == obj2);
    
    public static bool operator ==(DirectionValue<TOwnerType, TMemberValueType> obj, TMemberValueType? value) =>
        obj.Equals(value);

    public static bool operator !=(DirectionValue<TOwnerType, TMemberValueType> obj, TMemberValueType? value) => 
        !(obj == value);

    public bool Equals(IDirectionValue? other) =>
        other != null && CurrentValue.Equals(other.CurrentValue);

    public bool Equals(IDirectionValue<TMemberValueType?>? other) =>
        other != null && CurrentValue.Equals(other.CurrentValue);

    public bool Equals(TMemberValueType? value) =>
        CurrentValue.Equals(value);

    public override bool Equals(object? obj) =>
        obj switch
        {
            IDirectionValue<TMemberValueType?> other2 => Equals(other2),
            IDirectionValue other1 => Equals(other1),
            TMemberValueType value => Equals(value),
            _ => false
        };

    public override int GetHashCode() => 
        HashCode.Combine(MemberDiagnose, GetType());
    
    public override string ToString() =>
        $"{GetNameWithoutGenericArity()} for {MemberDiagnose.MemberName} of " +
        $"{MemberDiagnose.ObjectDiagnose.GetOwnerTypeString()}";

    public string ToCurrentValueString() =>
        $"{GetNameWithoutGenericArity()}: {{{ToShortCurrentValueString()}}}";

    public string GetNameWithoutGenericArity() =>
        GetType().GetNameWithoutGenericArity();
    

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

    private void OnNewObjectDiagnoseCreated(object? sender, IObjectDiagnose objectDiagnose)
    {
        if (objectDiagnose == MemberDiagnose.ObjectDiagnose) 
            return;
        
        GetCallersFromObjectDiagnoseByCallerDefinitions(objectDiagnose);
    }

    private void GetCallersFromKnownObjectDiagnosesByCallerDefinitions() => 
        AddNewCallers(
            DirectionValuesFinder.GetDirectionValuesFromKnownObjectDiagnosesByDefinitions(
                _callerDefinitions));
    
    private void GetCallersFromObjectDiagnoseByCallerDefinitions(IObjectDiagnose objectDiagnose) => 
        AddNewCallers(
            DirectionValuesFinder.GetDirectionValuesFromObjectDiagnoseByDefinitions(objectDiagnose,
                _callerDefinitions));

    private void AddNewCallers(IEnumerable<IDirectionValue> newCallers)
    {
        var newCallersArray = newCallers.ToArray();
        
        if (!newCallersArray.Any())
            return;
        
        _callers.AddRange(newCallersArray);
        foreach (var caller in newCallersArray)
            caller.JustCalled += OnCallerJustCalled;
    }

    private void OnCallerJustCalled(IDirectionValue caller, IDirectionValue? _) => 
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