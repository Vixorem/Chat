using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Chat.Options;
using Microsoft.Extensions.Options;

namespace Chat.DbUtils
{
    /// <summary>
    /// Класс отвечает за запросы к базе данных
    /// </summary>
    public class DbRequest
    {
        /// <summary>
        /// Содержит строку соединения
        /// </summary>
        private readonly string _connectionString;

        //.ctor
        public DbRequest(IOptions<ConnectionStringOptions> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }

        /// <summary>
        /// Создает объект из строки данных
        /// </summary>
        /// <param name="storedProcedureName">Имя хранимой процедуры</param>
        /// <param name="readerFunc">Делегат (колбэк)</param>
        /// <param name="args">Список параметров для процедуры</param>
        /// <typeparam name="T">Тип возвращаемого значения</typeparam>
        public T GetItemFromEntry<T>(string storedProcedureName, Func<IDataReader, T> readerFunc,
            params DbParam[] args)
        {
            using var connect = new SqlConnection(_connectionString);
            connect.Open();
            var cmd = new SqlCommand(storedProcedureName, connect)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var dbParam in args)
            {
                cmd.Parameters.AddWithValue(dbParam.Name, dbParam.Value);
            }

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return readerFunc(reader);
            }

            return default;
        }

        /// <summary>
        /// Создает список объектов из полученных строк
        /// </summary>
        /// <param name="storedProcedureName">Имя хранимой процедуры</param>
        /// <param name="readerFunc">Делегат (колбэк). </param>
        /// <param name="args">Список параметров для процедуры</param>
        /// <typeparam name="T">Тип объектов в списке</typeparam>
        public List<T> GetItemListFromEntries<T>(string storedProcedureName, Func<IDataReader, T> readerFunc,
            params DbParam[] args)
        {
            var result = new List<T>();
            using var connect = new SqlConnection(_connectionString);
            connect.Open();
            var cmd = new SqlCommand(storedProcedureName, connect)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var dbParam in args)
            {
                cmd.Parameters.AddWithValue(dbParam.Name, dbParam.Value);
            }

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(readerFunc(reader));
            }

            return result;
        }

        /// <summary>
        /// Выполняет запрос
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="args"></param>
        /// <returns>Кол-во затронутых записей или -1</returns>
        public void  ExecuteNonQuery(string storedProcedureName, params DbParam[] args)
        {
            using var connect = new SqlConnection(_connectionString);
            connect.Open();
            var cmd = new SqlCommand(storedProcedureName, connect)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var dbParam in args)
            {
                cmd.Parameters.AddWithValue(dbParam.Name, dbParam.Value);
            }

            cmd.ExecuteNonQuery();
        }
    }
}