using System;
using AutoMapper;
using CityManager.Model;
using Microsoft.Extensions.Logging;

namespace CityManager.Service
{
    public abstract class BaseService<T> : IBaseService<T>
    {
        /// <summary>
        /// Service Logger
        /// </summary>
        public readonly ILogger<T> _logger;

        /// <summary>
        /// AutoMapper
        /// </summary>
        public readonly IMapper _mapper;

        public BaseService(ILogger<T> logger)
        {
            _logger = logger;
        }

        public BaseService(ILogger<T> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Handle common or error Response in Service Layer
        /// </summary>
        /// <param name="args">Error Params</param>
        /// <typeparam name="TServiceCode">Class Type</typeparam>
        /// <returns></returns>
        public TServiceCode GetServiceCode<TServiceCode>(params object[] args)
        {
            return (TServiceCode)Activator.CreateInstance(typeof(TServiceCode), args);
        }
    }
}