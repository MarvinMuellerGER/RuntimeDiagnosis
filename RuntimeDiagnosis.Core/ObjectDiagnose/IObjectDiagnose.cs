using System.Runtime.CompilerServices;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;

namespace RuntimeDiagnosis.Core.ObjectDiagnose;

public interface IObjectDiagnose
{
    IDiagnosableObject Owner { get; }
    IEnumerable<IMemberDiagnose> MemberDiagnoses { get; }
    Type OwnerBaseType { get; }
    string GetOwnerTypeString();
    IMemberDiagnose? GetMemberDiagnose([CallerMemberName] string memberName = "");
    IMemberDiagnose<TMemberValueType?>? GetMemberDiagnose<TMemberValueType>([CallerMemberName] string memberName = "");
}

public interface IObjectDiagnose<TOwnerType> : IObjectDiagnose
    where TOwnerType : IDiagnosableObject
{
    new TOwnerType Owner { get; }
    new IMemberDiagnose<TOwnerType, TMemberValueType?>? GetMemberDiagnose<TMemberValueType>(
        [CallerMemberName] string memberName = "");
}