using System;
using System.Collections.Generic;
using System.Text;
using CrossplatApp.Models;

namespace CrossplatApp.DataSources
{
    class NewListData
    {
        
        public List<NewListModel> Cars = new List<NewListModel>()
        {
            new NewListModel(){
                Name="Ritz",
                Make="Maruthi",
               Year="2001"
            },
            new NewListModel(){
                Name="Alto",
                Make="Maruthi",
               Year="2001"
            },
            new NewListModel(){
                Name="compass",
                Make="Jeep",
               Year="2001"
            },
            new NewListModel(){
                Name="Cherokee",
                Make="Jeep",
               Year="2001"
            }

        };



    }
}
