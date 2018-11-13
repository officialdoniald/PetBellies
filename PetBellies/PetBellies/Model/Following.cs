namespace PetBellies.Model
{
    public class Following
    {
        public int id { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Pet, that the user follows.
        /// </summary>
        public int FUserID { get; set; }
    }
}
