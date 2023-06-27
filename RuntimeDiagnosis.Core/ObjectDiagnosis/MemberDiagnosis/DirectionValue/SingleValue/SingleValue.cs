using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.SingleValue;

[DebuggerDisplay($"{{ToString()}} ({{ToShortCurrentValueString()}})")]
public class SingleValue<TOwnerType, TMemberValueType, TValueType> : 
    ISingleValue<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    private TValueType? _value;
    private ushort _getCalledCount;
    private ushort _setCalledCount;
    private ushort _changedCount;
    private EventHandler<object?>? _valueChangedHandler;
    
    public string Name { get; }

    IDirectionValue ISingleValue.DirectionValue => DirectionValue;

    IDirectionValue<TMemberValueType?> ISingleValue<TMemberValueType?, TValueType?>.DirectionValue => DirectionValue;

    public IDirectionValue<TOwnerType, TMemberValueType?> DirectionValue { get; }

    object? ISingleValue.Value => Value;

    TValueType? ISingleValue<TValueType?>.Value
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

    event EventHandler<object?>? ISingleValue.ValueChanged
    {
        add => _valueChangedHandler += value;
        remove => _valueChangedHandler -= value;
    }

    public event EventHandler<TValueType?>? ValueChanged;
    
    public event EventHandler? ValueChangedUnified;

    public SingleValue(IDirectionValue<TOwnerType, TMemberValueType?> directionValue, string name)
    {
        DirectionValue = directionValue;
        Name = name;
        AttachEventHandlers();
    }
    
    public static bool operator ==(SingleValue<TOwnerType, TMemberValueType, TValueType> obj1, ISingleValue? obj2) =>
        obj1.Equals(obj2);

    public static bool operator !=(SingleValue<TOwnerType, TMemberValueType, TValueType> obj1, ISingleValue? obj2) => 
        !(obj1 == obj2);
    
    public static bool operator ==(
        SingleValue<TOwnerType, TMemberValueType, TValueType> obj1, ISingleValue<TValueType?>? obj2) =>
        obj1.Equals(obj2);

    public static bool operator !=(
        SingleValue<TOwnerType, TMemberValueType, TValueType> obj1, ISingleValue<TValueType?>? obj2) => 
        !(obj1 == obj2);
    
    public static bool operator ==(SingleValue<TOwnerType, TMemberValueType, TValueType> obj, TValueType? value) =>
        obj.Equals(value);

    public static bool operator !=(SingleValue<TOwnerType, TMemberValueType, TValueType> obj, TValueType? value) => 
        !(obj == value);

    public bool Equals(ISingleValue? other) =>
        other != null && Equals(Value, other.Value);

    public bool Equals(ISingleValue<TValueType?>? other) =>
        other != null && EqualityComparer<TValueType?>.Default.Equals(Value, other.Value);

    public bool Equals(TValueType? value) =>
        EqualityComparer<TValueType?>.Default.Equals(Value, value);

    public override bool Equals(object? obj) =>
        obj switch
        {
            ISingleValue<TValueType?> other2 => Equals(other2),
            ISingleValue other1 => Equals(other1),
            TValueType value => Equals(value),
            _ => false
        };

    public override int GetHashCode() => 
        HashCode.Combine(DirectionValue, Name);

    public override string ToString() =>
        $"{Name} of {DirectionValue.GetNameWithoutGenericArity()} for " +
        $"{DirectionValue.MemberDiagnosis.MemberName} of " +
        $"{DirectionValue.MemberDiagnosis.ObjectDiagnosis.GetOwnerTypeString()}";

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