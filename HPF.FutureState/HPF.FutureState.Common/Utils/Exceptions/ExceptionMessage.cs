namespace HPF.FutureState.Common.Utils.Exceptions
{
    public class ExceptionMessage
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return ErrorCode + "--" + Message;
        }
    }
}
