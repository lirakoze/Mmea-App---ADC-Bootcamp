using MmeaAppADC.Models;
using System.Collections.Generic;

namespace MmeaAppADC.Services
{
    public static class CountyData
    {

        public static List<County> Counties = new List<County>()
        {
            new County
            {
                Name="Nairobi",
                SubCounties =
                {
                    new SubCounty{Name="Dagoretti"},
                    new SubCounty{Name="Embakasi"},
                    new SubCounty{Name="Kasarani"},
                }
            },

            new County
            {
                Name="Kisumu"
            },
            new County
            {
                Name="Mombasa"
            },
            new County
            {
                Name="Kajiado"
            },
            new County
            {
                Name="Bungoma"
            },
            new County
            {
                Name="Busia"
            },
            new County
            {
                Name="Kisii"
            },
            new County
            {
                Name="Meru"
            }
        };
    }
}
