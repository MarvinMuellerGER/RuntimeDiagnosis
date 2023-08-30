namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;

// TODO: Write Summaries
// TODO: Implement Unit Tests
public static class ObjectExtensions
{
    public static bool Equals(this object obj, IDirectionValueDiagnosis directionValueDiagnosis) =>
        directionValueDiagnosis.Equals(obj);
}