﻿using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Chat.Services.Implementations
{
    /// <summary>
    /// Базовый класс сервисов
    /// </summary>
    public class BaseService
    {
        protected static readonly string NotMemberKicker = "Только участники беседы могут исключать пользователей";
        protected static readonly string NotMemberRemovee = "Только участники беседы могут быть исключены из нее";
        protected static readonly string NotMemberSender = "Только участники беседы могут писать сообщения";
        protected static readonly string UserNotFound = "Пользователь не найден";
        protected static readonly string GroupNotFound = "Групповой чат не найден";

        protected static readonly string NotMemberAdder =
            "Только участники беседы могут добавлять в нее новых пользователей";

        protected static readonly string TheChatAlreadyExists = "Пользователь уже участник беседы";
        protected static readonly string HistoryAccessDenied = "Вы не можете просматривать чужие сообщения";

        protected static readonly string ChatInfoAccessDenied =
            "Вы не можете получать информацию о чатах, в которых не состоите";

        protected static readonly string BindingNotSucceed = "Не удалось начать чат";
        protected static readonly string GuidIsNotCorrect = "Некорректный идентификатор пользователя или чата";
        protected static readonly string LoginIsNotCorrect = "Логин не соответствует формату";
        protected static readonly string UnableToAdd = "Не удалось создать нового пользователя или группу";
        protected static readonly string NumberNotPositive = "Числовой параметр меньше нуля";
        protected static readonly string EmptyContentNotAllowed = "Невозможно отправить пустое содержимое сообщения";

        protected readonly ILogger<BaseService> Logger;

        protected BaseService(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<BaseService>();
        }

        /// <summary>
        /// Выполняет функцию, обворачивая ее в try-catch
        /// </summary>
        /// <param name="function">Функция</param>
        protected ServiceResponse<T> ExecuteWithCatchGeneric<T>(Func<ServiceResponse<T>> function)
        {
            try
            {
                var result = function();
                if (result.ResultType == ServiceResultType.Warning)
                    Logger.LogWarning(result.ErrorMessage);
                return result;
            }
            catch (SqlException e)
            {
                Logger.LogError(e, e.Message);
                return ServiceResponse<T>.Fail(e);
            }
        }

        /// <summary>
        /// Выполняет функцию, обворачивая ее в try-catch
        /// </summary>
        /// <param name="function">Функция</param>
        protected ServiceResponse ExecuteWithCatch(Func<ServiceResponse> function)
        {
            try
            {
                var result = function();
                if (result.ResultType == ServiceResultType.Warning)
                    Logger.LogWarning(result.ErrorMessage);
                return result;
            }
            catch (SqlException e)
            {
                Logger.LogError(e, e.Message);
                return ServiceResponse.Fail(e);
            }
        }
    }
}