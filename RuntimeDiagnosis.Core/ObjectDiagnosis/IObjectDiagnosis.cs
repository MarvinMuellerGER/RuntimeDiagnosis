using System.Runtime.CompilerServices;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis;

public interface IObjectDiagnosis
{
    internal Action<string> InvokeOwnerPropertyChanged { get; }
    IDiagnosableObject Owner { get; }
    IEnumerable<IMemberDiagnosis> MemberDiagnoses { get; }
    Type OwnerBaseType { get; }
    string GetOwnerTypeString();
    IMemberDiagnosis? GetMemberDiagnose([CallerMemberName] string memberName = "");
    IMemberDiagnosis<TMemberValueType?>? GetMemberDiagnose<TMemberValueType>([CallerMemberName] string memberName = "");
}

public interface IObjectDiagnosis<TOwnerType> : IObjectDiagnosis
    where TOwnerType : IDiagnosableObject
{
    new TOwnerType Owner { get; }
    new IMemberDiagnosis<TOwnerType, TMemberValueType?>? GetMemberDiagnose<TMemberValueType>(
        [CallerMemberName] string memberName = "");
}