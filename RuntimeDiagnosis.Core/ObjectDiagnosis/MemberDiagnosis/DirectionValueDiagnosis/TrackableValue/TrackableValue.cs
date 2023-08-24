using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

[DebuggerDisplay($"{{ToString()}} ({{ToShortCurrentValueString()}})")]
public class TrackableValue<TOwnerType, TMemberValueType, TValueType> : 
    ITrackableValue<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    private TValueType? _value;
    private ushort _getCalledCount;
    private ushort _setCalledCount;
    private ushort _changedCount;
    private EventHandler<object?>? _valueChangedHandler;
    
    public string Name { get; }

    IDirectionValueDiagnosis ITrackableValue.DirectionValueDiagnosis => DirectionValueDiagnosis;

    IDirectionValueDiagnosis<TMemberValueType?> ITrackableValue<TMemberValueType?, TValueType?>.DirectionValueDiagnosis => DirectionValueDiagnosis;

    public IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> DirectionValueDiagnosis { get; }

    object? ITrackableValue.Value => Value;

    TValueType? ITrackableValue<TValueType?>.Value
    {
        get => Value;
        set => Value = value;
    }

    public TValueType? Value
    {
        get => GetValue();
        internal set => SetValue(value);
    }

    public ushort GetCalledCount
    {
        get => _getCalledCount;
        private set => SetField(ref _getCalledCount, value);
    }

    public ushort SetCalledCount
    {
        get => _setCalledCount;
        private set => SetField(ref _setCalledCount, value);
    }

    public ushort ChangedCount
    {
        get => _changedCount;
        private set => SetField(ref _changedCount, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    event EventHandler<object?>? ITrackableValue.ValueChanged
    {
        add => _valueChangedHandler += value;
        remove => _valueChangedHandler -= value;
    }

    public event EventHandler<TValueType?>? ValueChanged;
    
    public event EventHandler? ValueChangedUnified;

    public TrackableValue(IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> directionValueDiagnosis, string name)
    {
        DirectionValueDiagnosis = directionValueDiagnosis;
        Name = name;
        AttachEventHandlers();
    }
    
    public static bool operator ==(TrackableValue<TOwnerType, TMemberValueType, TValueType> obj1, ITrackableValue? obj2) =>
        obj1.Equals(obj2);

    public static bool operator !=(TrackableValue<TOwnerType, TMemberValueType, TValueType> obj1, ITrackableValue? obj2) => 
        !(obj1 == obj2);
    
    public static bool operator ==(
        TrackableValue<TOwnerType, TMemberValueType, TValueType> obj1, ITrackableValue<TValueType?>? obj2) =>
        obj1.Equals(obj2);

    public static bool operator !=(
        TrackableValue<TOwnerType, TMemberValueType, TValueType> obj1, ITrackableValue<TValueType?>? obj2) => 
        !(obj1 == obj2);
    
    public static bool operator ==(TrackableValue<TOwnerType, TMemberValueType, TValueType> obj, TValueType? value) =>
        obj.Equals(value);

    public static bool operator !=(TrackableValue<TOwnerType, TMemberValueType, TValueType> obj, TValueType? value) => 
        !(obj == value);

    public bool Equals(ITrackableValue? other) =>
        other != null && Equals(Value, other.Value);

    public bool Equals(ITrackableValue<TValueType?>? other) =>
        other != null && EqualityComparer<TValueType?>.Default.Equals(Value, other.Value);

    public bool Equals(TValueType? value) =>
        EqualityComparer<TValueType?>.Default.Equals(Value, value);

    public override bool Equals(object? obj) =>
        obj switch
        {
            ITrackableValue<TValueType?> other2 => Equals(other2),
            ITrackableValue other1 => Equals(other1),
            TValueType value => Equals(value),
            _ => false
        };

    public override int GetHashCode() => 
        HashCode.Combine(DirectionValueDiagnosis, Name);

    public override string ToString() =>
        $"{Name} of {DirectionValueDiagnosis.GetTypeNameWithoutGenericArity()} for " +
        $"{DirectionValueDiagnosis.MemberDiagnosis.MemberName} of " +
        $"{DirectionValueDiagnosis.MemberDiagnosis.ObjectDiagnosis.GetOwnerTypeString()}";

    public string ToCurrentValueString() =>
        $"{Name}: {_value}";
    
    [UsedImplicitly]
    public string ToShortCurrentValueString() =>
        $"{nameof(Value)}: {_value}";

    internal void SetValue(TValueType? value, bool setAgainEvenIfNotChanged = false)
    {
        SetCalledCount++;
        if (SetField(ref _value, value, setAgainEvenIfNotChanged, nameof(Value)))
            ChangedCount++;
    }

    private TValueType? GetValue()
    {
        GetCalledCount++;
        return _value;
    }

    protected bool SetField<T>(
        ref T field, T value, bool setAgainEvenIfNotChanged = false, [CallerMemberName] string? propertyName = null)
    {
        if (!setAgainEvenIfNotChanged && EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        InvokePropertyChanged(propertyName);
        return true;
    }

    private void InvokePropertyChanged([CallerMemberName] string? propertyName = null) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void AttachEventHandlers()
    {
        ValueChanged += OnValueChanged;
        PropertyChanged += OnPropertyChanged;
    }

    private void OnValueChanged(object? sender, TValueType? value) => 
        _valueChangedHandler?.Invoke(sender, value);

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Value):
                InvokeValueChanged();
                break;
        }
    }
    
    private void InvokeValueChanged()
    {
        ValueChanged?.Invoke(this, Value);
        ValueChangedUnified?.Invoke(this, EventArgs.Empty);
    }
}