namespace RuntimeDiagnosis.Core.ObjectDiagnosis.Kit;

// TODO: Implement Unit Tests
public sealed class ObjectRegistry<T> : IObjectRegistryInternal<T>
{
    private readonly List<T> _registeredObjectsInternal = new();
    private event EventHandler<object?>? NewObjectRegisteredInternal;

    public IEnumerable<T> RegisteredObjects => _registeredObjectsInternal;

    IEnumerable<object> IObjectRegistry.RegisteredObjects => RegisteredObjects.Cast<object>();

    public event EventHandler<T>? NewObjectRegistered;

    event EventHandler<object?>? IObjectRegistry.NewObjectRegistered
    {
        add => NewObjectRegisteredInternal += value;
        remove => NewObjectRegisteredInternal -= value;
    }

    void IObjectRegistryInternal<T>.RegisterObject(T objectToRegister)
    {
        _registeredObjectsInternal.Add(objectToRegister);
        NewObjectRegistered?.Invoke(this, objectToRegister);
        NewObjectRegisteredInternal?.Invoke(this, objectToRegister);
    }
}