using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using GivskudApp.Models;
using GivskudApp.Services;

namespace GivskudApp.ViewModel
{
    class AnimalInfopageViewModel {

        //public AnimalModel Data {get; set;}
        //public AnimalInfopageViewModel(AnimalModel Model) => Data = Model;

        //public string AnimalInfoHeight {
        //    get {
        //        if(Data.Information.Height.Count == 0) {
        //            return "n/a";
        //        } else if(Data.Information.Height.Count == 1) {
        //            return Data.Information.Height[0].ToString() + " cm";
        //        } else {
        //            string returnval = "";
        //            for(int i = 0; i < Data.Information.Height.Count; i++) {
        //                returnval += Data.Information.Height[i].ToString();
        //                if(i != Data.Information.Height.Count - 1) {
        //                    returnval += "-";
        //                }
        //            }
        //            returnval += " cm";
        //            return returnval;
        //        }
        //    }   
        //    set {}
        //}
        //public string AnimalInfoLength {
        //    get {
        //       if(Data.Information.Length.Count == 0) {
        //            return "n/a";
        //        } else if(Data.Information.Length.Count == 1) {
        //            return Data.Information.Length[0].ToString() + " cm";
        //        } else {
        //            string returnval = "";
        //            for(int i = 0; i < Data.Information.Length.Count; i++) {
        //                returnval += Data.Information.Length[i].ToString();
        //                if(i != Data.Information.Length.Count - 1) {
        //                    returnval += "-";
        //                }
        //            }
        //            returnval += " cm";
        //            return returnval;
        //        }
        //    }   
        //    set {}
        //}
        //public string AnimalInfoWeight {
        //    get {
        //        if(Data.Information.Weight.Count == 0) {
        //            return "n/a";
        //        } else if(Data.Information.Weight.Count == 1) {
        //            return Data.Information.Weight[0].ToString() + " kg";
        //        } else {
        //            string returnval = "";
        //            for(int i = 0; i < Data.Information.Weight.Count; i++) {
        //                returnval += Data.Information.Weight[i].ToString();
        //                if(i != Data.Information.Weight.Count - 1) {
        //                    returnval += "-";
        //                }
        //            }
        //            returnval += " kg";
        //            return returnval;
        //        }
        //    }   
        //    set {}
        //}
        //public string AnimalInfoPDays {
        //    get {
        //        return Data.Information.PDays + " Dage";
        //    }   
        //    set {}
        //}
        //public string AnimalInfoDescendants {
        //    get {
        //        if(Data.Information.Descendants.Count == 0) {
        //            return "n/a";
        //        } else if(Data.Information.Descendants.Count == 1) {
        //            return Data.Information.Descendants[0].ToString() + " Stk/Young";
        //        } else {
        //            string returnval = "";
        //            for(int i = 0; i < Data.Information.Descendants.Count; i++) {
        //                returnval += Data.Information.Descendants[i].ToString();
        //                if(i != Data.Information.Descendants.Count - 1) {
        //                    returnval += "-";
        //                }
        //            }
        //            returnval += " Stk/Young";
        //            return returnval;
        //        }
        //    }   
        //    set {}
        //}
        //public string AnimalInfoLifetime {
        //    get {
        //        if(Data.Information.Lifetime.Count == 0) {
        //            return "n/a";
        //        } else {
        //            string returnval = "";
        //            foreach(int i in Data.Information.Lifetime) {
        //                returnval += i.ToString();
        //            }
        //            return returnval;
        //        }
        //    }   
        //    set {}
        //}
        //public string AnimalInfoContinent {
        //    get {
        //        if(Data.Information.Continent.Count == 0) {
        //            return "n/a";
        //        } else {
        //            string returnval = "";
        //            for(int i = 0; i < Data.Information.Continent.Count; i++) {
        //                returnval += Data.Information.Continent[i];
        //                if(i != Data.Information.Continent.Count - 1) {
        //                    returnval += "/";
        //                }
        //            }
        //            return returnval;
        //        }
        //    }   
        //    set {}
        //}
        //public string AnimalInfoSpecies {
        //    get {
        //        return Data.Information.Species;
        //    }   
        //    set {}
        //}
        //public string AnimalInfoStatus {
        //    get {
        //        return Data.Information.Status;
        //    }   
        //    set {}
        //}
        //public string AnimalInfoEats{
        //    get {
        //        return Data.Information.Eats;
        //    }   
        //    set {}
        //}
        //public string AnimalHeaderImage {
        //    get {
        //        return Data.Image;
        //    }
        //    set {}
        //}
        //public string AnimalHeaderTitle {
        //    get {
        //        return Data.Name;
        //    }
        //    set {}
        //}
    }
}
