using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis;

public static class ObjectDiagnosesManager
{
    private static readonly List<IObjectDiagnosis> ObjectDiagnosesInternal = new();

    public static IEnumerable<IObjectDiagnosis> ObjectDiagnoses => ObjectDiagnosesInternal;

    public static event EventHandler<IObjectDiagnosis>? NewObjectDiagnoseCreated;

    public static ObjectDiagnosis<TOwnerType> CreateNewObjectDiagnose<TOwnerType>(TOwnerType owner, 
        Func<ObjectDiagnosis<TOwnerType>, IEnumerable<IMemberDiagnosis>> createMemberDiagnoses, 
        Action<string> invokePropertyChanged) 
        where TOwnerType : IDiagnosableObject
    {
        var objectDiagnose = new ObjectDiagnosis<TOwnerType>(owner, invokePropertyChanged);
        objectDiagnose.MemberDiagnoses = createMemberDiagnoses(objectDiagnose);
        AddNewObjectDiagnose(objectDiagnose);
        return objectDiagnose;
    }

    private static void AddNewObjectDiagnose(IObjectDiagnosis objectDiagnosis)
    {
        ObjectDiagnosesInternal.Add(objectDiagnosis);
        NewObjectDiagnoseCreated?.Invoke(null, objectDiagnosis);
    }
}