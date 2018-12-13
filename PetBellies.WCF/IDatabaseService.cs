using PetBellies.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace PetBellies.WCF
{
    [ServiceContract]
    interface IDatabaseService
    {
        #region GetFunctions

        [OperationContract]
        List<User> GetUsers();
        [OperationContract]
        List<Pet> GetPets();
        [OperationContract]
        List<Donates> GetDonates();
        [OperationContract]
        List<Petpictures> GetPetpictures();
        [OperationContract]
        List<Likes> GetLikes();
        [OperationContract]
        List<Favoritepets> GetFavoritepets();
        [OperationContract]
        List<Following> GetFollowing();
        [OperationContract]
        List<Hashtags> GetHashtags();
        [OperationContract]
        List<User> GetUsersByKeyword(string keyword);
        [OperationContract]
        byte[] GetGlobalCasualImage();

        #endregion

        #region GetByIDFunctions
        [OperationContract]
        User GetUserByEmail(string Email);
        [OperationContract]
        User GetUserByID(int ID);
        [OperationContract]
        Pet GetPetByID(int ID);
        [OperationContract]
        List<Pet> GetPetsByUserID(int UserID);
        [OperationContract]
        Donates GetDonateByID(int ID);
        [OperationContract]
        List<Petpictures> GetPetpictureByID(int ID);
        [OperationContract]
        List<Likes> GetLikeByPetpicturesID(int ID);
        [OperationContract]
        Likes GetLikeByUserID(int userid, int petpicturesid);
        [OperationContract]
        Favoritepets GetFavoritepetByID(int ID);
        [OperationContract]
        Following GetFollowingByID(int userID, int petid);
        [OperationContract]
        List<BlockedPeople> GetBlockedPeopleByID();
        [OperationContract]
        Petpictures GetOnePetpicturesByID(int ID);
        [OperationContract]
        List<Following> GetFollowingByfuserID(int petid);
        [OperationContract]
        List<Following> GetFollowingByuserID(int userID);
        [OperationContract]
        List<Hashtags> GetHashtagsByPetpictureID(int petpicturesid);

        #endregion

        #region InsertFunctions
        [OperationContract]
        bool InsertUser(User user);
        [OperationContract]
        int InsertPet(Pet pet);
        [OperationContract]
        bool InsertDonates(Donates donates);
        [OperationContract]
        int InsertPetpictures(Petpictures petpictures);
        [OperationContract]
        bool InsertBlockedPeople(BlockedPeople blockedPeople);
        [OperationContract]
        bool InsertFavoritepets(Favoritepets favoritepets);
        [OperationContract]
        bool InsertLikes(Likes likes);
        [OperationContract]
        bool InsertFollowing(Following following);
        [OperationContract]
        bool InsertHashtags(Hashtags hashtags);

        #endregion

        #region UpdateFunctions
        [OperationContract]
        bool UpdateUser(int ID, User user);
        [OperationContract]
        bool UpdatePet(int ID, Pet pet);
        [OperationContract]
        bool UpdateDonates(int ID, Donates donates);
        [OperationContract]
        bool UpdatePetpictures(int ID, Petpictures petpictures);
        [OperationContract]
        bool UpdateFavoritepets(int ID, Favoritepets favoritepets);
        [OperationContract]
        bool UpdateLikes(int ID, Likes likes);
        [OperationContract]
        bool UpdateFollowing(int ID, Following following);

        #endregion

        #region DeleteFunctions
        [OperationContract]
        bool DeleteUser(User user);
        [OperationContract]
        bool DeletePet(Pet pet);
        [OperationContract]
        bool DeleteDonates(Donates donates);
        [OperationContract]
        bool DeleteFavoritepets(Favoritepets favoritepets);
        [OperationContract]
        bool DeleteBlockedPeople(BlockedPeople blockedPeople);
        [OperationContract]
        bool DeleteLikes(Likes likes);
        [OperationContract]
        bool DeleteLikesNotByParam(Likes likes);
        [OperationContract]
        bool DeletePetpictures(Petpictures petpictures);
        [OperationContract]
        bool DeleteFollowing(int userid, int petid);
        [OperationContract]
        bool DeleteAccount(int UserID);
        [OperationContract]
        bool DeletePetpictures(int PetID, int userid);

        #endregion
    }
}
