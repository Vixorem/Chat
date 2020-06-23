﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chat.Services
{
    /// <inheritdoc/>
    public class ServiceResponse<T> : ServiceResponse
    {
        /// <summary>
        /// Хранит возвращаемое значение при успешной отработке сервиса, иначе default
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Вызывается при успешном возврате
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ServiceResponse<T> Ok(T value)
        {
            return new ServiceResponse<T>
            {
                ResultType = ServiceResultType.Ok,
                ErrorMessage = string.Empty,
                Value = value
            };
        }

        /// <summary>
        /// Вызывается при исключениях
        /// </summary>
        /// <param name="ex">Экземпляр исключения</param>
        public new static ServiceResponse<T> Fail(Exception ex)
        {
            return new ServiceResponse<T>
            {
                ResultType = ServiceResultType.Exception,
                ErrorMessage = ex.Message,
                Value = default
            };
        }

        /// <summary>
        /// Вызывается при пользовательских ошибках
        /// </summary>
        /// <param name="message">Сообщение ошибки</param>
        public new static ServiceResponse<T> Warning(string message)
        {
            return new ServiceResponse<T>
            {
                ResultType = ServiceResultType.Warning,
                ErrorMessage = message,
                Value = default
            };
        }
    }
}