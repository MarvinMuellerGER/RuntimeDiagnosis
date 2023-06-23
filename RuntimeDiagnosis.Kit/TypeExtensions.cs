namespace RuntimeDiagnosis.Kit;

public static class TypeExtensions
{
    public static string GetNameWithoutGenericArity(this Type type)
    {
        var name = type.Name;
        var index = name.IndexOf('`');
        return index == -1 ? name : name[..index];
    }
}