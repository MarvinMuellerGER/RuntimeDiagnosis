namespace RuntimeDiagnosis.Core.ObjectDiagnosis.Kit;

public interface IObjectRegistryInternal<T> : IObjectRegistry<T>
{
    internal void RegisterObject(T objectToRegister);
}