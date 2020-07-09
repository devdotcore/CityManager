using System.ComponentModel;

namespace CityManager.Model
{
    /// <summary>
    /// Return api status response code for adding an city to the data base
    /// </summary>
    public enum StatusCodes 
    {
        [Description("City details saved successfully.")]
        SUCCESS = 200,

        [Description("End API Invalid Request; Check Logs")]
        INVALID_REQUEST = 400,

        [Description("Invalid request, Lookup failed - Doesn't exists in database")]
        NOT_FOUND = 404,

        [Description("Error while saving details - System Exception, Check Logs.")]
        SYSTEM_ERROR = 500
    }

}