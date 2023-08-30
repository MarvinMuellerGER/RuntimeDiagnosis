namespace RuntimeDiagnosis.Core.ObjectDiagnosis;

// TODO: Write Summaries
public interface IObjectDiagnosesManager
{
    IEnumerable<IObjectDiagnosis> ObjectDiagnoses { get; }
    event EventHandler<IObjectDiagnosis>? NewObjectDiagnoseCreated;
}