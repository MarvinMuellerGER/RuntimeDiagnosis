using RuntimeDiagnosis.Core.ObjectDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis;

public sealed class ObjectDiagnosesManager : IObjectDiagnosesManagerInternal
{
    private readonly IObjectRegistryInternal<IObjectDiagnosis> _objectRegistry;

    public ObjectDiagnosesManager(IObjectRegistryInternal<IObjectDiagnosis> objectRegistry) => 
        _objectRegistry = objectRegistry;

    public IEnumerable<IObjectDiagnosis> ObjectDiagnoses => _objectRegistry.RegisteredObjects;

    public event EventHandler<IObjectDiagnosis>? NewObjectDiagnoseCreated
    {
        add => _objectRegistry.NewObjectRegistered += value;
        remove => _objectRegistry.NewObjectRegistered -= value;
    }

    void IObjectDiagnosesManagerInternal.AddNewObjectDiagnose(IObjectDiagnosis objectDiagnosis) =>
        _objectRegistry.RegisterObject(objectDiagnosis);
}