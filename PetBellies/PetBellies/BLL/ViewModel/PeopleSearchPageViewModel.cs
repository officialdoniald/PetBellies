using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetBellies.BLL.ViewModel
{
    public class PeopleSearchPageViewModel
    {
        public List<User> GetUserWithKeyword(string keyword)
        {
            return ShuffleList<User>(GlobalVariables.databaseConnection.GetUsersByKeyword(keyword));
        }

        public List<UserJustWithPicAndName> GetUserByKeyWord(string keyword, List<UserJustWithPicAndName> users)
        {
            List<UserJustWithPicAndName> searchablelist = new List<UserJustWithPicAndName>();

            if (!String.IsNullOrEmpty(keyword))
            {
                searchablelist = (
                                 from g
                                 in users
                                 where g.Name.Contains(keyword)
                                 select g
                                 ).ToList();
            }
            else
            {
                return searchablelist;
            }

            return searchablelist;
        }


        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
    }
}
