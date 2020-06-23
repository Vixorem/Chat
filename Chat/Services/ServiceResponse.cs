﻿using System;
 using System.Runtime.Serialization;
 using Microsoft.Extensions.Logging;

namespace Chat.Services
{
    /// <summary>
    /// Используется как возвращаемое значение для методов сервисов
    /// </summary>
    [DataContract]
    public class ServiceResponse
    {
        /// <summary>
        /// Состояние возвращаемого значение
        /// </summary>
        [DataMember]
        public ServiceResultType ResultType { get; protected set; }

        /// <summary>
        /// Сообщение об ошибке. Если сервис отработал с ошибками, содержит сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; protected set; }

        /// <summary>
        /// Вызывается при успешном возврате
        /// </summary>
        public static ServiceResponse Ok()
        {
            return new ServiceResponse
            {
                ResultType = ServiceResultType.Ok,
                ErrorMessage = string.Empty
            };
        }

        /// <summary>
        /// Вызывается при ошибках
        /// </summary>
        /// <param name="ex">Экземпляр исключения</param>
        public static ServiceResponse Fail(Exception ex)
        {
            return new ServiceResponse
            {
                ResultType = ServiceResultType.Exception,
                ErrorMessage = ex.Message
            };
        }

        /// <summary>
        /// Вызывается при пользовательских ошибках
        /// </summary>
        /// <param name="message">Сообщение ошибки</param>
        public static ServiceResponse Warning(string message)
        {
            return new ServiceResponse
            {
                ResultType = ServiceResultType.Warning,
                ErrorMessage = message
            };
        }
    }
}