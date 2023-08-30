namespace RuntimeDiagnosis.Core.ObjectDiagnosis.Kit;

public interface IObjectRegistry
{
    IEnumerable<object> RegisteredObjects { get; }
    event EventHandler<object?>? NewObjectRegistered;
}

public interface IObjectRegistry<T> : IObjectRegistry
{
    new IEnumerable<T> RegisteredObjects { get; }
    new event EventHandler<T>? NewObjectRegistered;
}