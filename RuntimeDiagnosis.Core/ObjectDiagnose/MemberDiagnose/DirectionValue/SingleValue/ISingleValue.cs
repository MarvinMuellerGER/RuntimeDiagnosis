using System.ComponentModel;

namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.SingleValue;

public interface ISingleValue : IProvidesCurrentValueString, INotifyPropertyChanged, IEquatable<ISingleValue>
{
    string Name { get; }
    IDirectionValue DirectionValue { get; }
    object? Value { get; }
    ushort SetCount { get; }
    event EventHandler<object?>? ValueChanged;
    event EventHandler? ValueChangedUnified;
    event EventHandler<ushort>? SetCountChanged;
    string ToString();
}

public interface ISingleValue<TValueType> : ISingleValue
{
    new TValueType? Value { get; }
    new event EventHandler<TValueType?>? ValueChanged;
    bool Equals(ISingleValue<TValueType?>? other);
    bool Equals(TValueType? value);
}

public interface ISingleValue<TMemberValueType, TValueType> : ISingleValue<TValueType?>
{
    new IDirectionValue<TMemberValueType?> DirectionValue { get; }
}

public interface ISingleValue<TOwnerType, TMemberValueType, TValueType> : ISingleValue<TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    new IDirectionValue<TOwnerType, TMemberValueType?> DirectionValue { get; }
}