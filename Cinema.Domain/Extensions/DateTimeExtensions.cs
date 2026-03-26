namespace onlineCinema.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime birthDate, DateTime today)
        {
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
