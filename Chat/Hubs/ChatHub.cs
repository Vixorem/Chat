using System;
using System.Threading.Tasks;
using Chat.ClientModels;
using Chat.Repositories.Abstracts;
using Chat.Services;
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
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IGroupRepository _groupRepository;

        /// <summary>
        /// .ctor
        /// </summary>
        public ChatHub(ILogger<ChatHub> logger, IUserRepository userRepository, IMessageRepository messageRepository,
            IGroupRepository groupRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Сохраняет и переотправляет полученное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public async Task SendMessage(string message)
        {
            var jsonObject = JObject.Parse(message);
            var senderId = Guid.Parse(jsonObject["sender"]["id"].ToString());
            var senderLogin = jsonObject["sender"]["id"].ToString();
            var receiverId = Guid.Parse(jsonObject["receiverId"].ToString());
            var content = jsonObject["content"].ToString();
            var clientMesId = jsonObject["clientMessageId"].ToString();
            var sentTime = DateTime.Now;
            var id = _messageRepository.SaveMessage(senderId, receiverId, content, sentTime);
            var messageObj = new MessageDto
            {
                Id = id,
                Content = content,
                Sender = new UserDto
                {
                    Id = senderId,
                    Login = senderLogin
                },
                ReceiverId = receiverId,
                SentTime = sentTime,
                Status = Status.Received,
                ClientMessageId = clientMesId
            };
            await Clients.All.SendAsync("ReceiveMessage", ServiceResponse<MessageDto>.Ok(messageObj));
        }
    }
}