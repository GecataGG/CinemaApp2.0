namespace CinemaApp.Data.Common
{
    public static class EntityValidation
    {
        public static class Movie
        {
            public const int TitleMaxLength = 100;
            public const int GenreMaxLength = 50;
            public const int DirectorMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
            public const int ImageUrlMaxLength = 2048;
        }

        public static class Cinema
        {
            public const int NameMaxLength = 80;
            public const int LocationMaxLength = 50;
        }

        public static class Projection
        {
            public const string TicketPriceType = "decimal(9,2)";
        }

        public static class Ticket
        {
            public const int MinQuantity = 1;
            public const int MaxQuantity = 20;
        }
    }
}