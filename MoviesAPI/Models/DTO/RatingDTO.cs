namespace MoviesAPI.Models.DTO
{
    public class RatingDTO
    {
        public short Score { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }

        public override bool Equals(object obj)
        {
            bool areEqual = false;

            if (obj != null)
            {
                Rating rating = obj as Rating;
                if (rating != null)
                    areEqual = rating.UserId == UserId &&
                            rating.MovieId == MovieId;
            }
            return areEqual;
        }

        public override int GetHashCode()
        {
            return (UserId.GetHashCode() + MovieId.GetHashCode()) / 2;
        }
    }
}