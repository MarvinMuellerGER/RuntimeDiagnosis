using System.ComponentModel;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.SingleValue;

public interface ISingleValue : IProvidesCurrentValueString, INotifyPropertyChanged, IEquatable<ISingleValue>
{
    string Name { get; }
    IDirectionValueDiagnosis DirectionValueDiagnosis { get; }
    object? Value { get; }
    ushort GetCalledCount { get; }
    ushort SetCalledCount { get; }
    ushort ChangedCount { get; }
    event EventHandler<object?>? ValueChanged;
    event EventHandler? ValueChangedUnified;
    string ToString();
}

public interface ISingleValue<TValueType> : ISingleValue
{
    new TValueType? Value { get; internal set; }
    new event EventHandler<TValueType?>? ValueChanged;
    bool Equals(ISingleValue<TValueType?>? other);
    bool Equals(TValueType? value);
}

public interface ISingleValue<TMemberValueType, TValueType> : ISingleValue<TValueType?>
{
    new IDirectionValueDiagnosis<TMemberValueType?> DirectionValueDiagnosis { get; }
}

public interface ISingleValue<TOwnerType, TMemberValueType, TValueType> : ISingleValue<TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    new IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> DirectionValueDiagnosis { get; }
}