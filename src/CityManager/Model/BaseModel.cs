namespace CityManager.Model
{

    public class BaseModel
    {
        /// <summary>
        /// If API has any error
        /// </summary>
        /// <value></value>
        public bool HasError { get; set; }

        /// <summary>
        /// Service code - populated if any error within response
        /// </summary>
        /// <value></value>
        public ServiceCode ServiceCode { get; set; }
    }
}