namespace onlineCinema.Validators
{
    public static class ValidationMessages
    {
        public const string NameRegex =
            @"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ\s\-']+$";

        // Загальні
        public const string FieldRequired = "{0} є обов'язковим";
        public const string FieldRequiredFeminine = "{0} є обов'язковою";
        public const string FieldRequiredOption = 
            "Виберіть {0} зі списку або додайте новий";
        public const string FieldTooLong =
            "{0} не може перевищувати {1} символів";
        public const string OnlyLetters =
            "{0} може містити тільки літери";

        // Контакти
        public const string EmailInvalidFormat =
            "Невірний формат електронної пошти (you@gmail.com)";
        public const string PhoneFormat =
            "Формат телефону має бути +380XXXXXXXXX";

        // Пароль
        public const string PasswordMinLength =
            "Пароль має бути не менше 6 символів";
        public const string PasswordFormat =
            "Пароль повинен містити хоча б одну {0}";
        public const string PasswordComplexity =
            @"Пароль повинен містити хоча б одну великую літеру, 
            одну малую літеру та одну цифру";
        public const string PasswordsMismatch = "Паролі не співпадають";

        // Дата народження
        public const string DateOfBirthFuture =
            "Дата народження не може бути в майбутньому";
        public const string DateOfBirthTooOld =
            "Дата народження не може бути раніше 01.01.1900";
    }
}
