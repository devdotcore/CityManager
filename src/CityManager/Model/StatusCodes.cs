using System.ComponentModel;

namespace CityManager.Model
{
    /// <summary>
    /// API Status codes for different scenarios
    /// </summary>
    public enum StatusCodes 
    {
        [Description("Success.")]
        SUCCESS = 200,

        [Description("Invalid Request. Check logs for details.")]
        INVALID_REQUEST = 400,

        [Description("Not Found, Lookup failed - Doesn't exists in database.")]
        NOT_FOUND = 404,

        [Description("System Exception Occurred. Check logs for details.")]
        SYSTEM_ERROR = 500
    }

}