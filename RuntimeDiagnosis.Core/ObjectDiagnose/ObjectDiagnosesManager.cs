using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;

namespace RuntimeDiagnosis.Core.ObjectDiagnose;

public static class ObjectDiagnosesManager
{
    private static readonly List<IObjectDiagnose> ObjectDiagnosesInternal = new();

    public static IEnumerable<IObjectDiagnose> ObjectDiagnoses => ObjectDiagnosesInternal;

    public static event EventHandler<IObjectDiagnose>? NewObjectDiagnoseCreated;

    public static ObjectDiagnose<TOwnerType> CreateNewObjectDiagnose<TOwnerType>(TOwnerType owner, 
        Func<ObjectDiagnose<TOwnerType>, IEnumerable<IMemberDiagnose>> createMemberDiagnoses, 
        Action<string> invokePropertyChanged) 
        where TOwnerType : IDiagnosableObject
    {
        var objectDiagnose = new ObjectDiagnose<TOwnerType>(owner, invokePropertyChanged);
        objectDiagnose.MemberDiagnoses = createMemberDiagnoses(objectDiagnose);
        AddNewObjectDiagnose(objectDiagnose);
        return objectDiagnose;
    }

    private static void AddNewObjectDiagnose(IObjectDiagnose objectDiagnose)
    {
        ObjectDiagnosesInternal.Add(objectDiagnose);
        NewObjectDiagnoseCreated?.Invoke(null, objectDiagnose);
    }
}