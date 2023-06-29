namespace RuntimeDiagnosis.Core.ObjectDiagnosis;

public static class ObjectDiagnosesManager
{
    private static readonly List<IObjectDiagnosis> ObjectDiagnosesInternal = new();

    public static IEnumerable<IObjectDiagnosis> ObjectDiagnoses => ObjectDiagnosesInternal;

    public static event EventHandler<IObjectDiagnosis>? NewObjectDiagnoseCreated;

    internal static void AddNewObjectDiagnose(IObjectDiagnosis objectDiagnosis)
    {
        ObjectDiagnosesInternal.Add(objectDiagnosis);
        NewObjectDiagnoseCreated?.Invoke(null, objectDiagnosis);
    }
}