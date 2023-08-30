namespace RuntimeDiagnosis.Core.ObjectDiagnosis;

public interface IObjectDiagnosesManager
{
    IEnumerable<IObjectDiagnosis> ObjectDiagnoses { get; }
    event EventHandler<IObjectDiagnosis>? NewObjectDiagnoseCreated;
}