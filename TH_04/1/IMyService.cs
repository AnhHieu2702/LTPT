using System.ServiceModel;

[ServiceContract]
public interface IMyService
{
    [OperationContract]
    int SumArray(string arrayStr);
    
    [OperationContract]
    void Acknowledge(string message);
}
