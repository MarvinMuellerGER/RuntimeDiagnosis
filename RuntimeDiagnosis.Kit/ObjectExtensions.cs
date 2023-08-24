namespace RuntimeDiagnosis.Kit;

public static class ObjectExtensions
{
    public static string GetTypeNameWithoutGenericArity(this object obj) =>
        obj.GetType().GetNameWithoutGenericArity();
}