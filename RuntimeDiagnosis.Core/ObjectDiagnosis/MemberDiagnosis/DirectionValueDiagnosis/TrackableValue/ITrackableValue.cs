using System.ComponentModel;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;

// TODO: Write Summaries
public interface ITrackableValue : IProvidesCurrentValueString, INotifyPropertyChanged, IEquatable<ITrackableValue>
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

public interface ITrackableValue<TValueType> : ITrackableValue
{
    new TValueType? Value { get; internal set; }
    new event EventHandler<TValueType?>? ValueChanged;
    bool Equals(ITrackableValue<TValueType?>? other);
    bool Equals(TValueType? value);
}

public interface ITrackableValue<TMemberValueType, TValueType> : ITrackableValue<TValueType?>
{
    new IDirectionValueDiagnosis<TMemberValueType?> DirectionValueDiagnosis { get; }
}

public interface ITrackableValue<TOwnerType, TMemberValueType, TValueType> : ITrackableValue<TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    new IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> DirectionValueDiagnosis { get; }
}