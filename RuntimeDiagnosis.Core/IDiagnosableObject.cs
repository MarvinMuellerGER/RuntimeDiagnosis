using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnosis;

namespace RuntimeDiagnosis.Core;

public interface IDiagnosableObject : INotifyPropertyChanged
{
    public IObjectDiagnosis ObjectDiagnosis { get; }
}

public interface IDiagnosableObject<TOwnerType> : IDiagnosableObject
    where TOwnerType : IDiagnosableObject<TOwnerType>
{
    public new IObjectDiagnosis<TOwnerType> ObjectDiagnosis { get; }
}