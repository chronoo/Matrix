using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExceptionHelper
{
    public class ExcepthionHelper
    {
		public static string GetMessage(Exception ex)
		{
            /*if(ex.Message== "Unable to connect to any of the specified MySQL hosts.")
            {
                return "Ошибка соединения с базой данной";
            }*/
            try
			{
				var @switch = new Dictionary<Type, Func<Exception, string>> {
				{ typeof(MySqlException), MySQLException},
				{ typeof(ArgumentException),ArgumentException},
				{ typeof(InvalidOperationException),InvalidOperationException},
				{ typeof(WebException),WebException},
				{ typeof(UriFormatException),UriFormatException},
                { typeof(Exception),Exception}
            };
				return @switch[ex.GetType()](ex);
			}
			catch
			{
				return "Неизвестная ошибка";
			}
		}
		private static string InvalidOperationException(Exception ex)
		{
			InvalidOperationException opEx = ex as InvalidOperationException;
			return "Ошибка базы данных";
		}
        private static string Exception(Exception ex)
        {
            switch (ex.Message)
            {
                case "LatexError":
                    return "Ошибка создания pdf-документа";
                case "TaskNotFind":
                    return "Файл задания не найден";
                default:                    
                    return "Ошибка приложения";
            }
        }
        private static string UriFormatException(Exception ex)
		{
			UriFormatException uriEx = ex as UriFormatException;
			return "Ошибка в строке соединения";
		}
		private static string ArgumentException(Exception ex)
		{
			ArgumentException argEx = ex as ArgumentException;
			switch (argEx.Message)
			{
				case "Формат строки инициализации не соответствует спецификации, начиная с индекса 0.":
					return "Ошибка соединения с базой данной";
				default:
					return "Ошибка исходных данных";
			}
		}
		private static string MySQLException(Exception ex)
		{
			MySqlException mysqlEx = ex as MySqlException;
			switch (mysqlEx.Number)
			{
				case 0:
					return "Невозможно соединиться с сервером базы данных";
				case 1042:
					return "Невозможно соединиться с сервером базы данных";
				case 1045:
					return "Невозможно соединиться с сервером базы данных";
				default:
					return "Ошибка соединения с базой данной";
			}
		}
		private static string WebException(Exception ex)
		{
			WebException mysqlEx = ex as WebException;
			var response = mysqlEx.Response as HttpWebResponse;
			if (response == null)
			{
				return "Не удалось установить соединение";
			}
			else
			{
				switch (response.StatusCode)
				{
					case HttpStatusCode.Unauthorized:
						return "Неправильный пароль";
					case HttpStatusCode.NotFound:
						return "Пользователя не существует";
					default:
						return "Ошибка соединения";
				}
			}
		}
	}
}
