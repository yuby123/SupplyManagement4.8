using System.Net;

namespace SupplyManagement.Utilities.Handler
{
    public class ResponseOKHandler<TEntity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }

        public ResponseOKHandler(TEntity data)
        {
            Code = (int)HttpStatusCode.OK;
            Status = HttpStatusCode.OK.ToString();
            Message = "Success to Retrieve Data";
            Data = data;
        }

        public ResponseOKHandler(string message)
        {
            Code = (int)HttpStatusCode.OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
        }

        public ResponseOKHandler(string message, TEntity data)
        {
            Code = (int)HttpStatusCode.OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
            Data = data;
        }

        public ResponseOKHandler()
        {

        }
    }
}
