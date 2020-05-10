using System;

namespace PetBellies.BLL.Helper
{
    public class GlobalEvents
    {
        public static event EventHandler<object> OnPetAdded;

        public static void OnPetAdded_Event(object sender, object args)
        {
            OnPetAdded?.Invoke(sender, args);
        }

        public static event EventHandler<object> OnPetDeleted;

        public static void OnPetDeleted_Event(object sender, object args)
        {
            OnPetDeleted?.Invoke(sender, args);
        }

        public static event EventHandler<object> OnProfileUpdated;

        public static void OnProfileUpdated_Event(object sender, object args)
        {
            OnProfileUpdated?.Invoke(sender, args);
        }

        public static event EventHandler<object> OnProfilePictureUpdated;

        public static void OnProfilePictureUpdated_Event(object sender, object args)
        {
            OnProfilePictureUpdated?.Invoke(sender, args);
        }

        public static event EventHandler<object> OnFollowUser;

        public static void OnFollowUser_Event(object sender, object args)
        {
            OnFollowUser?.Invoke(sender, args);
        }

        public static event EventHandler<object> OnUnFollowUser;

        public static void OnUnFollowUser_Event(object sender, object args)
        {
            OnUnFollowUser?.Invoke(sender, args);
        }
    }
}