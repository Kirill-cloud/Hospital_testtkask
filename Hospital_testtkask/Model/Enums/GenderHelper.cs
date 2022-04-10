using System;

namespace Hospital_testtkask.Model.Enums
{
	public static class GenderHelper
	{
		public static string FromGender(Gender gender) =>
			gender switch
			{
				Gender.Undefined => "Не определен",
				Gender.Female => "Женский",
				Gender.Male => "Мужской",
				_ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(gender)),
			};
	}
}
