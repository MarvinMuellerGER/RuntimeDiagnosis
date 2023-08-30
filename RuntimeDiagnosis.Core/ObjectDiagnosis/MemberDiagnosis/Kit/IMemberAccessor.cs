namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;

/// <summary>
/// Helper to access (private) member (property or field) of the type T at runtime by just passing a lambda expression
/// </summary>
public interface IMemberAccessor
{
    /// <summary>
    /// Gets or sets the value of the member in the object of type T, represented as an object instance.
    /// </summary>
    object? Value { get; set; }
}

/// <inheritdoc />
/// <typeparam name="TMember">Datatype of the accessed member</typeparam>
public interface IMemberAccessor<TMember> : IMemberAccessor
{
    /// <summary>
    /// Gets or sets the value of the member in the object of type T.
    /// </summary>
    /// <typeparam name="TMember">Datatype of the accessed member</typeparam>
    new TMember? Value { get; set; }
}