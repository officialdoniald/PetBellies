﻿using Newtonsoft.Json;
using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PetBellies.DAL
{
    public class DatabaseConnections
    {
        private Segédfüggvények Segédfüggvények = new Segédfüggvények();

        #region GetFunctions
        
        //All
        public List<WallFromDB> GetWallItemByUserID()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<WallFromDB>>(Segédfüggvények.RequestJson("Wall/GetWallItemByUserID/" + GlobalVariables.ActualUser.id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Indexed
        public List<WallFromDB> GetWallItemByUserIDBYRange()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<WallFromDB>>(Segédfüggvények.RequestJson("Wall/GetWallItemByUserIDBYRange?useriD=" + GlobalVariables.ActualUser.id + "&from=" + GlobalVariables.WallStartIndex +"&count=" + GlobalVariables.WallCount));
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Indexed
        public List<int> GetPetPicturesByRange()
        {
            try
            {
                var list = JsonConvert.DeserializeObject<List<int>>(Segédfüggvények.RequestJson("PetPictures/GetPetPicturesByRange?from=" + GlobalVariables.PetPicturesStartIndex + "&count=" + GlobalVariables.PetPicturesCount));

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<User>>(Segédfüggvények.RequestJson("Users/GetUsers"));
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public string GetWebApiURL()
        {
            try
            {
                return JsonConvert.DeserializeObject<string>(Segédfüggvények.RequestJson("CasualRequests/GetWebApiURL"));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Petpictures> GetPetpictures()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Petpictures>>(Segédfüggvények.RequestJson("Petpictures/GetPetPictures"));
            }
            catch (Exception)
            {
                return new List<Petpictures>();
            }
        }

        public List<int> GetPetpicturesByHashtags(string id)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<int>>(Segédfüggvények.RequestJson("Petpictures/GetPetpicturesByHashtags/" + id));
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }

        public List<int> GetPetpicturesIDSByPetID(int id)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<int>>(Segédfüggvények.RequestJson("Petpictures/GetPetpicturesIDSByPetID/" + id));
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }

        public List<int> GetPetpicturesIDS()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<int>>(Segédfüggvények.RequestJson("Petpictures/GetPetPictureIDS"));
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }

        public List<Hashtags> GetHashtags()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Hashtags>>(Segédfüggvények.RequestJson("Hashtags/GetHashtags"));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] GetCasualImage()
        {
            try
            {
                return JsonConvert.DeserializeObject<byte[]>(Segédfüggvények.RequestJson("CasualImage/GetCasualImage"));
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region GetByIDFunctions

        public List<User> GetUsersByKeyword(string keyword)
        {
            return GetUsers();
        }

        public User GetUserByEmail(string Email)
        {
            try
            {
                return JsonConvert.DeserializeObject<User>(Segédfüggvények.RequestJson("Users/GetUserByEmail/" + Email));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUserByID(int ID)
        {
            try
            {
                return JsonConvert.DeserializeObject<User>(Segédfüggvények.RequestJson("Users/GetUserById/" + ID));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Pet GetPetByID(int ID)
        {
            try
            {
                return JsonConvert.DeserializeObject<Pet>(Segédfüggvények.RequestJson("Pets/GetPetByID/" + ID));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Pet> GetPetsByUserID(int UserID)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Pet>>(Segédfüggvények.RequestJson("Pets/GetPetsByUserID/" + UserID));
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public List<Petpictures> GetPetPicturesByPetID(int ID)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Petpictures>>(Segédfüggvények.RequestJson("Petpictures/GetPetPicturesByPetID/" + ID));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Likes> GetLikesOnAPicture(int ID)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Likes>>(Segédfüggvények.RequestJson("Likes/GetLikesOnAPicture/" + ID));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool GetLikesByPetpicturesIDAndUserID(int userid, int petpicturesid)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(Segédfüggvények.RequestJson("CasualRequests/GetLikesByPetpicturesIDAndUserID?userid=" + userid + "&petpicturesid=" + petpicturesid));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool HaveIFollowedThisPet(int userID, int petid)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(Segédfüggvények.RequestJson("CasualRequests/HaveIFollowedThisPet?id=" + petid + "&userid=" + userID));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BlockedPeople> BlockedPeopleList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<BlockedPeople>>(Segédfüggvények.RequestJson("Blockedpeople/BlockedPeopleList/" + GlobalVariables.ActualUser.id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Petpictures GetPetPictureByID(int ID)
        {
            try
            {
                return JsonConvert.DeserializeObject<Petpictures>(Segédfüggvények.RequestJson("Petpictures/GetPetPictureByID/" + ID));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Following> GetFollowingByfuserID(int petid)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Following>>(Segédfüggvények.RequestJson("Following/GetFollowersByPetID/" + petid));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Following> GetFollowingByuserID(int userID)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Following>>(Segédfüggvények.RequestJson("Following/GetFollowersByUserID/" + userID));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Hashtags> GetHashtagsByPetpictureID(int petpicturesid)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Hashtags>>(Segédfüggvények.RequestJson("Hashtags/GetHashtagsByPetPicturesID/" + petpicturesid));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool GetPetpicturesExistByPetPicturesID(int petpicturesid)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(Segédfüggvények.RequestJson("Petpictures/GetPetpicturesExistByPetPicturesID/" + petpicturesid));
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region InsertFunctions

        public bool AddUser(User user)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Post, user, "Users/AddUser");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int AddPet(Pet pet)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Post, pet, "Pets/AddPet");

            return JsonConvert.DeserializeObject<int>(message.Content.ReadAsStringAsync().Result);
        }

        public int UploadPhoto(Petpictures petpictures)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Post, petpictures, "Petpictures/UploadPhoto");

            return JsonConvert.DeserializeObject<int>(message.Content.ReadAsStringAsync().Result);
        }

        public bool InsertBlockedPeople(BlockedPeople blockedPeople)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Post, blockedPeople, "Blockedpeople/InsertBlockeduser");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertLikes(Likes likes)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Post, likes, "Likes/InsertLike");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertFollowing(Following following)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Post, following, "Following/InsertFollowing");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertHashtags(Hashtags hashtags)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Post, hashtags, "Hashtags/Inserthashtag");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region UpdateFunctions

        public bool UpdateUser(int ID, User user)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Put, user, "Users/UpdateUser");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ForgotPasswordAsync(int ID, User user)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Put, user, "Users/ForgotPasswordAsync");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatePet(int ID, Pet pet)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Put, pet, "Pets/UpdatePet");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatePetpicturesReported(Petpictures ID)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Put, ID, "Petpictures/ReportImage");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateUserReported(User ID)
        {
            var message = Segédfüggvények.PostPut(HttpMethod.Put, ID, "Users/ReportUser");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region DeleteFunctions

        public bool DeletePet(Pet pet)
        {
            var message = Segédfüggvények.Delete("Pets/DeletePet/" + pet.id);

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteBlockedPeople(BlockedPeople blockedPeople)
        {
            var message = Segédfüggvények.Delete("Blockedpeople/DeleteBlockedUser", blockedPeople);

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteLikesNotByParam(Likes likes)
        {
            var message = Segédfüggvények.Delete("Likes/RemoveLike", likes);

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletePetpictures(Petpictures petpictures)
        {
            var message = Segédfüggvények.Delete("Petpictures/DeleteImage", petpictures);

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteFollowing(int userid, int petid)
        {
            var following = JsonConvert.DeserializeObject<Following>(Segédfüggvények.RequestJson("Following/GetFollowingByUserIDAndPetID?userid=" + userid + "&petid=" + petid));

            var message = Segédfüggvények.Delete("Following/DeleteFollowing", following);

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAccount(int UserID)
        {
            var message = Segédfüggvények.Delete("Users/DeleteUser/" + UserID);

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Casual

        public bool IsItABlockedUser(int ID, int blockedid)
        {
            return JsonConvert.DeserializeObject<bool>(Segédfüggvények.RequestJson("CasualRequests/IsItABlockedUser?userid" + ID + "&blockedid=" + blockedid));
        }

        #endregion
    }
}