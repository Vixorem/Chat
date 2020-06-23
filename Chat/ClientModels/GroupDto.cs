using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chat.ClientModels
{
    /// <summary>
    /// Клиентская модель группы
    /// </summary>
    [DataContract]
    public class GroupDto
    {
        /// <summary>
        /// Guid группы
        /// </summary>
        [DataMember]
        [JsonProperty("Id")]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Название группы
        /// </summary>
        [DataMember]
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Конвертирует модель бизнес-логики в клиентскую модель
        /// </summary>
        /// <param name="group">Экземпляр модели бизнес-логики</param>
        public static GroupDto ConvertFromDomain(Models.Group group)
        {
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name
            };
        }
    }
}