namespace BattleSimulator.CrossCutting
{
    public interface IResponse
    {
        bool Success { get; }
    }
    
    public class SuccessResponse : IResponse
    {
        public bool Success => true;
    }
    
    public class ErrorResponse : IResponse
    {
        public string Error { get; }

        public ErrorResponse(string error)
        {
            Error = error;
        }

        public bool Success => false;
    }
}
