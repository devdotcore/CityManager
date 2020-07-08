using System;
using CityManager.Helper;

namespace CityManager.Model
{
    public class ServiceCode
    {
        public ServiceCode() { }
        public ServiceCode(int code)
        {
            //ToDo: check the condition again?
            if (code != (int)StatusCodes.SUCCESS)
            {
                this.Code = (StatusCodes)code;
            }
        }
        /// <summary>
        /// Get Service Code
        /// </summary>
        /// <value></value>
        public StatusCodes Code { get; set; }

        /// <summary>
        /// Get Message in detail
        /// </summary>
        /// <returns></returns>
        public string Message { get { return Code.GetDescription(); } }

    }
}