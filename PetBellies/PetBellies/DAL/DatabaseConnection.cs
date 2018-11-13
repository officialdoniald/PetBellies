using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PetBellies.DAL
{
    public class DatabaseConnections
    {
        private Segédfüggvények Segédfüggvények = new Segédfüggvények();

        #region strings

        //GET
        public static string GET_Pictures_SQL { get; } =
                    "SELECT * FROM [dbo].[Pictures]";
        public static string GET_BlockedPeople_SQL { get; } =
                    "SELECT * FROM [dbo].[Blockedpeople] where UserID=@UserID;";
        public static string GET_USER_SQL { get; } =
                    "SELECT * FROM [dbo].[User]";
        public static string GET_Donates_SQL { get; } =
                     "SELECT * FROM [dbo].[Donates]";
        public static string GET_Favoritepets_SQL { get; } =
                     "SELECT * FROM [dbo].[Favoritepets]";
        public static string GET_Likes_SQL { get; } =
                     "SELECT * FROM [dbo].[Likes]";
        public static string GET_Pet_SQL { get; } =
                     "SELECT * FROM [dbo].[Pet]";
        public static string GET_Petpictures_SQL { get; } =
                     "SELECT * FROM [dbo].[Petpictures]";
        public static string GET_Following_SQL { get; } =
                    "SELECT * FROM [dbo].[Following]";
        public static string GET_HASHTAGS_SQL { get; } =
                    "SELECT * FROM [dbo].[Hashtags]";
        public static string GET_GlobalCasualImage_SQL { get; } =
                    "SELECT CasualImage FROM [dbo].[Table]";
        public static string GET_USERSBYKEYWORD_SQL { get; } =
            //"SELECT * FROM [dbo].[User] Where FirstName LIKE " + '%' + "@keyword" + '%' + " OR LastName= LIKE "+'%'+"@keyword"+'%';
            "SELECT id,Email,FirstName,LastName,ProfilePicture FROM [dbo].[User]";
        
        //GETBYID
        public static string GET_USERBYEMAIL_SQL { get; } =
                    "SELECT * FROM [dbo].[User] WHERE EMAIL=@EMAIL";
        public static string GET_USERBYFACEBOOKID_SQL { get; } =
                    "SELECT * FROM [dbo].[User] WHERE facebookid=@facebookid";
        public static string GET_USERBYID_SQL { get; } =
                    "SELECT * FROM [dbo].[User] WHERE ID=@id";
        public static string GET_DonatesBYID_SQL { get; } =
                    "SELECT * FROM [dbo].[Donates] WHERE ID=@id";
        public static string GET_FavoritepetsBYID_SQL { get; } =
                    "SELECT * FROM [dbo].[Favoritepets] WHERE ID=@id";
        public static string GET_LikesBYPetpicturesID_SQL { get; } =
                    "SELECT * FROM [dbo].[Likes] WHERE Petpicturesid=@id";
        public static string GET_LikesBYuserID_SQL { get; } =
                    "SELECT * FROM [dbo].[Likes] WHERE UserID=@userid AND Petpicturesid=@petpicturesid;";
        public static string GET_PetBYID_SQL { get; } =
                    "SELECT * FROM [dbo].[Pet] WHERE ID=@id";
        public static string GET_PetpicturesBYID_SQL { get; } =
                    "SELECT * FROM [dbo].[Petpictures] WHERE petid=@id";
        public static string GetOnePetpicturesByID_SQL { get; } =
                    "SELECT * FROM [dbo].[Petpictures] WHERE id=@id";
        public static string GET_FollowingBYID_SQL { get; } =
                    "SELECT * FROM [dbo].[Following] WHERE UserID=@userid AND fuserid=@petid";
        public static string GET_FollowingBYfuserID_SQL { get; } =
                    "SELECT * FROM [dbo].[Following] WHERE fuserid=@petid";
        public static string GET_FollowingBYuserID_SQL { get; } =
                    "SELECT * FROM [dbo].[Following] WHERE userid=@petid";
        public static string GET_PetsBYUserID_SQL { get; } =
                    "SELECT * FROM [dbo].[Pet] WHERE Uploader=@UserID";
        public static string GET_HashtagsByPetpicturesID_SQL { get; } =
                    "SELECT * FROM [dbo].[Hashtags] WHERE petpicturesid=@petpicturesid";

        //DELETE
        public static string DELETE_USER_SQL { get; } =
                   "DELETE FROM [dbo].[User] WHERE ID=@id";
        public static string DELETE_BlockedPeople_SQL { get; } =
                   "DELETE FROM [dbo].[Blockedpeople] WHERE UserID=@UserID AND BlockedUserID=@BlockedUserID;"+
                   "DELETE FROM [dbo].[Blockedpeople] WHERE BlockedUserID=@UserID AND UserID=@BlockedUserID;";
        public static string DELETE_Following_SQL { get; } =
                   "DELETE FROM [dbo].[Following] WHERE userID=@userid AND fuserid=@petid";
        public static string DELETE_Donates_SQL { get; } =
                    "DELETE FROM [dbo].[Donates] WHERE ID=@id";
        public static string DELETE_Favoritepets_SQL { get; } =
                    "DELETE FROM [dbo].[Favoritepets] WHERE ID=@id";
        public static string DELETE_Likes_SQL { get; } =
                    "DELETE FROM [dbo].[Likes] WHERE ID=@id";
        public static string DELETE_LikesByUserIdAndPetPicturesId_SQL { get; } =
                    "DELETE FROM [dbo].[Likes] WHERE UserID=@userid AND Petpicturesid=@petpicturesid;";
        public static string DELETE_Pet_SQL { get; } =
                    "DELETE FROM [dbo].[Favoritepets] WHERE petid=@id;" +
                    "DELETE FROM [dbo].[Following] WHERE FUserID=@id;" +
                    "DELETE FROM [dbo].[Pet] WHERE ID=@id;";
        public static string DELETE_Petpictures_SQL { get; } =
                    "DELETE FROM [dbo].[Following] WHERE userID=@userid;" +
                    "DELETE FROM [dbo].[Likes] WHERE Petpicturesid=@id;" +
                    "DELETE FROM [dbo].[Hashtags] WHERE Petpicturesid=@id;" +
            "DELETE FROM [dbo].[Petpictures] WHERE ID=@id;";
        public static string DELETE_Petpictures_SQL2 { get; } =
                    "DELETE FROM [dbo].[Likes] WHERE Petpicturesid=@id;" +
                    "DELETE FROM [dbo].[Hashtags] WHERE Petpicturesid=@id;" +
                    "DELETE FROM [dbo].[Petpictures] WHERE ID=@id;";
        public static string DELETE_Account_SQL { get; } =
                    "DELETE FROM [dbo].[Favoritepets] WHERE UserID=@UserID;" +
                    "DELETE FROM [dbo].[Following] WHERE UserID=@UserID;" +
                    "DELETE FROM [dbo].[User] WHERE id=@UserID;";
        public static string DELETE_Petpicutres_SQL { get; } =
                    "DELETE FROM [dbo].[Petpictures] WHERE PetID=@PetID;";

        //UPDATE
        public static string UPDATE_USER_SQL { get; } =
            "UPDATE [dbo].[USER] SET " +
            "FirstName=@FirstName," +
            "LastName=@LastName," +
            "FacebookId=@FacebookId," +
            "ProfilePicture=@ProfilePictureURL," +
            "Email=@Email," +
            "Password=@Password" +
            " WHERE ID=@ID";
        public static string UPDATE_Pet_SQL { get; } =
            "UPDATE [dbo].[Pet] SET " +
            "Name=@Name," +
            "Age=@Age," +
            "PetType=@PetType," +
            "HaveAnOwner=@HaveAnOwner," +
            "ProfilePicture=@ProfilePictureURL," +
            "Uploader=@Uploader" +
            " WHERE ID=@ID";
        public static string UPDATE_Donates_SQL { get; } =
            "UPDATE [dbo].[Donates] SET " +
            "UserID=@UserID," +
            "DonateDate=@DonateDate," +
            "HowMany=@HowMany," +
            "CashType=@CashType," +
            "PetID=@PetID" +
            " WHERE ID=@ID";
        public static string UPDATE_Petpictures_SQL { get; } =
            "UPDATE [dbo].[Petpictures] SET " +
            "PetID=@PetID," +
            "PictureURL=@PictureURL," +
            "UploadDate=@UploadDate" +
            " WHERE ID=@ID";
        public static string UPDATE_Likes_SQL { get; } =
            "UPDATE [dbo].[Likes] SET " +
            "Petpicturesid=@PetPictureURL," +
            "UserID=@UserID" +
            " WHERE ID=@ID";
        public static string UPDATE_Favoritepets_SQL { get; } =
            "UPDATE [dbo].[Favoritepets] SET " +
            "UserID=@UserID," +
            "PetID=@PetID" +
            " WHERE ID=@ID";
        public static string UPDATE_Following_SQL { get; } =
            "UPDATE [dbo].[Following] SET " +
            "UserID=@UserID," +
            "FUserID=@FUserID" +
            " WHERE ID=@ID";

        //INSERT
        public static string INSERT_User_SQL { get; } =
            "INSERT INTO [dbo].[USER]" +
            "([FirstName], [LastName], [FacebookId], [ProfilePicture], [Email], [Password]) " +
            "VALUES(" +
            "@FirstName,@LastName,@FacebookId,@ProfilePictureURL,@Email,@Password);";
        public static string INSERT_Pet_SQL { get; } =
            "INSERT INTO [dbo].[Pet]" +
            "([Name], [Age], [PetType], [HaveAnOwner], [ProfilePicture], [Uploader]) " +
            "VALUES(" +
            "@Name,@Age,@PetType,@HaveAnOwner,@ProfilePictureURL,@Uploader);SET @id = SCOPE_IDENTITY();";
        public static string INSERT_Donates_SQL { get; } =
            "INSERT INTO [dbo].[Donates]" +
            "([UserID], [DonateDate], [HowMany], [CashType], [PetID]) " +
            "VALUES(" +
            "@UserID,@DonateDate,@HowMany,@CashType,@PetID);";
        public static string INSERT_Petpictures_SQL { get; } =
            "INSERT INTO [dbo].[Petpictures]" +
            "([PetID], [PictureURL], [UploadDate]) " +
            "VALUES(" +
            "@PetID,@PictureURL,@UploadDate);SET @id = SCOPE_IDENTITY();";
        public static string INSERT_Likes_SQL { get; } =
            "INSERT INTO [dbo].[Likes]" +
            "([Petpicturesid], [UserID]) " +
            "VALUES(" +
            "@Petpicturesid,@UserID);";
        public static string INSERT_Favoritepets_SQL { get; } =
            "INSERT INTO [dbo].[Favoritepets]" +
            "([UserID], [PetID]) " +
            "VALUES(" +
            "@UserID,@PetID);";
        public static string INSERT_Following_SQL { get; } =
            "INSERT INTO [dbo].[Following]" +
            "([UserID], [FUserID]) " +
            "VALUES(" +
            "@UserID,@FUserID);";
        public static string INSERT_BlockedPeople_SQL { get; } =
            "INSERT INTO [dbo].[Blockedpeople]" +
            "([UserID], [BlockedUserID]) " +
            "VALUES(" +
            "@UserID,@BlockedUserID);"+
            "INSERT INTO [dbo].[Blockedpeople]" +
            "([UserID], [BlockedUserID]) " +
            "VALUES(" +
            "@BlockedUserID,@UserID);";
        public static string INSERT_Hashtags_SQL { get; } =
            "INSERT INTO [dbo].[Hashtags]" +
            "([petpicturesid], [hashtag]) " +
            "VALUES(" +
            "@petpicturesid,@hashtag);";

        #endregion

        #region GetFunctions

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_USER_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                User user = new User();

                                user.id = reader.GetInt32(reader.GetOrdinal("id"));
                                user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                user.Email = reader.GetString(reader.GetOrdinal("Email"));
                                user.Password = reader.GetString(reader.GetOrdinal("Password"));
                                try
                                {
                                    user.FacebookId = reader.GetString(reader.GetOrdinal("FacebookId"));
                                }
                                catch (Exception)
                                {
                                    user.FacebookId = null;
                                }
                                if (reader.GetStream(reader.GetOrdinal("ProfilePicture")).Length == 0)
                                    user.ProfilePictureURL = null;
                                else user.ProfilePictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("ProfilePicture")));
                                
                                users.Add(user);
                            }
                        }
                    }
                }
                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Pet> GetPets()
        {
            List<Pet> pets = new List<Pet>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_Pet_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Pet pet = new Pet();

                                pet.id = reader.GetInt32(reader.GetOrdinal("id"));
                                pet.Name = reader.GetString(reader.GetOrdinal("Name"));
                                pet.Age = reader.GetInt32(reader.GetOrdinal("Age"));
                                pet.PetType = reader.GetString(reader.GetOrdinal("pettype"));
                                pet.HaveAnOwner = reader.GetInt32(reader.GetOrdinal("HaveAnOwner"));
                                if (reader.GetStream(reader.GetOrdinal("ProfilePicture")).Length != 0)
                                    pet.ProfilePictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("ProfilePicture")));
                                else pet.ProfilePictureURL = null;
                                pet.Uploader = reader.GetInt32(reader.GetOrdinal("Uploader"));

                                pets.Add(pet);
                            }
                        }
                    }
                }
                return pets;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Donates> GetDonates()
        {
            List<Donates> donates = new List<Donates>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_Donates_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Donates donate = new Donates();

                                donate.id = reader.GetInt32(reader.GetOrdinal("id"));
                                donate.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                donate.DonateDate = reader.GetString(reader.GetOrdinal("DonateDate"));
                                donate.HowMany = reader.GetInt32(reader.GetOrdinal("HowMany"));
                                donate.CashType = reader.GetString(reader.GetOrdinal("CashType"));
                                donate.PetID = reader.GetInt32(reader.GetOrdinal("PetID"));

                                donates.Add(donate);
                            }
                        }
                    }
                }
                return donates;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Petpictures> GetPetpictures()
        {
            List<Petpictures> petpictures = new List<Petpictures>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_Petpictures_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Petpictures petpicture = new Petpictures();

                                petpicture.id = reader.GetInt32(reader.GetOrdinal("id"));
                                petpicture.PetID = reader.GetInt32(reader.GetOrdinal("PetID"));
                                if (reader.GetStream(reader.GetOrdinal("PictureURL")).Length != 0)
                                    petpicture.PictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("PictureURL")));
                                else petpicture.PictureURL = null;
                                petpicture.UploadDate = reader.GetString(reader.GetOrdinal("UploadDate"));

                                petpictures.Add(petpicture);
                            }
                        }
                    }
                }
                return petpictures;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Likes> GetLikes()
        {
            List<Likes> likes = new List<Likes>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_Likes_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Likes like = new Likes();

                                like.id = reader.GetInt32(reader.GetOrdinal("id"));
                                like.Petpicturesid = reader.GetInt32(reader.GetOrdinal("Petpicturesid"));
                                like.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));

                                likes.Add(like);
                            }
                        }
                    }
                }
                return likes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Favoritepets> GetFavoritepets()
        {
            List<Favoritepets> favoritepets = new List<Favoritepets>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_Favoritepets_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Favoritepets favoritepet = new Favoritepets();

                                favoritepet.id = reader.GetInt32(reader.GetOrdinal("id"));
                                favoritepet.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                favoritepet.PetID = reader.GetInt32(reader.GetOrdinal("PetID"));

                                favoritepets.Add(favoritepet);
                            }
                        }
                    }
                }
                return favoritepets;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Following> GetFollowing()
        {
            List<Following> followings = new List<Following>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_Following_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Following following = new Following();

                                following.id = reader.GetInt32(reader.GetOrdinal("id"));
                                following.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                following.FUserID = reader.GetInt32(reader.GetOrdinal("FUserID"));

                                followings.Add(following);
                            }
                        }
                    }
                }
                return followings;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Hashtags> GetHashtags()
        {
            List<Hashtags> hashtags = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_HASHTAGS_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            hashtags = new List<Hashtags>();

                            while (reader.Read())
                            {
                                Hashtags hashtag = new Hashtags();

                                hashtag.id = reader.GetInt32(reader.GetOrdinal("id"));
                                hashtag.petpicturesid = reader.GetInt32(reader.GetOrdinal("petpicturesid"));
                                hashtag.hashtag = reader.GetString(reader.GetOrdinal("hashtag"));

                                hashtags.Add(hashtag);
                            }
                        }
                    }
                }
                return hashtags;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<User> GetUsersByKeyword(string keyword)
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_USERSBYKEYWORD_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                User user = new User();

                                user.id = reader.GetInt32(reader.GetOrdinal("id"));
                                user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                user.Email = reader.GetString(reader.GetOrdinal("Email"));
                                if (reader.GetStream(reader.GetOrdinal("ProfilePicture")).Length != 0)
                                    user.ProfilePictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("ProfilePicture")));
                                else user.ProfilePictureURL = null;

                                users.Add(user);
                            }
                        }
                    }
                }
                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] GetGlobalCasualImage()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_GlobalCasualImage_SQL, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                System.IO.Stream stream = reader.GetStream(reader.GetOrdinal("CasualImage"));

                                if (stream.Length != 0)
                                    return Segédfüggvények.ReadFully(stream);
                                else return null;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region GetByIDFunctions

        public User GetUserByEmail(string Email)
        {
            User user = new User();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_USERBYEMAIL_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@EMAIL", Email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                user.id = reader.GetInt32(reader.GetOrdinal("id"));
                                user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                user.Email = reader.GetString(reader.GetOrdinal("Email"));
                                user.Password = reader.GetString(reader.GetOrdinal("Password"));
                                try
                                {
                                    user.FacebookId = reader.GetString(reader.GetOrdinal("FacebookId"));
                                }
                                catch (Exception)
                                {
                                    user.FacebookId = null;
                                }
                                if (reader.GetStream(reader.GetOrdinal("ProfilePicture")).Length != 0)
                                    user.ProfilePictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("ProfilePicture")));
                                else user.ProfilePictureURL = null;
                            }
                        }
                    }
                }
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUserByID(int ID)
        {
            User user = new User();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_USERBYID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                user.id = reader.GetInt32(reader.GetOrdinal("id"));
                                user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                user.Email = reader.GetString(reader.GetOrdinal("Email"));
                                user.Password = reader.GetString(reader.GetOrdinal("Password"));
                                try
                                {
                                    user.FacebookId = reader.GetString(reader.GetOrdinal("FacebookId"));
                                }
                                catch (Exception)
                                {
                                    user.FacebookId = null;
                                }
                                if(reader.GetStream(reader.GetOrdinal("ProfilePicture")).Length != 0)
                                    user.ProfilePictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("ProfilePicture")));
                                else user.ProfilePictureURL = null;
                            }
                        }
                    }
                }
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Pet GetPetByID(int ID)
        {
            Pet pet = new Pet();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_PetBYID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                pet.id = reader.GetInt32(reader.GetOrdinal("id"));
                                pet.Name = reader.GetString(reader.GetOrdinal("Name"));
                                pet.Age = reader.GetInt32(reader.GetOrdinal("Age"));
                                pet.PetType = reader.GetString(reader.GetOrdinal("pettype"));
                                pet.HaveAnOwner = reader.GetInt32(reader.GetOrdinal("HaveAnOwner"));
                                if (reader.GetStream(reader.GetOrdinal("ProfilePicture")).Length != 0)
                                    pet.ProfilePictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("ProfilePicture")));
                                else pet.ProfilePictureURL = null;
                                pet.Uploader = reader.GetInt32(reader.GetOrdinal("Uploader"));
                            }
                        }
                    }
                }
                return pet;
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                return null;
            }
        }

        public List<Pet> GetPetsByUserID(int UserID)
        {
            List<Pet> pets = new List<Pet>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_PetsBYUserID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Pet pet = new Pet();

                                pet.id = reader.GetInt32(reader.GetOrdinal("id"));
                                pet.Name = reader.GetString(reader.GetOrdinal("Name"));
                                pet.Age = reader.GetInt32(reader.GetOrdinal("Age"));
                                pet.PetType = reader.GetString(reader.GetOrdinal("pettype"));
                                pet.HaveAnOwner = reader.GetInt32(reader.GetOrdinal("HaveAnOwner"));
                                if (reader.GetStream(reader.GetOrdinal("ProfilePicture")).Length != 0)
                                    pet.ProfilePictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("ProfilePicture")));
                                else pet.ProfilePictureURL = null;
                                pet.Uploader = reader.GetInt32(reader.GetOrdinal("Uploader"));

                                pets.Add(pet);
                            }
                        }
                    }
                }
                return pets;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Donates GetDonateByID(int ID)
        {
            Donates donate = new Donates();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_DonatesBYID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                donate.id = reader.GetInt32(reader.GetOrdinal("id"));
                                donate.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                donate.DonateDate = reader.GetString(reader.GetOrdinal("DonateDate"));
                                donate.HowMany = reader.GetInt32(reader.GetOrdinal("HowMany"));
                                donate.CashType = reader.GetString(reader.GetOrdinal("CashType"));
                                donate.PetID = reader.GetInt32(reader.GetOrdinal("PetID"));
                            }
                        }
                    }
                }
                return donate;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Petpictures> GetPetpictureByID(int ID)
        {
            List<Petpictures> petpictures = new List<Petpictures>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_PetpicturesBYID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Petpictures petpicture = new Petpictures();

                                petpicture.id = reader.GetInt32(reader.GetOrdinal("id"));
                                petpicture.PetID = reader.GetInt32(reader.GetOrdinal("PetID"));
                                if (reader.GetStream(reader.GetOrdinal("PictureURL")).Length != 0)
                                    petpicture.PictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("PictureURL")));
                                else petpicture.PictureURL = null;
                                petpicture.UploadDate = reader.GetString(reader.GetOrdinal("UploadDate"));

                                petpictures.Add(petpicture);
                            }
                        }
                    }
                }
                return petpictures;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Likes> GetLikeByPetpicturesID(int ID)
        {
            List<Likes> likes = new List<Likes>();

            Likes like = new Likes();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_LikesBYPetpicturesID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                like = new Likes();

                                like.id = reader.GetInt32(reader.GetOrdinal("id"));
                                like.Petpicturesid = reader.GetInt32(reader.GetOrdinal("Petpicturesid"));
                                like.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));

                                likes.Add(like);
                            }
                        }
                    }
                }
                return likes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Likes GetLikeByUserID(int userid, int petpicturesid)
        {
            Likes like = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_LikesBYuserID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@petpicturesid", petpicturesid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                like = new Likes();

                                like.id = reader.GetInt32(reader.GetOrdinal("id"));
                                like.Petpicturesid = reader.GetInt32(reader.GetOrdinal("Petpicturesid"));
                                like.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                            }
                        }
                    }
                }
                return like;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Favoritepets GetFavoritepetByID(int ID)
        {
            Favoritepets favoritepet = new Favoritepets();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_FavoritepetsBYID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                favoritepet.id = reader.GetInt32(reader.GetOrdinal("id"));
                                favoritepet.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                favoritepet.PetID = reader.GetInt32(reader.GetOrdinal("PetID"));
                            }
                        }
                    }
                }
                return favoritepet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Following GetFollowingByID(int userID, int petid)
        {
            Following following = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_FollowingBYID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@userid", userID);
                    cmd.Parameters.AddWithValue("@petid", petid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                following = new Following();

                                following.id = reader.GetInt32(reader.GetOrdinal("id"));
                                following.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                following.FUserID = reader.GetInt32(reader.GetOrdinal("FUserID"));
                            }
                        }
                    }
                }
                return following;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BlockedPeople> GetBlockedPeopleByID()
        {
            List<BlockedPeople> blockedPeople = new List<BlockedPeople>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_BlockedPeople_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserID", GlobalVariables.ActualUser.id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                BlockedPeople blockedPeopleJustOne = new BlockedPeople();

                                blockedPeopleJustOne.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                                blockedPeopleJustOne.UserID = GlobalVariables.ActualUser.id;
                                blockedPeopleJustOne.BlockedUserID = reader.GetInt32(reader.GetOrdinal("BlockedUserID"));

                                blockedPeople.Add(blockedPeopleJustOne);
                            }
                        }
                    }
                }
                return blockedPeople;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Petpictures GetOnePetpicturesByID(int ID)
        {
            Petpictures petpicture = new Petpictures();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GetOnePetpicturesByID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                petpicture.id = reader.GetInt32(reader.GetOrdinal("id"));
                                petpicture.PetID = reader.GetInt32(reader.GetOrdinal("PetID"));
                                if (reader.GetStream(reader.GetOrdinal("PictureURL")).Length != 0)
                                    petpicture.PictureURL = Segédfüggvények.ReadFully(reader.GetStream(reader.GetOrdinal("ProfilePicture")));
                                else petpicture.PictureURL = null;
                                petpicture.UploadDate = reader.GetString(reader.GetOrdinal("UploadDate"));
                            }
                        }
                    }
                }
                return petpicture;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Following> GetFollowingByfuserID(int petid)
        {
            List<Following> followings = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_FollowingBYfuserID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@petid", petid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            followings = new List<Following>();

                            while (reader.Read())
                            {
                                Following following = new Following();

                                following.id = reader.GetInt32(reader.GetOrdinal("id"));
                                following.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                following.FUserID = reader.GetInt32(reader.GetOrdinal("FUserID"));

                                followings.Add(following);
                            }
                        }
                    }
                }
                return followings;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Following> GetFollowingByuserID(int userID)
        {
            List<Following> followings = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_FollowingBYuserID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@petid", userID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            followings = new List<Following>();

                            while (reader.Read())
                            {
                                Following following = new Following();

                                following.id = reader.GetInt32(reader.GetOrdinal("id"));
                                following.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                following.FUserID = reader.GetInt32(reader.GetOrdinal("FUserID"));

                                followings.Add(following);
                            }
                        }
                    }
                }
                return followings;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Hashtags> GetHashtagsByPetpictureID(int petpicturesid)
        {
            List<Hashtags> hashtags = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                using (SqlCommand cmd = new SqlCommand(GET_HashtagsByPetpicturesID_SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@petpicturesid", petpicturesid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader != null)
                        {
                            hashtags = new List<Hashtags>();

                            while (reader.Read())
                            {
                                Hashtags hashtag = new Hashtags();

                                hashtag.id = reader.GetInt32(reader.GetOrdinal("id"));
                                hashtag.petpicturesid = reader.GetInt32(reader.GetOrdinal("petpicturesid"));
                                hashtag.hashtag = reader.GetString(reader.GetOrdinal("hashtag"));

                                hashtags.Add(hashtag);
                            }
                        }
                    }
                }
                return hashtags;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region InsertFunctions
        
        public bool InsertUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(INSERT_User_SQL, conn);


                    cmd.Parameters.Add(
                        new SqlParameter("@FirstName", user.FirstName)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@LastName", user.LastName)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@FacebookId", user.FacebookId)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@ProfilePictureURL", (object)user.ProfilePictureURL ?? GlobalVariables.GlobalCasualImage)
                        {
                            SqlDbType = System.Data.SqlDbType.Image
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@Email", user.Email)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@Password", user.Password)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int InsertPet(Pet pet)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(INSERT_Pet_SQL, conn);

                    cmd.Parameters.Add(
                        new SqlParameter("@Name", pet.Name)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@Age", pet.Age)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@PetType", pet.PetType)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@HaveAnOwner", pet.HaveAnOwner)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    
                    if (pet.ProfilePictureURL is null)
                    {
                        pet.ProfilePictureURL = GlobalVariables.GlobalCasualImage;
                    }

                    cmd.Parameters.Add(
                       new SqlParameter("@ProfilePictureURL", pet.ProfilePictureURL)
                       {
                           SqlDbType = System.Data.SqlDbType.Image
                       }
                    );

                    cmd.Parameters.Add(
                        new SqlParameter("@Uploader", pet.Uploader)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );

                    var idpar = new SqlParameter("@id", DBNull.Value)
                    {
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idpar);

                    cmd.ExecuteNonQuery();

                    int returnInt = (int)idpar.Value;

                    return returnInt;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool InsertDonates(Donates donates)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(INSERT_Donates_SQL, conn);

                    cmd.Parameters.Add(
                        new SqlParameter("@UserID", donates.UserID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@DonateDate", donates.DonateDate)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@HowMany", donates.HowMany)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@CashType", donates.CashType)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@PetID", donates.PetID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int InsertPetpictures(Petpictures petpictures)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(INSERT_Petpictures_SQL, conn);

                    cmd.Parameters.Add(
                        new SqlParameter("@PetID", petpictures.PetID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@PictureURL", petpictures.PictureURL)
                        {
                            SqlDbType = System.Data.SqlDbType.Image
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@UploadDate", petpictures.UploadDate)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );

                    var idpar = new SqlParameter("@id", DBNull.Value)
                    {
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idpar);

                    cmd.ExecuteNonQuery();

                    int returnInt = (int)idpar.Value;

                    return returnInt;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool InsertBlockedPeople(BlockedPeople blockedPeople)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(INSERT_BlockedPeople_SQL, conn);

                    cmd.Parameters.Add(
                        new SqlParameter("@BlockedUserID", blockedPeople.BlockedUserID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@UserID", blockedPeople.UserID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertFavoritepets(Favoritepets favoritepets)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(INSERT_Favoritepets_SQL, conn);

                    cmd.Parameters.Add(
                        new SqlParameter("@PetID", favoritepets.PetID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@UserID", favoritepets.UserID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertLikes(Likes likes)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(INSERT_Likes_SQL, conn);

                    cmd.Parameters.Add(
                        new SqlParameter("@Petpicturesid", likes.Petpicturesid)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@UserID", likes.UserID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertFollowing(Following following)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(INSERT_Following_SQL, conn);

                    cmd.Parameters.Add(
                        new SqlParameter("@UserID", following.UserID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@FUserID", following.FUserID)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertHashtags(Hashtags hashtags)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(INSERT_Hashtags_SQL, conn);

                    cmd.Parameters.Add(
                        new SqlParameter("@petpicturesid", hashtags.petpicturesid)
                        {
                            SqlDbType = System.Data.SqlDbType.Int
                        }
                     );
                    cmd.Parameters.Add(
                        new SqlParameter("@hashtag", hashtags.hashtag)
                        {
                            SqlDbType = System.Data.SqlDbType.NVarChar
                        }
                     );

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region UpdateFunctions

        public bool UpdateUser(int ID, User user)
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            UPDATE_USER_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@FacebookId", user.FacebookId);
                        cmd.Parameters.AddWithValue("@ProfilePictureURL", user.ProfilePictureURL);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 1) return true;
                        else return false;
                    }
                }
            }
            catch (SqlException e)
            {
                string asd = e.Message;
                return false;
            }
        }

        public bool UpdatePet(int ID, Pet pet)
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            UPDATE_Pet_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@Name", pet.Name);
                        cmd.Parameters.AddWithValue("@Age", pet.Age);
                        cmd.Parameters.AddWithValue("@PetType", pet.PetType);
                        cmd.Parameters.AddWithValue("@HaveAnOwner", pet.HaveAnOwner);
                        cmd.Parameters.AddWithValue("@ProfilePictureURL", pet.ProfilePictureURL);
                        cmd.Parameters.AddWithValue("@Uploader", pet.Uploader);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 1) return true;
                        else return false;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateDonates(int ID, Donates donates)
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            UPDATE_Donates_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@UserID", donates.UserID);
                        cmd.Parameters.AddWithValue("@DonateDate", donates.DonateDate);
                        cmd.Parameters.AddWithValue("@HowMany", donates.HowMany);
                        cmd.Parameters.AddWithValue("@CashType", donates.CashType);
                        cmd.Parameters.AddWithValue("@PetID", donates.PetID);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 1) return true;
                        else return false;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdatePetpictures(int ID, Petpictures petpictures)
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            UPDATE_Petpictures_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PetID", petpictures.PetID);
                        cmd.Parameters.AddWithValue("@PictureURL", petpictures.PictureURL);
                        cmd.Parameters.AddWithValue("@UploadDate", petpictures.UploadDate);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 1) return true;
                        else return false;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateFavoritepets(int ID, Favoritepets favoritepets)
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            UPDATE_Favoritepets_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@UserID", favoritepets.UserID);
                        cmd.Parameters.AddWithValue("@PetID", favoritepets.PetID);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 1) return true;
                        else return false;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateLikes(int ID, Likes likes)
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            UPDATE_Likes_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PetPictureURL", likes.Petpicturesid);
                        cmd.Parameters.AddWithValue("@UserID", likes.UserID);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 1) return true;
                        else return false;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateFollowing(int ID, Following following)
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            UPDATE_Following_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@UserID", following.UserID);
                        cmd.Parameters.AddWithValue("@FUserID", following.FUserID);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 1) return true;
                        else return false;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        #endregion

        #region DeleteFunctions

        public bool DeleteUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_USER_SQL
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@id", user.id);
                        if (cmd.ExecuteNonQuery() == 1) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePet(Pet pet)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_Pet_SQL
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@id", pet.id);
                        if (cmd.ExecuteNonQuery() >= 0) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteDonates(Donates donates)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_Donates_SQL
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@id", donates.id);
                        if (cmd.ExecuteNonQuery() == 1) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFavoritepets(Favoritepets favoritepets)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_Favoritepets_SQL
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@id", favoritepets.id);
                        if (cmd.ExecuteNonQuery() == 1) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBlockedPeople(BlockedPeople blockedPeople)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_BlockedPeople_SQL
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", blockedPeople.UserID);
                        cmd.Parameters.AddWithValue("@BlockedUserID", blockedPeople.BlockedUserID);
                        if (cmd.ExecuteNonQuery() == 2) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteLikes(Likes likes)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_Likes_SQL
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@id", likes.id);
                        if (cmd.ExecuteNonQuery() == 1) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteLikesNotByParam(Likes likes)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_LikesByUserIdAndPetPicturesId_SQL
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@userid", likes.UserID);
                        cmd.Parameters.AddWithValue("@petpicturesid", likes.Petpicturesid);
                        if (cmd.ExecuteNonQuery() == 1) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePetpictures(Petpictures petpictures)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_Petpictures_SQL2
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@id", petpictures.id);
                        if (cmd.ExecuteNonQuery() >= 0) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFollowing(int userid, int petid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_Following_SQL
                        , conn))
                    {

                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Parameters.AddWithValue("@petid", petid);
                        if (cmd.ExecuteNonQuery() == 1) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAccount(int UserID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_Account_SQL
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        if (cmd.ExecuteNonQuery() >= 1) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePetpictures(int PetID, int userid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVariables.AzureDBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        DELETE_Petpictures_SQL
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("@PetID", PetID);
                        cmd.Parameters.AddWithValue("@userid", userid);
                        if (cmd.ExecuteNonQuery() >= 0) return true;
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}