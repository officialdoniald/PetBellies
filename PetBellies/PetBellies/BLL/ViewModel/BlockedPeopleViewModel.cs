using PetBellies.BLL.Helper;
using PetBellies.Model;
using System.Collections.Generic;

namespace PetBellies.BLL.ViewModel
{
    public class BlockedPeopleViewModel
    {
        public List<BlockedPeople> GetBlockedPeoples()
        {
            return GlobalVariables.databaseConnection.BlockedPeopleList();
        }

        public string InsertBlockedPeople(BlockedPeople blockedPeople)
        {
            // kész - homepage-ről például kiszedni
            // kész - ahol csak profilkép van onnan nem kell, csak a profil oldalakrol
            // kész - ellenőrzés a hashtagnél is hogy olyanokat ne jelenítsen meg, 
            //   ami blokkolt usertől van, illetve amikor rákeresünk egy hashtegre
            //   ott se jelenítse meg azokat

            //Itt majd ki kell kötni, hogy amíg van olyan pet, amit tőle követsz nem blokkolhatod.
            if (true)
            {
                bool success = GlobalVariables.databaseConnection.InsertBlockedPeople(blockedPeople);

                if (success)
                {
                    return string.Empty;
                }
                else
                {
                    return English.SomethingWentWrong();
                }
            }
        }

        public string DeleteBlockedPeople(BlockedPeople blockedPeople)
        {
            bool success = GlobalVariables.databaseConnection.DeleteBlockedPeople(blockedPeople);

            if (success)
            {
                return string.Empty;
            }
            else
            {
                return English.SomethingWentWrong();
            }
        }
    }
}