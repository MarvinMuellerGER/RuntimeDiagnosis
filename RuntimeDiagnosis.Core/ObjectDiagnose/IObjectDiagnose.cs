using System.Runtime.CompilerServices;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;

namespace RuntimeDiagnosis.Core.ObjectDiagnose;

public interface IObjectDiagnose
{
    object Owner { get; }
    List<IMemberDiagnose> MemberDiagnoses { get; }
    Type OwnerBaseType { get; }
    string GetOwnerTypeString();
    IMemberDiagnose? GetMemberDiagnose([CallerMemberName] string memberName = "");
    IMemberDiagnose<TMemberValueType?>? GetMemberDiagnose<TMemberValueType>([CallerMemberName] string memberName = "");
    TMemberValueType? GetCurrentMemberValue<TMemberValueType>(in TMemberValueType? internalProperty,
        [CallerMemberName] string memberName = "");
    void SetOriginalMemberValue<TMemberValueType>(in Action<TMemberValueType?> setMemberValue,
        in TMemberValueType? value, [CallerMemberName] string memberName = "");
    void AddMember<TMemberValueType>(
        in string memberName,
        Func<TMemberValueType?> getMemberInputValue,
        Action<TMemberValueType?> setMemberInputValue);
}

public interface IObjectDiagnose<TOwnerType> : IObjectDiagnose
    where TOwnerType : IDiagnosableObject
{
    new TOwnerType Owner { get; }
    new MemberDiagnose<TOwnerType, TMemberValueType?>? GetMemberDiagnose<TMemberValueType>(
        [CallerMemberName] string memberName = "");
}