using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PetBellies.BLL.ViewModel
{
    public class SearchFragmentViewModel
    {
        public List<Hashtags> GetHashtags()
        {
            return GlobalVariables.databaseConnection.GetHashtags();
        }

        public ObservableCollection<SearchModel> GetSearchModel()
        {
            ObservableCollection<Hashtags> hashtags = new ObservableCollection<Hashtags>(GetHashtags());

            var hastagsordered = hashtags.OrderBy(a => a.hashtag).GroupBy(a => a.hashtag);

            ObservableCollection<SearchModel> searchModelList = new ObservableCollection<SearchModel>();

            ObservableCollection<Petpictures> petpictures = new ObservableCollection<Petpictures>();

            SearchModel searchModel = new SearchModel();

            //searchModelList.Add(new SearchModel()
            //{
            //    hashtag = "#all",
            //});

            foreach (var item in hastagsordered)
            {
                searchModel.hashtag = "#" + item.Key;
                searchModelList.Add(searchModel);
                searchModel = new SearchModel();
            }

            return searchModelList;
        }

        public ObservableCollection<SearchModel> GetSearchModelWithKeyword(string searchword, ObservableCollection<SearchModel> searchModelList)
        {
            ObservableCollection<SearchModel> searchablelist = new ObservableCollection<SearchModel>();

            if (!String.IsNullOrEmpty(searchword))
            {
                searchablelist = new ObservableCollection<SearchModel>((

                                 from g
                                 in searchModelList
                                 where g.hashtag.Contains(searchword)
                                 select g

                                 ).ToList());
            }
            else
            {
                return searchModelList;
            }

            return searchablelist;
        }

        public List<int> GetPetpictures()
        {
            return GlobalVariables.databaseConnection.GetPetPicturesByRange();
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
