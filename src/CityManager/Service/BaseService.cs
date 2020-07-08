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
        /// Handle Error Response in Service Layer
        /// </summary>
        /// <param name="args">Error Params</param>
        /// <typeparam name="TError">Class Type</typeparam>
        /// <returns></returns>
        public TError GetErrorResponse<TError>(params object[] args)
        {
            return (TError)Activator.CreateInstance(typeof(TError), args);
        }
    }
}