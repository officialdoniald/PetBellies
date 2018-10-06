﻿using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetBellies.BLL.ViewModel
{
    public class SearchFragmentViewModel
    {
        public List<Hashtags> GetHashtags()
        {
            return GlobalVariables.databaseConnection.GetHashtags();
        }

        public List<SearchModel> GetSearchModel()
        {
            List<Hashtags> hashtags = GetHashtags();

            var hastagsordered = hashtags.OrderBy(a => a.hashtag).GroupBy(a => a.hashtag);

            List<SearchModel> searchModelList = new List<SearchModel>();

            List<Petpictures> petpictures = new List<Petpictures>();

            SearchModel searchModel = new SearchModel();

            searchModelList.Add(new SearchModel()
            {
                hashtag = "#all",
                petpicturesList = GlobalVariables.databaseConnection.GetPetpictures()
            });

            foreach (var item in hastagsordered)
            {
                foreach (var item1 in item)
                {
                    petpictures.Add(GlobalVariables.databaseConnection.GetOnePetpicturesByID(item1.petpicturesid));
                }

                searchModel.hashtag = "#" + item.Key;
                searchModel.petpicturesList = petpictures;

                searchModelList.Add(searchModel);

                petpictures = new List<Petpictures>();

                searchModel = new SearchModel();
            }

            return searchModelList;
        }

        public List<SearchModel> GetSearchModelWithKeyword(string searchword, List<SearchModel> searchModelList)
        {
            List<SearchModel> searchablelist = new List<SearchModel>();

            if (!String.IsNullOrEmpty(searchword))
            {
                searchablelist = (

                                 from g
                                 in searchModelList
                                 where g.hashtag.Contains(searchword)
                                 select g

                                 ).ToList();
            }
            else
            {
                return searchModelList;
            }

            return searchablelist;
        }

        public List<Petpictures> GetPetpictures()
        {
            List<Petpictures> petpicturelist = GlobalVariables.databaseConnection.GetPetpictures();

            return ShuffleList<Petpictures>(petpicturelist);
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
