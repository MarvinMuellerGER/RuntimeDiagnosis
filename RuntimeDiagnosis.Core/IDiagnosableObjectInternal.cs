using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

namespace RuntimeDiagnosis.Core;

public interface IDiagnosableObjectInternal : IDiagnosableObject
{
    IEnumerable<Func<IObjectDiagnosisInternal, IMemberDiagnosis>> CreateMemberDiagnosisActions { get; }
    void InvokePropertyChanged(string propertyName);
}

public interface IDiagnosableObjectInternal<TOwnerType> : IDiagnosableObject<TOwnerType>, IDiagnosableObjectInternal
    where TOwnerType : IDiagnosableObject<TOwnerType>
{
}