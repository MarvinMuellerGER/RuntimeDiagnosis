using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnose;

namespace RuntimeDiagnosis.Core;

public interface IDiagnosableObject : INotifyPropertyChanged
{
    public IObjectDiagnose ObjectDiagnose { get; }
}

public interface IDiagnosableObject<TOwnerType> : IDiagnosableObject 
    where TOwnerType : IDiagnosableObject
{
    public new IObjectDiagnose<TOwnerType> ObjectDiagnose { get; }
}