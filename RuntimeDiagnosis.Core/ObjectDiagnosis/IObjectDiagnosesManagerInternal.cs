namespace RuntimeDiagnosis.Core.ObjectDiagnosis;

public interface IObjectDiagnosesManagerInternal : IObjectDiagnosesManager
{
    internal void AddNewObjectDiagnose(IObjectDiagnosis objectDiagnosis);
}