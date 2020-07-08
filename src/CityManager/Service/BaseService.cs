using System;
using CityManager.Model;

namespace CityManager.Service
{
    public class BaseService
    {
        /// <summary>
        /// Handle Error Response in Service Layer
        /// </summary>
        /// <param name="args">Error Params</param>
        /// <typeparam name="T">Class Type</typeparam>
        /// <returns></returns>
        public T GetErrorResponse<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}