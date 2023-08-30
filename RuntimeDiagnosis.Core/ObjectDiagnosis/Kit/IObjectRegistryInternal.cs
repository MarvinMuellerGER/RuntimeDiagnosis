namespace RuntimeDiagnosis.Core.ObjectDiagnosis.Kit;

// TODO: Write Summaries
public interface IObjectRegistryInternal<T> : IObjectRegistry<T>
{
    internal void RegisterObject(T objectToRegister);
}