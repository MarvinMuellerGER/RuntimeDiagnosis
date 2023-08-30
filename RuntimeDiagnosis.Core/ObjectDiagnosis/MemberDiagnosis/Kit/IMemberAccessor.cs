namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;

public interface IMemberAccessor
{
    object? Value { get; set; }
}

public interface IMemberAccessor<T> : IMemberAccessor
{
    new T? Value { get; set; }
}