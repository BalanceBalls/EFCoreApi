namespace EFCoreApi.Models.Exception
{
	public class DataBaseException : BaseException
	{
		/// <summary>
		/// Текст исключения.
		/// </summary>
		private const string ErrorMessage = "Ошибка при взаимодействии с БД.";

		/// <summary>
		/// Конструктор по типу исключения и тексту ошибки.
		/// </summary>
		public DataBaseException(string errorDescription) : base(ErrorMessage, errorDescription)
		{
		}
	}
}