using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Chat.Services.Implementations;
using Newtonsoft.Json;

namespace Chat.ClientModels
{
    /// <summary>
    /// Клиентская модель пользователя
    /// </summary>
    [DataContract]
    public class UserDto
    {
        /// <summary>
        /// Guid пользователя
        /// </summary>
        [DataMember]
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [DataMember]
        [JsonProperty("Login")]
        public string Login { get; set; }

        /// <summary>
        /// Конвертирует модель бизнес-логики в клиентскую модель
        /// </summary>
        /// <param name="user">Экземпляр модели бизнес-логики</param>
        public static UserDto ConvertFromDomain(Models.User user)
        {
            var a = new UserDto
            {
                Id = user.Id,
                Login = user.Login
            };
            return a;
        }
    }
}