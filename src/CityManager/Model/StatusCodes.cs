using System.ComponentModel;

namespace CityManager.Model
{
    /// <summary>
    /// Return api status response code for adding an city to the data base
    /// </summary>
    public enum StatusCodes 
    {
        [Description("City Details Successfully saved to provided storage.")]
        SUCCESS = 200,

        [Description("End API Invalid Request; Check Logs")]
        INVALID_REQUEST = 400,

        [Description("Invalid request, lookup failed - unable to find")]
        NOT_FOUND = 404,

        [Description("Error while saving details - System Exceptions, Check Logs.")]
        SYSTEM_ERROR = 500
    }

}