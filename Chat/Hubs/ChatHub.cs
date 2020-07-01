using System;
using System.Threading.Tasks;
using Chat.ClientModels;
using Chat.Repositories.Abstracts;
using Chat.Services;
using Chat.Services.Abstracts;
using Chat.Utils.Extensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chat.Hubs
{
    /// <summary>
    /// Хаб
    /// </summary>
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IGroupService _groupService;

        /// <summary>
        /// .ctor
        /// </summary>
        public ChatHub(ILogger<ChatHub> logger, IUserService userService, IMessageService messageService,
            IGroupService groupService)
        {
            _logger = logger;
            _userService = userService;
            _messageService = messageService;
            _groupService = groupService;
        }

        /// <summary>
        /// Создание группы
        /// </summary>
        /// <param name="data">json строка с данными о новой группе</param>
        public async Task CreateGroup(string data)
        {
            var jsonObject = JObject.Parse(data);
            var groupCreatorId = Guid.Parse(JObject.Parse(data)["creatorId"].ToString());
            var dto = jsonObject.ToGroupDto();
            var response = _groupService.Add(dto.Name, groupCreatorId);
            await Clients.All.SendAsync("AcceptGroupCreation", response);
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="message">json строка MessageDto</param>
        public async Task SendMessage(string message)
        {
            var jsonObject = JObject.Parse(message);
            var dto = jsonObject.ToMessageDto();
            var res = _messageService.SaveTextMessage(dto.Sender.Id, dto.ReceiverId, dto.Content);
            res.Value.ClientMessageId = dto.ClientMessageId;
            res.Value.Sender = dto.Sender;
            await Clients.All.SendAsync("ReceiveMessage", res);
        }
    }
}